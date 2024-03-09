Option Explicit On
Option Strict On
Imports ASCOM.Tools.Novas31

Public Class MainForm

    Private Structure sPosData
        Public Ra As Double
        Public Dec As Double
        Public Distance As Double
    End Structure


    Private DB As New cDB

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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


            ASCOM.Tools.Novas31.Novas.Equ2Hor(CurrentJdt, deltaT, Acc, 0.0, 0.0, OnSurfaceObserver, PosApparent.Ra, PosApparent.Dec, RefractionOption.NoRefraction, zenithDistance, azimuth, refractedRa, refractedDeclination)
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

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Ato.AstroCalc.RunTestCase_SelfBuild()
    End Sub

    Private Sub btnFromClipboard_Click(sender As Object, e As EventArgs) Handles btnFromClipboard.Click
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

End Class
