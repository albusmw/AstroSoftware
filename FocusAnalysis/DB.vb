Option Explicit On
Option Strict On

Imports System.ComponentModel

Public Class cDB

    Public Config As New cConfig

    Public Property SERFileNoExtension As String = String.Empty

    '''<summary>Path to IPP DLL.</summary>
    Public Property IPPPath As String = String.Empty

    '''<summary>Handle to Intel IPP functions.</summary>
    Public IntelIPP As cIntelIPP

    Public OriginalFrame As AstroNET.Statistics
    Public OriginalFrame_Stat As AstroNET.Statistics.sStatistics

    Public NoBiasFrame As AstroNET.Statistics
    Public NoBiasFrame_Stat As AstroNET.Statistics.sStatistics

    '''<summary>Per-pixel maximum of all frames.</summary>
    Public GlobalMax(,) As UInt16
    '''<summary>Per-pixel minimum of all frames.</summary>
    Public GlobalMin(,) As UInt16

    Public Trace_Indicator() As Double
    Public Trace_OVLD() As Double
    Public Trace_TotalEnergy() As Double

    '''<summary>Converter used to display single SER frames.</summary>
    Public SERFrameImage As New cImageFromData

    Public Plotter As cZEDGraph
    Public HistoPlot As cZEDGraph

    Public Log As cLogTextBox

End Class

Public Class cConfig

    <Category("Paths and Files")>
    <DisplayName("EXE path")>
    Public ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

    '''<summary>Path of the EXE used to play SER files.</summary>
    <Category("Paths and Files")>
    <DisplayName("SER player to use")>
    Public Property SERPlayer As String = "C:\bin\SER Player\ser-player.exe"

    <Category("Paths and Files")>
    <DisplayName("EXCEL file to generate")>
    Public Property ExcelFileName As String = String.Empty

    <Category("Processing")>
    <DisplayName("ROI inner x %")>
    Public Property ROIPct As Double = 100.0

    <Category("Processing")>
    <DisplayName("Indicator x % of energy within ... pixel")>
    Public Property EnergyPCT As Double = 10.0

    <Category("Processing")>
    <DisplayName("Sleep [ms] before next frame")>
    Public Property WaitOnNextFrame As Integer = 0

End Class
