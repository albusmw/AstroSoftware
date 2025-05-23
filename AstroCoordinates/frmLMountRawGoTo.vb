Option Explicit On
Option Strict On

Public Class frmLMountRawGoTo

    Private Sub cbRead_CheckedChanged(sender As Object, e As EventArgs) Handles cbRead.CheckedChanged
        tMountRead.Enabled = cbRead.Checked
    End Sub

    Private Sub tMountRead_Tick(sender As Object, e As EventArgs) Handles tMountRead.Tick
        DB.PWI4.LoadStatus()
        Dim Axis0 As Double = CType(DB.PWI4.GetValue(ePWI4.mount__axis0__position_degs), Double)
        Dim Axis1 As Double = CType(DB.PWI4.GetValue(ePWI4.mount__axis1__position_degs), Double)
        lbAxis0Pos.Text = Axis0.ValRegIndep("000.0000")
        lbAxis1Pos.Text = Axis1.ValRegIndep("000.0000")
    End Sub

End Class