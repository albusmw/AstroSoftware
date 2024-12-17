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
        lbExamples = New ListBox()
        tbLog = New TextBox()
        pgMain = New PropertyGrid()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_Open = New ToolStripMenuItem()
        tsmiFile_FITSWork = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiFile_Exit = New ToolStripMenuItem()
        tsmiProcess = New ToolStripMenuItem()
        tsmiFile_OpenOriginal = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbInputFile
        ' 
        tbInputFile.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbInputFile.Location = New Point(12, 27)
        tbInputFile.Name = "tbInputFile"
        tbInputFile.Size = New Size(1173, 23)
        tbInputFile.TabIndex = 0
        ' 
        ' lbExamples
        ' 
        lbExamples.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lbExamples.FormattingEnabled = True
        lbExamples.ItemHeight = 15
        lbExamples.Location = New Point(255, 56)
        lbExamples.Name = "lbExamples"
        lbExamples.Size = New Size(930, 184)
        lbExamples.TabIndex = 2
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Font = New Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbLog.Location = New Point(255, 246)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(930, 634)
        tbLog.TabIndex = 3
        tbLog.WordWrap = False
        ' 
        ' pgMain
        ' 
        pgMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        pgMain.Location = New Point(12, 56)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(237, 824)
        pgMain.TabIndex = 5
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiProcess})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1197, 24)
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
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1197, 892)
        Controls.Add(pgMain)
        Controls.Add(tbLog)
        Controls.Add(lbExamples)
        Controls.Add(tbInputFile)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Name = "Form1"
        Text = "Form1"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tbInputFile As TextBox
    Friend WithEvents lbExamples As ListBox
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

End Class
