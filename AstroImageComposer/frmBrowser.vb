Option Explicit On
Option Strict On

Public Class frmBrowser

    Public ImageForm As frmImage

    Private Sub frmBrowser_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim root = New TreeNode("\\192.168.100.10\dsc") : root.Tag = "\\192.168.100.10\dsc" : root.ImageKey = "drive"
        tvDirTree.Nodes.Add(root)

    End Sub

    Private Sub tvDirTree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvDirTree.AfterSelect
        If AppendSubDirectories(tvDirTree.SelectedNode) = True Then
            ListAllFiles(tvDirTree.SelectedNode)
        End If
        tvDirTree.SelectedNode.Expand()
        lvFiles.BackColor = Color.White
    End Sub

    '''<summary>Append all subdirectories to the selected node.</summary>
    Private Function AppendSubDirectories(ByRef Node As TreeNode) As Boolean
        'Get a list of all subdirectories
        Dim AllDirectories As New List(Of String)
        Try
            AllDirectories = New List(Of String)(System.IO.Directory.GetDirectories(CStr(Node.Tag)))
        Catch ex As Exception
            'Directory list is not available -> indicate as red and exit
            Node.ForeColor = Color.Red
            Return False
        End Try
        'Add all directories
        For Each subDir As String In AllDirectories
            Dim info As New IO.DirectoryInfo(subDir)
            If info.Exists Then
                Dim NodeToAdd As New TreeNode(info.Name)
                NodeToAdd.Tag = subDir
                NodeToAdd.ImageKey = "folder"
                Node.Nodes.Add(NodeToAdd)
            End If
        Next subDir
        Return True
    End Function

    '''<summary>List all files in the selected directory.</summary>
    Private Sub ListAllFiles(ByRef Node As TreeNode)
        lvFiles.Items.Clear()
        Dim nodeDirInfo As New IO.DirectoryInfo(CStr(Node.Tag))
        For Each file As IO.FileInfo In nodeDirInfo.GetFiles
            Dim item As New ListViewItem(file.Name, "file")
            Dim subItems As ListViewItem.ListViewSubItem() = {New ListViewItem.ListViewSubItem(item, "File"), New ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString())}
            item.SubItems.AddRange(subItems)
            lvFiles.Items.Add(item)
        Next file
        lvFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub

    Private Sub lvFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvFiles.SelectedIndexChanged
        If lvFiles.SelectedItems.Count = 0 Then
            tssl_Path.BackColor = Color.White
        Else
            Dim FullPath As String = tvDirTree.SelectedNode.FullPath
            Dim FileName As String = lvFiles.SelectedItems(0).Text
            Dim SelectedFile As String = System.IO.Path.Combine(FullPath, FileName)
            If System.IO.File.Exists(SelectedFile) Then
                tssl_Path.Text = SelectedFile
                tssl_Path.BackColor = Color.LimeGreen
                Dim Extension As String = System.IO.Path.GetExtension(SelectedFile).ToUpper
                Select Case Extension
                    Case ".FIT", ".FITS"
                        ImageForm.LoadImage(SelectedFile)
                End Select
            Else
                tssl_Path.BackColor = Color.Red
            End If
        End If
    End Sub

End Class