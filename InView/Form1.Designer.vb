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
        btnFromClipboard = New Button()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_Exit = New ToolStripMenuItem()
        tsmiCalc = New ToolStripMenuItem()
        tsmiCalc_Visibility = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiCalc_TC1 = New ToolStripMenuItem()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbLog.Location = New Point(12, 94)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(860, 445)
        tbLog.TabIndex = 1
        tbLog.WordWrap = False
        ' 
        ' btnFromClipboard
        ' 
        btnFromClipboard.Location = New Point(146, 56)
        btnFromClipboard.Name = "btnFromClipboard"
        btnFromClipboard.Size = New Size(164, 23)
        btnFromClipboard.TabIndex = 2
        btnFromClipboard.Text = "Astrobin Clipboard"
        btnFromClipboard.UseVisualStyleBackColor = True
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiCalc})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(884, 24)
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
        tsmiCalc.DropDownItems.AddRange(New ToolStripItem() {tsmiCalc_Visibility, ToolStripMenuItem1, tsmiCalc_TC1})
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
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(884, 551)
        Controls.Add(btnFromClipboard)
        Controls.Add(tbLog)
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
    Friend WithEvents btnFromClipboard As Button
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_Exit As ToolStripMenuItem
    Friend WithEvents tsmiCalc As ToolStripMenuItem
    Friend WithEvents tsmiCalc_Visibility As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiCalc_TC1 As ToolStripMenuItem

End Class
