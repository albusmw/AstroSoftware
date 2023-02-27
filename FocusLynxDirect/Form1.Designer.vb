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
        Me.btnTest = New System.Windows.Forms.Button()
        Me.tbInfo = New System.Windows.Forms.TextBox()
        Me.btnTempComp = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(22, 26)
        Me.btnTest.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(195, 81)
        Me.btnTest.TabIndex = 0
        Me.btnTest.Text = "Read status"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'tbInfo
        '
        Me.tbInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbInfo.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tbInfo.Location = New System.Drawing.Point(22, 116)
        Me.tbInfo.Multiline = True
        Me.tbInfo.Name = "tbInfo"
        Me.tbInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbInfo.Size = New System.Drawing.Size(1452, 832)
        Me.tbInfo.TabIndex = 1
        '
        'btnTempComp
        '
        Me.btnTempComp.Location = New System.Drawing.Point(229, 26)
        Me.btnTempComp.Margin = New System.Windows.Forms.Padding(6)
        Me.btnTempComp.Name = "btnTempComp"
        Me.btnTempComp.Size = New System.Drawing.Size(365, 81)
        Me.btnTempComp.TabIndex = 2
        Me.btnTempComp.Text = "Temperature Compensation"
        Me.btnTempComp.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 32.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1486, 960)
        Me.Controls.Add(Me.btnTempComp)
        Me.Controls.Add(Me.tbInfo)
        Me.Controls.Add(Me.btnTest)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnTest As Button
    Friend WithEvents tbInfo As TextBox
    Friend WithEvents btnTempComp As Button
End Class
