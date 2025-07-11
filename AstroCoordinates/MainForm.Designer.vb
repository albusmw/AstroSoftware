﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        tbRAParsed = New TextBox()
        tbRAParsedShort = New TextBox()
        tbRAParsedDecimal = New TextBox()
        tbDecParsed = New TextBox()
        tbDecParsedShort = New TextBox()
        tbDecParsedDecimal = New TextBox()
        tlpMain = New TableLayoutPanel()
        btnRA = New Button()
        btnDec = New Button()
        btnGetObject = New Button()
        tbSelected = New TextBox()
        btnGoTo = New Button()
        cbJ2000 = New CheckBox()
        ttMain = New ToolTip(components)
        bgMain = New ComponentModel.BackgroundWorker()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_OpenEXE = New ToolStripMenuItem()
        tsmiFile_DropBoxCAT = New ToolStripMenuItem()
        tsmiFile_LoadVizier = New ToolStripMenuItem()
        tsmiFile_AstroBin = New ToolStripMenuItem()
        tsmiFile_InTheSky = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiFile_End = New ToolStripMenuItem()
        tsmiEnter = New ToolStripMenuItem()
        tsmiEnter_RA = New ToolStripMenuItem()
        tsmiEnter_Dec = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        tsmiEnter_RADeg = New ToolStripMenuItem()
        tsmiEnter_ParseClipboard = New ToolStripMenuItem()
        tsmiGoTo = New ToolStripMenuItem()
        tsmiGoTo_Object = New ToolStripMenuItem()
        tsmiGoTo_ZenithAndStop = New ToolStripMenuItem()
        tsmiGoTo_SunOpposition = New ToolStripMenuItem()
        RawGotoCommandToolStripMenuItem = New ToolStripMenuItem()
        tlpMain.SuspendLayout()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tbRAParsed
        ' 
        tbRAParsed.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbRAParsed.Font = New Font("Courier New", 9F)
        tbRAParsed.Location = New Point(130, 16)
        tbRAParsed.Name = "tbRAParsed"
        tbRAParsed.Size = New Size(115, 21)
        tbRAParsed.TabIndex = 4
        ' 
        ' tbRAParsedShort
        ' 
        tbRAParsedShort.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbRAParsedShort.Font = New Font("Courier New", 9F)
        tbRAParsedShort.Location = New Point(251, 16)
        tbRAParsedShort.Name = "tbRAParsedShort"
        tbRAParsedShort.Size = New Size(115, 21)
        tbRAParsedShort.TabIndex = 5
        ' 
        ' tbRAParsedDecimal
        ' 
        tbRAParsedDecimal.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbRAParsedDecimal.Font = New Font("Courier New", 9F)
        tbRAParsedDecimal.Location = New Point(372, 16)
        tbRAParsedDecimal.Name = "tbRAParsedDecimal"
        tbRAParsedDecimal.Size = New Size(117, 21)
        tbRAParsedDecimal.TabIndex = 6
        ' 
        ' tbDecParsed
        ' 
        tbDecParsed.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDecParsed.Font = New Font("Courier New", 9F)
        tbDecParsed.Location = New Point(130, 69)
        tbDecParsed.Name = "tbDecParsed"
        tbDecParsed.Size = New Size(115, 21)
        tbDecParsed.TabIndex = 7
        ' 
        ' tbDecParsedShort
        ' 
        tbDecParsedShort.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDecParsedShort.Font = New Font("Courier New", 9F)
        tbDecParsedShort.Location = New Point(251, 69)
        tbDecParsedShort.Name = "tbDecParsedShort"
        tbDecParsedShort.Size = New Size(115, 21)
        tbDecParsedShort.TabIndex = 8
        ' 
        ' tbDecParsedDecimal
        ' 
        tbDecParsedDecimal.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDecParsedDecimal.Font = New Font("Courier New", 9F)
        tbDecParsedDecimal.Location = New Point(372, 69)
        tbDecParsedDecimal.Name = "tbDecParsedDecimal"
        tbDecParsedDecimal.Size = New Size(117, 21)
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
        tlpMain.Controls.Add(tbRAParsedDecimal, 3, 0)
        tlpMain.Controls.Add(tbRAParsedShort, 2, 0)
        tlpMain.Controls.Add(tbDecParsed, 1, 1)
        tlpMain.Controls.Add(tbDecParsedDecimal, 3, 1)
        tlpMain.Controls.Add(tbRAParsed, 1, 0)
        tlpMain.Controls.Add(tbDecParsedShort, 2, 1)
        tlpMain.Controls.Add(btnRA, 0, 0)
        tlpMain.Controls.Add(btnDec, 0, 1)
        tlpMain.Location = New Point(8, 27)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 2
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpMain.Size = New Size(492, 107)
        tlpMain.TabIndex = 10
        ' 
        ' btnRA
        ' 
        btnRA.Dock = DockStyle.Fill
        btnRA.Location = New Point(3, 3)
        btnRA.Name = "btnRA"
        btnRA.Size = New Size(121, 47)
        btnRA.TabIndex = 10
        btnRA.Text = "RA" & vbCrLf & "(Right ascension)"
        btnRA.UseVisualStyleBackColor = True
        ' 
        ' btnDec
        ' 
        btnDec.Dock = DockStyle.Fill
        btnDec.Location = New Point(3, 56)
        btnDec.Name = "btnDec"
        btnDec.Size = New Size(121, 48)
        btnDec.TabIndex = 11
        btnDec.Text = "DEC" & vbCrLf & "(Declination)"
        btnDec.UseVisualStyleBackColor = True
        ' 
        ' btnGetObject
        ' 
        btnGetObject.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnGetObject.Location = New Point(8, 140)
        btnGetObject.Name = "btnGetObject"
        btnGetObject.Size = New Size(75, 23)
        btnGetObject.TabIndex = 11
        btnGetObject.Text = "Get object"
        btnGetObject.UseVisualStyleBackColor = True
        ' 
        ' tbSelected
        ' 
        tbSelected.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbSelected.Font = New Font("Courier New", 9F, FontStyle.Bold)
        tbSelected.Location = New Point(89, 142)
        tbSelected.Name = "tbSelected"
        tbSelected.Size = New Size(267, 21)
        tbSelected.TabIndex = 10
        ' 
        ' btnGoTo
        ' 
        btnGoTo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnGoTo.Location = New Point(422, 140)
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
        cbJ2000.Location = New Point(362, 143)
        cbJ2000.Name = "cbJ2000"
        cbJ2000.Size = New Size(54, 19)
        cbJ2000.TabIndex = 13
        cbJ2000.Text = "J2000"
        cbJ2000.UseVisualStyleBackColor = True
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiEnter, tsmiGoTo})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(504, 24)
        msMain.TabIndex = 17
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_OpenEXE, tsmiFile_DropBoxCAT, tsmiFile_LoadVizier, tsmiFile_AstroBin, tsmiFile_InTheSky, ToolStripMenuItem1, tsmiFile_End})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(37, 20)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_OpenEXE
        ' 
        tsmiFile_OpenEXE.Name = "tsmiFile_OpenEXE"
        tsmiFile_OpenEXE.Size = New Size(180, 22)
        tsmiFile_OpenEXE.Text = "Open EXE folder"
        ' 
        ' tsmiFile_DropBoxCAT
        ' 
        tsmiFile_DropBoxCAT.Name = "tsmiFile_DropBoxCAT"
        tsmiFile_DropBoxCAT.Size = New Size(180, 22)
        tsmiFile_DropBoxCAT.Text = "Load Dropbox CAT"
        ' 
        ' tsmiFile_LoadVizier
        ' 
        tsmiFile_LoadVizier.Name = "tsmiFile_LoadVizier"
        tsmiFile_LoadVizier.Size = New Size(180, 22)
        tsmiFile_LoadVizier.Text = "Load Vizier catalogs"
        ' 
        ' tsmiFile_AstroBin
        ' 
        tsmiFile_AstroBin.Name = "tsmiFile_AstroBin"
        tsmiFile_AstroBin.Size = New Size(180, 22)
        tsmiFile_AstroBin.Text = "AstroBin around"
        ' 
        ' tsmiFile_InTheSky
        ' 
        tsmiFile_InTheSky.Name = "tsmiFile_InTheSky"
        tsmiFile_InTheSky.Size = New Size(180, 22)
        tsmiFile_InTheSky.Text = "In the sky"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(177, 6)
        ' 
        ' tsmiFile_End
        ' 
        tsmiFile_End.Name = "tsmiFile_End"
        tsmiFile_End.Size = New Size(180, 22)
        tsmiFile_End.Text = "Exit"
        ' 
        ' tsmiEnter
        ' 
        tsmiEnter.DropDownItems.AddRange(New ToolStripItem() {tsmiEnter_RA, tsmiEnter_Dec, ToolStripMenuItem2, tsmiEnter_RADeg, tsmiEnter_ParseClipboard})
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
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(180, 6)
        ' 
        ' tsmiEnter_RADeg
        ' 
        tsmiEnter_RADeg.Name = "tsmiEnter_RADeg"
        tsmiEnter_RADeg.Size = New Size(183, 22)
        tsmiEnter_RADeg.Text = "RA [°]"
        ' 
        ' tsmiEnter_ParseClipboard
        ' 
        tsmiEnter_ParseClipboard.Name = "tsmiEnter_ParseClipboard"
        tsmiEnter_ParseClipboard.Size = New Size(183, 22)
        tsmiEnter_ParseClipboard.Text = "Parse clipboard"
        ' 
        ' tsmiGoTo
        ' 
        tsmiGoTo.DropDownItems.AddRange(New ToolStripItem() {tsmiGoTo_Object, tsmiGoTo_ZenithAndStop, tsmiGoTo_SunOpposition, RawGotoCommandToolStripMenuItem})
        tsmiGoTo.Name = "tsmiGoTo"
        tsmiGoTo.Size = New Size(45, 20)
        tsmiGoTo.Text = "Goto"
        ' 
        ' tsmiGoTo_Object
        ' 
        tsmiGoTo_Object.Name = "tsmiGoTo_Object"
        tsmiGoTo_Object.Size = New Size(206, 22)
        tsmiGoTo_Object.Text = "Object and track"
        ' 
        ' tsmiGoTo_ZenithAndStop
        ' 
        tsmiGoTo_ZenithAndStop.Name = "tsmiGoTo_ZenithAndStop"
        tsmiGoTo_ZenithAndStop.Size = New Size(206, 22)
        tsmiGoTo_ZenithAndStop.Text = "Zenith and stop"
        ' 
        ' tsmiGoTo_SunOpposition
        ' 
        tsmiGoTo_SunOpposition.Name = "tsmiGoTo_SunOpposition"
        tsmiGoTo_SunOpposition.Size = New Size(206, 22)
        tsmiGoTo_SunOpposition.Text = "Sun opposition and track"
        ' 
        ' RawGotoCommandToolStripMenuItem
        ' 
        RawGotoCommandToolStripMenuItem.Name = "RawGotoCommandToolStripMenuItem"
        RawGotoCommandToolStripMenuItem.Size = New Size(206, 22)
        RawGotoCommandToolStripMenuItem.Text = "Raw goto command ..."
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(504, 170)
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
    Friend WithEvents bgMain As System.ComponentModel.BackgroundWorker
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenEXE As ToolStripMenuItem
    Friend WithEvents tsmiEnter As ToolStripMenuItem
    Friend WithEvents tsmiEnter_RA As ToolStripMenuItem
    Friend WithEvents tsmiEnter_Dec As ToolStripMenuItem
    Friend WithEvents tsmiGoTo As ToolStripMenuItem
    Friend WithEvents tsmiGoTo_Object As ToolStripMenuItem
    Friend WithEvents tsmiGoTo_ZenithAndStop As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_End As ToolStripMenuItem
    Friend WithEvents tsmiFile_LoadVizier As ToolStripMenuItem
    Friend WithEvents tsmiGoTo_SunOpposition As ToolStripMenuItem
    Friend WithEvents tsmiFile_AstroBin As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents tsmiEnter_RADeg As ToolStripMenuItem
    Friend WithEvents btnRA As Button
    Friend WithEvents btnDec As Button
    Friend WithEvents tsmiFile_InTheSky As ToolStripMenuItem
    Friend WithEvents tsmiEnter_ParseClipboard As ToolStripMenuItem
    Friend WithEvents tsmiFile_DropBoxCAT As ToolStripMenuItem
    Friend WithEvents RawGotoCommandToolStripMenuItem As ToolStripMenuItem
End Class
