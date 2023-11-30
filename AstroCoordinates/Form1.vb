Option Explicit On
Option Strict On

Public Class MainForm

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub

    Private Sub btnParseRA_Click(sender As Object, e As EventArgs) Handles btnParseRA.Click
        UpdateRA(InputBox("RA to parse: ", "RA to parse"))
    End Sub

    Private Sub btnParseDec_Click(sender As Object, e As EventArgs) Handles btnParseDec.Click
        UpdateDec(InputBox("Dec to parse: ", "Dec to parse"))
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

    Private Sub tbRAParsed_Click(sender As Object, e As EventArgs) Handles tbRAParsed.Click, tbDecParsed.Click, tbRAParsedDecimal.Click, tbDecParsedDecimal.Click, tbRAParsedShort.Click, tbDecParsedShort.Click, tbSelected.Click
        tbRAParsed.BackColor = Color.White
        tbRAParsedDecimal.BackColor = Color.White
        tbRAParsedShort.BackColor = Color.White
        tbDecParsed.BackColor = Color.White
        tbDecParsedDecimal.BackColor = Color.White
        tbDecParsedShort.BackColor = Color.White
        tbSelected.BackColor = Color.White
        With CType(sender, TextBox)
            If IsNothing(.Text) Then Exit Sub
            If .Text.Length = 0 Then Exit Sub
            Clipboard.Clear()
            Clipboard.SetText(.Text)
            .BackColor = Color.LimeGreen
        End With

    End Sub

    Private Sub btnGoTo_Click(sender As Object, e As EventArgs) Handles btnGoTo.Click
        Dim PWI_adr As String = "http://localhost:8220"
        Dim Response As String = String.Empty
        Dim Epoche As String = CStr(IIf(cbJ2000.Checked = True, "j2000", "apparent"))
        Response = Download.GetResponse(PWI_adr & "/mount/goto_ra_dec_" & Epoche & "?ra_hours=" & tbRAParsedDecimal.Text & "&dec_degs=" & tbDecParsedDecimal.Text)
        Dim RAOffset As Double = tbRAOffset.Text.ValRegIndep
        Dim DecOffset As Double = tbDecOffset.Text.ValRegIndep
        If (RAOffset <> 0.0) Or (DecOffset <> 0) Then
            Response = Download.GetResponse(PWI_adr & "/mount/offset?ra_add_arcsec=" & (RAOffset * 60).ValRegIndep & "&dec_add_arcsec=" & (DecOffset * 60).ValRegIndep)
        End If
    End Sub

End Class
