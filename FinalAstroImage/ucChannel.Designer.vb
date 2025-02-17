<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChannel
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        cbFile = New CheckBox()
        tbFile = New TextBox()
        SuspendLayout()
        ' 
        ' cbFile
        ' 
        cbFile.AutoSize = True
        cbFile.Checked = True
        cbFile.CheckState = CheckState.Checked
        cbFile.Location = New Point(3, 5)
        cbFile.Name = "cbFile"
        cbFile.Size = New Size(79, 19)
        cbFile.TabIndex = 5
        cbFile.Text = "Channel 1"
        cbFile.UseVisualStyleBackColor = True
        ' 
        ' tbFile
        ' 
        tbFile.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbFile.Location = New Point(88, 3)
        tbFile.Name = "tbFile"
        tbFile.Size = New Size(1047, 23)
        tbFile.TabIndex = 4
        tbFile.Text = "\\192.168.100.10\dsc\DSS_Autosave\2024_07_10 - NGC2070 Ha 1.tif"
        ' 
        ' ucChannel
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(cbFile)
        Controls.Add(tbFile)
        Name = "ucChannel"
        Size = New Size(1138, 32)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents cbFile As CheckBox
    Friend WithEvents tbFile As TextBox

End Class
