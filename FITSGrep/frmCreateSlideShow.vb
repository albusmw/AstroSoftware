Option Explicit On
Option Strict On

#Disable Warning CA1416 ' Validate platform compatibility

Public Class frmCreateSlideShow

    Private WithEvents DD1 As cDragDrop
    Public SlideShow As New cCreateSlideShow
    Public Files As New List(Of String)

    Public Sub AddFiles(ByVal Files As IEnumerable(Of String))
        For Each Item As String In Files
            AddFile(Item)
        Next Item
    End Sub

    Public Sub AddFile(ByVal File As String)
        If Files.Contains(File) = False Then
            Files.Add(File)
        End If
        Files.Sort(AddressOf Sorter)
    End Sub

    Private Sub frmCreateSlideShow_Load(sender As Object, e As EventArgs) Handles Me.Load
        pgMain.SelectedObject = SlideShow
        DD1 = New cDragDrop(lbFiles, False)
        lbFiles.DataSource = Files
    End Sub

    Private Sub tsmiFile_Create_Click(sender As Object, e As EventArgs) Handles tsmiFile_Create.Click
        SlideShow.Create(Files)
        Utils.StartWithItsEXE(SlideShow.PDFFile)
    End Sub

    Private Sub DD1_DropOccured(Files() As String) Handles DD1.DropOccured
        For Each Item As String In Files
            AddFile(Item)
        Next Item
    End Sub

    '''<summary>Sort 2 files ignoring path and extension.</summary>
    Private Function Sorter(ByVal A As String, ByVal B As String) As Integer
        Dim FileNameA As String = System.IO.Path.GetFileNameWithoutExtension(A)
        Dim FileNameB As String = System.IO.Path.GetFileNameWithoutExtension(B)
        Return FileNameA.CompareTo(FileNameB)
    End Function

End Class