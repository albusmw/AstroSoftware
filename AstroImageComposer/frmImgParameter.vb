Option Explicit On
Option Strict On

Public Class frmImgParameter

    Public FormToModify As frmImage
    Public RTFGen As New cRTFGen

    Public Sub DisplayRTF()
        'Init
        If RTFGen.RTFControlAttached = False Then
            RTFGen.AttachToControl(rtbMain)
            RTFGen.RTFInit(10)
        End If
        'Compose the report
        Dim StatReport As List(Of String) = FormToModify.ImgStat.StatisticsReport(True, True)
        RTFGen.Clear()
        For Each Line As String In StatReport
            RTFGen.AddEntry(Line)
        Next Line
    End Sub

End Class