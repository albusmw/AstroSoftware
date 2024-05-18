Option Explicit On
Option Strict On
Imports System.Windows.Forms.AxHost

Public Class frmImage

    Public ImgData As AstroNET.Statistics
    Public ImgStat As AstroNET.Statistics.sStatistics

    Public ImageFromData As New cImageFromData

    Public FITSHeader As cFITSHeaderParser
    Public DataStartPos As Integer = 0
    Public UseIPP As Boolean = False
    Public ForceDirect As Boolean = True

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

        'Display the calculated image data
        DisplayImageData()

        'Set window title
        Me.TabText = FileName

        Return String.Empty

    End Function

    Public Sub DisplayImageData()

        'Generate display image
        ImageFromData.ColorMap_LowerEnd = ImgStat.MonoStatistics_Int.Min.Key
        ImageFromData.ColorMap_UpperEnd = ImgStat.MonoStatistics_Int.Max.Key
        ImageFromData.GenerateDisplayImage(ImgData.DataProcessor_UInt16.ImageData(0).Data, DB.IPP)

        'Display image
        ImageFromData.OutputImage.UnlockBits()
        pbMain.BackColor = ImageFromData.ColorMap_BackColor
        pbMain.Image = ImageFromData.OutputImage.BitmapToProcess

    End Sub

End Class