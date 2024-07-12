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

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
    ' Form linking code
    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    Public Shared Function GetMyModifierForm(ByVal GUID As String) As frmImageModifier
        For Each SingleForm As System.Windows.Forms.Form In System.Windows.Forms.Application.OpenForms
            Try
                If CType(SingleForm, frmImageModifier).LinkedGUID = GUID Then Return CType(SingleForm, frmImageModifier)
            Catch ex As Exception
                'Do nothing ....
            End Try
        Next SingleForm
        Return Nothing
    End Function

    Public Shared Function GetMyHistoForm(ByVal GUID As String) As frmGraph
        For Each SingleForm As System.Windows.Forms.Form In System.Windows.Forms.Application.OpenForms
            Try
                If CType(SingleForm, frmGraph).LinkedGUID = GUID Then Return CType(SingleForm, frmGraph)
            Catch ex As Exception
                'Do nothing ....
            End Try
        Next SingleForm
        Return Nothing
    End Function

End Class
