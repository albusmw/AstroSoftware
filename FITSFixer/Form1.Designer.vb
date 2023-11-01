<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        tbPath = New TextBox()
        btnRun = New Button()
        lbFiles = New ListBox()
        cbSim = New CheckBox()
        tbStatus = New TextBox()
        tUpdateStatus = New Timer(components)
        SuspendLayout()
        ' 
        ' tbPath
        ' 
        tbPath.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbPath.Location = New Point(12, 12)
        tbPath.Name = "tbPath"
        tbPath.Size = New Size(606, 23)
        tbPath.TabIndex = 0
        tbPath.Text = "\\192.168.100.10\astro"
        ' 
        ' btnRun
        ' 
        btnRun.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnRun.Location = New Point(713, 12)
        btnRun.Name = "btnRun"
        btnRun.Size = New Size(75, 23)
        btnRun.TabIndex = 1
        btnRun.Text = "Fix files"
        btnRun.UseVisualStyleBackColor = True
        ' 
        ' lbFiles
        ' 
        lbFiles.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lbFiles.FormattingEnabled = True
        lbFiles.IntegralHeight = False
        lbFiles.ItemHeight = 15
        lbFiles.Location = New Point(12, 77)
        lbFiles.Name = "lbFiles"
        lbFiles.Size = New Size(776, 363)
        lbFiles.TabIndex = 2
        ' 
        ' cbSim
        ' 
        cbSim.AutoSize = True
        cbSim.Checked = True
        cbSim.CheckState = CheckState.Checked
        cbSim.Location = New Point(624, 15)
        cbSim.Name = "cbSim"
        cbSim.Size = New Size(83, 19)
        cbSim.TabIndex = 3
        cbSim.Text = "Simulation"
        cbSim.UseVisualStyleBackColor = True
        ' 
        ' tbStatus
        ' 
        tbStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbStatus.Location = New Point(12, 41)
        tbStatus.Name = "tbStatus"
        tbStatus.ReadOnly = True
        tbStatus.Size = New Size(776, 23)
        tbStatus.TabIndex = 4
        tbStatus.Text = "---"
        ' 
        ' tUpdateStatus
        ' 
        tUpdateStatus.Enabled = True
        tUpdateStatus.Interval = 250
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(tbStatus)
        Controls.Add(cbSim)
        Controls.Add(lbFiles)
        Controls.Add(btnRun)
        Controls.Add(tbPath)
        Name = "Form1"
        Text = "FITS file length fixer"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tbPath As TextBox
    Friend WithEvents btnRun As Button
    Friend WithEvents lbFiles As ListBox
    Friend WithEvents cbSim As CheckBox
    Friend WithEvents tbStatus As TextBox
    Friend WithEvents tUpdateStatus As Timer
End Class
