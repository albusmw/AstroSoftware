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
        tbLog = New TextBox()
        msMain = New MenuStrip()
        tsmiCalc = New ToolStripMenuItem()
        tsmiCalc_TC1 = New ToolStripMenuItem()
        tsmiCalc_TC2 = New ToolStripMenuItem()
        tsmiCalc_TC3 = New ToolStripMenuItem()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Font = New Font("Courier New", 12F, FontStyle.Regular, GraphicsUnit.Point)
        tbLog.Location = New Point(10, 40)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(1516, 847)
        tbLog.TabIndex = 1
        tbLog.WordWrap = False
        ' 
        ' msMain
        ' 
        msMain.ImageScalingSize = New Size(20, 20)
        msMain.Items.AddRange(New ToolStripItem() {tsmiCalc})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1538, 24)
        msMain.TabIndex = 3
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiCalc
        ' 
        tsmiCalc.DropDownItems.AddRange(New ToolStripItem() {tsmiCalc_TC1, tsmiCalc_TC2, tsmiCalc_TC3})
        tsmiCalc.Name = "tsmiCalc"
        tsmiCalc.Size = New Size(68, 20)
        tsmiCalc.Text = "Calculate"
        ' 
        ' tsmiCalc_TC1
        ' 
        tsmiCalc_TC1.Name = "tsmiCalc_TC1"
        tsmiCalc_TC1.Size = New Size(358, 22)
        tsmiCalc_TC1.Text = "Test case 1 (use debugger)"
        ' 
        ' tsmiCalc_TC2
        ' 
        tsmiCalc_TC2.Name = "tsmiCalc_TC2"
        tsmiCalc_TC2.Size = New Size(358, 22)
        tsmiCalc_TC2.Text = "Test case 2 (compare CCDGuide with own calculation)"
        ' 
        ' tsmiCalc_TC3
        ' 
        tsmiCalc_TC3.Name = "tsmiCalc_TC3"
        tsmiCalc_TC3.Size = New Size(358, 22)
        tsmiCalc_TC3.Text = "Test case 3 (ASCOM)"
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1538, 896)
        Controls.Add(tbLog)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Name = "MainForm"
        Text = "InView - astronomical calculation test"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents tbLog As TextBox
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiCalc As ToolStripMenuItem
    Friend WithEvents tsmiCalc_TC1 As ToolStripMenuItem
    Friend WithEvents tsmiCalc_TC2 As ToolStripMenuItem
    Friend WithEvents tsmiCalc_TC3 As ToolStripMenuItem

End Class
