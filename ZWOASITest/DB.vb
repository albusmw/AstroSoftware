Option Explicit On
Option Strict On

Public Class M
    '''<summary>DB that holds all relevant information.</summary>
    Public Shared WithEvents DB As New cDB
End Class

'''<summary>Capture data class.</summary>
Public Class cCaptureDB
    '''<summary>Number of bytes per sample.</summary>
    Public ReadOnly Property BytePerSample As Integer = 2
    '''<summary>Helper to pin the buffer in order to access it via direct memory operations.</summary>
    Public Pinner As New cDLLDynCall.cPinHandler
    '''<summary>The image buffer itself.</summary>
    Public ImgBufferer_UInt16() As UInt16 = Array.Empty(Of UShort)()
    '''<summary>Pointer to the buffer.</summary>
    Public ImgBuffererPtr As IntPtr = Nothing
    '''<summary>ROI which was used to generate the image.</summary>
    Public ROI As Rectangle = Nothing
    '''<summary>Current size of the buffer [sample].</summary>
    Public ReadOnly Property BufferSample() As Integer
        Get
            If IsNothing(ImgBufferer_UInt16) Then Return 0
            Return CInt(ImgBufferer_UInt16.LongLength)
        End Get
    End Property
    '''<summary>Current size of the buffer [byte].</summary>
    Public ReadOnly Property BufferBytes() As Integer
        Get
            If IsNothing(ImgBufferer_UInt16) Then Return 0
            Return CInt(ImgBufferer_UInt16.LongLength * BytePerSample)
        End Get
    End Property
End Class

'''<summary>Database holding relevant information.</summary>
Public Class cDB

    Public Enum eCaptureMode
        <System.ComponentModel.Description("Still")>
        Still
        <System.ComponentModel.Description("Video")>
        Video
    End Enum

    Const Cat_CaptureControl As String = "1. Capture control"
    Const Cat_FlowControl As String = "2. Flow control"
    Const Cat_ValueStepping As String = "3. Value adjustment"
    Const Cat_GraphOut As String = "4. Graph display"
    Const Cat_ImageOut As String = "5. Image display"

    '===============================================================================================================

    '''<summary>Capture mode.</summary>
    <ComponentModel.Category(Cat_CaptureControl)>
    <ComponentModel.DisplayName("a) Capture mode")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
    <ComponentModelEx.EnumDefaultValue(eCaptureMode.Video)>
    Public Property Capture_Mode As eCaptureMode = eCaptureMode.Video

    '''<summary>Exposure time [s].</summary>
    <ComponentModel.Category(Cat_CaptureControl)>
    <ComponentModel.DisplayName("b) Exposure time")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.DoublePropertyConverter_s))>
    <ComponentModel.DefaultValue(0.001)>
    Public Property Capture_ExposureTime As Double = 0.02

    <ComponentModel.Browsable(False)>
    Public ReadOnly Property Capture_ExposureTime_us As Integer
        Get
            Return CInt(Capture_ExposureTime * 1000000)
        End Get
    End Property

    '''<summary>Gain.</summary>
    <ComponentModel.Category(Cat_CaptureControl)>
    <ComponentModel.DisplayName("c) Gain")>
    <ComponentModel.DefaultValue(20)>
    Public Property Capture_Gain As Integer = 0

    '''<summary>Gamma camera.</summary>
    <ComponentModel.Category(Cat_CaptureControl)>
    <ComponentModel.DisplayName("d) Gamma camera")>
    <ComponentModel.DefaultValue(50)>
    Public Property Capture_Gamma As Integer = 50

    '''<summary>ROI area.</summary>
    <ComponentModel.Category(Cat_CaptureControl)>
    <ComponentModel.DisplayName("e) ROI area")>
    Public Property ROISet As Rectangle = Nothing

    <ComponentModel.Category(Cat_CaptureControl)>
    <ComponentModel.DisplayName("f) Image type")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
    <ComponentModelEx.EnumDefaultValue(ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_RAW16)>
    Public ReadOnly Property ImgType As ZWO.ASICameraDll.ASI_IMG_TYPE
        Get
            Return MyImgType
        End Get
    End Property
    Public MyImgType As ZWO.ASICameraDll.ASI_IMG_TYPE = ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_RAW16

    '===============================================================================================================

    <ComponentModel.Category(Cat_FlowControl)>
    <ComponentModel.DisplayName("a) Calculate statistics?")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(True)>
    Public Property Flow_CalcStatistics As Boolean = True

    <ComponentModel.Category(Cat_FlowControl)>
    <ComponentModel.DisplayName("b) Display statistics?")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(True)>
    Public Property Flow_DisplayStatistics As Boolean = True

    <ComponentModel.Category(Cat_FlowControl)>
    <ComponentModel.DisplayName("c) Display image?")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(True)>
    Public Property Flow_DisplayImage As Boolean = True

    <ComponentModel.Category(Cat_FlowControl)>
    <ComponentModel.DisplayName("d) Store image?")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(False)>
    Public Property Flow_StoreImage As Boolean = False

    '===============================================================================================================

    <ComponentModel.Category(Cat_ValueStepping)>
    <ComponentModel.DisplayName("a) Value step size - Exposure")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.DoublePropertyConverter_s))>
    <ComponentModel.DefaultValue(0.00001)>
    Public Property ValueStep_Exposure As Double = 0.00001

    <ComponentModel.Category(Cat_ValueStepping)>
    <ComponentModel.DisplayName("b) Value step size - Gain")>
    <ComponentModel.DefaultValue(5)>
    Public Property ValueStep_Gain As Integer = 5

    <ComponentModel.Category(Cat_ValueStepping)>
    <ComponentModel.DisplayName("c) Value step size - Gamma")>
    <ComponentModel.DefaultValue(0.01)>
    Public Property ValueStep_Gamma As Double = 0.01

    '===============================================================================================================

    <ComponentModel.Category(Cat_GraphOut)>
    <ComponentModel.DisplayName("a) X axis scaling")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
    <ComponentModel.DefaultValue(GetType(cZEDGraph.eScalingMode), "Automatic")>
    Public Property GraphOut_AutoAlways As cZEDGraph.eScalingMode = cZEDGraph.eScalingMode.Automatic

    '===============================================================================================================

    '''<summary>Display an ROI part only?</summary>
    <ComponentModel.Category(Cat_ImageOut)>
    <ComponentModel.DisplayName("a) Display ROI part only")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(False)>
    Public Property ImageOut_DisplayPart As Boolean = False

    '''<summary>ROI part definition.</summary>
    <ComponentModel.Category(Cat_ImageOut)>
    <ComponentModel.DisplayName("b) Part ROI")>
    Public Property ImageOut_ROI As New Rectangle(0, 0, 0, 0)

    '''<summary>Picture interpolation.</summary>
    <ComponentModel.Category(Cat_ImageOut)>
    <ComponentModel.DisplayName("c) Picture interpolation")>
    Public Property ImageOut_Interpolation As Drawing2D.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor

    '===============================================================================================================

    '''<summary>EXE path.</summary>
    Public ReadOnly MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
    '''<summary>Intel IPP.</summary>
    Public IPP As cIntelIPP
    '''<summary>Statistics calculation.</summary>
    Public StatCalc As AstroNET.Statistics

    '''<summary>All related to the capture process.</summary>
    Public CaptureDB As New cCaptureDB

    '''<summary>Single frame statistics.</summary>
    Public SingleStat As AstroNET.Statistics.sStatistics
    '''<summary>Accumulated statistics.</summary>
    Public LoopStat As AstroNET.Statistics.sStatistics

    '''<summary>Image generation class.</summary>
    Public ImageFromData As New cImageFromData

    '''<summary>Log content.</summary>
    Public Log_Generic As New Text.StringBuilder

    Public Sub EnsureCorrectBuffer()
        Dim RequiredBytes As Long = GetBufferSize(ROISet, MyImgType)
        If CaptureDB.BufferBytes <> RequiredBytes Then
            If IsNothing(CaptureDB.ImgBuffererPtr) = False Then CaptureDB.Pinner.ForceUnpinAll()
            ReDim CaptureDB.ImgBufferer_UInt16((ROISet.Height * ROISet.Width) - 1)
            CaptureDB.ImgBuffererPtr = CaptureDB.Pinner.Pin(CaptureDB.ImgBufferer_UInt16)
        End If
    End Sub

    '''<summary>Calculate the required buffer size [byte].</summary>
    Private Function GetBufferSize(ByVal ROI As Rectangle, ByVal ImgType As ZWO.ASICameraDll.ASI_IMG_TYPE) As Integer
        Dim RetVal As Integer = ROI.Width * ROI.Height
        Select Case ImgType
            Case ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_RAW8, ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_Y8
                Return RetVal
            Case ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_RAW16
                Return RetVal * 2
            Case ZWO.ASICameraDll.ASI_IMG_TYPE.ASI_IMG_RGB24
                Return RetVal * 3
            Case Else
                Return 0
        End Select
    End Function

End Class
