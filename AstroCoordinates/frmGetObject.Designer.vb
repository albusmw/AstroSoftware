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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGetObject))
        tbSearchString = New TextBox()
        Label1 = New Label()
        lbResults = New ListBox()
        ssMain = New StatusStrip()
        tsslLoaded = New ToolStripStatusLabel()
        tsslSelectionLength = New ToolStripStatusLabel()
        tssbLoad = New ToolStripSplitButton()
        tsmiLoadCat = New ToolStripMenuItem()
        scMain = New SplitContainer()
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
        tbSearchString.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbSearchString.Location = New Point(93, 12)
        tbSearchString.Name = "tbSearchString"
        tbSearchString.Size = New Size(695, 21)
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
        lbResults.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        lbResults.FormattingEnabled = True
        lbResults.HorizontalScrollbar = True
        lbResults.IntegralHeight = False
        lbResults.ItemHeight = 15
        lbResults.Location = New Point(3, 3)
        lbResults.Name = "lbResults"
        lbResults.Size = New Size(582, 380)
        lbResults.TabIndex = 2
        ' 
        ' ssMain
        ' 
        ssMain.Items.AddRange(New ToolStripItem() {tsslLoaded, tsslSelectionLength, tssbLoad})
        ssMain.Location = New Point(0, 428)
        ssMain.Name = "ssMain"
        ssMain.Size = New Size(800, 22)
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
        ' tssbLoad
        ' 
        tssbLoad.DisplayStyle = ToolStripItemDisplayStyle.Text
        tssbLoad.DropDownItems.AddRange(New ToolStripItem() {tsmiLoadCat})
        tssbLoad.Image = CType(resources.GetObject("tssbLoad.Image"), Image)
        tssbLoad.ImageTransparentColor = Color.Magenta
        tssbLoad.Name = "tssbLoad"
        tssbLoad.Size = New Size(61, 20)
        tssbLoad.Text = "Load ..."
        ' 
        ' tsmiLoadCat
        ' 
        tsmiLoadCat.Name = "tsmiLoadCat"
        tsmiLoadCat.Size = New Size(147, 22)
        tsmiLoadCat.Text = "Load catalogs"
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
        scMain.Panel2.Controls.Add(tbDetails)
        scMain.Size = New Size(776, 386)
        scMain.SplitterDistance = 588
        scMain.TabIndex = 4
        ' 
        ' tbDetails
        ' 
        tbDetails.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbDetails.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbDetails.Location = New Point(3, 3)
        tbDetails.Multiline = True
        tbDetails.Name = "tbDetails"
        tbDetails.ScrollBars = ScrollBars.Both
        tbDetails.Size = New Size(178, 380)
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
        ClientSize = New Size(800, 450)
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
    Friend WithEvents tssbLoad As ToolStripSplitButton
    Friend WithEvents tsmiLoadCat As ToolStripMenuItem
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents tbDetails As TextBox
    Friend WithEvents tUpdateDetails As Timer
End Class
