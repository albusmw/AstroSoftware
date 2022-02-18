Option Explicit On
Option Strict On
Imports System.Windows.Forms

Public Class Form1

    Private DB As New cDB

    Dim MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Dim MyAssemblyVersion As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
    Dim MyFileVersion As String = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion
    Dim MyproductVersion As String = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion

    Dim NL As String = System.Environment.NewLine

    Dim SBIGEthernet As New cSBIGEthernet

    Dim Chooser As New ASCOM.Utilities.Chooser
    Dim Focuser As ASCOM.DriverAccess.Focuser
    Dim SelectedFocuser As String = String.Empty


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "SBIG Ethernet Version " & MyAssemblyVersion & " from <" & My.Resources.BuildDate.Trim(New Char() {Chr(10), Chr(13), Chr(32)}) & ">"
        pgMain.SelectedObject = DB
        SBIGEthernet.IP = DB.IP
    End Sub

    Private Sub tUpdate_Tick(sender As Object, e As EventArgs) Handles tUpdate.Tick

        tsslActive.BackColor = System.Drawing.Color.Red : DE()
        tUpdate.Tag = "Updating"

        'Display textual status
        Dim Status As New List(Of String)
        Status.Add("-------------------------------------------------------")
        Status.Add("Data & Time     :" & Format(SBIGEthernet.LastUpdate, "yyyy-MM-dd HH:mm:ss"))
        Status.Add("-------------------------------------------------------")
        Status.Add("Fan    State    : " & SBIGEthernet.FanState.ToString.Trim)
        Status.Add("       Power    : " & SBIGEthernet.FanPower.ToString.Trim & " %")
        Status.Add("Cooler State    : " & CStr(IIf(SBIGEthernet.CoolerState = False, "Off", "On")))
        Status.Add("       Power    : " & SBIGEthernet.CoolerPower.ToString.Trim & " %")
        Status.Add("CCD Temperature : " & SBIGEthernet.CCDTemperature.ToString.Trim & " °C")
        Status.Add("    Setpoint    : " & SBIGEthernet.CCDTemperatureSetpoint.ToString.Trim & " °C")
        Status.Add("    Ambient     : " & SBIGEthernet.AmbientTemperature.ToString.Trim & " °C")
        Status.Add("Sensor Size     : " & SBIGEthernet.CameraSizeX.ToString.Trim & " x " & SBIGEthernet.CameraSizeX.ToString.Trim & " pixel")
        Status.Add("       Pixel    : " & SBIGEthernet.PixelSizeX.ToString.Trim & " x " & SBIGEthernet.PixelSizeX.ToString.Trim & " µm")
        Status.Add("       Binning  : " & SBIGEthernet.BinX.ToString.Trim & "x" & SBIGEthernet.BinY.ToString.Trim)
        Status.Add("       (Max     : " & SBIGEthernet.MaxBinX.ToString.Trim & "x" & SBIGEthernet.MaxBinY.ToString.Trim & ")")
        Status.Add("Frame Start     : " & SBIGEthernet.StartX.ToString.Trim & "." & SBIGEthernet.StartY.ToString.Trim & " (unbinned)")
        Status.Add("      Size      : " & SBIGEthernet.NumX.ToString.Trim & " x " & SBIGEthernet.NumY.ToString.Trim & " pixel (unbinned)")
        Status.Add("Electrons / ADU : " & SBIGEthernet.ElectronsPerADU.ToString.Trim)
        Status.Add("  Max ADU       : " & SBIGEthernet.MaxADU.ToString.Trim)
        Status.Add("  Full well     : " & SBIGEthernet.FullWellCapacity.ToString.Trim)
        Status.Add("Overscan        : " & CStr(IIf(SBIGEthernet.Overscan = False, "Off", "On")))
        Status.Add("-------------------------------------------------------")
        Status.Add("Imager state    : " & SBIGEthernet.ImagerState.ToString.Trim)
        Status.Add("Image available : " & CStr(IIf(SBIGEthernet.ImageReady = False, "no", "YES")))
        Status.Add("-------------------------------------------------------")
        Status.Add("Filter state    : " & SBIGEthernet.FilterState.ToString.Trim)
        Status.Add("Filter selected : " & SBIGEthernet.CurrentFilterName & " (#" & SBIGEthernet.CurrentFilter.ToString.ToString & ")")
        Status.Add("-------------------------------------------------------")
        Status.Add("Last expose     : " & Format(SBIGEthernet.LastExposeStart, "yyyy-MM-dd HH:mm:ss"))

        'Set GUI components ...
        tsbDownload.Enabled = SBIGEthernet.ImageReady

        'Set GUI components - fan
        cbFanState.Tag = SBIGEthernet.FanState
        cbFanState.SelectedIndex = SBIGEthernet.FanState

        'Set GUI components - cooler
        btnSwitchCooler.Tag = SBIGEthernet.CoolerState
        btnSwitchCooler.Text = CStr(IIf(SBIGEthernet.CoolerState = True, "OFF", "ON"))

        'Set GUI components - cooler
        If tbTemperatureSetPoint.Focused = False Then
            tbTemperatureSetPoint.Text = Format(SBIGEthernet.CCDTemperatureSetpoint, "0.0").Trim.Replace(",", ".")
        End If

        'Set expose state
        If SBIGEthernet.ImagerState = cSBIGEthernet.eImagerState.Exposing Then
            Try
                Dim Exposed As Double = CInt((Now - SBIGEthernet.LastExposeStart).TotalSeconds)
                tssbExposing.Maximum = CInt(SBIGEthernet.LastExposeDuration)
                tssbExposing.Value = CInt(Exposed)
                tsslExposing.Text = "Exposing " & Exposed & "/" & tssbExposing.Maximum & " s"
            Catch ex As Exception
                tssbExposing.Value = 0
                tsslExposing.Text = SBIGEthernet.ImagerState.ToString.Trim
            End Try
        Else
            tssbExposing.Value = 0
            tsslExposing.Text = SBIGEthernet.ImagerState.ToString.Trim
        End If

        tbMain.Text = Join(Status.ToArray, System.Environment.NewLine)

        tUpdate.Tag = String.Empty

        tUpdate.Interval = DB.UpdateInterval

        tsslActive.BackColor = System.Drawing.Color.DarkGray : DE()

    End Sub

    Private Sub LoadImage(ByVal FileName As String)
        If System.IO.File.Exists(FileName) Then
            Try
                System.IO.File.Delete(FileName)
            Catch ex As Exception

            End Try
        End If
        SBIGEthernet.DownloadFITS(FileName)
    End Sub

    Private Sub btnSwitchCooler_Click(sender As Object, e As EventArgs) Handles btnSwitchCooler.Click
        If IsUpdating() Then Exit Sub
        PrepareAction()
        SBIGEthernet.SetCoolerState(Not SBIGEthernet.CoolerState)
        FinishAction()
    End Sub

    Private Sub cbFanState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFanState.SelectedIndexChanged
        If IsUpdating() Then Exit Sub
        PrepareAction()
        SBIGEthernet.SetFanState(CType(cbFanState.SelectedIndex, cSBIGEthernet.eFanState))
        FinishAction()
    End Sub

    Private Sub tbTemperatureSetPoint_KeyUp(sender As Object, e As KeyEventArgs) Handles tbTemperatureSetPoint.KeyUp
        If IsUpdating() Then Exit Sub
        If e.KeyCode = Keys.Return Then
            PrepareAction()
            SBIGEthernet.SetTemperatureSetPoint(Val(tbTemperatureSetPoint.Text.Replace(",", ".")))
            FinishAction()
        End If
    End Sub

    '================================================================================

    Private Function IsUpdating() As Boolean
        If CStr(tUpdate.Tag) = "Updating" Then Return True Else Return False
    End Function

    Private Sub PrepareAction()
        'Wait until there is no update running
        tUpdate.Enabled = False
        If CStr(tUpdate.Tag) <> String.Empty Then
            Do
                System.Threading.Thread.Sleep(10)
            Loop Until CStr(tUpdate.Tag) = String.Empty
        End If
        SetCameraParameters()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub FinishAction()
        tUpdate.Enabled = CBool(tsbUpdate.Tag)
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub CalulateFITSStatisticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalulateFITSStatisticsToolStripMenuItem.Click

        Dim RootDirectory As String = "\\Observatory\DATA_IO\Darks_-10Degree_60s"
        Dim FITSFiles As New List(Of String)
        For Each File As String In System.IO.Directory.GetFiles(RootDirectory, "Dark_*.FIT")
            FITSFiles.Add(File)
        Next File

        cFITSStatistics.GetStatistics(FITSFiles, RootDirectory)
        MsgBox("DONE!")

    End Sub

    Private Function DateTimeAsFileName() As String
        Return DateTimeAsFileName(Now)
    End Function

    Private Function DateTimeAsFileName(ByVal DateAndTime As DateTime) As String
        Return Format(DateAndTime, "yyyy-MM-ddTHH.mm.ss.fff")
    End Function

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub StorageRootToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StorageRootToolStripMenuItem.Click
        Process.Start(DB.StorageRoot)
    End Sub

    Private Sub tsbUpdate_Click(sender As Object, e As EventArgs) Handles tsbUpdate.Click
        tUpdate.Enabled = Not tUpdate.Enabled           'Timer
        tsbUpdate.Tag = tUpdate.Enabled                 'Button tag
        tsbUpdate.BackColor = CType(IIf(tUpdate.Enabled = True, System.Drawing.Color.Green, System.Drawing.SystemColors.Control), System.Drawing.Color)
        tbMain.Enabled = tUpdate.Enabled
        PrepareAction()
        SBIGEthernet.SetFilterName(1, "Luminance")
        SBIGEthernet.SetFilterName(2, "Red")
        SBIGEthernet.SetFilterName(3, "Green")
        SBIGEthernet.SetFilterName(4, "Blue")
        SBIGEthernet.SetFilterName(5, "Halpha")
        FinishAction()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub tsbExpose_Click(sender As Object, e As EventArgs) Handles tsbExpose.Click
        CType(sender, ToolStripButton).Enabled = False : DE()
        PrepareAction()
        SBIGEthernet.SetROI(DB.StartX, DB.StartY, DB.NumX, DB.NumY)
        SBIGEthernet.StartExpose(DB.ExposeTime, cSBIGEthernet.eFrameType.Light, Now)
        FinishAction()
        CType(sender, ToolStripButton).Enabled = True : DE()
    End Sub

    Private Sub MyFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MyFolderToolStripMenuItem.Click
        Process.Start(MyPath)
    End Sub

    Private Sub tsbDownload_Click(sender As Object, e As EventArgs) Handles tsbDownload.Click
        CType(sender, ToolStripButton).Enabled = False : DE()
        PrepareAction()
        Dim FileName As String = DB.StorageRoot & "\" & DB.FileBaseName
        If DB.StorageTimeStamp.Length > 0 Then FileName &= "_" & Format(Now, DB.StorageTimeStamp)
        FileName &= ".FIT"
        LoadImage(FileName)
        If DB.AutoOpenLast Then Process.Start(FileName)
        FinishAction()
        CType(sender, ToolStripButton).Enabled = True : DE()
    End Sub

    Private Sub SetCameraParameters()
        Dim Binning(1) As Integer
        Select Case DB.Binning.Length
            Case 0
                Binning(0) = 1 : Binning(1) = Binning(0)
            Case 1
                Binning(0) = CInt(DB.Binning) : Binning(1) = Binning(0)
            Case Else
                Binning(0) = CInt(DB.Binning.Substring(0, 1))
                Binning(1) = CInt(DB.Binning.Substring(DB.Binning.Length - 1, 1))
        End Select
        SBIGEthernet.SetBinning(Binning(0), Binning(1))
    End Sub

    Private Sub tsmiDarkFrame_Click(sender As Object, e As EventArgs) Handles tsmiDarkFrame.Click

        Dim WaitTime As Integer = 500

        CType(sender, ToolStripMenuItem).Enabled = False

        tsslExposeSeries.Text = "Starting " & DB.ExposeCount.ToString.Trim & " dark exposes ..."
        For Idx As Integer = 1 To DB.ExposeCount
            'Display status
            tsslExposeSeries.Text = "Exposing dark " & Idx.ToString.Trim & " / " & DB.ExposeCount.ToString.Trim
            System.Windows.Forms.Application.DoEvents()
            'Run dark expose
            PrepareAction()
            SBIGEthernet.StartExpose(DB.ExposeTime, cSBIGEthernet.eFrameType.Dark, Now)
            FinishAction()
            System.Threading.Thread.Sleep(WaitTime)
            'Wait for idle and image ready
            Do
                System.Threading.Thread.Sleep(WaitTime)
                System.Windows.Forms.Application.DoEvents()
            Loop Until SBIGEthernet.ImagerState = cSBIGEthernet.eImagerState.Idle And SBIGEthernet.ImageReady = True
            'Load image
            PrepareAction()
            LoadImage(DB.StorageRoot & "\Dark_" & DateTimeAsFileName() & "_" & Format(Idx, "000") & ".FIT")
            FinishAction()
        Next Idx
        tsslExposeSeries.Text = "No sequence running."

        CType(sender, ToolStripMenuItem).Enabled = True

    End Sub

    Private Sub LToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LToolStripMenuItem.Click, RToolStripMenuItem.Click, GToolStripMenuItem.Click, BToolStripMenuItem.Click, HalphaToolStripMenuItem.Click
        CType(sender, ToolStripMenuItem).Enabled = False : DE()
        PrepareAction()
        Dim FilterIndex As Integer = 0
        Select Case CType(sender, ToolStripMenuItem).Text
            Case "L" : FilterIndex = 1
            Case "R" : FilterIndex = 2
            Case "G" : FilterIndex = 3
            Case "G" : FilterIndex = 4
            Case "H-alpha" : FilterIndex = 5
        End Select
        Dim Answer As String = SBIGEthernet.ChangeFilter(FilterIndex)
        FinishAction()
        CType(sender, ToolStripMenuItem).Enabled = True : DE()
    End Sub

    Private Sub ToolStripButton1_Click_1(sender As Object, e As EventArgs)
        CType(sender, ToolStripButton).Enabled = False : DE()
        PrepareAction()
        SBIGEthernet.StartExpose(DB.ExposeTime, cSBIGEthernet.eFrameType.Light, Now)
        Dim FileName As String = DB.StorageRoot & "\CurrentImage"
        If DB.StorageTimeStamp.Length > 0 Then FileName &= "_" & Format(Now, DB.StorageTimeStamp)
        FileName &= ".FIT"
        LoadImage(FileName)
        If DB.AutoOpenLast Then Process.Start(FileName)
        FinishAction()
        CType(sender, ToolStripButton).Enabled = True : DE()
    End Sub

    Private Sub SetROIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetROIToolStripMenuItem.Click
        Dim Center As String = InputBox("Center: (Format xxx-yyy)")
        If Center.Length > 0 And Center.Contains("-") Then
            Dim Size As Integer = CInt(InputBox("Width:", "Width", "40"))
            DB.StartX = CInt(Split(Center, "-")(0)) - (Size \ 2)
            DB.StartY = CInt(Split(Center, "-")(1)) - (Size \ 2)
            DB.NumX = Size
            DB.NumY = Size
        End If
        pgMain.SelectedObject = DB
    End Sub

    Private Sub ConnectToFocuserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConnectToFocuserToolStripMenuItem.Click
        If String.IsNullOrEmpty(SelectedFocuser) = True Then
            Chooser.DeviceType = "Focuser"
            SelectedFocuser = Chooser.Choose("ASCOM.PWI3.Focuser")
            If String.IsNullOrEmpty(SelectedFocuser) = True Then Exit Sub
        End If
        Focuser = New ASCOM.DriverAccess.Focuser(SelectedFocuser)
        Focuser.Connected = True
        Dim Pos As Integer = Focuser.Position
        Dim Actions As ArrayList = Focuser.SupportedActions
    End Sub

    Private Sub AutoFocusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutoFocusToolStripMenuItem.Click
        If IsNothing(Focuser) = True Then
            MsgBox("Please connect focuser first!", MsgBoxStyle.OkOnly Or MsgBoxStyle.Critical, "Select focuser")
            Exit Sub
        End If
        DB.AutoOpenLast = False
        DB.StorageTimeStamp = String.Empty
        For Focus As Integer = 2500 To 2700 Step 20
            'Move focuser
            Focuser.Move(Focus)
            Do
                System.Threading.Thread.Sleep(100) : DE()
                Debug.Print(Focuser.Position.ToString.Trim & ":" & Focuser.IsMoving.ToString.Trim)
            Loop Until Focuser.IsMoving = False And Focuser.Position = Focus
            'Expose
            tsbExpose_Click(tsbExpose, Nothing)
            Do
                System.Threading.Thread.Sleep(100)
            Loop Until tsbExpose.Enabled = True
            'Download and store
            DB.FileBaseName = "SBIG_Focus_" & Format(Focuser.Position, "00000")
            tsbDownload_Click(tsbDownload, Nothing)
            Do
                System.Threading.Thread.Sleep(100)
            Loop Until tsbDownload.Enabled = True
        Next Focus
    End Sub

End Class
