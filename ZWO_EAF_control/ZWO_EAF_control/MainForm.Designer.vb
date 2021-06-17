<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnMoveIn = New System.Windows.Forms.Button()
        Me.tUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.btnMoveOut = New System.Windows.Forms.Button()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsslPosition = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tbStepSize = New System.Windows.Forms.TextBox()
        Me.ssMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(12, 12)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(126, 70)
        Me.btnOpen.TabIndex = 0
        Me.btnOpen.Text = "Open focuser"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnMoveIn
        '
        Me.btnMoveIn.Location = New System.Drawing.Point(396, 12)
        Me.btnMoveIn.Name = "btnMoveIn"
        Me.btnMoveIn.Size = New System.Drawing.Size(84, 34)
        Me.btnMoveIn.TabIndex = 1
        Me.btnMoveIn.Text = "Move IN"
        Me.btnMoveIn.UseVisualStyleBackColor = True
        '
        'tUpdate
        '
        '
        'btnMoveOut
        '
        Me.btnMoveOut.Location = New System.Drawing.Point(396, 52)
        Me.btnMoveOut.Name = "btnMoveOut"
        Me.btnMoveOut.Size = New System.Drawing.Size(84, 34)
        Me.btnMoveOut.TabIndex = 2
        Me.btnMoveOut.Text = "Move OUT"
        Me.btnMoveOut.UseVisualStyleBackColor = True
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslPosition})
        Me.ssMain.Location = New System.Drawing.Point(0, 428)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(800, 22)
        Me.ssMain.TabIndex = 3
        Me.ssMain.Text = "StatusStrip1"
        '
        'tsslPosition
        '
        Me.tsslPosition.Name = "tsslPosition"
        Me.tsslPosition.Size = New System.Drawing.Size(71, 17)
        Me.tsslPosition.Text = "Position: ???"
        '
        'tbStepSize
        '
        Me.tbStepSize.Location = New System.Drawing.Point(486, 38)
        Me.tbStepSize.Name = "tbStepSize"
        Me.tbStepSize.Size = New System.Drawing.Size(66, 20)
        Me.tbStepSize.TabIndex = 4
        Me.tbStepSize.Text = "100"
        Me.tbStepSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.tbStepSize)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.btnMoveOut)
        Me.Controls.Add(Me.btnMoveIn)
        Me.Controls.Add(Me.btnOpen)
        Me.Name = "MainForm"
        Me.Text = "Form1"
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnOpen As Button
    Friend WithEvents btnMoveIn As Button
    Friend WithEvents tUpdate As Timer
    Friend WithEvents btnMoveOut As Button
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslPosition As ToolStripStatusLabel
    Friend WithEvents tbStepSize As TextBox
End Class
