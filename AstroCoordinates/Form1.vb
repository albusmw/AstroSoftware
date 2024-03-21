Option Explicit On
Option Strict On

Public Class MainForm

    Public Shared ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

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

    Private Sub UpdateRA(ByVal RA As Double)
        tbRAParsed.Text = Ato.AstroCalc.FormatHMS(RA, "h ", "m ", "s")
        tbRAParsedShort.Text = Ato.AstroCalc.FormatHMS(RA)
        tbRAParsedDecimal.Text = Format(RA, "00.000000").Replace(",", ".")
    End Sub

    Private Sub UpdateDec(ByVal Dec As Double)
        tbDecParsed.Text = Ato.AstroCalc.Format360Degree(Dec)
        tbDecParsedShort.Text = Ato.AstroCalc.Format360Degree(Dec, ":", ":", "", 2)
        tbDecParsedDecimal.Text = Format(Dec, "00.000000").Replace(",", ".")
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

    Private Sub GotoObject()
        Dim Response As String = String.Empty
        Response = Download.GetResponse(cPWI4.Goto_RaDec(cbJ2000.Checked, tbRAParsedDecimal.Text.ValRegIndep, tbDecParsedDecimal.Text.ValRegIndep))
        Response = Download.GetResponse(cPWI4.Tracking(True))
    End Sub

    '═════════════════════════════════════════════════════════════════════════════
    ' Buttons
    '═════════════════════════════════════════════════════════════════════════════

    Private Sub btnGoTo_Click(sender As Object, e As EventArgs) Handles btnGoTo.Click
        GotoObject()
    End Sub

    '═════════════════════════════════════════════════════════════════════════════
    ' Menu
    '═════════════════════════════════════════════════════════════════════════════

    Private Sub tsmiFile_OpenEXE_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenEXE.Click
        Process.Start("explorer.exe", MyPath)
    End Sub

    Private Sub tsmiEnter_RA_Click(sender As Object, e As EventArgs) Handles tsmiEnter_RA.Click
        UpdateDec(AstroParser.ParseDeclination(InputBox("Dec to parse: ", "Dec to parse")))
    End Sub

    Private Sub tsmiEnter_Dec_Click(sender As Object, e As EventArgs) Handles tsmiEnter_Dec.Click
        UpdateRA(AstroParser.ParseRA(InputBox("RA to parse: ", "RA to parse")))
    End Sub

    Private Sub ObjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObjectToolStripMenuItem.Click
        GotoObject()
    End Sub

    Private Sub ZenithAndStopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZenithAndStopToolStripMenuItem.Click
        Dim Response As String = String.Empty
        Response = Download.GetResponse(cPWI4.Tracking(False))
        Response = Download.GetResponse(cPWI4.Goto_AltAz(90, 0))
    End Sub

    Private Sub tsmiFile_End_Click(sender As Object, e As EventArgs) Handles tsmiFile_End.Click
        End
    End Sub

    Private Sub tsmiFile_LoadVizier_Click(sender As Object, e As EventArgs) Handles tsmiFile_LoadVizier.Click
        Dim X As New cVizier
        X.LoadCatalogs()
    End Sub

End Class
