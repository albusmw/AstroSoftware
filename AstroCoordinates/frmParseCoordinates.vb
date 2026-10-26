Option Explicit On
Option Strict On

Public Class frmParseCoordinates

    Public Property ParsedRA As Double = Double.NaN
    Public Property ParsedDec As Double = Double.NaN

    Private Sub tbCoordinatesText_TextChanged(sender As Object, e As EventArgs) Handles tbCoordinatesText.TextChanged

        tbCoordinatesText.Text.ParseCoord(ParsedRA, ParsedDec)

        If Double.IsNaN(ParsedRA) = False Then
            tbParsedRA.Text = ParsedRA.ToHMS : tbParsedRA.BackColor = Color.White
        Else
            tbParsedRA.BackColor = Color.Red
        End If

        If Double.IsNaN(ParsedDec) = False Then
            tbParsedDec.Text = ParsedDec.ToDegMinSec : tbParsedDec.BackColor = Color.White
        Else
            tbParsedDec.BackColor = Color.Red
        End If

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class