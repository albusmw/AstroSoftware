Option Explicit On
Option Strict On

Public Class Form1

    Dim FITSBlockSize As Integer = 2880
    Dim ScannedFile As Integer = 0
    Dim OKFiles As Integer = 0
    Dim BadFiles As Integer = 0

    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click

        btnRun.Enabled = False
        lbFiles.Items.Clear()
        ScannedFile = 0
        OKFiles = 0
        BadFiles = 0
        RunFix(tbPath.Text)
        btnRun.Enabled = True

    End Sub

    Private Sub RunFix(ByVal EntryDir As String)

        ScanDirFiles(EntryDir)
        For Each Directory As String In System.IO.Directory.GetDirectories(EntryDir)
            'lbFiles.Items.Add(Directory)
            RunFix(Directory)
        Next Directory

    End Sub

    Private Sub ScanDirFiles(ByVal Dir As String)

        For Each File As String In System.IO.Directory.GetFiles(Dir, "*.fit?")
            ScannedFile += 1
            Dim FileLength As Long = New System.IO.FileInfo(File).Length
            Dim ExpectedSize As Long = CInt(Math.Ceiling(FileLength / FITSBlockSize)) * FITSBlockSize
            If FileLength = ExpectedSize Then
                OKFiles += 1
                'lbFiles.Items.Add("OK: <" & File & ">")
            Else
                BadFiles += 1
                Dim MissingBytes As Integer = CInt(ExpectedSize - FileLength)
                Dim BytesToAdd As Byte() = Enumerable.Repeat(Of Byte)(&H20, MissingBytes).ToArray
                If cbSim.Checked = False Then
                    Using MyStream As New IO.FileStream(File, IO.FileMode.Append)
                        MyStream.Write(BytesToAdd)
                        MyStream.Flush()
                        MyStream.Close()
                    End Using
                    Dim NewFileLength As Long = New System.IO.FileInfo(File).Length
                    If NewFileLength = ExpectedSize Then
                        lbFiles.Items.Add("FIXED: <" & File & ">")
                    Else
                        lbFiles.Items.Add("PROBLEM: <" & File & ">")
                    End If
                Else
                    lbFiles.Items.Add("BAD: <" & File & ">")
                End If
            End If
            DE()
        Next File


    End Sub

    Private Sub tUpdateStatus_Tick(sender As Object, e As EventArgs) Handles tUpdateStatus.Tick
        tbStatus.Text = ScannedFile.ToString.Trim & " files scanned, " & OKFiles.ToString.Trim & " files ok, " & BadFiles.ToString.Trim & " files bad"
    End Sub

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

End Class
