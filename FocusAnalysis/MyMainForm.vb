Option Explicit On
Option Strict On

#Disable Warning CA1416 ' Validate platform compatibility
Public Class MyMainForm

    Private DB As New cDB
    Private DD1 As cDragDrop
    Private DD2 As cDragDrop

    Private Class ExcelRows
        Public Const Frame As String = "Frame #"
        Public Const SERTime As String = "SER Time" & vbCrLf & "stamp"
        Public Const Foc_Moment As String = "Focus" & vbCrLf & "Moment"
        Public Const Foc_Pos As String = "Focus" & vbCrLf & "Position"
        Public Const Sum_AllPix As String = "Sum of all pixel" & vbCrLf & "Raw"
        Public Const Energy_10pct As String = "Raw pixel for" & vbCrLf & "10% energy"
        Public Const Energy_20pct As String = "Raw for" & vbCrLf & "20% energy"
        Public Const Energy_50pct As String = "Raw for" & vbCrLf & "50% energy"
        Public Const Sum_NoBias As String = "Sum of all pixel" & vbCrLf & "No bias"
        Public Const NoBias_Energy_10pct As String = "Pixel for" & vbCrLf & "10% energy"
        Public Const NoBias_Energy_20pct As String = "Pixel for" & vbCrLf & "20% energy"
        Public Const NoBias_Energy_50pct As String = "Pixel for" & vbCrLf & "50% energy"
        Public Const Min_ADUValue As String = "Minimum" & vbCrLf & "ADU value"
        Public Const Min_ADUValueCount As String = "# pixel" & vbCrLf & "with min value"
        Public Const Max_ADUValue As String = "Peak pixel" & vbCrLf & "ADU value"
        Public Const Max_ADUValueCount As String = "# pixel" & vbCrLf & "with peak value"
    End Class

    Private Sub MyMainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Load IPP
        Dim IPPLoadError As String = String.Empty
        DB.IPPPath = cIntelIPP.SearchDLLToUse(cIntelIPP.PossiblePaths(DB.Config.MyPath).ToArray, IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = True Then
            DB.IntelIPP = New cIntelIPP(DB.IPPPath)
        End If
        DB.OriginalFrame = New AstroNET.Statistics(DB.IntelIPP)
        DB.NoBiasFrame = New AstroNET.Statistics(DB.IntelIPP)
        DB.Plotter = New cZEDGraph(zgcMain)
        DB.HistoPlot = New cZEDGraph(zgcHisto)
        DB.Log = New cLogTextBox(tbLog)
        pgMain.SelectedObject = DB.Config
        'Drag-and-drop
        DD1 = New cDragDrop(tbSERFile, True)
        DD2 = New cDragDrop(tbPWI4LogFile, True)
    End Sub

    Private Sub tsmiFile_OpenSerSequence_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenSerSequence.Click
        Dim FileName As String = tbSERFile.Text
        If System.IO.File.Exists(FileName) Then
            Dim EXEToUse As String = DB.Config.SERPlayer
            If System.IO.File.Exists(EXEToUse) = False Then EXEToUse = Utils.GetOpenWithEXE(FileName)
            Dim SI As New ProcessStartInfo
            With SI
                .WorkingDirectory = System.IO.Path.GetDirectoryName(FileName)
                .FileName = EXEToUse
                .Arguments = Chr(34) & FileName & Chr(34)
            End With
            Process.Start(SI)
        End If
    End Sub

    Private Sub btnAnalysis_Click(sender As Object, e As EventArgs) Handles btnAnalysis.Click
        Analysis()
    End Sub

    Private Sub Analysis()

        DB.Log.Clear()

        'Load PWI4 file
        Dim PWI4Log As List(Of KeyValuePair(Of DateTime, Double)) = LoadPWI4Log(tbPWI4LogFile.Text)
        If IsNothing(PWI4Log) = False Then
            DB.Log.Log("PWI4Log first moment: " & PWI4Log.First.Key.LongWithMS)
            DB.Log.Log("PWI4Log last moment : " & PWI4Log.Last.Key.LongWithMS)
        Else
            DB.Log.Log("PWI4Log not found.")
        End If

        'Open the SER file
        Dim BinaryIN As New System.IO.BinaryReader(System.IO.File.OpenRead(tbSERFile.Text))
        Dim SERHeader As New cSERFormat.cSERHeader(BinaryIN)
        Dim TimeStamps As DateTime() = SERHeader.ReadTrailer(BinaryIN)
        DB.Log.Log("Frames              : " & SERHeader.FrameCount.ValRegIndep)
        DB.Log.Log("Size                : " & SERHeader.FrameWidth.ValRegIndep & "x" & SERHeader.FrameWidth.ValRegIndep)
        DB.Log.Log("SER first moment    : " & TimeStamps.First.LongWithMS)
        DB.Log.Log("SER last moment     : " & TimeStamps.Last.LongWithMS)
        DB.SERFileNoExtension = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(tbSERFile.Text), System.IO.Path.GetFileNameWithoutExtension(tbSERFile.Text))

        'Exit if the stream is not OK or there are no 16 bit per pixel
        If BinaryIN.BaseStream.Position <> cSERFormat.cSERHeader.SERHeaderLength Then Exit Sub
        If SERHeader.BytePerPixel <> 2 Then Exit Sub

        'Prepare buffers
        'DB.OriginalFrame.ResetAllProcessors()
        DB.NoBiasFrame.ResetAllProcessors()
        Dim ROI_Full As New Rectangle(0, 0, SERHeader.FrameWidth, SERHeader.FrameHeight)
        Dim ROI As Rectangle
        If DB.Config.ROIPct <> 100.0 Then
            ROI = ROI_Full.InnerPct(DB.Config.ROIPct)
        Else
            ROI = ROI_Full
        End If
        Dim FullFrameSize As Integer = CInt(ROI.Width * ROI.Height * SERHeader.BytePerPixel)
        ReDim DB.Trace_Indicator(SERHeader.FrameCount - 1) : DB.Trace_Indicator.Init(Double.NaN)
        ReDim DB.Trace_OVLD(SERHeader.FrameCount - 1) : DB.Trace_OVLD.Init(Double.NaN)
        ReDim DB.Trace_TotalEnergy(SERHeader.FrameCount - 1) : DB.Trace_TotalEnergy.Init(Double.NaN)

        '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        '1st run: global maximum and minimum
        ReDim DB.OriginalFrame.DataProcessor_UInt16.ImageData(0).Data(SERHeader.FrameWidth - 1, SERHeader.FrameHeight - 1)
        pbImageStream.Maximum = SERHeader.FrameCount

        'Global MIN and Global MAX are always done on the full frame
        ReDim DB.GlobalMax(SERHeader.FrameWidth - 1, SERHeader.FrameHeight - 1) : DB.GlobalMax.Init(UInt16.MinValue)
        ReDim DB.GlobalMin(SERHeader.FrameWidth - 1, SERHeader.FrameHeight - 1) : DB.GlobalMin.Init(UInt16.MaxValue)

        For FrameCountIdx As Integer = 0 To SERHeader.FrameCount - 1
            DB.OriginalFrame.DataProcessor_UInt16.ImageData(0).Data = GetSERROI(BinaryIN, SERHeader.FrameWidth, SERHeader.FrameHeight, ROI_Full)
            DB.GlobalMax = DB.GlobalMax.MaximumPerElement(DB.OriginalFrame.DataProcessor_UInt16.ImageData(0).Data)
            DB.GlobalMin = DB.GlobalMin.MinimumPerElement(DB.OriginalFrame.DataProcessor_UInt16.ImageData(0).Data)
            pbImageStream.Value = FrameCountIdx
            tsslImageStream.Text = FrameCountIdx.ValRegIndep & "/" & pbImageStream.Maximum.ValRegIndep
            De()
        Next FrameCountIdx

        '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        'Reposition SER pointer again
        BinaryIN.BaseStream.Position = cSERFormat.cSERHeader.SERHeaderLength

        'This are all results which will be available in the EXCEL table
        Dim FrameResults As New Dictionary(Of Integer, List(Of Object))
        Dim ResultParameters As New List(Of String)
        With ResultParameters
            .Add(ExcelRows.Frame)
            .Add(ExcelRows.SERTime)
            .Add(ExcelRows.Foc_Moment)
            .Add(ExcelRows.Foc_Pos)
            .Add(ExcelRows.Sum_AllPix)
            .Add(ExcelRows.Energy_10pct)
            .Add(ExcelRows.Energy_20pct)
            .Add(ExcelRows.Energy_50pct)
            .Add(ExcelRows.Sum_NoBias)
            .Add(ExcelRows.NoBias_Energy_10pct)
            .Add(ExcelRows.NoBias_Energy_20pct)
            .Add(ExcelRows.NoBias_Energy_50pct)
            .Add(ExcelRows.Min_ADUValue)
            .Add(ExcelRows.Min_ADUValueCount)
            .Add(ExcelRows.Max_ADUValue)
            .Add(ExcelRows.Max_ADUValueCount)
        End With

        Dim FocusFit_X As New List(Of Double)
        Dim FocusFit_Y As New List(Of Double)

        'Move over all frames
        DB.Plotter.Clear()
        DB.HistoPlot.ManuallyScaleXAxisLin(0, 65536)
        For FrameCountIdx As Integer = 0 To SERHeader.FrameCount - 1

            'Show status start
            pbImageStream.Value = FrameCountIdx
            tsslImageStream.Text = FrameCountIdx.ValRegIndep & "/" & pbImageStream.Maximum.ValRegIndep
            De()

            'Get timestamp
            Dim FrameResult As New Dictionary(Of String, Object)
            FrameResult.Add(ExcelRows.Frame, FrameCountIdx)                                         'frame idx
            FrameResult.Add(ExcelRows.SERTime, TimeStamps(FrameCountIdx).LongWithMS)                'frame time stamp

            'Get focus informations stored
            Dim BestFocus As KeyValuePair(Of DateTime, Double) = FocusInMoment(PWI4Log, TimeStamps(FrameCountIdx))
            If Double.IsNaN(BestFocus.Value) = False Then
                FrameResult.Add(ExcelRows.Foc_Moment, BestFocus.Key.LongWithMS)                         'focus time stamp
                FrameResult.Add(ExcelRows.Foc_Pos, BestFocus.Value)                                     'focus position
            Else
                FrameResult.Add(ExcelRows.Foc_Moment, "---")
                FrameResult.Add(ExcelRows.Foc_Pos, "---")
            End If

            '1.) Read in 1 frame and convert to 2-byte data type - TODO: Little / Big Endian
            DB.OriginalFrame.DataProcessor_UInt16.ImageData(0).Data = GetSERROI(BinaryIN, SERHeader.FrameWidth, SERHeader.FrameHeight, ROI)
            Dim SamplesPerFrame As Long = DB.OriginalFrame.DataProcessor_UInt16.ImageData(0).Data.LongLength

            'Calculate original statistics and display the image
            DB.OriginalFrame_Stat = DB.OriginalFrame.ImageStatistics
            UpdateSERFrameImage(DB.OriginalFrame.DataProcessor_UInt16.ImageData(0).Data, DB.OriginalFrame_Stat)

            'Report statistics
            FrameResult.Add(ExcelRows.Min_ADUValue, DB.OriginalFrame_Stat.MonochromHistogram_Int.First.Key)
            FrameResult.Add(ExcelRows.Min_ADUValueCount, DB.OriginalFrame_Stat.MonochromHistogram_Int.First.Value)
            FrameResult.Add(ExcelRows.Max_ADUValue, DB.OriginalFrame_Stat.MonochromHistogram_Int.Last.Key)
            FrameResult.Add(ExcelRows.Max_ADUValueCount, DB.OriginalFrame_Stat.MonochromHistogram_Int.Last.Value)
            FrameResult.Add(ExcelRows.Sum_AllPix, AstroNET.Statistics.TotalEnergy(DB.OriginalFrame_Stat.MonochromHistogram_Int))
            FrameResult.Add(ExcelRows.Energy_10pct, AstroNET.Statistics.FocusQualityIndicator(DB.OriginalFrame_Stat.MonochromHistogram_Int, 10.0))
            FrameResult.Add(ExcelRows.Energy_20pct, AstroNET.Statistics.FocusQualityIndicator(DB.OriginalFrame_Stat.MonochromHistogram_Int, 20.0))
            FrameResult.Add(ExcelRows.Energy_50pct, AstroNET.Statistics.FocusQualityIndicator(DB.OriginalFrame_Stat.MonochromHistogram_Int, 50.0))

            tbSummary.Text = Join(DB.OriginalFrame_Stat.StatisticsReport(True, False).ToArray, System.Environment.NewLine)

            'Calculate CCDF
            Dim CCDF_X As Double() = {}
            Dim CCDF_Y As Double() = {}
            AstroNET.Statistics.CountCCDF(DB.OriginalFrame_Stat.MonochromHistogram_Int, SamplesPerFrame, CCDF_X, CCDF_Y)

            'Plot histogram and CCDF
            DB.HistoPlot.PlotXvsY("CCDF", CCDF_X, CCDF_Y, New cZEDGraph.sGraphStyle(Color.Blue))
            'DB.HistoPlot.PlotXvsY("Histo", DB.OriginalFrame_Stat.MonochromHistogram_Int, New cZEDGraph.sGraphStyle(Color.Blue))
            If FrameCountIdx = 0 Then
                DB.HistoPlot.ManuallyScaleYAxisLog(1 / SamplesPerFrame, 1)
                DB.HistoPlot.ManuallyScaleXAxisLin(0, 65535)
            End If
            DB.HistoPlot.ForceUpdate()

            'Overload indication
            Dim OVLD_Pixel As ULong = 0
            Dim MaxADU_Value As Long = DB.OriginalFrame_Stat.MonochromHistogram_Int.Last.Key
            Dim MaxADU_Count As ULong = DB.OriginalFrame_Stat.MonochromHistogram_Int.Last.Value
            If MaxADU_Value >= UInt16.MaxValue - 1 Then
                If MaxADU_Count > 3 Then
                    OVLD_Pixel = MaxADU_Count
                    tsslADUOVLD.Text = "ADU OVLD (#: " & MaxADU_Count.ValRegIndep & ")"
                    tsslADUOVLD.BackColor = Color.Red
                Else
                    tsslADUOVLD.Text = "ADU critical (#: " & MaxADU_Count.ValRegIndep & ")"
                    tsslADUOVLD.BackColor = Color.Orange
                End If
            Else
                tsslADUOVLD.Text = "ADU ok (max: " & MaxADU_Value.ValRegIndep & ")"
                tsslADUOVLD.BackColor = Color.Green
            End If
            pbMaxADU.Value = CInt(MaxADU_Value)

            '3.) Subtract the bias value
            Dim SubtraceMode As Integer = 2
            Select Case SubtraceMode
                Case 1
                    'Subtrace per-pixel min
                    DB.NoBiasFrame.DataProcessor_UInt16.ImageData(0).Data = DB.OriginalFrame.DataProcessor_UInt16.ImageData(0).Data.Subtract(DB.GlobalMin)
                Case 2
                    'Subtrace the minimum ADU value of the frame from all pixel
                    DB.NoBiasFrame.DataProcessor_UInt16.ImageData(0).Data = DB.OriginalFrame.DataProcessor_UInt16.ImageData(0).Data.CreateCopy
                    DB.IntelIPP.SubC(DB.NoBiasFrame.DataProcessor_UInt16.ImageData(0).Data, CType(DB.OriginalFrame_Stat.MonochromHistogram_Int.First.Key, UShort))
            End Select

            'Statistics and report this
            DB.NoBiasFrame_Stat = DB.NoBiasFrame.ImageStatistics
            FrameResult.Add(ExcelRows.Sum_NoBias, AstroNET.Statistics.TotalEnergy(DB.NoBiasFrame_Stat.MonochromHistogram_Int))
            FrameResult.Add(ExcelRows.NoBias_Energy_10pct, AstroNET.Statistics.FocusQualityIndicator(DB.NoBiasFrame_Stat.MonochromHistogram_Int, 10.0))
            FrameResult.Add(ExcelRows.NoBias_Energy_20pct, AstroNET.Statistics.FocusQualityIndicator(DB.NoBiasFrame_Stat.MonochromHistogram_Int, 20.0))
            FrameResult.Add(ExcelRows.NoBias_Energy_50pct, AstroNET.Statistics.FocusQualityIndicator(DB.NoBiasFrame_Stat.MonochromHistogram_Int, 50.0))

            '4.) Change the frame as required - set all pixel below x-percentil to zero
            'Dim ClipLimit As UInt16 = CUShort(DB.NoBiasFrame_Stat.MonochromHistogram_PctFract(90))
            'SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data = SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data.ClipBelow(ClipLimit, UInt16.MinValue)
            'Stat_NoBias = SingleStatCalc.ImageStatistics

            '5.) Process focus relevant data
            'FrameResult.Add(ExcelRows.Sum_NoBias, AstroNET.Statistics.TotalEnergy(Stat_NoBias.MonochromHistogram_Int))

            Dim XAxisMax As Double = Double.MinValue
            DB.Trace_Indicator(FrameCountIdx) = AstroNET.Statistics.FocusQualityIndicator(DB.NoBiasFrame_Stat.MonochromHistogram_Int, DB.Config.EnergyPCT) : XAxisMax = Double.Max(XAxisMax, DB.Trace_Indicator.Max)
            DB.Trace_OVLD(FrameCountIdx) = OVLD_Pixel : XAxisMax = Double.Max(XAxisMax, DB.Trace_OVLD.Max)
            DB.Trace_TotalEnergy(FrameCountIdx) = AstroNET.Statistics.TotalEnergy(DB.NoBiasFrame_Stat.MonochromHistogram_Int)

            DB.Plotter.PlotData("Total energy", DB.Trace_TotalEnergy, True)
            DB.Plotter.PlotData("Pixel for " & DB.Config.EnergyPCT & " % of total energy", DB.Trace_Indicator, New cZEDGraph.sGraphStyle(Color.DarkGreen))
            DB.Plotter.PlotData("OVLD pixel", DB.Trace_OVLD, New cZEDGraph.sGraphStyle(Color.Violet, cZEDGraph.eCurveMode.LinesAndPoints))

            'Scale and update
            DB.Plotter.ManuallyScaleXAxisLin(0, DB.Trace_Indicator.Length)
            DB.Plotter.ManuallyScaleYAxisLin(0, XAxisMax)
            DB.Plotter.ManuallyScaleY2AxisLog(0, DB.Trace_TotalEnergy.Max)
            DB.Plotter.ForceUpdate()

            '6.) Store results
            Dim ExcelRow As New List(Of Object)
            For Each Parameter As String In ResultParameters
                ExcelRow.Add(FrameResult(Parameter))
            Next Parameter
            FrameResults.Add(FrameCountIdx, ExcelRow)

            If Double.IsNaN(BestFocus.Value) = False Then
                FocusFit_X.Add(BestFocus.Value)
                FocusFit_Y.Add(AstroNET.Statistics.FocusQualityIndicator(DB.NoBiasFrame_Stat.MonochromHistogram_Int, 10.0))
            End If

            'Slow down
            If DB.Config.WaitOnNextFrame > 0 Then System.Threading.Thread.Sleep(DB.Config.WaitOnNextFrame)

        Next FrameCountIdx

        'Fit polynomial
        Dim FitPolynom As Double() = {}
        SignalProcessing.RegressPoly(FocusFit_X.ToArray, FocusFit_Y.ToArray, 2, FitPolynom)
        DB.Log.Log("FitPolynom - A0: " & FitPolynom(0).ValRegIndep)      'c
        DB.Log.Log("FitPolynom - A1: " & FitPolynom(1).ValRegIndep)      'b*x
        DB.Log.Log("FitPolynom - A2: " & FitPolynom(2).ValRegIndep)      'a*x²

        'Calculate zero point of deviation
        Dim OptFocus As Double = (-FitPolynom(1)) / (2 * FitPolynom(2))
        DB.Log.Log("OptFocus: " & OptFocus.ValRegIndep)

        'Indicate finished
        pbImageStream.Value = 0
        pbMaxADU.Value = 0
        tsslImageStream.Text = pbImageStream.Maximum.ValRegIndep & " frames loaded"
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
            If String.IsNullOrEmpty(DB.Config.ExcelFileName) = False Then
                Dim FileToGenerate As String = IO.Path.Combine(DB.Config.MyPath, DB.Config.ExcelFileName)
                workbook.SaveAs(FileToGenerate)
                Utils.StartWithItsEXE(FileToGenerate)
            End If
        End Using

    End Sub

    Public Function GetSERROI(ByRef BinaryIN As System.IO.BinaryReader, ByVal Width As Integer, ByVal Height As Integer, ByVal ROI As Rectangle) As UInt16(,)
        Dim FullFrameSize As Integer = Width * Height * 2
        Dim FullFrame(Width - 1, Height - 1) As UInt16
        DB.IntelIPP.Transpose(BinaryIN.ReadBytes(FullFrameSize), FullFrame)
        If (ROI.X > 0) Or (ROI.Y > 0) Or (ROI.Width <> Width) Or (ROI.Height <> Height) Then
            FullFrame = FullFrame.GetROI(ROI)
        End If
        Return FullFrame
    End Function

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
        If IsNothing(MomentAndPosition) Then Return New KeyValuePair(Of DateTime, Double)(DateTime.MinValue, Double.NaN)
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

    Private Sub UpdateSERFrameImage(ByRef Frame(,) As UInt16, ByRef FrameStat As AstroNET.Statistics.sStatistics)
        Dim NoROI As Rectangle = Nothing
        With DB.SERFrameImage
            .CM = cColorMaps.eMaps.FalseColor
            .CM_LowerEnd_Absolute = FrameStat.MonoStatistics_Int.Min.Key
            .CM_UpperEnd_Absolute = FrameStat.MonoStatistics_Int.Max.Key
            .GenerateDisplayImage(Frame, NoROI, FrameStat, DB.IntelIPP)
            .OutputImage.UnlockBits()
            pbExSERFrameImage.Image = .OutputImage.BitmapToProcess
        End With

    End Sub

    Private Sub De()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub tsmiFile_Save_GlobalMin_Click(sender As Object, e As EventArgs) Handles tsmiFile_Save_GlobalMin.Click
        Dim FileName As String = DB.SERFileNoExtension & "_GlobalMin.fits"
        cFITSWriter.Write(FileName, DB.GlobalMin, cFITSWriter.eBitPix.Int16)
        Utils.StartWithItsEXE(FileName)
    End Sub

    Private Sub tsmiFile_Save_GlobalMax_Click(sender As Object, e As EventArgs) Handles tsmiFile_Save_GlobalMax.Click
        Dim FileName As String = DB.SERFileNoExtension & "_GlobalMax.fits"
        cFITSWriter.Write(FileName, DB.GlobalMax, cFITSWriter.eBitPix.Int16)
        Utils.StartWithItsEXE(FileName)
    End Sub

    Private Sub btnSetSERFile_Click(sender As Object, e As EventArgs) Handles btnSetSERFile.Click
        With ofdMain
            If System.IO.File.Exists(tbSERFile.Text) Then .InitialDirectory = System.IO.Path.GetDirectoryName(tbSERFile.Text)
            .Filter = "SER file (*.ser)|*.ser"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
            tbSERFile.Text = .FileName
        End With
    End Sub

End Class
