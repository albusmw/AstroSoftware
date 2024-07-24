<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBrowser
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        tvDirTree = New TreeView()
        scMain = New SplitContainer()
        tbFilter = New TextBox()
        tbSelectedPath = New TextBox()
        lvFiles = New ListView()
        ch_Name = New ColumnHeader()
        ch_Type = New ColumnHeader()
        ch_Date = New ColumnHeader()
        ilIcons = New ImageList(components)
        ssMain = New StatusStrip()
        tssl_Path = New ToolStripStatusLabel()
        ch_ExpTime = New ColumnHeader()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        ssMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tvDirTree
        ' 
        tvDirTree.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tvDirTree.Location = New Point(3, 3)
        tvDirTree.Name = "tvDirTree"
        tvDirTree.Size = New Size(262, 647)
        tvDirTree.TabIndex = 0
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.Location = New Point(3, 4)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(tvDirTree)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(tbFilter)
        scMain.Panel2.Controls.Add(tbSelectedPath)
        scMain.Panel2.Controls.Add(lvFiles)
        scMain.Size = New Size(805, 653)
        scMain.SplitterDistance = 268
        scMain.TabIndex = 2
        ' 
        ' tbFilter
        ' 
        tbFilter.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        tbFilter.Location = New Point(456, 8)
        tbFilter.Name = "tbFilter"
        tbFilter.Size = New Size(74, 23)
        tbFilter.TabIndex = 2
        tbFilter.Text = "*.fit?"
        ' 
        ' tbSelectedPath
        ' 
        tbSelectedPath.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbSelectedPath.Location = New Point(3, 8)
        tbSelectedPath.Name = "tbSelectedPath"
        tbSelectedPath.ReadOnly = True
        tbSelectedPath.Size = New Size(447, 23)
        tbSelectedPath.TabIndex = 1
        ' 
        ' lvFiles
        ' 
        lvFiles.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lvFiles.Columns.AddRange(New ColumnHeader() {ch_Name, ch_Type, ch_Date, ch_ExpTime})
        lvFiles.GridLines = True
        lvFiles.GroupImageList = ilIcons
        lvFiles.LargeImageList = ilIcons
        lvFiles.Location = New Point(3, 37)
        lvFiles.Name = "lvFiles"
        lvFiles.Size = New Size(527, 613)
        lvFiles.SmallImageList = ilIcons
        lvFiles.TabIndex = 0
        lvFiles.UseCompatibleStateImageBehavior = False
        lvFiles.View = View.Details
        ' 
        ' ch_Name
        ' 
        ch_Name.Text = "Name"
        ' 
        ' ch_Type
        ' 
        ch_Type.Text = "Type"
        ' 
        ' ch_Date
        ' 
        ch_Date.Text = "Date and time"
        ' 
        ' ilIcons
        ' 
        ilIcons.ColorDepth = ColorDepth.Depth32Bit
        ilIcons.ImageSize = New Size(16, 16)
        ilIcons.TransparentColor = Color.Transparent
        ' 
        ' ssMain
        ' 
        ssMain.Items.AddRange(New ToolStripItem() {tssl_Path})
        ssMain.Location = New Point(0, 660)
        ssMain.Name = "ssMain"
        ssMain.Size = New Size(810, 22)
        ssMain.TabIndex = 3
        ssMain.Text = "StatusStrip1"
        ' 
        ' tssl_Path
        ' 
        tssl_Path.Name = "tssl_Path"
        tssl_Path.Size = New Size(122, 17)
        tssl_Path.Text = "--- no file selected ---"
        ' 
        ' ch_ExpTime
        ' 
        ch_ExpTime.Text = "Exposure time"
        ' 
        ' frmBrowser
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(810, 682)
        Controls.Add(ssMain)
        Controls.Add(scMain)
        Name = "frmBrowser"
        Text = "File browser"
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        scMain.Panel2.PerformLayout()
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        ssMain.ResumeLayout(False)
        ssMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tvDirTree As TreeView
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents lvFiles As ListView
    Friend WithEvents ch_Name As ColumnHeader
    Friend WithEvents ch_Type As ColumnHeader
    Friend WithEvents ch_Date As ColumnHeader
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tssl_Path As ToolStripStatusLabel
    Friend WithEvents ilIcons As ImageList
    Friend WithEvents tbSelectedPath As TextBox
    Friend WithEvents tbFilter As TextBox
    Friend WithEvents ch_ExpTime As ColumnHeader
End Class
