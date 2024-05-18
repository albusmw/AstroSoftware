<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImage
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

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        pbMain = New PictureBox()
        CType(pbMain, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pbMain
        ' 
        pbMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pbMain.BackColor = Color.Gray
        pbMain.Location = New Point(10, 9)
        pbMain.Margin = New Padding(3, 2, 3, 2)
        pbMain.Name = "pbMain"
        pbMain.Size = New Size(890, 545)
        pbMain.SizeMode = PictureBoxSizeMode.Zoom
        pbMain.TabIndex = 0
        pbMain.TabStop = False
        ' 
        ' frmImage
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(911, 563)
        Controls.Add(pbMain)
        Name = "frmImage"
        Text = "Image display"
        CType(pbMain, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pbMain As PictureBox
End Class
