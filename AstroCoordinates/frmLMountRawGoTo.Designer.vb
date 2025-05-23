<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLMountRawGoTo
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
        components = New ComponentModel.Container()
        btnAxis0Up = New Button()
        btnAxis0Down = New Button()
        cbTrack = New CheckBox()
        btnAxis1Up = New Button()
        btnAxis1Down = New Button()
        lbAxis0Pos = New Label()
        lbAxis1Pos = New Label()
        gbMove = New GroupBox()
        cbRead = New CheckBox()
        tMountRead = New Timer(components)
        gbMove.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnAxis0Up
        ' 
        btnAxis0Up.Location = New Point(6, 22)
        btnAxis0Up.Name = "btnAxis0Up"
        btnAxis0Up.Size = New Size(59, 54)
        btnAxis0Up.TabIndex = 0
        btnAxis0Up.Text = "Axis0 Up"
        btnAxis0Up.UseVisualStyleBackColor = True
        ' 
        ' btnAxis0Down
        ' 
        btnAxis0Down.Location = New Point(6, 128)
        btnAxis0Down.Name = "btnAxis0Down"
        btnAxis0Down.Size = New Size(59, 54)
        btnAxis0Down.TabIndex = 1
        btnAxis0Down.Text = "Axis0 Up"
        btnAxis0Down.UseVisualStyleBackColor = True
        ' 
        ' cbTrack
        ' 
        cbTrack.AutoSize = True
        cbTrack.Location = New Point(12, 243)
        cbTrack.Name = "cbTrack"
        cbTrack.Size = New Size(54, 19)
        cbTrack.TabIndex = 2
        cbTrack.Text = "Track"
        cbTrack.UseVisualStyleBackColor = True
        ' 
        ' btnAxis1Up
        ' 
        btnAxis1Up.Location = New Point(81, 22)
        btnAxis1Up.Name = "btnAxis1Up"
        btnAxis1Up.Size = New Size(59, 54)
        btnAxis1Up.TabIndex = 3
        btnAxis1Up.Text = "Axis1 Up"
        btnAxis1Up.UseVisualStyleBackColor = True
        ' 
        ' btnAxis1Down
        ' 
        btnAxis1Down.Location = New Point(81, 128)
        btnAxis1Down.Name = "btnAxis1Down"
        btnAxis1Down.Size = New Size(59, 54)
        btnAxis1Down.TabIndex = 4
        btnAxis1Down.Text = "Axis1 Down"
        btnAxis1Down.UseVisualStyleBackColor = True
        ' 
        ' lbAxis0Pos
        ' 
        lbAxis0Pos.BorderStyle = BorderStyle.FixedSingle
        lbAxis0Pos.Location = New Point(6, 77)
        lbAxis0Pos.Name = "lbAxis0Pos"
        lbAxis0Pos.Size = New Size(57, 51)
        lbAxis0Pos.TabIndex = 5
        lbAxis0Pos.Text = "???"
        lbAxis0Pos.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lbAxis1Pos
        ' 
        lbAxis1Pos.BorderStyle = BorderStyle.FixedSingle
        lbAxis1Pos.Location = New Point(81, 77)
        lbAxis1Pos.Name = "lbAxis1Pos"
        lbAxis1Pos.Size = New Size(57, 51)
        lbAxis1Pos.TabIndex = 6
        lbAxis1Pos.Text = "???"
        lbAxis1Pos.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' gbMove
        ' 
        gbMove.Controls.Add(btnAxis0Up)
        gbMove.Controls.Add(lbAxis1Pos)
        gbMove.Controls.Add(btnAxis0Down)
        gbMove.Controls.Add(lbAxis0Pos)
        gbMove.Controls.Add(btnAxis1Up)
        gbMove.Controls.Add(btnAxis1Down)
        gbMove.Location = New Point(12, 43)
        gbMove.Name = "gbMove"
        gbMove.Size = New Size(150, 194)
        gbMove.TabIndex = 7
        gbMove.TabStop = False
        gbMove.Text = "Movement"
        ' 
        ' cbRead
        ' 
        cbRead.AutoSize = True
        cbRead.Location = New Point(18, 12)
        cbRead.Name = "cbRead"
        cbRead.Size = New Size(52, 19)
        cbRead.TabIndex = 8
        cbRead.Text = "Read"
        cbRead.UseVisualStyleBackColor = True
        ' 
        ' tMountRead
        ' 
        ' 
        ' frmLMountRawGoTo
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(172, 275)
        Controls.Add(cbRead)
        Controls.Add(gbMove)
        Controls.Add(cbTrack)
        Name = "frmLMountRawGoTo"
        Text = "L Mount Raw GoTo"
        gbMove.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnAxis0Up As Button
    Friend WithEvents btnAxis0Down As Button
    Friend WithEvents cbTrack As CheckBox
    Friend WithEvents btnAxis1Up As Button
    Friend WithEvents btnAxis1Down As Button
    Friend WithEvents lbAxis0Pos As Label
    Friend WithEvents lbAxis1Pos As Label
    Friend WithEvents gbMove As GroupBox
    Friend WithEvents cbRead As CheckBox
    Friend WithEvents tMountRead As Timer
End Class
