Option Explicit On
Option Strict On

Public Class frmBrowser

    '''<summary>Drag-and-drop handler.</summary>
    Private WithEvents DD As Ato.DragDrop

    Public ImageForm As frmImage

    Private FullPath As String = String.Empty
    Private FileName As String = String.Empty

    Public Function SelectedFile() As String
        Return System.IO.Path.Combine(FullPath, FileName)
    End Function

    Public Function SelectedFileExtension() As String
        Return System.IO.Path.GetExtension(SelectedFile).ToUpper
    End Function

    Private Sub frmBrowser_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Init drap-and-drop
        DD = New Ato.DragDrop(tvDirTree)
    End Sub

    Private Sub tvDirTree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvDirTree.AfterSelect
        AfterSelect()
    End Sub

    Private Sub DD_DropOccured(Files() As String) Handles DD.DropOccured
        Dim root = New TreeNode(Files(0)) : root.Tag = Files(0) : root.ImageKey = "drive"
        tvDirTree.Nodes.Clear()
        tvDirTree.Nodes.Add(root)
        tvDirTree.SelectedNode = root
        AfterSelect()
    End Sub

    '''<summary>Action to take place after a node was selected (or added and first selected).</summary>
    Private Sub AfterSelect()
        tbSelectedPath.Text = tvDirTree.SelectedNode.FullPath
        If AppendSubDirectories(tvDirTree.SelectedNode) = True Then
            ListAllFiles(tvDirTree.SelectedNode)
        End If
        tvDirTree.SelectedNode.Expand()
        lvFiles.BackColor = Color.White
    End Sub

    '''<summary>Append all subdirectories to the selected node.</summary>
    Private Function AppendSubDirectories(ByRef Node As TreeNode) As Boolean
        Node.Nodes.Clear()
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
        If IsNothing(Node) Then Exit Sub
        Dim nodeDirInfo As New IO.DirectoryInfo(CStr(Node.Tag))
        Dim AllFiles As IO.FileInfo() = nodeDirInfo.GetFiles(tbFilter.Text)
        Dim LocalGrepper As New cFITSGrepper
        LocalGrepper.Grep(AllFiles)
        For Each File As IO.FileInfo In AllFiles
            Dim ThisFITSHeader As Dictionary(Of eFITSKeywords, Object) = LocalGrepper.AllFileHeaders(File.FullName)
            Dim DT As DateTime = File.CreationTime
            Dim ExpTime As Double = Double.NaN
            GetTableParameters(ThisFITSHeader, DT, ExpTime)
            'Create the item and add all parameters (Type, creation time)
            Dim ListItem As New ListViewItem(File.Name, "file")
            Dim subItems As New List(Of ListViewItem.ListViewSubItem)
            subItems.Add(New ListViewItem.ListViewSubItem(ListItem, "File"))
            subItems.Add(New ListViewItem.ListViewSubItem(ListItem, DT.ToString()))
            subItems.Add(New ListViewItem.ListViewSubItem(ListItem, ExpTime.ValRegIndep))
            'Get icon
            If ilIcons.Images.ContainsKey(File.Extension.ToUpper) = False Then ilIcons.Images.Add(File.Extension.ToUpper, System.Drawing.Icon.ExtractAssociatedIcon(File.FullName))
            'Configure item and add to list
            ListItem.ImageKey = File.Extension.ToUpper
            ListItem.SubItems.AddRange(subItems.ToArray)
            lvFiles.Items.Add(ListItem)
        Next File
        lvFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub

    Private Sub GetTableParameters(ByRef HeaderElements As Dictionary(Of eFITSKeywords, Object), ByRef DT As DateTime, ByRef ExpTime As Double)
        If HeaderElements.ContainsKey(eFITSKeywords.EXPOSURE) Then ExpTime = CDbl(HeaderElements(eFITSKeywords.EXPOSURE))
        If HeaderElements.ContainsKey(eFITSKeywords.EXPTIME) Then ExpTime = CDbl(HeaderElements(eFITSKeywords.EXPTIME))
        If HeaderElements.ContainsKey(eFITSKeywords.DATE_OBS) Then DT = Date.Parse(CStr(HeaderElements(eFITSKeywords.DATE_OBS)))
        If HeaderElements.ContainsKey(eFITSKeywords.TIME_OBS) Then DT = DT + TimeSpan.Parse(CStr(HeaderElements(eFITSKeywords.TIME_OBS)))
    End Sub

    Private Sub lvFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvFiles.SelectedIndexChanged
        If lvFiles.SelectedItems.Count = 0 Then
            tssl_Path.BackColor = Color.White
        Else
            FullPath = tvDirTree.SelectedNode.FullPath
            FileName = lvFiles.SelectedItems(0).Text
            If System.IO.File.Exists(SelectedFile) Then
                tssl_Path.Text = SelectedFile()
                tssl_Path.BackColor = Color.LimeGreen
                Select Case SelectedFileExtension()
                    Case ".FIT", ".FITS"
                        ImageForm.LoadImage(SelectedFile)
                End Select
            Else
                tssl_Path.BackColor = Color.Red
            End If
        End If
    End Sub

    Private Sub tbFilter_TextChanged(sender As Object, e As EventArgs) Handles tbFilter.TextChanged
        ListAllFiles(tvDirTree.SelectedNode)
    End Sub

End Class