Option Explicit On
Option Strict On

Public Class MainForm

    Private Sub btnParseRA_Click(sender As Object, e As EventArgs) Handles btnParseRA.Click
        UpdateRA(InputBox("RA to parse: ", "RA to parse"))
    End Sub

    Private Sub btnParseDec_Click(sender As Object, e As EventArgs) Handles btnParseDec.Click
        UpdateDec(InputBox("Dec to parse: ", "Dec to parse"))
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub

    Private Sub btnGetObject_Click(sender As Object, e As EventArgs) Handles btnGetObject.Click
        Dim NewForm As New frmGetObject
        If NewForm.ShowDialog() = DialogResult.OK Then
            tbSelected.Text = NewForm.SelectedObject
            UpdateRA(NewForm.SelectedRA)
            UpdateDec(NewForm.SelectedDec)
        End If
    End Sub

    Private Sub UpdateRA(ByVal Text As String)
        If String.IsNullOrEmpty(Text) Then Exit Sub
        Dim Parsed As Double = AstroParser.ParseRA(Text)
        tbRAParsed.Text = Ato.AstroCalc.FormatHMS(Parsed, "h ", "m ", "s")
        tbRAParsedShort.Text = Ato.AstroCalc.FormatHMS(Parsed)
        tbRAParsedDecimal.Text = Format(Parsed, "00.000000").Replace(",", ".")
    End Sub

    Private Sub UpdateDec(ByVal Text As String)
        If String.IsNullOrEmpty(Text) Then Exit Sub
        Dim Parsed As Double = AstroParser.ParseDeclination(Text)
        tbDecParsed.Text = Ato.AstroCalc.Format360Degree(Parsed)
        tbDecParsedShort.Text = Ato.AstroCalc.Format360Degree(Parsed, ":", ":", "", 2)
        tbDecParsedDecimal.Text = Format(Parsed, "00.000000").Replace(",", ".")
    End Sub

End Class
