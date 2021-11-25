Option Explicit On
Option Strict On

Public Class M
    '''<summary>DB that holds all relevant information.</summary>
    Public Shared WithEvents DB As New cDB
End Class

'''<summary>Database holding relevant information.</summary>
Public Class cDB

    <ComponentModel.Browsable(False)>
    Public Log_Generic As New Text.StringBuilder

End Class
