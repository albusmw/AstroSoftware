<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParseCoordinates
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        tbCoordinatesText = New TextBox()
        Label2 = New Label()
        Label3 = New Label()
        tbParsedDec = New TextBox()
        tbParsedRA = New TextBox()
        btnOK = New Button()
        btnCancel = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 15)
        Label1.Name = "Label1"
        Label1.Size = New Size(138, 15)
        Label1.TabIndex = 0
        Label1.Text = "Coordinates text to parse"
        ' 
        ' tbCoordinatesText
        ' 
        tbCoordinatesText.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbCoordinatesText.Location = New Point(156, 12)
        tbCoordinatesText.Name = "tbCoordinatesText"
        tbCoordinatesText.Size = New Size(186, 23)
        tbCoordinatesText.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 44)
        Label2.Name = "Label2"
        Label2.Size = New Size(60, 15)
        Label2.TabIndex = 2
        Label2.Text = "Parsed RA"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(12, 73)
        Label3.Name = "Label3"
        Label3.Size = New Size(67, 15)
        Label3.TabIndex = 3
        Label3.Text = "Parsed DEC"
        ' 
        ' tbParsedDec
        ' 
        tbParsedDec.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbParsedDec.Location = New Point(156, 70)
        tbParsedDec.Name = "tbParsedDec"
        tbParsedDec.Size = New Size(186, 23)
        tbParsedDec.TabIndex = 4
        ' 
        ' tbParsedRA
        ' 
        tbParsedRA.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbParsedRA.Location = New Point(156, 41)
        tbParsedRA.Name = "tbParsedRA"
        tbParsedRA.Size = New Size(186, 23)
        tbParsedRA.TabIndex = 5
        ' 
        ' btnOK
        ' 
        btnOK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnOK.Location = New Point(267, 108)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(75, 23)
        btnOK.TabIndex = 6
        btnOK.Text = "OK"
        btnOK.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCancel.Location = New Point(12, 108)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(75, 23)
        btnCancel.TabIndex = 7
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' frmParseCoordinates
        ' 
        AcceptButton = btnOK
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btnCancel
        ClientSize = New Size(354, 143)
        Controls.Add(btnCancel)
        Controls.Add(btnOK)
        Controls.Add(tbParsedRA)
        Controls.Add(tbParsedDec)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(tbCoordinatesText)
        Controls.Add(Label1)
        Name = "frmParseCoordinates"
        Text = "Parse coordinates"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents tbCoordinatesText As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents tbParsedDec As TextBox
    Friend WithEvents tbParsedRA As TextBox
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
End Class
