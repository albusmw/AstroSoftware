Option Explicit On
Option Strict On

Public Class ucChannel

    Public Event FileAction(sender As Object, ByVal Action As eFileAction)

    Public Property ChannelName As String
        Get
            Return cbFile.Text
        End Get
        Set(value As String)
            cbFile.Text = value
        End Set
    End Property

    Public Property FileName As String
        Get
            Return tbFile.Text
        End Get
        Set(value As String)
            tbFile.Text = value
        End Set
    End Property

    Public Property FileStatus As Color
        Get
            Return tbFile.BackColor
        End Get
        Set(value As Color)
            tbFile.BackColor = value
        End Set
    End Property

    Public Property LogContent As String
        Get
            Return tbLog.Text
        End Get
        Set(value As String)
            tbLog.Text = value
        End Set
    End Property

    Public Enum eFileAction
        OpenFolder
        OpenFileDefault
        OpenFileFITSWork
        OpenFileIrfanView
    End Enum

    Private Sub cbFileAction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFileAction.SelectedIndexChanged
        RaiseEvent FileAction(Me, CType(CType(sender, ComboBox).SelectedIndex, eFileAction))
    End Sub

End Class
