<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        tbLog = New TextBox()
        pgMain = New PropertyGrid()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_OpenOriginal = New ToolStripMenuItem()
        tsmiFile_OpenOriginal_Default = New ToolStripMenuItem()
        tsmiFile_OpenOriginal_FITSWork = New ToolStripMenuItem()
        tsmiFile_OpenOutput = New ToolStripMenuItem()
        tsmiFile_OpenOutput_Default = New ToolStripMenuItem()
        tsmiFile_OpenOutput_FITSWork = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        tsmiFile_ExploreOutput = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiFile_Exit = New ToolStripMenuItem()
        tsmiProcess = New ToolStripMenuItem()
        tsmiPreset = New ToolStripMenuItem()
        tsmiProcess_QHY600Overscan = New ToolStripMenuItem()
        EqualCropToolStripMenuItem = New ToolStripMenuItem()
        scMain = New SplitContainer()
        lbInputFiles = New ListBox()
        scTop = New SplitContainer()
        msMain.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        CType(scTop, ComponentModel.ISupportInitialize).BeginInit()
        scTop.Panel1.SuspendLayout()
        scTop.Panel2.SuspendLayout()
        scTop.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Font = New Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbLog.Location = New Point(3, 3)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(831, 390)
        tbLog.TabIndex = 3
        tbLog.WordWrap = False
        ' 
        ' pgMain
        ' 
        pgMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgMain.Location = New Point(3, 3)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(414, 390)
        pgMain.TabIndex = 5
        pgMain.ToolbarVisible = False
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiProcess, tsmiPreset})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1291, 24)
        msMain.TabIndex = 6
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_OpenOriginal, tsmiFile_OpenOutput, ToolStripMenuItem2, tsmiFile_ExploreOutput, ToolStripMenuItem1, tsmiFile_Exit})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(37, 20)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_OpenOriginal
        ' 
        tsmiFile_OpenOriginal.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_OpenOriginal_Default, tsmiFile_OpenOriginal_FITSWork})
        tsmiFile_OpenOriginal.Name = "tsmiFile_OpenOriginal"
        tsmiFile_OpenOriginal.Size = New Size(192, 22)
        tsmiFile_OpenOriginal.Text = "Open original file"
        ' 
        ' tsmiFile_OpenOriginal_Default
        ' 
        tsmiFile_OpenOriginal_Default.Name = "tsmiFile_OpenOriginal_Default"
        tsmiFile_OpenOriginal_Default.Size = New Size(197, 22)
        tsmiFile_OpenOriginal_Default.Text = "... with default software"
        ' 
        ' tsmiFile_OpenOriginal_FITSWork
        ' 
        tsmiFile_OpenOriginal_FITSWork.Name = "tsmiFile_OpenOriginal_FITSWork"
        tsmiFile_OpenOriginal_FITSWork.Size = New Size(197, 22)
        tsmiFile_OpenOriginal_FITSWork.Text = "... with FITSWork"
        ' 
        ' tsmiFile_OpenOutput
        ' 
        tsmiFile_OpenOutput.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_OpenOutput_Default, tsmiFile_OpenOutput_FITSWork})
        tsmiFile_OpenOutput.Name = "tsmiFile_OpenOutput"
        tsmiFile_OpenOutput.Size = New Size(192, 22)
        tsmiFile_OpenOutput.Text = "Open converted file"
        ' 
        ' tsmiFile_OpenOutput_Default
        ' 
        tsmiFile_OpenOutput_Default.Name = "tsmiFile_OpenOutput_Default"
        tsmiFile_OpenOutput_Default.Size = New Size(197, 22)
        tsmiFile_OpenOutput_Default.Text = "... with default software"
        ' 
        ' tsmiFile_OpenOutput_FITSWork
        ' 
        tsmiFile_OpenOutput_FITSWork.Name = "tsmiFile_OpenOutput_FITSWork"
        tsmiFile_OpenOutput_FITSWork.Size = New Size(197, 22)
        tsmiFile_OpenOutput_FITSWork.Text = "... with FITSWork"
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(189, 6)
        ' 
        ' tsmiFile_ExploreOutput
        ' 
        tsmiFile_ExploreOutput.Name = "tsmiFile_ExploreOutput"
        tsmiFile_ExploreOutput.Size = New Size(192, 22)
        tsmiFile_ExploreOutput.Text = "Explorer - Output path"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(189, 6)
        ' 
        ' tsmiFile_Exit
        ' 
        tsmiFile_Exit.Name = "tsmiFile_Exit"
        tsmiFile_Exit.Size = New Size(192, 22)
        tsmiFile_Exit.Text = "Exit"
        ' 
        ' tsmiProcess
        ' 
        tsmiProcess.Name = "tsmiProcess"
        tsmiProcess.Size = New Size(71, 20)
        tsmiProcess.Text = "Process ..."
        ' 
        ' tsmiPreset
        ' 
        tsmiPreset.DropDownItems.AddRange(New ToolStripItem() {tsmiProcess_QHY600Overscan, EqualCropToolStripMenuItem})
        tsmiPreset.Name = "tsmiPreset"
        tsmiPreset.Size = New Size(56, 20)
        tsmiPreset.Text = "Presets"
        ' 
        ' tsmiProcess_QHY600Overscan
        ' 
        tsmiProcess_QHY600Overscan.Name = "tsmiProcess_QHY600Overscan"
        tsmiProcess_QHY600Overscan.Size = New Size(169, 22)
        tsmiProcess_QHY600Overscan.Text = "QHY600 Overscan"
        ' 
        ' EqualCropToolStripMenuItem
        ' 
        EqualCropToolStripMenuItem.Name = "EqualCropToolStripMenuItem"
        EqualCropToolStripMenuItem.Size = New Size(169, 22)
        EqualCropToolStripMenuItem.Text = "Equal crop"
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.Location = New Point(3, 3)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(pgMain)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(tbLog)
        scMain.Size = New Size(1261, 396)
        scMain.SplitterDistance = 420
        scMain.TabIndex = 7
        ' 
        ' lbInputFiles
        ' 
        lbInputFiles.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lbInputFiles.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbInputFiles.FormattingEnabled = True
        lbInputFiles.ItemHeight = 15
        lbInputFiles.Location = New Point(3, 3)
        lbInputFiles.Name = "lbInputFiles"
        lbInputFiles.ScrollAlwaysVisible = True
        lbInputFiles.Size = New Size(1261, 394)
        lbInputFiles.TabIndex = 8
        ' 
        ' scTop
        ' 
        scTop.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scTop.Location = New Point(12, 40)
        scTop.Name = "scTop"
        scTop.Orientation = Orientation.Horizontal
        ' 
        ' scTop.Panel1
        ' 
        scTop.Panel1.Controls.Add(lbInputFiles)
        ' 
        ' scTop.Panel2
        ' 
        scTop.Panel2.Controls.Add(scMain)
        scTop.Size = New Size(1267, 812)
        scTop.SplitterDistance = 406
        scTop.TabIndex = 9
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1291, 864)
        Controls.Add(scTop)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Name = "Form1"
        Text = "Astro Image Converter"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        scMain.Panel2.PerformLayout()
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        scTop.Panel1.ResumeLayout(False)
        scTop.Panel2.ResumeLayout(False)
        CType(scTop, ComponentModel.ISupportInitialize).EndInit()
        scTop.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents tbLog As TextBox
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_FITSWork As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_Exit As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenOutput As ToolStripMenuItem
    Friend WithEvents tsmiProcess As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenOriginal As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents tsmiPreset As ToolStripMenuItem
    Friend WithEvents tsmiProcess_QHY600Overscan As ToolStripMenuItem
    Friend WithEvents EqualCropToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lbInputFiles As ListBox
    Friend WithEvents scTop As SplitContainer
    Friend WithEvents tsmiFile_OpenOriginal_Default As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenOriginal_FITSWork As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenOutput_Default As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenOutput_FITSWork As ToolStripMenuItem
    Friend WithEvents tsmiFile_ExploreOutput As ToolStripMenuItem

End Class
