Option Explicit On
Option Strict On
Imports FocusAnalysis.cSERFormat



#Disable Warning CA1416 ' Validate platform compatibility
Public Class MyMainForm

    Private ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
    Private IPPPath As String = String.Empty

    '''<summary>Handle to Intel IPP functions.</summary>
    Private IntelIPP As cIntelIPP

    '''<summary>Statistics processor (for the last file).</summary>
    Private SingleStatCalc As AstroNET.Statistics

    '''<summary>Statistics of the last frame.</summary>
    Private LastStat As AstroNET.Statistics.sStatistics

    Private Sub tsmiFile_OpenSerSequence_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenSerSequence.Click

        With ofdMain
            .Filter = "SER file (*.ser)|*.ser"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        'Open the SER file
        Dim BinaryIN As New System.IO.BinaryReader(System.IO.File.OpenRead(ofdMain.FileName))
        Dim SERHeader As New cSERFormat.cSERHeader(BinaryIN)
        Dim TimeStamps As DateTime() = SERHeader.ReadTrailer(BinaryIN)

        SingleStatCalc.ResetAllProcessors()

        'Exit if the stream is not OK or there are no 16 bit per pixel
        If BinaryIN.BaseStream.Position <> cSERFormat.cSERHeader.SERHeaderLength Then Exit Sub
        If SERHeader.BytePerPixel <> 2 Then Exit Sub
        Dim FullFrameSize As Integer = CInt(SERHeader.FrameWidth * SERHeader.FrameHeight * SERHeader.BytePerPixel)
        ReDim SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data(SERHeader.FrameWidth - 1, SERHeader.FrameHeight - 1)

        '1st run: global maximum and minimum
        Dim GlobalMax(SERHeader.FrameWidth - 1, SERHeader.FrameHeight - 1) As UInt16 : GlobalMax.Init(UInt16.MinValue)
        Dim GlobalMin(SERHeader.FrameWidth - 1, SERHeader.FrameHeight - 1) As UInt16 : GlobalMin.Init(UInt16.MaxValue)
        pbImageStream.Maximum = SERHeader.FrameCount
        For FrameCountIdx As Integer = 0 To SERHeader.FrameCount - 1
            IntelIPP.Transpose(BinaryIN.ReadBytes(FullFrameSize), SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data)
            GlobalMax = GlobalMax.MaximumPerElement(SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data)
            GlobalMin = GlobalMin.MinimumPerElement(SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data)
            pbImageStream.Value = FrameCountIdx
            tImageStream.Text = FrameCountIdx.ValRegIndep & "/" & pbImageStream.Maximum.ValRegIndep
            De()
        Next FrameCountIdx

        'Reposition again
        BinaryIN.BaseStream.Position = cSERFormat.cSERHeader.SERHeaderLength

        Dim FrameResults As New Dictionary(Of Integer, List(Of Object))
        Dim ResultParameters As New List(Of String)
        With ResultParameters
            .Add("Frame #")
            .Add("Time stamp")
            .Add("Focus position")
            .Add("Sum of all pixel - raw")
            .Add("Sum of all pixel - corrected")
            .Add("Pixel for 10pct energy")
            .Add("Pixel for 20pct energy")
            .Add("Pixel for 50pct energy")
            .Add("Saturated #")
            .Add("Saturated value")
        End With

        For FrameCountIdx As Integer = 0 To SERHeader.FrameCount - 1

            Dim FrameResult As New Dictionary(Of String, Object)
            FrameResult.Add("Frame #", FrameCountIdx)                                   'frame idx
            FrameResult.Add("Time stamp", TimeStamps(FrameCountIdx).LongWithMS)         'frame idx
            FrameResult.Add("Focus position", "???")                                    'focus position

            pbImageStream.Value = FrameCountIdx
            tImageStream.Text = FrameCountIdx.ValRegIndep & "/" & pbImageStream.Maximum.ValRegIndep
            De()

            '1.) Read in 1 frame and convert to 2-byte data type - TODO: Little / Big Endian
            IntelIPP.Transpose(BinaryIN.ReadBytes(FullFrameSize), SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data)
            LastStat = SingleStatCalc.ImageStatistics
            FrameResult.Add("Saturated #", LastStat.MonochromHistogram_Int.Last.Value)
            FrameResult.Add("Saturated value", LastStat.MonochromHistogram_Int.Last.Key)
            FrameResult.Add("Sum of all pixel - raw", AstroNET.Statistics.TotalEnergy(LastStat.MonochromHistogram_Int))

            '2.) Correct by min-of-all frame (black current, dead cold or hot pixel)
            SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data = SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data.Subtract(GlobalMin)
            LastStat = SingleStatCalc.ImageStatistics

            '3.) Process focus relevant data
            FrameResult.Add("Sum of all pixel - corrected", AstroNET.Statistics.TotalEnergy(LastStat.MonochromHistogram_Int))
            FrameResult.Add("Pixel for 10pct energy", AstroNET.Statistics.FocusQualityIndicator(LastStat.MonochromHistogram_Int, 10.0))
            FrameResult.Add("Pixel for 20pct energy", AstroNET.Statistics.FocusQualityIndicator(LastStat.MonochromHistogram_Int, 20.0))
            FrameResult.Add("Pixel for 50pct energy", AstroNET.Statistics.FocusQualityIndicator(LastStat.MonochromHistogram_Int, 50.0))

            '4.) Store results
            Dim ExcelRow As New List(Of Object)
            For Each Parameter As String In ResultParameters
                ExcelRow.Add(FrameResult(Parameter))
            Next Parameter
            FrameResults.Add(FrameCountIdx, ExcelRow) : De()

        Next FrameCountIdx

        'Indicate finished
        pbImageStream.Value = 0
        tImageStream.Text = pbImageStream.Maximum.ValRegIndep & " frames loaded"
        De()

        '4) Build EXCEL output, save and open
        Using workbook As New ClosedXML.Excel.XLWorkbook
            Dim WorkSheet_Single As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Frame Statistics")
            WorkSheet_Single.Cell(1, 1).InsertData(ResultParameters, True)
            For FrameCountIdx As Integer = 0 To SERHeader.FrameCount - 1
                WorkSheet_Single.Cell(FrameCountIdx + 2, 1).InsertData(FrameResults(FrameCountIdx), True)
            Next FrameCountIdx
            For Each col In WorkSheet_Single.ColumnsUsed
                col.AdjustToContents()
            Next col
            Dim FileToGenerate As String = IO.Path.Combine(MyPath, "SERFocus.xlsx")
            workbook.SaveAs(FileToGenerate)
            Ato.Utils.StartWithItsEXE(FileToGenerate)
        End Using


    End Sub

    Private Sub MyMainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Load IPP
        Dim IPPLoadError As String = String.Empty
        IPPPath = cIntelIPP.SearchDLLToUse(cIntelIPP.PossiblePaths(MyPath).ToArray, IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = True Then
            IntelIPP = New cIntelIPP(IPPPath)
        End If
        SingleStatCalc = New AstroNET.Statistics(IntelIPP)
    End Sub

    Private Sub De()
        System.Windows.Forms.Application.DoEvents()
    End Sub

End Class
#Enable Warning CA1416 ' Validate platform compatibility
