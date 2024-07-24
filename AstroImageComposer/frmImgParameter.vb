Option Explicit On
Option Strict On

Public Class frmImgParameter

    Public FormToModify As frmImage
    Public RTFGen As New cRTFGen

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

    Public Sub DisplayStatisticsReport()
        DisplayRTF(FormToModify.ImgStat.StatisticsReport(True, True))
    End Sub

    Public Sub DisplayRTF(ByVal Text As IEnumerable(Of String))
        'Init
        If RTFGen.RTFControlAttached = False Then
            RTFGen.AttachToControl(rtbMain)
            RTFGen.RTFInit(10)
        End If
        RTFGen.Clear()
        For Each Line As String In Text
            RTFGen.AddEntry(Line)
        Next Line
    End Sub

End Class