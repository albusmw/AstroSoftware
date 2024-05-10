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
        pgMain = New PropertyGrid()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        tbLog.Font = New Font("Courier New", 12F, FontStyle.Regular, GraphicsUnit.Point)
        tbLog.Location = New Point(10, 490)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(410, 397)
        tbLog.TabIndex = 1
        tbLog.WordWrap = False
        ' 
        ' msMain
        ' 
        msMain.ImageScalingSize = New Size(20, 20)
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiCalc})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1538, 24)
        msMain.TabIndex = 3
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_Exit})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(37, 20)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_Exit
        ' 
        tsmiFile_Exit.Name = "tsmiFile_Exit"
        tsmiFile_Exit.Size = New Size(93, 22)
        tsmiFile_Exit.Text = "Exit"
        ' 
        ' tsmiCalc
        ' 
        tsmiCalc.DropDownItems.AddRange(New ToolStripItem() {tsmiCalc_Visibility, ToolStripMenuItem1, tsmiCalc_TC1, tsmiCalc_TC2, tsmiCalc_ClipAstrobin})
        tsmiCalc.Name = "tsmiCalc"
        tsmiCalc.Size = New Size(68, 20)
        tsmiCalc.Text = "Calculate"
        ' 
        ' tsmiCalc_Visibility
        ' 
        tsmiCalc_Visibility.Name = "tsmiCalc_Visibility"
        tsmiCalc_Visibility.Size = New Size(212, 22)
        tsmiCalc_Visibility.Text = "Object visibility"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(209, 6)
        ' 
        ' tsmiCalc_TC1
        ' 
        tsmiCalc_TC1.Name = "tsmiCalc_TC1"
        tsmiCalc_TC1.Size = New Size(212, 22)
        tsmiCalc_TC1.Text = "Test case 1 (use debugger)"
        ' 
        ' tsmiCalc_TC2
        ' 
        tsmiCalc_TC2.Name = "tsmiCalc_TC2"
        tsmiCalc_TC2.Size = New Size(212, 22)
        tsmiCalc_TC2.Text = "Test case 2 (CCDGuide)"
        ' 
        ' tsmiCalc_ClipAstrobin
        ' 
        tsmiCalc_ClipAstrobin.Name = "tsmiCalc_ClipAstrobin"
        tsmiCalc_ClipAstrobin.Size = New Size(212, 22)
        tsmiCalc_ClipAstrobin.Text = "From clipboard (AstroBin)"
        ' 
        ' zgcMain
        ' 
        zgcMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        zgcMain.Location = New Point(426, 26)
        zgcMain.Margin = New Padding(4, 4, 4, 4)
        zgcMain.Name = "zgcMain"
        zgcMain.ScrollGrace = 0R
        zgcMain.ScrollMaxX = 0R
        zgcMain.ScrollMaxY = 0R
        zgcMain.ScrollMaxY2 = 0R
        zgcMain.ScrollMinX = 0R
        zgcMain.ScrollMinY = 0R
        zgcMain.ScrollMinY2 = 0R
        zgcMain.Size = New Size(1100, 860)
        zgcMain.TabIndex = 4
        ' 
        ' pgMain
        ' 
        pgMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        pgMain.Location = New Point(10, 25)
        pgMain.Margin = New Padding(3, 2, 3, 2)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(410, 460)
        pgMain.TabIndex = 5
        pgMain.ToolbarVisible = False
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1538, 896)
        Controls.Add(pgMain)
        Controls.Add(tbLog)
        Controls.Add(zgcMain)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Name = "MainForm"
        Text = "InView"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
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
    Friend WithEvents pgMain As PropertyGrid

End Class
