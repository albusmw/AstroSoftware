Option Explicit On
Option Strict On

Public Class MainForm

    Dim Tester As cPegasusAstro

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click

        If IsNothing(Tester) Then Tester = New cPegasusAstro
        Tester.Update()
        pgMain.SelectedObject = Tester

    End Sub

    Private Sub btnWriteTest_Click(sender As Object, e As EventArgs) Handles btnWriteTest.Click

        If IsNothing(Tester) Then Tester = New cPegasusAstro
        Tester.Update()
        Tester.USB1 = Not Tester.USB1
        pgMain.SelectedObject = Tester

    End Sub

End Class
