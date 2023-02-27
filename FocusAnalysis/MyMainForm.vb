Option Explicit On
Option Strict On

Public Class MyMainForm

    Private ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
    Private IPPPath As String = String.Empty

    '''<summary>Handle to Intel IPP functions.</summary>
    Private IntelIPP As cIntelIPP

    '''<summary>Statistics processor (for the last file).</summary>
    Private SingleStatCalc As AstroNET.Statistics

    '''<summary>Statistics of the last frame.</summary>
    Private LastStat As AstroNET.Statistics.sStatistics

    Private Sub tsmiFile_OpenSerSequence_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenSerSequence.Click

        With ofdMain
            .Filter = "SER file (*.ser)|*.ser"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        'Open the SER file
        Dim BinaryIN As New System.IO.BinaryReader(System.IO.File.OpenRead(ofdMain.FileName))
        Dim SERHeader As New cSERFormat.cSERHeader(BinaryIN)
        Dim TimeStamps As DateTime() = SERHeader.ReadTrailer(BinaryIN)

        SingleStatCalc.ResetAllProcessors()

        'Go on if stream position is ok
        If BinaryIN.BaseStream.Position = cSERFormat.cSERHeader.SERHeaderLength Then

            'We assume RGGB and 16 bit for now ...
            If SERHeader.BytePerPixel = 2 Then

                Dim FrameResults As New Dictionary(Of Integer, List(Of Object))

                pbImageStream.Maximum = SERHeader.FrameCount
                For FrameCountIdx As Integer = 1 To SERHeader.FrameCount

                    Dim FrameResult As New List(Of Object)
                    FrameResult.Add(FrameCountIdx)              'frame idx
                    FrameResult.Add("???")                      'focus position

                    pbImageStream.Value = FrameCountIdx
                    tImageStream.Text = FrameCountIdx.ValRegIndep & "/" & pbImageStream.Maximum.ValRegIndep
                    De()

                    '1.) Read in 1 frame and convert to 2-byte data type
                    Dim FullFrameSize As Integer = CInt(SERHeader.FrameWidth * SERHeader.FrameHeight * SERHeader.BytePerPixel)
                    ReDim SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data(SERHeader.FrameWidth - 1, SERHeader.FrameHeight - 1)
                    IntelIPP.Transpose(BinaryIN.ReadBytes(FullFrameSize), SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data)

                    'Calculate generic statistics
                    LastStat = SingleStatCalc.ImageStatistics

                    'Process focus relevant data
                    Dim EnergyPct As Double = 20.0
                    Dim TotalEnery As Long = AstroNET.Statistics.TotalEnergy(LastStat.MonochromHistogram_Int)
                    FrameResult.Add(TotalEnery)                 'total energy
                    Dim BinFor80pct As UInt64 = AstroNET.Statistics.FocusQualityIndicator(LastStat.MonochromHistogram_Int, EnergyPct)
                    FrameResult.Add(BinFor80pct)
                    Dim BinFor80pct_R As UInt64 = AstroNET.Statistics.FocusQualityIndicator(LastStat.BayerHistograms_Int(0, 0), EnergyPct)
                    FrameResult.Add(BinFor80pct_R)

                    'Store results
                    FrameResults.Add(FrameCountIdx, FrameResult) : De()

                Next FrameCountIdx

                'Indicate finished
                pbImageStream.Value = 0
                tImageStream.Text = pbImageStream.Maximum.ValRegIndep & " frames loaded"
                De()

                '4) Build EXCEL output, save and open
                Using workbook As New ClosedXML.Excel.XLWorkbook
                    Dim WorkSheet_Single As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Frame Statistics")
                    WorkSheet_Single.Cell(1, 1).InsertData(New List(Of String)({"Frame #", "Focus position", "Total Energy", "Total 80pct pixel #", "R channel 80pct pixel #"}), True)
                    For FrameCountIdx As Integer = 1 To SERHeader.FrameCount
                        WorkSheet_Single.Cell(FrameCountIdx + 1, 1).InsertData(FrameResults(FrameCountIdx), True)
                    Next FrameCountIdx
                    For Each col In WorkSheet_Single.ColumnsUsed
                        col.AdjustToContents()
                    Next col
                    Dim FileToGenerate As String = IO.Path.Combine(MyPath, "SERFocus.xlsx")
                    workbook.SaveAs(FileToGenerate)
                    Process.Start(FileToGenerate)
                End Using

            End If

        End If

    End Sub

    Private Sub MyMainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Load IPP
        Dim IPPLoadError As String = String.Empty
        IPPPath = cIntelIPP.SearchDLLToUse(cIntelIPP.PossiblePaths(MyPath).ToArray, IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = True Then
            IntelIPP = New cIntelIPP(IPPPath)
        End If
        SingleStatCalc = New AstroNET.Statistics(IntelIPP)
    End Sub

    Private Sub De()
        System.Windows.Forms.Application.DoEvents()
    End Sub

End Class
