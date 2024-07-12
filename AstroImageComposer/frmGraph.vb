Option Explicit On
Option Strict On

'''<summary>Display graphical data.</summary>
Public Class frmGraph

    Public MyZEDGraph As cZEDGraph

    Private PlotStyle As cZEDGraph.eCurveMode = cZEDGraph.eCurveMode.LinesAndPoints

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
    '''<summary>GUID of the linked image form.</summary>
    Public ReadOnly Property LinkedGUID As String
        Get
            Return MyLinkedGUID
        End Get
    End Property
    Private MyLinkedGUID As String = String.Empty

    Public Sub SetLinkedGUID(ByVal GUIDToUse As String)
        MyLinkedGUID = GUIDToUse
    End Sub
    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    '''<summary>Open a simple form with a ZEDGraph on it and plots the statistical data.</summary>
    '''<param name="FileName">Filename that is plotted (indicated in the header).</param>
    '''<param name="Stats">Statistics data to plot.</param>
    Public Sub PlotStatistics(ByVal FileName As String, ByRef Stats As AstroNET.Statistics.sStatistics)
        MyZEDGraph = New cZEDGraph(zgcMain)
        With MyZEDGraph
            'AddHandler .PointValueHandler, AddressOf PointValueHandler
            Dim XAxisMargin As Integer = 128                                    'axis margin to see the most outer values
            Select Case Stats.DataMode
                Case AstroNET.Statistics.eDataMode.Fixed
                    'Plot histogram
                    .Clear()
                    'If IsNothing(Stats.BayerHistograms_Int) = False And AIS.Config.CalcStat_Bayer Then
                    .PlotXvsY("[0,0]", Stats.BayerHistograms_Int(0, 0), 1, New cZEDGraph.sGraphStyle(Color.Red, PlotStyle, 1))
                    .PlotXvsY("[0,1]", Stats.BayerHistograms_Int(0, 1), 1, New cZEDGraph.sGraphStyle(Color.LightGreen, PlotStyle, 1))
                    .PlotXvsY("[1,0]", Stats.BayerHistograms_Int(1, 0), 1, New cZEDGraph.sGraphStyle(Color.Green, PlotStyle, 1))
                    .PlotXvsY("[1,1]", Stats.BayerHistograms_Int(1, 1), 1, New cZEDGraph.sGraphStyle(Color.Blue, PlotStyle, 1))
                    'End If
                    'If IsNothing(Stats.MonochromHistogram_Int) = False And AIS.Config.CalcStat_Mono Then
                    .PlotXvsY("Mono histo", Stats.MonochromHistogram_Int, 1, New cZEDGraph.sGraphStyle(Color.Black, PlotStyle, 1))
                    'End If
                    .ManuallyScaleXAxisLin(Stats.MonoStatistics_Int.Min.Key - XAxisMargin, Stats.MonoStatistics_Int.Max.Key + XAxisMargin)
                Case AstroNET.Statistics.eDataMode.Float
                    ''Plot histogram
                    '.Plotter.Clear()
                    'If IsNothing(Stats.BayerHistograms_Float32) = False And AIS.Config.CalcStat_Bayer Then
                    '    .Plotter.PlotXvsY(AIS.Config.BayerPatternName(0) & "[0,0]", Stats.BayerHistograms_Float32(0, 0), 1, New cZEDGraph.sGraphStyle(Color.Red, AIS.Config.PlotStyle, 1))
                    '    .Plotter.PlotXvsY(AIS.Config.BayerPatternName(1) & "[0,1]", Stats.BayerHistograms_Float32(0, 1), 1, New cZEDGraph.sGraphStyle(Color.LightGreen, AIS.Config.PlotStyle, 1))
                    '    .Plotter.PlotXvsY(AIS.Config.BayerPatternName(2) & "[1,0]", Stats.BayerHistograms_Float32(1, 0), 1, New cZEDGraph.sGraphStyle(Color.Green, AIS.Config.PlotStyle, 1))
                    '    .Plotter.PlotXvsY(AIS.Config.BayerPatternName(3) & "[1,1]", Stats.BayerHistograms_Float32(1, 1), 1, New cZEDGraph.sGraphStyle(Color.Blue, AIS.Config.PlotStyle, 1))
                    'End If
                    'If IsNothing(Stats.MonochromHistogram_Float32) = False And AIS.Config.CalcStat_Mono Then
                    '    .Plotter.PlotXvsY("Mono histo", Stats.MonochromHistogram_Float32, 1, New cZEDGraph.sGraphStyle(Color.Black, AIS.Config.PlotStyle, 1))
                    'End If
                    '.Plotter.ManuallyScaleXAxisLin(Stats.MonoStatistics_Int.Min.Key - XAxisMargin, Stats.MonoStatistics_Int.Max.Key + XAxisMargin)
            End Select
            .AutoScaleYAxisLog()
            .GridOnOff(True, True)
            .ForceUpdate()
            .MaximizePlotArea()
            'Set style of the window
            .SetCaptions(String.Empty, "Pixel value", "# of pixel with this value")
        End With
    End Sub

End Class