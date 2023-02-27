<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MyMainForm
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
        Me.btnRunTest = New System.Windows.Forms.Button()
        Me.tbLogOutput = New System.Windows.Forms.TextBox()
        Me.btnClearLog = New System.Windows.Forms.Button()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.scLeft = New System.Windows.Forms.SplitContainer()
        Me.gpProperties = New System.Windows.Forms.GroupBox()
        Me.tcProperties = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.pgConfig = New System.Windows.Forms.PropertyGrid()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pgGraphics = New System.Windows.Forms.PropertyGrid()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbTotalFrames = New System.Windows.Forms.TextBox()
        Me.gbOutputs = New System.Windows.Forms.GroupBox()
        Me.tcOutputs = New System.Windows.Forms.TabControl()
        Me.tpStatistics = New System.Windows.Forms.TabPage()
        Me.tbStatOutput = New System.Windows.Forms.TextBox()
        Me.tpLog = New System.Windows.Forms.TabPage()
        Me.scRight = New System.Windows.Forms.SplitContainer()
        Me.btnStopTest = New System.Windows.Forms.Button()
        Me.btnResetStatHold = New System.Windows.Forms.Button()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsslCaptureProgress = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        CType(Me.scLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scLeft.Panel1.SuspendLayout()
        Me.scLeft.Panel2.SuspendLayout()
        Me.scLeft.SuspendLayout()
        Me.gpProperties.SuspendLayout()
        Me.tcProperties.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.gbOutputs.SuspendLayout()
        Me.tcOutputs.SuspendLayout()
        Me.tpStatistics.SuspendLayout()
        Me.tpLog.SuspendLayout()
        CType(Me.scRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scRight.SuspendLayout()
        Me.ssMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRunTest
        '
        Me.btnRunTest.Location = New System.Drawing.Point(12, 12)
        Me.btnRunTest.Name = "btnRunTest"
        Me.btnRunTest.Size = New System.Drawing.Size(75, 23)
        Me.btnRunTest.TabIndex = 0
        Me.btnRunTest.Text = "Run"
        Me.btnRunTest.UseVisualStyleBackColor = True
        '
        'tbLogOutput
        '
        Me.tbLogOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLogOutput.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tbLogOutput.Location = New System.Drawing.Point(6, 6)
        Me.tbLogOutput.Multiline = True
        Me.tbLogOutput.Name = "tbLogOutput"
        Me.tbLogOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLogOutput.Size = New System.Drawing.Size(412, 298)
        Me.tbLogOutput.TabIndex = 1
        Me.tbLogOutput.WordWrap = False
        '
        'btnClearLog
        '
        Me.btnClearLog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearLog.Location = New System.Drawing.Point(3, 306)
        Me.btnClearLog.Name = "btnClearLog"
        Me.btnClearLog.Size = New System.Drawing.Size(415, 23)
        Me.btnClearLog.TabIndex = 2
        Me.btnClearLog.Text = "Clear log"
        Me.btnClearLog.UseVisualStyleBackColor = True
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(12, 41)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.scLeft)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.scRight)
        Me.scMain.Size = New System.Drawing.Size(1373, 1095)
        Me.scMain.SplitterDistance = 456
        Me.scMain.TabIndex = 3
        '
        'scLeft
        '
        Me.scLeft.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scLeft.Location = New System.Drawing.Point(3, 3)
        Me.scLeft.Name = "scLeft"
        Me.scLeft.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scLeft.Panel1
        '
        Me.scLeft.Panel1.Controls.Add(Me.gpProperties)
        '
        'scLeft.Panel2
        '
        Me.scLeft.Panel2.Controls.Add(Me.gbOutputs)
        Me.scLeft.Size = New System.Drawing.Size(450, 1089)
        Me.scLeft.SplitterDistance = 691
        Me.scLeft.TabIndex = 3
        '
        'gpProperties
        '
        Me.gpProperties.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gpProperties.Controls.Add(Me.tcProperties)
        Me.gpProperties.Controls.Add(Me.Label2)
        Me.gpProperties.Controls.Add(Me.tbTotalFrames)
        Me.gpProperties.Location = New System.Drawing.Point(3, 3)
        Me.gpProperties.Name = "gpProperties"
        Me.gpProperties.Size = New System.Drawing.Size(444, 685)
        Me.gpProperties.TabIndex = 0
        Me.gpProperties.TabStop = False
        Me.gpProperties.Text = "Configuration"
        '
        'tcProperties
        '
        Me.tcProperties.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcProperties.Controls.Add(Me.TabPage1)
        Me.tcProperties.Controls.Add(Me.TabPage2)
        Me.tcProperties.Location = New System.Drawing.Point(6, 173)
        Me.tcProperties.Name = "tcProperties"
        Me.tcProperties.SelectedIndex = 0
        Me.tcProperties.Size = New System.Drawing.Size(432, 506)
        Me.tcProperties.TabIndex = 13
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.pgConfig)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(424, 478)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Generic control"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'pgConfig
        '
        Me.pgConfig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgConfig.Location = New System.Drawing.Point(3, 3)
        Me.pgConfig.Name = "pgConfig"
        Me.pgConfig.Size = New System.Drawing.Size(418, 472)
        Me.pgConfig.TabIndex = 11
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pgGraphics)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(424, 478)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Image encoding"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'pgGraphics
        '
        Me.pgGraphics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgGraphics.Location = New System.Drawing.Point(3, 3)
        Me.pgGraphics.Name = "pgGraphics"
        Me.pgGraphics.Size = New System.Drawing.Size(418, 472)
        Me.pgGraphics.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 117)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(108, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Frames per capture"
        '
        'tbTotalFrames
        '
        Me.tbTotalFrames.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbTotalFrames.Location = New System.Drawing.Point(343, 114)
        Me.tbTotalFrames.Name = "tbTotalFrames"
        Me.tbTotalFrames.Size = New System.Drawing.Size(95, 23)
        Me.tbTotalFrames.TabIndex = 2
        Me.tbTotalFrames.Text = "1000000"
        Me.tbTotalFrames.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'gbOutputs
        '
        Me.gbOutputs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbOutputs.Controls.Add(Me.tcOutputs)
        Me.gbOutputs.Location = New System.Drawing.Point(3, 3)
        Me.gbOutputs.Name = "gbOutputs"
        Me.gbOutputs.Size = New System.Drawing.Size(444, 388)
        Me.gbOutputs.TabIndex = 3
        Me.gbOutputs.TabStop = False
        Me.gbOutputs.Text = "Outputs"
        '
        'tcOutputs
        '
        Me.tcOutputs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcOutputs.Controls.Add(Me.tpStatistics)
        Me.tcOutputs.Controls.Add(Me.tpLog)
        Me.tcOutputs.Location = New System.Drawing.Point(6, 22)
        Me.tcOutputs.Name = "tcOutputs"
        Me.tcOutputs.SelectedIndex = 0
        Me.tcOutputs.Size = New System.Drawing.Size(432, 360)
        Me.tcOutputs.TabIndex = 0
        '
        'tpStatistics
        '
        Me.tpStatistics.Controls.Add(Me.tbStatOutput)
        Me.tpStatistics.Location = New System.Drawing.Point(4, 24)
        Me.tpStatistics.Name = "tpStatistics"
        Me.tpStatistics.Padding = New System.Windows.Forms.Padding(3)
        Me.tpStatistics.Size = New System.Drawing.Size(424, 332)
        Me.tpStatistics.TabIndex = 0
        Me.tpStatistics.Text = "Statistics"
        Me.tpStatistics.UseVisualStyleBackColor = True
        '
        'tbStatOutput
        '
        Me.tbStatOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbStatOutput.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tbStatOutput.Location = New System.Drawing.Point(6, 6)
        Me.tbStatOutput.Multiline = True
        Me.tbStatOutput.Name = "tbStatOutput"
        Me.tbStatOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbStatOutput.Size = New System.Drawing.Size(412, 320)
        Me.tbStatOutput.TabIndex = 2
        Me.tbStatOutput.WordWrap = False
        '
        'tpLog
        '
        Me.tpLog.Controls.Add(Me.tbLogOutput)
        Me.tpLog.Controls.Add(Me.btnClearLog)
        Me.tpLog.Location = New System.Drawing.Point(4, 24)
        Me.tpLog.Name = "tpLog"
        Me.tpLog.Padding = New System.Windows.Forms.Padding(3)
        Me.tpLog.Size = New System.Drawing.Size(424, 332)
        Me.tpLog.TabIndex = 1
        Me.tpLog.Text = "Log"
        Me.tpLog.UseVisualStyleBackColor = True
        '
        'scRight
        '
        Me.scRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scRight.Location = New System.Drawing.Point(0, 0)
        Me.scRight.Name = "scRight"
        Me.scRight.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.scRight.Size = New System.Drawing.Size(913, 1095)
        Me.scRight.SplitterDistance = 631
        Me.scRight.TabIndex = 0
        '
        'btnStopTest
        '
        Me.btnStopTest.Location = New System.Drawing.Point(93, 12)
        Me.btnStopTest.Name = "btnStopTest"
        Me.btnStopTest.Size = New System.Drawing.Size(75, 23)
        Me.btnStopTest.TabIndex = 4
        Me.btnStopTest.Text = "Stop"
        Me.btnStopTest.UseVisualStyleBackColor = True
        '
        'btnResetStatHold
        '
        Me.btnResetStatHold.Location = New System.Drawing.Point(174, 12)
        Me.btnResetStatHold.Name = "btnResetStatHold"
        Me.btnResetStatHold.Size = New System.Drawing.Size(105, 23)
        Me.btnResetStatHold.TabIndex = 5
        Me.btnResetStatHold.Text = "Reset stat hold"
        Me.btnResetStatHold.UseVisualStyleBackColor = True
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslCaptureProgress})
        Me.ssMain.Location = New System.Drawing.Point(0, 1152)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(1397, 22)
        Me.ssMain.TabIndex = 6
        Me.ssMain.Text = "StatusStrip1"
        '
        'tsslCaptureProgress
        '
        Me.tsslCaptureProgress.Name = "tsslCaptureProgress"
        Me.tsslCaptureProgress.Size = New System.Drawing.Size(22, 17)
        Me.tsslCaptureProgress.Text = "---"
        '
        'MyMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1397, 1174)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.btnResetStatHold)
        Me.Controls.Add(Me.btnStopTest)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.btnRunTest)
        Me.Name = "MyMainForm"
        Me.Text = "MainForm"
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.scLeft.Panel1.ResumeLayout(False)
        Me.scLeft.Panel2.ResumeLayout(False)
        CType(Me.scLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scLeft.ResumeLayout(False)
        Me.gpProperties.ResumeLayout(False)
        Me.gpProperties.PerformLayout()
        Me.tcProperties.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.gbOutputs.ResumeLayout(False)
        Me.tcOutputs.ResumeLayout(False)
        Me.tpStatistics.ResumeLayout(False)
        Me.tpStatistics.PerformLayout()
        Me.tpLog.ResumeLayout(False)
        Me.tpLog.PerformLayout()
        CType(Me.scRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scRight.ResumeLayout(False)
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnRunTest As Button
    Friend WithEvents tbLogOutput As TextBox
    Friend WithEvents btnClearLog As Button
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents scLeft As SplitContainer
    Friend WithEvents gpProperties As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tbTotalFrames As TextBox
    Friend WithEvents btnStopTest As Button
    Friend WithEvents scRight As SplitContainer
    Friend WithEvents btnResetStatHold As Button
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslCaptureProgress As ToolStripStatusLabel
    Friend WithEvents pgGraphics As PropertyGrid
    Friend WithEvents tcProperties As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents pgConfig As PropertyGrid
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents gbOutputs As GroupBox
    Friend WithEvents tcOutputs As TabControl
    Friend WithEvents tpStatistics As TabPage
    Friend WithEvents tbStatOutput As TextBox
    Friend WithEvents tpLog As TabPage
End Class
