Option Explicit On
Option Strict On

'TODO: Join ASIGetControlCaps and ControlValues output

Public Class MainForm

    Private Sub btnRunTest_Click(sender As Object, e As EventArgs) Handles btnRunTest.Click

        'Get number of cameras, if there is at least one continue
        Dim Cameras As Integer = ZWO.ASICameraDll.ASIGetNumOfConnectedCameras()

        If Cameras > 0 Then

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

            'Read out every control that are available and set to default
            ZWO.ASICameraDll.ASIGetNumOfControls(0, NumberOfControls)
            Log(NumberOfControls.ValRegIndep & " controls")
            For ControlIdx As Integer = 0 To NumberOfControls - 1
                Dim ControlCap As ZWO.ASICameraDll.ASI_CONTROL_CAPS = Nothing
                ZWO.ASICameraDll.ASIGetControlCaps(CamHandle, ControlIdx, ControlCap)
                Log("  " & ControlCap.ControlType.ToString.PadRight(25) & ":" & ControlCap.NameAsString.PadRight(25) & ": " & TabFormat(ControlCap.MinValue) & " ... " & TabFormat(ControlCap.MaxValue) & ", default: " & TabFormat(ControlCap.DefaultValue) & " (" & ControlCap.DescriptionAsString & ")")
                ZWO.ASICameraDll.ASISetControlValue(CamHandle, ControlCap.ControlType, ControlCap.DefaultValue)
            Next ControlIdx
            LogSep("-")

            'Read out all set values
            Log("ControlValues:")
            For Each X As ZWO.ASICameraDll.ASI_CONTROL_TYPE In [Enum].GetValues(GetType(ZWO.ASICameraDll.ASI_CONTROL_TYPE))
                Log("  " & X.ToString.Trim.PadRight(25) & ": " & ZWO.ASICameraDll.ASIGetControlValue(CamHandle, X))
            Next X
            LogSep("-")

            'Read out all special values
            Log("SpecialValues:")
            Dim Offset_HighestDR As Integer : Dim Offset_UnityGain As Integer : Dim Gain_LowestRN As Integer : Dim Offset_LowestRN As Integer
            ZWO.ASICameraDll.ASIGetGainOffset(CamHandle, Offset_HighestDR, Offset_UnityGain, Gain_LowestRN, Offset_LowestRN)
            Log("  " & "Offset configuration:")
            Log("  " & "  HighestDR         : " & Offset_HighestDR.ValRegIndep)
            Log("  " & "  UnityGain         : " & Offset_UnityGain.ValRegIndep)
            Log("  " & "  LowestRN          : " & Offset_LowestRN.ValRegIndep)
            Log("  " & "    @               : " & Gain_LowestRN.ValRegIndep)
            LogSep("-")

            'Configure exposure
            Log("Preparing exposure ...")
            Dim ROIWidth As Integer = -1
            Dim ROIHeight As Integer = -1
            Dim ROIBin As Integer = -1
            Dim ROIImgType As ZWO.ASICameraDll.ASI_IMG_TYPE = ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_END
            Dim StartPosX As Integer = -1
            Dim StartPosY As Integer = -1
            CallOK("ASISetROIFormat", ZWO.ASICameraDll.ASISetROIFormat(CamHandle, CameraInfo.MaxWidth, CameraInfo.MaxHeight, 1, ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_RAW16))
            CallOK("ASIGetROIFormat", ZWO.ASICameraDll.ASIGetROIFormat(CamHandle, ROIWidth, ROIHeight, ROIBin, ROIImgType))
            CallOK("ASISetStartPos", ZWO.ASICameraDll.ASISetStartPos(CamHandle, 0, 0))
            CallOK("ASIGetStartPos", ZWO.ASICameraDll.ASIGetStartPos(CamHandle, StartPosX, StartPosY))

            CallOK("ASIGetStartPos", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_EXPOSURE, 1000))

            'Prepare buffers
            Dim CamRawBuffer((CameraInfo.MaxWidth * CameraInfo.MaxHeight * 2) - 1) As Byte
            Dim CamRawGAC As Runtime.InteropServices.GCHandle = Runtime.InteropServices.GCHandle.Alloc(CamRawBuffer, Runtime.InteropServices.GCHandleType.Pinned)
            Dim CamRawPtr As IntPtr = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(CamRawBuffer, 0)
            Dim CamRawBufferBytes As Integer = CInt(CamRawBuffer.LongLength * 2)

            'Configure parameters
            Dim ExpTimeToSet As Integer = 1
            Dim GainToSet As Integer = 1
            Dim GammaToSet As Integer = 1
            Dim BrightnessToSet As Integer = 1
            CallOK("ASISetControlValue ASI_EXPOSURE", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_EXPOSURE, ExpTimeToSet))
            CallOK("ASISetControlValue ASI_GAIN", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_GAIN, GainToSet))
            CallOK("ASISetControlValue ASI_GAMMA", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_GAMMA, GammaToSet))
            CallOK("ASISetControlValue ASI_BRIGHTNESS", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_BRIGHTNESS, BrightnessToSet))
            CallOK("ASISetControlValue ASI_WB_B", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_WB_B, 0))
            CallOK("ASISetControlValue ASI_WB_R", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_WB_R, 0))
            CallOK("ASISetControlValue ASI_MONO_BIN", ZWO.ASICameraDll.ASISetControlValue(CamHandle, ZWO.ASICameraDll.ASI_CONTROL_TYPE.ASI_MONO_BIN, 1))
            LogSep("-")

            'Run exposure
            Log("Exposing ...")
            Dim Status As ZWO.ASICameraDll.ASI_EXPOSURE_STATUS = RunExposure(CamHandle, 5)

            'Release buffers
            CamRawGAC.Free()

            'Close camera
            Log("Closing camera ...")
            CallOK("ASICloseCamera", ZWO.ASICameraDll.ASICloseCamera(CamHandle))
            GC.Collect()
            LogSep("=")

        End If


    End Sub

    Private Function RunExposure(ByRef CamHandle As Integer, ByVal MaxFailCount As Integer) As ZWO.ASICameraDll.ASI_EXPOSURE_STATUS

        Dim Ticker As New Stopwatch : Ticker.Reset() : Ticker.Start()
        CallOK("ASIStartExposure", ZWO.ASICameraDll.ASIStartExposure(CamHandle, ZWO.ASICameraDll.ASI_BOOL.ASI_FALSE))
        Dim ExpStatus As ZWO.ASICameraDll.ASI_EXPOSURE_STATUS = ZWO.ASICameraDll.ASI_EXPOSURE_STATUS.ASI_EXP_FAILED
        Dim ExpFailedCount As Integer = 0
        Do
            System.Threading.Thread.Sleep(1000)
            'Application.DoEvents()

            If CallOK("ASIGetExpStatus", ZWO.ASICameraDll.ASIGetExpStatus(CamHandle, ExpStatus)) = False Then
                Exit Do
            Else
                Select Case ExpStatus
                    Case ZWO.ASICameraDll.ASI_EXPOSURE_STATUS.ASI_EXP_FAILED
                        'Exit on too many failures or Restart exposure
                        ExpFailedCount += 1
                        If ExpFailedCount >= MaxFailCount Then Exit Do
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
        Loop Until 1 = 0
        Ticker.Stop()
        LogTiming("Exposure duration", Ticker)

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
        DE()
    End Sub

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

End Class
