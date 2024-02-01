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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Label1 = New Label()
        Label2 = New Label()
        tbRAParsed = New TextBox()
        tbRAParsedShort = New TextBox()
        tbRAParsedDecimal = New TextBox()
        tbDecParsed = New TextBox()
        tbDecParsedShort = New TextBox()
        tbDecParsedDecimal = New TextBox()
        tlpMain = New TableLayoutPanel()
        btnGetObject = New Button()
        tbSelected = New TextBox()
        btnGoTo = New Button()
        cbJ2000 = New CheckBox()
        ttMain = New ToolTip(components)
        BackgroundWorker1 = New ComponentModel.BackgroundWorker()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_OpenEXE = New ToolStripMenuItem()
        tsmiEnter = New ToolStripMenuItem()
        tsmiEnter_RA = New ToolStripMenuItem()
        tsmiEnter_Dec = New ToolStripMenuItem()
        GotoToolStripMenuItem = New ToolStripMenuItem()
        ObjectToolStripMenuItem = New ToolStripMenuItem()
        ZenithAndStopToolStripMenuItem = New ToolStripMenuItem()
        tlpMain.SuspendLayout()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(3, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(121, 43)
        Label1.TabIndex = 0
        Label1.Text = "RA (Right ascension)"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label2
        ' 
        Label2.Dock = DockStyle.Fill
        Label2.Location = New Point(3, 43)
        Label2.Name = "Label2"
        Label2.Size = New Size(121, 44)
        Label2.TabIndex = 1
        Label2.Text = "DEC (Declination)"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tbRAParsed
        ' 
        tbRAParsed.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbRAParsed.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbRAParsed.Location = New Point(130, 11)
        tbRAParsed.Name = "tbRAParsed"
        tbRAParsed.Size = New Size(166, 21)
        tbRAParsed.TabIndex = 4
        ' 
        ' tbRAParsedShort
        ' 
        tbRAParsedShort.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbRAParsedShort.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbRAParsedShort.Location = New Point(302, 11)
        tbRAParsedShort.Name = "tbRAParsedShort"
        tbRAParsedShort.Size = New Size(166, 21)
        tbRAParsedShort.TabIndex = 5
        ' 
        ' tbRAParsedDecimal
        ' 
        tbRAParsedDecimal.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbRAParsedDecimal.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbRAParsedDecimal.Location = New Point(474, 11)
        tbRAParsedDecimal.Name = "tbRAParsedDecimal"
        tbRAParsedDecimal.Size = New Size(167, 21)
        tbRAParsedDecimal.TabIndex = 6
        ' 
        ' tbDecParsed
        ' 
        tbDecParsed.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDecParsed.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbDecParsed.Location = New Point(130, 54)
        tbDecParsed.Name = "tbDecParsed"
        tbDecParsed.Size = New Size(166, 21)
        tbDecParsed.TabIndex = 7
        ' 
        ' tbDecParsedShort
        ' 
        tbDecParsedShort.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDecParsedShort.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbDecParsedShort.Location = New Point(302, 54)
        tbDecParsedShort.Name = "tbDecParsedShort"
        tbDecParsedShort.Size = New Size(166, 21)
        tbDecParsedShort.TabIndex = 8
        ' 
        ' tbDecParsedDecimal
        ' 
        tbDecParsedDecimal.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDecParsedDecimal.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbDecParsedDecimal.Location = New Point(474, 54)
        tbDecParsedDecimal.Name = "tbDecParsedDecimal"
        tbDecParsedDecimal.Size = New Size(167, 21)
        tbDecParsedDecimal.TabIndex = 9
        ' 
        ' tlpMain
        ' 
        tlpMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpMain.ColumnCount = 4
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 127F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333359F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333359F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        tlpMain.Controls.Add(Label1, 0, 0)
        tlpMain.Controls.Add(tbRAParsedDecimal, 3, 0)
        tlpMain.Controls.Add(tbRAParsedShort, 2, 0)
        tlpMain.Controls.Add(tbDecParsed, 1, 1)
        tlpMain.Controls.Add(tbDecParsedDecimal, 3, 1)
        tlpMain.Controls.Add(Label2, 0, 1)
        tlpMain.Controls.Add(tbRAParsed, 1, 0)
        tlpMain.Controls.Add(tbDecParsedShort, 2, 1)
        tlpMain.Location = New Point(8, 27)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 2
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpMain.Size = New Size(644, 87)
        tlpMain.TabIndex = 10
        ' 
        ' btnGetObject
        ' 
        btnGetObject.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnGetObject.Location = New Point(8, 120)
        btnGetObject.Name = "btnGetObject"
        btnGetObject.Size = New Size(75, 23)
        btnGetObject.TabIndex = 11
        btnGetObject.Text = "Get object"
        btnGetObject.UseVisualStyleBackColor = True
        ' 
        ' tbSelected
        ' 
        tbSelected.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbSelected.Font = New Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point)
        tbSelected.Location = New Point(89, 122)
        tbSelected.Name = "tbSelected"
        tbSelected.ReadOnly = True
        tbSelected.Size = New Size(419, 21)
        tbSelected.TabIndex = 10
        ' 
        ' btnGoTo
        ' 
        btnGoTo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnGoTo.Location = New Point(574, 120)
        btnGoTo.Name = "btnGoTo"
        btnGoTo.Size = New Size(75, 23)
        btnGoTo.TabIndex = 12
        btnGoTo.Text = "Goto"
        btnGoTo.UseVisualStyleBackColor = True
        ' 
        ' cbJ2000
        ' 
        cbJ2000.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cbJ2000.AutoSize = True
        cbJ2000.Checked = True
        cbJ2000.CheckState = CheckState.Checked
        cbJ2000.Location = New Point(514, 123)
        cbJ2000.Name = "cbJ2000"
        cbJ2000.Size = New Size(54, 19)
        cbJ2000.TabIndex = 13
        cbJ2000.Text = "J2000"
        cbJ2000.UseVisualStyleBackColor = True
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiEnter, GotoToolStripMenuItem})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(656, 24)
        msMain.TabIndex = 17
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_OpenEXE})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(37, 20)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_OpenEXE
        ' 
        tsmiFile_OpenEXE.Name = "tsmiFile_OpenEXE"
        tsmiFile_OpenEXE.Size = New Size(159, 22)
        tsmiFile_OpenEXE.Text = "Open EXE folder"
        ' 
        ' tsmiEnter
        ' 
        tsmiEnter.DropDownItems.AddRange(New ToolStripItem() {tsmiEnter_RA, tsmiEnter_Dec})
        tsmiEnter.Name = "tsmiEnter"
        tsmiEnter.Size = New Size(46, 20)
        tsmiEnter.Text = "Enter"
        ' 
        ' tsmiEnter_RA
        ' 
        tsmiEnter_RA.Name = "tsmiEnter_RA"
        tsmiEnter_RA.Size = New Size(183, 22)
        tsmiEnter_RA.Text = "RA (Right ascension)"
        ' 
        ' tsmiEnter_Dec
        ' 
        tsmiEnter_Dec.Name = "tsmiEnter_Dec"
        tsmiEnter_Dec.Size = New Size(183, 22)
        tsmiEnter_Dec.Text = "DEC (Declination)"
        ' 
        ' GotoToolStripMenuItem
        ' 
        GotoToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ObjectToolStripMenuItem, ZenithAndStopToolStripMenuItem})
        GotoToolStripMenuItem.Name = "GotoToolStripMenuItem"
        GotoToolStripMenuItem.Size = New Size(45, 20)
        GotoToolStripMenuItem.Text = "Goto"
        ' 
        ' ObjectToolStripMenuItem
        ' 
        ObjectToolStripMenuItem.Name = "ObjectToolStripMenuItem"
        ObjectToolStripMenuItem.Size = New Size(180, 22)
        ObjectToolStripMenuItem.Text = "Object and track"
        ' 
        ' ZenithAndStopToolStripMenuItem
        ' 
        ZenithAndStopToolStripMenuItem.Name = "ZenithAndStopToolStripMenuItem"
        ZenithAndStopToolStripMenuItem.Size = New Size(180, 22)
        ZenithAndStopToolStripMenuItem.Text = "Zenith and stop"
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(656, 150)
        Controls.Add(cbJ2000)
        Controls.Add(btnGoTo)
        Controls.Add(tbSelected)
        Controls.Add(btnGetObject)
        Controls.Add(tlpMain)
        Controls.Add(msMain)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = msMain
        Name = "MainForm"
        Text = "Astro coordinate tool"
        tlpMain.ResumeLayout(False)
        tlpMain.PerformLayout()
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents tbRAParsed As TextBox
    Friend WithEvents tbRAParsedShort As TextBox
    Friend WithEvents tbRAParsedDecimal As TextBox
    Friend WithEvents tbDecParsed As TextBox
    Friend WithEvents tbDecParsedShort As TextBox
    Friend WithEvents tbDecParsedDecimal As TextBox
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents btnGetObject As Button
    Friend WithEvents tbSelected As TextBox
    Friend WithEvents btnGoTo As Button
    Friend WithEvents cbJ2000 As CheckBox
    Friend WithEvents ttMain As ToolTip
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenEXE As ToolStripMenuItem
    Friend WithEvents tsmiEnter As ToolStripMenuItem
    Friend WithEvents tsmiEnter_RA As ToolStripMenuItem
    Friend WithEvents tsmiEnter_Dec As ToolStripMenuItem
    Friend WithEvents GotoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ObjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ZenithAndStopToolStripMenuItem As ToolStripMenuItem
End Class
