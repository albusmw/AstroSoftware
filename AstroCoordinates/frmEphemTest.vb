Option Explicit On
Option Strict On

Public Class frmEphemTest

    Private Horizons As cHorizons_Loader

    Private Horizons_Moments As List(Of Date)
    Private Horizons_Azi As List(Of Double)
    Private Horizons_Elev As List(Of Double)

    Private Pos_Long As Double = 11.691474
    Private Pos_Lat As Double = 47.87851
    Private Pos_Heigth As Double = 677

    Private Moment_Start As New Date(2026, 11, 16, 0, 0, 0, DateTimeKind.Utc)
    Private Moment_Stop As New Date(2026, 11, 18, 0, 0, 0, DateTimeKind.Utc)
    Private Moment_Steps As New TimeSpan(0, 5, 0)

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click

        'Try to run the moon calculation parallel -> creates speed-up
        Dim UTCTimes As DateTime() = cAstroInView.GetUTCVector(Moment_Start, Moment_Steps, Moment_Stop).ToArray

        Dim Vec_Elevation1() As Double = {} : Dim Vec_Azimuth1() As Double = {}
        Dim Vec_Elevation2() As Double = {} : Dim Vec_Azimuth2() As Double = {}
        Dim Vec_Elevation3() As Double = {} : Dim Vec_Azimuth3() As Double = {}
        Dim Vec_Elevation4() As Double = {} : Dim Vec_Azimuth4() As Double = {}

        Dim Stopper As New Stopwatch

        'AstroAlgo - seriell (reference)
        Stopper.Reset() : Stopper.Start()
        VSOPEx.RunParallel = False
        VSOPEx.CalcPackage = VSOPEx.eCalcPackage.AstroAlgo
        VSOPEx.Moon(UTCTimes, Pos_Long, Pos_Lat, Vec_Elevation1, Vec_Azimuth1)
        Stopper.Stop()
        Dim T1 As Double = Stopper.ElapsedMilliseconds

        'AstroAlgo - parallel
        Stopper.Reset() : Stopper.Start()
        VSOPEx.RunParallel = True
        VSOPEx.Moon(UTCTimes, Pos_Long, Pos_Lat, Vec_Elevation2, Vec_Azimuth2)
        Stopper.Stop()
        Dim T2 As Double = Stopper.ElapsedMilliseconds

        'VSOP87 - not fully implemented (moon only as EMB)
        Stopper.Reset() : Stopper.Start()
        VSOPEx.RunParallel = True
        VSOPEx.Moon_VSOP87(UTCTimes, Pos_Long, Pos_Lat, Vec_Elevation3, Vec_Azimuth3)
        Stopper.Stop()
        Dim T3 As Double = Stopper.ElapsedMilliseconds

        'CosineKitty
        Stopper.Reset() : Stopper.Start()
        VSOPEx.RunParallel = True
        VSOPEx.CalcPackage = VSOPEx.eCalcPackage.CosineKitty
        VSOPEx.Moon(UTCTimes, Pos_Long, Pos_Lat, Vec_Elevation4, Vec_Azimuth4)
        Stopper.Stop()
        Dim T4 As Double = Stopper.ElapsedMilliseconds

        'Debug
        Dim PlotForm As New frmZEDGraph
        Dim Plotter As New cZEDGraph(PlotForm.zgcMain)

        Plotter.PlotXvsT("Horizon Azimuth", Horizons_Moments, Horizons_Azi, New cZEDGraph.sGraphStyle(Color.DarkGreen, cZEDGraph.eCurveMode.Dots))
        Plotter.PlotXvsT("Horizon Elevation", Horizons_Moments, Horizons_Elev, New cZEDGraph.sGraphStyle(Color.DarkRed, cZEDGraph.eCurveMode.Dots))

        Plotter.PlotXvsT("AstroAlgo Elevation", UTCTimes, Vec_Elevation1, New cZEDGraph.sGraphStyle(Color.Black))
        Plotter.PlotXvsT("AstroAlgo Azimuth", UTCTimes, Vec_Azimuth1, New cZEDGraph.sGraphStyle(Color.Gray))

        Plotter.PlotXvsT("CosineKitty Azimuth", UTCTimes, Vec_Azimuth4, New cZEDGraph.sGraphStyle(Color.OrangeRed))
        Plotter.PlotXvsT("CosineKitty Elevation", UTCTimes, Vec_Elevation4, New cZEDGraph.sGraphStyle(Color.Red))

        PlotForm.Show()

    End Sub

    Private Sub btnHorizon_Click(sender As Object, e As EventArgs) Handles btnHorizon.Click

        Horizons = New cHorizons_Loader
        With Horizons
            .Observer_Long = Pos_Long
            .Observer_Lat = Pos_Lat
            .Observer_Heigth = Pos_Heigth
            .FromDate = Moment_Start
            .ToDate = Moment_Stop
            .Stepping = Moment_Steps
        End With

        Horizons.Go(cHorizons_Loader.eBodies.Moon)
        tbHorizonOutput.Text = Join(Horizons.GetTable, System.Environment.NewLine)

        Horizons.GetQuantity_vs_Time("Azi_(a-app)", Horizons_Moments, Horizons_Azi)
        Horizons.GetQuantity_vs_Time("Elev_(a-app)", Horizons_Moments, Horizons_Elev)

    End Sub

End Class