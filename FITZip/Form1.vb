Option Explicit On
Option Strict On
Imports FITZip.Form1

'''<summary>Code and decode a file using Shannon Fano coding.</summary>
Public Class Form1

    'Test files:
    ' "C:\Users\albus\OneDrive\Transfer_Kevin_Morefield\QHY600_L_300_025_020_003_060_ExtendFullwell.fits"
    ' "C:\!Work\TestData\QHY600_H_alpha_480_056_050_001_060_Photographic.fits"
    ' "C:\!Work\TestData\NGC7293 (Helix nebula)_00002.fits"
    ' "C:\!Work\TestData\ShanFano1.fits"
    ' "\\192.168.100.10\dsc\2025_03_31 - Flat OIII\Flat OIII_00011.fits"

    Public Class cFileProps
        '''<summary>Name of the file.</summary>
        Public FileName As String = String.Empty
        '''<summary>Size of the file.</summary>
        Public FileSize As Long = -1
        '''<summary>Data start position of the FITS file.</summary>
        Public DataStartPos As Integer = -1
        '''<summary>Tail byte (if there is only one ...).</summary>
        Public TailByte As Byte = 255
        '''<summary>Checksum.</summary>
        Public Checksum As String = String.Empty
        '''<summary>Name of the output file.</summary>
        Public OutFile As String = String.Empty
        '''<summary>Raw header.</summary>
        Public RawHeader As Byte() = {}
        '''<summary>LOG content.</summary>
        Public AllLog As New List(Of String)
    End Class

    '''<summary>Location of the EXE.</summary>
    Public ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

    '''<summary>Drag-and-drop handler.</summary>
    Private WithEvents DD As cDragDrop

    '''<summary>Use the IPP.</summary>
    Private UseIPP As Boolean = True

    '''<summary>Read FITS data as-is without using BZERO or BSCALE.</summary>
    Private ForceDirect As Boolean = True

    '''<summary>IPP path to use.</summary>
    Private IPPPathToUse As String = String.Empty

    '''<summary>Handle to Intel IPP functions.</summary>
    Private IPP As cIntelIPP = Nothing

    '''<summary>FITS reader.</summary>
    Private FITSReader As cFITSReader = Nothing

    '''<summary>FITS header parser.</summary>
    Private FITSHeaderParser As cFITSHeaderParser = Nothing

    '''<summary>Statistics calculator (where the image data are stored ...).</summary>
    'Private SingleStatCalc As AstroNET.Statistics = Nothing

    '''<summary>Block size for FITS files.</summary>
    Private Const FITSBlockSize As Integer = 2880

    '''<summary>Image statistics.</summary>
    'Private Statistics As AstroNET.Statistics.sStatistics

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Load IPP
        Dim IPPLoadError = String.Empty
        IPPPathToUse = cIntelIPP.SearchDLLToUse(cIntelIPP.PossiblePaths(MyPath).ToArray, IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = False Then
            MsgBox("IPP not found <" & IPPLoadError & ">")
        End If

        'Init drap-and-drop
        DD = New cDragDrop(lbFiles, True)

    End Sub

    Private Sub btnCompress_Click(sender As Object, e As EventArgs) Handles btnCompress.Click
        For Each FITsFile As String In lbFiles.Items
            Dim FileProps As New cFileProps
            FileProps.FileName = FITsFile
            Compress(FileProps)
        Next FITsFile
    End Sub

    Private Sub btnDeCompress_Click(sender As Object, e As EventArgs) Handles btnDeCompress.Click

        For Each FITZipFile As String In lbFiles.Items

            Dim FileProps As New cFileProps
            FileProps.FileName = FITZipFile

            'Start with file
            Logging(FileProps, "FILE <" & FileProps.FileName & ">")

            Dim SFGen As New cShanFano
            SFGen.DecodeFITZipFile(FITZipFile, String.Empty)

            Logging(FileProps, "END.")

            Logging(FileProps, New String("="c, 60))

        Next FITZipFile

    End Sub

    Private Function Compress(ByRef FileProps As cFileProps) As Boolean

        Dim Stopper As New Stopwatch

        'Create IPP instance - search path automatically
        If IsNothing(IPP) Then IPP = New cIntelIPP
        If IsNothing(FITSReader) Then FITSReader = New cFITSReader(IPP.IPPPath)
        'If IsNothing(SingleStatCalc) Then SingleStatCalc = New AstroNET.Statistics(IPP)

        'Start with file
        Logging(FileProps, "FILE <" & FileProps.FileName & ">")

        'Read fits header
        Stopper.Restart() : Stopper.Start()
        Logging(FileProps, "  Read header ...")
        FITSHeaderParser = New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileProps.FileName, FileProps.DataStartPos))
        Logging(FileProps, "    -> data format: <" & FITSHeaderParser.BitPix.ValRegIndep & ">")
        Logging(FileProps, "    -> width      : <" & FITSHeaderParser.Width.ValRegIndep & " pixel>")
        Logging(FileProps, "    -> heigth     : <" & FITSHeaderParser.Height.ValRegIndep & " pixel>")
        Finish(FileProps, Stopper)

        'Get raw header bytes
        Using X As IO.FileStream = System.IO.File.OpenRead(FileProps.FileName)
            ReDim FileProps.RawHeader(FileProps.DataStartPos - 1)
            X.Seek(0, IO.SeekOrigin.Begin)
            X.Read(FileProps.RawHeader, 0, FileProps.RawHeader.Length)
            X.Close()
        End Using

        'Check file conditions
        Stopper.Restart() : Stopper.Start()
        Logging(FileProps, "  File consistance check ...")
        Dim FileOK As Boolean = True
        If FITSHeaderParser.BitPix <> 16 Then
            Logging(FileProps, "-- ERROR: Only BitPix format 16 supported --")
            FileOK = False
        End If
        If FITSHeaderParser.NAXIS3 > 1 Then
            Logging(FileProps, "-- ERROR: Only 2D mono data supported (NAXIS3=0) --")
            FileOK = False
        End If

        'Check if the tail contains only 1 unique tail byte (and e.g. no other information is coded)
        Dim CheckResult As String = CheckFITSFileIsOK(FileProps)
        If String.IsNullOrEmpty(CheckResult) = False Then
            Logging(FileProps, "-- ERROR: " & CheckResult)
            FileOK = False
        Else
            Logging(FileProps, "    -> tail byte: <0x" & Hex(FileProps.TailByte).Trim & ">")
        End If

        'Set the output file
        If String.IsNullOrEmpty(FileProps.OutFile) Then
            FileProps.OutFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(FileProps.FileName), System.IO.Path.GetFileNameWithoutExtension(FileProps.FileName) & ".tar")
        End If
        If System.IO.File.Exists(FileProps.OutFile) Then
            Logging(FileProps, "-- ERROR: Output file <" & FileProps.OutFile & "> already exists")
            FileOK = False
        End If
        Finish(FileProps, Stopper)

        If FileOK Then

            'Get checksum of complete file
            Stopper.Restart() : Stopper.Start()
            Logging(FileProps, "  Get checksum ...")
            FileProps.Checksum = RecursiveDirScanner.GetHash(FileProps.FileName)
            Logging(FileProps, "    -> SHA256 hash: <" & FileProps.Checksum & ">")
            Finish(FileProps, Stopper)

            'Read UInt16 data
            Stopper.Restart() : Stopper.Start()
            Logging(FileProps, "  Read data content and calculate statistics...")

            'We just combine 2 byte to 1 UInt16 value and count how often each of this value is present
            'This will replace reading the complete data and calculate the histogram; the real data are not required in this step
            Dim Histo As New Dictionary(Of UInt16, System.UInt64)
            Dim OneMore As System.UInt32 = 1
            Using DataReader As New System.IO.BinaryReader(System.IO.File.OpenRead(FileProps.FileName))
                DataReader.BaseStream.Position = FileProps.DataStartPos
                Dim OneLineBytes As Integer = 2 * FITSHeaderParser.Width
                For X1 As Integer = 1 To FITSHeaderParser.Height
                    Dim Buffer() As Byte = DataReader.ReadBytes(OneLineBytes)
                    For BytesPtr As Integer = 0 To Buffer.Length - 2 Step 2
                        Dim Pixel As UInt16 = CUShort(BitConverter.ToInt16({Buffer(BytesPtr + 1), Buffer(BytesPtr)}, 0) + 32768)
                        If Histo.ContainsKey(Pixel) = False Then Histo.Add(Pixel, 0)
                        Histo(Pixel) += OneMore
                    Next BytesPtr
                Next X1
            End Using
            Finish(FileProps, Stopper)

            'Code book generation
            Stopper.Restart() : Stopper.Start()
            Logging(FileProps, "  Codebook generation ...")
            Dim SFGen As New cShanFano
            Logging(FileProps, "    ", SFGen.GenCodeBook(Histo))
            Finish(FileProps, Stopper)

            'Compression
            Stopper.Restart() : Stopper.Start()
            Logging(FileProps, "  Compression ...")
            Using DataReader As New System.IO.BinaryReader(System.IO.File.OpenRead(FileProps.FileName))
                SFGen.Compress(DataReader, FileProps.DataStartPos, FITSHeaderParser.Width, FITSHeaderParser.Height)
            End Using
            Finish(FileProps, Stopper)

            'Store
            Stopper.Restart() : Stopper.Start()
            Logging(FileProps, "  Storing ...")
            SFGen.StoreSFTar(FileProps.OutFile, FileProps.RawHeader, FileProps.FileSize, FileProps.TailByte, FileProps.Checksum)
            Finish(FileProps, Stopper)

            'Normal compression (for reference)
            If 1 = 0 Then
                Stopper.Restart() : Stopper.Start()
                Logging(FileProps, "Normal compression comparison")
                Logging(FileProps, SFGen.CompressStandard(FileProps.FileName))
                Finish(FileProps, Stopper)
            End If

            Logging(FileProps, "END.")

            Logging(FileProps, New String("="c, 60))
            Return True

        Else

            Logging(FileProps, New String("!"c, 60))
            Return False

        End If

    End Function

    Private Function CheckFITSFileIsOK(ByRef FileProps As cFileProps) As String
        FileProps.FileSize = (New System.IO.FileInfo(FileProps.FileName)).Length
        Dim HeaderLength As Long = FileProps.DataStartPos
        Dim DataContentLength As Long = (FITSHeaderParser.Width * FITSHeaderParser.Height * 2)
        Dim TailLength As Long = FileProps.FileSize - HeaderLength - DataContentLength
        If TailLength = 0 Then
            Return String.Empty
        Else
            'Check if the rest of the bytes is 0
            Using Checker As New System.IO.FileStream(FileProps.FileName, IO.FileMode.Open, IO.FileAccess.Read)
                Dim Tail(CInt(TailLength) - 1) As Byte
                Checker.Seek(HeaderLength + DataContentLength, IO.SeekOrigin.Begin)
                Checker.Read(Tail, 0, Tail.Length)
                Dim TailBytes As New Dictionary(Of Byte, Integer)
                Dim NonZeroBytes As Integer = 0
                For Each SingleByte As Byte In Tail
                    If TailBytes.ContainsKey(SingleByte) = False Then TailBytes.Add(SingleByte, 0)
                    TailBytes(SingleByte) += 1
                Next SingleByte
                Select Case TailBytes.Count
                    Case 1
                        FileProps.TailByte = TailBytes.First.Key
                    Case Else
                        Return TailBytes.Count.ValRegIndep & " different non-zero tail bytes found in tail of length " & TailLength.ValRegIndep
                End Select
            End Using
            Return String.Empty
        End If
    End Function

    Private Sub Finish(ByRef FileProps As cFileProps, ByVal Stopper As Stopwatch)
        Stopper.Stop()
        Logging(FileProps, "    DONE in <" & Stopper.ElapsedMilliseconds.ValRegIndep & " ms>")
        Logging(FileProps, New String("─"c, 60))
    End Sub

    Private Sub Logging(ByRef FileProps As cFileProps, ByVal LogText As String)
        Logging(FileProps, New String() {LogText})
    End Sub

    Private Sub Logging(ByRef FileProps As cFileProps, ByVal Indent As String, ByVal LogText() As String)
        For Each Line As String In LogText
            Logging(FileProps, Indent & Line)
        Next Line
    End Sub

    Private Sub Logging(ByRef FileProps As cFileProps, ByVal LogText() As String)
        FileProps.AllLog.AddRange(LogText)
        tbLog.Text = Join(FileProps.AllLog.ToArray, System.Environment.NewLine)
        tbLog.SelectionStart = tbLog.Text.Length - 1 : tbLog.ScrollToCaret()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub btnClearList_Click(sender As Object, e As EventArgs) Handles btnClearList.Click
        lbFiles.Items.Clear()
    End Sub

    Private Sub lbFiles_KeyUp(sender As Object, e As KeyEventArgs) Handles lbFiles.KeyUp
        'Delete this item
        If e.KeyCode = Keys.Delete Then lbFiles.Items.Remove(lbFiles.SelectedItems(0))
    End Sub

End Class
