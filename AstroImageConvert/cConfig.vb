Option Explicit On
Option Strict On
Imports System.Reflection

Public Class cConfig

    Private Class Categories
        Public Const Cat1 As String = "1. Initial processing"
        Public Const Cat2 As String = "2. Sharpen"
        Public Const Cat3 As String = "3. Range compression"
        Public Const Cat_Store As String = "4. Storing"
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

    <System.ComponentModel.Category(Categories.Cat1)>
    <System.ComponentModel.DisplayName("1. Initial binning")>
    Public Property InitialBinning As Integer = 2

    <System.ComponentModel.Category(Categories.Cat1)>
    <System.ComponentModel.DisplayName("2. Initial binning - median outer removal")>
    Public Property InitialBinning_OuterRemoval As Integer = 1

    '──────────────────────────────────────────────────────────────────────────────────────────────

    <System.ComponentModel.Category(Categories.Cat2)>
    <System.ComponentModel.DisplayName("1. Sigma")>
    Public Property Sharpen_Sigma As Double = 3.0

    <System.ComponentModel.Category(Categories.Cat2)>
    <System.ComponentModel.DisplayName("2. Strength")>
    Public Property Sharpen_Strength As Double = 5.5

    <System.ComponentModel.Category(Categories.Cat2)>
    <System.ComponentModel.DisplayName("3. KernelSize")>
    Public Property Sharpen_KernelSize As Integer = 7

    '──────────────────────────────────────────────────────────────────────────────────────────────

    <System.ComponentModel.Category(Categories.Cat3)>
    <System.ComponentModel.DisplayName("1. Number of low samples to cut")>
    Public Property RangeCut_Low As Integer = 5000

    <System.ComponentModel.Category(Categories.Cat3)>
    <System.ComponentModel.DisplayName("2. Number of high samples to cut")>
    Public Property RangeCut_High As Integer = 5000

    '──────────────────────────────────────────────────────────────────────────────────────────────

    <System.ComponentModel.Category(Categories.Cat_Store)>
    <System.ComponentModel.DisplayName("1. Base file name")>
    Public Property FileName As String = "AstroImageConverter"

    <System.ComponentModel.Category(Categories.Cat_Store)>
    <System.ComponentModel.DisplayName("2. File type")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
    <ComponentModel.DefaultValue(eOutputFormat.PNG)>
    Public Property Format As eOutputFormat = eOutputFormat.PNG

    <System.ComponentModel.Category(Categories.Cat_Store)>
    <System.ComponentModel.DisplayName("3. File resolution [bit]")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
    <ComponentModel.DefaultValue(eBits.Bit16)>
    Public Property Bits As eBits = eBits.Bit16

    <System.ComponentModel.Category(Categories.Cat_Store)>
    <System.ComponentModel.DisplayName("4. Output gamma")>
    Public Property Gamma As Double = 0.2


    Public Property LastGeneratedFile As String = String.Empty

    Public Property FITSViewer As String = String.Empty

End Class
