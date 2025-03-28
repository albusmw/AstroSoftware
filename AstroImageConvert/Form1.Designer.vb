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
        tbInputFile = New TextBox()
        tbLog = New TextBox()
        pgMain = New PropertyGrid()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_OpenOriginal = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        tsmiFile_Open = New ToolStripMenuItem()
        tsmiFile_FITSWork = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiFile_Exit = New ToolStripMenuItem()
        tsmiProcess = New ToolStripMenuItem()
        scMain = New SplitContainer()
        tsmiPreset = New ToolStripMenuItem()
        tsmiProcess_QHY600Overscan = New ToolStripMenuItem()
        msMain.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbInputFile
        ' 
        tbInputFile.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbInputFile.Location = New Point(12, 27)
        tbInputFile.Name = "tbInputFile"
        tbInputFile.Size = New Size(1297, 23)
        tbInputFile.TabIndex = 0
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Font = New Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbLog.Location = New Point(3, 3)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(855, 917)
        tbLog.TabIndex = 3
        tbLog.WordWrap = False
        ' 
        ' pgMain
        ' 
        pgMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgMain.Location = New Point(3, 3)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(426, 917)
        pgMain.TabIndex = 5
        pgMain.ToolbarVisible = False
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiProcess, tsmiPreset})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1321, 24)
        msMain.TabIndex = 6
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_OpenOriginal, ToolStripMenuItem2, tsmiFile_Open, tsmiFile_FITSWork, ToolStripMenuItem1, tsmiFile_Exit})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(37, 20)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_OpenOriginal
        ' 
        tsmiFile_OpenOriginal.Name = "tsmiFile_OpenOriginal"
        tsmiFile_OpenOriginal.Size = New Size(217, 22)
        tsmiFile_OpenOriginal.Text = "Open original file"
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(214, 6)
        ' 
        ' tsmiFile_Open
        ' 
        tsmiFile_Open.Name = "tsmiFile_Open"
        tsmiFile_Open.Size = New Size(217, 22)
        tsmiFile_Open.Text = "Open with default software"
        ' 
        ' tsmiFile_FITSWork
        ' 
        tsmiFile_FITSWork.Name = "tsmiFile_FITSWork"
        tsmiFile_FITSWork.Size = New Size(217, 22)
        tsmiFile_FITSWork.Text = "Open with FITSWork"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(214, 6)
        ' 
        ' tsmiFile_Exit
        ' 
        tsmiFile_Exit.Name = "tsmiFile_Exit"
        tsmiFile_Exit.Size = New Size(217, 22)
        tsmiFile_Exit.Text = "Exit"
        ' 
        ' tsmiProcess
        ' 
        tsmiProcess.Name = "tsmiProcess"
        tsmiProcess.Size = New Size(71, 20)
        tsmiProcess.Text = "Process ..."
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.Location = New Point(12, 56)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(pgMain)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(tbLog)
        scMain.Size = New Size(1297, 923)
        scMain.SplitterDistance = 432
        scMain.TabIndex = 7
        ' 
        ' tsmiPreset
        ' 
        tsmiPreset.DropDownItems.AddRange(New ToolStripItem() {tsmiProcess_QHY600Overscan})
        tsmiPreset.Name = "tsmiPreset"
        tsmiPreset.Size = New Size(56, 20)
        tsmiPreset.Text = "Presets"
        ' 
        ' tsmiProcess_QHY600Overscan
        ' 
        tsmiProcess_QHY600Overscan.Name = "tsmiProcess_QHY600Overscan"
        tsmiProcess_QHY600Overscan.Size = New Size(180, 22)
        tsmiProcess_QHY600Overscan.Text = "QHY600 Overscan"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1321, 991)
        Controls.Add(scMain)
        Controls.Add(tbInputFile)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Name = "Form1"
        Text = "Form1"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        scMain.Panel2.PerformLayout()
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tbInputFile As TextBox
    Friend WithEvents tbLog As TextBox
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_FITSWork As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_Exit As ToolStripMenuItem
    Friend WithEvents tsmiFile_Open As ToolStripMenuItem
    Friend WithEvents tsmiProcess As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenOriginal As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents tsmiPreset As ToolStripMenuItem
    Friend WithEvents tsmiProcess_QHY600Overscan As ToolStripMenuItem

End Class
