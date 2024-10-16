<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInView
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
        pgMain = New PropertyGrid()
        zgcMain = New ZedGraph.ZedGraphControl()
        ssMain = New StatusStrip()
        tsslMain = New ToolStripStatusLabel()
        msMain = New MenuStrip()
        tsmiTime = New ToolStripMenuItem()
        tsmiTime_Today = New ToolStripMenuItem()
        tsmiTime_ThisMonth = New ToolStripMenuItem()
        tsmiTime_Next365Days = New ToolStripMenuItem()
        tsmiTime_NextDay = New ToolStripMenuItem()
        tsmiGenerate = New ToolStripMenuItem()
        tsmiGenerate_VisImage = New ToolStripMenuItem()
        tsmiGenerate_ExcelExport = New ToolStripMenuItem()
        ssMain.SuspendLayout()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' pgMain
        ' 
        pgMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        pgMain.Location = New Point(12, 27)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(309, 704)
        pgMain.TabIndex = 0
        ' 
        ' zgcMain
        ' 
        zgcMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        zgcMain.Location = New Point(328, 27)
        zgcMain.Margin = New Padding(4, 3, 4, 3)
        zgcMain.Name = "zgcMain"
        zgcMain.ScrollGrace = 0R
        zgcMain.ScrollMaxX = 0R
        zgcMain.ScrollMaxY = 0R
        zgcMain.ScrollMaxY2 = 0R
        zgcMain.ScrollMinX = 0R
        zgcMain.ScrollMinY = 0R
        zgcMain.ScrollMinY2 = 0R
        zgcMain.Size = New Size(848, 704)
        zgcMain.TabIndex = 1
        ' 
        ' ssMain
        ' 
        ssMain.Items.AddRange(New ToolStripItem() {tsslMain})
        ssMain.Location = New Point(0, 734)
        ssMain.Name = "ssMain"
        ssMain.Size = New Size(1189, 22)
        ssMain.TabIndex = 2
        ssMain.Text = "StatusStrip1"
        ' 
        ' tsslMain
        ' 
        tsslMain.Name = "tsslMain"
        tsslMain.Size = New Size(22, 17)
        tsslMain.Text = "---"
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiTime, tsmiGenerate})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1189, 24)
        msMain.TabIndex = 3
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiTime
        ' 
        tsmiTime.DropDownItems.AddRange(New ToolStripItem() {tsmiTime_Today, tsmiTime_ThisMonth, tsmiTime_Next365Days, tsmiTime_NextDay})
        tsmiTime.Name = "tsmiTime"
        tsmiTime.Size = New Size(85, 20)
        tsmiTime.Text = "Time presets"
        ' 
        ' tsmiTime_Today
        ' 
        tsmiTime_Today.Name = "tsmiTime_Today"
        tsmiTime_Today.Size = New Size(166, 22)
        tsmiTime_Today.Text = "Today"
        ' 
        ' tsmiTime_ThisMonth
        ' 
        tsmiTime_ThisMonth.Name = "tsmiTime_ThisMonth"
        tsmiTime_ThisMonth.Size = New Size(166, 22)
        tsmiTime_ThisMonth.Text = "This month"
        ' 
        ' tsmiTime_Next365Days
        ' 
        tsmiTime_Next365Days.Name = "tsmiTime_Next365Days"
        tsmiTime_Next365Days.Size = New Size(166, 22)
        tsmiTime_Next365Days.Text = "Next 365 days"
        ' 
        ' tsmiTime_NextDay
        ' 
        tsmiTime_NextDay.Name = "tsmiTime_NextDay"
        tsmiTime_NextDay.ShortcutKeys = Keys.Control Or Keys.N
        tsmiTime_NextDay.Size = New Size(166, 22)
        tsmiTime_NextDay.Text = "Next day"
        ' 
        ' tsmiGenerate
        ' 
        tsmiGenerate.DropDownItems.AddRange(New ToolStripItem() {tsmiGenerate_VisImage, tsmiGenerate_ExcelExport})
        tsmiGenerate.Name = "tsmiGenerate"
        tsmiGenerate.Size = New Size(66, 20)
        tsmiGenerate.Text = "Generate"
        ' 
        ' tsmiGenerate_VisImage
        ' 
        tsmiGenerate_VisImage.Name = "tsmiGenerate_VisImage"
        tsmiGenerate_VisImage.Size = New Size(180, 22)
        tsmiGenerate_VisImage.Text = "Visibility image"
        ' 
        ' tsmiGenerate_ExcelExport
        ' 
        tsmiGenerate_ExcelExport.Name = "tsmiGenerate_ExcelExport"
        tsmiGenerate_ExcelExport.Size = New Size(180, 22)
        tsmiGenerate_ExcelExport.Text = "Excel export"
        ' 
        ' frmInView
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1189, 756)
        Controls.Add(ssMain)
        Controls.Add(msMain)
        Controls.Add(zgcMain)
        Controls.Add(pgMain)
        MainMenuStrip = msMain
        Name = "frmInView"
        Text = "In View Display"
        ssMain.ResumeLayout(False)
        ssMain.PerformLayout()
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents zgcMain As ZedGraph.ZedGraphControl
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiTime As ToolStripMenuItem
    Friend WithEvents tsmiTime_ThisMonth As ToolStripMenuItem
    Friend WithEvents tsmiTime_Next365Days As ToolStripMenuItem
    Friend WithEvents tsslMain As ToolStripStatusLabel
    Friend WithEvents tsmiTime_Today As ToolStripMenuItem
    Friend WithEvents tsmiTime_NextDay As ToolStripMenuItem
    Friend WithEvents tsmiGenerate As ToolStripMenuItem
    Friend WithEvents tsmiGenerate_VisImage As ToolStripMenuItem
    Friend WithEvents tsmiGenerate_ExcelExport As ToolStripMenuItem
End Class
