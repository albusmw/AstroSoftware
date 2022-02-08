Option Explicit On
Option Strict On

'''<summary>Commands for the database.</summary>
Public Class DBCommands

    '''<summary>Command to build the base table.</summary>
    '''<remarks>The most inportant informations are on-top</remarks>
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

End Class