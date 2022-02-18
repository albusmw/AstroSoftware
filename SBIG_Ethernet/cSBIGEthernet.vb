Option Explicit On
Option Strict On

Public Class cSBIGEthernet

  Private WithEvents MyWebClient As Net.WebClient

  '================================================================================

  Public Enum eFanState As Integer
    [Off] = 0
    [On] = 1
    [Auto] = 2
  End Enum

  Public Enum eImagerState As Integer
    [Idle] = 0
    [Exposing] = 2
    [ReadingOutCCD] = 3
    [Error] = 5
  End Enum

  Public Enum eFrameType As Integer
    [Dark] = 0
    [Light] = 1
    [Bias] = 2
    [FlatField] = 3
  End Enum

  Public Enum eFilterState As Integer
    [Idle] = 0
    [Moving] = 1
    [Error] = 2
  End Enum

  '================================================================================

  Public ReadOnly Property LastUpdate() As DateTime
    Get
      Return MyLastUpdate
    End Get
  End Property
  Private MyLastUpdate As New DateTime(0)

  '''<summary>Last expose start date and time - will return Date(0) if expose was finished.</summary>
  Public ReadOnly Property LastExposeStart() As DateTime
    Get
      Return MyLastExposeStart
    End Get
  End Property
  Private MyLastExposeStart As New DateTime(0)

  '''<summary>Duration of last requested expose [s] - will return Double.NaN if expose was finised.</summary>
  Public ReadOnly Property LastExposeDuration() As Double
    Get
      Return MyLastExposeDuration
    End Get
  End Property
    Private MyLastExposeDuration As Double = Double.NaN

    Public Property SleepBetweenQueries() As Integer
        Get
            Return MySleepBetweenQueries
        End Get
        Set(value As Integer)
            MySleepBetweenQueries = value
        End Set
    End Property
    Private MySleepBetweenQueries As Integer = 100

  Public Property IP() As String
    Get
      Return MyIP
    End Get
    Set(value As String)
      If value <> MyIP Then
        If IsNothing(MyWebClient) = False Then
          MyWebClient.Dispose()
          MyWebClient = Nothing
        End If
      End If
      MyIP = value
    End Set
  End Property
  Private MyIP As String = String.Empty

  '================================================================================

  Public ReadOnly Property BinX() As Integer
    Get
      ReadSettings()
      Return MyBinX
    End Get
  End Property
  Private MyBinX As Integer = -1

  Public ReadOnly Property BinY() As Integer
    Get
      ReadSettings()
      Return MyBinY
    End Get
  End Property
  Private MyBinY As Integer = -1

  Public ReadOnly Property CoolerState() As Boolean
    Get
      ReadSettings()
      Return MyCoolerState
    End Get
  End Property
  Private MyCoolerState As Boolean = False

  Public ReadOnly Property CCDTemperature() As Double
    Get
      ReadSettings()
      Return MyCCDTemperature
    End Get
  End Property
  Private MyCCDTemperature As Double = Double.NaN

  Public ReadOnly Property CCDTemperatureSetpoint() As Double
    Get
      ReadSettings()
      Return MyCCDTemperatureSetpoint
    End Get
  End Property
  Private MyCCDTemperatureSetpoint As Double = Double.NaN

  Public ReadOnly Property CoolerPower() As Double
    Get
      ReadSettings()
      Return MyCoolerPower
    End Get
  End Property
  Private MyCoolerPower As Double = Double.NaN

  Public ReadOnly Property CameraSizeX() As Integer
    Get
      ReadSettings()
      Return MyCameraSizeX
    End Get
  End Property
  Private MyCameraSizeX As Integer = -1

  Public ReadOnly Property CameraSizeY() As Integer
    Get
      ReadSettings()
      Return MyCameraSizeY
    End Get
  End Property
  Private MyCameraSizeY As Integer = -1

  Public ReadOnly Property ElectronsPerADU() As Double
    Get
      ReadSettings()
      Return MyElectronsPerADU
    End Get
  End Property
  Private MyElectronsPerADU As Double = Double.NaN

  Public ReadOnly Property FanState() As eFanState
    Get
      ReadSettings()
      Return MyFanState
    End Get
  End Property
  Private MyFanState As eFanState = eFanState.Off

  Public ReadOnly Property FanPower() As Double
    Get
      ReadSettings()
      Return MyFanPower
    End Get
  End Property
  Private MyFanPower As Double = Double.NaN

  Public ReadOnly Property FullWellCapacity() As Double
    Get
      ReadSettings()
      Return MyFullWellCapacity
    End Get
  End Property
  Private MyFullWellCapacity As Double = Double.NaN

  Public ReadOnly Property AmbientTemperature() As Double
    Get
      ReadSettings()
      Return MyAmbientTemperature
    End Get
  End Property
  Private MyAmbientTemperature As Double = Double.NaN

  Public ReadOnly Property MaxADU() As Double
    Get
      ReadSettings()
      Return MyMaxADU
    End Get
  End Property
  Private MyMaxADU As Double = Double.NaN

  Public ReadOnly Property MaxBinX() As Integer
    Get
      ReadSettings()
      Return MyMaxBinX
    End Get
  End Property
  Private MyMaxBinX As Integer = -1

  Public ReadOnly Property MaxBinY() As Integer
    Get
      ReadSettings()
      Return MyMaxBinY
    End Get
  End Property
  Private MyMaxBinY As Integer = -1

  Public ReadOnly Property StartX() As Integer
    Get
      ReadSettings()
      Return MyStartX
    End Get
  End Property
  Private MyStartX As Integer = -1

  Public ReadOnly Property StartY() As Integer
    Get
      ReadSettings()
      Return MyStartY
    End Get
  End Property
  Private MyStartY As Integer = -1

  Public ReadOnly Property NumX() As Integer
    Get
      ReadSettings()
      Return MyNumX
    End Get
  End Property
  Private MyNumX As Integer = -1

  Public ReadOnly Property NumY() As Integer
    Get
      ReadSettings()
      Return MyNumY
    End Get
  End Property
  Private MyNumY As Integer = -1

  Public ReadOnly Property PixelSizeX() As Double
    Get
      ReadSettings()
      Return MyPixelSizeX
    End Get
  End Property
  Private MyPixelSizeX As Double = Double.NaN

  Public ReadOnly Property PixelSizeY() As Double
    Get
      ReadSettings()
      Return MyPixelSizeY
    End Get
  End Property
  Private MyPixelSizeY As Double = Double.NaN

  Public ReadOnly Property Overscan() As Boolean
    Get
      ReadSettings()
      Return MyOverscan
    End Get
  End Property
  Private MyOverscan As Boolean = False

  '================================================================================

  Public ReadOnly Property ImagerState() As eImagerState
    Get
      ReadSettings()
      Return MyImagerState
    End Get
  End Property
  Private MyImagerState As eImagerState = eImagerState.Error

  Public ReadOnly Property ImageReady() As Boolean
    Get
      ReadSettings()
      Return MyImageReady
    End Get
  End Property
  Private MyImageReady As Boolean = False

  Public ReadOnly Property FilterState() As eFilterState
    Get
      ReadSettings()
      Return MyFilterState
    End Get
  End Property
  Private MyFilterState As eFilterState = eFilterState.Error

  Public ReadOnly Property CurrentFilter() As Integer
    Get
      ReadSettings()
      Return MyCurrentFilter
    End Get
  End Property
  Private MyCurrentFilter As Integer = -1

  Public ReadOnly Property CurrentFilterName() As String
    Get
      ReadSettings()
      Return MyCurrentFilterName
    End Get
  End Property
  Private MyCurrentFilterName As String = String.Empty

    '================================================================================

    Public Sub SetROI(ByVal StartX As Integer, ByVal StartY As Integer, ByVal NumX As Integer, ByVal NumY As Integer)
        Dim Command As String = "http://" & MyIP & "/api/ImagerSetSettings.cgi?StartX=" & StartX.ToString.Trim & "&StartY=" & StartY.ToString.Trim & "&NumX=" & NumX.ToString.Trim & "&NumY=" & NumY.ToString.Trim
        GetContent(Command)
    End Sub

    Public Sub SetFanState(ByVal State As eFanState)
    Dim Command As String = "http://" & MyIP & "/api/ImagerSetSettings.cgi?FanState=" & CStr(CInt(State)).Trim
    GetContent(Command)
  End Sub

  Public Sub SetCoolerState(ByVal State As Boolean)
    Dim Command As String = "http://" & MyIP & "/api/ImagerSetSettings.cgi?CoolerState=" & CStr(IIf(State = True, "1", "0")).Trim
    GetContent(Command)
  End Sub

  Public Sub SetTemperatureSetPoint(ByVal Value As Double)
    Dim Command As String = "http://" & MyIP & "/api/ImagerSetSettings.cgi?CCDTemperatureSetpoint=" & CStr(Value).Replace(",", ".").Trim
    GetContent(Command)
  End Sub

  Public Sub SetBinning(ByVal X As Integer, ByVal Y As Integer)
    Dim Command As String = "http://" & MyIP & "/api/ImagerSetSettings.cgi?BinX=" & CStr(X).Trim & "&BinY=" & CStr(Y).Trim
    GetContent(Command)
  End Sub

    '================================================================================

    Public Sub SetFilterName(ByVal FilterIndex As Integer, ByVal Name As String)
        Dim Command As String = "http://" & MyIP & "/api/SetFilterName.cgi?"
        Command &= "Filter" & FilterIndex.ToString.Trim & "=" & Name
        GetContent(Command)
    End Sub

    Public Function ChangeFilter(ByVal NewFilter As Integer) As String
        Dim Command As String = "http://" & MyIP & "/api/ChangeFilter.cgi?"
        Command &= "NewPosition=" & NewFilter.ToString.Trim
        Return GetContent(Command)
    End Function

    Public Sub StartExpose(ByVal Duration As Double, ByVal FrameType As eFrameType, ByVal DateAndTime As DateTime)
    Dim Command As String = "http://" & MyIP & "/api/ImagerStartExposure.cgi?"
    Command &= "Duration=" & Duration.ToString.Trim.Replace(",", ".") & "&"
    Command &= "FrameType=" & CInt(FrameType).ToString.Trim & "&"
    Command &= "DateTime=" & Format(DateAndTime, "yyyy-MM-dd HH:mm:ss.fff").Trim.Replace(" ", "T")
    MyLastExposeDuration = Duration
    MyLastExposeStart = Now
    GetContent(Command)
  End Sub

  Public Sub ReadSettings()

    If (Now - LastUpdate).TotalMilliseconds > 1000 Then

            Dim QueryString As String = "http://" & MyIP & "/api/ImagerGetSettings.cgi?" & Join(GetImagerSettings, "&")
            Dim Answer As String() = Split(GetContent(QueryString), System.Environment.NewLine)
            Dim AnswerPtr As Integer = 0
            If Answer.Length <= 1 Then Exit Sub
            MyBinX = CInt(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyBinY = CInt(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyCoolerState = CBool(IIf(Answer(AnswerPtr).Trim = "0", False, True)) : AnswerPtr += 1
      MyCCDTemperature = Val(Answer(AnswerPtr).Trim.Replace(",", ".")) : AnswerPtr += 1
      MyCCDTemperatureSetpoint = Val(Answer(AnswerPtr).Trim.Replace(",", ".")) : AnswerPtr += 1
      MyCoolerPower = Val(Answer(AnswerPtr).Trim.Replace(",", ".")) : AnswerPtr += 1
      MyCameraSizeX = CInt(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyCameraSizeY = CInt(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyElectronsPerADU = Val(Answer(AnswerPtr).Trim.Replace(",", ".")) : AnswerPtr += 1
      MyFanState = GetFanState(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyFanPower = Val(Answer(AnswerPtr).Trim.Replace(",", ".")) : AnswerPtr += 1
      MyFullWellCapacity = Val(Answer(AnswerPtr).Trim.Replace(",", ".")) : AnswerPtr += 1
      MyAmbientTemperature = Val(Answer(AnswerPtr).Trim.Replace(",", ".")) : AnswerPtr += 1
      MyMaxADU = Val(Answer(AnswerPtr).Trim.Replace(",", ".")) : AnswerPtr += 1
      MyMaxBinX = CInt(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyMaxBinY = CInt(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyStartX = CInt(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyStartY = CInt(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyNumX = CInt(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyNumY = CInt(Answer(AnswerPtr).Trim) : AnswerPtr += 1
      MyPixelSizeX = Val(Answer(AnswerPtr).Trim.Replace(",", ".")) : AnswerPtr += 1
      MyPixelSizeY = Val(Answer(AnswerPtr).Trim.Replace(",", ".")) : AnswerPtr += 1
      MyOverscan = CBool(IIf(Answer(AnswerPtr).Trim = "0", False, True)) : AnswerPtr += 1

            System.Threading.Thread.Sleep(SleepBetweenQueries)
      Dim ImagerState As String = GetContent("http://" & MyIP & "/api/ImagerState.cgi")
      Select Case ImagerState.Trim
        Case "0" : MyImagerState = eImagerState.Idle
        Case "2" : MyImagerState = eImagerState.Exposing
        Case "3" : MyImagerState = eImagerState.ReadingOutCCD
        Case "5" : MyImagerState = eImagerState.Error
        Case Else : MyImagerState = eImagerState.Error
      End Select
      'Reset expose states in case of no expose running
      If MyImagerState <> eImagerState.Exposing Then
        MyLastExposeDuration = Double.NaN
        MyLastExposeStart = New Date(0)
      End If

            System.Threading.Thread.Sleep(SleepBetweenQueries)
      Dim ImagerImageReady As String = GetContent("http://" & MyIP & "/api/ImagerImageReady.cgi")
      If ImagerImageReady.Trim = "1" Then MyImageReady = True Else MyImageReady = False

            System.Threading.Thread.Sleep(SleepBetweenQueries)
      Dim FilterState As String = GetContent("http://" & MyIP & "/api/FilterState.cgi")
      Select Case FilterState.Trim
        Case "0" : MyFilterState = eFilterState.Idle
        Case "1" : MyFilterState = eFilterState.Moving
        Case "2" : MyFilterState = eFilterState.Error
        Case Else : MyImagerState = eImagerState.Error
      End Select

            System.Threading.Thread.Sleep(SleepBetweenQueries)
      Dim FilterSetting As String() = Split(GetContent("http://" & MyIP & "/api/GetFilterSetting.cgi?CurrentFilter&CurrentFilterName&Filter1Name&Filter2Name&Filter3Name&Filter4Name&Filter5Name&Filter6Name&Filter7Name&Filter8Name"), System.Environment.NewLine)
      If FilterSetting.Length >= 2 Then
        MyCurrentFilter = CInt(FilterSetting(0).Trim)
        MyCurrentFilterName = FilterSetting(1).Trim
      End If

      MyLastUpdate = Now

    End If

  End Sub

  Public Function DownloadFITS(ByVal LocalFile As String) As String

    If IsNothing(MyWebClient) = True Then
      MyWebClient = New Net.WebClient
    End If

    Try
      MyWebClient.DownloadFile(New Uri("http://" & MyIP & "/api/Imager.FIT"), LocalFile)
    Catch ex As Exception
      Return ex.Message
    End Try

    Return String.Empty

  End Function

  Private Function GetImagerSettings() As String()
    Dim RetVal As New List(Of String)
    RetVal.Add("BinX")
    RetVal.Add("BinY")
    RetVal.Add("CoolerState")
    RetVal.Add("CCDTemperature")
    RetVal.Add("CCDTemperatureSetpoint")
    RetVal.Add("CoolerPower")
    RetVal.Add("CameraXSize")
    RetVal.Add("CameraYSize")
    RetVal.Add("ElectronsPerADU")
    RetVal.Add("FanState")
    RetVal.Add("FanPower")
    RetVal.Add("FullWellCapacity")
    RetVal.Add("AmbientTemperature")
    RetVal.Add("MaxADU")
    RetVal.Add("MaxBinX")
    RetVal.Add("MaxBinY")
    RetVal.Add("StartX")
    RetVal.Add("StartY")
    RetVal.Add("NumX")
    RetVal.Add("NumY")
    RetVal.Add("PixelSizeX")
    RetVal.Add("PixelSizeY")
    RetVal.Add("OverScan")
    Return RetVal.ToArray
  End Function

  Private Function GetFanState(ByVal Answer As String) As eFanState
    Select Case Answer
      Case "0" : Return eFanState.Off
      Case "1" : Return eFanState.On
      Case "2" : Return eFanState.Auto
      Case Else : Return eFanState.Off
    End Select
  End Function

  Private Function GetContent(ByVal URL As String) As String

    If IsNothing(MyWebClient) = True Then
      MyWebClient = New Net.WebClient
    End If

    Dim Answer As String
    Try
      Answer = MyWebClient.DownloadString(New Uri(URL))
    Catch ex As Exception
      Answer = String.Empty
    End Try

    Return Answer

  End Function

End Class
