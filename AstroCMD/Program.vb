Option Explicit On
Option Strict On
Imports System.ComponentModel

Module Program

    '''<summary>Location of the EXE.</summary>
    Public ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

    '''<summary>Handle to Intel IPP functions.</summary>
    Public IPP As cIntelIPP

    '''<summary>Loaded FITS header entries.</summary>
    Public FITSHeader As Dictionary(Of eFITSKeywords, Object)

    Sub Main(args As String())

        Dim FileName As String = "C:\Users\albus\OneDrive\Transfer_Kevin_Morefield\QHY600_L_300_025_020_003_060_ExtendFullwell.fits"
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

        Dim FITSHeader As New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileName, DataStartPos))
        Container.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(FileName, UseIPP, ForceDirect)
        Dim Stat As AstroNET.Statistics.sStatistics = Container.ImageStatistics()

        Console.WriteLine("Hello World!")

    End Sub

End Module
