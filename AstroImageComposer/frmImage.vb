Option Explicit On
Option Strict On
Imports Microsoft.VisualBasic.ApplicationServices

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
    Private PictureCoord As Drawing.PointF
    '''<summary>Fixed-point data point (respecting cut) of the mouse within the picture.</summary>
    Private DataCoord As Drawing.Point

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
        If IsNothing(ImgData) Then Exit Sub
        PictureCoord = pbMain.ScreenCoordinatesToImageCoordinates
        DataCoord = New Point(CInt(PictureCoord.X + ImageFromData.Cut_Left), CInt(PictureCoord.Y + ImageFromData.Cut_Top))
        Dim DataValue As Double = ImgData.GetDataValue(DataCoord.X, DataCoord.Y)
        tssl_Coord.Text = "Coord: <" & DataCoord.X.ValRegIndep("0") & ":" & DataCoord.Y.ValRegIndep("0") & ">: " & DataValue.ValRegIndep
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
        Dim FITSReader As New cFITSReader(DB.IPPPath)
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

        'Entering conditions
        If IsNothing(ImgData) Then Exit Sub

        'Calculate ROI to pass
        Dim ROI As New Rectangle(0, 0, 0, 0)
        If (ImageFromData.Cut_Left > 0) Or (ImageFromData.Cut_Right > 0) Or (ImageFromData.Cut_Top > 0) Or (ImageFromData.Cut_Bottom > 0) Then
            Dim NewWidth As Integer = ImgData.NAXIS1 - ImageFromData.Cut_Left - ImageFromData.Cut_Right
            Dim NewHeigth As Integer = ImgData.NAXIS2 - ImageFromData.Cut_Top - ImageFromData.Cut_Bottom
            ROI = New Rectangle(ImageFromData.Cut_Left, ImageFromData.Cut_Top, NewWidth, NewHeigth)
        End If

        'Generate display image
        ImageFromData.GenerateDisplayImage(ImgData, ROI, ImgStat, DB.IPP)

        'Display image
        ImageFromData.OutputImage.UnlockBits()
        pbMain.BackColor = ImageFromData.CM_BackColor
        pbMain.Image = ImageFromData.OutputImage.BitmapToProcess

    End Sub

    Private Sub tsmi_ThisLOWEnd_Click(sender As Object, e As EventArgs) Handles tsmi_ThisLOWEnd.Click
        ImageFromData.CM_LowerEnd_Absolute = ImgData.GetDataValue(PictureCoord)
        DB.GetMyModifierForm(GUID).ReactOnChangedProperty()
    End Sub

    Private Sub tsmi_ThisUPPEREnd_Click(sender As Object, e As EventArgs) Handles tsmi_ThisUPPEREnd.Click
        ImageFromData.CM_UpperEnd_Absolute = ImgData.GetDataValue(PictureCoord)
        DB.GetMyModifierForm(GUID).ReactOnChangedProperty()
    End Sub

    Private Sub tsmi_FromHisto_Click(sender As Object, e As EventArgs) Handles tsmi_FromHisto.Click
        Dim HistoForm As frmGraph = DB.GetMyHistoForm(GUID)
        If IsNothing(HistoForm) Then Exit Sub
        Dim GP As ZedGraph.GraphPane = HistoForm.MyZEDGraph.MainGraph.GraphPane
        ImageFromData.CM_LowerEnd_Absolute = GP.XAxis.Scale.Min
        ImageFromData.CM_UpperEnd_Absolute = GP.XAxis.Scale.Max
        DB.GetMyModifierForm(GUID).ReactOnChangedProperty()
    End Sub

    Private Sub tsmi_FullEnd_Click(sender As Object, e As EventArgs) Handles tsmi_FullEnd.Click
        ImageFromData.CM_LowerEnd_Absolute = ImgStat.MonoStatistics_Int.Min.Key
        ImageFromData.CM_UpperEnd_Absolute = ImgStat.MonoStatistics_Int.Max.Key
        DB.GetMyModifierForm(GUID).ReactOnChangedProperty()
    End Sub

    '''<summary>Default value for new image.</summary>
    Private Sub SetDefaultsForNewImage()
        If IsNothing(ImgStat.MonoStatistics_Int) Then Exit Sub
        SetPercentilRange(1, 99)
        ImageFromData.CM_Gamma = 0.5
    End Sub

    '''<summary>Set the plot range to the given percentil range.</summary>
    '''<param name="Lower">Lower limit.</param>
    '''<param name="Upper">Upper limit.</param>
    Private Sub SetPercentilRange(ByVal Lower As Integer, ByVal Upper As Integer)
        If IsNothing(ImgStat.MonoStatistics_Int) Then Exit Sub
        ImageFromData.CM_LowerEnd_Absolute = ImgStat.MonoStatistics_Int.GetPercentile(Lower)
        ImageFromData.CM_UpperEnd_Absolute = ImgStat.MonoStatistics_Int.GetPercentile(Upper)
    End Sub

    Private Sub tsmi_Percentil_Click(sender As Object, e As EventArgs) Handles tsmi_Percentil.Click
        Dim Lower As String = InputBox("Lower end", "Lower end", "10")
        Dim Upper As String = InputBox("Upper end", "Upper end", "90")
        ImageFromData.CM_LowerEnd_Absolute = ImgStat.MonoStatistics_Int.GetPercentile(Lower.ValRegIndepInteger)
        ImageFromData.CM_UpperEnd_Absolute = ImgStat.MonoStatistics_Int.GetPercentile(Upper.ValRegIndepInteger)
        DB.GetMyModifierForm(GUID).ReactOnChangedProperty()
    End Sub

    Private Sub tsmi_SetCut_Click(sender As Object, e As EventArgs) Handles tsmi_SetCut.Click
        'Set a new cut around the current pixel
        Dim W As String = InputBox("Width", "Width", "100")
        Dim H As String = InputBox("Heigth", "Heigth", "100")
        ImageFromData.Cut_Left = DataCoord.X - (W.ValRegIndepInteger \ 2)
        ImageFromData.Cut_Top = DataCoord.Y - (H.ValRegIndepInteger \ 2)
        ImageFromData.Cut_Right = ImgData.NAXIS1 - (DataCoord.X + W.ValRegIndepInteger)
        ImageFromData.Cut_Bottom = ImgData.NAXIS2 - (DataCoord.Y + H.ValRegIndepInteger)
        DB.GetMyModifierForm(GUID).ReactOnChangedProperty()
    End Sub

    Private Sub tsmi_ResetCut_Click(sender As Object, e As EventArgs) Handles tsmi_ResetCut.Click
        ImageFromData.Cut_Left = 0
        ImageFromData.Cut_Top = 0
        ImageFromData.Cut_Right = 0
        ImageFromData.Cut_Bottom = 0
        DB.GetMyModifierForm(GUID).ReactOnChangedProperty()
    End Sub

End Class