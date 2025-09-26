<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGetObject
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
        tbSearchString = New TextBox()
        Label1 = New Label()
        lbResults = New ListBox()
        ssMain = New StatusStrip()
        tsslLoaded = New ToolStripStatusLabel()
        tsslSelectionLength = New ToolStripStatusLabel()
        tsslObsCalcResult = New ToolStripStatusLabel()
        tspgMain = New ToolStripProgressBar()
        scMain = New SplitContainer()
        scLeft = New SplitContainer()
        pgFilter = New PropertyGrid()
        tbDetails = New TextBox()
        zgcMain = New ZedGraph.ZedGraphControl()
        tUpdateDetails = New Timer(components)
        msMain = New MenuStrip()
        tsmiData = New ToolStripMenuItem()
        tsmiData_AstroBin = New ToolStripMenuItem()
        tsmiData_SetLocation = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiData_Accept = New ToolStripMenuItem()
        tsmiTools = New ToolStripMenuItem()
        tsmiTools_InViewDisplay = New ToolStripMenuItem()
        tsmiTools_Recalc = New ToolStripMenuItem()
        tsmiTools_GetBestObjects = New ToolStripMenuItem()
        ssMain.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        CType(scLeft, ComponentModel.ISupportInitialize).BeginInit()
        scLeft.Panel1.SuspendLayout()
        scLeft.Panel2.SuspendLayout()
        scLeft.SuspendLayout()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbSearchString
        ' 
        tbSearchString.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbSearchString.Font = New Font("Courier New", 9F)
        tbSearchString.Location = New Point(94, 26)
        tbSearchString.Name = "tbSearchString"
        tbSearchString.Size = New Size(1126, 21)
        tbSearchString.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 28)
        Label1.Name = "Label1"
        Label1.Size = New Size(75, 15)
        Label1.TabIndex = 1
        Label1.Text = "Search string"
        ' 
        ' lbResults
        ' 
        lbResults.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lbResults.Font = New Font("Courier New", 9F)
        lbResults.FormattingEnabled = True
        lbResults.HorizontalScrollbar = True
        lbResults.IntegralHeight = False
        lbResults.ItemHeight = 15
        lbResults.Location = New Point(0, 0)
        lbResults.Name = "lbResults"
        lbResults.Size = New Size(354, 402)
        lbResults.TabIndex = 2
        ' 
        ' ssMain
        ' 
        ssMain.ImageScalingSize = New Size(20, 20)
        ssMain.Items.AddRange(New ToolStripItem() {tsslLoaded, tsslSelectionLength, tsslObsCalcResult, tspgMain})
        ssMain.Location = New Point(0, 912)
        ssMain.Name = "ssMain"
        ssMain.Size = New Size(1231, 22)
        ssMain.TabIndex = 3
        ssMain.Text = "StatusStrip1"
        ' 
        ' tsslLoaded
        ' 
        tsslLoaded.Name = "tsslLoaded"
        tsslLoaded.Size = New Size(22, 17)
        tsslLoaded.Text = "---"
        ' 
        ' tsslSelectionLength
        ' 
        tsslSelectionLength.Name = "tsslSelectionLength"
        tsslSelectionLength.Size = New Size(22, 17)
        tsslSelectionLength.Text = "---"
        ' 
        ' tsslObsCalcResult
        ' 
        tsslObsCalcResult.DisplayStyle = ToolStripItemDisplayStyle.Text
        tsslObsCalcResult.Name = "tsslObsCalcResult"
        tsslObsCalcResult.Size = New Size(0, 17)
        ' 
        ' tspgMain
        ' 
        tspgMain.Name = "tspgMain"
        tspgMain.Size = New Size(175, 16)
        tspgMain.Style = ProgressBarStyle.Continuous
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.FixedPanel = FixedPanel.Panel1
        scMain.Location = New Point(10, 57)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(scLeft)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(zgcMain)
        scMain.Size = New Size(1209, 846)
        scMain.SplitterDistance = 354
        scMain.TabIndex = 4
        ' 
        ' scLeft
        ' 
        scLeft.Dock = DockStyle.Fill
        scLeft.Location = New Point(0, 0)
        scLeft.Margin = New Padding(3, 2, 3, 2)
        scLeft.Name = "scLeft"
        scLeft.Orientation = Orientation.Horizontal
        ' 
        ' scLeft.Panel1
        ' 
        scLeft.Panel1.Controls.Add(pgFilter)
        scLeft.Panel1.Controls.Add(lbResults)
        ' 
        ' scLeft.Panel2
        ' 
        scLeft.Panel2.Controls.Add(tbDetails)
        scLeft.Size = New Size(354, 846)
        scLeft.SplitterDistance = 532
        scLeft.SplitterWidth = 3
        scLeft.TabIndex = 3
        ' 
        ' pgFilter
        ' 
        pgFilter.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgFilter.HelpVisible = False
        pgFilter.Location = New Point(0, 408)
        pgFilter.Name = "pgFilter"
        pgFilter.PropertySort = PropertySort.Alphabetical
        pgFilter.Size = New Size(354, 121)
        pgFilter.TabIndex = 3
        pgFilter.ToolbarVisible = False
        ' 
        ' tbDetails
        ' 
        tbDetails.Dock = DockStyle.Fill
        tbDetails.Font = New Font("Courier New", 9F)
        tbDetails.Location = New Point(0, 0)
        tbDetails.Multiline = True
        tbDetails.Name = "tbDetails"
        tbDetails.ScrollBars = ScrollBars.Both
        tbDetails.Size = New Size(354, 311)
        tbDetails.TabIndex = 5
        tbDetails.WordWrap = False
        ' 
        ' zgcMain
        ' 
        zgcMain.Dock = DockStyle.Fill
        zgcMain.Location = New Point(0, 0)
        zgcMain.Margin = New Padding(4, 3, 4, 3)
        zgcMain.Name = "zgcMain"
        zgcMain.ScrollGrace = 0R
        zgcMain.ScrollMaxX = 0R
        zgcMain.ScrollMaxY = 0R
        zgcMain.ScrollMaxY2 = 0R
        zgcMain.ScrollMinX = 0R
        zgcMain.ScrollMinY = 0R
        zgcMain.ScrollMinY2 = 0R
        zgcMain.Size = New Size(851, 846)
        zgcMain.TabIndex = 1
        ' 
        ' tUpdateDetails
        ' 
        tUpdateDetails.Enabled = True
        tUpdateDetails.Interval = 1000
        ' 
        ' msMain
        ' 
        msMain.ImageScalingSize = New Size(20, 20)
        msMain.Items.AddRange(New ToolStripItem() {tsmiData, tsmiTools})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1231, 24)
        msMain.TabIndex = 6
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiData
        ' 
        tsmiData.DropDownItems.AddRange(New ToolStripItem() {tsmiData_AstroBin, tsmiData_SetLocation, ToolStripMenuItem1, tsmiData_Accept})
        tsmiData.Name = "tsmiData"
        tsmiData.Size = New Size(43, 20)
        tsmiData.Text = "Data"
        ' 
        ' tsmiData_AstroBin
        ' 
        tsmiData_AstroBin.Name = "tsmiData_AstroBin"
        tsmiData_AstroBin.Size = New Size(203, 22)
        tsmiData_AstroBin.Text = "Parse AstroBin clipboard"
        ' 
        ' tsmiData_SetLocation
        ' 
        tsmiData_SetLocation.Name = "tsmiData_SetLocation"
        tsmiData_SetLocation.Size = New Size(203, 22)
        tsmiData_SetLocation.Text = "Set location"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(200, 6)
        ' 
        ' tsmiData_Accept
        ' 
        tsmiData_Accept.Name = "tsmiData_Accept"
        tsmiData_Accept.Size = New Size(203, 22)
        tsmiData_Accept.Text = "Accept and close"
        ' 
        ' tsmiTools
        ' 
        tsmiTools.DropDownItems.AddRange(New ToolStripItem() {tsmiTools_InViewDisplay, tsmiTools_Recalc, tsmiTools_GetBestObjects})
        tsmiTools.Name = "tsmiTools"
        tsmiTools.Size = New Size(42, 20)
        tsmiTools.Text = "Tool"
        ' 
        ' tsmiTools_InViewDisplay
        ' 
        tsmiTools_InViewDisplay.Name = "tsmiTools_InViewDisplay"
        tsmiTools_InViewDisplay.Size = New Size(184, 22)
        tsmiTools_InViewDisplay.Text = "Open In View display"
        ' 
        ' tsmiTools_Recalc
        ' 
        tsmiTools_Recalc.Name = "tsmiTools_Recalc"
        tsmiTools_Recalc.Size = New Size(184, 22)
        tsmiTools_Recalc.Text = "Recalc"
        ' 
        ' tsmiTools_GetBestObjects
        ' 
        tsmiTools_GetBestObjects.Name = "tsmiTools_GetBestObjects"
        tsmiTools_GetBestObjects.Size = New Size(184, 22)
        tsmiTools_GetBestObjects.Text = "Get best objects"
        ' 
        ' frmGetObject
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1231, 934)
        Controls.Add(scMain)
        Controls.Add(ssMain)
        Controls.Add(msMain)
        Controls.Add(Label1)
        Controls.Add(tbSearchString)
        MainMenuStrip = msMain
        Name = "frmGetObject"
        Text = "Object search"
        ssMain.ResumeLayout(False)
        ssMain.PerformLayout()
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        scLeft.Panel1.ResumeLayout(False)
        scLeft.Panel2.ResumeLayout(False)
        scLeft.Panel2.PerformLayout()
        CType(scLeft, ComponentModel.ISupportInitialize).EndInit()
        scLeft.ResumeLayout(False)
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tbSearchString As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lbResults As ListBox
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslLoaded As ToolStripStatusLabel
    Friend WithEvents tsslSelectionLength As ToolStripStatusLabel
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents tUpdateDetails As Timer
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiData As ToolStripMenuItem
    Friend WithEvents tsmiData_AstroBin As ToolStripMenuItem
    Friend WithEvents tsmiTools As ToolStripMenuItem
    Friend WithEvents tsmiTools_InViewDisplay As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiData_Accept As ToolStripMenuItem
    Friend WithEvents tsmiData_SetLocation As ToolStripMenuItem
    Friend WithEvents tsmiTools_Recalc As ToolStripMenuItem
    Friend WithEvents tsmiTools_GetBestObjects As ToolStripMenuItem
    Friend WithEvents tsslObsCalcResult As ToolStripStatusLabel
    Friend WithEvents tspgMain As ToolStripProgressBar
    Friend WithEvents tbDetails As TextBox
    Friend WithEvents zgcMain As ZedGraph.ZedGraphControl
    Friend WithEvents scLeft As SplitContainer
    Friend WithEvents pgFilter As PropertyGrid
End Class
