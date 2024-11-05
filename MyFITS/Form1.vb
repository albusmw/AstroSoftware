Option Explicit On
Option Strict On

Public Class Form1

    '''<summary>Handle to Intel IPP functions.</summary>
    Private IPP As cIntelIPP
    Private FITS_Reader As cFITSReader
    Private FITS_Header As cFITSHeaderParser
    Private HeaderElements As List(Of cFITSHeaderParser.sHeaderElement)
    Private DataStartPos As Integer = -1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load IPP
        Dim IPPLoadError = String.Empty
        Dim IPPPathToUse = cIntelIPP.SearchDLLToUse(IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = True Then
            IPP = New cIntelIPP(IPPPathToUse)
        Else
            IPP = Nothing
        End If
    End Sub

    Private Sub tsmiFile_Open_Click(sender As Object, e As EventArgs) Handles tsmiFile_Open.Click
        'Show open dialog
        With ofdMain
            .Filter = "FITS files (*.fit?)|*.fit?"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With
        Dim LogContent As New List(Of String)
        LogContent.Add("File : <" & ofdMain.FileName & ">")
        'Load header and get data start position
        HeaderElements = cFITSHeaderChanger.ParseHeader(ofdMain.FileName, DataStartPos)
        Dim KeywordMaxLength As Integer = 0
        Dim ValueMaxLength As Integer = cFITSHeaderChanger.MaxValueLength(HeaderElements)
        For Each Entry As cFITSHeaderParser.sHeaderElement In HeaderElements
            LogContent.Add(Entry.Keyword.ToString.PadRight(8, " "c) & " = " & Entry.Value.ToString.PadLeft(ValueMaxLength, " "c) & " / " & Entry.Comment.ToString.Trim)
        Next Entry
        LogContent.Add("Data start position : <" & DataStartPos.ValRegIndep & ">")
        tbFITSHeader.Text = Join(LogContent.ToArray, System.Environment.NewLine)
    End Sub

    Private Sub tsmiOp_Transpose_Click(sender As Object, e As EventArgs) Handles tsmiOp_Transpose.Click
        'Transpose the content of the file
        Dim FixFileName As String = ofdMain.FileName & "_FIXED.fits"
        System.IO.File.Copy(ofdMain.FileName, FixFileName)
        Using OldFile As New System.IO.FileStream(ofdMain.FileName, IO.FileMode.Open)
            Using NewFile As New System.IO.FileStream(FixFileName, IO.FileMode.Open)
                'Copy header
                Dim Header(DataStartPos - 1) As Byte
                OldFile.Seek(0, IO.SeekOrigin.Begin)
                OldFile.Read(Header, 0, Header.Count)
                NewFile.Write(Header)
                'Get dimensions
                Dim NAXIS1 As Integer = -1
                Dim NAXIS2 As Integer = -1
                Dim BytePerPixel As Integer = -1
                For Each Entry As cFITSHeaderParser.sHeaderElement In HeaderElements
                    Select Case Entry.Keyword
                        Case eFITSKeywords.NAXIS1
                            NAXIS1 = CInt(Entry.Value)
                        Case eFITSKeywords.NAXIS2
                            NAXIS2 = CInt(Entry.Value)
                        Case eFITSKeywords.BITPIX
                            BytePerPixel = CInt(Math.Abs(CInt(Entry.Value)) / 8)
                    End Select
                Next Entry
                'Start transpose - we assume 2 byte per value
                'Currently not working ...
                Dim WritePtr As Integer = -1
                Dim SingleValueBuffer(BytePerPixel - 1) As Byte
                For Idx1 As Integer = 0 To NAXIS1 - 1
                    For Idx2 As Integer = 0 To NAXIS2 - 1
                        OldFile.Read(SingleValueBuffer, 0, SingleValueBuffer.Length)
                        WritePtr = DataStartPos + (((Idx1 * NAXIS2) + Idx2) * BytePerPixel)
                        NewFile.Seek(WritePtr, IO.SeekOrigin.Begin)
                        NewFile.Write(SingleValueBuffer)
                    Next Idx2
                Next Idx1
                'Read to the end and just copy tail
                Dim TailSize As Integer = CInt(OldFile.Length - OldFile.Position)
                Dim TailSizeBuffer(TailSize - 1) As Byte
                OldFile.Read(TailSizeBuffer, 0, TailSizeBuffer.Length)
                NewFile.Write(TailSizeBuffer)
                'Flush and close
                NewFile.Flush()
                NewFile.Close()
            End Using
            OldFile.Close()
        End Using
        MsgBox("OK")
    End Sub

End Class
