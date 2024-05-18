<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGraph
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

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
        zgcMain = New ZedGraph.ZedGraphControl()
        tsMain = New ToolStrip()
        tscbObject = New ToolStripComboBox()
        ttMain = New ToolTip(components)
        tsMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' zgcMain
        ' 
        zgcMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        zgcMain.Location = New Point(0, 37)
        zgcMain.Margin = New Padding(5, 4, 5, 4)
        zgcMain.Name = "zgcMain"
        zgcMain.ScrollGrace = 0R
        zgcMain.ScrollMaxX = 0R
        zgcMain.ScrollMaxY = 0R
        zgcMain.ScrollMaxY2 = 0R
        zgcMain.ScrollMinX = 0R
        zgcMain.ScrollMinY = 0R
        zgcMain.ScrollMinY2 = 0R
        zgcMain.Size = New Size(1184, 855)
        zgcMain.TabIndex = 0
        ' 
        ' tsMain
        ' 
        tsMain.ImageScalingSize = New Size(20, 20)
        tsMain.Items.AddRange(New ToolStripItem() {tscbObject})
        tsMain.Location = New Point(0, 0)
        tsMain.Name = "tsMain"
        tsMain.Size = New Size(1184, 28)
        tsMain.TabIndex = 1
        tsMain.Text = "ToolStrip1"
        ' 
        ' tscbObject
        ' 
        tscbObject.Name = "tscbObject"
        tscbObject.Size = New Size(138, 28)
        ' 
        ' frmGraph
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1184, 893)
        Controls.Add(tsMain)
        Controls.Add(zgcMain)
        Margin = New Padding(3, 4, 3, 4)
        Name = "frmGraph"
        Text = "frmGraph"
        tsMain.ResumeLayout(False)
        tsMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents zgcMain As ZedGraph.ZedGraphControl
    Friend WithEvents tsMain As ToolStrip
    Friend WithEvents tscbObject As ToolStripComboBox
    Friend WithEvents ttMain As ToolTip
End Class
