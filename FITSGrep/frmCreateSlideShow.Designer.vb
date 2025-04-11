<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateSlideShow
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
        pgMain = New PropertyGrid()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_Create = New ToolStripMenuItem()
        scMain = New SplitContainer()
        lbFiles = New ListBox()
        msMain.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' pgMain
        ' 
        pgMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgMain.Location = New Point(3, 3)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(394, 749)
        pgMain.TabIndex = 0
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1224, 24)
        msMain.TabIndex = 1
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_Create})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(37, 20)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_Create
        ' 
        tsmiFile_Create.Name = "tsmiFile_Create"
        tsmiFile_Create.Size = New Size(108, 22)
        tsmiFile_Create.Text = "Create"
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.Location = New Point(12, 27)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(pgMain)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(lbFiles)
        scMain.Size = New Size(1200, 755)
        scMain.SplitterDistance = 400
        scMain.TabIndex = 2
        ' 
        ' lbFiles
        ' 
        lbFiles.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lbFiles.FormattingEnabled = True
        lbFiles.IntegralHeight = False
        lbFiles.ItemHeight = 15
        lbFiles.Location = New Point(3, 3)
        lbFiles.Name = "lbFiles"
        lbFiles.ScrollAlwaysVisible = True
        lbFiles.Size = New Size(790, 749)
        lbFiles.TabIndex = 0
        ' 
        ' frmCreateSlideShow
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1224, 794)
        Controls.Add(scMain)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Name = "frmCreateSlideShow"
        Text = "Slide Show Creator"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_Create As ToolStripMenuItem
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents lbFiles As ListBox
End Class
