Option Explicit On
Option Strict On

'══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
' Class to display object visibility information as graph and table
'══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

Public Class frmInView

    Public Shared ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    '''<summary>InView calculation.</summary>
    Public WithEvents AstroInView As New cAstroInView
    '''<summary>InView calculation properties.</summary>
    Public Props As New cAstroInView.cProps
    '''<summary>InView plot properties.</summary>
    Public PlotConfig As New cAstroInView.cPlotConfig
    '''<summary>InView vectors to display.</summary>
    Public Vector As New cAstroInView.cVectors

    '''<summary>ZEDGraph plotter.</summary>
    Private Plotter As cZEDGraph

    Private TraceStyle_Sun As New cZEDGraph.sGraphStyle(Color.Orange, cZEDGraph.eCurveMode.Lines)
    Private TraceStyle_Object As New cZEDGraph.sGraphStyle(Color.Black, cZEDGraph.eCurveMode.Lines)
    Private TraceStyle_MoonAltitude As New cZEDGraph.sGraphStyle(Color.DarkBlue, cZEDGraph.eCurveMode.Lines)
    Private TraceStyle_MoonIllumination As New cZEDGraph.sGraphStyle(Color.LightBlue, cZEDGraph.eCurveMode.Lines)
    Private TraceStyle_Observable As New cZEDGraph.sGraphStyle(Color.DarkGreen, cZEDGraph.eCurveMode.LinesAndPoints, PlotConfig.ObservableTraceDotSize)

    '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    ' Form processing code
    '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

    Private Sub frmInView_Load(sender As Object, e As EventArgs) Handles Me.Load
        pgCalcProp.SelectedObject = Props
        Plotter = New cZEDGraph(zgcMain)
    End Sub

    Private Sub pgMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pgCalcProp.MouseWheel
        Dim OneHour = 1
        Select Case pgCalcProp.SelectedGridItem.PropertyDescriptor.Name
            Case "Declination"
                Dim CurrentValue = Props.Declination.ParseDegree
                CurrentValue += 10 / 60 * Math.Sign(e.Delta)
                Props.Declination = CurrentValue.ToDegMinSec
            Case "RightAscension"
                Dim CurrentValue = Props.RightAscension.ParseRA
                CurrentValue += 10 / (24 * 60) * Math.Sign(e.Delta)
                Props.RightAscension = CurrentValue.ToHMS
            Case "Latitude"
                Dim CurrentValue = Props.Observatory_Latitude
                CurrentValue += 10 / 60 * Math.Sign(e.Delta)
                Props.Observatory_Latitude = CurrentValue
            Case "Longitude"
                Dim CurrentValue = Props.Observatory_Longitude
                CurrentValue += 10 / 60 * Math.Sign(e.Delta)
                Props.Observatory_Longitude = CurrentValue
            Case "UTC_Start"
                Props.UTC_Start = Props.UTC_Start.Add(New TimeSpan(OneHour * Math.Sign(e.Delta), 0, 0))
            Case "UTC_Range"
                Props.CalculationRange = Props.CalculationRange.Add(New TimeSpan(OneHour * Math.Sign(e.Delta), 0, 0))
            Case "Limit_ObjectMinHeigth"
                PlotConfig.Limit_ObjectMinHeigth = PlotConfig.Limit_ObjectMinHeigth + 1 * Math.Sign(e.Delta)
            Case "Limit_SunMaxHeigth"
                PlotConfig.Limit_SunMaxHeigth = PlotConfig.Limit_SunMaxHeigth + 1 * Math.Sign(e.Delta)
            Case "Limit_MoonMaxHeigth"
                PlotConfig.Limit_MoonMaxHeigth = PlotConfig.Limit_MoonMaxHeigth + 1 * Math.Sign(e.Delta)
        End Select
        Recalc()
    End Sub

    Private Sub PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles pgCalcProp.PropertyValueChanged
        Recalc()
    End Sub

    Private Sub pgDispProp_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles pgDispProp.PropertyValueChanged
        PlotTraces(Plotter)
    End Sub

    Private Sub tsmiTime_Today_Click(sender As Object, e As EventArgs) Handles tsmiTime_Today.Click
        With Props
            .CalculationRange = cAstroInView.TimeSpans.OneDay
            .UTC_Start = Now.ToUniversalTime
            .Stepping = cAstroInView.TimeSpans.OneMinute
        End With
        Recalc()
    End Sub

    Private Sub tsmiTime_ThisMonth_Click(sender As Object, e As EventArgs) Handles tsmiTime_ThisMonth.Click
        With Props
            .CalculationRange = cAstroInView.TimeSpans.OneMonth
            .UTC_Start = Now.ToUniversalTime
            .Stepping = cAstroInView.TimeSpans.OneMinute
        End With
        Recalc()
    End Sub

    Private Sub tsmiTime_Next365Days_Click(sender As Object, e As EventArgs) Handles tsmiTime_Next365Days.Click
        With Props
            .CalculationRange = cAstroInView.TimeSpans.OneYear
            .UTC_Start = Now.ToUniversalTime
            .Stepping = 10 * cAstroInView.TimeSpans.OneMinute
        End With
        Recalc()
    End Sub

    Private Sub tsmiTime_NextDay_Click(sender As Object, e As EventArgs) Handles tsmiTime_NextDay.Click
        With Props
            .CalculationRange = cAstroInView.TimeSpans.OneDay
            .UTC_Start = .UTC_Start.Add(cAstroInView.TimeSpans.OneDay)
            .Stepping = cAstroInView.TimeSpans.OneMinute
        End With
        Recalc()
    End Sub

    Private Sub tsmiTime_PrevDay_Click(sender As Object, e As EventArgs) Handles tsmiTime_PrevDay.Click
        With Props
            .CalculationRange = cAstroInView.TimeSpans.OneDay
            .UTC_Start = .UTC_Start.Subtract(cAstroInView.TimeSpans.OneDay)
            .Stepping = cAstroInView.TimeSpans.OneMinute
        End With
        Recalc()
    End Sub

    '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    ' Calculation code
    '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

    '''<summary>Recalculate the traces and plot them.</summary>
    Public Sub Recalc()
        pgCalcProp.SelectedObject = Props
        pgDispProp.SelectedObject = PlotConfig
        AstroInViewCalc()
        PlotTraces(Plotter)
    End Sub

    '''<summary>Calculate the requested parameters over time.</summary>
    Public Sub AstroInViewCalc()

        'Exit on invalid conditions
        Dim ErrorText As String = String.Empty
        If Props.Stepping = TimeSpan.Zero Then ErrorText = "Stepping zero"
        If Props.CalculationRange = TimeSpan.Zero Then ErrorText = "Range zero"
        If String.IsNullOrEmpty(ErrorText) = False Then
            tsslMain.Text = "ERROR: <" & ErrorText & ">" : De()
            Exit Sub
        End If

        'Calculate
        tsslMain.Text = "Calculating ..." : De()
        Vector = AstroInView.CalculateBaseVectors(Props)
        tsslMain.Text = "Calculated." : De()

        'TimingLog.ShowLog()

    End Sub

    Public Sub PlotTraces(ByRef Graph As cZEDGraph)

        'Update the observable trace
        AstroInView.UpdateObservable(Vector, PlotConfig)

        'Set x axis caption and data
        Dim PlotXAxis As cAstroInView.sPlotXAxis = AstroInView.GenerateAxis(Vector, PlotConfig, Props)
        Dim XAxis As DateTime() = Vector.UTCTimes.ToArray.Add(PlotXAxis.XAxisTimeOffset)

        'Start plot
        If IsNothing(Graph) = True Then Exit Sub
        Graph.Clear()
        Graph.SetCaptions("Object " & Props.ObjectName, PlotXAxis.XAxisCaption, "Object altitude [°]")

        'Plot traces
        If PlotConfig.Trace_Observable Then Graph.PlotXvsT("Observable [°]", XAxis, Vector.Object_Observable, TraceStyle_Observable)
        If PlotConfig.Trace_Sun Then Graph.PlotXvsT("Sun altiude [°]", XAxis, Vector.Sun_Altitude, TraceStyle_Sun)
        If PlotConfig.Trace_NonObservable Then Graph.PlotXvsT("Object altitude [°]", XAxis, Vector.Object_Altitude, TraceStyle_Object)
        If PlotConfig.Trace_MoonAltitude Then Graph.PlotXvsT("Moon altitude [°]", XAxis, Vector.MoonAltitude, TraceStyle_MoonAltitude)
        If PlotConfig.Trace_MoonIllumination Then Graph.PlotXvsT("Moon illumination [%]", XAxis, Vector.MoonIllumination, TraceStyle_MoonIllumination)

        Graph.MainGraph.GraphPane.XAxis.Type = ZedGraph.AxisType.Date
        Graph.MainGraph.GraphPane.XAxis.MajorGrid.IsVisible = True
        Graph.MainGraph.GraphPane.YAxis.MajorGrid.IsVisible = True
        'Graph.ManuallyScaleXAxisLin(TimeLine.First.ToOADate, TimeLine.Last.ToOADate)
        Graph.ManuallyScaleYAxisLin(PlotConfig.Axis_YMin, PlotConfig.Axis_YMax)
        Graph.ForceUpdate()

    End Sub

    Private Sub tsmiGenerate_VisImage_Click(sender As Object, e As EventArgs) Handles tsmiGenerate_VisImage.Click

        Clipboard.SetImage(AstroInView.GenerateVisibilityImage(PlotConfig, Props))
        MsgBox("DONE.")

    End Sub

    Private Sub De()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub tsmiGenerate_ExcelExport_Click(sender As Object, e As EventArgs) Handles tsmiGenerate_ExcelExport.Click
        Using workbook As New ClosedXML.Excel.XLWorkbook
            Dim worksheet1 As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add(Props.ObjectName)
            worksheet1.Cell(1, 1).InsertData(New List(Of String)({"UTC Time", "JD", "Obj Altitude", "Obj Azimuth", "Sun altitude"}), True)
            For Idx As Integer = 0 To Vector.UTCTimes.Count - 1
                Dim Results As New List(Of Object)
                'Build the EXCEL result row
                Results.Add(Vector.UTCTimes(Idx))
                Results.Add(Vector.JD(Idx))
                Results.Add(Vector.ObjectPosition(Idx).ALT_deg)
                Results.Add(Vector.ObjectPosition(Idx).AZ_deg)
                Results.Add(Vector.SunPosition(Idx).AzAlt.Alt)
                Results.Add(Ato.AstroCalc.LST(Vector.UTCTimes(Idx), Props.Observatory_Longitude))
                worksheet1.Cell(Idx + 2, 1).InsertData(Results, True)
            Next Idx

            For Each col In worksheet1.ColumnsUsed
                col.AdjustToContents()
            Next col

            '4) Save and open
            Dim FileToGenerate As String = IO.Path.Combine(MyPath, "ObjectPos.xlsx")
            workbook.SaveAs(FileToGenerate)
            Ato.Utils.StartWithItsEXE(FileToGenerate)

        End Using

    End Sub

    Private Sub AstroInView_CalcProgress(Message As String) Handles AstroInView.CalcProgress
        tsslMain.Text = Message : De()
    End Sub

    Private Sub frmInView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Hide()
        End If
    End Sub

    Private Sub tsmiTime_Recalc_Click(sender As Object, e As EventArgs) Handles tsmiTime_Recalc.Click
        Recalc()
    End Sub

End Class