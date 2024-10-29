Option Explicit On
Option Strict On

'══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
' Class to display object visibility information as graph and table
'══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

Public Class frmInView

    Public Shared ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    '''<summary>All vectors over time.</summary>
    Public Class Vector
        '''<summary>UTC time - base time scale.</summary>
        Public Shared UTCTimes As DateTime() = Nothing
        '''<summary>Julian date.</summary>
        Public Shared JD As Double() = Nothing
        '''<summary>Object position.</summary>
        Public Shared ObjectPosition As Ato.AstroCalc.sAzAlt() = Nothing
        '''<summary>Sun position.</summary>
        Public Shared SunPosition As AstroCalc.NET.Sun.sSunPos() = Nothing
        '''<summary>Moon illumination.</summary>
        Public Shared MoonIllumination As Double() = Nothing
        '''<summary>Moon altitude.</summary>
        Public Shared MoonAltitude As Double() = Nothing
        '''<summary>Object azimuth.</summary>
        Public Shared Object_Azimuth As Double()
        '''<summary>Object altitude.</summary>
        Public Shared Object_Altitude As Double()
        '''<summary>Trace that contains the object altitude if the object is observable.</summary>
        Public Shared Object_Observable As Double()
        '''<summary>Sun altitude.</summary>
        Public Shared Sun_Altitude As Double()
        Public Shared Sub GetDerived()
            ReDim Object_Azimuth(ObjectPosition.GetUpperBound(0))
            ReDim Object_Altitude(ObjectPosition.GetUpperBound(0))
            ReDim Object_Observable(ObjectPosition.GetUpperBound(0))
            ReDim Sun_Altitude(ObjectPosition.GetUpperBound(0))
            For Idx As Integer = 0 To ObjectPosition.GetUpperBound(0)
                'Store vectors for display
                Object_Azimuth(Idx) = ObjectPosition(Idx).AZ_deg
                Sun_Altitude(Idx) = SunPosition(Idx).Altitude
            Next Idx
        End Sub
    End Class

    '''<summary>Object, location and parameters to generate plot for.</summary>
    Public Props As New cInViewProps
    '''<summary>ZEDGraph plotter.</summary>
    Private Plotter As cZEDGraph

    Private TraceStyle_Observable As New cZEDGraph.sGraphStyle(Color.Green, cZEDGraph.eCurveMode.Lines, 3)
    Private TraceStyle_Sun As New cZEDGraph.sGraphStyle(Color.Orange, cZEDGraph.eCurveMode.Dots)
    Private TraceStyle_Object As New cZEDGraph.sGraphStyle(Color.Red, cZEDGraph.eCurveMode.Lines)
    Private TraceStyle_MoonAltitude As New cZEDGraph.sGraphStyle(Color.DarkBlue, cZEDGraph.eCurveMode.Dots)
    Private TraceStyle_MoonIllumination As New cZEDGraph.sGraphStyle(Color.LightBlue, cZEDGraph.eCurveMode.Lines)

    '''<summary>Time spans as language codes.</summary>
    Public Class TimeSpans
        Public Shared ReadOnly OneSecond As New TimeSpan(0, 0, 0, 1)
        Public Shared ReadOnly OneMinute As New TimeSpan(0, 0, 1, 0)
        Public Shared ReadOnly OneHour As New TimeSpan(0, 1, 0, 0)
        Public Shared ReadOnly OneDay As New TimeSpan(1, 0, 0, 0)
        Public Shared ReadOnly OneMonth As New TimeSpan(31, 0, 0, 0)
        Public Shared ReadOnly OneYear As New TimeSpan(365, 0, 0, 0)
    End Class

    '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    ' Form processing code
    '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

    Private Sub frmInView_Load(sender As Object, e As EventArgs) Handles Me.Load
        pgMain.SelectedObject = Props
        Plotter = New cZEDGraph(zgcMain)
    End Sub

    Private Sub pgMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pgMain.MouseWheel
        Dim OneHour = 1
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
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
            Case "Limit_MinHeigth"
                Props.Limit_MinHeigth = Props.Limit_MinHeigth + 1 * Math.Sign(e.Delta)
            Case "Limit_MaxSunHeigth"
                Props.Limit_MaxSunHeigth = Props.Limit_MaxSunHeigth + 1 * Math.Sign(e.Delta)
        End Select
        Recalc()
    End Sub

    Private Sub pgMain_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles pgMain.PropertyValueChanged
        Recalc()
    End Sub

    Private Sub tsmiTime_Today_Click(sender As Object, e As EventArgs) Handles tsmiTime_Today.Click
        With Props
            .CalculationRange = TimeSpans.OneDay
            .UTC_Start = Now.ToUniversalTime
            .Stepping = TimeSpans.OneMinute
        End With
        Recalc()
    End Sub

    Private Sub tsmiTime_ThisMonth_Click(sender As Object, e As EventArgs) Handles tsmiTime_ThisMonth.Click
        With Props
            .CalculationRange = TimeSpans.OneMonth
            .UTC_Start = Now.ToUniversalTime
            .Stepping = TimeSpans.OneMinute
        End With
        Recalc()
    End Sub

    Private Sub tsmiTime_Next365Days_Click(sender As Object, e As EventArgs) Handles tsmiTime_Next365Days.Click
        With Props
            .CalculationRange = TimeSpans.OneYear
            .UTC_Start = Now.ToUniversalTime
            .Stepping = 10 * TimeSpans.OneMinute
        End With
        Recalc()
    End Sub

    Private Sub tsmiTime_NextDay_Click(sender As Object, e As EventArgs) Handles tsmiTime_NextDay.Click
        With Props
            .CalculationRange = TimeSpans.OneDay
            .UTC_Start = .UTC_Start.AddDays(1)
            .Stepping = TimeSpans.OneMinute
        End With
        Recalc()
    End Sub

    '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    ' Calculation code
    '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

    Public Sub Recalc()
        pgMain.SelectedObject = Props
        CalcOverTime()
    End Sub

    '''<summary>Calculate the requested parameters over time.</summary>
    Public Sub CalcOverTime()

        Dim TimingLog As New cLogging
        Dim TicTocID As Long

        'Exit on invalid conditions
        Dim ErrorText As String = String.Empty
        If Props.Stepping = TimeSpan.Zero Then ErrorText = "Stepping zero"
        If Props.CalculationRange = TimeSpan.Zero Then ErrorText = "Range zero"
        If String.IsNullOrEmpty(ErrorText) = False Then
            tsslMain.Text = "ERROR: <" & ErrorText & ">" : De()
            Exit Sub
        End If

        'Show status
        tsslMain.Text = "Calculating ..." : De()

        'Generate UTC timeline
        TicTocID = TimingLog.Tic("UTC list generation")
        Vector.UTCTimes = GetUTCVector(Props.UTC_Start, Props.Stepping, Props.UTC_Start.Add(Props.CalculationRange)).ToArray  'All points in UTC time to calculate the position
        TimingLog.Toc(TicTocID)

        'Generate derived timelines
        TicTocID = TimingLog.Tic("Derived timelines")
        ReDim Vector.JD(Vector.UTCTimes.Count - 1)                  'All points in julian dates
        Dim Ptr As Integer = 0
        For Each UTCMoment As DateTime In Vector.UTCTimes
            Vector.JD(Ptr) = AstroCoordinates.Ato.AstroCalc.JulianDateTime(UTCMoment)
            Ptr += 1
        Next UTCMoment
        TimingLog.Toc(TicTocID)

        'Show status
        tsslMain.Text = "Calculating <" & Vector.UTCTimes.Count.ValRegIndep & "> points ..." : De()

        'Calculate all positions
        TicTocID = TimingLog.Tic("ObjectPosition")
        Vector.ObjectPosition = Ato.AstroCalc.GetObjectPosition(Vector.UTCTimes, Props.GetLocation, Props.GetJ2000, Nothing)
        TimingLog.Toc(TicTocID)
        TicTocID = TimingLog.Tic("SunPosition")
        Vector.SunPosition = AstroCalc.NET.Sun.SunPos(Vector.UTCTimes, Props.GetLocation.Longitude_deg, Props.GetLocation.Latitude_deg)
        TimingLog.Toc(TicTocID)

        TicTocID = TimingLog.Tic("Moon")
        Vector.MoonAltitude = VSOPEx.MoonAltitude(Vector.UTCTimes, Props.GetLocation.Longitude_deg, Props.GetLocation.Latitude_deg) 'ASCOMDynamic.AstroUtils.MoonPhase(Vector.JD)
        Vector.MoonIllumination = VSOPEx.MoonIllumination(Vector.UTCTimes)
        TimingLog.Toc(TicTocID)



        'TimingLog.ShowLog()

        'Generate traces
        Vector.GetDerived()
        For Idx As Integer = 0 To Vector.ObjectPosition.GetUpperBound(0)
            'Observable
            Dim Observable As Boolean = True
            If Vector.Sun_Altitude(Idx) > Props.Limit_MaxSunHeigth Then Observable = False
            If Vector.ObjectPosition(Idx).ALT_deg < Props.Limit_MinHeigth Then Observable = False
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
        Select Case Props.Trace_TimeAxis
            Case cInViewProps.eTimeAxis.UTC
                XAxisCaption = "UTC Time"
                XAxisTimeOffset = New TimeSpan(0)
            Case cInViewProps.eTimeAxis.ObservatoryTime
                XAxisCaption = "Observatory time @ <" & Props.ObservatoryLocationName & ">"
                XAxisTimeOffset = New TimeSpan(Props.ObservatoryUTCOffset, 0, 0)
            Case cInViewProps.eTimeAxis.ObserverTime
                XAxisCaption = "Observer time @ <" & Props.OperatorLocationName & ">"
                XAxisTimeOffset = New TimeSpan(Props.OperatorUTCOffset, 0, 0)
        End Select

        If IsNothing(Plotter) = True Then Exit Sub

        'Plot traces
        Plotter.Clear()
        Plotter.SetCaptions("Object <" & Props.ObjectName & ">", XAxisCaption, "Object altitude [°]")

        If Props.Trace_Observable Then Plotter.PlotXvsT("Object observable", Vector.UTCTimes.ToArray.Add(XAxisTimeOffset), Vector.Object_Observable, TraceStyle_Observable, False)
        If Props.Trace_Sun Then Plotter.PlotXvsT("Sun", Vector.UTCTimes.ToArray.Add(XAxisTimeOffset), Vector.Sun_Altitude, TraceStyle_Sun, False)
        If Props.Trace_NonObservable Then Plotter.PlotXvsT("Object", Vector.UTCTimes.ToArray.Add(XAxisTimeOffset), Vector.Object_Altitude, TraceStyle_Object, False)
        Plotter.PlotXvsT("Moon altitude", Vector.UTCTimes.ToArray.Add(XAxisTimeOffset), Vector.MoonAltitude, TraceStyle_MoonAltitude, False)
        Plotter.PlotXvsT("Moon illumination", Vector.UTCTimes.ToArray.Add(XAxisTimeOffset), Vector.MoonIllumination, TraceStyle_MoonIllumination, False)

        zgcMain.GraphPane.XAxis.Type = ZedGraph.AxisType.Date
        'Plotter.ManuallyScaleXAxisLin(TimeLine.First.ToOADate, TimeLine.Last.ToOADate)
        Plotter.ManuallyScaleYAxisLin(Props.Axis_YMin, 100)
        Plotter.ForceUpdate()

        'Show status
        tsslMain.Text = "Calculated." : De()

    End Sub

    Private Sub tsmiGenerate_VisImage_Click(sender As Object, e As EventArgs) Handles tsmiGenerate_VisImage.Click

        'Generate a small bitmap that shows the sun and object visibility properties

        Dim Width As Integer = 365                              '1 year
        Dim Height As Integer = 24 * 12                         '5 minutes steps per day

        Dim UTCStart As New DateTime(Now.Year, 1, 1, 0, 0, 0)   '1. Januar of this year
        Dim UTCNoew As DateTime = Now.ToUniversalTime

        'All points in UTC time to calculate the position
        Dim UTCTimes As List(Of DateTime) = GetUTCVector(UTCStart, 5 * TimeSpans.OneMinute, UTCStart.Add(TimeSpans.OneYear))
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
                Dim SunAlt As Double = SunPositions(TimPtr).Altitude
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
                        If ObjectPosition(TimPtr).Alt > Props.Limit_MinHeigth Then
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

    '''<summary>Get a vector with UTC time values.</summary>
    '''<param name="UTC_Start">First entry (included).</param>
    '''<param name="Stepping">Entry stepping.</param>
    '''<param name="UTC_Stop">Last entry (included).</param>
    '''<returns></returns>
    Private Function GetUTCVector(ByVal UTC_Start As DateTime, ByVal Stepping As TimeSpan, ByVal UTC_Stop As DateTime) As List(Of DateTime)
        Dim RetVal As New List(Of DateTime)
        Dim UTCMoment As DateTime = UTC_Start
        Do
            RetVal.Add(UTCMoment)
            UTCMoment = UTCMoment.Add(Stepping)
            If UTCMoment >= UTC_Stop Then Exit Do
        Loop Until 1 = 0
        Return RetVal
    End Function

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
                Results.Add(Vector.SunPosition(Idx).Altitude)
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

