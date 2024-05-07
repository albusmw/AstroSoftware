<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
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
        components = New ComponentModel.Container()
        tbLog = New TextBox()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_Exit = New ToolStripMenuItem()
        tsmiCalc = New ToolStripMenuItem()
        tsmiCalc_Visibility = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiCalc_TC1 = New ToolStripMenuItem()
        tsmiCalc_TC2 = New ToolStripMenuItem()
        tsmiCalc_ClipAstrobin = New ToolStripMenuItem()
        zgcMain = New ZedGraph.ZedGraphControl()
        scMain = New SplitContainer()
        Label2 = New Label()
        tbObjectDec = New TextBox()
        Label1 = New Label()
        tbObjectRA = New TextBox()
        msMain.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Font = New Font("Courier New", 12F, FontStyle.Regular, GraphicsUnit.Point)
        tbLog.Location = New Point(0, 52)
        tbLog.Margin = New Padding(3, 4, 3, 4)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(531, 824)
        tbLog.TabIndex = 1
        tbLog.WordWrap = False
        ' 
        ' msMain
        ' 
        msMain.ImageScalingSize = New Size(20, 20)
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiCalc})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Padding = New Padding(7, 3, 0, 3)
        msMain.Size = New Size(1618, 30)
        msMain.TabIndex = 3
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_Exit})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(46, 24)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_Exit
        ' 
        tsmiFile_Exit.Name = "tsmiFile_Exit"
        tsmiFile_Exit.Size = New Size(116, 26)
        tsmiFile_Exit.Text = "Exit"
        ' 
        ' tsmiCalc
        ' 
        tsmiCalc.DropDownItems.AddRange(New ToolStripItem() {tsmiCalc_Visibility, ToolStripMenuItem1, tsmiCalc_TC1, tsmiCalc_TC2, tsmiCalc_ClipAstrobin})
        tsmiCalc.Name = "tsmiCalc"
        tsmiCalc.Size = New Size(84, 24)
        tsmiCalc.Text = "Calculate"
        ' 
        ' tsmiCalc_Visibility
        ' 
        tsmiCalc_Visibility.Name = "tsmiCalc_Visibility"
        tsmiCalc_Visibility.Size = New Size(268, 26)
        tsmiCalc_Visibility.Text = "Object visibility"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(265, 6)
        ' 
        ' tsmiCalc_TC1
        ' 
        tsmiCalc_TC1.Name = "tsmiCalc_TC1"
        tsmiCalc_TC1.Size = New Size(268, 26)
        tsmiCalc_TC1.Text = "Test case 1 (use debugger)"
        ' 
        ' tsmiCalc_TC2
        ' 
        tsmiCalc_TC2.Name = "tsmiCalc_TC2"
        tsmiCalc_TC2.Size = New Size(268, 26)
        tsmiCalc_TC2.Text = "Test case 2 (CCDGuide)"
        ' 
        ' tsmiCalc_ClipAstrobin
        ' 
        tsmiCalc_ClipAstrobin.Name = "tsmiCalc_ClipAstrobin"
        tsmiCalc_ClipAstrobin.Size = New Size(268, 26)
        tsmiCalc_ClipAstrobin.Text = "From clipboard (AstroBin)"
        ' 
        ' zgcMain
        ' 
        zgcMain.Dock = DockStyle.Fill
        zgcMain.Location = New Point(0, 0)
        zgcMain.Margin = New Padding(4, 5, 4, 5)
        zgcMain.Name = "zgcMain"
        zgcMain.ScrollGrace = 0R
        zgcMain.ScrollMaxX = 0R
        zgcMain.ScrollMaxY = 0R
        zgcMain.ScrollMaxY2 = 0R
        zgcMain.ScrollMinX = 0R
        zgcMain.ScrollMinY = 0R
        zgcMain.ScrollMinY2 = 0R
        zgcMain.Size = New Size(1059, 876)
        zgcMain.TabIndex = 4
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.Location = New Point(12, 42)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(Label2)
        scMain.Panel1.Controls.Add(tbObjectDec)
        scMain.Panel1.Controls.Add(Label1)
        scMain.Panel1.Controls.Add(tbObjectRA)
        scMain.Panel1.Controls.Add(tbLog)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(zgcMain)
        scMain.Size = New Size(1594, 876)
        scMain.SplitterDistance = 531
        scMain.TabIndex = 5
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(243, 21)
        Label2.Name = "Label2"
        Label2.Size = New Size(35, 20)
        Label2.TabIndex = 5
        Label2.Text = "Dec"
        ' 
        ' tbObjectDec
        ' 
        tbObjectDec.Location = New Point(284, 18)
        tbObjectDec.Name = "tbObjectDec"
        tbObjectDec.Size = New Size(233, 27)
        tbObjectDec.TabIndex = 4
        tbObjectDec.Text = "45"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 21)
        Label1.Name = "Label1"
        Label1.Size = New Size(28, 20)
        Label1.TabIndex = 3
        Label1.Text = "RA"
        ' 
        ' tbObjectRA
        ' 
        tbObjectRA.Location = New Point(46, 18)
        tbObjectRA.Name = "tbObjectRA"
        tbObjectRA.Size = New Size(191, 27)
        tbObjectRA.TabIndex = 2
        tbObjectRA.Text = "6h 15m 8s"
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1618, 930)
        Controls.Add(scMain)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Margin = New Padding(3, 4, 3, 4)
        Name = "MainForm"
        Text = "InView"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel1.PerformLayout()
        scMain.Panel2.ResumeLayout(False)
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents tbLog As TextBox
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_Exit As ToolStripMenuItem
    Friend WithEvents tsmiCalc As ToolStripMenuItem
    Friend WithEvents tsmiCalc_Visibility As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiCalc_TC1 As ToolStripMenuItem
    Friend WithEvents tsmiCalc_TC2 As ToolStripMenuItem
    Friend WithEvents zgcMain As ZedGraph.ZedGraphControl
    Friend WithEvents tsmiCalc_ClipAstrobin As ToolStripMenuItem
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents Label2 As Label
    Friend WithEvents tbObjectDec As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents tbObjectRA As TextBox

End Class
