﻿Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class MDIParent

    '''<summary>Drag-and-drop handler.</summary>
    Private WithEvents DD As Ato.DragDrop

    Private Sub MDIParent_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.Text = (New cGetBuildDateTime).GetMainformTitle
        dpMain.Theme = New WeifenLuo.WinFormsUI.Docking.VS2015LightTheme
        DB.Init()

        'Init drap-and-drop
        DD = New Ato.DragDrop(dpMain)

    End Sub

    Private Sub DD_DropOccured(Files() As String) Handles DD.DropOccured
        'Handle drag-and-drop for the first dropped FIT(s) file
        Dim AllFiles As New List(Of String)
        For Each File As String In Files
            If System.IO.Path.GetExtension(File).ToUpper.StartsWith(".FIT") Then
                OpenFile(File)
                Exit Sub
            End If
        Next File
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click
        Using OpenFileDialog As New OpenFileDialog
            OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog.Filter = "FITS files (*.fit*)|*.fit*"
            OpenFileDialog.Multiselect = False
            If OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'Load file
                OpenFile(OpenFileDialog.FileName)
            End If
        End Using
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CutToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasteToolStripMenuItem.Click
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub tsmiNewImageWindow_Click(sender As Object, e As EventArgs) Handles tsmiNewImageWindow.Click
        Dim X As New frmImage
        X.Show(dpMain, DockState.Float)
    End Sub

    Private Sub ImageParameterEditorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImageParameterEditorToolStripMenuItem.Click
        Dim X As New frmImgParameter
        X.Show(dpMain, DockState.Float)
    End Sub

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
    ' Load a file
    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    Private Sub OpenFile(ByVal FileName As String)

        If System.IO.File.Exists(FileName) = False Then Exit Sub

        'Dispplay image
        Dim ImageForm As New frmImage
        ImageForm.LoadImage(FileName)
        ImageForm.Show(dpMain, DockState.Float)

        'Display histo
        Dim StatForm As New frmGraph
        StatForm.PlotStatistics(FileName, ImageForm.ImgStat)
        StatForm.Show(dpMain, DockState.Float)

        'Display image properties
        Dim ImgPropForm As New frmImageModifier
        ImgPropForm.FormToModify = ImageForm
        ImgPropForm.Show(dpMain, DockState.Float)


    End Sub

End Class