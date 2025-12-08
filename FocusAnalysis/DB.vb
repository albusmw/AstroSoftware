Option Explicit On
Option Strict On

Public Class DB

    Public Shared ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

    Public Shared Property SERFileNoExtension As String = String.Empty

    Public Shared OriginalFrame As AstroNET.Statistics
    Public Shared OriginalFrame_Stat As AstroNET.Statistics.sStatistics

    Public Shared NoBiasFrame As AstroNET.Statistics
    Public Shared NoBiasFrame_Stat As AstroNET.Statistics.sStatistics

    '''<summary>Per-pixel maximum of all frames.</summary>
    Public Shared GlobalMax(,) As UInt16
    '''<summary>Per-pixel minimum of all frames.</summary>
    Public Shared GlobalMin(,) As UInt16

    Public Shared SERPlayer As String = "C:\bin\SER Player\ser-player.exe"

End Class
