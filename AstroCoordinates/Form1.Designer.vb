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
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(MainForm))
        Label1 = New Label()
        Label2 = New Label()
        btnParseRA = New Button()
        btnParseDec = New Button()
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
        tbDecOffset = New TextBox()
        tbRAOffset = New TextBox()
        ttMain = New ToolTip(components)
        tlpMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(3, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(157, 93)
        Label1.TabIndex = 0
        Label1.Text = "RA (Right ascension)"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label2
        ' 
        Label2.Dock = DockStyle.Fill
        Label2.Location = New Point(3, 93)
        Label2.Name = "Label2"
        Label2.Size = New Size(157, 93)
        Label2.TabIndex = 1
        Label2.Text = "DEC (Declination)"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnParseRA
        ' 
        btnParseRA.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnParseRA.Location = New Point(166, 126)
        btnParseRA.Name = "btnParseRA"
        btnParseRA.Size = New Size(157, 26)
        btnParseRA.TabIndex = 2
        btnParseRA.Text = "Enter"
        btnParseRA.UseVisualStyleBackColor = True
        ' 
        ' btnParseDec
        ' 
        btnParseDec.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnParseDec.Location = New Point(166, 34)
        btnParseDec.Name = "btnParseDec"
        btnParseDec.Size = New Size(157, 25)
        btnParseDec.TabIndex = 3
        btnParseDec.Text = "Enter"
        btnParseDec.UseVisualStyleBackColor = True
        ' 
        ' tbRAParsed
        ' 
        tbRAParsed.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbRAParsed.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbRAParsed.Location = New Point(329, 36)
        tbRAParsed.Name = "tbRAParsed"
        tbRAParsed.Size = New Size(157, 21)
        tbRAParsed.TabIndex = 4
        ' 
        ' tbRAParsedShort
        ' 
        tbRAParsedShort.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbRAParsedShort.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbRAParsedShort.Location = New Point(492, 36)
        tbRAParsedShort.Name = "tbRAParsedShort"
        tbRAParsedShort.Size = New Size(157, 21)
        tbRAParsedShort.TabIndex = 5
        ' 
        ' tbRAParsedDecimal
        ' 
        tbRAParsedDecimal.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbRAParsedDecimal.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbRAParsedDecimal.Location = New Point(655, 36)
        tbRAParsedDecimal.Name = "tbRAParsedDecimal"
        tbRAParsedDecimal.Size = New Size(158, 21)
        tbRAParsedDecimal.TabIndex = 6
        ' 
        ' tbDecParsed
        ' 
        tbDecParsed.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDecParsed.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbDecParsed.Location = New Point(329, 129)
        tbDecParsed.Name = "tbDecParsed"
        tbDecParsed.Size = New Size(157, 21)
        tbDecParsed.TabIndex = 7
        ' 
        ' tbDecParsedShort
        ' 
        tbDecParsedShort.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDecParsedShort.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbDecParsedShort.Location = New Point(492, 129)
        tbDecParsedShort.Name = "tbDecParsedShort"
        tbDecParsedShort.Size = New Size(157, 21)
        tbDecParsedShort.TabIndex = 8
        ' 
        ' tbDecParsedDecimal
        ' 
        tbDecParsedDecimal.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDecParsedDecimal.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point)
        tbDecParsedDecimal.Location = New Point(655, 129)
        tbDecParsedDecimal.Name = "tbDecParsedDecimal"
        tbDecParsedDecimal.Size = New Size(158, 21)
        tbDecParsedDecimal.TabIndex = 9
        ' 
        ' tlpMain
        ' 
        tlpMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpMain.ColumnCount = 5
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpMain.Controls.Add(Label1, 0, 0)
        tlpMain.Controls.Add(tbRAParsedDecimal, 4, 0)
        tlpMain.Controls.Add(tbRAParsedShort, 3, 0)
        tlpMain.Controls.Add(tbDecParsed, 2, 1)
        tlpMain.Controls.Add(tbDecParsedDecimal, 4, 1)
        tlpMain.Controls.Add(Label2, 0, 1)
        tlpMain.Controls.Add(btnParseDec, 1, 0)
        tlpMain.Controls.Add(btnParseRA, 1, 1)
        tlpMain.Controls.Add(tbRAParsed, 2, 0)
        tlpMain.Controls.Add(tbDecParsedShort, 3, 1)
        tlpMain.Location = New Point(5, 12)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 2
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpMain.Size = New Size(816, 186)
        tlpMain.TabIndex = 10
        ' 
        ' btnGetObject
        ' 
        btnGetObject.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnGetObject.Location = New Point(8, 204)
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
        tbSelected.Location = New Point(89, 206)
        tbSelected.Name = "tbSelected"
        tbSelected.ReadOnly = True
        tbSelected.Size = New Size(464, 21)
        tbSelected.TabIndex = 10
        ' 
        ' btnGoTo
        ' 
        btnGoTo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnGoTo.Location = New Point(743, 204)
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
        cbJ2000.Location = New Point(683, 207)
        cbJ2000.Name = "cbJ2000"
        cbJ2000.Size = New Size(54, 19)
        cbJ2000.TabIndex = 13
        cbJ2000.Text = "J2000"
        cbJ2000.UseVisualStyleBackColor = True
        ' 
        ' tbDecOffset
        ' 
        tbDecOffset.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        tbDecOffset.Location = New Point(621, 204)
        tbDecOffset.Name = "tbDecOffset"
        tbDecOffset.Size = New Size(56, 23)
        tbDecOffset.TabIndex = 14
        tbDecOffset.Text = "0"
        tbDecOffset.TextAlign = HorizontalAlignment.Center
        ttMain.SetToolTip(tbDecOffset, "Dec Offset in ArcMin")
        ' 
        ' tbRAOffset
        ' 
        tbRAOffset.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        tbRAOffset.Location = New Point(559, 204)
        tbRAOffset.Name = "tbRAOffset"
        tbRAOffset.Size = New Size(56, 23)
        tbRAOffset.TabIndex = 15
        tbRAOffset.Text = "0"
        tbRAOffset.TextAlign = HorizontalAlignment.Center
        ttMain.SetToolTip(tbRAOffset, "RA Offset in ArcMin")
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(825, 234)
        Controls.Add(tbRAOffset)
        Controls.Add(tbDecOffset)
        Controls.Add(cbJ2000)
        Controls.Add(btnGoTo)
        Controls.Add(tbSelected)
        Controls.Add(btnGetObject)
        Controls.Add(tlpMain)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "MainForm"
        Text = "Astro coordinate tool"
        tlpMain.ResumeLayout(False)
        tlpMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnParseRA As Button
    Friend WithEvents btnParseDec As Button
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
    Friend WithEvents tbDecOffset As TextBox
    Friend WithEvents tbRAOffset As TextBox
    Friend WithEvents ttMain As ToolTip
End Class
