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
        scMain = New SplitContainer()
        cbCustomOnly = New CheckBox()
        scRight = New SplitContainer()
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
        ssMain.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        CType(scRight, ComponentModel.ISupportInitialize).BeginInit()
        scRight.Panel1.SuspendLayout()
        scRight.Panel2.SuspendLayout()
        scRight.SuspendLayout()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbSearchString
        ' 
        tbSearchString.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbSearchString.Font = New Font("Courier New", 9F)
        tbSearchString.Location = New Point(93, 24)
        tbSearchString.Name = "tbSearchString"
        tbSearchString.Size = New Size(1303, 21)
        tbSearchString.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 24)
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
        lbResults.Location = New Point(3, 37)
        lbResults.Name = "lbResults"
        lbResults.Size = New Size(315, 1067)
        lbResults.TabIndex = 2
        ' 
        ' ssMain
        ' 
        ssMain.ImageScalingSize = New Size(20, 20)
        ssMain.Items.AddRange(New ToolStripItem() {tsslLoaded, tsslSelectionLength})
        ssMain.Location = New Point(0, 1164)
        ssMain.Name = "ssMain"
        ssMain.Size = New Size(1408, 22)
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
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.FixedPanel = FixedPanel.Panel1
        scMain.Location = New Point(12, 51)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(cbCustomOnly)
        scMain.Panel1.Controls.Add(lbResults)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(scRight)
        scMain.Size = New Size(1384, 1110)
        scMain.SplitterDistance = 321
        scMain.TabIndex = 4
        ' 
        ' cbCustomOnly
        ' 
        cbCustomOnly.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cbCustomOnly.Location = New Point(3, 8)
        cbCustomOnly.Name = "cbCustomOnly"
        cbCustomOnly.Size = New Size(315, 23)
        cbCustomOnly.TabIndex = 3
        cbCustomOnly.Text = "Only custom catalog"
        cbCustomOnly.UseVisualStyleBackColor = True
        ' 
        ' scRight
        ' 
        scRight.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scRight.FixedPanel = FixedPanel.Panel1
        scRight.Location = New Point(3, 3)
        scRight.Name = "scRight"
        scRight.Orientation = Orientation.Horizontal
        ' 
        ' scRight.Panel1
        ' 
        scRight.Panel1.Controls.Add(tbDetails)
        ' 
        ' scRight.Panel2
        ' 
        scRight.Panel2.Controls.Add(zgcMain)
        scRight.Size = New Size(1052, 1104)
        scRight.SplitterDistance = 281
        scRight.TabIndex = 1
        ' 
        ' tbDetails
        ' 
        tbDetails.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbDetails.Font = New Font("Courier New", 9F)
        tbDetails.Location = New Point(3, 3)
        tbDetails.Multiline = True
        tbDetails.Name = "tbDetails"
        tbDetails.ScrollBars = ScrollBars.Both
        tbDetails.Size = New Size(1047, 275)
        tbDetails.TabIndex = 0
        tbDetails.WordWrap = False
        ' 
        ' zgcMain
        ' 
        zgcMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        zgcMain.Location = New Point(4, 3)
        zgcMain.Margin = New Padding(4, 3, 4, 3)
        zgcMain.Name = "zgcMain"
        zgcMain.ScrollGrace = 0R
        zgcMain.ScrollMaxX = 0R
        zgcMain.ScrollMaxY = 0R
        zgcMain.ScrollMaxY2 = 0R
        zgcMain.ScrollMinX = 0R
        zgcMain.ScrollMinY = 0R
        zgcMain.ScrollMinY2 = 0R
        zgcMain.Size = New Size(1044, 812)
        zgcMain.TabIndex = 0
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
        msMain.Size = New Size(1408, 24)
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
        tsmiTools.DropDownItems.AddRange(New ToolStripItem() {tsmiTools_InViewDisplay, tsmiTools_Recalc})
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
        ' frmGetObject
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1408, 1186)
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
        scRight.Panel1.ResumeLayout(False)
        scRight.Panel1.PerformLayout()
        scRight.Panel2.ResumeLayout(False)
        CType(scRight, ComponentModel.ISupportInitialize).EndInit()
        scRight.ResumeLayout(False)
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
    Friend WithEvents tbDetails As TextBox
    Friend WithEvents tUpdateDetails As Timer
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiData As ToolStripMenuItem
    Friend WithEvents tsmiData_AstroBin As ToolStripMenuItem
    Friend WithEvents tsmiTools As ToolStripMenuItem
    Friend WithEvents tsmiTools_InViewDisplay As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiData_Accept As ToolStripMenuItem
    Friend WithEvents tsmiData_SetLocation As ToolStripMenuItem
    Friend WithEvents scRight As SplitContainer
    Friend WithEvents zgcMain As ZedGraph.ZedGraphControl
    Friend WithEvents cbCustomOnly As CheckBox
    Friend WithEvents tsmiTools_Recalc As ToolStripMenuItem
End Class
