Option Explicit On
Option Strict On

#Disable Warning CA1416 ' Validate platform compatibility

Public Class frmCreateSlideShow

    Public SlideShow As New cCreateSlideShow
    Public ImageFile As New List(Of String)

    Private Sub frmCreateSlideShow_Load(sender As Object, e As EventArgs) Handles Me.Load
        pgMain.SelectedObject = SlideShow
    End Sub

    Private Sub tsmiFile_Create_Click(sender As Object, e As EventArgs) Handles tsmiFile_Create.Click

        SlideShow.Create(ImageFile)
        Utils.StartWithItsEXE(SlideShow.PDFFile)

    End Sub

End Class