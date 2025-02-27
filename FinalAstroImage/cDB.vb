Option Explicit On
Option Strict On

Public Class cDB

    '''<summary>Class for data to image conversion.</summary>
    Public ImageFromData As New cImageFromData

    '''<summary>Class for TIFF loading.</summary>
    Public WithEvents TIFF_IO As New ImageFileFormatSpecific.cTIFF

    Public Property ChannelCount As Integer = 3

    '''<summary>All channels with the data in it.</summary>
    Public Channels() As cChannel

    '''<summary>FITS data viewer.</summary>
    Public FITSWork As String = String.Empty

    '''<summary>IrfanView.</summary>
    Public IrfanView As String = String.Empty

    '''<summary>Intel IPP.</summary>
    Public IPP As cIntelIPP

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    '''<summary>Structure for one single channel.</summary>
    Public Class cChannel
        '''<summary>File the data where loaded from.</summary>
        Public FileName As String = String.Empty
        '''<summary>Raw file channel data.</summary>
        Public Data(,) As Double = {}
    End Class

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    '''<summary>Constructor.</summary>
    Public Sub New()

        'Create channels
        ReDim Channels(ChannelCount - 1)
        For Idx As Integer = 0 To Channels.GetUpperBound(0)
            Channels(Idx) = New cChannel
        Next Idx

        Dim EXELocations As New List(Of String)

        'Set FITS viewer
        Dim FITSWork4EXE As String = "FITSWork4.exe"
        EXELocations.Clear()
        EXELocations = Everything.GetExactMatch(FITSWork4EXE, Everything.GetSearchResult(FITSWork4EXE))
        If EXELocations.Count > 0 Then FITSWork = EXELocations(0)

        'Set IrfanView
        Dim IrfanViewEXE As String = "i_view64.exe"
        EXELocations.Clear()
        EXELocations = Everything.GetExactMatch(IrfanViewEXE, Everything.GetSearchResult(IrfanViewEXE))
        If EXELocations.Count > 0 Then IrfanView = EXELocations(0)

        'Set IPP
        IPP = New cIntelIPP

    End Sub

End Class
