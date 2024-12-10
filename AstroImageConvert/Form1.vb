Option Explicit On
Option Strict On

Public Class Form1

    Public Shared MyPath As String = IO.Path.GetDirectoryName(Reflection.Assembly.GetEntryAssembly.Location)
    Dim IPP As New cIntelIPP
    Dim DataContent(,) As Double = {}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DD1 As New Ato.DragDrop(tbTIFFFile, True)
        With lbExamples
            .Items.Clear()
            .Items.Add("C:\Users\albus\Dropbox\Transfer iPhone\downloads\libtiffpic\pc260001.tif")
            .Items.Add("C:\Users\albus\Dropbox\Transfer iPhone\downloads\libtiffpic\quad-jpeg.tif")
            .Items.Add("C:\Users\albus\Dropbox\Astro\!Bilder\2020-02 - Königsdorf Orion.tif")
            .Items.Add("\\192.168.100.10\dsc\DSS_Autosave\2024_11_29 - NGC371 Ha.tif")
            .Items.Add("\\192.168.100.10\dsc\DSS_Autosave\2024_09_13 - NGC2024_Fix16.tif")
        End With
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Dim TIFF_IO As New ImageFileFormatSpecific.cTIFF
        If System.IO.File.Exists(tbTIFFFile.Text) Then
            tbLog.Text = "Processing ..."
            TIFF_IO.LoadTIFF(tbTIFFFile.Text, DataContent)
            tbLog.Text = "OK"
        End If

        Dim ImageOut(,) As Double = ImageProcessing.BinningMedian(DataContent, 4)
        Dim ImageOut_Min As Double = ImageOut.Minimum
        Dim ImageOut_Max As Double = ImageOut.Maximum
        Dim FinalImage(ImageOut.GetUpperBound(0), ImageOut.GetUpperBound(1)) As UInt16
        For Idx1 As Integer = 0 To ImageOut.GetUpperBound(0)
            For Idx2 As Integer = 0 To ImageOut.GetUpperBound(1)
                Dim NewValue As Double = (((ImageOut(Idx1, Idx2) - ImageOut_Min) / (ImageOut_Max - ImageOut_Min))) ^ 0.2
                FinalImage(Idx1, Idx2) = CUShort(UInt16.MaxValue * NewValue)
            Next Idx2
        Next Idx1

        Dim FileOut As String = System.IO.Path.Combine(MyPath, "Test.png")
        Dim PNGSave As New ImageFileFormatSpecific.cPNG
        PNGSave.Save_16bpp(FileOut, FinalImage)
        MsgBox("OK")

        'Dim X As New cImageFromData
        'X.GenerateDisplayImage(DataContent, IPP)
        'X.OutputImage.BitmapToProcess.Save("C:\Test.png")

    End Sub

    Private Sub lbExamples_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbExamples.SelectedIndexChanged
        tbTIFFFile.Text = CType(lbExamples.SelectedItem, String)
    End Sub

End Class
