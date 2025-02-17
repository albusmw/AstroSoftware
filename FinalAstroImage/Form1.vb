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

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
    ' Main processor
    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    Private Sub ReProcess()

        'Load the channels
        LoadFile(0, UcChannel1)
        LoadFile(1, UcChannel2)
        LoadFile(2, UcChannel3)

        'Display the combined image
        DB.ImageFromData.OutputImage.UnlockBits()
        pbMain.Image = DB.ImageFromData.OutputImage.BitmapToProcess

    End Sub

    '''<summary>Load the file to the corresponding channel.</summary>
    '''<param name="ChannelIndex">Channel to load.</param>
    '''<param name="tb">Text box containing the file name.</param>
    '''<returns>TRUE if channel contains valid data, FALSE else.</returns>
    Private Function LoadFile(ByVal ChannelIndex As Integer, ByVal tb As ucChannel) As Boolean
        If System.IO.File.Exists(tb.FileName) Then
            'Load the file if it requires loading
            If DB.Channels(ChannelIndex).FileName <> tb.FileName Then
                'Load and store filename as indication for loaded
                tb.FileStatus = Color.Orange : De()
                DB.Channels(ChannelIndex).Data = DB.TIFF_IO.LoadToDouble(tb.FileName)
                DB.Channels(ChannelIndex).FileName = tb.FileName
            End If
            'Indicate as loaded
            tb.FileStatus = Color.LimeGreen : De()
            Return True
        Else
            'File does NOT exist -> no channel data
            DB.Channels(ChannelIndex) = New cDB.cChannel
            tb.FileStatus = Color.Red : De()
            Return False
        End If
    End Function

    Public Sub De()
        System.Windows.Forms.Application.DoEvents()
    End Sub

End Class
