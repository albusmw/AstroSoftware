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
        components = New ComponentModel.Container()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_OpenSerSequence = New ToolStripMenuItem()
        tsmiFile_Save = New ToolStripMenuItem()
        tsmiFile_Save_GlobalMin = New ToolStripMenuItem()
        tsmiFile_Save_GlobalMax = New ToolStripMenuItem()
        ofdMain = New OpenFileDialog()
        tbLog = New TextBox()
        tbSERFile = New TextBox()
        Label2 = New Label()
        tbPWI4LogFile = New TextBox()
        Label3 = New Label()
        btnAnalysis = New Button()
        btnSetSERFile = New Button()
        StatusStrip1 = New StatusStrip()
        pbImageStream = New ToolStripProgressBar()
        tsslImageStream = New ToolStripStatusLabel()
        tsslADUOVLD = New ToolStripStatusLabel()
        pbMaxADU = New ToolStripProgressBar()
        scMain = New SplitContainer()
        scLeft = New SplitContainer()
        pgMain = New PropertyGrid()
        tbSummary = New TextBox()
        scRight = New SplitContainer()
        pbExSERFrameImage = New PictureBoxEx()
        zgcMain = New ZedGraph.ZedGraphControl()
        scTopRight = New SplitContainer()
        zgcHisto = New ZedGraph.ZedGraphControl()
        msMain.SuspendLayout()
        StatusStrip1.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        CType(scLeft, ComponentModel.ISupportInitialize).BeginInit()
        scLeft.Panel1.SuspendLayout()
        scLeft.Panel2.SuspendLayout()
        scLeft.SuspendLayout()
        CType(scRight, ComponentModel.ISupportInitialize).BeginInit()
        scRight.Panel1.SuspendLayout()
        scRight.Panel2.SuspendLayout()
        scRight.SuspendLayout()
        CType(pbExSERFrameImage, ComponentModel.ISupportInitialize).BeginInit()
        CType(scTopRight, ComponentModel.ISupportInitialize).BeginInit()
        scTopRight.Panel1.SuspendLayout()
        scTopRight.Panel2.SuspendLayout()
        scTopRight.SuspendLayout()
        SuspendLayout()
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Padding = New Padding(7, 2, 0, 2)
        msMain.Size = New Size(1292, 24)
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
        tsmiFile_OpenSerSequence.Size = New Size(178, 22)
        tsmiFile_OpenSerSequence.Text = "Open SER sequence"
        ' 
        ' tsmiFile_Save
        ' 
        tsmiFile_Save.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_Save_GlobalMin, tsmiFile_Save_GlobalMax})
        tsmiFile_Save.Name = "tsmiFile_Save"
        tsmiFile_Save.Size = New Size(178, 22)
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
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        tbLog.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbLog.Location = New Point(3, 3)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ReadOnly = True
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(485, 414)
        tbLog.TabIndex = 4
        tbLog.WordWrap = False
        ' 
        ' tbSERFile
        ' 
        tbSERFile.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbSERFile.Location = New Point(91, 27)
        tbSERFile.Name = "tbSERFile"
        tbSERFile.Size = New Size(1089, 23)
        tbSERFile.TabIndex = 2
        tbSERFile.Text = "C:\!Work\Focus\2025_12_10__Focus_SII.ser"
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
        tbPWI4LogFile.Size = New Size(1122, 23)
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
        btnAnalysis.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnAnalysis.Location = New Point(1219, 30)
        btnAnalysis.Name = "btnAnalysis"
        btnAnalysis.Size = New Size(61, 49)
        btnAnalysis.TabIndex = 6
        btnAnalysis.Text = "Analysis"
        btnAnalysis.UseVisualStyleBackColor = True
        ' 
        ' btnSetSERFile
        ' 
        btnSetSERFile.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSetSERFile.Location = New Point(1186, 26)
        btnSetSERFile.Name = "btnSetSERFile"
        btnSetSERFile.Size = New Size(32, 23)
        btnSetSERFile.TabIndex = 7
        btnSetSERFile.Text = "..."
        btnSetSERFile.UseVisualStyleBackColor = True
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {pbImageStream, tsslImageStream, tsslADUOVLD, pbMaxADU})
        StatusStrip1.Location = New Point(0, 951)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(1292, 22)
        StatusStrip1.TabIndex = 8
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' pbImageStream
        ' 
        pbImageStream.Name = "pbImageStream"
        pbImageStream.Size = New Size(100, 16)
        ' 
        ' tsslImageStream
        ' 
        tsslImageStream.Name = "tsslImageStream"
        tsslImageStream.Size = New Size(23, 17)
        tsslImageStream.Text = "0/?"
        ' 
        ' tsslADUOVLD
        ' 
        tsslADUOVLD.Name = "tsslADUOVLD"
        tsslADUOVLD.Size = New Size(80, 17)
        tsslADUOVLD.Text = "ADU overload"
        ' 
        ' pbMaxADU
        ' 
        pbMaxADU.ForeColor = Color.Lime
        pbMaxADU.Maximum = 65535
        pbMaxADU.Name = "pbMaxADU"
        pbMaxADU.Size = New Size(100, 16)
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.Location = New Point(14, 85)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(scLeft)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(scRight)
        scMain.Size = New Size(1265, 856)
        scMain.SplitterDistance = 497
        scMain.TabIndex = 9
        ' 
        ' scLeft
        ' 
        scLeft.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scLeft.Location = New Point(3, 3)
        scLeft.Name = "scLeft"
        scLeft.Orientation = Orientation.Horizontal
        ' 
        ' scLeft.Panel1
        ' 
        scLeft.Panel1.Controls.Add(pgMain)
        scLeft.Panel1.Controls.Add(tbSummary)
        ' 
        ' scLeft.Panel2
        ' 
        scLeft.Panel2.Controls.Add(tbLog)
        scLeft.Size = New Size(491, 847)
        scLeft.SplitterDistance = 423
        scLeft.TabIndex = 5
        ' 
        ' pgMain
        ' 
        pgMain.HelpVisible = False
        pgMain.Location = New Point(3, 3)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(485, 187)
        pgMain.TabIndex = 6
        pgMain.ToolbarVisible = False
        ' 
        ' tbSummary
        ' 
        tbSummary.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbSummary.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        tbSummary.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbSummary.Location = New Point(3, 196)
        tbSummary.Multiline = True
        tbSummary.Name = "tbSummary"
        tbSummary.ReadOnly = True
        tbSummary.ScrollBars = ScrollBars.Both
        tbSummary.Size = New Size(485, 223)
        tbSummary.TabIndex = 5
        tbSummary.WordWrap = False
        ' 
        ' scRight
        ' 
        scRight.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scRight.Location = New Point(3, 3)
        scRight.Name = "scRight"
        scRight.Orientation = Orientation.Horizontal
        ' 
        ' scRight.Panel1
        ' 
        scRight.Panel1.Controls.Add(scTopRight)
        ' 
        ' scRight.Panel2
        ' 
        scRight.Panel2.Controls.Add(zgcMain)
        scRight.Size = New Size(758, 850)
        scRight.SplitterDistance = 424
        scRight.TabIndex = 1
        ' 
        ' pbExSERFrameImage
        ' 
        pbExSERFrameImage.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pbExSERFrameImage.BackColor = Color.Gray
        pbExSERFrameImage.Location = New Point(3, 3)
        pbExSERFrameImage.Name = "pbExSERFrameImage"
        pbExSERFrameImage.Size = New Size(419, 410)
        pbExSERFrameImage.SizeMode = PictureBoxSizeMode.Zoom
        pbExSERFrameImage.TabIndex = 0
        pbExSERFrameImage.TabStop = False
        ' 
        ' zgcMain
        ' 
        zgcMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        zgcMain.Location = New Point(3, 3)
        zgcMain.Margin = New Padding(4, 3, 4, 3)
        zgcMain.Name = "zgcMain"
        zgcMain.ScrollGrace = 0R
        zgcMain.ScrollMaxX = 0R
        zgcMain.ScrollMaxY = 0R
        zgcMain.ScrollMaxY2 = 0R
        zgcMain.ScrollMinX = 0R
        zgcMain.ScrollMinY = 0R
        zgcMain.ScrollMinY2 = 0R
        zgcMain.Size = New Size(751, 416)
        zgcMain.TabIndex = 0
        ' 
        ' scTopRight
        ' 
        scTopRight.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scTopRight.Location = New Point(3, 3)
        scTopRight.Name = "scTopRight"
        ' 
        ' scTopRight.Panel1
        ' 
        scTopRight.Panel1.Controls.Add(zgcHisto)
        ' 
        ' scTopRight.Panel2
        ' 
        scTopRight.Panel2.Controls.Add(pbExSERFrameImage)
        scTopRight.Size = New Size(751, 416)
        scTopRight.SplitterDistance = 320
        scTopRight.TabIndex = 1
        ' 
        ' zgcHisto
        ' 
        zgcHisto.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        zgcHisto.Location = New Point(4, 3)
        zgcHisto.Margin = New Padding(4, 3, 4, 3)
        zgcHisto.Name = "zgcHisto"
        zgcHisto.ScrollGrace = 0R
        zgcHisto.ScrollMaxX = 0R
        zgcHisto.ScrollMaxY = 0R
        zgcHisto.ScrollMaxY2 = 0R
        zgcHisto.ScrollMinX = 0R
        zgcHisto.ScrollMinY = 0R
        zgcHisto.ScrollMinY2 = 0R
        zgcHisto.Size = New Size(312, 410)
        zgcHisto.TabIndex = 0
        ' 
        ' MyMainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1292, 973)
        Controls.Add(scMain)
        Controls.Add(StatusStrip1)
        Controls.Add(btnSetSERFile)
        Controls.Add(btnAnalysis)
        Controls.Add(Label3)
        Controls.Add(tbPWI4LogFile)
        Controls.Add(Label2)
        Controls.Add(tbSERFile)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Margin = New Padding(4, 3, 4, 3)
        Name = "MyMainForm"
        Text = "FocusAnalysis"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        scLeft.Panel1.ResumeLayout(False)
        scLeft.Panel1.PerformLayout()
        scLeft.Panel2.ResumeLayout(False)
        scLeft.Panel2.PerformLayout()
        CType(scLeft, ComponentModel.ISupportInitialize).EndInit()
        scLeft.ResumeLayout(False)
        scRight.Panel1.ResumeLayout(False)
        scRight.Panel2.ResumeLayout(False)
        CType(scRight, ComponentModel.ISupportInitialize).EndInit()
        scRight.ResumeLayout(False)
        CType(pbExSERFrameImage, ComponentModel.ISupportInitialize).EndInit()
        scTopRight.Panel1.ResumeLayout(False)
        scTopRight.Panel2.ResumeLayout(False)
        CType(scTopRight, ComponentModel.ISupportInitialize).EndInit()
        scTopRight.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenSerSequence As ToolStripMenuItem
    Friend WithEvents ofdMain As OpenFileDialog
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
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents pbImageStream As ToolStripProgressBar
    Friend WithEvents tsslImageStream As ToolStripStatusLabel
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents pbExSERFrameImage As PictureBoxEx
    Friend WithEvents scRight As SplitContainer
    Friend WithEvents zgcMain As ZedGraph.ZedGraphControl
    Friend WithEvents tsslADUOVLD As ToolStripStatusLabel
    Friend WithEvents scLeft As SplitContainer
    Friend WithEvents tbSummary As TextBox
    Friend WithEvents pbMaxADU As ToolStripProgressBar
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents scTopRight As SplitContainer
    Friend WithEvents zgcHisto As ZedGraph.ZedGraphControl
End Class
