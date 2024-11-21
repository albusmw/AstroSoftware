Option Explicit On
Option Strict On

'══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
' Class to display object visibility information as graph and table
'══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

Public Class frmInView

    Public Shared ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    'Vector calculator and properties
    Public WithEvents AstroInView As New cAstroInView
    Public Props As New cAstroInView.cProps
    Public Vector As New cAstroInView.cVectors

    Public PlotConfig As New cPlotConfig

    '''<summary>ZEDGraph plotter.</summary>
    Private Plotter As cZEDGraph

    Private TraceStyle_Sun As New cZEDGraph.sGraphStyle(Color.Orange, cZEDGraph.eCurveMode.Lines)
    Private TraceStyle_Object As New cZEDGraph.sGraphStyle(Color.Black, cZEDGraph.eCurveMode.Lines)
    Private TraceStyle_MoonAltitude As New cZEDGraph.sGraphStyle(Color.DarkBlue, cZEDGraph.eCurveMode.Lines)
    Private TraceStyle_MoonIllumination As New cZEDGraph.sGraphStyle(Color.LightBlue, cZEDGraph.eCurveMode.Lines)

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
                Dim CurrentValue = Props.ObservatoryLatitude
                CurrentValue += 10 / 60 * Math.Sign(e.Delta)
                Props.ObservatoryLatitude = CurrentValue
            Case "Longitude"
                Dim CurrentValue = Props.ObservatoryLongitude
                CurrentValue += 10 / 60 * Math.Sign(e.Delta)
                Props.ObservatoryLongitude = CurrentValue
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
        PlotTraces()
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
        PlotTraces()
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
        Vector = AstroInView.Calculate(Props)
        tsslMain.Text = "Calculated." : De()

        'TimingLog.ShowLog()

    End Sub

    Public Sub PlotTraces()

        'Generate traces
        For Idx As Integer = 0 To Vector.ObjectPosition.GetUpperBound(0)
            'Observable
            Dim Observable As Boolean = True
            If Vector.Sun_Altitude(Idx) > PlotConfig.Limit_SunMaxHeigth Then Observable = False
            If Vector.ObjectPosition(Idx).ALT_deg < PlotConfig.Limit_ObjectMinHeigth Then Observable = False
            If Vector.MoonAltitude(Idx) > PlotConfig.Limit_MoonMaxHeigth Then Observable = False
            If Observable Then
                Vector.Object_Observable(Idx) = Vector.ObjectPosition(Idx).ALT_deg
                Vector.Object_Altitude(Idx) = Double.NaN
            Else
                Vector.Object_Observable(Idx) = Double.NaN
                Vector.Object_Altitude(Idx) = Vector.ObjectPosition(Idx).ALT_deg
            End If
        Next Idx

        'Set x axis caption and data
        Dim XAxisCaption As String = String.Empty
        Dim XAxisTimeOffset As New TimeSpan(0)
        Dim XAxisStart As DateTime = Vector.UTCTimes.First
        Select Case PlotConfig.Trace_TimeAxis
            Case cPlotConfig.eTimeAxis.UTC
                XAxisCaption = "UTC Time"
                XAxisTimeOffset = New TimeSpan(0)
            Case cPlotConfig.eTimeAxis.ObservatoryTime
                XAxisCaption = "Observatory time @ " & Props.ObservatoryLocationName & ""
                XAxisTimeOffset = New TimeSpan(Props.ObservatoryUTCOffset, 0, 0)
            Case cPlotConfig.eTimeAxis.ObserverTime
                XAxisCaption = "Observer time @ " & Props.OperatorLocationName & ""
                XAxisTimeOffset = New TimeSpan(Props.OperatorUTCOffset, 0, 0)
        End Select
        XAxisStart = XAxisStart.Add(XAxisTimeOffset)
        XAxisCaption &= ", starting " & XAxisStart.ValRegIndep

        'Start plot
        If IsNothing(Plotter) = True Then Exit Sub
        Plotter.Clear()
        Plotter.SetCaptions("Object <" & Props.ObjectName & ">", XAxisCaption, "Object altitude [°]")

        'Plot traces
        Dim TraceStyle_Observable As New cZEDGraph.sGraphStyle(Color.DarkGreen, cZEDGraph.eCurveMode.LinesAndPoints, PlotConfig.ObservableTraceDotSize)
        If PlotConfig.Trace_Observable Then Plotter.PlotXvsT("Observable [°]", Vector.UTCTimes.ToArray.Add(XAxisTimeOffset), Vector.Object_Observable, TraceStyle_Observable, False)
        If PlotConfig.Trace_Sun Then Plotter.PlotXvsT("Sun altiude [°]", Vector.UTCTimes.ToArray.Add(XAxisTimeOffset), Vector.Sun_Altitude, TraceStyle_Sun, False)
        If PlotConfig.Trace_NonObservable Then Plotter.PlotXvsT("Object altitude [°]", Vector.UTCTimes.ToArray.Add(XAxisTimeOffset), Vector.Object_Altitude, TraceStyle_Object, False)
        If PlotConfig.Trace_MoonAltitude Then Plotter.PlotXvsT("Moon altitude [°]", Vector.UTCTimes.ToArray.Add(XAxisTimeOffset), Vector.MoonAltitude, TraceStyle_MoonAltitude, False)
        If PlotConfig.Trace_MoonIllumination Then Plotter.PlotXvsT("Moon illumination [%]", Vector.UTCTimes.ToArray.Add(XAxisTimeOffset), Vector.MoonIllumination, TraceStyle_MoonIllumination, False)

        zgcMain.GraphPane.XAxis.Type = ZedGraph.AxisType.Date
        zgcMain.GraphPane.XAxis.MajorGrid.IsVisible = True
        zgcMain.GraphPane.YAxis.MajorGrid.IsVisible = True
        'Plotter.ManuallyScaleXAxisLin(TimeLine.First.ToOADate, TimeLine.Last.ToOADate)
        Plotter.ManuallyScaleYAxisLin(PlotConfig.Axis_YMin, PlotConfig.Axis_YMax)
        Plotter.ForceUpdate()

    End Sub

    Private Sub tsmiGenerate_VisImage_Click(sender As Object, e As EventArgs) Handles tsmiGenerate_VisImage.Click

        'Generate a small bitmap that shows the sun and object visibility properties

        Dim Width As Integer = 365                              '1 year
        Dim Height As Integer = 24 * 12                         '5 minutes steps per day

        Dim UTCStart As New DateTime(Now.Year, 1, 1, 0, 0, 0)   '1. Januar of this year
        Dim UTCNoew As DateTime = Now.ToUniversalTime

        'All points in UTC time to calculate the position
        Dim UTCTimes As List(Of DateTime) = Props.GetUTCVector(UTCStart, 5 * cAstroInView.TimeSpans.OneMinute, UTCStart.Add(cAstroInView.TimeSpans.OneYear))
        Dim SunPositions As AstroCalc.NET.Sun.sSunPos() = AstroCalc.NET.Sun.SunPos(UTCTimes, Props.GetLocation.Longitude_deg, Props.GetLocation.Latitude_deg)
        Dim ObjectPosition As Ato.AstroCalc.sAzAlt() = Ato.AstroCalc.GetObjectPosition(UTCTimes, Props.GetLocation, Props.GetJ2000, Nothing)

        Dim ScaleImage As New cLockBitmap32Bit(Width, Height)
        ScaleImage.LockBits(False)
        Dim CopyPtr As Integer = 0
        For Y As Integer = 0 To ScaleImage.Height - 1
            For X As Integer = 0 To ScaleImage.Width - 1
                CopyPtr = (Y * ScaleImage.Width) + X
                Dim TimPtr As Integer = (X * ScaleImage.Height) + Y
                Dim ColorToUse As System.Drawing.Color
                Dim SunAlt As Double = SunPositions(TimPtr).AzAlt.Alt
                Select Case SunAlt
                    Case Is > 0
                        ColorToUse = System.Drawing.Color.FromArgb(255, 255, 255)
                    Case -6 To 0
                        ColorToUse = System.Drawing.Color.Orange
                    Case -12 To -6
                        ColorToUse = System.Drawing.Color.DarkOrange
                    Case -18 To -12
                        ColorToUse = System.Drawing.Color.DarkRed
                    Case Is < -18
                        ColorToUse = System.Drawing.Color.FromArgb(0, 0, 0)
                        If ObjectPosition(TimPtr).Alt > PlotConfig.Limit_ObjectMinHeigth Then
                            ColorToUse = System.Drawing.Color.FromArgb(0, 255, 0)
                        End If
                End Select
                ScaleImage.Pixels(CopyPtr) = (CInt(ColorToUse.A) << 24) + (CInt(ColorToUse.R) << 16) + (CInt(ColorToUse.G) << 8) + CInt(ColorToUse.B)
            Next X
        Next Y
        ScaleImage.UnlockBits()
        Clipboard.Clear()
        Clipboard.SetImage(ScaleImage.BitmapToProcess)
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
                Results.Add(Ato.AstroCalc.LST(Vector.UTCTimes(Idx), Props.ObservatoryLongitude))
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

End Class

'══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
' Class that describes what to show
'══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

Public Class cPlotConfig

    Private Const Cat1_Limits As String = "1. Observable limits"
    Private Const Cat2_Traces As String = "2. Active traces"
    Private Const Cat3_PlotConfig As String = "3. Plot configuration"

    Public Enum eTimeAxis
        <ComponentModel.Description("UTC")> UTC
        <ComponentModel.Description("Observatory time")> ObservatoryTime
        <ComponentModel.Description("Observer time")> ObserverTime
    End Enum

    '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

    <ComponentModel.Category(Cat1_Limits)>
    <ComponentModel.DisplayName("1. Minimum heigth above horizon")>
    Public Property Limit_ObjectMinHeigth As Double
        Get
            Return MyLimit_ObjectMinHeigth
        End Get
        Set(value As Double)
            MyLimit_ObjectMinHeigth = value
        End Set
    End Property
    Private MyLimit_ObjectMinHeigth As Double = 5.0

    <ComponentModel.Category(Cat1_Limits)>
    <ComponentModel.DisplayName("2. Maximum sun heigth")>
    Public Property Limit_SunMaxHeigth As Double
        Get
            Return MyLimit_SunMaxHeigth
        End Get
        Set(value As Double)
            MyLimit_SunMaxHeigth = value
        End Set
    End Property
    Private MyLimit_SunMaxHeigth As Double = -12.0

    <ComponentModel.Category(Cat1_Limits)>
    <ComponentModel.DisplayName("3. Maximum moon heigth")>
    Public Property Limit_MoonMaxHeigth As Double
        Get
            Return MyLimit_MoonMaxHeigth
        End Get
        Set(value As Double)
            MyLimit_MoonMaxHeigth = value
        End Set
    End Property
    Private MyLimit_MoonMaxHeigth As Double = 90.0

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    <ComponentModel.Category(Cat2_Traces)>
    <ComponentModel.DisplayName("1. Observable object")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    Public Property Trace_Observable As Boolean
        Get
            Return MyTrace_Observable
        End Get
        Set(value As Boolean)
            MyTrace_Observable = value
        End Set
    End Property
    Private MyTrace_Observable As Boolean = True

    <ComponentModel.Category(Cat2_Traces)>
    <ComponentModel.DisplayName("2. Object altitude")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    Public Property Trace_NonObservable As Boolean
        Get
            Return MyTrace_NonObservable
        End Get
        Set(value As Boolean)
            MyTrace_NonObservable = value
        End Set
    End Property
    Private MyTrace_NonObservable As Boolean = True

    <ComponentModel.Category(Cat2_Traces)>
    <ComponentModel.DisplayName("3. Sun altitude")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    Public Property Trace_Sun As Boolean
        Get
            Return MyTrace_Sun
        End Get
        Set(value As Boolean)
            MyTrace_Sun = value
        End Set
    End Property
    Private MyTrace_Sun As Boolean = True

    <ComponentModel.Category(Cat2_Traces)>
    <ComponentModel.DisplayName("4. Moon altitude")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    Public Property Trace_MoonAltitude As Boolean
        Get
            Return MyTrace_MoonAltitude
        End Get
        Set(value As Boolean)
            MyTrace_MoonAltitude = value
        End Set
    End Property
    Private MyTrace_MoonAltitude As Boolean = True

    <ComponentModel.Category(Cat2_Traces)>
    <ComponentModel.DisplayName("5. Moon illumination")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    Public Property Trace_MoonIllumination As Boolean
        Get
            Return MyTrace_MoonIllumination
        End Get
        Set(value As Boolean)
            MyTrace_MoonIllumination = value
        End Set
    End Property
    Private MyTrace_MoonIllumination As Boolean = True

    <ComponentModel.Category(Cat3_PlotConfig)>
    <ComponentModel.DisplayName("1. Time axis")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
    Public Property Trace_TimeAxis As eTimeAxis
        Get
            Return MyTrace_TimeAxis
        End Get
        Set(value As eTimeAxis)
            MyTrace_TimeAxis = value
        End Set
    End Property
    Private MyTrace_TimeAxis As eTimeAxis = eTimeAxis.UTC

    <ComponentModel.Category(Cat3_PlotConfig)>
    <ComponentModel.DisplayName("2. Y axis maximum")>
    Public Property Axis_YMax As Double
        Get
            Return MyAxis_YMax
        End Get
        Set(value As Double)
            MyAxis_YMax = value
        End Set
    End Property
    Private MyAxis_YMax As Double = 100

    <ComponentModel.Category(Cat3_PlotConfig)>
    <ComponentModel.DisplayName("3. Y axis minimum")>
    Public Property Axis_YMin As Double
        Get
            Return MyAxis_YMin
        End Get
        Set(value As Double)
            MyAxis_YMin = value
        End Set
    End Property
    Private MyAxis_YMin As Double = -12

    <ComponentModel.Category(Cat3_PlotConfig)>
    <ComponentModel.DisplayName("4. Observable trace dot size")>
    Public Property ObservableTraceDotSize As Integer
        Get
            Return MyObservableTraceDotSize
        End Get
        Set(value As Integer)
            MyObservableTraceDotSize = value
        End Set
    End Property
    Private MyObservableTraceDotSize As Integer = 2


    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════



End Class