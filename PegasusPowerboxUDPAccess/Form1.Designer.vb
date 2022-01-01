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
        Me.btnTest = New System.Windows.Forms.Button()
        Me.pgMain = New System.Windows.Forms.PropertyGrid()
        Me.btnWriteTest = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(12, 12)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(75, 23)
        Me.btnTest.TabIndex = 0
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'pgMain
        '
        Me.pgMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgMain.Location = New System.Drawing.Point(93, 12)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(695, 426)
        Me.pgMain.TabIndex = 1
        '
        'btnWriteTest
        '
        Me.btnWriteTest.Location = New System.Drawing.Point(12, 415)
        Me.btnWriteTest.Name = "btnWriteTest"
        Me.btnWriteTest.Size = New System.Drawing.Size(75, 23)
        Me.btnWriteTest.TabIndex = 2
        Me.btnWriteTest.Text = "Write test"
        Me.btnWriteTest.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnWriteTest)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.btnTest)
        Me.Name = "MainForm"
        Me.Text = "PegasusAstro Ultimate Powerbox UDP Access"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnTest As Button
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents btnWriteTest As Button
End Class
