Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class MDIParent

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            Dim FileName = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
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

    Private Sub MDIParent_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = (New cGetBuildDateTime).GetMainformTitle
        dpMain.Theme = New WeifenLuo.WinFormsUI.Docking.VS2015LightTheme
    End Sub

    Private Sub ImageParameterEditorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImageParameterEditorToolStripMenuItem.Click
        Dim X As New frmImgParameter
        X.Show(dpMain, DockState.Float)
    End Sub

End Class
