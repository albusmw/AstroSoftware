Option Explicit On
Option Strict On

Public Class Form1

    Dim X As New cFocusLynx()
    Dim NL As String = System.Environment.NewLine

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        X.Lynx_Location = "192.168.10.125"
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Running
        Dim FocuserID As String = String.Empty
        Dim ErrorCode As String = X.Connect(FocuserID)
        Dim Status As Dictionary(Of String, String) = X.GetStatus
        Dim Configuration As Dictionary(Of String, String) = X.GetConfig
        Dim HubInfo As Dictionary(Of String, String) = X.GetHubInfo
        Dim DevType As String = X.DecodeDeviceType(Configuration("Dev Typ"))
        tbInfo.Text = X.GetStatus_Str(Status) & NL & X.GetConfig_Str(Configuration) & NL & X.GetHubInfo_Str(HubInfo)
        X.Disconnect()
        Done
    End Sub

    Private Sub btnTempComp_Click(sender As Object, e As EventArgs) Handles btnTempComp.Click

        Dim FocuserID As String = String.Empty
        Dim ErrorCode As String = X.Connect(FocuserID)
        Dim Status As Boolean = CBool(IIf(MsgBox("Activate", vbYesNo) = MsgBoxResult.Yes, True, False))
        Dim ExecutionStatus As String = X.SetTempComp(Status)
        If String.IsNullOrEmpty(ExecutionStatus) = True Then
            tbInfo.Text = "<OK>"
        Else
            tbInfo.Text = "!!!" & ExecutionStatus
        End If
        X.Disconnect()

    End Sub

    Private Sub Running()
        tbInfo.BackColor = Color.LightGray : System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub Done()
        tbInfo.BackColor = Color.White : System.Windows.Forms.Application.DoEvents()
    End Sub


End Class
