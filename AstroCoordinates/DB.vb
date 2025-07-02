Option Explicit On
Option Strict On

Public Class DB

    'My path
    Public Shared ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    'Planewave PWI4 interface
    Public Shared PWI4 As New cPWI4("http://localhost:8220")

End Class
