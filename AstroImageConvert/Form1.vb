Option Explicit On
Option Strict On

Public Class Form1

    Public Shared MyPath As String = IO.Path.GetDirectoryName(Reflection.Assembly.GetEntryAssembly.Location)

    Private WithEvents TIFF_IO As New ImageFileFormatSpecific.cTIFF
    Private WithEvents FITSReader As cFITSReader
    Private WithEvents FITSHeader As New cFITSHeaderParser

    Private IPPPath As String = String.Empty
    Private DataContent(,) As Double = {}

    Private Binning As Integer = 3
    Private LastGeneratedFile As String = String.Empty

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DD1 As New Ato.DragDrop(tbInputFile, True)
        IPPPath = cIntelIPP.SearchDLLToUse
        FITSReader = New cFITSReader(IPPPath)
        With lbExamples
            .Items.Clear()
            .Items.Add("C:\Users\albus\Dropbox\Transfer iPhone\downloads\libtiffpic\pc260001.tif")
            .Items.Add("C:\Users\albus\Dropbox\Transfer iPhone\downloads\libtiffpic\quad-jpeg.tif")
            .Items.Add("C:\Users\albus\Dropbox\Astro\!Bilder\2020-02 - Königsdorf Orion.tif")
            .Items.Add("\\192.168.100.10\dsc\DSS_Autosave\2024_11_29 - NGC371 Ha.tif")
            .Items.Add("\\192.168.100.10\dsc\DSS_Autosave\2024_09_13 - NGC2024_Fix16.tif")
            .Items.Add("\\192.168.100.10\dsc\2024_12_02\Abell 7 (Ancient Planetary Nebula)\Abell 7 (Ancient Planetary Nebula)\Abell 7 (Ancient Planetary Nebula)_00002.fits")
        End With
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click

        Dim FileToLoad As String = tbInputFile.Text

        'Read file
        If System.IO.File.Exists(FileToLoad) = False Then
            Log("File <" & FileToLoad & "> NOT FOUND!")
            Exit Sub
        Else
            Log("Processing <" & FileToLoad & ">...")
            Dim Extension As String = System.IO.Path.GetExtension(FileToLoad).Trim("."c).ToUpper
            Select Case Extension
                Case "TIF", "TIFF"
                    TIFF_IO.LoadTIFF(FileToLoad, DataContent)
                Case "FIT", "FITS"
                    Dim DataStartPos As Integer = -1
                    FITSHeader = New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileToLoad, DataStartPos))
                    FITSReader.ReadIn(FileToLoad, DataContent)
                Case Else
                    Log("File extension <" & Extension & "> NOT SUPPORTED!")
            End Select
            Log("OK")
        End If

        'Apply median filter
        Log("Binning with factor <" & Binning & "> ...")
        Dim ImageOut(,) As Double = ImageProcessing.Binning.ToDouble_Mean_RemoveOuter(DataContent, Binning, 1)
        Dim ImageOut_Min As Double = ImageOut.Minimum
        Dim ImageOut_Max As Double = ImageOut.Maximum

        'TODO:
        'Set data MIN and MAX to x pct / ignore the 1000 pixel with the lower / highest value
        'Set a different color scema

        'Apply gamma and save
        Dim Format As String = "PNG"
        Dim Bits As Integer = 16
        Dim Gamma As Double = 0.2
        Select Case Bits
            Case 8
                Log("Storing 8 bit ...")
                Dim FinalImage_8Bit(ImageOut.GetUpperBound(0), ImageOut.GetUpperBound(1)) As Byte
                For Idx1 As Integer = 0 To ImageOut.GetUpperBound(0)
                    For Idx2 As Integer = 0 To ImageOut.GetUpperBound(1)
                        FinalImage_8Bit(Idx1, Idx2) = GetFinalValue8Bit(ImageOut(Idx1, Idx2), ImageOut_Min, ImageOut_Max, Gamma)
                    Next Idx2
                Next Idx1
                LastGeneratedFile = System.IO.Path.Combine(MyPath, "Test.jpeg")
                Dim JPEGSave As New ImageFileFormatSpecific.cJPEG
                Log("Getting real 8 bit data ...")
                Dim ToStore As System.Windows.Media.Imaging.FormatConvertedBitmap = ImageFileFormatSpecific.GetConvertedBitmap(FinalImage_8Bit)
                Log("Saving real 8 bit data ...")
                JPEGSave.Save_8bpp(LastGeneratedFile, ToStore)
            Case 16
                Log("Storing 16 bit ...")
                Dim FinalImage_16Bit(ImageOut.GetUpperBound(0), ImageOut.GetUpperBound(1)) As UInt16
                For Idx1 As Integer = 0 To ImageOut.GetUpperBound(0)
                    For Idx2 As Integer = 0 To ImageOut.GetUpperBound(1)
                        FinalImage_16Bit(Idx1, Idx2) = GetFinalValue16Bit(ImageOut(Idx1, Idx2), ImageOut_Min, ImageOut_Max, Gamma)
                    Next Idx2
                Next Idx1
                Select Case Format
                    Case "PNG"
                        Log("PNG ...")
                        LastGeneratedFile = System.IO.Path.Combine(MyPath, "Test.png")
                        Dim PNGSave As New ImageFileFormatSpecific.cPNG
                        PNGSave.Save_16bpp(LastGeneratedFile, FinalImage_16Bit)
                    Case "JPEG"
                        Log("JPEG - NOT SUPPORTED -")
                        'Not supported
                End Select

        End Select
        Log("Generated file <" & LastGeneratedFile & ">")
        Log("-- FINISHED --")

        'Dim X As New cImageFromData
        'X.GenerateDisplayImage(DataContent, IPP)
        'X.OutputImage.BitmapToProcess.Save("C:\Test.png")

    End Sub

    Private Function GetFinalValue16Bit(ByVal Data As Double, ByVal DataMin As Double, ByVal DataMax As Double, ByVal Gamma As Double) As UInt16
        Dim ValueZeroToOne As Double = (((Data - DataMin) / (DataMax - DataMin))) ^ Gamma
        Return CType(UInt16.MaxValue * ValueZeroToOne, UInt16)
    End Function

    Private Function GetFinalValue8Bit(ByVal Data As Double, ByVal DataMin As Double, ByVal DataMax As Double, ByVal Gamma As Double) As Byte
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

    Private Sub btnOpenGeneratedFile_Click(sender As Object, e As EventArgs) Handles btnOpenGeneratedFile.Click
        If System.IO.File.Exists(LastGeneratedFile) Then
            Ato.Utils.StartWithItsEXE(LastGeneratedFile)
        End If
    End Sub

End Class
