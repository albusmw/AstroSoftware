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
        btnLocationHolz = New Button()
        btnLocationDSC = New Button()
        tbDetails = New TextBox()
        tUpdateDetails = New Timer(components)
        ssMain.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbSearchString
        ' 
        tbSearchString.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbSearchString.Font = New Font("Courier New", 9F)
        tbSearchString.Location = New Point(93, 12)
        tbSearchString.Name = "tbSearchString"
        tbSearchString.Size = New Size(712, 21)
        tbSearchString.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 15)
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
        lbResults.Location = New Point(3, 3)
        lbResults.Name = "lbResults"
        lbResults.Size = New Size(358, 533)
        lbResults.TabIndex = 2
        ' 
        ' ssMain
        ' 
        ssMain.Items.AddRange(New ToolStripItem() {tsslLoaded, tsslSelectionLength})
        ssMain.Location = New Point(0, 581)
        ssMain.Name = "ssMain"
        ssMain.Size = New Size(817, 22)
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
        scMain.Location = New Point(12, 39)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(lbResults)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(btnLocationHolz)
        scMain.Panel2.Controls.Add(btnLocationDSC)
        scMain.Panel2.Controls.Add(tbDetails)
        scMain.Size = New Size(793, 539)
        scMain.SplitterDistance = 364
        scMain.TabIndex = 4
        ' 
        ' btnLocationHolz
        ' 
        btnLocationHolz.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnLocationHolz.Location = New Point(308, 509)
        btnLocationHolz.Name = "btnLocationHolz"
        btnLocationHolz.Size = New Size(114, 23)
        btnLocationHolz.TabIndex = 2
        btnLocationHolz.Text = "Holzkirchen"
        btnLocationHolz.UseVisualStyleBackColor = True
        ' 
        ' btnLocationDSC
        ' 
        btnLocationDSC.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnLocationDSC.Location = New Point(3, 509)
        btnLocationDSC.Name = "btnLocationDSC"
        btnLocationDSC.Size = New Size(58, 23)
        btnLocationDSC.TabIndex = 1
        btnLocationDSC.Text = "DSC"
        btnLocationDSC.UseVisualStyleBackColor = True
        ' 
        ' tbDetails
        ' 
        tbDetails.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbDetails.Font = New Font("Courier New", 9F)
        tbDetails.Location = New Point(3, 3)
        tbDetails.Multiline = True
        tbDetails.Name = "tbDetails"
        tbDetails.ScrollBars = ScrollBars.Both
        tbDetails.Size = New Size(419, 500)
        tbDetails.TabIndex = 0
        tbDetails.WordWrap = False
        ' 
        ' tUpdateDetails
        ' 
        tUpdateDetails.Enabled = True
        tUpdateDetails.Interval = 1000
        ' 
        ' frmGetObject
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(817, 603)
        Controls.Add(scMain)
        Controls.Add(ssMain)
        Controls.Add(Label1)
        Controls.Add(tbSearchString)
        Name = "frmGetObject"
        Text = "Object search"
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

    Friend WithEvents tbSearchString As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lbResults As ListBox
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslLoaded As ToolStripStatusLabel
    Friend WithEvents tsslSelectionLength As ToolStripStatusLabel
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents tbDetails As TextBox
    Friend WithEvents tUpdateDetails As Timer
    Friend WithEvents btnLocationHolz As Button
    Friend WithEvents btnLocationDSC As Button
End Class
