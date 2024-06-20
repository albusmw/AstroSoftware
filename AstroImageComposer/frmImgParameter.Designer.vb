<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImgParameter
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
        rtbMain = New RichTextBox()
        SuspendLayout()
        ' 
        ' rtbMain
        ' 
        rtbMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        rtbMain.Location = New Point(12, 12)
        rtbMain.Name = "rtbMain"
        rtbMain.Size = New Size(852, 510)
        rtbMain.TabIndex = 0
        rtbMain.Text = ""
        rtbMain.WordWrap = False
        ' 
        ' frmImgParameter
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(876, 534)
        Controls.Add(rtbMain)
        Name = "frmImgParameter"
        Text = "Image parameter"
        ResumeLayout(False)
    End Sub

    Friend WithEvents rtbMain As RichTextBox
End Class
