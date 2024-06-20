Option Explicit On
Option Strict On

Public Class frmImage

    '''<summary>The image data itself.</summary>
    Public ImgData As AstroNET.Statistics
    '''<summary>The image statistics based on the data.</summary>
    Public ImgStat As AstroNET.Statistics.sStatistics
    '''<summary>The image generated from the image data.</summary>
    Public ImageFromData As New cImageFromData

    '''<summary>FITS header (if there was any).</summary>
    Public FITSHeader As cFITSHeaderParser
    '''<summary>Data start position within the file.</summary>
    Public DataStartPos As Integer = 0

    '''<summary>Use Intel IPP for reading.</summary>
    Public Property UseIPP As Boolean = False
    '''<summary>Force direct read (do not use scaling, ...).</summary>
    Public Property ForceDirect As Boolean = True

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

End Class