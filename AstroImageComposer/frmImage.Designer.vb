<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImage
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

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        ssMain = New StatusStrip()
        tssl_Coord = New ToolStripStatusLabel()
        cmsMain = New ContextMenuStrip(components)
        tsmi_ThisLOWEnd = New ToolStripMenuItem()
        tsmi_ThisUPPEREnd = New ToolStripMenuItem()
        tsmi_FromHisto = New ToolStripMenuItem()
        tsmi_FullEnd = New ToolStripMenuItem()
        ssMain.SuspendLayout()
        cmsMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' ssMain
        ' 
        ssMain.ImageScalingSize = New Size(20, 20)
        ssMain.Items.AddRange(New ToolStripItem() {tssl_Coord})
        ssMain.Location = New Point(0, 541)
        ssMain.Name = "ssMain"
        ssMain.Padding = New Padding(1, 0, 12, 0)
        ssMain.Size = New Size(911, 22)
        ssMain.TabIndex = 1
        ssMain.Text = "StatusStrip1"
        ' 
        ' tssl_Coord
        ' 
        tssl_Coord.Name = "tssl_Coord"
        tssl_Coord.Size = New Size(56, 17)
        tssl_Coord.Text = "Coord: ??"
        ' 
        ' cmsMain
        ' 
        cmsMain.ImageScalingSize = New Size(20, 20)
        cmsMain.Items.AddRange(New ToolStripItem() {tsmi_ThisLOWEnd, tsmi_ThisUPPEREnd, tsmi_FromHisto, tsmi_FullEnd})
        cmsMain.Name = "cmsMain"
        cmsMain.Size = New Size(230, 114)
        ' 
        ' tsmi_ThisLOWEnd
        ' 
        tsmi_ThisLOWEnd.Name = "tsmi_ThisLOWEnd"
        tsmi_ThisLOWEnd.Size = New Size(229, 22)
        tsmi_ThisLOWEnd.Text = "Value as LUT lower end"
        ' 
        ' tsmi_ThisUPPEREnd
        ' 
        tsmi_ThisUPPEREnd.Name = "tsmi_ThisUPPEREnd"
        tsmi_ThisUPPEREnd.Size = New Size(229, 22)
        tsmi_ThisUPPEREnd.Text = "Value as LUT upper end"
        ' 
        ' tsmi_FromHisto
        ' 
        tsmi_FromHisto.Name = "tsmi_FromHisto"
        tsmi_FromHisto.Size = New Size(229, 22)
        tsmi_FromHisto.Text = "Value from histo axis as limits"
        ' 
        ' tsmi_FullEnd
        ' 
        tsmi_FullEnd.Name = "tsmi_FullEnd"
        tsmi_FullEnd.Size = New Size(229, 22)
        tsmi_FullEnd.Text = "Full-range ends as used"
        ' 
        ' frmImage
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(911, 563)
        Controls.Add(ssMain)
        Name = "frmImage"
        Text = "Image display"
        ssMain.ResumeLayout(False)
        ssMain.PerformLayout()
        cmsMain.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tssl_Coord As ToolStripStatusLabel
    Friend WithEvents cmsMain As ContextMenuStrip
    Friend WithEvents tsmi_ThisLOWEnd As ToolStripMenuItem
    Friend WithEvents tsmi_ThisUPPEREnd As ToolStripMenuItem
    Friend WithEvents tsmi_FromHisto As ToolStripMenuItem
    Friend WithEvents tsmi_FullEnd As ToolStripMenuItem
End Class
