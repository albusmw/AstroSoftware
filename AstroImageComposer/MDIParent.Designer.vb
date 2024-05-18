<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIParent
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIParent))
        msMain = New MenuStrip()
        FileMenu = New ToolStripMenuItem()
        OpenToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        SaveToolStripMenuItem = New ToolStripMenuItem()
        SaveAsToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator4 = New ToolStripSeparator()
        PrintToolStripMenuItem = New ToolStripMenuItem()
        PrintPreviewToolStripMenuItem = New ToolStripMenuItem()
        PrintSetupToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator5 = New ToolStripSeparator()
        ExitToolStripMenuItem = New ToolStripMenuItem()
        EditMenu = New ToolStripMenuItem()
        UndoToolStripMenuItem = New ToolStripMenuItem()
        RedoToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator6 = New ToolStripSeparator()
        CutToolStripMenuItem = New ToolStripMenuItem()
        CopyToolStripMenuItem = New ToolStripMenuItem()
        PasteToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator7 = New ToolStripSeparator()
        SelectAllToolStripMenuItem = New ToolStripMenuItem()
        ToolsMenu = New ToolStripMenuItem()
        OptionsToolStripMenuItem = New ToolStripMenuItem()
        HelpMenu = New ToolStripMenuItem()
        ContentsToolStripMenuItem = New ToolStripMenuItem()
        IndexToolStripMenuItem = New ToolStripMenuItem()
        SearchToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator8 = New ToolStripSeparator()
        AboutToolStripMenuItem = New ToolStripMenuItem()
        NewToolStripMenuItem1 = New ToolStripMenuItem()
        tsmiNewImageWindow = New ToolStripMenuItem()
        ImageParameterEditorToolStripMenuItem = New ToolStripMenuItem()
        ssMain = New StatusStrip()
        ToolStripStatusLabel = New ToolStripStatusLabel()
        ToolTip = New ToolTip(components)
        dpMain = New WeifenLuo.WinFormsUI.Docking.DockPanel()
        VS2015DarkTheme1 = New WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme()
        msMain.SuspendLayout()
        ssMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' msMain
        ' 
        msMain.ImageScalingSize = New Size(20, 20)
        msMain.Items.AddRange(New ToolStripItem() {FileMenu, EditMenu, ToolsMenu, HelpMenu, NewToolStripMenuItem1})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Padding = New Padding(8, 3, 0, 3)
        msMain.Size = New Size(1464, 30)
        msMain.TabIndex = 5
        msMain.Text = "MenuStrip"
        ' 
        ' FileMenu
        ' 
        FileMenu.DropDownItems.AddRange(New ToolStripItem() {OpenToolStripMenuItem, ToolStripSeparator3, SaveToolStripMenuItem, SaveAsToolStripMenuItem, ToolStripSeparator4, PrintToolStripMenuItem, PrintPreviewToolStripMenuItem, PrintSetupToolStripMenuItem, ToolStripSeparator5, ExitToolStripMenuItem})
        FileMenu.ImageTransparentColor = SystemColors.ActiveBorder
        FileMenu.Name = "FileMenu"
        FileMenu.Size = New Size(46, 24)
        FileMenu.Text = "&File"
        ' 
        ' OpenToolStripMenuItem
        ' 
        OpenToolStripMenuItem.Image = CType(resources.GetObject("OpenToolStripMenuItem.Image"), Image)
        OpenToolStripMenuItem.ImageTransparentColor = Color.Black
        OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        OpenToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.O
        OpenToolStripMenuItem.Size = New Size(224, 26)
        OpenToolStripMenuItem.Text = "&Open"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(221, 6)
        ' 
        ' SaveToolStripMenuItem
        ' 
        SaveToolStripMenuItem.Image = CType(resources.GetObject("SaveToolStripMenuItem.Image"), Image)
        SaveToolStripMenuItem.ImageTransparentColor = Color.Black
        SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        SaveToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.S
        SaveToolStripMenuItem.Size = New Size(224, 26)
        SaveToolStripMenuItem.Text = "&Save"
        ' 
        ' SaveAsToolStripMenuItem
        ' 
        SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        SaveAsToolStripMenuItem.Size = New Size(224, 26)
        SaveAsToolStripMenuItem.Text = "Save &As"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(221, 6)
        ' 
        ' PrintToolStripMenuItem
        ' 
        PrintToolStripMenuItem.Image = CType(resources.GetObject("PrintToolStripMenuItem.Image"), Image)
        PrintToolStripMenuItem.ImageTransparentColor = Color.Black
        PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        PrintToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.P
        PrintToolStripMenuItem.Size = New Size(224, 26)
        PrintToolStripMenuItem.Text = "&Print"
        ' 
        ' PrintPreviewToolStripMenuItem
        ' 
        PrintPreviewToolStripMenuItem.Image = CType(resources.GetObject("PrintPreviewToolStripMenuItem.Image"), Image)
        PrintPreviewToolStripMenuItem.ImageTransparentColor = Color.Black
        PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem"
        PrintPreviewToolStripMenuItem.Size = New Size(224, 26)
        PrintPreviewToolStripMenuItem.Text = "Print Pre&view"
        ' 
        ' PrintSetupToolStripMenuItem
        ' 
        PrintSetupToolStripMenuItem.Name = "PrintSetupToolStripMenuItem"
        PrintSetupToolStripMenuItem.Size = New Size(224, 26)
        PrintSetupToolStripMenuItem.Text = "Print Setup"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(221, 6)
        ' 
        ' ExitToolStripMenuItem
        ' 
        ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        ExitToolStripMenuItem.Size = New Size(224, 26)
        ExitToolStripMenuItem.Text = "E&xit"
        ' 
        ' EditMenu
        ' 
        EditMenu.DropDownItems.AddRange(New ToolStripItem() {UndoToolStripMenuItem, RedoToolStripMenuItem, ToolStripSeparator6, CutToolStripMenuItem, CopyToolStripMenuItem, PasteToolStripMenuItem, ToolStripSeparator7, SelectAllToolStripMenuItem})
        EditMenu.Name = "EditMenu"
        EditMenu.Size = New Size(49, 24)
        EditMenu.Text = "&Edit"
        ' 
        ' UndoToolStripMenuItem
        ' 
        UndoToolStripMenuItem.Image = CType(resources.GetObject("UndoToolStripMenuItem.Image"), Image)
        UndoToolStripMenuItem.ImageTransparentColor = Color.Black
        UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        UndoToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Z
        UndoToolStripMenuItem.Size = New Size(210, 26)
        UndoToolStripMenuItem.Text = "&Undo"
        ' 
        ' RedoToolStripMenuItem
        ' 
        RedoToolStripMenuItem.Image = CType(resources.GetObject("RedoToolStripMenuItem.Image"), Image)
        RedoToolStripMenuItem.ImageTransparentColor = Color.Black
        RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        RedoToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Y
        RedoToolStripMenuItem.Size = New Size(210, 26)
        RedoToolStripMenuItem.Text = "&Redo"
        ' 
        ' ToolStripSeparator6
        ' 
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New Size(207, 6)
        ' 
        ' CutToolStripMenuItem
        ' 
        CutToolStripMenuItem.Image = CType(resources.GetObject("CutToolStripMenuItem.Image"), Image)
        CutToolStripMenuItem.ImageTransparentColor = Color.Black
        CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        CutToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.X
        CutToolStripMenuItem.Size = New Size(210, 26)
        CutToolStripMenuItem.Text = "Cu&t"
        ' 
        ' CopyToolStripMenuItem
        ' 
        CopyToolStripMenuItem.Image = CType(resources.GetObject("CopyToolStripMenuItem.Image"), Image)
        CopyToolStripMenuItem.ImageTransparentColor = Color.Black
        CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        CopyToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.C
        CopyToolStripMenuItem.Size = New Size(210, 26)
        CopyToolStripMenuItem.Text = "&Copy"
        ' 
        ' PasteToolStripMenuItem
        ' 
        PasteToolStripMenuItem.Image = CType(resources.GetObject("PasteToolStripMenuItem.Image"), Image)
        PasteToolStripMenuItem.ImageTransparentColor = Color.Black
        PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        PasteToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.V
        PasteToolStripMenuItem.Size = New Size(210, 26)
        PasteToolStripMenuItem.Text = "&Paste"
        ' 
        ' ToolStripSeparator7
        ' 
        ToolStripSeparator7.Name = "ToolStripSeparator7"
        ToolStripSeparator7.Size = New Size(207, 6)
        ' 
        ' SelectAllToolStripMenuItem
        ' 
        SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        SelectAllToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.A
        SelectAllToolStripMenuItem.Size = New Size(210, 26)
        SelectAllToolStripMenuItem.Text = "Select &All"
        ' 
        ' ToolsMenu
        ' 
        ToolsMenu.DropDownItems.AddRange(New ToolStripItem() {OptionsToolStripMenuItem})
        ToolsMenu.Name = "ToolsMenu"
        ToolsMenu.Size = New Size(58, 24)
        ToolsMenu.Text = "&Tools"
        ' 
        ' OptionsToolStripMenuItem
        ' 
        OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        OptionsToolStripMenuItem.Size = New Size(144, 26)
        OptionsToolStripMenuItem.Text = "&Options"
        ' 
        ' HelpMenu
        ' 
        HelpMenu.DropDownItems.AddRange(New ToolStripItem() {ContentsToolStripMenuItem, IndexToolStripMenuItem, SearchToolStripMenuItem, ToolStripSeparator8, AboutToolStripMenuItem})
        HelpMenu.Name = "HelpMenu"
        HelpMenu.Size = New Size(55, 24)
        HelpMenu.Text = "&Help"
        ' 
        ' ContentsToolStripMenuItem
        ' 
        ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem"
        ContentsToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.F1
        ContentsToolStripMenuItem.Size = New Size(211, 26)
        ContentsToolStripMenuItem.Text = "&Contents"
        ' 
        ' IndexToolStripMenuItem
        ' 
        IndexToolStripMenuItem.Image = CType(resources.GetObject("IndexToolStripMenuItem.Image"), Image)
        IndexToolStripMenuItem.ImageTransparentColor = Color.Black
        IndexToolStripMenuItem.Name = "IndexToolStripMenuItem"
        IndexToolStripMenuItem.Size = New Size(211, 26)
        IndexToolStripMenuItem.Text = "&Index"
        ' 
        ' SearchToolStripMenuItem
        ' 
        SearchToolStripMenuItem.Image = CType(resources.GetObject("SearchToolStripMenuItem.Image"), Image)
        SearchToolStripMenuItem.ImageTransparentColor = Color.Black
        SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        SearchToolStripMenuItem.Size = New Size(211, 26)
        SearchToolStripMenuItem.Text = "&Search"
        ' 
        ' ToolStripSeparator8
        ' 
        ToolStripSeparator8.Name = "ToolStripSeparator8"
        ToolStripSeparator8.Size = New Size(208, 6)
        ' 
        ' AboutToolStripMenuItem
        ' 
        AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        AboutToolStripMenuItem.Size = New Size(211, 26)
        AboutToolStripMenuItem.Text = "&About ..."
        ' 
        ' NewToolStripMenuItem1
        ' 
        NewToolStripMenuItem1.DropDownItems.AddRange(New ToolStripItem() {tsmiNewImageWindow, ImageParameterEditorToolStripMenuItem})
        NewToolStripMenuItem1.Name = "NewToolStripMenuItem1"
        NewToolStripMenuItem1.Size = New Size(66, 24)
        NewToolStripMenuItem1.Text = "New ..."
        ' 
        ' tsmiNewImageWindow
        ' 
        tsmiNewImageWindow.Name = "tsmiNewImageWindow"
        tsmiNewImageWindow.Size = New Size(251, 26)
        tsmiNewImageWindow.Text = "Image window"
        ' 
        ' ImageParameterEditorToolStripMenuItem
        ' 
        ImageParameterEditorToolStripMenuItem.Name = "ImageParameterEditorToolStripMenuItem"
        ImageParameterEditorToolStripMenuItem.Size = New Size(251, 26)
        ImageParameterEditorToolStripMenuItem.Text = "Image parameter editor"
        ' 
        ' ssMain
        ' 
        ssMain.ImageScalingSize = New Size(20, 20)
        ssMain.Items.AddRange(New ToolStripItem() {ToolStripStatusLabel})
        ssMain.Location = New Point(0, 1194)
        ssMain.Name = "ssMain"
        ssMain.Padding = New Padding(1, 0, 18, 0)
        ssMain.Size = New Size(1464, 26)
        ssMain.TabIndex = 7
        ssMain.Text = "StatusStrip"
        ' 
        ' ToolStripStatusLabel
        ' 
        ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        ToolStripStatusLabel.Size = New Size(49, 20)
        ToolStripStatusLabel.Text = "Status"
        ' 
        ' dpMain
        ' 
        dpMain.BackColor = Color.FromArgb(CByte(0), CByte(192), CByte(192))
        dpMain.Dock = DockStyle.Fill
        dpMain.Location = New Point(0, 30)
        dpMain.Margin = New Padding(3, 4, 3, 4)
        dpMain.Name = "dpMain"
        dpMain.Size = New Size(1464, 1164)
        dpMain.TabIndex = 9
        ' 
        ' MDIParent
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1464, 1220)
        Controls.Add(dpMain)
        Controls.Add(msMain)
        Controls.Add(ssMain)
        IsMdiContainer = True
        MainMenuStrip = msMain
        Margin = New Padding(5, 4, 5, 4)
        Name = "MDIParent"
        Text = "MDIParent"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        ssMain.ResumeLayout(False)
        ssMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ssMain As System.Windows.Forms.StatusStrip
    Friend WithEvents PrintPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintSetupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain As System.Windows.Forms.MenuStrip
    Friend WithEvents EditMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dpMain As WeifenLuo.WinFormsUI.Docking.DockPanel
    Friend WithEvents NewToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents tsmiNewImageWindow As ToolStripMenuItem
    Friend WithEvents VS2015DarkTheme1 As WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme
    Friend WithEvents ImageParameterEditorToolStripMenuItem As ToolStripMenuItem

End Class
