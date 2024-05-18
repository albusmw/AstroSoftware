Option Explicit On
Option Strict On

Public Class DB

    '''<summary>Handle to Intel IPP functions.</summary>
    Public Shared IPP As cIntelIPP

    Public Shared ReadOnly Property IPPPath As String
        Get
            Return IPP.IPPPath
        End Get
    End Property

    '''<summary>Location of the EXE.</summary>
    Public Shared ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

    Public Shared Sub Init()

        'Load IPP
        Dim IPPLoadError = String.Empty
        Dim IPPPathToUse = cIntelIPP.SearchDLLToUse(cIntelIPP.PossiblePaths(MyPath).ToArray, IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = True Then
            IPP = New cIntelIPP(IPPPathToUse)
            cFITSWriter.UseIPPForWriting = True
        Else
            cFITSWriter.UseIPPForWriting = False
        End If
        cFITSWriter.IPPPath = IPP.IPPPath
        cFITSReader.IPPPath = IPP.IPPPath

    End Sub

End Class
