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
        SuspendLayout()
        ' 
        ' pgMain
        ' 
        pgMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgMain.Location = New Point(12, 12)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(772, 700)
        pgMain.TabIndex = 0
        ' 
        ' frmImageModifier
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(796, 724)
        Controls.Add(pgMain)
        Name = "frmImageModifier"
        Text = "Image modifier"
        ResumeLayout(False)
    End Sub

    Friend WithEvents pgMain As PropertyGrid
End Class
