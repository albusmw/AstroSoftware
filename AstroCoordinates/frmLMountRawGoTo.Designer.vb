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
        btnAxis1Up = New Button()
        btnAxis1Down = New Button()
        lbAxis0Pos = New Label()
        lbAxis1Pos = New Label()
        gbMove = New GroupBox()
        tlpMovement = New TableLayoutPanel()
        Label2 = New Label()
        Label1 = New Label()
        cbRead = New CheckBox()
        tMountRead = New Timer(components)
        tbIncrement = New TextBox()
        Label3 = New Label()
        btnStartRaDecTracking = New Button()
        gbMove.SuspendLayout()
        tlpMovement.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnAxis0Up
        ' 
        btnAxis0Up.Dock = DockStyle.Fill
        btnAxis0Up.Font = New Font("Segoe UI", 18F)
        btnAxis0Up.Location = New Point(3, 59)
        btnAxis0Up.Name = "btnAxis0Up"
        btnAxis0Up.Size = New Size(131, 50)
        btnAxis0Up.TabIndex = 0
        btnAxis0Up.Text = "▲"
        btnAxis0Up.UseVisualStyleBackColor = True
        ' 
        ' btnAxis0Down
        ' 
        btnAxis0Down.Dock = DockStyle.Fill
        btnAxis0Down.Font = New Font("Segoe UI", 18F)
        btnAxis0Down.Location = New Point(3, 171)
        btnAxis0Down.Name = "btnAxis0Down"
        btnAxis0Down.Size = New Size(131, 50)
        btnAxis0Down.TabIndex = 1
        btnAxis0Down.Text = "▼"
        btnAxis0Down.UseVisualStyleBackColor = True
        ' 
        ' btnAxis1Up
        ' 
        btnAxis1Up.Dock = DockStyle.Fill
        btnAxis1Up.Font = New Font("Segoe UI", 18F)
        btnAxis1Up.Location = New Point(140, 59)
        btnAxis1Up.Name = "btnAxis1Up"
        btnAxis1Up.Size = New Size(131, 50)
        btnAxis1Up.TabIndex = 3
        btnAxis1Up.Text = "▲"
        btnAxis1Up.UseVisualStyleBackColor = True
        ' 
        ' btnAxis1Down
        ' 
        btnAxis1Down.Dock = DockStyle.Fill
        btnAxis1Down.Font = New Font("Segoe UI", 18F)
        btnAxis1Down.Location = New Point(140, 171)
        btnAxis1Down.Name = "btnAxis1Down"
        btnAxis1Down.Size = New Size(131, 50)
        btnAxis1Down.TabIndex = 4
        btnAxis1Down.Text = "▼"
        btnAxis1Down.UseVisualStyleBackColor = True
        ' 
        ' lbAxis0Pos
        ' 
        lbAxis0Pos.BorderStyle = BorderStyle.FixedSingle
        lbAxis0Pos.Dock = DockStyle.Fill
        lbAxis0Pos.Location = New Point(3, 112)
        lbAxis0Pos.Name = "lbAxis0Pos"
        lbAxis0Pos.Size = New Size(131, 56)
        lbAxis0Pos.TabIndex = 5
        lbAxis0Pos.Text = "???"
        lbAxis0Pos.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lbAxis1Pos
        ' 
        lbAxis1Pos.BorderStyle = BorderStyle.FixedSingle
        lbAxis1Pos.Dock = DockStyle.Fill
        lbAxis1Pos.Location = New Point(140, 112)
        lbAxis1Pos.Name = "lbAxis1Pos"
        lbAxis1Pos.Size = New Size(131, 56)
        lbAxis1Pos.TabIndex = 6
        lbAxis1Pos.Text = "???"
        lbAxis1Pos.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' gbMove
        ' 
        gbMove.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gbMove.Controls.Add(tlpMovement)
        gbMove.Location = New Point(12, 43)
        gbMove.Name = "gbMove"
        gbMove.Size = New Size(286, 254)
        gbMove.TabIndex = 7
        gbMove.TabStop = False
        gbMove.Text = "Movement"
        ' 
        ' tlpMovement
        ' 
        tlpMovement.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpMovement.ColumnCount = 2
        tlpMovement.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpMovement.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpMovement.Controls.Add(Label2, 1, 0)
        tlpMovement.Controls.Add(Label1, 0, 0)
        tlpMovement.Controls.Add(btnAxis0Down, 0, 3)
        tlpMovement.Controls.Add(btnAxis0Up, 0, 1)
        tlpMovement.Controls.Add(btnAxis1Down, 1, 3)
        tlpMovement.Controls.Add(lbAxis0Pos, 0, 2)
        tlpMovement.Controls.Add(lbAxis1Pos, 1, 2)
        tlpMovement.Controls.Add(btnAxis1Up, 1, 1)
        tlpMovement.Location = New Point(6, 24)
        tlpMovement.Name = "tlpMovement"
        tlpMovement.RowCount = 4
        tlpMovement.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tlpMovement.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tlpMovement.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tlpMovement.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tlpMovement.Size = New Size(274, 224)
        tlpMovement.TabIndex = 9
        ' 
        ' Label2
        ' 
        Label2.BorderStyle = BorderStyle.FixedSingle
        Label2.Dock = DockStyle.Fill
        Label2.Location = New Point(140, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(131, 56)
        Label2.TabIndex = 8
        Label2.Text = "Axis1"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label1
        ' 
        Label1.BorderStyle = BorderStyle.FixedSingle
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(3, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(131, 56)
        Label1.TabIndex = 7
        Label1.Text = "Axis0"
        Label1.TextAlign = ContentAlignment.MiddleCenter
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
        ' tbIncrement
        ' 
        tbIncrement.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        tbIncrement.Location = New Point(235, 10)
        tbIncrement.Name = "tbIncrement"
        tbIncrement.Size = New Size(57, 23)
        tbIncrement.TabIndex = 9
        tbIncrement.Text = "5"
        tbIncrement.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(136, 13)
        Label3.Name = "Label3"
        Label3.Size = New Size(80, 15)
        Label3.TabIndex = 10
        Label3.Text = "Increment [°]:"
        ' 
        ' btnStartRaDecTracking
        ' 
        btnStartRaDecTracking.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnStartRaDecTracking.Location = New Point(12, 303)
        btnStartRaDecTracking.Name = "btnStartRaDecTracking"
        btnStartRaDecTracking.Size = New Size(286, 30)
        btnStartRaDecTracking.TabIndex = 11
        btnStartRaDecTracking.Text = "Start RaDec tracking from here"
        btnStartRaDecTracking.UseVisualStyleBackColor = True
        ' 
        ' frmLMountRawGoTo
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(310, 345)
        Controls.Add(btnStartRaDecTracking)
        Controls.Add(Label3)
        Controls.Add(tbIncrement)
        Controls.Add(cbRead)
        Controls.Add(gbMove)
        Name = "frmLMountRawGoTo"
        Text = "L Mount Raw GoTo"
        gbMove.ResumeLayout(False)
        tlpMovement.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnAxis0Up As Button
    Friend WithEvents btnAxis0Down As Button
    Friend WithEvents btnAxis1Up As Button
    Friend WithEvents btnAxis1Down As Button
    Friend WithEvents lbAxis0Pos As Label
    Friend WithEvents lbAxis1Pos As Label
    Friend WithEvents gbMove As GroupBox
    Friend WithEvents cbRead As CheckBox
    Friend WithEvents tMountRead As Timer
    Friend WithEvents tlpMovement As TableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents tbIncrement As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnStartRaDecTracking As Button
End Class
