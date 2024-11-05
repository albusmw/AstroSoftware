<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_Open = New ToolStripMenuItem()
        tsmiOp = New ToolStripMenuItem()
        tsmiOp_Transpose = New ToolStripMenuItem()
        ofdMain = New OpenFileDialog()
        tbFITSHeader = New TextBox()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiOp})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1067, 24)
        msMain.TabIndex = 0
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_Open})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(37, 20)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_Open
        ' 
        tsmiFile_Open.Name = "tsmiFile_Open"
        tsmiFile_Open.Size = New Size(103, 22)
        tsmiFile_Open.Text = "Open"
        ' 
        ' tsmiOp
        ' 
        tsmiOp.DropDownItems.AddRange(New ToolStripItem() {tsmiOp_Transpose})
        tsmiOp.Name = "tsmiOp"
        tsmiOp.Size = New Size(77, 20)
        tsmiOp.Text = "Operations"
        ' 
        ' tsmiOp_Transpose
        ' 
        tsmiOp_Transpose.Name = "tsmiOp_Transpose"
        tsmiOp_Transpose.Size = New Size(180, 22)
        tsmiOp_Transpose.Text = "Transpose file"
        ' 
        ' tbFITSHeader
        ' 
        tbFITSHeader.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbFITSHeader.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbFITSHeader.Location = New Point(12, 27)
        tbFITSHeader.Multiline = True
        tbFITSHeader.Name = "tbFITSHeader"
        tbFITSHeader.ScrollBars = ScrollBars.Both
        tbFITSHeader.Size = New Size(1043, 663)
        tbFITSHeader.TabIndex = 1
        tbFITSHeader.WordWrap = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1067, 702)
        Controls.Add(tbFITSHeader)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Name = "Form1"
        Text = "MyFITS"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_Open As ToolStripMenuItem
    Friend WithEvents ofdMain As OpenFileDialog
    Friend WithEvents tbFITSHeader As TextBox
    Friend WithEvents tsmiOp As ToolStripMenuItem
    Friend WithEvents tsmiOp_Transpose As ToolStripMenuItem

End Class
