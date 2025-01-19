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
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiTime_Recalc = New ToolStripMenuItem()
        tsmiGenerate = New ToolStripMenuItem()
        tsmiGenerate_VisImage = New ToolStripMenuItem()
        tsmiGenerate_ExcelExport = New ToolStripMenuItem()
        scMain = New SplitContainer()
        pgDispProp = New PropertyGrid()
        tsmiTime_Night = New ToolStripMenuItem()
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
        pgCalcProp.Location = New Point(3, 3)
        pgCalcProp.Name = "pgCalcProp"
        pgCalcProp.Size = New Size(303, 345)
        pgCalcProp.TabIndex = 0
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
        ssMain.ImageScalingSize = New Size(20, 20)
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
        msMain.ImageScalingSize = New Size(20, 20)
        msMain.Items.AddRange(New ToolStripItem() {tsmiTime, tsmiGenerate})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1189, 24)
        msMain.TabIndex = 3
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiTime
        ' 
        tsmiTime.DropDownItems.AddRange(New ToolStripItem() {tsmiTime_Night, tsmiTime_Today, tsmiTime_ThisMonth, tsmiTime_Next365Days, tsmiTime_NextDay, tsmiTime_PrevDay, ToolStripMenuItem1, tsmiTime_Recalc})
        tsmiTime.Name = "tsmiTime"
        tsmiTime.Size = New Size(86, 20)
        tsmiTime.Text = "Time presets"
        ' 
        ' tsmiTime_Today
        ' 
        tsmiTime_Today.Name = "tsmiTime_Today"
        tsmiTime_Today.Size = New Size(184, 22)
        tsmiTime_Today.Text = "Today"
        ' 
        ' tsmiTime_ThisMonth
        ' 
        tsmiTime_ThisMonth.Name = "tsmiTime_ThisMonth"
        tsmiTime_ThisMonth.Size = New Size(184, 22)
        tsmiTime_ThisMonth.Text = "This month"
        ' 
        ' tsmiTime_Next365Days
        ' 
        tsmiTime_Next365Days.Name = "tsmiTime_Next365Days"
        tsmiTime_Next365Days.Size = New Size(184, 22)
        tsmiTime_Next365Days.Text = "Next 365 days"
        ' 
        ' tsmiTime_NextDay
        ' 
        tsmiTime_NextDay.Name = "tsmiTime_NextDay"
        tsmiTime_NextDay.ShortcutKeys = Keys.Control Or Keys.N
        tsmiTime_NextDay.Size = New Size(184, 22)
        tsmiTime_NextDay.Text = "Next day"
        ' 
        ' tsmiTime_PrevDay
        ' 
        tsmiTime_PrevDay.Name = "tsmiTime_PrevDay"
        tsmiTime_PrevDay.ShortcutKeys = Keys.Control Or Keys.P
        tsmiTime_PrevDay.Size = New Size(184, 22)
        tsmiTime_PrevDay.Text = "Previous day"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(181, 6)
        ' 
        ' tsmiTime_Recalc
        ' 
        tsmiTime_Recalc.Name = "tsmiTime_Recalc"
        tsmiTime_Recalc.Size = New Size(184, 22)
        tsmiTime_Recalc.Text = "Recalc only"
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
        tsmiGenerate_VisImage.Size = New Size(154, 22)
        tsmiGenerate_VisImage.Text = "Visibility image"
        ' 
        ' tsmiGenerate_ExcelExport
        ' 
        tsmiGenerate_ExcelExport.Name = "tsmiGenerate_ExcelExport"
        tsmiGenerate_ExcelExport.Size = New Size(154, 22)
        tsmiGenerate_ExcelExport.Text = "Excel export"
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        scMain.Location = New Point(12, 27)
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
        scMain.Size = New Size(309, 704)
        scMain.SplitterDistance = 351
        scMain.TabIndex = 4
        ' 
        ' pgDispProp
        ' 
        pgDispProp.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgDispProp.Location = New Point(3, 5)
        pgDispProp.Name = "pgDispProp"
        pgDispProp.Size = New Size(303, 340)
        pgDispProp.TabIndex = 5
        ' 
        ' tsmiTime_Night
        ' 
        tsmiTime_Night.Name = "tsmiTime_Night"
        tsmiTime_Night.Size = New Size(184, 22)
        tsmiTime_Night.Text = "This / next night"
        ' 
        ' frmInView
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1189, 756)
        Controls.Add(scMain)
        Controls.Add(ssMain)
        Controls.Add(msMain)
        Controls.Add(zgcMain)
        MainMenuStrip = msMain
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
    Friend WithEvents tsmiTime_Night As ToolStripMenuItem
End Class
