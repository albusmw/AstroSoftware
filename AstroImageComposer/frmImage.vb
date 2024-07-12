Option Explicit On
Option Strict On

'''<summary>Image display form holding image data, the image itself and the statistics.</summary>
Public Class frmImage

    '''<summary>Unique GUID of this form.</summary>
    Public ReadOnly Property GUID As String
        Get
            Return MyGUID
        End Get
    End Property
    Private MyGUID As String = String.Empty

    '''<summary>The image data itself.</summary>
    Public ImgData As AstroNET.Statistics
    '''<summary>The image statistics based on the data.</summary>
    Public ImgStat As AstroNET.Statistics.sStatistics
    '''<summary>The image generated from the image data.</summary>
    Public ImageFromData As New cImageFromData

    'This elements are self-coded and will not work in 64-bit from the toolbox ...
    Private WithEvents pbMain As New PictureBoxEx

    '''<summary>FITS header (if there was any).</summary>
    Public FITSHeader As cFITSHeaderParser
    '''<summary>Data start position within the file.</summary>
    Public DataStartPos As Integer = 0

    '''<summary>Floating-point coordinated of the mouse within the picture.</summary>
    Private FloatCenter As Drawing.PointF

    '''<summary>Use Intel IPP for reading.</summary>
    Public Property UseIPP As Boolean = False
    '''<summary>Force direct read (do not use scaling, ...).</summary>
    Public Property ForceDirect As Boolean = True

    Private Sub frmImage_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Load custom controls - main image (must be done due to 64-bit IDE limitation)
        Me.Controls.Add(pbMain)
        With pbMain
            .Dock = DockStyle.Fill
            .InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            .SizeMode = PictureBoxSizeMode.Zoom
            .BackColor = Color.Purple
            .ContextMenuStrip = cmsMain
        End With
        MyGUID = (New Guid).ToString
    End Sub

    '''<summary>Moving the mouse changed the point to zoom in.</summary>
    Private Sub pbMain_MouseMove(sender As Object, e As MouseEventArgs) Handles pbMain.MouseMove
        FloatCenter = pbMain.ScreenCoordinatesToImageCoordinates
        Dim DataValue As Double = ImgData.GetDataValue(FloatCenter)
        tssl_Coord.Text = "Coord: <" & FloatCenter.X.ValRegIndep("0") & ":" & FloatCenter.Y.ValRegIndep("0") & ">: " & DataValue.ValRegIndep
        'CalculateZoomParameters()
        'ShowDetails()
    End Sub

    '''<summary>Load data from the given image file.</summary>
    '''<param name="FileName">File to load.</param>
    '''<returns>Error code on error or nothing on no error.</returns>
    Public Function LoadImage(ByVal FileName As String) As String

        'Date container
        ImgData = New AstroNET.Statistics(DB.IPP)
        ImgData.ResetAllProcessors()

        'FITS reader
        Dim FITSReader As New cFITSReader
        FITSHeader = New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileName, DataStartPos))

        Select Case FITSHeader.BitPix
            Case 8
                ImgData.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt8(FileName, UseIPP)
            Case 16
                With ImgData.DataProcessor_UInt16
                    .ImageData(0).Data = FITSReader.ReadInUInt16(FileName, UseIPP, ForceDirect)
                    If FITSHeader.NAXIS3 > 1 Then
                        For Idx As Integer = 1 To FITSHeader.NAXIS3 - 1
                            DataStartPos += CInt(.ImageData(Idx - 1).Length * FITSHeader.BytesPerSample)        'move to next plane
                            .ImageData(Idx).Data = FITSReader.ReadInUInt16(FileName, DataStartPos, UseIPP, ForceDirect)
                        Next Idx
                    End If
                End With
            Case 32
                ImgData.DataProcessor_Int32.ImageData = FITSReader.ReadInInt32(FileName, UseIPP)
            Case -32
                With ImgData.DataProcessor_Float32
                    .ImageData(0).Data = FITSReader.ReadInFloat32(FileName, UseIPP)
                    If FITSHeader.NAXIS3 > 1 Then
                        For Idx As Integer = 1 To FITSHeader.NAXIS3 - 1
                            DataStartPos += CInt(.ImageData(Idx - 1).Length * FITSHeader.BytesPerSample)        'move to next plane
                            .ImageData(Idx).Data = FITSReader.ReadInFloat32(FileName, UseIPP)
                        Next Idx
                    End If
                End With
            Case Else
                Return "!!! File format <" & FITSHeader.BitPix.ToString.Trim & "> not yet supported!"
        End Select

        'Calculate statistis
        ImgStat = ImgData.ImageStatistics()

        'Set defaults
        SetDefaultsForNewImage()

        'Display the calculated image data
        DisplayImageData()

        'Set window title
        Me.TabText = FileName

        Return String.Empty

    End Function

    Public Sub DisplayImageData()

        'Generate display image
        ImageFromData.GenerateDisplayImage(ImgData.DataProcessor_UInt16.ImageData(0).Data, DB.IPP)

        'Display image
        ImageFromData.OutputImage.UnlockBits()
        pbMain.BackColor = ImageFromData.ColorMap_BackColor
        pbMain.Image = ImageFromData.OutputImage.BitmapToProcess

    End Sub

    Private Sub SetDefaultsForNewImage()
        ImageFromData.ColorMap_LowerEnd = ImgStat.MonoStatistics_Int.Min.Key
        ImageFromData.ColorMap_UpperEnd = ImgStat.MonoStatistics_Int.Max.Key
    End Sub

    Private Sub tsmi_ThisLOWEnd_Click(sender As Object, e As EventArgs) Handles tsmi_ThisLOWEnd.Click
        ImageFromData.ColorMap_LowerEnd = ImgData.GetDataValue(FloatCenter)
        DB.GetMyModifierForm(GUID).ReactOnChangedProperty()
    End Sub

    Private Sub tsmi_ThisUPPEREnd_Click(sender As Object, e As EventArgs) Handles tsmi_ThisUPPEREnd.Click
        ImageFromData.ColorMap_UpperEnd = ImgData.GetDataValue(FloatCenter)
        DB.GetMyModifierForm(GUID).ReactOnChangedProperty()
    End Sub

    Private Sub tsmi_FromHisto_Click(sender As Object, e As EventArgs) Handles tsmi_FromHisto.Click
        Dim HistoForm As frmGraph = DB.GetMyHistoForm(GUID)
        If IsNothing(HistoForm) Then Exit Sub
        Dim GP As ZedGraph.GraphPane = HistoForm.MyZEDGraph.MainGraph.GraphPane
        ImageFromData.ColorMap_LowerEnd = GP.XAxis.Scale.Min
        ImageFromData.ColorMap_UpperEnd = GP.XAxis.Scale.Max
        DB.GetMyModifierForm(GUID).ReactOnChangedProperty()
    End Sub

End Class