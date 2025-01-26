Option Explicit On
Option Strict On

Public Class Form1

    Private Config As New cConfig

    Public Shared MyPath As String = IO.Path.GetDirectoryName(Reflection.Assembly.GetEntryAssembly.Location)

    Private WithEvents TIFF_IO As New ImageFileFormatSpecific.cTIFF
    Private WithEvents PNG_IO As New ImageFileFormatSpecific.cPNG
    Private WithEvents FITSReader As cFITSReader
    Private WithEvents FITSHeader As New cFITSHeaderParser

    Private IPP As cIntelIPP
    Private IPPPath As String = String.Empty
    Private DataContent(,) As Single = {}
    Private UInt16Data As AstroNET.Statistics

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim DD1 As New Ato.DragDrop(tbInputFile, True)
        IPPPath = cIntelIPP.SearchDLLToUse
        IPP = New cIntelIPP(IPPPath)
        FITSReader = New cFITSReader(IPPPath)
        UInt16Data = New AstroNET.Statistics(IPPPath)
        With lbExamples
            .Items.Clear()
            '.Items.Add("C:\Users\albus\Dropbox\Transfer iPhone\downloads\libtiffpic\pc260001.tif")
            '.Items.Add("C:\Users\albus\Dropbox\Transfer iPhone\downloads\libtiffpic\quad-jpeg.tif")
            '.Items.Add("C:\Users\albus\Dropbox\Astro\!Bilder\2020-02 - Königsdorf Orion.tif")
            '.Items.Add("\\192.168.100.10\dsc\DSS_Autosave\2024_11_29 - NGC371 Ha.tif")
            '.Items.Add("\\192.168.100.10\dsc\DSS_Autosave\2024_09_13 - NGC2024_Fix16.tif")
            .Items.Add("\\192.168.100.10\dsc\DSS_Autosave\2024_09_13 - NGC2024.tif")
            .Items.Add("\\192.168.100.10\dsc\2024_11_23\Capture\05_43_23\processed\Stack_9frames_72s.png")
            .Items.Add("\\192.168.100.10\dsc\DSS_Autosave\2024_08_26 - NGC6995 (Veil nebula).tif")
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

        Dim FileToLoad As String = tbInputFile.Text
        tbLog.Text = String.Empty

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
                    TIFF_IO.Load(FileToLoad, DataContent)
                Case "PNG"
                    PNG_IO.Load(FileToLoad, DataContent)
                Case "FIT", "FITS"
                    Dim DataStartPos As Integer = -1
                    FITSHeader = New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileToLoad, DataStartPos))
                    FITSReader.ReadIn(FileToLoad, DataContent)
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
            ImageOut = ImageProcessing.Binning.Mean_RemoveOuter_Single(DataContent, Config.InitialBinning, Config.InitialBinning_OuterRemoval)
            Log("  OK")
        Else
            ImageOut = DataContent.CreateCopy
        End If

        '──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        'Sharpen
        If Config.Sharpen_KernelSize > 0 Then
            Log("Sharpen ...")
            Dim ReturnCode As String = cOpenCvSharp.Sharpen(DataContent, Config.Sharpen_Sigma, Config.Sharpen_Strength, Config.Sharpen_KernelSize)
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
        Dim Keys As List(Of Long) = Stat.MonochromHistogram_Int.KeyList
        Keys.Sort()

        'Get the min cut-off
        Dim ImageOut_Min As Double = Double.NaN
        Dim PixelMin As ULong = 0
        If Config.RangeCut_High > 0 Then
            For Idx As Integer = 0 To Keys.Count - 1
                PixelMin += Stat.MonochromHistogram_Int(Keys(Idx))
                If PixelMin > Config.RangeCut_Low Then
                    ImageOut_Min = Keys(Idx)
                    Exit For
                End If
            Next Idx
        Else
            ImageOut_Min = Keys.First
        End If


        'Get the max cut-off
        Keys.Reverse()
        Dim ImageOut_Max As Double = Double.NaN
        Dim PixelMax As ULong = 0
        If Config.RangeCut_High > 0 Then
            For Idx As Integer = 0 To Keys.Count - 1
                PixelMax += Stat.MonochromHistogram_Int(Keys(Idx))
                If PixelMax > Config.RangeCut_High Then
                    ImageOut_Max = Keys(Idx)
                    Exit For
                End If
            Next Idx
        Else
            ImageOut_Max = Keys.First
        End If


        ImageOut_Min = (ImageOut_Min - ScaleB) / ScaleA
        ImageOut_Max = (ImageOut_Max - ScaleB) / ScaleA

        Log("Data MIN used: " & ImageOut_Min.ValRegIndep)
        Log("Data MAX used: " & ImageOut_Max.ValRegIndep)


        'TODO:
        'Set data MIN and MAX to x pct / ignore the 1000 pixel with the lower / highest value
        'Set a different color scema

        'Apply gamma and save
        Select Case Config.Bits
            Case 8
                Log("Storing 8 bit ...")
                Dim FinalImage_8Bit(ImageOut.GetUpperBound(0), ImageOut.GetUpperBound(1)) As Byte
                For Idx1 As Integer = 0 To ImageOut.GetUpperBound(0)
                    For Idx2 As Integer = 0 To ImageOut.GetUpperBound(1)
                        FinalImage_8Bit(Idx1, Idx2) = GetFinalValue8Bit(ImageOut(Idx1, Idx2), ImageOut_Min, ImageOut_Max, Config.Gamma)
                    Next Idx2
                Next Idx1
                Config.LastGeneratedFile = System.IO.Path.Combine(MyPath, Config.FileName & ".jpeg")
                Dim JPEGSave As New ImageFileFormatSpecific.cJPEG
                Log("Getting real 8 bit data ...")
                Dim ToStore As System.Windows.Media.Imaging.FormatConvertedBitmap = ImageFileFormatSpecific.GetConvertedBitmap(FinalImage_8Bit)
                Log("Saving real 8 bit data ...")
                JPEGSave.Save_8bpp(Config.LastGeneratedFile, ToStore)
            Case 16
                Log("Storing 16 bit ...")
                Dim FinalImage_16Bit(ImageOut.GetUpperBound(0), ImageOut.GetUpperBound(1)) As UInt16
                For Idx1 As Integer = 0 To ImageOut.GetUpperBound(0)
                    For Idx2 As Integer = 0 To ImageOut.GetUpperBound(1)
                        FinalImage_16Bit(Idx1, Idx2) = GetFinalValue16Bit(ImageOut(Idx1, Idx2), ImageOut_Min, ImageOut_Max, Config.Gamma)
                    Next Idx2
                Next Idx1
                Select Case Config.Format
                    Case "PNG"
                        Log("PNG ...")
                        Config.LastGeneratedFile = System.IO.Path.Combine(MyPath, Config.FileName & "Test.png")
                        Dim PNGSave As New ImageFileFormatSpecific.cPNG
                        PNGSave.Save_16bpp(Config.LastGeneratedFile, FinalImage_16Bit)
                    Case "JPEG"
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

    Private Function ValDictPixel(ByRef ValDict As SortedDictionary(Of Double, Integer)) As Integer
        Dim RetVal As Integer = 0
        For Each Entry As Double In ValDict.Keys
            RetVal += ValDict(Entry)
        Next Entry
        Return RetVal
    End Function

    Private Function GetFinalValue16Bit(ByVal Data As Double, ByVal DataMin As Double, ByVal DataMax As Double, ByVal Gamma As Double) As UInt16
        If Data < DataMin Then Data = DataMin
        If Data > DataMax Then Data = DataMax
        Dim ValueZeroToOne As Double = (((Data - DataMin) / (DataMax - DataMin))) ^ Gamma
        Return CType(UInt16.MaxValue * ValueZeroToOne, UInt16)
    End Function

    Private Function GetFinalValue8Bit(ByVal Data As Double, ByVal DataMin As Double, ByVal DataMax As Double, ByVal Gamma As Double) As Byte
        If Data < DataMin Then Data = DataMin
        If Data > DataMax Then Data = DataMax
        Dim ValueZeroToOne As Double = (((Data - DataMin) / (DataMax - DataMin))) ^ Gamma
        Return CType(Byte.MaxValue * ValueZeroToOne, Byte)
    End Function

    Private Sub lbExamples_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbExamples.SelectedIndexChanged
        tbInputFile.Text = CType(lbExamples.SelectedItem, String)
    End Sub

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

    Private Sub tsmiFile_FITSWork_Click(sender As Object, e As EventArgs) Handles tsmiFile_FITSWork.Click
        If System.IO.File.Exists(Config.LastGeneratedFile) Then
            If String.IsNullOrEmpty(Config.FITSViewer) = True Then
                Ato.Utils.StartWithItsEXE(Config.LastGeneratedFile)
            Else
                Process.Start(Config.FITSViewer, Config.LastGeneratedFile)
            End If
        End If
    End Sub

    Private Sub tsmiFile_Open_Click(sender As Object, e As EventArgs) Handles tsmiFile_Open.Click
        If System.IO.File.Exists(Config.LastGeneratedFile) Then
            Ato.Utils.StartWithItsEXE(Config.LastGeneratedFile)
        End If
    End Sub

    Private Sub tsmiFile_Exit_Click(sender As Object, e As EventArgs) Handles tsmiFile_Exit.Click
        End
    End Sub

    Private Sub tsmiFile_OpenOriginal_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenOriginal.Click
        If System.IO.File.Exists(tbInputFile.Text) Then
            Ato.Utils.StartWithItsEXE(tbInputFile.Text)
        End If
    End Sub

End Class
