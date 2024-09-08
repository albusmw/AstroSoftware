<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImageModifier
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

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
        pgMain = New PropertyGrid()
        tlpMain = New TableLayoutPanel()
        btnLower = New Button()
        btnHigher = New Button()
        tlpMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' pgMain
        ' 
        pgMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgMain.Location = New Point(12, 12)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(812, 817)
        pgMain.TabIndex = 0
        ' 
        ' tlpMain
        ' 
        tlpMain.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpMain.ColumnCount = 2
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpMain.Controls.Add(btnLower, 0, 0)
        tlpMain.Controls.Add(btnHigher, 1, 0)
        tlpMain.Location = New Point(12, 835)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 1
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpMain.Size = New Size(812, 50)
        tlpMain.TabIndex = 1
        ' 
        ' btnLower
        ' 
        btnLower.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnLower.Location = New Point(3, 3)
        btnLower.Name = "btnLower"
        btnLower.Size = New Size(400, 44)
        btnLower.TabIndex = 0
        btnLower.Text = "DOWN"
        btnLower.UseVisualStyleBackColor = True
        ' 
        ' btnHigher
        ' 
        btnHigher.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnHigher.Location = New Point(409, 3)
        btnHigher.Name = "btnHigher"
        btnHigher.Size = New Size(400, 44)
        btnHigher.TabIndex = 1
        btnHigher.Text = "UP"
        btnHigher.UseVisualStyleBackColor = True
        ' 
        ' frmImageModifier
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(836, 888)
        Controls.Add(tlpMain)
        Controls.Add(pgMain)
        Name = "frmImageModifier"
        Text = "Image modifier"
        tlpMain.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents btnLower As Button
    Friend WithEvents btnHigher As Button
End Class
