Option Explicit On
Option Strict On

'''<summary>Commands for the database.</summary>
Public Class DBCommands

    '''<summary>Command to build the base table.</summary>
    '''<remarks>The most inportant informations are on-top.</remarks>
    Public Shared Function CMD_BuildTable(ByVal TableName As String, ByVal FITSKeywords As List(Of String)) As String

        Dim StdType As String = "STRING DEFAULT NULL"
        Dim SQLCommand As String =
                "CREATE TABLE IF NOT EXISTS
                [" & TableName & "] (
                [FileName] STRING ,"
        For Each FITSKeyword As String In FITSKeywords
            SQLCommand &= "[" & FITSKeyword & "] " & StdType & ","
        Next FITSKeyword
        SQLCommand = SQLCommand.Substring(0, SQLCommand.Length - 1) & ")"

        Return SQLCommand

    End Function

    '''<summary>Command to save 1 transaction.</summary>
    '''<param name="TableName">Table to access.</param>
    Public Shared Function CMD_SaveFileInfo(ByVal TableName As String, ByVal FileName As String, ByVal Keywords As Dictionary(Of eFITSKeywords, Object)) As String

        Dim Parameters As New Dictionary(Of String, String)

        'Store FITS information
        AddParam(Parameters, "FileName", FileName)
        For Each Entry As KeyValuePair(Of eFITSKeywords, Object) In Keywords
            AddParam(Parameters, FITSKeyword.KeywordString(Entry.Key), Entry.Value)
        Next Entry

        Dim Cols As New List(Of String) : Dim Values As New List(Of String) : Parameters.KeyAndValueList(Cols, Values)
        Dim ColsCommand As String = "(" & Join(Cols.ToArray, ", ") & ")"
        Dim ValuesCommand As String = "(" & Join(Values.ToArray, ",") & ")"
        Return "INSERT INTO " & TableName & " " & ColsCommand & " VALUES " & ValuesCommand & ";"

    End Function

    Private Shared Sub AddParam(ByRef AllParams As Dictionary(Of String, String), ByVal Name As String, ByVal Parameter As Object)
        'If IsNothing(Parameter) = False Then AllParams.Add(Name, SQLT.Q(Parameter))
    End Sub

End Class