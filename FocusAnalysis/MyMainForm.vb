Option Explicit On
Option Strict On

#Disable Warning CA1416 ' Validate platform compatibility
Public Class MyMainForm

    Private ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

    Private IPPPath As String = String.Empty

    '''<summary>Handle to Intel IPP functions.</summary>
    Private IntelIPP As cIntelIPP

    '''<summary>Statistics processor (for the last file).</summary>
    Private SingleStatCalc As AstroNET.Statistics

    '''<summary>Statistics of the last frame.</summary>
    'Private LastStat As AstroNET.Statistics.sStatistics

    Private DD1 As Ato.DragDrop
    Private DD2 As Ato.DragDrop

    Private Sub tsmiFile_OpenSerSequence_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenSerSequence.Click

        With ofdMain
            .Filter = "SER file (*.ser)|*.ser"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
            tbSERFile.Text = .FileName
        End With

    End Sub

    Private Sub MyMainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Load IPP
        Dim IPPLoadError As String = String.Empty
        IPPPath = cIntelIPP.SearchDLLToUse(cIntelIPP.PossiblePaths(MyPath).ToArray, IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = True Then
            IntelIPP = New cIntelIPP(IPPPath)
        End If
        SingleStatCalc = New AstroNET.Statistics(IntelIPP)
        'Drag-and-drop
        DD1 = New Ato.DragDrop(tbSERFile, True)
        DD2 = New Ato.DragDrop(tbPWI4LogFile, True)
    End Sub

    Private Sub btnAnalysis_Click(sender As Object, e As EventArgs) Handles btnAnalysis.Click
        Analysis()
    End Sub

    Private Sub Analysis()

        tbLog.Text = String.Empty

        'Load PWI4 file
        Dim PWI4Log As List(Of KeyValuePair(Of DateTime, Double)) = LoadPWI4Log(tbPWI4LogFile.Text)
        tbLog.Text &= "PWI4Log first moment: " & PWI4Log.First.Key.LongWithMS & System.Environment.NewLine
        tbLog.Text &= "PWI4Log last moment : " & PWI4Log.Last.Key.LongWithMS & System.Environment.NewLine

        'Open the SER file
        Dim BinaryIN As New System.IO.BinaryReader(System.IO.File.OpenRead(tbSERFile.Text))
        Dim SERHeader As New cSERFormat.cSERHeader(BinaryIN)
        Dim TimeStamps As DateTime() = SERHeader.ReadTrailer(BinaryIN)
        tbLog.Text &= "SER first moment    : " & TimeStamps.First.LongWithMS & System.Environment.NewLine
        tbLog.Text &= "SER last moment     : " & TimeStamps.Last.LongWithMS & System.Environment.NewLine

        SingleStatCalc.ResetAllProcessors()

        'Exit if the stream is not OK or there are no 16 bit per pixel
        If BinaryIN.BaseStream.Position <> cSERFormat.cSERHeader.SERHeaderLength Then Exit Sub
        If SERHeader.BytePerPixel <> 2 Then Exit Sub
        Dim FullFrameSize As Integer = CInt(SERHeader.FrameWidth * SERHeader.FrameHeight * SERHeader.BytePerPixel)
        ReDim SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data(SERHeader.FrameWidth - 1, SERHeader.FrameHeight - 1)

        '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
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

        '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        'Reposition SER pointer again
        BinaryIN.BaseStream.Position = cSERFormat.cSERHeader.SERHeaderLength

        'This are all results which will be available in the EXCEL table
        Dim FrameResults As New Dictionary(Of Integer, List(Of Object))
        Dim ResultParameters As New List(Of String)
        With ResultParameters
            .Add("Frame #")
            .Add("Time stamp")
            .Add("Focus moment")
            .Add("Focus position")
            .Add("Sum of all pixel - raw")
            .Add("Sum of all pixel - corrected")
            .Add("Pixel for 10pct energy")
            .Add("Pixel for 20pct energy")
            .Add("Number of saturated pixel")
            .Add("Saturation ADU value")
        End With

        Dim FocusFit_X As New List(Of Double)
        Dim FocusFit_Y As New List(Of Double)

        'Move over all frames
        For FrameCountIdx As Integer = 0 To SERHeader.FrameCount - 1

            'Show status start
            pbImageStream.Value = FrameCountIdx
            tImageStream.Text = FrameCountIdx.ValRegIndep & "/" & pbImageStream.Maximum.ValRegIndep
            De()

            'Get timestamp and focus informations stored
            Dim FrameResult As New Dictionary(Of String, Object)
            Dim BestFocus As KeyValuePair(Of DateTime, Double) = FocusInMoment(PWI4Log, TimeStamps(FrameCountIdx))

            FrameResult.Add("Frame #", FrameCountIdx)                                               'frame idx
            FrameResult.Add("Time stamp", TimeStamps(FrameCountIdx).LongWithMS)                     'frame time stamp
            FrameResult.Add("Focus moment", BestFocus.Key.LongWithMS)                               'focus time stamp
            FrameResult.Add("Focus position", BestFocus.Value)                                      'focus position

            '1.) Read in 1 frame and convert to 2-byte data type - TODO: Little / Big Endian
            IntelIPP.Transpose(BinaryIN.ReadBytes(FullFrameSize), SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data)

            '2.) Calculate original statistics and report this
            Dim OriginalStat As AstroNET.Statistics.sStatistics = SingleStatCalc.ImageStatistics
            FrameResult.Add("Number of saturated pixel", OriginalStat.MonochromHistogram_Int.Last.Value)
            FrameResult.Add("Saturation ADU value", OriginalStat.MonochromHistogram_Int.Last.Key)
            FrameResult.Add("Sum of all pixel - raw", AstroNET.Statistics.TotalEnergy(OriginalStat.MonochromHistogram_Int))

            '3.) Change the frame as required - subtract the "master dark" (per-pixel minimum value)
            Dim ChangedStat As AstroNET.Statistics.sStatistics
            SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data = SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data.Subtract(GlobalMin)
            ChangedStat = SingleStatCalc.ImageStatistics

            '3.) Change the frame as required - set all pixel below x-percentil to zero
            Dim ClipLimit As UInt16 = CUShort(ChangedStat.MonochromHistogram_PctFract(90))
            SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data = SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data.ClipBelow(ClipLimit, UInt16.MinValue)
            ChangedStat = SingleStatCalc.ImageStatistics

            '4.) Process focus relevant data
            FrameResult.Add("Sum of all pixel - corrected", AstroNET.Statistics.TotalEnergy(ChangedStat.MonochromHistogram_Int))
            FrameResult.Add("Pixel for 10pct energy", AstroNET.Statistics.FocusQualityIndicator(ChangedStat.MonochromHistogram_Int, 10.0))
            FrameResult.Add("Pixel for 20pct energy", AstroNET.Statistics.FocusQualityIndicator(ChangedStat.MonochromHistogram_Int, 20.0))

            '5.) Store results
            Dim ExcelRow As New List(Of Object)
            For Each Parameter As String In ResultParameters
                ExcelRow.Add(FrameResult(Parameter))
            Next Parameter
            FrameResults.Add(FrameCountIdx, ExcelRow)

            If Double.IsNaN(BestFocus.Value) = False Then
                FocusFit_X.Add(BestFocus.Value)
                FocusFit_Y.Add(AstroNET.Statistics.FocusQualityIndicator(ChangedStat.MonochromHistogram_Int, 10.0))
            End If

        Next FrameCountIdx

        'Fit polynomial
        Dim FitPolynom As Double() = {}
        SignalProcessing.RegressPoly(FocusFit_X.ToArray, FocusFit_Y.ToArray, 2, FitPolynom)
        tbLog.Text &= "FitPolynom - A0: " & FitPolynom(0).ValRegIndep & System.Environment.NewLine      'c
        tbLog.Text &= "FitPolynom - A1: " & FitPolynom(1).ValRegIndep & System.Environment.NewLine      'b*x
        tbLog.Text &= "FitPolynom - A2: " & FitPolynom(2).ValRegIndep & System.Environment.NewLine      'a*x²

        'Calculate zero point of deviation
        Dim OptFocus As Double = (-FitPolynom(1)) / (2 * FitPolynom(2))
        tbLog.Text &= "OptFocus: " & OptFocus.ValRegIndep & System.Environment.NewLine

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

    '''<summary>Read the PWI4DataRecorder log file to get the focus positions.</summary>
    '''<param name="FileName">PWI4 log file.</param>
    '''<returns>All found timing stamps and focus positions.</returns>
    Private Function LoadPWI4Log(ByVal FileName As String) As List(Of KeyValuePair(Of DateTime, Double))
        If System.IO.File.Exists(FileName) = False Then Return Nothing
        Dim AllLines As String() = System.IO.File.ReadAllLines(FileName)
        If AllLines.Count < 2 Then Return Nothing
        Dim RetVal As New List(Of KeyValuePair(Of DateTime, Double))
        For Idx As Integer = 1 To AllLines.GetUpperBound(0)
            Dim Splitted As String() = Split(AllLines(Idx), "|")
            Dim Moment As DateTime = Splitted(2).Parse_LongWithMS
            Dim FocusPos As Double = Splitted.Last.ValRegIndep
            RetVal.Add(New KeyValuePair(Of Date, Double)(Moment, FocusPos))
        Next Idx
        Return RetVal
    End Function

    '''<summary>Get focus of the requested moment.</summary>
    Private Function FocusInMoment(ByRef MomentAndPosition As List(Of KeyValuePair(Of DateTime, Double)), ByVal RequestedDateTime As DateTime) As KeyValuePair(Of DateTime, Double)
        Dim BestValueLeft As New KeyValuePair(Of DateTime, Double)(DateTime.MinValue, Double.NaN)
        Dim BestValueRight As New KeyValuePair(Of DateTime, Double)(DateTime.MaxValue, Double.NaN)
        For Each Moment As KeyValuePair(Of DateTime, Double) In MomentAndPosition
            Dim MomentDelta As Double = (Moment.Key - RequestedDateTime).TotalMilliseconds
            If (MomentDelta < 0) Then
                Dim BestDeltaLeft As Double = (BestValueLeft.Key - RequestedDateTime).TotalMilliseconds
                If Math.Abs(MomentDelta) < Math.Abs(BestDeltaLeft) Then BestValueLeft = Moment
            End If
            If (MomentDelta > 0) Then
                Dim BestDeltaRight As Double = (BestValueRight.Key - RequestedDateTime).TotalMilliseconds
                If (MomentDelta > 0) And (Math.Abs(MomentDelta) < Math.Abs(BestDeltaRight)) Then BestValueRight = Moment
            End If
        Next Moment
        Dim DeltaLeft As Double = (BestValueLeft.Key - RequestedDateTime).TotalMilliseconds
        Dim DeltaRight As Double = (BestValueRight.Key - RequestedDateTime).TotalMilliseconds
        Dim FocusPos As Double = MathEx.LinInterpolation(DeltaLeft, BestValueLeft.Value, DeltaRight, BestValueRight.Value, 0)
        Return New KeyValuePair(Of DateTime, Double)(RequestedDateTime, FocusPos)
    End Function

    Private Sub De()
        System.Windows.Forms.Application.DoEvents()
    End Sub

End Class
#Enable Warning CA1416 ' Validate platform compatibility
