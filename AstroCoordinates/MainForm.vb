Option Explicit On
Option Strict On

Public Class MainForm

    Private ReadOnly MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = GetBuildDateTime.GetMainformTitle
        Me.CenterToScreen()
    End Sub

    Private Sub btnGetObject_Click(sender As Object, e As EventArgs) Handles btnGetObject.Click
        Dim SelectionForm As New frmGetObject
        SelectionForm.SelectedObject = New cObjectInfo("Manual", "Manual", tbRAParsedDecimal.Text.ValRegIndep, tbDecParsedDecimal.Text.ValRegIndep)
        If SelectionForm.ShowDialog() = DialogResult.OK Then
            tbSelected.Text = SelectionForm.SelectedObject.FullName(True)
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

    Private Sub CoordinateClick(sender As Object, e As EventArgs) Handles tbRAParsed.Click, tbDecParsed.Click, tbRAParsedDecimal.Click, tbDecParsedDecimal.Click, tbRAParsedShort.Click, tbDecParsedShort.Click, tbSelected.Click
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
        Response = Download.GetResponse(DB.PWI4.Command.Goto_RaDec(cbJ2000.Checked, tbRAParsedDecimal.Text.ValRegIndep, tbDecParsedDecimal.Text.ValRegIndep))
        Response = Download.GetResponse(DB.PWI4.Command.Tracking(True))
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
        Process.Start("explorer.exe", DB.MyPath)
    End Sub

    Private Sub tsmiEnter_Dec_Click(sender As Object, e As EventArgs) Handles tsmiEnter_Dec.Click, btnDec.Click
        Dim TextInput As String = InputBox("Dec to parse [°]: ", "Dec to parse")
        If TextInput.Trim.Length > 0 Then UpdateDec(TextInput.ParseDegree)
    End Sub

    Private Sub tsmiEnter_RA_Click(sender As Object, e As EventArgs) Handles tsmiEnter_RA.Click, btnRA.Click
        Dim TextInput As String = InputBox("RA to parse [hms format]: ", "RA to parse")
        If TextInput.Trim.Length > 0 Then UpdateRA(TextInput.ParseRA)
    End Sub

    Private Sub tsmiEnter_RADeg_Click(sender As Object, e As EventArgs) Handles tsmiEnter_RADeg.Click
        Dim TextInput As String = InputBox("RA to parse [°]: ", "RA to parse")
        If TextInput.Trim.Length > 0 Then UpdateRA(TextInput.ParseDegree * (24 / 360))
    End Sub

    Private Sub tsmiGoTo_Object_Click(sender As Object, e As EventArgs) Handles tsmiGoTo_Object.Click
        GotoObject()
    End Sub

    Private Sub tsmiGoTo_ZenithAndStop_Click(sender As Object, e As EventArgs) Handles tsmiGoTo_ZenithAndStop.Click
        Dim Response As String = String.Empty
        Response = Download.GetResponse(DB.PWI4.Command.Tracking(False))
        Response = Download.GetResponse(DB.PWI4.Command.Goto_AltAz(90, 0))
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
        CalcLog.Add("  Sun Altitude: " & (SunPos.AzAlt.Alt.ToDegMinSec))
        CalcLog.Add("  Sun Azimuth : " & (SunPos.AzAlt.AZ.ToDegMinSec))
        CalcLog.Add("  Sun RA      : " & (SunPos.RaDec.RA.ToDegMinSec))
        CalcLog.Add("  Sun Dec     : " & (SunPos.RaDec.DEC.ToDegMinSec))
        CalcLog.Add("Sun opposition:")
        Dim UseAltAz As Boolean = False
        If UseAltAz Then
            Dim Sun_Oppo_Alt As Double = -SunPos.AzAlt.Alt
            Dim Sun_Oppo_Az As Double = SunPos.AzAlt.AZ - 180 : If SunPos.AzAlt.AZ < 0 Then SunPos.AzAlt.AZ += 360
            CalcLog.Add("  Sun Altitude: " & (Sun_Oppo_Alt.ToDegMinSec))
            CalcLog.Add("  Sun Azimuth : " & (Sun_Oppo_Az.ToDegMinSec))
            Response = Download.GetResponse(DB.PWI4.Command.Goto_AltAz(Sun_Oppo_Alt, Sun_Oppo_Az))
        Else
            Dim Sun_Oppo_Ra As Double = SunPos.RaDec.RA - 180 : If Sun_Oppo_Ra < 0 Then Sun_Oppo_Ra += 360
            Dim Sun_Oppo_Dec As Double = -SunPos.RaDec.DEC_deg
            CalcLog.Add("  Sun RA      : " & (Sun_Oppo_Ra.ToDegMinSec))
            CalcLog.Add("  Sun Dec     : " & (Sun_Oppo_Dec.ToDegMinSec))
            Response = Download.GetResponse(DB.PWI4.Command.Goto_RaDec(cbJ2000.Checked, Sun_Oppo_Ra, Sun_Oppo_Dec))
        End If
        Response = Download.GetResponse(DB.PWI4.Command.Tracking(True))
        Dim X As New frmLogDisplay : X.Show(CalcLog)
    End Sub

    Private Sub tsmiFile_End_Click(sender As Object, e As EventArgs) Handles tsmiFile_End.Click
        End
    End Sub

    Private Sub tsmiFile_LoadVizier_Click(sender As Object, e As EventArgs) Handles tsmiFile_LoadVizier.Click
        Dim Vizier As New cVizier("C:\!AstroCat")
        Dim Log As New List(Of String)
        Dim RawOutput As Boolean = False
        Log.Add("GetVizierCats")
        Vizier.GetVizierCats()
        Log.Add("'════════════════════════════════════════════════════════════════════════")
        Log.Add("DownloadCatalogs")
        Log.AddRange(Vizier.DownloadCatalogs())
        Log.Add("'════════════════════════════════════════════════════════════════════════")
        Log.Add("UncompressCatalogs")
        Log.AddRange(Vizier.UncompressCatalogs())
        Log.Add("'════════════════════════════════════════════════════════════════════════")
        Log.Add("InspectCatalog")
        Log.AddRange(Vizier.InspectCatalog(RawOutput))
        Log.Add("'════════════════════════════════════════════════════════════════════════")
        Log.Add("GetCatData")
        Dim CatData As Dictionary(Of String, List(Of cObjectInfo)) = Vizier.GetCatData()
        Log.Add("'════════════════════════════════════════════════════════════════════════")
        'Log.Add("GetCommonLabels")
        'Log.AddRange(Vizier.GetCommonLabels())
        Vizier.Debug_AllFormats.Sort()
        Vizier.Debug_AllUnits.Sort()
        Vizier.Debug_AllLabels.Sort()
        Dim X As New frmLogDisplay
        X.Show(Log)
    End Sub

    Private Sub tsmiFile_AstroBin_Click(sender As Object, e As EventArgs) Handles tsmiFile_AstroBin.Click
        Dim ABS As New frmAstroBinSearch
        ABS.Show()
        Dim SearchRadius As Double = InputBox("Search radius [°]", "Search radius", "1").ValRegIndep
        Dim SearchRadiusRad As Double = SearchRadius * (24 / 360)
        With ABS
            .RaMin = tbRAParsedDecimal.Text.ValRegIndep - (SearchRadiusRad / 2)
            .RaMax = tbRAParsedDecimal.Text.ValRegIndep + (SearchRadiusRad / 2)
            .DecMin = tbDecParsedDecimal.Text.ValRegIndep - (SearchRadius / 2)
            .DecMax = tbDecParsedDecimal.Text.ValRegIndep + (SearchRadius / 2)
            .FieldRadiusMin = 0
            .FieldRadiusMax = SearchRadius
            .TopPickOnly = True
        End With

    End Sub

    Private Sub tsmiFile_InTheSky_Click(sender As Object, e As EventArgs) Handles tsmiFile_InTheSky.Click
        'Show the sky chart
        Dim Location As Astronomy.Net.sLatLong = Ato.AstroCalc.KnownLocations.DSC
        Dim URL As String = "https://in-the-sky.org/staratlas.php?"
        Dim Parameter As New List(Of String)
        Parameter.Add("no_cookie=1")
        Parameter.Add("latitude=" & Location.Latitude.ValRegIndep)
        Parameter.Add("longitude=" & Location.Longitude.ValRegIndep)
        Parameter.Add("timezone=0.00")
        Parameter.Add("year=" & Now.Year.ValRegIndep)
        Parameter.Add("month=" & Now.Month.ValRegIndep)
        Parameter.Add("day=" & Now.Day.ValRegIndep)
        Parameter.Add("hour=" & Now.Hour.ValRegIndep)
        Parameter.Add("min=" & Now.Minute.ValRegIndep)
        Parameter.Add("PLlimitmag=2")
        Parameter.Add("zoom=16")
        Parameter.Add("ra=" & tbRAParsedDecimal.Text)
        Parameter.Add("dec=" & tbDecParsedDecimal.Text)
        cAstrobinAPIV2.OpenURLInBrowser(URL & Join(Parameter.ToArray, "&"))
    End Sub

    Private Sub tsmiEnter_ParseClipboard_Click(sender As Object, e As EventArgs) Handles tsmiEnter_ParseClipboard.Click
        Dim ClipContent As String = Clipboard.GetText(TextDataFormat.UnicodeText)
        ClipContent = InputBox("Content:", "Content", ClipContent)
        Dim ClipRa As Double = Double.NaN
        Dim ClipDec As Double = Double.NaN
        If ClipContent.ParseCoord(ClipRa, ClipDec) Then
            UpdateRA(ClipRa)
            UpdateDec(ClipDec)
        End If
    End Sub

    Private Sub tsmiFile_DropBoxCAT_Click(sender As Object, e As EventArgs) Handles tsmiFile_DropBoxCAT.Click
        Dim DSCShare As String = "https://www.dropbox.com/scl/fo/gdqyke3ifvlfc5o8ucr72/ALSsHL12tDmzghQsNvwTuho?rlkey=50f3tqrl3qtsrprmi3ls6j73e&st=yssp9gwr&dl=1"
        Dim DBL As New cDropBox(DSCShare)
        DBL.LoadContent()
        Dim CatName As String = "CustomCatalog.txt"
        If DBL.Content.Contains(CatName) Then
            Dim CustomCat As String = System.IO.Path.Combine(MyPath, CatName)
            If System.IO.File.Exists(CustomCat) Then System.IO.File.Delete(CustomCat)
            System.IO.File.WriteAllBytes(CustomCat, DBL.GetContent(CatName))
            Dim Lines As Integer = System.IO.File.ReadAllLines(CustomCat).Count
            MsgBox("CustomCatalog.txt with <" & Lines.ValRegIndep & "> lines loaded from dropbox", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Loaded")
        End If
    End Sub

    Private Sub RawGotoCommandToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RawGotoCommandToolStripMenuItem.Click
        Dim X As New frmLMountRawGoTo
        X.Show()
    End Sub

End Class
