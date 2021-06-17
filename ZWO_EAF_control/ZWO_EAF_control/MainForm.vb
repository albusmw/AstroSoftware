Option Explicit On
Option Strict On
Imports System.ComponentModel

Public Class MainForm

    Private ID As Integer = Integer.MinValue
    Private Position As Integer = Integer.MinValue
    Private MaxStep As Integer = Integer.MinValue

    Private ZWOEAF As Ato.cZWOEAF

    Private RequestedPosition As Integer = Integer.MinValue

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click

        Dim ECodes As New List(Of Ato.cZWOEAF.EAF_ERROR_CODE)

        ZWOEAF = New Ato.cZWOEAF("C:\GIT\src\3rdParty\EAF SDK\lib\Win64\EAF_focuser.dll")
        Dim Number As Integer = ZWOEAF.EAFGetNum

        ECodes.Add(ZWOEAF.EAFGetID(0, ID))
        ECodes.Add(ZWOEAF.EAFOpen(ID))
        tUpdate.Enabled = True

        ECodes.Add(ZWOEAF.EAFGetMaxStep(ID, MaxStep))

    End Sub

    Private Sub tUpdate_Tick(sender As Object, e As EventArgs) Handles tUpdate.Tick
        ZWOEAF.EAFGetPosition(ID, Position)
        If RequestedPosition <> Integer.MinValue Then
            If RequestedPosition <> Position Then
                Dim RetVal As Ato.cZWOEAF.EAF_ERROR_CODE = ZWOEAF.EAFMove(ID, RequestedPosition)
            End If
        End If
        tsslPosition.Text = "Position: " & Position.ToString.Trim
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub MainForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If ID <> Integer.MinValue Then ZWOEAF.EAFClose(ID)
    End Sub

    Private Sub btnMoveIn_Click(sender As Object, e As EventArgs) Handles btnMoveIn.Click
        RequestedPosition = Position + CInt(tbStepSize.Text)
    End Sub

    Private Sub btnMoveOut_Click(sender As Object, e As EventArgs) Handles btnMoveOut.Click
        RequestedPosition = Position - CInt(tbStepSize.Text)
    End Sub

End Class
