Option Explicit On
Option Strict On

Public Class MainForm

    Private Structure sPosData
        Public Ra As Double
        Public Dec As Double
        Public Distance As Double
    End Structure

    Private DB As New cDB
    Private Plotter As cZEDGraph

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Plotter = New cZEDGraph(zgcMain)
        Dim X As New ASCOM.Tools.RiseSetTimes

        DB.Properties.Latitude = Ato.AstroCalc.KnownLocations.DSC.Latitude.ToDegMinSec
        DB.Properties.Longitude = Ato.AstroCalc.KnownLocations.DSC.Longitude.ToDegMinSec
        pgMain.SelectedObject = DB.Properties

    End Sub

    Private Sub ASCOMCalculate()

        '═════════════════════════════════════════════════════════════════════════════
        'Definitions
        '═════════════════════════════════════════════════════════════════════════════

        Dim ObjectToProcess As New ASCOM.Tools.Novas31.Object3
        Dim OnSurfaceObserver As New ASCOM.Tools.Novas31.OnSurface
        Dim Obs As New ASCOM.Tools.Novas31.Observer

        Dim PosApparent As sPosData
        Dim PosLocal As sPosData
        Dim Place As New ASCOM.Tools.Novas31.SkyPos

        'Object's un-refracted horizon coordinates
        Dim zenithDistance As Double = Double.NaN
        Dim azimuth As Double = Double.NaN
        Dim refractedRa As Double = Double.NaN
        Dim refractedDeclination As Double = Double.NaN

        '═════════════════════════════════════════════════════════════════════════════
        'Calculation preparation
        '═════════════════════════════════════════════════════════════════════════════

        'Line 699 of test data
        Dim DateToCalculate As New DateTime(2024, 2, 7, 10, 37, 0, DateTimeKind.Utc)
        Dim JdTt As Double = ASCOM.Tools.Utilities.JulianDateFromDateTime(DateToCalculate)  'ASCOM.Tools.Utilities.JulianDateUtc
        Dim deltaT As Double = Double.NaN

        With ObjectToProcess
            .Name = "Jupiter"
            .Type = ASCOM.Tools.Novas31.ObjectType.MajorPlanetSunOrMoon
            .Number = ASCOM.Tools.Novas31.Body.Jupiter
        End With

        'Set observer parameters
        OnSurfaceObserver = DB.Properties.GetObserver

        'Set on-surface observer parameters
        Obs.Where = ASCOM.Tools.Novas31.ObserverLocation.EarthSurface
        Obs.OnSurf = OnSurfaceObserver


        Dim Acc As ASCOM.Tools.Novas31.Accuracy = ASCOM.Tools.Novas31.Accuracy.Full

        '═════════════════════════════════════════════════════════════════════════════
        'Calculation
        '═════════════════════════════════════════════════════════════════════════════

        Dim Stopper As New Stopwatch
        Stopper.Restart() : Stopper.Start()

        Dim zenithDistanceList As New List(Of Double)
        Dim JdTtSteps_min As Double = 1 / (24 * 60)
        For CurrentJdt As Double = JdTt - 1 To JdTt + 1 Step JdTtSteps_min

            deltaT = ASCOM.Tools.Utilities.DeltaT(CurrentJdt)                   'Returns the value of DeltaT (Terrestrial time minus Universal Time = TT-UT1) at the given Julian date.

            'Object's topocentric coordinates
            Dim rc1 As Short = ASCOM.Tools.Novas31.Novas.TopoPlanet(CurrentJdt, ObjectToProcess, deltaT, OnSurfaceObserver, Acc, PosApparent.Ra, PosApparent.Dec, PosApparent.Distance)

            'Object's local place of a solar system body.
            Dim rc2 As Short = ASCOM.Tools.Novas31.Novas.LocalPlanet(CurrentJdt, ObjectToProcess, deltaT, OnSurfaceObserver, Acc, PosLocal.Ra, PosLocal.Dec, PosLocal.Distance)


            Dim rc3 As Short = ASCOM.Tools.Novas31.Novas.Place(CurrentJdt, ObjectToProcess, Obs, deltaT, ASCOM.Tools.Novas31.CoordSys.EquinoxOfDate, Acc, Place)


            ASCOM.Tools.Novas31.Novas.Equ2Hor(CurrentJdt, deltaT, Acc, 0.0, 0.0, OnSurfaceObserver, PosApparent.Ra, PosApparent.Dec, ASCOM.Tools.Novas31.RefractionOption.NoRefraction, zenithDistance, azimuth, refractedRa, refractedDeclination)
            zenithDistanceList.Add(zenithDistance)

        Next CurrentJdt

        Stopper.Stop()

        'Convert Ra-Dec to Alt-Az
        Dim Transformer As New ASCOM.Tools.Transform
        With Transformer
            .SetApparent(PosLocal.Ra, PosLocal.Dec)
            .JulianDateTT = JdTt
            .SiteElevation = OnSurfaceObserver.Height
            .SiteLatitude = OnSurfaceObserver.Latitude
            .SiteLongitude = OnSurfaceObserver.Longitude
            .SitePressure = OnSurfaceObserver.Pressure
            .SiteTemperature = OnSurfaceObserver.Temperature
        End With

        'Output
        AddToLog("Date and time                       " & DateToCalculate.ValRegIndep)
        AddToLog("Julian Date                         " & JdTt.ValRegIndep)
        AddToLog("DeltaT                              " & deltaT.ValRegIndep)
        AddToLog("Site   : Latitude                   " & OnSurfaceObserver.Latitude.ToDegMinSec.PadLeft(15) & " = " & OnSurfaceObserver.Latitude.ValRegIndep.PadLeft(15))
        AddToLog("         Longitude                  " & OnSurfaceObserver.Longitude.ToDegMinSec.PadLeft(15) & " = " & OnSurfaceObserver.Longitude.ValRegIndep.PadLeft(15))
        AddToLog("         Height [m]                 " & OnSurfaceObserver.Height.ValRegIndep.PadLeft(15))
        AddToLog("Jupiter: Apparent Right Ascension   " & PosApparent.Ra.ToHMS.PadLeft(15) & " = " & PosApparent.Ra.ValRegIndep.PadLeft(15))
        AddToLog("                  Declination       " & PosApparent.Dec.ToDegMinSec.PadLeft(15) & " = " & PosApparent.Dec.ValRegIndep.PadLeft(15))
        AddToLog("Jupiter: ?????    Right Ascension   " & PosLocal.Ra.ToHMS.PadLeft(15) & " = " & PosLocal.Ra.ValRegIndep.PadLeft(15))
        AddToLog("                  Declination       " & PosLocal.Dec.ToDegMinSec.PadLeft(15) & " = " & PosLocal.Dec.ValRegIndep.PadLeft(15))
        AddToLog("         Elevation (from Transform) " & Transformer.ElevationTopocentric.ToDegMinSec.PadLeft(15) & " = " & Transformer.ElevationTopocentric.ValRegIndep.PadLeft(15))
        AddToLog("         Elevation (from Equ2Hor)   " & (90 - zenithDistance).ToDegMinSec.PadLeft(15) & " = " & (90 - zenithDistance).ValRegIndep.PadLeft(15))
        AddToLog("         Azimuth (from Transform)   " & Transformer.AzimuthTopocentric.ToDegMinSec.PadLeft(15) & " = " & Transformer.AzimuthTopocentric.ValRegIndep.PadLeft(15))
        AddToLog("         Azimuth (from Equ2Hor)     " & azimuth.ToDegMinSec.PadLeft(15) & " = " & azimuth.ValRegIndep.PadLeft(15))
        AddToLog("Duration                            " & Stopper.ElapsedMilliseconds.ValRegIndep & " ms")

    End Sub

    Private Sub tsmiFile_Exit_Click(sender As Object, e As EventArgs) Handles tsmiFile_Exit.Click
        End
    End Sub

    Private Sub ObjectVisibilityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiCalc_Visibility.Click

        Dim LeftWidth As Integer = 20
        Dim SelectedObject As New sObjectInfo
        Dim Result As New List(Of String)

        Try
            'Display selected object information

            Dim CurrentUTC As DateTime = DateTime.UtcNow
            Dim CurrentLocation As Ato.AstroCalc.sLatLong = Ato.AstroCalc.KnownLocations.DSC
            Dim ObjectCoord As New Ato.AstroCalc.sRADec(SelectedObject.RA, SelectedObject.Dec)
            Dim ObjectHourAngle As Double = Double.NaN
            Dim Pos As Ato.AstroCalc.sAzAlt '= GetObjectPosition_ASCOM(CurrentUTC, CurrentLocation, ObjectCoord)
            Dim Details As New List(Of String)
            Details.Add(SelectedObject.VerboseName & " - Catalog: " & SelectedObject.Catalog)
            Details.Add("═══════════════════════════════════════════════════════")
            Details.Add(" mag".PadLeft(LeftWidth) & ": " & SelectedObject.Mag.ValRegIndep)
            If SelectedObject.Diameter > 0 Then Details.Add(" diameter".PadLeft(LeftWidth) & ": " & SelectedObject.Diameter.ValRegIndep & " '")
            If SelectedObject.HIP > 0 Then Details.Add(" HIP".PadLeft(LeftWidth) & ": " & SelectedObject.HIP.ToString.Trim)
            If SelectedObject.HD > 0 Then Details.Add(" HD".PadLeft(LeftWidth) & ": " & SelectedObject.HD.ToString.Trim)
            Details.Add("   RA".PadLeft(LeftWidth) & ": " & SelectedObject.RA.ToHMS)
            Details.Add("   Dec".PadLeft(LeftWidth) & ": " & SelectedObject.Dec.ToDegMinSec)
            Details.Add("   Alt".PadLeft(LeftWidth) & ":  " & Pos.Alt.ToDegMinSec)
            Details.Add("   Az".PadLeft(LeftWidth) & ":  " & Pos.AZ.ToDegMinSec)
            Details.Add("   Hour angle".PadLeft(LeftWidth) & ":  " & (ObjectHourAngle * (24 / 360)).ToHMS)
            Details.Add("═══════════════════════════════════════════════════════")
            Details.Add(" UTC time".PadLeft(LeftWidth) & ": " & Format(CurrentUTC, "HH:mm:ss zzz"))
            Details.Add(" Location".PadLeft(LeftWidth) & ": " & CurrentLocation.ToString)
            Details.Add(" Local Siderial Time".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.LST(CurrentUTC, CurrentLocation.Longitude).ValRegIndep)
            Details.Add(" Local Siderial Time".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.LSTFormated(CurrentUTC, CurrentLocation.Longitude))
            Details.Add(" Location latitude".PadLeft(LeftWidth) & ": " & CurrentLocation.Latitude.ToDegMinSec)
            Details.Add(" Location longitude".PadLeft(LeftWidth) & ": " & CurrentLocation.Longitude.ToDegMinSec)
            Result.AddRange(Details)
        Catch ex As Exception
            Result.Add("ERROR: <" & ex.Message & ">")
        End Try

        AddToLog(Result)

    End Sub

    Private Sub tsmiCalc_TC1_Click(sender As Object, e As EventArgs) Handles tsmiCalc_TC1.Click
        tbLog.Text = Join(Ato.AstroCalc.RunTestCase_SelfBuild.ToArray, System.Environment.NewLine)
    End Sub

    Private Sub tsmiCalc_TC2_Click(sender As Object, e As EventArgs) Handles tsmiCalc_TC2.Click

        Dim AllResources As String() = Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames
        Dim Content As String() = ReadAllLines(AllResources(1))

        Dim CurrentLocation As New Ato.AstroCalc.sLatLong
        Dim TimeZone As Integer = -4
        Dim J2000 As New Ato.AstroCalc.sRADec

        'Find start of tracking details
        Dim TrackingDetailsStart As Integer = -1
        For Idx As Integer = 0 To Content.GetUpperBound(0)
            Dim Keyword As String = Content(Idx).Trim
            If Keyword.ToUpper.StartsWith("Location".ToUpper) Then
                Dim LongStr As String() = Content(Idx + 1).Trim.Split(" "c)
                Dim LatStr As String() = Content(Idx + 2).Trim.Split(" "c)
                CurrentLocation = New Ato.AstroCalc.sLatLong(cCCDGuide.GetDMS(LatStr, 15), cCCDGuide.GetDMS(LongStr, 14))
            End If
            If Keyword.ToUpper.StartsWith("J2000".ToUpper) Then
                Dim Entry As String() = Content(Idx).Trim.Split(" "c)
                J2000 = New Ato.AstroCalc.sRADec(cCCDGuide.GetHMS(Entry, 7), cCCDGuide.GetDMS(Entry, 12))
            End If
            If Keyword.ToUpper.StartsWith("Tracking details".ToUpper) Then
                TrackingDetailsStart = Idx + 6
            End If
        Next Idx

        'Plot some header data to the log
        ClearLog()
        AddToLog(" Location Lat  : " & CurrentLocation.Latitude_deg.ToDegMinSec)
        AddToLog(" Location Long : " & CurrentLocation.Longitude_deg.ToDegMinSec)
        AddToLog(" Object RA     : " & J2000.RA.ToHMS)
        AddToLog(" Object Dec    : " & J2000.DEC.ToDegMinSec)
        AddToLog("Date      |Time      |DateTime           | AZ CCD   |ALT CCD   |  AZ      |ALT       | LST")

        Dim Alt_CCD As New List(Of Double)
        Dim Azi_CCD As New List(Of Double)
        Dim Alt_Own As New List(Of Double)
        Dim Azi_Own As New List(Of Double)

        'Get data and compare with own generated data
        For Idx As Integer = TrackingDetailsStart To Content.GetUpperBound(0)
            'Get input data from CCDGuide table
            Dim SingleLine As String() = cCCDGuide.OnlySingleSpaces(Content(Idx)).Split(" ")
            Dim MomentLocal As DateTime = cCCDGuide.GetDateTime(SingleLine, 0)      'CCDGuide time and date are local
            Dim MomentUTC As DateTime = MomentLocal.AddHours(-TimeZone)
            'Calculate
            Dim ObjectHourAngle As Double = Double.NaN
            Dim JulianDay As Double = Ato.AstroCalc.JulianDateTime(MomentUTC)
            Dim LocalStarTime As String = Ato.AstroCalc.LSTFormated(MomentUTC, CurrentLocation.Longitude_deg)
            Dim CCDAltAz As New Ato.AstroCalc.sAzAlt(SingleLine(2).ValRegIndep, SingleLine(3).ValRegIndep)
            Dim MyAltAz As Ato.AstroCalc.sAzAlt = Ato.AstroCalc.GetObjectPosition(MomentUTC, CurrentLocation, J2000, ObjectHourAngle)
            'Store data for display
            Alt_CCD.Add(CCDAltAz.Alt)
            Azi_CCD.Add(CCDAltAz.AZ)
            Alt_Own.Add(MyAltAz.Alt)
            Azi_Own.Add(MyAltAz.AZ)
            'Output data to the log
            Dim LogLine As New List(Of String)
            LogLine.Add(SingleLine(0).PadLeft(10, " "c))                            'Date
            LogLine.Add(SingleLine(1).PadLeft(10, " "c))                            'Time
            LogLine.Add(MomentLocal.ValRegIndep)                                    'Moment [parsed]
            LogLine.Add(SingleLine(2).PadLeft(10, " "c))                            'Azimuth [CCDGuide]
            LogLine.Add(SingleLine(3).PadLeft(10, " "c))                            'Altitude [CCDGuide]
            LogLine.Add(MyAltAz.AZ.ValRegIndep("000.000").PadLeft(10, " "c))        'Azimuth [self calculated]
            LogLine.Add(MyAltAz.Alt.ValRegIndep("##0.000").PadLeft(10, " "c))       'Altitude [self calculated]
            LogLine.Add(LocalStarTime.PadLeft(10, " "c))                            'LocalStartTime [self calculated]
            AddToLog(Join(LogLine.ToArray, "|"))
        Next Idx

        'Current status: - Altitude is correct (only small calculation errors).
        '                - Azimuth seems to be as "half speed" or "wrong sign"  

        'Plotter.PlotXvsY("CCDGuide", Azi_CCD.ToArray, Alt_CCD.ToArray, New cZEDGraph.sGraphStyle(Color.Green, cZEDGraph.eCurveMode.Dots))
        'Plotter.PlotXvsY("My own", Azi_Own.ToArray, Alt_Own.ToArray, New cZEDGraph.sGraphStyle(Color.Red, cZEDGraph.eCurveMode.Dots))
        Plotter.SetCaptions("Object trace", "Azimuth", "Altitude")
        Plotter.PlotData("CCDGuide", Azi_CCD.ToArray, New cZEDGraph.sGraphStyle(Color.Green, cZEDGraph.eCurveMode.Lines))
        Plotter.PlotData("My own", Azi_Own.ToArray, New cZEDGraph.sGraphStyle(Color.Red, cZEDGraph.eCurveMode.Dots))
        Plotter.SetCaptions("Object trace", "Calculation index", "Azimuth")
        Plotter.ManuallyScaleYAxisLin(0, 360)
        Plotter.ForceUpdate()

    End Sub

    Private Function ReadAllLines(ByRef ResourceName As String) As String()
        Dim RetVal As New List(Of String)
        Dim ResourceStream As System.IO.Stream = Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream(ResourceName)
        Dim TextStream As New System.IO.StreamReader(ResourceStream, System.Text.Encoding.UTF8)
        Do While TextStream.EndOfStream = False
            RetVal.Add(TextStream.ReadLine())
        Loop
        Return RetVal.ToArray
    End Function

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
    ' Log output
    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    Private Sub ClearLog()
        tbLog.Text = String.Empty
    End Sub

    Private Sub AddToLog(ByVal Text As List(Of String))
        For Each Entry As String In Text
            AddToLog(Entry)
        Next Entry
    End Sub

    Private Sub AddToLog(ByVal Text As String)
        If tbLog.Text.Length > 0 Then
            tbLog.Text &= System.Environment.NewLine & Text
        Else
            tbLog.Text &= Text
        End If
    End Sub

    Private Sub tsmiCalc_ClipAstrobin_Click(sender As Object, e As EventArgs) Handles tsmiCalc_ClipAstrobin.Click
        'Parse the data from the coordinates given on the AstroBin object page
        Dim ClipContent As String() = Split(Clipboard.GetText, System.Environment.NewLine)
        Dim RA As Double = Double.NaN
        Dim Dec As Double = Double.NaN
        For Each Line As String In ClipContent
            If Line.Trim.StartsWith("RA center:") Then
                RA = Line.Trim.Replace("RA center:", String.Empty).ParseRA
            End If
            If Line.Trim.StartsWith("DEC center:") Then
                Dec = Line.Trim.Replace("DEC center:", String.Empty).ParseDegree
            End If
        Next Line
    End Sub

    Private Sub CalcAll()

        Dim UTC_Stop As DateTime = DB.Properties.UTC_Start.Add(DB.Properties.UTC_Range)

        'Form timeline
        Dim MomentUTC As DateTime = DB.Properties.UTC_Start
        Dim TimeLine As New List(Of DateTime)
        Do
            TimeLine.Add(MomentUTC)
            MomentUTC = MomentUTC.Add(DB.Properties.UTC_Stepping)
            If MomentUTC >= UTC_Stop Then Exit Do
        Loop Until 1 = 0

        'Calculate all positions
        Dim Stopper As New Stopwatch : Stopper.Reset()
        Stopper.Start()
        Dim Positions As Ato.AstroCalc.sAzAlt() = Ato.AstroCalc.GetObjectPosition(TimeLine.ToArray, DB.Properties.GetLocation, DB.Properties.GetJ2000, Nothing)
        Stopper.Stop()
        ClearLog()
        AddToLog("Calculation took " & Stopper.ElapsedMilliseconds & " ms")

        'Plot
        Dim SunHeights As New List(Of Double)
        Dim Object_Azimuth As New List(Of Double)
        Dim Object_Altitude As New List(Of Double)
        Dim Object_AltitudeOK As New List(Of Double)
        For Idx As Integer = 0 To Positions.GetUpperBound(0)
            Dim SunAz As Double = Double.NaN
            Dim SunHeight As Double = Double.NaN
            AstroCalc.NET.Sun.SunPos(TimeLine(Idx), DB.Properties.GetLocation.Longitude_deg, DB.Properties.GetLocation.Latitude_deg, SunAz, SunHeight)
            SunHeights.Add(SunHeight)
            Object_Azimuth.Add(Positions(Idx).AZ_deg)
            Object_Altitude.Add(Positions(Idx).ALT_deg)
            'Observable
            If (SunHeight <= DB.Properties.Limit_MaxSunHeigth) And (Positions(Idx).ALT_deg >= DB.Properties.Limit_MinHeigth) Then
                Object_AltitudeOK.Add(Positions(Idx).ALT_deg)
            Else
                Object_AltitudeOK.Add(Double.NaN)
            End If
        Next Idx
        If IsNothing(Plotter) = True Then Exit Sub

        'Plot
        Plotter.Clear()
        Plotter.SetCaptions("Object trace", "Time", "Altitude")
        Plotter.PlotXvsT("Trace", TimeLine.ToArray, Object_AltitudeOK.ToArray, New cZEDGraph.sGraphStyle(Color.Green, cZEDGraph.eCurveMode.Dots), False)
        zgcMain.GraphPane.XAxis.Type = ZedGraph.AxisType.Date
        'Plotter.ManuallyScaleXAxisLin(TimeLine.First.ToOADate, TimeLine.Last.ToOADate)
        Plotter.ManuallyScaleYAxisLin(0, 90)
        Plotter.ForceUpdate()

    End Sub

    Private Sub pgMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pgMain.MouseWheel
        Dim OneHour As Integer = 1
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
            Case "Declination"
                Dim CurrentValue As Double = DB.Properties.Declination.ParseDegree
                CurrentValue += 10 / 60 * Math.Sign(e.Delta)
                DB.Properties.Declination = CurrentValue.ToDegMinSec
            Case "RightAscension"
                Dim CurrentValue As Double = DB.Properties.RightAscension.ParseRA
                CurrentValue += 10 / (24 * 60) * Math.Sign(e.Delta)
                DB.Properties.RightAscension = CurrentValue.ToHMS
            Case "Latitude"
                Dim CurrentValue As Double = DB.Properties.Latitude.ParseDegree
                CurrentValue += 10 / 60 * Math.Sign(e.Delta)
                DB.Properties.Latitude = CurrentValue.ToDegMinSec
            Case "Longitude"
                Dim CurrentValue As Double = DB.Properties.Longitude.ParseDegree
                CurrentValue += 10 / 60 * Math.Sign(e.Delta)
                DB.Properties.Longitude = CurrentValue.ToDegMinSec
            Case "UTC_Start"
                DB.Properties.UTC_Start = DB.Properties.UTC_Start.Add(New TimeSpan(OneHour * Math.Sign(e.Delta), 0, 0))
            Case "UTC_Range"
                DB.Properties.UTC_Range = DB.Properties.UTC_Range.Add(New TimeSpan(OneHour * Math.Sign(e.Delta), 0, 0))
            Case "Limit_MinHeigth"
                DB.Properties.Limit_MinHeigth = DB.Properties.Limit_MinHeigth + (1 * Math.Sign(e.Delta))
            Case "Limit_MaxSunHeigth"
                DB.Properties.Limit_MaxSunHeigth = DB.Properties.Limit_MaxSunHeigth + (1 * Math.Sign(e.Delta))
        End Select
        pgMain.SelectedObject = DB.Properties
        CalcAll()
    End Sub

    Private Sub pgMain_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles pgMain.PropertyValueChanged
        CalcAll()
    End Sub

End Class
