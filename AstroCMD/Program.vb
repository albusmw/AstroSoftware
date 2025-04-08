Option Explicit On
Option Strict On
Imports System.Text

Module Program

    '''<summary>Location of the EXE.</summary>
    Public ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

    '''<summary>Handle to Intel IPP functions.</summary>
    Public IPP As cIntelIPP

    '''<summary>Loaded FITS header entries.</summary>
    Public FITSHeader As Dictionary(Of eFITSKeywords, Object)

    Public AllLog As New List(Of String)

    Sub Main(args As String())

        Dim FileName As String = String.Empty
        Dim Stopper As New Stopwatch
        AllLog = New List(Of String)

        'Testmode
        If args.Count = 0 Then
            FileName = "C:\Users\albus\OneDrive\Transfer_Kevin_Morefield\QHY600_L_300_025_020_003_060_ExtendFullwell.fits"
            FileName = "C:\!Work\TestData\QHY600_H_alpha_480_056_050_001_060_Photographic.fits"
            FileName = "C:\!Work\TestData\NGC7293 (Helix nebula)_00002.fits"
            FileName = "C:\!Work\TestData\ShanFano1.fits"
            FileName = "\\192.168.100.10\dsc\2025_03_31 - Flat OIII\Flat OIII_00011.fits"
        End If

        Dim ForceDirect As Boolean = False

        'Load IPP
        Dim IPPLoadError = String.Empty
        Dim IPPPathToUse = cIntelIPP.SearchDLLToUse(cIntelIPP.PossiblePaths(MyPath).ToArray, IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = True Then
            IPP = New cIntelIPP(IPPPathToUse)
        Else
            IPP = Nothing
        End If

        Dim Container As New AstroNET.Statistics(IPP)
        Dim UseIPP As Boolean = True : If IsNothing(IPP) Then UseIPP = False
        Dim DataStartPos As Integer = -1
        Dim FITSReader As New cFITSReader(IPPPathToUse)

        'Load the image data and calculate the statistics
        Dim FITSHeader As New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileName, DataStartPos))
        Container.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(FileName, UseIPP, ForceDirect)
        Dim Stat As AstroNET.Statistics.sStatistics = Container.ImageStatistics()
        AllLog.Add("Statistics report:")
        AllLog.AddRange(Container.ImageStatistics.StatisticsReport(True, True, {"R", "G1", "G2", "B"}))
        AllLog.Add("══════════════════════════════════════════════════════════════════════════════════════")

        '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

        AllLog.Add("Starting Shannon-Fano:")

        'Code book generation
        Stopper.Restart() : Stopper.Start()
        Dim Dic As Dictionary(Of UInt16, ULong) = Stat.MonochromHistogram_Uint16
        Dim SFGen As New cShanFano(Of UInt16)
        AllLog.AddRange(SFGen.GenCodeBook(Dic))
        Stopper.Stop()
        AllLog.Add("Codebook generation: <" & Stopper.ElapsedMilliseconds.ValRegIndep & " ms>")

        'Compression
        Stopper.Restart() : Stopper.Start()
        SFGen.Compress(Container.DataProcessor_UInt16.ImageData(0).Data)
        Stopper.Stop()
        AllLog.Add("Coding: <" & Stopper.ElapsedMilliseconds.ValRegIndep & " ms>")
        AllLog.Add("══════════════════════════════════════════════════════════════════════════════════════")

        'Store
        Stopper.Restart() : Stopper.Start()
        SFGen.StoreSFTar("C:\!Work\TestData\ShanFano1fits.tar")
        AllLog.Add("Storing: <" & Stopper.ElapsedMilliseconds.ValRegIndep & " ms>")
        AllLog.Add("══════════════════════════════════════════════════════════════════════════════════════")

        'Normal compression
        'AllLog.Add("Normal compression comparison")
        'Stopper.Restart() : Stopper.Start()
        'AllLog.AddRange(SFGen.CompressStandard(FileName))
        'Stopper.Stop()
        'AllLog.Add("Coding: <" & Stopper.ElapsedMilliseconds.ValRegIndep & " ms>")
        'AllLog.Add("══════════════════════════════════════════════════════════════════════════════════════")

        'Show an output window if required
        Using X As New frmLogDisplay
            X.Show(AllLog)
            Do
                System.Windows.Forms.Application.DoEvents()
            Loop Until X.IsDisposed
        End Using

        Console.WriteLine("Hello World!")

        'End
        Console.WriteLine("Press any key to continue ...")
        Console.ReadKey()

    End Sub



End Module
