Option Explicit On
Option Strict On
Imports System.Reflection

Public Class cConfig

    Private Class Categories
        Public Const Cat_Crop As String = "1. Crop"
        Public Const Cat_Bin As String = "2. Binning"
        Public Const Cat_Sharp As String = "3. Sharpen"
        Public Const Cat_RangeCompression As String = "4. Range compression"
        Public Const Cat_OutputProcessing As String = "5. Output processing"
        Public Const Cat_Storing As String = "6. Storing"
        Public Const Cat_Misc As String = "7. Misc"
    End Class

    Public Enum eBlur
        NoBlur = 0
        Blur3 = 3
        Blur5 = 5
    End Enum

    Public Enum eBits
        <ComponentModel.Description("8 Bit")>
        Bit8 = 8
        <ComponentModel.Description("16 Bit")>
        Bit16 = 16
    End Enum

    Public Enum eOutputFormat
        <ComponentModel.Description("JPEG")>
        JPEG
        <ComponentModel.Description("PNG")>
        PNG
    End Enum

    <System.ComponentModel.Category(Categories.Cat_Crop)>
    <System.ComponentModel.DisplayName("1. Left")>
    <System.ComponentModel.DefaultValue(20)>
    Public Property CropLeft As Integer = 20

    <System.ComponentModel.Category(Categories.Cat_Crop)>
    <System.ComponentModel.DisplayName("2. Right")>
    <System.ComponentModel.DefaultValue(20)>
    Public Property CropRight As Integer = 20

    <System.ComponentModel.Category(Categories.Cat_Crop)>
    <System.ComponentModel.DisplayName("3. Top")>
    <System.ComponentModel.DefaultValue(20)>
    Public Property CropTop As Integer = 20

    <System.ComponentModel.Category(Categories.Cat_Crop)>
    <System.ComponentModel.DisplayName("4. Bottom")>
    <System.ComponentModel.DefaultValue(20)>
    Public Property CropBottom As Integer = 20

    '──────────────────────────────────────────────────────────────────────────────────────────────

    <System.ComponentModel.Category(Categories.Cat_Bin)>
    <System.ComponentModel.DisplayName("1. Initial binning")>
    Public Property InitialBinning As Integer = 2

    <System.ComponentModel.Category(Categories.Cat_Bin)>
    <System.ComponentModel.DisplayName("2. Initial binning - median outer removal")>
    Public Property InitialBinning_OuterRemoval As Integer = 1

    '──────────────────────────────────────────────────────────────────────────────────────────────

    <System.ComponentModel.Category(Categories.Cat_Sharp)>
    <System.ComponentModel.DisplayName("1. Sigma")>
    <System.ComponentModel.DefaultValue(3.0)>
    Public Property Sharpen_Sigma As Double = 3.0

    <System.ComponentModel.Category(Categories.Cat_Sharp)>
    <System.ComponentModel.DisplayName("2. Strength")>
    <System.ComponentModel.DefaultValue(5.5)>
    Public Property Sharpen_Strength As Double = 5.5

    <System.ComponentModel.Category(Categories.Cat_Sharp)>
    <System.ComponentModel.DisplayName("3. KernelSize")>
    <System.ComponentModel.DefaultValue(7)>
    Public Property Sharpen_KernelSize As Integer = 7

    '──────────────────────────────────────────────────────────────────────────────────────────────

    <System.ComponentModel.Category(Categories.Cat_RangeCompression)>
    <System.ComponentModel.DisplayName("1. Number of low samples to cut")>
    <System.ComponentModel.DefaultValue(5000)>
    Public Property RangeCut_Low As Integer = 5000

    <System.ComponentModel.Category(Categories.Cat_RangeCompression)>
    <System.ComponentModel.DisplayName("2. Number of high samples to cut")>
    <System.ComponentModel.DefaultValue(5000)>
    Public Property RangeCut_High As Integer = 5000

    '──────────────────────────────────────────────────────────────────────────────────────────────

    <System.ComponentModel.Category(Categories.Cat_OutputProcessing)>
    <System.ComponentModel.DisplayName("1. File type")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
    <ComponentModel.DefaultValue(eOutputFormat.PNG)>
    Public Property Format As eOutputFormat = eOutputFormat.PNG

    <System.ComponentModel.Category(Categories.Cat_OutputProcessing)>
    <System.ComponentModel.DisplayName("2. File resolution [bit]")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
    <ComponentModel.DefaultValue(eBits.Bit16)>
    Public Property Bits As eBits = eBits.Bit16

    <System.ComponentModel.Category(Categories.Cat_OutputProcessing)>
    <System.ComponentModel.DisplayName("3. Output gamma")>
    <System.ComponentModel.DefaultValue(0.2)>
    Public Property Gamma As Double = 0.2

    '──────────────────────────────────────────────────────────────────────────────────────────────

    <System.ComponentModel.Category(Categories.Cat_Storing)>
    <System.ComponentModel.DisplayName("1. Use base file name")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(True)>
    Public Property Store_UseBaseFileName As Boolean = True

    <System.ComponentModel.Category(Categories.Cat_Storing)>
    <System.ComponentModel.DisplayName("2. Special file name")>
    <ComponentModel.DefaultValue("AstroImageConverter")>
    Public Property Store_SpecialFileName As String = "AstroImageConverter"

    <System.ComponentModel.Category(Categories.Cat_Storing)>
    <System.ComponentModel.DisplayName("3. Output path")>
    <ComponentModel.DefaultValue("")>
    Public Property Store_OutputPath As String = IO.Path.GetDirectoryName(Reflection.Assembly.GetEntryAssembly.Location)

    '──────────────────────────────────────────────────────────────────────────────────────────────

    <System.ComponentModel.Category(Categories.Cat_Misc)>
    <System.ComponentModel.DisplayName("1. FITS viewer to use")>
    Public Property FITSViewer As String = String.Empty

    <System.ComponentModel.Category(Categories.Cat_Misc)>
    <System.ComponentModel.DisplayName("2. Name of last stored file")>
    Public Property LastGeneratedFile As String = String.Empty

    '''<summary>Is any crop defined?</summary>
    '''<returns>TRUE if there is one, FALSE else.</returns>
    Public Function IsCrop() As Boolean
        If CropLeft > 0 Then Return True
        If CropRight > 0 Then Return True
        If CropBottom > 0 Then Return True
        If CropTop > 0 Then Return True
        Return False
    End Function

End Class
