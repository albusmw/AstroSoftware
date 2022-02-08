<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.btnRunTest = New System.Windows.Forms.Button()
        Me.tbLogOutput = New System.Windows.Forms.TextBox()
        Me.btnClearLog = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnRunTest
        '
        Me.btnRunTest.Location = New System.Drawing.Point(12, 12)
        Me.btnRunTest.Name = "btnRunTest"
        Me.btnRunTest.Size = New System.Drawing.Size(75, 23)
        Me.btnRunTest.TabIndex = 0
        Me.btnRunTest.Text = "Run test"
        Me.btnRunTest.UseVisualStyleBackColor = True
        '
        'tbLogOutput
        '
        Me.tbLogOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLogOutput.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tbLogOutput.Location = New System.Drawing.Point(12, 41)
        Me.tbLogOutput.Multiline = True
        Me.tbLogOutput.Name = "tbLogOutput"
        Me.tbLogOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLogOutput.Size = New System.Drawing.Size(1250, 1025)
        Me.tbLogOutput.TabIndex = 1
        Me.tbLogOutput.WordWrap = False
        '
        'btnClearLog
        '
        Me.btnClearLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearLog.Location = New System.Drawing.Point(1187, 12)
        Me.btnClearLog.Name = "btnClearLog"
        Me.btnClearLog.Size = New System.Drawing.Size(75, 23)
        Me.btnClearLog.TabIndex = 2
        Me.btnClearLog.Text = "Clear log"
        Me.btnClearLog.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1274, 1078)
        Me.Controls.Add(Me.btnClearLog)
        Me.Controls.Add(Me.tbLogOutput)
        Me.Controls.Add(Me.btnRunTest)
        Me.Name = "MainForm"
        Me.Text = "MainForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnRunTest As Button
    Friend WithEvents tbLogOutput As TextBox
    Friend WithEvents btnClearLog As Button
End Class
