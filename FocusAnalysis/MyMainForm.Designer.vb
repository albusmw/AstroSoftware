<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MyMainForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_OpenSerSequence = New ToolStripMenuItem()
        tsmiFile_Save = New ToolStripMenuItem()
        tsmiFile_Save_GlobalMin = New ToolStripMenuItem()
        tsmiFile_Save_GlobalMax = New ToolStripMenuItem()
        ofdMain = New OpenFileDialog()
        gbStatus = New GroupBox()
        tbLog = New TextBox()
        tImageStream = New Label()
        pbImageStream = New ProgressBar()
        Label1 = New Label()
        tbSERFile = New TextBox()
        Label2 = New Label()
        tbPWI4LogFile = New TextBox()
        Label3 = New Label()
        btnAnalysis = New Button()
        btnSetSERFile = New Button()
        msMain.SuspendLayout()
        gbStatus.SuspendLayout()
        SuspendLayout()
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Padding = New Padding(7, 2, 0, 2)
        msMain.Size = New Size(703, 24)
        msMain.TabIndex = 0
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_OpenSerSequence, tsmiFile_Save})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(37, 20)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_OpenSerSequence
        ' 
        tsmiFile_OpenSerSequence.Name = "tsmiFile_OpenSerSequence"
        tsmiFile_OpenSerSequence.Size = New Size(180, 22)
        tsmiFile_OpenSerSequence.Text = "Open SER sequence"
        ' 
        ' tsmiFile_Save
        ' 
        tsmiFile_Save.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_Save_GlobalMin, tsmiFile_Save_GlobalMax})
        tsmiFile_Save.Name = "tsmiFile_Save"
        tsmiFile_Save.Size = New Size(180, 22)
        tsmiFile_Save.Text = "Save ..."
        ' 
        ' tsmiFile_Save_GlobalMin
        ' 
        tsmiFile_Save_GlobalMin.Name = "tsmiFile_Save_GlobalMin"
        tsmiFile_Save_GlobalMin.Size = New Size(156, 22)
        tsmiFile_Save_GlobalMin.Text = "Global MIN file"
        ' 
        ' tsmiFile_Save_GlobalMax
        ' 
        tsmiFile_Save_GlobalMax.Name = "tsmiFile_Save_GlobalMax"
        tsmiFile_Save_GlobalMax.Size = New Size(156, 22)
        tsmiFile_Save_GlobalMax.Text = "Global MAX file"
        ' 
        ' ofdMain
        ' 
        ofdMain.FileName = "OpenFileDialog1"
        ' 
        ' gbStatus
        ' 
        gbStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gbStatus.Controls.Add(tbLog)
        gbStatus.Controls.Add(tImageStream)
        gbStatus.Controls.Add(pbImageStream)
        gbStatus.Controls.Add(Label1)
        gbStatus.Location = New Point(13, 120)
        gbStatus.Margin = New Padding(4, 3, 4, 3)
        gbStatus.Name = "gbStatus"
        gbStatus.Padding = New Padding(4, 3, 4, 3)
        gbStatus.Size = New Size(677, 382)
        gbStatus.TabIndex = 1
        gbStatus.TabStop = False
        gbStatus.Text = "Status"
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbLog.Location = New Point(7, 54)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ReadOnly = True
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(663, 322)
        tbLog.TabIndex = 4
        tbLog.WordWrap = False
        ' 
        ' tImageStream
        ' 
        tImageStream.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        tImageStream.Location = New Point(589, 33)
        tImageStream.Margin = New Padding(4, 0, 4, 0)
        tImageStream.Name = "tImageStream"
        tImageStream.Size = New Size(82, 15)
        tImageStream.TabIndex = 3
        tImageStream.Text = "0/?"
        tImageStream.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pbImageStream
        ' 
        pbImageStream.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        pbImageStream.Location = New Point(107, 33)
        pbImageStream.Margin = New Padding(4, 3, 4, 3)
        pbImageStream.Name = "pbImageStream"
        pbImageStream.Size = New Size(474, 15)
        pbImageStream.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(19, 33)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(79, 15)
        Label1.TabIndex = 0
        Label1.Text = "Image stream"
        ' 
        ' tbSERFile
        ' 
        tbSERFile.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbSERFile.Location = New Point(91, 27)
        tbSERFile.Name = "tbSERFile"
        tbSERFile.Size = New Size(532, 23)
        tbSERFile.TabIndex = 2
        tbSERFile.Text = "C:\!Work\Focus\05_48_38.ser"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 30)
        Label2.Name = "Label2"
        Label2.Size = New Size(45, 15)
        Label2.TabIndex = 3
        Label2.Text = "SER file"
        ' 
        ' tbPWI4LogFile
        ' 
        tbPWI4LogFile.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbPWI4LogFile.Location = New Point(91, 56)
        tbPWI4LogFile.Name = "tbPWI4LogFile"
        tbPWI4LogFile.Size = New Size(600, 23)
        tbPWI4LogFile.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(12, 59)
        Label3.Name = "Label3"
        Label3.Size = New Size(73, 15)
        Label3.TabIndex = 5
        Label3.Text = "PWI4 log file"
        ' 
        ' btnAnalysis
        ' 
        btnAnalysis.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        btnAnalysis.Location = New Point(14, 85)
        btnAnalysis.Name = "btnAnalysis"
        btnAnalysis.Size = New Size(676, 29)
        btnAnalysis.TabIndex = 6
        btnAnalysis.Text = "Analysis"
        btnAnalysis.UseVisualStyleBackColor = True
        ' 
        ' btnSetSERFile
        ' 
        btnSetSERFile.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSetSERFile.Location = New Point(629, 27)
        btnSetSERFile.Name = "btnSetSERFile"
        btnSetSERFile.Size = New Size(61, 23)
        btnSetSERFile.TabIndex = 7
        btnSetSERFile.Text = "..."
        btnSetSERFile.UseVisualStyleBackColor = True
        ' 
        ' MyMainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(703, 514)
        Controls.Add(btnSetSERFile)
        Controls.Add(btnAnalysis)
        Controls.Add(Label3)
        Controls.Add(tbPWI4LogFile)
        Controls.Add(Label2)
        Controls.Add(tbSERFile)
        Controls.Add(gbStatus)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Margin = New Padding(4, 3, 4, 3)
        Name = "MyMainForm"
        Text = "FocusAnalysis"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        gbStatus.ResumeLayout(False)
        gbStatus.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenSerSequence As ToolStripMenuItem
    Friend WithEvents ofdMain As OpenFileDialog
    Friend WithEvents gbStatus As GroupBox
    Friend WithEvents tImageStream As Label
    Friend WithEvents pbImageStream As ProgressBar
    Friend WithEvents Label1 As Label
    Friend WithEvents tbSERFile As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tbPWI4LogFile As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnAnalysis As Button
    Friend WithEvents tbLog As TextBox
    Friend WithEvents tsmiFile_Save As ToolStripMenuItem
    Friend WithEvents tsmiFile_Save_GlobalMin As ToolStripMenuItem
    Friend WithEvents tsmiFile_Save_GlobalMax As ToolStripMenuItem
    Friend WithEvents btnSetSERFile As Button
End Class
