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
        btnCalculate = New Button()
        tbLog = New TextBox()
        SuspendLayout()
        ' 
        ' btnCalculate
        ' 
        btnCalculate.Location = New Point(12, 12)
        btnCalculate.Name = "btnCalculate"
        btnCalculate.Size = New Size(75, 23)
        btnCalculate.TabIndex = 0
        btnCalculate.Text = "Calculate"
        btnCalculate.UseVisualStyleBackColor = True
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbLog.Location = New Point(12, 41)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(776, 397)
        tbLog.TabIndex = 1
        tbLog.WordWrap = False
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(tbLog)
        Controls.Add(btnCalculate)
        Name = "MainForm"
        Text = "InView"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnCalculate As Button
    Friend WithEvents tbLog As TextBox

End Class
