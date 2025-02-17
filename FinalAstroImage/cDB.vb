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
        ReDim Channels(ChannelCount - 1)
        For Idx As Integer = 0 To Channels.GetUpperBound(0)
            Channels(Idx) = New cChannel
        Next Idx
    End Sub

End Class
