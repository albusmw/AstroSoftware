Option Explicit On
Option Strict On
Imports System.IO
Imports System.Net.Security
Imports System.Reflection
Imports System.Resources
Imports AstroCoordinates.cAstroCats

Public Class frmGetObject

    Public ReadOnly Property SelectedObject As String
        Get
            Return MySelectedObject
        End Get
    End Property
    Private MySelectedObject As String = String.Empty

    Public ReadOnly Property SelectedRA As String
        Get
            Return MySelectedRA
        End Get
    End Property
    Private MySelectedRA As String = String.Empty

    Public ReadOnly Property SelectedDec As String
        Get
            Return MySelectedDec
        End Get
    End Property
    Private MySelectedDec As String = String.Empty

    Dim MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Dim Objects As New Dictionary(Of String, String())

    Private Sub frmGetObject_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Load catalogs copied from PixInsight
        Dim DoubleEntries As Integer = 0
        DoubleEntries += AddToCat(GetResourceLines("AstroCoordinates.messier.txt"))
        DoubleEntries += AddToCat(GetResourceLines("AstroCoordinates.namedStars.txt"))
        DoubleEntries += AddToCat(GetResourceLines("AstroCoordinates.ngc2000.txt"))

        tsslLoaded.Text = Objects.Count.ToString.Trim & " objects loaded, " & DoubleEntries.ToString.Trim & " double enties"

    End Sub

    Private Function GetResourceLines(ByVal Name As String) As String()
        Return Split((New StreamReader(Assembly.GetExecutingAssembly.GetManifestResourceStream(Name))).ReadToEnd, vbLf)
    End Function

    '''<summary>Load the PixInsight catalog data (tab separated).</summary>
    '''<param name="FileName">File to load.</param>
    '''<returns>Number of double entries.</returns>
    Private Function AddToCat(ByRef FileContent As String()) As Integer
        Dim RetVal As Integer = 0
        For Idx As Integer = 1 To FileContent.GetUpperBound(0)
            Dim FileLine As String = FileContent(Idx).Trim
            If FileLine.Length > 0 Then
                Dim Splitted As String() = Split(FileLine, vbTab)
                Dim Key As String = Splitted(0)
                If Objects.ContainsKey(Key) Then
                    RetVal += 1
                Else
                    Objects.Add(Splitted(0), Splitted)
                End If
            End If
        Next Idx
        Return RetVal
    End Function

    Private Sub tbSearchString_TextChanged(sender As Object, e As EventArgs) Handles tbSearchString.TextChanged
        Dim FoundEntries As New List(Of String)
        Dim SearchString As String = tbSearchString.Text.Trim.ToUpper
        For Each Entry As String In Objects.Keys
            If Entry.ToUpper.Contains(SearchString) Then
                'Search string found in the first element
                FoundEntries.Add(FormatElement(Objects(Entry)))
            Else
                Dim LastEntry As String = Objects(Entry)(Objects(Entry).GetUpperBound(0))
                If LastEntry.ToUpper.Contains(SearchString) Then
                    'Search string found in the last element
                    FoundEntries.Add(FormatElement(Objects(Entry)))
                Else
                    If Objects(Entry).Length = 8 Then
                        'Star cat
                        If Objects(Entry)(5).Contains(SearchString) Then
                            'HD
                            FoundEntries.Add(FormatElement(Objects(Entry)))
                        Else
                            If Objects(Entry)(6).Contains(SearchString) Then
                                'HIP
                                FoundEntries.Add(FormatElement(Objects(Entry)))
                            End If
                        End If
                    End If
                End If
            End If
        Next Entry
        lbResults.Items.Clear()
        lbResults.Items.AddRange(FoundEntries.ToArray)
        tsslSelectionLength.Text = FoundEntries.Count.ToString.Trim & " entries filtered"
    End Sub

    Private Function FormatElement(ByVal Element As String()) As String
        'Make a nice table
        Element(0) = Element(0).Trim.PadRight(20)
        Element(1) = Element(1).Trim.PadLeft(15)
        Element(2) = Element(2).Trim.PadLeft(15)
        Return Join(Element, vbTab)
    End Function

    Private Sub lbResults_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbResults.SelectedIndexChanged
        Dim Selection As String() = Split(CStr(lbResults.SelectedItem), vbTab)
        MySelectedObject = Selection(0).Trim
        If Selection.Length >= 6 Then MySelectedObject &= "(" & Selection(Selection.GetUpperBound(0)) & ")"
        MySelectedRA = Selection(1)
        MySelectedDec = Selection(2)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub tsmiLoadCat_Click(sender As Object, e As EventArgs) Handles tsmiLoadCat.Click
        'Load catalogs
        Dim Loader As New cHenryDraper
        Loader.DownloadData()
    End Sub

End Class