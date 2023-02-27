<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MyMainForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_OpenSerSequence = New System.Windows.Forms.ToolStripMenuItem()
        Me.ofdMain = New System.Windows.Forms.OpenFileDialog()
        Me.gbStatus = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pbImageStream = New System.Windows.Forms.ProgressBar()
        Me.tImageStream = New System.Windows.Forms.Label()
        Me.msMain.SuspendLayout()
        Me.gbStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(725, 24)
        Me.msMain.TabIndex = 0
        Me.msMain.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile_OpenSerSequence})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'tsmiFile_OpenSerSequence
        '
        Me.tsmiFile_OpenSerSequence.Name = "tsmiFile_OpenSerSequence"
        Me.tsmiFile_OpenSerSequence.Size = New System.Drawing.Size(178, 22)
        Me.tsmiFile_OpenSerSequence.Text = "Open SER sequence"
        '
        'ofdMain
        '
        Me.ofdMain.FileName = "OpenFileDialog1"
        '
        'gbStatus
        '
        Me.gbStatus.Controls.Add(Me.tImageStream)
        Me.gbStatus.Controls.Add(Me.pbImageStream)
        Me.gbStatus.Controls.Add(Me.Label1)
        Me.gbStatus.Location = New System.Drawing.Point(12, 27)
        Me.gbStatus.Name = "gbStatus"
        Me.gbStatus.Size = New System.Drawing.Size(368, 157)
        Me.gbStatus.TabIndex = 1
        Me.gbStatus.TabStop = False
        Me.gbStatus.Text = "Status"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Image stream"
        '
        'pbImageStream
        '
        Me.pbImageStream.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbImageStream.Location = New System.Drawing.Point(92, 29)
        Me.pbImageStream.Name = "pbImageStream"
        Me.pbImageStream.Size = New System.Drawing.Size(194, 13)
        Me.pbImageStream.TabIndex = 2
        '
        'tImageStream
        '
        Me.tImageStream.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tImageStream.Location = New System.Drawing.Point(292, 29)
        Me.tImageStream.Name = "tImageStream"
        Me.tImageStream.Size = New System.Drawing.Size(70, 13)
        Me.tImageStream.TabIndex = 3
        Me.tImageStream.Text = "0/?"
        Me.tImageStream.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MyMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(725, 609)
        Me.Controls.Add(Me.gbStatus)
        Me.Controls.Add(Me.msMain)
        Me.MainMenuStrip = Me.msMain
        Me.Name = "MyMainForm"
        Me.Text = "FocusAnalysis"
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.gbStatus.ResumeLayout(False)
        Me.gbStatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents msMain As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenSerSequence As ToolStripMenuItem
    Friend WithEvents ofdMain As OpenFileDialog
    Friend WithEvents gbStatus As GroupBox
    Friend WithEvents tImageStream As Label
    Friend WithEvents pbImageStream As ProgressBar
    Friend WithEvents Label1 As Label
End Class
