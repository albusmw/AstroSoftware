Option Explicit On
Option Strict On

Public Class Form1

    Private Enum eViewerTool
        [Default]
        FITSView
    End Enum

    Private Config As New cConfig

    Private WithEvents TIFF_IO As New ImageFileFormatSpecific.cTIFF
    Private WithEvents PNG_IO As New ImageFileFormatSpecific.cPNG
    Private WithEvents FITSReader As cFITSReader
    Private WithEvents FITSHeader As New cFITSHeaderParser

    Private IPP As cIntelIPP
    Private IPPPath As String = String.Empty
    Private RawFileData(,) As Single = {}
    Private UInt16Data As AstroNET.Statistics
    Private AstroProcessing As New cAstroProcessing

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim DD1 As New cDragDrop(lbInputFiles, True)
        IPPPath = cIntelIPP.SearchDLLToUse
        IPP = New cIntelIPP(IPPPath)
        FITSReader = New cFITSReader(IPPPath)
        UInt16Data = New AstroNET.Statistics(IPPPath)
        Dim ListOfExamples As New List(Of String)
        With ListOfExamples
            .Clear()
            '.Add("C:\Users\albus\Dropbox\Transfer iPhone\downloads\libtiffpic\pc260001.tif")
            '.Add("C:\Users\albus\Dropbox\Transfer iPhone\downloads\libtiffpic\quad-jpeg.tif")
            '.Add("C:\Users\albus\Dropbox\Astro\!Bilder\2020-02 - Königsdorf Orion.tif")
            '.Add("\\192.168.100.10\dsc\DSS_Autosave\2024_11_29 - NGC371 Ha.tif")
            '.Add("\\192.168.100.10\dsc\DSS_Autosave\2024_09_13 - NGC2024_Fix16.tif")
            .Add("\\192.168.100.10\dsc\DSS_Autosave\2024_09_13 - NGC2024.tif")
            .Add("\\192.168.100.10\dsc\2024_11_23\Capture\05_43_23\processed\Stack_9frames_72s.png")
            .Add("\\192.168.100.10\dsc\DSS_Autosave\2024_08_26 - NGC6995 (Veil nebula).tif")
            '.Items.Add("\\192.168.100.10\dsc\2024_12_02\Abell 7 (Ancient Planetary Nebula)\Abell 7 (Ancient Planetary Nebula)\Abell 7 (Ancient Planetary Nebula)_00002.fits")
        End With

        'Display properties
        pgMain.SelectedObject = Config

        'Set FITS viewer
        Dim FileName As String = "FITSWork4.exe"
        Dim Locations As List(Of String) = Everything.GetExactMatch(FileName, Everything.GetSearchResult(FileName))
        If Locations.Count > 0 Then Config.FITSViewer = Locations(0)

    End Sub

    Private Sub tsmiProcess_Click(sender As Object, e As EventArgs) Handles tsmiProcess.Click
        For Each File As String In lbInputFiles.Items
            Convert(File)
        Next File
    End Sub

    Public Sub Convert(ByVal FileToLoad As String)

        'Prepare
        tbLog.Text = String.Empty
        Config.LastGeneratedFile = String.Empty

        '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        'Read file
        If System.IO.File.Exists(FileToLoad) = False Then
            Log("File <" & FileToLoad & "> NOT FOUND!")
            Exit Sub
        Else
            Log("Processing <" & FileToLoad & ">...")
            Dim Extension As String = System.IO.Path.GetExtension(FileToLoad).Trim("."c).ToUpper
            Select Case Extension
                Case "TIF", "TIFF"
                    'Load tags and get a rectangle to read if specified
                    TIFF_IO.LoadAllTags(FileToLoad)
                    For Each Entry As String In TIFF_IO.ReportAllTags
                        Log(Entry)
                    Next Entry
                    If Config.IsCrop = False Then
                        TIFF_IO.Load(FileToLoad, RawFileData)
                    Else
                        Dim ReadWidth As Integer = TIFF_IO.IMAGEWIDTH - Config.CropLeft - Config.CropRight
                        Dim ReadHeigth As Integer = TIFF_IO.IMAGELENGTH - Config.CropTop - Config.CropBottom
                        Dim ReadRect As New Rectangle(Config.CropLeft, Config.CropTop, ReadWidth, ReadHeigth)
                        RawFileData = TIFF_IO.LoadToSingle(FileToLoad, ReadRect)
                    End If
                Case "PNG"
                    PNG_IO.Load(FileToLoad, RawFileData)
                Case "FIT", "FITS"
                    'Decide which part to read
                    Dim DataStartPos As Integer = -1
                    FITSHeader = New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileToLoad, DataStartPos))
                    If Config.IsCrop = False Then
                        FITSReader.ReadIn(FileToLoad, RawFileData)
                    Else
                        Dim ROIWidth As Integer = FITSHeader.Width - Config.CropLeft - Config.CropRight
                        Dim ROIHeigth As Integer = FITSHeader.Height - Config.CropTop - Config.CropBottom
                        Dim ROI As New Rectangle(Config.CropLeft, Config.CropTop, ROIWidth, ROIHeigth)
                        RawFileData = FITSReader.ReadInUInt16(FileToLoad, True, ROI, True).ToSingle
                    End If

                Case Else
                    Log("File extension <" & Extension & "> NOT SUPPORTED!")
            End Select
            Log("  OK")
        End If

        '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        'Apply binning - mean with outer removal
        Dim ImageOut(,) As Single
        If Config.InitialBinning > 1 Then
            Log("Binning with factor <" & Config.InitialBinning & "> ...")
            ImageOut = ImageProcessing.Binning.Mean_RemoveOuter_Single(RawFileData, Config.InitialBinning, Config.InitialBinning_OuterRemoval)
            Log("  OK")
        Else
            ImageOut = RawFileData.CreateCopy
        End If

        '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        'Sharpen
        If Config.Sharpen_KernelSize > 0 Then
            Log("Sharpen ...")
            Dim ReturnCode As String = cOpenCvSharp.Sharpen(RawFileData, Config.Sharpen_Sigma, Config.Sharpen_Strength, Config.Sharpen_KernelSize)
            Log("  -> " & ReturnCode)
        End If

        Dim DataMin As Single
        Dim DataMax As Single
        IPP.MinMax(ImageOut, DataMin, DataMax)
        Log("Data MIN     : " & DataMin.ValRegIndep)
        Log("Data MAX     : " & DataMax.ValRegIndep)

        '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        'Calculate a UInt16 statistics
        Dim ScaleA As Double = CType(-UInt16.MaxValue, Double) / (DataMin - DataMax)
        Dim ScaleB As Double = -ScaleA * DataMin
        UInt16Data.DataProcessor_UInt16.LoadImageData(ImageOut.ToUInt16(ScaleA, ScaleB))
        Dim UInt16Stat As New AstroNET.Statistics(IPPPath)
        Dim Stat As AstroNET.Statistics.sStatistics = UInt16Data.ImageStatistics()

        'Get the min cut-off
        Dim ImageOut_Min As Double = AstroProcessing.GetCutOff(Stat.MonochromHistogram_Int, False, Config.RangeCut_Low)

        'Get the max cut-off
        Dim ImageOut_Max As Double = AstroProcessing.GetCutOff(Stat.MonochromHistogram_Int, True, Config.RangeCut_High)

        ImageOut_Min = (ImageOut_Min - ScaleB) / ScaleA
        ImageOut_Max = (ImageOut_Max - ScaleB) / ScaleA

        Log("Data MIN used: " & ImageOut_Min.ValRegIndep)
        Log("Data MAX used: " & ImageOut_Max.ValRegIndep)

        'TODO:
        'Set data MIN and MAX to x pct / ignore the 1000 pixel with the lower / highest value
        'Set a different color scema

        'Apply gamma and save
        Select Case Config.Bits
            Case cConfig.eBits.Bit8
                Log("Storing 8 bit ...")
                Dim FinalImage_8Bit(ImageOut.GetUpperBound(0), ImageOut.GetUpperBound(1)) As Byte
                For Idx1 As Integer = 0 To ImageOut.GetUpperBound(0)
                    For Idx2 As Integer = 0 To ImageOut.GetUpperBound(1)
                        FinalImage_8Bit(Idx1, Idx2) = AstroProcessing.GetPixelValue8Bit(ImageOut(Idx1, Idx2), ImageOut_Min, ImageOut_Max, Config.Gamma)
                    Next Idx2
                Next Idx1
                Config.LastGeneratedFile = GetFileToGenerate(FileToLoad)
                Dim JPEGSave As New ImageFileFormatSpecific.cJPEG
                Log("Getting real 8 bit data ...")
                Dim ToStore As System.Windows.Media.Imaging.FormatConvertedBitmap = ImageFileFormatSpecific.GetConvertedBitmap(FinalImage_8Bit)
                Log("Saving real 8 bit data ...")
                JPEGSave.Save_8bpp(Config.LastGeneratedFile, ToStore)
            Case cConfig.eBits.Bit16
                Log("Storing 16 bit ...")
                Dim FinalImage_16Bit(ImageOut.GetUpperBound(0), ImageOut.GetUpperBound(1)) As UInt16
                For Idx1 As Integer = 0 To ImageOut.GetUpperBound(0)
                    For Idx2 As Integer = 0 To ImageOut.GetUpperBound(1)
                        FinalImage_16Bit(Idx1, Idx2) = AstroProcessing.GetPixelValue16Bit(ImageOut(Idx1, Idx2), ImageOut_Min, ImageOut_Max, Config.Gamma)
                    Next Idx2
                Next Idx1
                Select Case Config.Format
                    Case cConfig.eOutputFormat.PNG
                        Log("PNG ...")
                        Config.LastGeneratedFile = GetFileToGenerate(FileToLoad)
                        Dim PNGSave As New ImageFileFormatSpecific.cPNG
                        PNGSave.Save_16bpp(Config.LastGeneratedFile, FinalImage_16Bit)
                    Case cConfig.eOutputFormat.JPEG
                        Log("JPEG - NOT SUPPORTED -")
                        'Not supported
                End Select

        End Select
        Log("Generated file <" & Config.LastGeneratedFile & ">")
        Log("-- FINISHED --")

        'Dim X As New cImageFromData
        'X.GenerateDisplayImage(DataContent, IPP)
        'X.OutputImage.BitmapToProcess.Save("C:\Test.png")

    End Sub

    Private Function GetFileToGenerate(ByVal File As String) As String
        Select Case Config.Format
            Case cConfig.eOutputFormat.JPEG
                Return System.IO.Path.Combine(Config.Store_OutputPath, System.IO.Path.GetFileNameWithoutExtension(File)) & ".jpeg"
            Case cConfig.eOutputFormat.PNG
                Return System.IO.Path.Combine(Config.Store_OutputPath, System.IO.Path.GetFileNameWithoutExtension(File)) & ".png"
        End Select
    End Function

    Private Function ValDictPixel(ByRef ValDict As SortedDictionary(Of Double, Integer)) As Integer
        Dim RetVal As Integer = 0
        For Each Entry As Double In ValDict.Keys
            RetVal += ValDict(Entry)
        Next Entry
        Return RetVal
    End Function

    Private Sub TIFF_IO_LogInfo(Text As String) Handles TIFF_IO.LogInfo
        Log(Text)
    End Sub

    Private Sub Log(ByVal Text As String)
        Text = Now.ForLogging & "|" & Text
        If String.IsNullOrEmpty(tbLog.Text) Then
            tbLog.Text = Text
        Else
            tbLog.Text &= System.Environment.NewLine & Text
        End If
        tbLog.SelectionStart = tbLog.Text.Length - 1
        tbLog.ScrollToCaret()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub tsmiProcess_QHY600Overscan_Click(sender As Object, e As EventArgs) Handles tsmiProcess_QHY600Overscan.Click
        With Config
            .CropBottom = 34
            .CropRight = 24
        End With
        pgMain.SelectedObject = Config
    End Sub

    Private Sub EqualCropToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EqualCropToolStripMenuItem.Click
        Dim Crop As Integer = -1
        Try
            Crop = CInt(InputBox("Equal crop [pixel]:", "Pixel crop", "0"))
        Catch ex As Exception
            Crop = -1
        End Try
        If Crop > -1 Then
            With Config
                .CropLeft = Crop
                .CropRight = Crop
                .CropTop = Crop
                .CropBottom = Crop
            End With
        End If
        pgMain.SelectedObject = Config
    End Sub

    Private Sub tsmiFile_OpenOriginal_Default_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenOriginal_Default.Click
        OpenFile(lbInputFiles.SelectedItem.ToString, eViewerTool.Default)
    End Sub

    Private Sub tsmiFile_OpenOriginal_FITSWork_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenOriginal_FITSWork.Click
        OpenFile(lbInputFiles.SelectedItem.ToString, eViewerTool.FITSView)
    End Sub

    Private Sub tsmiFile_OpenOutput_Default_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenOutput_Default.Click
        OpenFile(Config.LastGeneratedFile, eViewerTool.Default)
    End Sub

    Private Sub tsmiFile_OpenOutput_FITSWork_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenOutput_FITSWork.Click
        OpenFile(Config.LastGeneratedFile, eViewerTool.FITSView)
    End Sub

    Private Sub tsmiFile_ExploreOutput_Click(sender As Object, e As EventArgs) Handles tsmiFile_ExploreOutput.Click
        Utils.StartWithItsEXE(Config.Store_OutputPath)
    End Sub

    Private Sub tsmiFile_Exit_Click(sender As Object, e As EventArgs) Handles tsmiFile_Exit.Click
        End
    End Sub

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
    ' Functions
    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    '''<summary>Open file with a dedicated tool.</summary>
    '''<param name="FileName">File to open.</param>
    '''<param name="Tool">Tool to use.</param>
    Private Sub OpenFile(ByVal FileName As String, ByVal Tool As eViewerTool)
        If System.IO.File.Exists(FileName) Then
            Select Case Tool
                Case eViewerTool.Default
                    Utils.StartWithItsEXE(FileName)
                Case eViewerTool.FITSView
                    If String.IsNullOrEmpty(Config.FITSViewer) = False Then
                        Process.Start(Config.FITSViewer, Chr(34) & FileName & Chr(34))
                    Else
                        MsgBox("FITSWork not found under <" & Config.FITSViewer & ">!", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "FITSWork not found")
                    End If
            End Select
        Else
            MsgBox("File <" & FileName & "> does not exist!", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "File not found")
        End If
    End Sub

End Class