End Class

'══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
' Class that describes what to show
'══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

Public Class cInViewProps

    Private Const Cat1 As String = "1. Object"
    Private Const Cat2 As String = "2. Observatory"
    Private Const Cat3 As String = "3. Operator / Observer"
    Private Const Cat4 As String = "4. Calculation"

    Public Enum eTimeAxis
        <ComponentModel.Description("UTC")> UTC
        <ComponentModel.Description("Observatory time")> ObservatoryTime
        <ComponentModel.Description("Observer time")> ObserverTime
    End Enum

    <ComponentModel.Category(Cat1)>
    <ComponentModel.DisplayName("1. Object name")>
    Public Property ObjectName As String = String.Empty

    <ComponentModel.Category(Cat1)>
    <ComponentModel.DisplayName("2. Right Ascension")>
    Public Property RightAscension As String = String.Empty

    <ComponentModel.Category(Cat1)>
    <ComponentModel.DisplayName("3. Declination")>
    Public Property Declination As String = String.Empty

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    <ComponentModel.Category(Cat2)>
    <ComponentModel.DisplayName("1. Location name")>
    Public Property ObservatoryLocationName As String = String.Empty

    <ComponentModel.Category(Cat2)>
    <ComponentModel.DisplayName("2. Latitude")>
    <ComponentModel.Description("Geodetic (ITRS) latitude; north positive (degrees)")>
    <ComponentModel.TypeConverter(GetType(Ato.AstroCalc.DoublePropertyConverter_dms))>
    Public Property ObservatoryLatitude As Double = Double.NaN

    <ComponentModel.Category(Cat2)>
    <ComponentModel.DisplayName("3. Longitude")>
    <ComponentModel.Description("Geodetic (ITRS) longitude; east positive (degrees)")>
    <ComponentModel.TypeConverter(GetType(Ato.AstroCalc.DoublePropertyConverter_dms))>
    Public Property ObservatoryLongitude As Double = Double.NaN

    <ComponentModel.Category(Cat2)>
    <ComponentModel.DisplayName("4. UTC offset")>
    <ComponentModel.Description("UTC offset [h] of the location")>
    Public Property ObservatoryUTCOffset As Integer = 0

    <ComponentModel.Category(Cat2)>
    <ComponentModel.DisplayName("5. Height")>
    Public Property ObservatoryHeigth As Double = 0

    <ComponentModel.Category(Cat2)>
    <ComponentModel.DisplayName("6. Temperature")>
    <ComponentModel.Description("Observer's location's ambient temperature (degrees Celsius)")>
    Public Property ObservatoryTemperature As Double = 0

    <ComponentModel.Category(Cat2)>
    <ComponentModel.DisplayName("7. Pressure")>
    <ComponentModel.Description("Observer's location's atmospheric pressure (millibars)")>
    Public Property ObservatoryPressure As Double = 1013.0

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    <ComponentModel.Category(Cat3)>
    <ComponentModel.DisplayName("1. Location name")>
    Public Property OperatorLocationName As String = "Holzkirchen"


    <ComponentModel.Category(Cat3)>
    <ComponentModel.DisplayName("2. UTC offset")>
    <ComponentModel.Description("UTC offset [h] of the operator")>
    Public Property OperatorUTCOffset As Integer = 2

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    <ComponentModel.Category(Cat4)>
    <ComponentModel.DisplayName("1. Start date and time")>
    Public Property UTC_Start As DateTime = Now.ToUniversalTime

    <ComponentModel.Category(Cat4)>
    <ComponentModel.DisplayName("2. Calculation duration")>
    Public Property CalculationRange As TimeSpan = New TimeSpan(1, 0, 0, 0)

    <ComponentModel.Category(Cat4)>
    <ComponentModel.DisplayName("3. Calculation stepping")>
    Public Property Stepping As TimeSpan = New TimeSpan(0, 1, 0)

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    <ComponentModel.Category("5. Plots")>
    <ComponentModel.DisplayName("4. Time axis")>
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

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    <ComponentModel.Category("4. Limits")>
    <ComponentModel.DisplayName("1. Minimum heigth above horizon")>
    Public Property Limit_MinHeigth As Double
        Get
            Return MyLimit_MinHeigth
        End Get
        Set(value As Double)
            MyLimit_MinHeigth = value
        End Set
    End Property
    Private MyLimit_MinHeigth As Double = 5.0

    <ComponentModel.Category("4. Limits")>
    <ComponentModel.DisplayName("2. Maximum sun heigth")>
    Public Property Limit_MaxSunHeigth As Double
        Get
            Return MyLimit_MaxSunHeigth
        End Get
        Set(value As Double)
            MyLimit_MaxSunHeigth = value
        End Set
    End Property
    Private MyLimit_MaxSunHeigth As Double = -12.0

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    <ComponentModel.Category("5. Plots")>
    <ComponentModel.DisplayName("1. Observable trace")>
    Public Property Trace_Observable As Boolean
        Get
            Return MyTrace_Observable
        End Get
        Set(value As Boolean)
            MyTrace_Observable = value
        End Set
    End Property
    Private MyTrace_Observable As Boolean = True

    <ComponentModel.Category("5. Plots")>
    <ComponentModel.DisplayName("2. Non-obserable trace")>
    Public Property Trace_NonObservable As Boolean
        Get
            Return MyTrace_NonObservable
        End Get
        Set(value As Boolean)
            MyTrace_NonObservable = value
        End Set
    End Property
    Private MyTrace_NonObservable As Boolean = True

    <ComponentModel.Category("5. Plots")>
    <ComponentModel.DisplayName("3. Sun trace")>
    Public Property Trace_Sun As Boolean
        Get
            Return MyTrace_Sun
        End Get
        Set(value As Boolean)
            MyTrace_Sun = value
        End Set
    End Property
    Private MyTrace_Sun As Boolean = True

    <ComponentModel.Category("5. Plots")>
    <ComponentModel.DisplayName("4. Y axis minimum")>
    Public Property Axis_YMin As Double
        Get
            Return MyAxis_YMin
        End Get
        Set(value As Double)
            MyAxis_YMin = value
        End Set
    End Property
    Private MyAxis_YMin As Double = -18

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    '''<summary>Get the current observer position.</summary>
    Public Function GetObserver() As ASCOMDynamic.sOnSurface
        Dim RetVal As New ASCOMDynamic.sOnSurface
        With RetVal
            .Temperature = ObservatoryTemperature
            .Pressure = ObservatoryPressure
            .Latitude = GetLocation.Latitude
            .Longitude = GetLocation.Longitude
            .Height = ObservatoryHeigth
        End With
        Return RetVal
    End Function

    Public Function GetLocation() As Ato.AstroCalc.sLatLong
        Return New Ato.AstroCalc.sLatLong(ObservatoryLatitude, ObservatoryLongitude)
    End Function

    Public Function GetJ2000() As Ato.AstroCalc.sRADec
        Return New Ato.AstroCalc.sRADec(RightAscension, Declination)
    End Function

End Class