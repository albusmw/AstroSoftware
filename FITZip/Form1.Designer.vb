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
        lbFiles = New ListBox()
        btnCompress = New Button()
        ssMain = New StatusStrip()
        tsslStatus = New ToolStripStatusLabel()
        tbLog = New TextBox()
        scMain = New SplitContainer()
        ssMain.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' lbFiles
        ' 
        lbFiles.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lbFiles.Font = New Font("Courier New", 9.75F)
        lbFiles.FormattingEnabled = True
        lbFiles.IntegralHeight = False
        lbFiles.Location = New Point(3, 3)
        lbFiles.Name = "lbFiles"
        lbFiles.Size = New Size(1094, 282)
        lbFiles.TabIndex = 0
        ' 
        ' btnCompress
        ' 
        btnCompress.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCompress.Location = New Point(12, 595)
        btnCompress.Name = "btnCompress"
        btnCompress.Size = New Size(111, 45)
        btnCompress.TabIndex = 1
        btnCompress.Text = "Compress"
        btnCompress.UseVisualStyleBackColor = True
        ' 
        ' ssMain
        ' 
        ssMain.Items.AddRange(New ToolStripItem() {tsslStatus})
        ssMain.Location = New Point(0, 643)
        ssMain.Name = "ssMain"
        ssMain.Size = New Size(1124, 22)
        ssMain.TabIndex = 2
        ssMain.Text = "StatusStrip1"
        ' 
        ' tsslStatus
        ' 
        tsslStatus.Name = "tsslStatus"
        tsslStatus.Size = New Size(30, 17)
        tsslStatus.Text = "IDLE"
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Font = New Font("Courier New", 9.75F)
        tbLog.Location = New Point(3, 3)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(1094, 279)
        tbLog.TabIndex = 3
        tbLog.WordWrap = False
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.Location = New Point(12, 12)
        scMain.Name = "scMain"
        scMain.Orientation = Orientation.Horizontal
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(lbFiles)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(tbLog)
        scMain.Size = New Size(1100, 577)
        scMain.SplitterDistance = 288
        scMain.TabIndex = 4
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1124, 665)
        Controls.Add(scMain)
        Controls.Add(ssMain)
        Controls.Add(btnCompress)
        Name = "Form1"
        Text = "FITZip - ZIP FITS files"
        ssMain.ResumeLayout(False)
        ssMain.PerformLayout()
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        scMain.Panel2.PerformLayout()
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lbFiles As ListBox
    Friend WithEvents btnCompress As Button
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslStatus As ToolStripStatusLabel
    Friend WithEvents tbLog As TextBox
    Friend WithEvents scMain As SplitContainer

End Class
