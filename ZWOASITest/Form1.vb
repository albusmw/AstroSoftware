Option Explicit On
Option Strict On

'TODO: Join ASIGetControlCaps and ControlValues output

Public Class MyMainForm

    Private DoEvent As New Stopwatch
    Private DoEventEveryms As Integer = 100

    Private zgcMain As New ZedGraph.ZedGraphControl
    Private Plotter As cZEDGraphService

    Private pbMain As PictureBoxEx

    '''<summary>Monitor for the MIDI events.</summary>
    Private WithEvents MIDI As cMIDIMonitor

    '''<summary>SteamDeck.</summary>
    Private WithEvents MyDeck As StreamDeckSharp.IStreamDeckBoard

    Private Class Flags
        Public Shared ResetStatHold As Boolean = False
        Public Shared StopNow As Boolean = False
    End Class

    Private Sub btnRunTest_Click(sender As Object, e As EventArgs) Handles btnRunTest.Click

        'Get number of cameras, if there is at least one continue
        Dim Cameras As Integer = ZWO.ASICameraDll.ASIGetNumOfConnectedCameras()

        If Cameras > 0 Then

            btnRunTest.Enabled = False
            Dim CamHandle As Integer = 0
            Log("Opening first camera ...")

            Dim CameraInfo As ZWO.ASICameraDll.ASI_CAMERA_INFO = Nothing
            Dim CameraID As ZWO.ASICameraDll.ASI_ID = Nothing

            Dim NumberOfControls As Integer = 0

            'Open camera
            CallOK("ASIOpenCamera", ZWO.ASICameraDll.ASIOpenCamera(CamHandle))
            CallOK("ASIGetID", ZWO.ASICameraDll.ASIGetID(CamHandle, CameraID))

            'Set a specific ID
            'CameraID.ID = New Byte() {Asc("M"), Asc("A"), Asc("R"), Asc("T"), Asc("I"), Asc("N"), Asc("_"), Asc("W")}
            'CallOK(ZWO.ASICameraDll.ASISetID(CamHandle, CameraID))

            'Display all camera info elements
            CallOK("ASIGetCameraProperty", ZWO.ASICameraDll.ASIGetCameraProperty(CameraInfo, CamHandle))
            Log("Camera Info for <" & CameraID.IDAsString & ">:")
            Log("  " & "Name".PadRight(20) & ": " & CameraInfo.NameAsString)
            Log("  " & "CameraID".PadRight(20) & ": " & CameraInfo.CameraID)
            Log("  " & "MaxHeight".PadRight(20) & ": " & CameraInfo.MaxHeight)
            Log("  " & "MaxWidth".PadRight(20) & ": " & CameraInfo.MaxWidth)
            Log("  " & "IsColorCam".PadRight(20) & ": " & CameraInfo.IsColorCam)
            Log("  " & "BayerPattern".PadRight(20) & ": " & CameraInfo.BayerPattern)
            Log("  " & "SupportedBins".PadRight(20) & ": " & cZWOASI.SupportedBins(CameraInfo.SupportedBins))
            Log("  " & "SupportedVideoFormat".PadRight(20) & ": " & cZWOASI.SupportedVideoFormat(CameraInfo.SupportedVideoFormat))
            Log("  " & "PixelSize".PadRight(20) & ": " & CameraInfo.PixelSize)
            Log("  " & "MechanicalShutter".PadRight(20) & ": " & CameraInfo.MechanicalShutter)
            Log("  " & "ST4Port".PadRight(20) & ": " & CameraInfo.ST4Port)
            Log("  " & "IsCoolerCam".PadRight(20) & ": " & CameraInfo.IsCoolerCam)
            Log("  " & "IsUSB3Host".PadRight(20) & ": " & CameraInfo.IsUSB3Host)
            Log("  " & "IsUSB3Camera".PadRight(20) & ": " & CameraInfo.IsUSB3Camera)
            Log("  " & "ElecPerADU".PadRight(20) & ": " & CameraInfo.ElecPerADU)
            Log("  " & "BitDepth".PadRight(20) & ": " & CameraInfo.BitDepth)
            Log("  " & "IsTriggerCam".PadRight(20) & ": " & CameraInfo.IsTriggerCam)
            LogSep("-")

            'Open camera
            CallOK("ASIInitCamera", ZWO.ASICameraDll.ASIInitCamera(CamHandle))

            '================================================================================================================================
            'Read out every control that are available and set to default
            ZWO.ASICameraDll.ASIGetNumOfControls(0, NumberOfControls)
            Log(NumberOfControls.ValRegIndep & " controls")
            For ControlIdx As Integer = 0 To NumberOfControls - 1
                Dim ControlCap As ZWO.ASICameraDll.ASI_CONTROL_CAPS = Nothing
                ZWO.ASICameraDll.ASIGetControlCaps(CamHandle, ControlIdx, ControlCap)
                ZWO.ASICameraDll.ASISetControlValue(CamHandle, ControlCap.ControlType, ControlCap.DefaultValue)
                Log("  " & ControlCap.ControlType.ToString.PadRight(25) & ":" & ControlCap.NameAsString.PadRight(25) & ": " & TabFormat(ControlCap.MinValue) & " ... " & TabFormat(ControlCap.MaxValue) & ", default: " & TabFormat(ControlCap.DefaultValue) & " (" & ControlCap.DescriptionAsString & ")")
            Next ControlIdx
            LogSep("-")

            'Configure exposure
            Log("Preparing exposure ...")
            Dim Width As Integer = CInt(CameraInfo.MaxWidth)
            Dim Height As Integer = CInt(CameraInfo.MaxHeight)
            Dim ROI_X As Integer = CInt((CameraInfo.MaxWidth / 2) - (Width / 2))
            Dim ROI_Y As Integer = CInt((CameraInfo.MaxHeight / 2) - (Height / 2))
            Dim ROIRequested As New Rectangle(ROI_X, ROI_Y, Width, Height)
            M.DB.MyImgType = ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_RAW16
            M.DB.ROISet = SetROI(CamHandle, ROIRequested, M.DB.ImgType)

            '================================================================================================================================
            'Configure parameters
            CallOK("ASISetControlValue ASI_EXPOSURE", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_EXPOSURE, M.DB.Capture_ExposureTime_us))
            CallOK("ASISetControlValue ASI_GAIN", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_GAIN, M.DB.Capture_Gain))
            CallOK("ASISetControlValue ASI_GAMMA", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_GAMMA, M.DB.Capture_Gamma))
            CallOK("ASISetControlValue ASI_BRIGHTNESS", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_BRIGHTNESS, 1))
            CallOK("ASISetControlValue ASI_WB_B", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_WB_B, 0))
            CallOK("ASISetControlValue ASI_WB_R", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_WB_R, 0))
            CallOK("ASISetControlValue ASI_MONO_BIN", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_MONO_BIN, 1))
            CallOK("ASISetControlValue ASI_HIGH_SPEED_MODE", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_HIGH_SPEED_MODE, 1))
            CallOK("ASISetControlValue ASI_BANDWIDTHOVERLOAD", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_BANDWIDTHOVERLOAD, 100))
            LogSep("-")

            '================================================================================================================================
            'Read out all set values
            Log("ControlValues:")
            For Each X As ZWO.ASICameraDll.ASI_CONTROL_TYPE In [Enum].GetValues(GetType(ZWO.ASICameraDll.ASI_CONTROL_TYPE))
                Log("  " & X.ToString.Trim.PadRight(25) & ": " & ZWO.ASICameraDll.ASIGetControlValue(CamHandle, X))
            Next X
            LogSep("-")

            'Read out all set values - special values
            Log("SpecialValues:")
            Dim Offset_HighestDR As Integer : Dim Offset_UnityGain As Integer : Dim Gain_LowestRN As Integer : Dim Offset_LowestRN As Integer
            ZWO.ASICameraDll.ASIGetGainOffset(CamHandle, Offset_HighestDR, Offset_UnityGain, Gain_LowestRN, Offset_LowestRN)
            Log("  " & "Offset configuration:")
            Log("  " & "  HighestDR         : " & Offset_HighestDR.ValRegIndep)
            Log("  " & "  UnityGain         : " & Offset_UnityGain.ValRegIndep)
            Log("  " & "  LowestRN          : " & Offset_LowestRN.ValRegIndep)
            Log("  " & "    @               : " & Gain_LowestRN.ValRegIndep)
            LogSep("-")

            'Run exposure
            Log("Exposing ...")

            Dim Duration_Total_ms As Long = 0

            M.DB.StatCalc.ResetAllProcessors()

            Flags.StopNow = False

            DoEvent.Restart()

            Dim StartCapture As Boolean = True
            Dim VideoModeRunning As Boolean = False

            If M.DB.Capture_Mode = cDB.eCaptureMode.Video Then
                If ZWO.ASICameraDll.ASIStartVideoCapture(CamHandle) = ZWO.ASICameraDll.ASI_ERROR_CODE.ASI_SUCCESS Then
                    VideoModeRunning = True
                Else
                    StartCapture = False
                    LogError("Could not start video mode!")
                End If
            End If

            Dim Stopper As New Stopwatch : Stopper.Restart()
            Dim CapturedFrames As Integer = 0
            Dim DroppedFrames As Integer = 0

            Do

                'Calculate required buffer size and configure buffer
                M.DB.EnsureCorrectBuffer()

                'Change settings if required
                Dim ValueChanged As Boolean = False
                Dim ExpTime_Running As Integer = ZWO.ASICameraDll.ASIGetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_EXPOSURE)
                If ExpTime_Running <> M.DB.Capture_ExposureTime_us Then
                    CallOK("ASISetControlValue ASI_EXPOSURE", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_EXPOSURE, M.DB.Capture_ExposureTime_us))
                    ValueChanged = True
                End If
                Dim Gain_Running As Integer = ZWO.ASICameraDll.ASIGetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_GAIN)
                If Gain_Running <> M.DB.Capture_Gain Then
                    CallOK("ASISetControlValue ASI_GAIN", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_GAIN, M.DB.Capture_Gain))
                    ValueChanged = True
                End If
                Dim Gamma_Running As Integer = ZWO.ASICameraDll.ASIGetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_GAMMA)
                If Gamma_Running <> M.DB.Capture_Gamma Then
                    CallOK("ASISetControlValue ASI_GAMMA", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_GAMMA, M.DB.Capture_Gamma))
                    ValueChanged = True
                End If
                Dim ROI_Running As Rectangle = GetROI(CamHandle)
                If ROI_Running <> M.DB.ROISet Then
                    M.DB.ROISet = SetROI(CamHandle, M.DB.ROISet, M.DB.ImgType) : ValueChanged = True
                End If
                If ValueChanged = True Then UpdatePropGrid()

                'Capture
                Dim CaptureOK As Boolean = False
                If VideoModeRunning = True Then
                    'Video mode
                    Dim CaptureStatus As ZWO.ASICameraDll.ASI_ERROR_CODE = ZWO.ASICameraDll.ASI_ERROR_CODE.ASI_ERROR_END
                    Do
                        CaptureStatus = ZWO.ASICameraDll.ASIGetVideoData(CamHandle, M.DB.CaptureDB.ImgBuffererPtr, M.DB.CaptureDB.ImgBufferSize, 0)
                        ZWO.ASICameraDll.ASIGetDroppedFrames(CamHandle, DroppedFrames)
                        If CaptureStatus = ZWO.ASICameraDll.ASI_ERROR_CODE.ASI_SUCCESS Then
                            CapturedFrames += 1
                            CaptureOK = True
                            Dim DroppedPct As Double = 100 * (DroppedFrames / (DroppedFrames + CapturedFrames))
                            tsslCaptureProgress.Text = CapturedFrames.ValRegIndep & " video frames captured, dropped " & DroppedFrames.ValRegIndep & " (" & DroppedPct.ValRegIndep("0.0") & " %)"
                        Else
                            System.Threading.Thread.Sleep(1)
                        End If
                    Loop Until (CaptureStatus = ZWO.ASICameraDll.ASI_ERROR_CODE.ASI_SUCCESS) Or (Flags.StopNow = True)
                Else
                    'Still mode
                    Dim CaptureStatus As ZWO.ASICameraDll.ASI_EXPOSURE_STATUS = ZWO.ASICameraDll.ASI_EXPOSURE_STATUS.ASI_EXP_FAILED
                    Dim Duration_ms As Long = 0
                    CaptureStatus = RunExposure(CamHandle, 0, -1, Duration_ms)
                    Duration_Total_ms += Duration_ms
                    If CaptureStatus = ZWO.ASICameraDll.ASI_EXPOSURE_STATUS.ASI_EXP_SUCCESS Then
                        CallOK("ASIGetDataAfterExp", ZWO.ASICameraDll.ASIGetDataAfterExp(CamHandle, M.DB.CaptureDB.ImgBuffererPtr, M.DB.CaptureDB.ImgBufferSize))
                        CapturedFrames += 1
                        CaptureOK = True
                        tsslCaptureProgress.Text = CapturedFrames.ValRegIndep & " still frames captured"
                    Else
                        Log("Failed after " & CapturedFrames.ValRegIndep & " exposures, status: <" & CaptureStatus.ToString.Trim & ">")
                        Exit Do
                    End If
                End If

                'Process
                If CaptureOK = True Then
                    ProcessFrame(CapturedFrames)
                End If

                ProcessEvents()

            Loop Until Flags.StopNow = True

            Stopper.Stop()
            Duration_Total_ms = Stopper.ElapsedMilliseconds
            Log("Speed: " & (CapturedFrames / (Duration_Total_ms / 1000)).ValRegIndep("0.0") & " fps")

            'Close camera
            Log("Closing camera ...")
            If VideoModeRunning = True Then
                ZWO.ASICameraDll.ASIStopVideoCapture(CamHandle)
            End If
            CallOK("ASICloseCamera", ZWO.ASICameraDll.ASICloseCamera(CamHandle))
            GC.Collect()
            LogSep("=")

        Else

            Log("!!! NO CAMERA FOUND")

        End If

        btnRunTest.Enabled = True

    End Sub

    Private Sub ProcessEvents()
        'Process events
        If DoEvent.ElapsedMilliseconds >= DoEventEveryms Then
            Plotter.ForceUpdate()
            System.Windows.Forms.Application.DoEvents()
            DoEvent.Restart()
        End If
    End Sub

    Private Sub ProcessFrame(ByVal FrameIdx As Integer)

        Dim Data_Min As UInt16 = UInt16.MaxValue
        Dim Data_Max As UInt16 = UInt16.MinValue
        M.DB.StatCalc.DataProcessor_UInt16.ImageData(0).Data = M.DB.CaptureDB.ImgBufferer_UInt16
        M.DB.IPP.MinMax(M.DB.StatCalc.DataProcessor_UInt16.ImageData(0).Data, Data_Min, Data_Max)

        If M.DB.Flow_CalcStatistics = True Then

            'Calculate single frame statistics
            M.DB.SingleStat = M.DB.StatCalc.ImageStatistics()

            'Calculate combined statistics
            If Flags.ResetStatHold = True Then
                FrameIdx = 1 : Flags.ResetStatHold = False
            End If
            If FrameIdx = 1 Then M.DB.LoopStat = Nothing
            M.DB.LoopStat = AstroNET.Statistics.CombineStatistics(M.DB.SingleStat.DataMode, M.DB.SingleStat, M.DB.LoopStat)

            'Show statistics text
            tbStatOutput.Text = Join(M.DB.SingleStat.StatisticsReport(True, False).ToArray, System.Environment.NewLine)

            'Display statistics
            If M.DB.Flow_DisplayStatistics = True Then
                Dim CurrentCurveWidth As Integer = 1
                Plotter.PlotXvsY("Mono", M.DB.SingleStat.MonochromHistogram_Int, 1, New cZEDGraphService.sGraphStyle(System.Drawing.Color.Black, cZEDGraphService.eCurveMode.LinesAndPoints, CurrentCurveWidth))
                Plotter.PlotXvsY("Mono Accumumulated", M.DB.LoopStat.MonochromHistogram_Int, M.DB.LoopStat.Count, New cZEDGraphService.sGraphStyle(System.Drawing.Color.Orange, cZEDGraphService.eCurveMode.Dots, CurrentCurveWidth))
                Plotter.GridOnOff(True, True)
                Select Case M.DB.GraphOut_AutoAlways
                    Case cZEDGraphService.eScalingMode.LeaveAsIs
                        'Do nothing
                    Case cZEDGraphService.eScalingMode.Automatic
                        Plotter.ManuallyScaleXAxis(0, M.DB.SingleStat.MonoStatistics_Int.Max.Key)
                    Case cZEDGraphService.eScalingMode.FullRange
                        Plotter.ManuallyScaleXAxis(0, 65536)
                End Select
                Plotter.AutoScaleYAxisLog()
            End If

        End If

        'Display image
        If M.DB.Flow_DisplayImage = True Then
            Dim SpeedReport As List(Of String) = M.DB.ImageFromData.GenerateDisplayImage(M.DB.StatCalc.DataProcessor_UInt16.ImageData(0).Data, Data_Min, Data_Max, M.DB.IPP)
            'ReportToFile(SpeedReport)
            M.DB.ImageFromData.OutputImage.UnlockBits()
            If DisplayROIPart() = False Then
                pbMain.Image = M.DB.ImageFromData.OutputImage.BitmapToProcess
            Else
                pbMain.Image = M.DB.ImageFromData.OutputImage.BitmapToProcess.Clone(M.DB.ImageOut_ROI, Imaging.PixelFormat.Format24bppRgb)
            End If
            pbMain.InterpolationMode = M.DB.ImageOut_Interpolation
            pbMain.Invalidate()
            pbMain.Refresh()
        End If

        'Store image
        If M.DB.Flow_StoreImage = True Then
            Dim FITSFormat As cFITSWriter.eBitPix = cFITSWriter.eBitPix.Int16
            cFITSWriter.Write("C:\TEMP\Image" & FrameIdx.ValRegIndep("00000") & ".fits", M.DB.CaptureDB.ImgBufferer_UInt16, FITSFormat, False)
        End If

    End Sub

    '''<summary>Returns to if a display ROI part is selected and valid.</summary>
    Private Function DisplayROIPart() As Boolean
        If M.DB.ImageOut_DisplayPart = False Then
            Return False
        Else
            If M.DB.ImageOut_ROI.Width = 0 Then Return False
            If M.DB.ImageOut_ROI.Height = 0 Then Return False
            If M.DB.ImageOut_ROI.X + M.DB.ImageOut_ROI.Width > M.DB.ImageFromData.OutputImage.BitmapToProcess.Width Then Return False
            If M.DB.ImageOut_ROI.Y + M.DB.ImageOut_ROI.Height > M.DB.ImageFromData.OutputImage.BitmapToProcess.Height Then Return False
            Return True
        End If
    End Function

    '''<summary>Set the ROI wanted.</summary>
    Private Function SetROI(ByVal CamHandle As Integer, ByVal ROI As Rectangle, ByVal ImgType As ZWO.ASICameraDll.ASI_IMG_TYPE) As Rectangle
        Dim RetVal As New Rectangle(-1, -1, -1, -1)
        Dim Binning As Integer = 1
        CallOK("ASISetROIFormat", ZWO.ASICameraDll.ASISetROIFormat(CamHandle, ROI.Width, ROI.Height, Binning, ImgType))
        CallOK("ASISetStartPos", ZWO.ASICameraDll.ASISetStartPos(CamHandle, ROI.X, ROI.Y))
        Binning = -1 : ImgType = ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_END
        CallOK("ASIGetROIFormat", ZWO.ASICameraDll.ASIGetROIFormat(CamHandle, RetVal.Width, RetVal.Height, Binning, ImgType))
        CallOK("ASIGetStartPos", ZWO.ASICameraDll.ASIGetStartPos(CamHandle, RetVal.X, RetVal.Y))
        Return GetROI(CamHandle, Binning, ImgType)
    End Function

    Private Function GetROI(ByVal CamHandle As Integer) As Rectangle
        Dim Binning As Integer = -1
        Dim ImgType As ZWO.ASICameraDll.ASI_IMG_TYPE = ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_END
        Return GetROI(CamHandle, Binning, ImgType)
    End Function

    Private Function GetROI(ByVal CamHandle As Integer, ByRef Binning As Integer, ByRef ImgType As ZWO.ASICameraDll.ASI_IMG_TYPE) As Rectangle
        Dim RetVal As New Rectangle(-1, -1, -1, -1)
        ImgType = ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_END
        Binning = -1
        CallOK("ASIGetROIFormat", ZWO.ASICameraDll.ASIGetROIFormat(CamHandle, RetVal.Width, RetVal.Height, Binning, ImgType))
        CallOK("ASIGetStartPos", ZWO.ASICameraDll.ASIGetStartPos(CamHandle, RetVal.X, RetVal.Y))
        Return RetVal
    End Function

    '''<summary>Run the exposure.</summary>
    '''<param name="CamHandle">Camera handle.</param>
    '''<param name="PollSuspend">Suspend ... ms between each poll for ASIGetExpStatus.</param>
    '''<param name="MaxFailCount">Maximum # of failed accepted; if exceeded the routine will return with a failure; set -1 to poll without end.</param>
    '''<returns>Exposure status.</returns>
    Private Function RunExposure(ByRef CamHandle As Integer, ByVal PollSuspend As Integer, ByVal MaxFailCount As Integer, ByRef Duration_ms As Long) As ZWO.ASICameraDll.ASI_EXPOSURE_STATUS

        Dim Ticker As New Stopwatch : Ticker.Reset() : Ticker.Start()
        CallOK("ASIStartExposure", ZWO.ASICameraDll.ASIStartExposure(CamHandle, ZWO.ASICameraDll.ASI_BOOL.ASI_FALSE))
        Dim ExpStatus As ZWO.ASICameraDll.ASI_EXPOSURE_STATUS = ZWO.ASICameraDll.ASI_EXPOSURE_STATUS.ASI_EXP_FAILED
        Dim ExpFailedCount As Integer = 0
        Do
            If CallOK("ASIGetExpStatus", ZWO.ASICameraDll.ASIGetExpStatus(CamHandle, ExpStatus)) = False Then
                Exit Do
            Else
                Select Case ExpStatus
                    Case ZWO.ASICameraDll.ASI_EXPOSURE_STATUS.ASI_EXP_FAILED
                        'Exit on too many failures or Restart exposure
                        ExpFailedCount += 1
                        If (ExpFailedCount >= MaxFailCount) And (MaxFailCount <> -1) Then Exit Do
                        ZWO.ASICameraDll.ASIStopExposure(CamHandle)
                        ZWO.ASICameraDll.ASIStartExposure(CamHandle, ZWO.ASICameraDll.ASI_BOOL.ASI_FALSE)
                        Log("###### EXPOSING FAILED! ######")
                    Case ZWO.ASICameraDll.ASI_EXPOSURE_STATUS.ASI_EXP_IDLE
                        Log("Exposure idle, existing ...")
                    Case ZWO.ASICameraDll.ASI_EXPOSURE_STATUS.ASI_EXP_WORKING
                        'Still working ...
                    Case ZWO.ASICameraDll.ASI_EXPOSURE_STATUS.ASI_EXP_SUCCESS
                        Exit Do
                End Select
            End If
            System.Threading.Thread.Sleep(PollSuspend)
        Loop Until 1 = 0
        Ticker.Stop()
        Duration_ms = Ticker.ElapsedMilliseconds

        Return ExpStatus

    End Function

    Private Sub LogTiming(ByVal Text As String, ByVal Ticker As Stopwatch)
        Log(Text & ": " & Ticker.ElapsedMilliseconds.ValRegIndep & " ms")
    End Sub

    Private Sub LogSep(ByVal Ch As String)
        Log(New String(Ch.Chars(0), 40))
    End Sub

    Private Function TabFormat(ByVal Text As Integer) As String
        Return Text.ValRegIndep.PadLeft(10)
    End Function

    Private Function CallOK(ByVal FunctionName As String, ByVal ErrorCode As ZWO.ASICameraDll.ASI_ERROR_CODE) As Boolean
        If ErrorCode = ZWO.ASICameraDll.ASI_ERROR_CODE.ASI_SUCCESS Then
            Return True
        Else
            LogError("ZWO ASI ERROR on <" & FunctionName & ">: <" & ErrorCode.ToString.Trim & "> #####")
            Return False
        End If
    End Function

    Private Sub LogError(ByVal Text As String)
        Log("########### " & Text & " ###########")
    End Sub

    Private Sub Log(ByVal Text As String)
        Text = Now.ForLogging & "|" & Text
        If M.DB.Log_Generic.Length = 0 Then
            M.DB.Log_Generic.Append(Text)
        Else
            M.DB.Log_Generic.Append(System.Environment.NewLine & Text)
        End If
        DisplayLog()
    End Sub

    Private Sub DisplayLog()
        With tbLogOutput
            .Text = M.DB.Log_Generic.ToString
            If tbLogOutput.Text.Length > 0 Then
                .SelectionStart = .Text.Length - 1
                .SelectionLength = 0
            End If
            .ScrollToCaret()
        End With
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub btnClearLog_Click(sender As Object, e As EventArgs) Handles btnClearLog.Click
        M.DB.Log_Generic.Clear()
        DisplayLog()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Load IPP
        Dim IPPLoadError As String = String.Empty
        Dim IPPPathToUse As String = cIntelIPP.SearchDLLToUse(cIntelIPP.PossiblePaths(M.DB.MyPath).ToArray, IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = True Then
            M.DB.IPP = New cIntelIPP(IPPPathToUse)
            cFITSWriter.UseIPPForWriting = True
        Else
            cFITSWriter.UseIPPForWriting = False
        End If
        cFITSWriter.IPPPath = M.DB.IPP.IPPPath
        M.DB.StatCalc = New AstroNET.Statistics(M.DB.IPP.IPPPath)

        'Add ZEDGraph
        scRight.Panel2.Controls.Add(zgcMain)
        zgcMain.Dock = DockStyle.Fill
        Plotter = New cZEDGraphService(zgcMain)

        'Load custom controls - main image (must be done due to 64-bit IDE limitation)
        pbMain = New PictureBoxEx
        scRight.Panel1.Controls.Add(pbMain)
        pbMain.Dock = DockStyle.Fill
        pbMain.InterpolationMode = M.DB.ImageOut_Interpolation
        pbMain.SizeMode = PictureBoxSizeMode.Zoom
        pbMain.BackColor = Color.Purple

        'Configure GUI elements
        UpdatePropGrid()

        'MIDI monitor
        MIDI = New cMIDIMonitor
        If MIDI.MIDIDeviceCount > 0 Then MIDI.SelectMidiDevice(0)

        'StreamDeck
        MyDeck = StreamDeckSharp.StreamDeck.OpenDevice()
        For Idx As Integer = 0 To 33
            Dim MyImage As New Bitmap(144, 144)
            Dim newGraphics As Graphics = Graphics.FromImage(MyImage)
            newGraphics.DrawString(Chr(Idx + 65).ToString.Trim, New Font("Courier New", 54), New SolidBrush(Color.Blue), New PointF(40, 40))
            newGraphics.DrawRectangle((New Pen(Color.Orange, 2)), New Rectangle(2, 2, 140, 140))
            SetKeyBitmap(Idx, MyImage)
        Next Idx

    End Sub

    Private Sub SetKeyBitmap(ByVal KeyId As Integer, ByVal FileName As String)
        Dim X As OpenMacroBoard.SDK.IKeyBitmapFactory = OpenMacroBoard.SDK.KeyBitmap.Create
        Dim KeyToLoad As OpenMacroBoard.SDK.KeyBitmap = OpenMacroBoard.SDK.KeyBitmapDrawingExtensions.FromFile(X, FileName)
        MyDeck.SetKeyBitmap(KeyId, KeyToLoad)
    End Sub

    Private Sub SetKeyBitmap(ByVal KeyId As Integer, ByVal ImageToLoad As Bitmap)
        Dim X As OpenMacroBoard.SDK.IKeyBitmapFactory = OpenMacroBoard.SDK.KeyBitmap.Create
        Dim KeyToLoad As OpenMacroBoard.SDK.KeyBitmap = OpenMacroBoard.SDK.KeyBitmapDrawingExtensions.FromBitmap(X, ImageToLoad)
        MyDeck.SetKeyBitmap(KeyId, KeyToLoad)
    End Sub

    Private Sub UpdatePropGrid()
        pgConfig.SelectedObject = M.DB
        pgGraphics.SelectedObject = M.DB.ImageFromData
    End Sub

    Private Sub btnStopTest_Click(sender As Object, e As EventArgs) Handles btnStopTest.Click
        Flags.StopNow = True
    End Sub

    Private Sub tbGain_MouseWheel(sender As Object, e As MouseEventArgs)
        ChangeGain(e.Delta)
    End Sub

    Private Sub tbExp_MouseWheel(sender As Object, e As MouseEventArgs)
        ChangeExp(e.Delta)
    End Sub

    Private Sub btnResetStatHold_Click(sender As Object, e As EventArgs) Handles btnResetStatHold.Click
        Flags.ResetStatHold = True
    End Sub

    Private Sub ReportToFile(ByVal Content As List(Of String))
        System.IO.File.AppendAllLines(System.IO.Path.Combine(M.DB.MyPath, "SpeedReport.txt"), Content.ToArray)
    End Sub

    Private Sub ChangeExp(ByVal Delta As Integer)
        Dim Capture_ExposureTime_New As Double = M.DB.Capture_ExposureTime
        Capture_ExposureTime_New += Math.Sign(Delta) * M.DB.ValueStep_Exposure
        If Capture_ExposureTime_New < 0 Then Capture_ExposureTime_New = 0
        M.DB.Capture_ExposureTime = Capture_ExposureTime_New
        UpdatePropGrid()
    End Sub

    Private Sub ChangeGain(ByVal Delta As Integer)
        Dim Capture_Gain_New As Integer = M.DB.Capture_Gain
        Capture_Gain_New += Math.Sign(Delta) * M.DB.ValueStep_Gain
        If Capture_Gain_New < 0 Then Capture_Gain_New = 0
        If Capture_Gain_New > 510 Then Capture_Gain_New = 510
        M.DB.Capture_Gain = Capture_Gain_New
        UpdatePropGrid()
    End Sub

    Private Sub ChangeGamma_Capture(ByVal Delta As Integer)
        Dim NewGamma As Integer = M.DB.Capture_Gamma
        NewGamma += Math.Sign(Delta)
        If NewGamma < 0 Then NewGamma = 0
        If NewGamma > 100 Then NewGamma = 100
        M.DB.Capture_Gamma = NewGamma
        UpdatePropGrid()
    End Sub

    Private Sub ChangeGamma_Display(ByVal Delta As Integer)
        Dim NewGamma As Double = M.DB.ImageFromData.Gamma
        NewGamma += Math.Sign(Delta) * M.DB.ValueStep_Gamma
        If NewGamma < 0 Then NewGamma = 0
        M.DB.ImageFromData.Gamma = NewGamma
        UpdatePropGrid()
    End Sub

    '''<summary>Handle data entered via a MIDI input device.</summary>
    Private Sub MIDI_Increment(Channel As Integer, Value As Integer) Handles MIDI.Increment
        Select Case Channel
            Case 1
                ChangeExp(Value)
            Case 2
                ChangeGain(Value)
            Case 3
                ChangeGamma_Display(Value)
            Case 8
                ChangeGamma_Capture(Value)
        End Select
    End Sub

    Private Sub MIDI_Reset(Channel As Integer) Handles MIDI.Reset
        Select Case Channel
            Case 1
                M.DB.Capture_ExposureTime = 0.001
            Case 2
                M.DB.Capture_Gain = 20
            Case 3
                M.DB.ImageFromData.Gamma = 1.0
            Case 8
                M.DB.Capture_Gamma = 50
        End Select
        UpdatePropGrid()
    End Sub

    Private Sub MyDeck_KeyStateChanged(sender As Object, e As OpenMacroBoard.SDK.KeyEventArgs) Handles MyDeck.KeyStateChanged
        Select Case e.Key
            Case 0
                M.DB.ImageFromData.ColorMap = cColorMaps.eMaps.None
            Case 1
                M.DB.ImageFromData.ColorMap = cColorMaps.eMaps.Bone
            Case 2
                M.DB.ImageFromData.ColorMap = cColorMaps.eMaps.FalseColor
            Case 3
                M.DB.ROISet = New Rectangle(M.DB.ROISet.X - 50, M.DB.ROISet.Y, M.DB.ROISet.Width, M.DB.ROISet.Height)
        End Select
    End Sub

End Class
