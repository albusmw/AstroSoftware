Imports System.Windows.Forms

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.tUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.tbMain = New System.Windows.Forms.TextBox()
        Me.btnSwitchCooler = New System.Windows.Forms.Button()
        Me.cbFanState = New System.Windows.Forms.ComboBox()
        Me.tbTemperatureSetPoint = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsslExposing = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssbExposing = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsslExposeSeries = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslActive = New System.Windows.Forms.ToolStripStatusLabel()
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StorageRootToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MyFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSequences = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiDarkFrame = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.LoadFilterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HalphaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CalulateFITSStatisticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommandsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetROIToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConnectToFocuserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.tsbUpdate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbExpose = New System.Windows.Forms.ToolStripButton()
        Me.tsbDownload = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.pgMain = New System.Windows.Forms.PropertyGrid()
        Me.AutoFocusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.ssMain.SuspendLayout()
        Me.msMain.SuspendLayout()
        Me.tsMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tUpdate
        '
        Me.tUpdate.Interval = 1000
        '
        'tbMain
        '
        Me.tbMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbMain.Enabled = False
        Me.tbMain.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMain.Location = New System.Drawing.Point(194, 52)
        Me.tbMain.Multiline = True
        Me.tbMain.Name = "tbMain"
        Me.tbMain.Size = New System.Drawing.Size(635, 536)
        Me.tbMain.TabIndex = 1
        '
        'btnSwitchCooler
        '
        Me.btnSwitchCooler.Location = New System.Drawing.Point(91, 50)
        Me.btnSwitchCooler.Name = "btnSwitchCooler"
        Me.btnSwitchCooler.Size = New System.Drawing.Size(73, 23)
        Me.btnSwitchCooler.TabIndex = 4
        Me.btnSwitchCooler.Text = "???"
        Me.btnSwitchCooler.UseVisualStyleBackColor = True
        '
        'cbFanState
        '
        Me.cbFanState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFanState.FormattingEnabled = True
        Me.cbFanState.Items.AddRange(New Object() {"Off", "On", "Auto"})
        Me.cbFanState.Location = New System.Drawing.Point(91, 23)
        Me.cbFanState.Name = "cbFanState"
        Me.cbFanState.Size = New System.Drawing.Size(73, 21)
        Me.cbFanState.TabIndex = 5
        '
        'tbTemperatureSetPoint
        '
        Me.tbTemperatureSetPoint.Location = New System.Drawing.Point(91, 79)
        Me.tbTemperatureSetPoint.Name = "tbTemperatureSetPoint"
        Me.tbTemperatureSetPoint.Size = New System.Drawing.Size(44, 20)
        Me.tbTemperatureSetPoint.TabIndex = 6
        Me.tbTemperatureSetPoint.Text = "???"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Fan state:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Switch cooler"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "CCD Setpoint"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(141, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "° C"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbFanState)
        Me.GroupBox1.Controls.Add(Me.btnSwitchCooler)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.tbTemperatureSetPoint)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 479)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(173, 109)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cooling"
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslExposing, Me.tssbExposing, Me.tsslExposeSeries, Me.tsslActive})
        Me.ssMain.Location = New System.Drawing.Point(0, 602)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(841, 24)
        Me.ssMain.TabIndex = 16
        Me.ssMain.Text = "StatusStrip1"
        '
        'tsslExposing
        '
        Me.tsslExposing.AutoSize = False
        Me.tsslExposing.Name = "tsslExposing"
        Me.tsslExposing.Size = New System.Drawing.Size(100, 19)
        Me.tsslExposing.Text = "???"
        '
        'tssbExposing
        '
        Me.tssbExposing.Name = "tssbExposing"
        Me.tssbExposing.Size = New System.Drawing.Size(300, 18)
        Me.tssbExposing.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'tsslExposeSeries
        '
        Me.tsslExposeSeries.Name = "tsslExposeSeries"
        Me.tsslExposeSeries.Size = New System.Drawing.Size(386, 19)
        Me.tsslExposeSeries.Spring = True
        Me.tsslExposeSeries.Text = "No sequence running."
        '
        'tsslActive
        '
        Me.tsslActive.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslActive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsslActive.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsslActive.Name = "tsslActive"
        Me.tsslActive.Size = New System.Drawing.Size(38, 19)
        Me.tsslActive.Text = "COM"
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.tsmiSequences, Me.TestToolStripMenuItem, Me.CommandsToolStripMenuItem})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(841, 24)
        Me.msMain.TabIndex = 19
        Me.msMain.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StorageRootToolStripMenuItem, Me.MyFolderToolStripMenuItem})
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'StorageRootToolStripMenuItem
        '
        Me.StorageRootToolStripMenuItem.Name = "StorageRootToolStripMenuItem"
        Me.StorageRootToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.StorageRootToolStripMenuItem.Text = "Storage root"
        '
        'MyFolderToolStripMenuItem
        '
        Me.MyFolderToolStripMenuItem.Name = "MyFolderToolStripMenuItem"
        Me.MyFolderToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.MyFolderToolStripMenuItem.Text = "My folder"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(100, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'tsmiSequences
        '
        Me.tsmiSequences.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiDarkFrame, Me.ToolStripMenuItem2, Me.LoadFilterToolStripMenuItem})
        Me.tsmiSequences.Name = "tsmiSequences"
        Me.tsmiSequences.Size = New System.Drawing.Size(124, 20)
        Me.tsmiSequences.Text = "Exposure sequences"
        '
        'tsmiDarkFrame
        '
        Me.tsmiDarkFrame.Name = "tsmiDarkFrame"
        Me.tsmiDarkFrame.Size = New System.Drawing.Size(152, 22)
        Me.tsmiDarkFrame.Text = "Dark frame(s)"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(149, 6)
        '
        'LoadFilterToolStripMenuItem
        '
        Me.LoadFilterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LToolStripMenuItem, Me.RToolStripMenuItem, Me.GToolStripMenuItem, Me.BToolStripMenuItem, Me.HalphaToolStripMenuItem})
        Me.LoadFilterToolStripMenuItem.Name = "LoadFilterToolStripMenuItem"
        Me.LoadFilterToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.LoadFilterToolStripMenuItem.Text = "Load filter"
        '
        'LToolStripMenuItem
        '
        Me.LToolStripMenuItem.Name = "LToolStripMenuItem"
        Me.LToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.LToolStripMenuItem.Text = "L"
        '
        'RToolStripMenuItem
        '
        Me.RToolStripMenuItem.Name = "RToolStripMenuItem"
        Me.RToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.RToolStripMenuItem.Text = "R"
        '
        'GToolStripMenuItem
        '
        Me.GToolStripMenuItem.Name = "GToolStripMenuItem"
        Me.GToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.GToolStripMenuItem.Text = "G"
        '
        'BToolStripMenuItem
        '
        Me.BToolStripMenuItem.Name = "BToolStripMenuItem"
        Me.BToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.BToolStripMenuItem.Text = "B"
        '
        'HalphaToolStripMenuItem
        '
        Me.HalphaToolStripMenuItem.Name = "HalphaToolStripMenuItem"
        Me.HalphaToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.HalphaToolStripMenuItem.Text = "H-alpha"
        '
        'TestToolStripMenuItem
        '
        Me.TestToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CalulateFITSStatisticsToolStripMenuItem})
        Me.TestToolStripMenuItem.Name = "TestToolStripMenuItem"
        Me.TestToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.TestToolStripMenuItem.Text = "Test"
        '
        'CalulateFITSStatisticsToolStripMenuItem
        '
        Me.CalulateFITSStatisticsToolStripMenuItem.Name = "CalulateFITSStatisticsToolStripMenuItem"
        Me.CalulateFITSStatisticsToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.CalulateFITSStatisticsToolStripMenuItem.Text = "Calulate FITS statistics"
        '
        'CommandsToolStripMenuItem
        '
        Me.CommandsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetROIToolStripMenuItem, Me.ConnectToFocuserToolStripMenuItem, Me.AutoFocusToolStripMenuItem})
        Me.CommandsToolStripMenuItem.Name = "CommandsToolStripMenuItem"
        Me.CommandsToolStripMenuItem.Size = New System.Drawing.Size(81, 20)
        Me.CommandsToolStripMenuItem.Text = "Commands"
        '
        'SetROIToolStripMenuItem
        '
        Me.SetROIToolStripMenuItem.Name = "SetROIToolStripMenuItem"
        Me.SetROIToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.SetROIToolStripMenuItem.Text = "Set ROI"
        '
        'ConnectToFocuserToolStripMenuItem
        '
        Me.ConnectToFocuserToolStripMenuItem.Name = "ConnectToFocuserToolStripMenuItem"
        Me.ConnectToFocuserToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ConnectToFocuserToolStripMenuItem.Text = "Connect to Focuser"
        '
        'tsMain
        '
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbUpdate, Me.ToolStripSeparator1, Me.tsbExpose, Me.tsbDownload, Me.ToolStripSeparator2})
        Me.tsMain.Location = New System.Drawing.Point(0, 24)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.Size = New System.Drawing.Size(841, 25)
        Me.tsMain.TabIndex = 24
        Me.tsMain.Text = "ToolStrip1"
        '
        'tsbUpdate
        '
        Me.tsbUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbUpdate.Image = CType(resources.GetObject("tsbUpdate.Image"), System.Drawing.Image)
        Me.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpdate.Name = "tsbUpdate"
        Me.tsbUpdate.Size = New System.Drawing.Size(49, 22)
        Me.tsbUpdate.Text = "Update"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tsbExpose
        '
        Me.tsbExpose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbExpose.Image = CType(resources.GetObject("tsbExpose.Image"), System.Drawing.Image)
        Me.tsbExpose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExpose.Name = "tsbExpose"
        Me.tsbExpose.Size = New System.Drawing.Size(47, 22)
        Me.tsbExpose.Text = "Expose"
        '
        'tsbDownload
        '
        Me.tsbDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbDownload.Enabled = False
        Me.tsbDownload.Image = CType(resources.GetObject("tsbDownload.Image"), System.Drawing.Image)
        Me.tsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDownload.Name = "tsbDownload"
        Me.tsbDownload.Size = New System.Drawing.Size(101, 22)
        Me.tsbDownload.Text = "Download image"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'pgMain
        '
        Me.pgMain.HelpVisible = False
        Me.pgMain.Location = New System.Drawing.Point(10, 52)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(178, 421)
        Me.pgMain.TabIndex = 25
        Me.pgMain.ToolbarVisible = False
        '
        'AutoFocusToolStripMenuItem
        '
        Me.AutoFocusToolStripMenuItem.Name = "AutoFocusToolStripMenuItem"
        Me.AutoFocusToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.AutoFocusToolStripMenuItem.Text = "Auto focus"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(841, 626)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.tsMain)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.msMain)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.tbMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.msMain
        Me.Name = "Form1"
        Me.Text = "SBIG Ethernet Control"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tUpdate As System.Windows.Forms.Timer
    Friend WithEvents tbMain As System.Windows.Forms.TextBox
    Friend WithEvents btnSwitchCooler As Button
    Friend WithEvents cbFanState As ComboBox
    Friend WithEvents tbTemperatureSetPoint As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ssMain As System.Windows.Forms.StatusStrip
    Friend WithEvents tssbExposing As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents tsslExposing As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsslExposeSeries As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents msMain As System.Windows.Forms.MenuStrip
    Friend WithEvents TestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CalulateFITSStatisticsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsMain As ToolStrip
    Friend WithEvents tsbUpdate As ToolStripButton
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents tsslActive As ToolStripStatusLabel
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StorageRootToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsbExpose As ToolStripButton
    Friend WithEvents MyFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsbDownload As ToolStripButton
    Friend WithEvents tsmiSequences As ToolStripMenuItem
    Friend WithEvents tsmiDarkFrame As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents LoadFilterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HalphaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents CommandsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetROIToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConnectToFocuserToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AutoFocusToolStripMenuItem As ToolStripMenuItem
End Class
