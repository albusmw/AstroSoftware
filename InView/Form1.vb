Option Explicit On
Option Strict On
Imports DocumentFormat.OpenXml.Office.Word
Imports InView.AstroCalc.NET

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

        Calculate()

    End Sub

    Private Sub Calculate()

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
        DB.Properties.SetToHolzkirchen()
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
        AddToLog("Site   : Latitude                   " & Ato.AstroCalc.Format360Degree(OnSurfaceObserver.Latitude).PadLeft(15) & " = " & OnSurfaceObserver.Latitude.ValRegIndep.PadLeft(15))
        AddToLog("         Longitude                  " & Ato.AstroCalc.Format360Degree(OnSurfaceObserver.Longitude).PadLeft(15) & " = " & OnSurfaceObserver.Longitude.ValRegIndep.PadLeft(15))
        AddToLog("         Height [m]                 " & OnSurfaceObserver.Height.ValRegIndep.PadLeft(15))
        AddToLog("Jupiter: Apparent Right Ascension   " & Ato.AstroCalc.FormatHMS(PosApparent.Ra).PadLeft(15) & " = " & PosApparent.Ra.ValRegIndep.PadLeft(15))
        AddToLog("                  Declination       " & Ato.AstroCalc.Format360Degree(PosApparent.Dec).PadLeft(15) & " = " & PosApparent.Dec.ValRegIndep.PadLeft(15))
        AddToLog("Jupiter: ?????    Right Ascension   " & Ato.AstroCalc.FormatHMS(PosLocal.Ra).PadLeft(15) & " = " & PosLocal.Ra.ValRegIndep.PadLeft(15))
        AddToLog("                  Declination       " & Ato.AstroCalc.Format360Degree(PosLocal.Dec).PadLeft(15) & " = " & PosLocal.Dec.ValRegIndep.PadLeft(15))
        AddToLog("         Elevation (from Transform) " & Ato.AstroCalc.Format360Degree(Transformer.ElevationTopocentric).PadLeft(15) & " = " & Transformer.ElevationTopocentric.ValRegIndep.PadLeft(15))
        AddToLog("         Elevation (from Equ2Hor)   " & Ato.AstroCalc.Format360Degree(90 - zenithDistance).PadLeft(15) & " = " & (90 - zenithDistance).ValRegIndep.PadLeft(15))
        AddToLog("         Azimuth (from Transform)   " & Ato.AstroCalc.Format360Degree(Transformer.AzimuthTopocentric).PadLeft(15) & " = " & Transformer.AzimuthTopocentric.ValRegIndep.PadLeft(15))
        AddToLog("         Azimuth (from Equ2Hor)     " & Ato.AstroCalc.Format360Degree(azimuth).PadLeft(15) & " = " & azimuth.ValRegIndep.PadLeft(15))
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
            Details.Add("   RA".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.FormatHMS(SelectedObject.RA))
            Details.Add("   Dec".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.Format360Degree(SelectedObject.Dec))
            Details.Add("   Alt".PadLeft(LeftWidth) & ":  " & Ato.AstroCalc.Format360Degree(Pos.Alt))
            Details.Add("   Az".PadLeft(LeftWidth) & ":  " & Ato.AstroCalc.Format360Degree(Pos.AZ))
            Details.Add("   Hour angle".PadLeft(LeftWidth) & ":  " & Ato.AstroCalc.FormatHMS(ObjectHourAngle * (24 / 360)))
            Details.Add("═══════════════════════════════════════════════════════")
            Details.Add(" UTC time".PadLeft(LeftWidth) & ": " & Format(CurrentUTC, "HH:mm:ss zzz"))
            Details.Add(" Location".PadLeft(LeftWidth) & ": " & CurrentLocation.ToString)
            Details.Add(" Local Siderial Time".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.LST(CurrentUTC, CurrentLocation.Longitude).ValRegIndep)
            Details.Add(" Local Siderial Time".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.LSTFormated(CurrentUTC, CurrentLocation.Longitude))
            Details.Add(" Location latitude".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.Format360Degree(CurrentLocation.Latitude))
            Details.Add(" Location longitude".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.Format360Degree(CurrentLocation.Longitude))
            Result.AddRange(Details)
        Catch ex As Exception
            Result.Add("ERROR: <" & ex.Message & ">")
        End Try

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
        AddToLog(" Location Lat  : " & Ato.AstroCalc.Format360Degree(CurrentLocation.Latitude_deg))
        AddToLog(" Location Long : " & Ato.AstroCalc.Format360Degree(CurrentLocation.Longitude_deg))
        AddToLog(" Object RA     : " & Ato.AstroCalc.FormatHMS(J2000.RA))
        AddToLog(" Object Dec    : " & Ato.AstroCalc.Format360Degree(J2000.DEC))
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
                RA = AstroParser.ParseRA(Line.Trim.Replace("RA center:", String.Empty).Trim)
            End If
            If Line.Trim.StartsWith("DEC center:") Then
                Dec = AstroParser.ParseDeclination(Line.Trim.Replace("DEC center:", String.Empty))
            End If
        Next Line
    End Sub

    Private Sub EntryUpdate(sender As Object, e As EventArgs) Handles tbObjectRA.TextChanged, tbObjectDec.TextChanged
        CalcAll()
    End Sub

    Private Sub CalcAll()

        Dim UTC_Start As DateTime = Now.ToUniversalTime
        Dim UTC_Stop As DateTime = UTC_Start.AddDays(7)
        Dim UTC_Delta As New TimeSpan(0, 1, 0)

        'Form timeline
        Dim MomentUTC As DateTime = UTC_Start
        Dim TimeLine As New List(Of DateTime)
        Do
            TimeLine.Add(MomentUTC)
            MomentUTC = MomentUTC.Add(UTC_Delta)
            If MomentUTC >= UTC_Stop Then Exit Do
        Loop Until 1 = 0

        'Calculate all positions
        Dim CurrentLocation As Ato.AstroCalc.sLatLong = Ato.AstroCalc.KnownLocations.DSC
        Dim J2000 As New Ato.AstroCalc.sRADec(tbObjectRA.Text, tbObjectDec.Text)
        Dim Stopper As New Stopwatch : Stopper.Reset() :
        Stopper.Start()
        Dim Positions As Ato.AstroCalc.sAzAlt() = Ato.AstroCalc.GetObjectPosition(TimeLine.ToArray, CurrentLocation, J2000, Nothing)
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
            Sun.SunPos(TimeLine(Idx), CurrentLocation.Longitude_deg, CurrentLocation.Latitude_deg, SunAz, SunHeight)
            SunHeights.Add(SunHeight)
            Object_Azimuth.Add(Positions(Idx).AZ_deg)
            Object_Altitude.Add(Positions(Idx).ALT_deg)
            'Observable
            If (SunHeight <= -6) And (Positions(Idx).ALT_deg >= 10) Then
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

    Private Sub tbObjectDec_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbObjectDec.MouseWheel
        Dim CurrentValue As Double = AstroParser.ParseDeclination(tbObjectDec.Text)
        CurrentValue += (10 / 60) * Math.Sign(e.Delta)
        tbObjectDec.Text = Ato.AstroCalc.Format360Degree(CurrentValue)
    End Sub

    Private Sub tbObjectRA_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbObjectRA.MouseWheel
        Dim CurrentValue As Double = AstroParser.ParseRA(tbObjectRA.Text)
        CurrentValue += (10 / (24 * 60)) * Math.Sign(e.Delta)
        tbObjectRA.Text = Ato.AstroCalc.FormatHMS(CurrentValue)
    End Sub

End Class
