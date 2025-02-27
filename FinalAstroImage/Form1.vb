Option Explicit On
Option Strict On

Public Class Form1

    '''<summary>Database holding main properties.</summary>
    Private DB As New cDB

    '''<summary>The main picture box.</summary>
    '''<remarks>This elements are self-coded and will not work in 64-bit from the toolbox ...</remarks>
    Private WithEvents pbMain As PictureBoxEx

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Load custom controls - main image (must be done due to 64-bit IDE limitation)
        pbMain = New PictureBoxEx
        scMain.Panel2.Controls.Add(pbMain)
        pbMain.Dock = DockStyle.Fill
        pbMain.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        pbMain.SizeMode = PictureBoxSizeMode.Zoom
        pbMain.BackColor = Color.Purple

    End Sub

    Private Sub tsmiAction_Recalc_Click(sender As Object, e As EventArgs) Handles tsmiAction_Recalc.Click
        ReProcess()
    End Sub

    Private Sub UcChannel1_FileAction(sender As Object, Action As ucChannel.eFileAction) Handles UcChannel1.FileAction, UcChannel2.FileAction, UcChannel3.FileAction
        Dim FileName As String = CType(sender, ucChannel).FileName
        Select Case Action
            Case ucChannel.eFileAction.OpenFolder
                Dim FolderName As String = System.IO.Path.GetDirectoryName(FileName)
                Utils.StartWithItsEXE(FolderName)
            Case ucChannel.eFileAction.OpenFileDefault
                Utils.StartWithItsEXE(FileName)
            Case ucChannel.eFileAction.OpenFileFITSWork
                If String.IsNullOrEmpty(DB.FITSWork) = True Then
                    Utils.StartWithItsEXE(FileName)
                Else
                    Process.Start(DB.FITSWork, Chr(34) & FileName & Chr(34))
                End If
            Case ucChannel.eFileAction.OpenFileIrfanView
                If String.IsNullOrEmpty(DB.IrfanView) = True Then
                    Utils.StartWithItsEXE(FileName)
                Else
                    Process.Start(DB.IrfanView, FileName)
                End If
        End Select
    End Sub

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
    ' Main processor
    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    Private Sub ReProcess()

        'Load the channels
        LoadFile(0, UcChannel1)
        LoadFile(1, UcChannel2)
        LoadFile(2, UcChannel3)

        'Calculate the common ROI
        Dim CommonROI As New cAstroImageProcessing.cROICombiner
        For ChannelIndex As Integer = 0 To 2
            Dim NAXIS1 As Integer = DB.Channels(ChannelIndex).Data.GetUpperBound(0) + 1
            Dim NAXIS2 As Integer = DB.Channels(ChannelIndex).Data.GetUpperBound(1) + 1
            CommonROI.Files.Add(ChannelIndex.ToString.Trim, New cAstroImageProcessing.cROICombiner.cFileSpecifics(NAXIS1, NAXIS2, 0, 0))
        Next ChannelIndex
        CommonROI.Calculate()

        'Generate the image - just RGB ...
        DB.ImageFromData.GenerateDisplayImageRGB(DB.Channels(0).Data, DB.Channels(1).Data, DB.Channels(2).Data, DB.IPP)

        'Display the combined image
        DB.ImageFromData.OutputImage.UnlockBits()
        pbMain.Image = DB.ImageFromData.OutputImage.BitmapToProcess

    End Sub

    '''<summary>Load the file to the corresponding channel.</summary>
    '''<param name="ChannelIndex">Channel to load.</param>
    '''<param name="ucC">Channel box containing the file name.</param>
    '''<returns>TRUE if channel contains valid data, FALSE else.</returns>
    Private Function LoadFile(ByVal ChannelIndex As Integer, ByVal ucC As ucChannel) As Boolean
        ucC.LogContent = String.Empty
        If System.IO.File.Exists(ucC.FileName) Then
            'Load the file if it requires loading
            If DB.Channels(ChannelIndex).FileName <> ucC.FileName Then
                'Load and store filename as indication for loaded
                ucC.FileStatus = Color.Orange : De()
                DB.Channels(ChannelIndex).Data = DB.TIFF_IO.LoadToDouble(ucC.FileName)
                DB.Channels(ChannelIndex).FileName = ucC.FileName
                ucC.LogContent = (DB.Channels(ChannelIndex).Data.GetUpperBound(0) + 1).ValRegIndep & "x" & (DB.Channels(ChannelIndex).Data.GetUpperBound(1) + 1).ValRegIndep
            End If
            'Indicate as loaded
            ucC.FileStatus = Color.LimeGreen : De()
            Return True
        Else
            'File does NOT exist -> no channel data
            DB.Channels(ChannelIndex) = New cDB.cChannel
            ucC.FileStatus = Color.Red : De()
            Return False
        End If
    End Function

    Public Sub De()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub tsmiFile_Exit_Click(sender As Object, e As EventArgs) Handles tsmiFile_Exit.Click
        End
    End Sub

End Class
