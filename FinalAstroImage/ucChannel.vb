Option Explicit On
Option Strict On

Public Class ucChannel

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

End Class
