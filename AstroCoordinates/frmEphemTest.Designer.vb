<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEphemTest
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
        btnTest = New Button()
        btnHorizon = New Button()
        tbHorizonOutput = New TextBox()
        SuspendLayout()
        ' 
        ' btnTest
        ' 
        btnTest.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnTest.Location = New Point(683, 8)
        btnTest.Name = "btnTest"
        btnTest.Size = New Size(105, 51)
        btnTest.TabIndex = 0
        btnTest.Text = "Run test"
        btnTest.UseVisualStyleBackColor = True
        ' 
        ' btnHorizon
        ' 
        btnHorizon.Location = New Point(12, 12)
        btnHorizon.Name = "btnHorizon"
        btnHorizon.Size = New Size(244, 47)
        btnHorizon.TabIndex = 1
        btnHorizon.Text = "Horizon loader"
        btnHorizon.UseVisualStyleBackColor = True
        ' 
        ' tbHorizonOutput
        ' 
        tbHorizonOutput.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbHorizonOutput.Font = New Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbHorizonOutput.Location = New Point(12, 65)
        tbHorizonOutput.Multiline = True
        tbHorizonOutput.Name = "tbHorizonOutput"
        tbHorizonOutput.ScrollBars = ScrollBars.Both
        tbHorizonOutput.Size = New Size(776, 373)
        tbHorizonOutput.TabIndex = 2
        tbHorizonOutput.WordWrap = False
        ' 
        ' frmEphemTest
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(tbHorizonOutput)
        Controls.Add(btnHorizon)
        Controls.Add(btnTest)
        Name = "frmEphemTest"
        Text = "Ephemerides test"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnTest As Button
    Friend WithEvents btnHorizon As Button
    Friend WithEvents tbHorizonOutput As TextBox
End Class
