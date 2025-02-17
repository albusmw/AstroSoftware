<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        UcChannel1 = New ucChannel()
        UcChannel2 = New ucChannel()
        UcChannel3 = New ucChannel()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiAction = New ToolStripMenuItem()
        tsmiAction_Recalc = New ToolStripMenuItem()
        tcMain = New TabControl()
        tp1 = New TabPage()
        tp2 = New TabPage()
        tp3 = New TabPage()
        scMain = New SplitContainer()
        msMain.SuspendLayout()
        tcMain.SuspendLayout()
        tp1.SuspendLayout()
        tp2.SuspendLayout()
        tp3.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' UcChannel1
        ' 
        UcChannel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        UcChannel1.ChannelName = "Channel 1"
        UcChannel1.FileName = "\\192.168.100.10\dsc\DSS_Autosave\2024_07_10 - NGC2070 Ha 1.tif"
        UcChannel1.FileStatus = SystemColors.Window
        UcChannel1.Location = New Point(6, 6)
        UcChannel1.Name = "UcChannel1"
        UcChannel1.Size = New Size(483, 32)
        UcChannel1.TabIndex = 0
        ' 
        ' UcChannel2
        ' 
        UcChannel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        UcChannel2.ChannelName = "Channel 2"
        UcChannel2.FileName = "\\192.168.100.10\dsc\DSS_Autosave\2024_07_12 - NGC2070 O-III 1.tif"
        UcChannel2.FileStatus = SystemColors.Window
        UcChannel2.Location = New Point(6, 6)
        UcChannel2.Name = "UcChannel2"
        UcChannel2.Size = New Size(406, 32)
        UcChannel2.TabIndex = 1
        ' 
        ' UcChannel3
        ' 
        UcChannel3.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        UcChannel3.ChannelName = "Channel 3"
        UcChannel3.FileName = "\\192.168.100.10\dsc\DSS_Autosave\2024_07_13 - NGC2070 S-II.tif"
        UcChannel3.FileStatus = SystemColors.Window
        UcChannel3.Location = New Point(6, 6)
        UcChannel3.Name = "UcChannel3"
        UcChannel3.Size = New Size(406, 32)
        UcChannel3.TabIndex = 2
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiAction})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(1322, 24)
        msMain.TabIndex = 3
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(37, 20)
        tsmiFile.Text = "File"
        ' 
        ' tsmiAction
        ' 
        tsmiAction.DropDownItems.AddRange(New ToolStripItem() {tsmiAction_Recalc})
        tsmiAction.Name = "tsmiAction"
        tsmiAction.Size = New Size(54, 20)
        tsmiAction.Text = "Action"
        ' 
        ' tsmiAction_Recalc
        ' 
        tsmiAction_Recalc.Name = "tsmiAction_Recalc"
        tsmiAction_Recalc.Size = New Size(108, 22)
        tsmiAction_Recalc.Text = "Recalc"
        ' 
        ' tcMain
        ' 
        tcMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tcMain.Controls.Add(tp1)
        tcMain.Controls.Add(tp2)
        tcMain.Controls.Add(tp3)
        tcMain.Location = New Point(3, 3)
        tcMain.Name = "tcMain"
        tcMain.SelectedIndex = 0
        tcMain.Size = New Size(503, 856)
        tcMain.TabIndex = 4
        ' 
        ' tp1
        ' 
        tp1.Controls.Add(UcChannel1)
        tp1.Location = New Point(4, 24)
        tp1.Name = "tp1"
        tp1.Padding = New Padding(3)
        tp1.Size = New Size(495, 828)
        tp1.TabIndex = 0
        tp1.Text = "Channel 1"
        tp1.UseVisualStyleBackColor = True
        ' 
        ' tp2
        ' 
        tp2.Controls.Add(UcChannel2)
        tp2.Location = New Point(4, 24)
        tp2.Name = "tp2"
        tp2.Padding = New Padding(3)
        tp2.Size = New Size(418, 828)
        tp2.TabIndex = 1
        tp2.Text = "Channel 2"
        tp2.UseVisualStyleBackColor = True
        ' 
        ' tp3
        ' 
        tp3.Controls.Add(UcChannel3)
        tp3.Location = New Point(4, 24)
        tp3.Name = "tp3"
        tp3.Padding = New Padding(3)
        tp3.Size = New Size(418, 828)
        tp3.TabIndex = 2
        tp3.Text = "Channel 3"
        tp3.UseVisualStyleBackColor = True
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.Location = New Point(12, 27)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(tcMain)
        scMain.Size = New Size(1298, 862)
        scMain.SplitterDistance = 509
        scMain.TabIndex = 5
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1322, 901)
        Controls.Add(scMain)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Name = "Form1"
        Text = "FinalAstroImage"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        tcMain.ResumeLayout(False)
        tp1.ResumeLayout(False)
        tp2.ResumeLayout(False)
        tp3.ResumeLayout(False)
        scMain.Panel1.ResumeLayout(False)
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents UcChannel1 As ucChannel
    Friend WithEvents UcChannel2 As ucChannel
    Friend WithEvents UcChannel3 As ucChannel
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiAction As ToolStripMenuItem
    Friend WithEvents tsmiAction_Recalc As ToolStripMenuItem
    Friend WithEvents tcMain As TabControl
    Friend WithEvents tp1 As TabPage
    Friend WithEvents tp2 As TabPage
    Friend WithEvents tp3 As TabPage
    Friend WithEvents scMain As SplitContainer

End Class
