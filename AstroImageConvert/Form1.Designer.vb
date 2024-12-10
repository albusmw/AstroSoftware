<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        tbTIFFFile = New TextBox()
        btnProcess = New Button()
        lbExamples = New ListBox()
        tbLog = New TextBox()
        SuspendLayout()
        ' 
        ' tbTIFFFile
        ' 
        tbTIFFFile.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbTIFFFile.Location = New Point(12, 12)
        tbTIFFFile.Name = "tbTIFFFile"
        tbTIFFFile.Size = New Size(1033, 23)
        tbTIFFFile.TabIndex = 0
        ' 
        ' btnProcess
        ' 
        btnProcess.Location = New Point(12, 41)
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(75, 23)
        btnProcess.TabIndex = 1
        btnProcess.Text = "Process"
        btnProcess.UseVisualStyleBackColor = True
        ' 
        ' lbExamples
        ' 
        lbExamples.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lbExamples.FormattingEnabled = True
        lbExamples.ItemHeight = 15
        lbExamples.Location = New Point(12, 70)
        lbExamples.Name = "lbExamples"
        lbExamples.Size = New Size(1033, 184)
        lbExamples.TabIndex = 2
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Location = New Point(12, 260)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(1033, 426)
        tbLog.TabIndex = 3
        tbLog.WordWrap = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1057, 698)
        Controls.Add(tbLog)
        Controls.Add(lbExamples)
        Controls.Add(btnProcess)
        Controls.Add(tbTIFFFile)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tbTIFFFile As TextBox
    Friend WithEvents btnProcess As Button
    Friend WithEvents lbExamples As ListBox
    Friend WithEvents tbLog As TextBox

End Class
