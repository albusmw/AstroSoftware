Option Explicit On
Option Strict On

Public Class MainForm

    Public Shared ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    Private PWI4 As New cPWI4("http://localhost:8220")

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = (New cGetBuildDateTime).GetMainformTitle
        Me.CenterToScreen()
    End Sub

    Private Sub btnGetObject_Click(sender As Object, e As EventArgs) Handles btnGetObject.Click
        Dim SelectionForm As New frmGetObject
        If SelectionForm.ShowDialog() = DialogResult.OK Then
            tbSelected.Text = SelectionForm.SelectedObject.VerboseName
            UpdateRA(SelectionForm.SelectedObject.RA)
            UpdateDec(SelectionForm.SelectedObject.Dec)
        End If
    End Sub

    Private Sub UpdateRA(ByVal RA As Double)
        tbRAParsed.Text = RA.ToHMS("h ", "m ", "s")
        tbRAParsedShort.Text = RA.ToHMS
        tbRAParsedDecimal.Text = Format(RA, "00.000000").Replace(",", ".")
    End Sub

    Private Sub UpdateDec(ByVal Dec As Double)
        tbDecParsed.Text = Dec.ToDegMinSec
        tbDecParsedShort.Text = Dec.ToDegMinSec(":", ":", "", 2)
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
        Response = Download.GetResponse(PWI4.Command.Goto_RaDec(cbJ2000.Checked, tbRAParsedDecimal.Text.ValRegIndep, tbDecParsedDecimal.Text.ValRegIndep))
        Response = Download.GetResponse(PWI4.Command.Tracking(True))
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

    Private Sub tsmiEnter_Dec_Click(sender As Object, e As EventArgs) Handles tsmiEnter_Dec.Click
        Dim TextInput As String = InputBox("Dec to parse [°]: ", "Dec to parse")
        UpdateDec(TextInput.ParseDegree)
    End Sub

    Private Sub tsmiEnter_RA_Click(sender As Object, e As EventArgs) Handles tsmiEnter_RA.Click
        Dim TextInput As String = InputBox("RA to parse [hms format]: ", "RA to parse")
        UpdateRA(TextInput.ParseRA)
    End Sub

    Private Sub tsmiGoTo_Object_Click(sender As Object, e As EventArgs) Handles tsmiGoTo_Object.Click
        GotoObject()
    End Sub

    Private Sub tsmiGoTo_ZenithAndStop_Click(sender As Object, e As EventArgs) Handles tsmiGoTo_ZenithAndStop.Click
        Dim Response As String = String.Empty
        Response = Download.GetResponse(PWI4.Command.Tracking(False))
        Response = Download.GetResponse(PWI4.Command.Goto_AltAz(90, 0))
    End Sub

    Private Sub tsmiGoTo_SunOpposition_Click(sender As Object, e As EventArgs) Handles tsmiGoTo_SunOpposition.Click
        'Go to the sun opposition
        Dim CalcLog As New List(Of String)
        Dim Response As String = String.Empty
        CalcLog.Add("Sun opposition calculation")
        CalcLog.Add("  UTC         : " & (Now.ToUniversalTime.ToString))
        CalcLog.Add("  Longitude   : " & (Ato.AstroCalc.KnownLocations.DSC.Longitude.ToDegMinSec))
        CalcLog.Add("  Latitude    : " & (Ato.AstroCalc.KnownLocations.DSC.Latitude.ToDegMinSec))
        Dim SunPos As AstroCalc.NET.Sun.sSunPos = AstroCalc.NET.Sun.SunPos(Now.ToUniversalTime, Ato.AstroCalc.KnownLocations.DSC.Longitude, Ato.AstroCalc.KnownLocations.DSC.Latitude)
        CalcLog.Add("  Sun Altitude: " & (SunPos.Altitude.ToDegMinSec))
        CalcLog.Add("  Sun Azimuth : " & (SunPos.Azimuth.ToDegMinSec))
        CalcLog.Add("  Sun RA      : " & (SunPos.RightAscension.ToDegMinSec))
        CalcLog.Add("  Sun Dec     : " & (SunPos.Declination.ToDegMinSec))
        CalcLog.Add("Sun opposition:")
        Dim UseAltAz As Boolean = False
        If UseAltAz Then
            Dim Sun_Oppo_Alt As Double = -SunPos.Altitude
            Dim Sun_Oppo_Az As Double = SunPos.Azimuth - 180 : If SunPos.Azimuth < 0 Then SunPos.Azimuth += 360
            CalcLog.Add("  Sun Altitude: " & (Sun_Oppo_Alt.ToDegMinSec))
            CalcLog.Add("  Sun Azimuth : " & (Sun_Oppo_Az.ToDegMinSec))
            Response = Download.GetResponse(PWI4.Command.Goto_AltAz(Sun_Oppo_Alt, Sun_Oppo_Az))
        Else
            Dim Sun_Oppo_Ra As Double = SunPos.RightAscension - 180 : If Sun_Oppo_Ra < 0 Then Sun_Oppo_Ra += 360
            Dim Sun_Oppo_Dec As Double = -SunPos.Declination
            CalcLog.Add("  Sun RA      : " & (Sun_Oppo_Ra.ToDegMinSec))
            CalcLog.Add("  Sun Dec     : " & (Sun_Oppo_Dec.ToDegMinSec))
            Response = Download.GetResponse(PWI4.Command.Goto_RaDec(cbJ2000.Checked, Sun_Oppo_Ra, Sun_Oppo_Dec))
        End If
        Response = Download.GetResponse(PWI4.Command.Tracking(True))
        Dim X As New frmLogDisplay : X.Show(CalcLog)
    End Sub

    Private Sub tsmiFile_End_Click(sender As Object, e As EventArgs) Handles tsmiFile_End.Click
        End
    End Sub

    Private Sub tsmiFile_LoadVizier_Click(sender As Object, e As EventArgs) Handles tsmiFile_LoadVizier.Click
        Dim X As New cVizier
        X.LoadCatalogs()
    End Sub



End Class
