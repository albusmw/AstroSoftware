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
        pgCalcProp = New PropertyGrid()
        zgcMain = New ZedGraph.ZedGraphControl()
        ssMain = New StatusStrip()
        tsslMain = New ToolStripStatusLabel()
        msMain = New MenuStrip()
        tsmiTime = New ToolStripMenuItem()
        tsmiTime_Today = New ToolStripMenuItem()
        tsmiTime_ThisMonth = New ToolStripMenuItem()
        tsmiTime_Next365Days = New ToolStripMenuItem()
        tsmiTime_NextDay = New ToolStripMenuItem()
        tsmiTime_PrevDay = New ToolStripMenuItem()
        tsmiGenerate = New ToolStripMenuItem()
        tsmiGenerate_VisImage = New ToolStripMenuItem()
        tsmiGenerate_ExcelExport = New ToolStripMenuItem()
        scMain = New SplitContainer()
        pgDispProp = New PropertyGrid()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiTime_Recalc = New ToolStripMenuItem()
        ssMain.SuspendLayout()
        msMain.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' pgCalcProp
        ' 
        pgCalcProp.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgCalcProp.Location = New Point(3, 4)
        pgCalcProp.Margin = New Padding(3, 4, 3, 4)
        pgCalcProp.Name = "pgCalcProp"
        pgCalcProp.Size = New Size(346, 461)
        pgCalcProp.TabIndex = 0
        ' 
        ' zgcMain
        ' 
        zgcMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        zgcMain.Location = New Point(375, 36)
        zgcMain.Margin = New Padding(5, 4, 5, 4)
        zgcMain.Name = "zgcMain"
        zgcMain.ScrollGrace = 0R
        zgcMain.ScrollMaxX = 0R
        zgcMain.ScrollMaxY = 0R
        zgcMain.ScrollMaxY2 = 0R
        zgcMain.ScrollMinX = 0R
        zgcMain.ScrollMinY = 0R
        zgcMain.ScrollMinY2 = 0R
        zgcMain.Size = New Size(969, 939)
        zgcMain.TabIndex = 1
        ' 
        ' ssMain
        ' 
        ssMain.ImageScalingSize = New Size(20, 20)
        ssMain.Items.AddRange(New ToolStripItem() {tsslMain})
        ssMain.Location = New Point(0, 982)
        ssMain.Name = "ssMain"
        ssMain.Padding = New Padding(1, 0, 16, 0)
        ssMain.Size = New Size(1359, 26)
        ssMain.TabIndex = 2
        ssMain.Text = "StatusStrip1"
        ' 
        ' tsslMain
        ' 
        tsslMain.Name = "tsslMain"
        tsslMain.Size = New Size(27, 20)
        tsslMain.Text = "---"
        ' 
        ' msMain
        ' 
        msMain.ImageScalingSize = New Size(20, 20)
        msMain.Items.AddRange(New ToolStripItem() {tsmiTime, tsmiGenerate})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Padding = New Padding(7, 3, 0, 3)
        msMain.Size = New Size(1359, 30)
        msMain.TabIndex = 3
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiTime
        ' 
        tsmiTime.DropDownItems.AddRange(New ToolStripItem() {tsmiTime_Today, tsmiTime_ThisMonth, tsmiTime_Next365Days, tsmiTime_NextDay, tsmiTime_PrevDay, ToolStripMenuItem1, tsmiTime_Recalc})
        tsmiTime.Name = "tsmiTime"
        tsmiTime.Size = New Size(107, 24)
        tsmiTime.Text = "Time presets"
        ' 
        ' tsmiTime_Today
        ' 
        tsmiTime_Today.Name = "tsmiTime_Today"
        tsmiTime_Today.Size = New Size(229, 26)
        tsmiTime_Today.Text = "Today"
        ' 
        ' tsmiTime_ThisMonth
        ' 
        tsmiTime_ThisMonth.Name = "tsmiTime_ThisMonth"
        tsmiTime_ThisMonth.Size = New Size(229, 26)
        tsmiTime_ThisMonth.Text = "This month"
        ' 
        ' tsmiTime_Next365Days
        ' 
        tsmiTime_Next365Days.Name = "tsmiTime_Next365Days"
        tsmiTime_Next365Days.Size = New Size(229, 26)
        tsmiTime_Next365Days.Text = "Next 365 days"
        ' 
        ' tsmiTime_NextDay
        ' 
        tsmiTime_NextDay.Name = "tsmiTime_NextDay"
        tsmiTime_NextDay.ShortcutKeys = Keys.Control Or Keys.N
        tsmiTime_NextDay.Size = New Size(229, 26)
        tsmiTime_NextDay.Text = "Next day"
        ' 
        ' tsmiTime_PrevDay
        ' 
        tsmiTime_PrevDay.Name = "tsmiTime_PrevDay"
        tsmiTime_PrevDay.ShortcutKeys = Keys.Control Or Keys.P
        tsmiTime_PrevDay.Size = New Size(229, 26)
        tsmiTime_PrevDay.Text = "Previous day"
        ' 
        ' tsmiGenerate
        ' 
        tsmiGenerate.DropDownItems.AddRange(New ToolStripItem() {tsmiGenerate_VisImage, tsmiGenerate_ExcelExport})
        tsmiGenerate.Name = "tsmiGenerate"
        tsmiGenerate.Size = New Size(83, 24)
        tsmiGenerate.Text = "Generate"
        ' 
        ' tsmiGenerate_VisImage
        ' 
        tsmiGenerate_VisImage.Name = "tsmiGenerate_VisImage"
        tsmiGenerate_VisImage.Size = New Size(194, 26)
        tsmiGenerate_VisImage.Text = "Visibility image"
        ' 
        ' tsmiGenerate_ExcelExport
        ' 
        tsmiGenerate_ExcelExport.Name = "tsmiGenerate_ExcelExport"
        tsmiGenerate_ExcelExport.Size = New Size(194, 26)
        tsmiGenerate_ExcelExport.Text = "Excel export"
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        scMain.Location = New Point(14, 36)
        scMain.Margin = New Padding(3, 4, 3, 4)
        scMain.Name = "scMain"
        scMain.Orientation = Orientation.Horizontal
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(pgCalcProp)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(pgDispProp)
        scMain.Size = New Size(353, 939)
        scMain.SplitterDistance = 469
        scMain.SplitterWidth = 5
        scMain.TabIndex = 4
        ' 
        ' pgDispProp
        ' 
        pgDispProp.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgDispProp.Location = New Point(3, 7)
        pgDispProp.Margin = New Padding(3, 4, 3, 4)
        pgDispProp.Name = "pgDispProp"
        pgDispProp.Size = New Size(346, 454)
        pgDispProp.TabIndex = 5
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(226, 6)
        ' 
        ' tsmiTime_Recalc
        ' 
        tsmiTime_Recalc.Name = "tsmiTime_Recalc"
        tsmiTime_Recalc.Size = New Size(229, 26)
        tsmiTime_Recalc.Text = "Recalc only"
        ' 
        ' frmInView
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1359, 1008)
        Controls.Add(scMain)
        Controls.Add(ssMain)
        Controls.Add(msMain)
        Controls.Add(zgcMain)
        MainMenuStrip = msMain
        Margin = New Padding(3, 4, 3, 4)
        Name = "frmInView"
        Text = "In View Display"
        ssMain.ResumeLayout(False)
        ssMain.PerformLayout()
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pgCalcProp As PropertyGrid
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
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents pgDispProp As PropertyGrid
    Friend WithEvents tsmiTime_PrevDay As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiTime_Recalc As ToolStripMenuItem
End Class
