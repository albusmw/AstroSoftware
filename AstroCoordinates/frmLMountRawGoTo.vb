Option Explicit On
Option Strict On
Imports System.Linq.Expressions

Public Class frmLMountRawGoTo

    Dim Axis0 As Double = Double.NaN
    Dim Axis1 As Double = Double.NaN

    Private ReadOnly Property Increment() As Double
        Get
            Return tbIncrement.Text.ValRegIndep
        End Get
    End Property

    Private Sub cbRead_CheckedChanged(sender As Object, e As EventArgs) Handles cbRead.CheckedChanged
        tMountRead.Enabled = cbRead.Checked
    End Sub

    Private Sub btnAxis0Up_Click(sender As Object, e As EventArgs) Handles btnAxis0Up.Click
        UpdateAxisPos()
        Dim Response As String = Download.GetResponse(DB.PWI4.Command.Goto_Raw(Axis0 + Increment, Axis1))
    End Sub

    Private Sub btnAxis0Down_Click(sender As Object, e As EventArgs) Handles btnAxis0Down.Click
        UpdateAxisPos()
        Dim Response As String = Download.GetResponse(DB.PWI4.Command.Goto_Raw(Axis0 - Increment, Axis1))
    End Sub

    Private Sub btnAxis1Up_Click(sender As Object, e As EventArgs) Handles btnAxis1Up.Click
        UpdateAxisPos()
        Dim Response As String = Download.GetResponse(DB.PWI4.Command.Goto_Raw(Axis0, Axis1 + Increment))
    End Sub

    Private Sub btnAxis1Down_Click(sender As Object, e As EventArgs) Handles btnAxis1Down.Click
        UpdateAxisPos()
        Dim Response As String = Download.GetResponse(DB.PWI4.Command.Goto_Raw(Axis0, Axis1 - Increment))
    End Sub

    Private Sub tMountRead_Tick(sender As Object, e As EventArgs) Handles tMountRead.Tick
        UpdateAxisPos()
    End Sub

    Private Sub UpdateAxisPos()
        DB.PWI4.LoadStatus()
        Axis0 = CType(DB.PWI4.GetValue(ePWI4.mount__axis0__position_degs), Double)
        Axis1 = CType(DB.PWI4.GetValue(ePWI4.mount__axis1__position_degs), Double)
        lbAxis0Pos.Text = Axis0.ValRegIndep("000.000000")
        lbAxis1Pos.Text = Axis1.ValRegIndep("000.000000")
    End Sub

    Private Sub lbAxis0Pos_Click(sender As Object, e As EventArgs) Handles lbAxis0Pos.Click
        DB.PWI4.LoadStatus()
        Dim NewPos As String = InputBox("Axis0 Goto ...", "Axis0 GOTO", lbAxis0Pos.Text)
        Dim Response As String = Download.GetResponse(DB.PWI4.Command.Goto_Raw(NewPos.ValRegIndep, Axis1))
    End Sub

    Private Sub lbAxis1Pos_Click(sender As Object, e As EventArgs) Handles lbAxis1Pos.Click
        DB.PWI4.LoadStatus()
        Dim NewPos As String = InputBox("Axis1 Goto ...", "Axis1 GOTO", lbAxis1Pos.Text)
        Dim Response As String = Download.GetResponse(DB.PWI4.Command.Goto_Raw(Axis0, NewPos.ValRegIndep))
    End Sub

    Private Sub btnStartRaDecTracking_Click(sender As Object, e As EventArgs) Handles btnStartRaDecTracking.Click
        DB.PWI4.LoadStatus()
        Dim Ra As Double = CType(DB.PWI4.GetValue(ePWI4.mount__ra_j2000_hours), Double)
        Dim Dec As Double = CType(DB.PWI4.GetValue(ePWI4.mount__dec_j2000_degs), Double)
        Download.GetResponse(DB.PWI4.Command.Goto_RaDec(True, Ra, Dec))
        Download.GetResponse(DB.PWI4.Command.Tracking(True))
    End Sub

End Class