Option Explicit On
Option Strict On
Imports System.IO
Imports System.Net.Security
Imports System.Reflection
Imports System.Resources
Imports AstroCoordinates.cAstroCats

Public Class frmGetObject

    Dim ColSep As String = "|"

    'DSC coordinates
    Private MyLat As Double = -(30 + (31 / 60) + (34.7 / 3600))     'according to Overview.md
    Private MyLong As Double = -(70 + (51 / 60) + (11.8 / 3600))    'according to Overview.md

    Public ReadOnly Property SelectedObject As String
        Get
            Return MySelectedObject
        End Get
    End Property
    Private MySelectedObject As String = String.Empty

    Public ReadOnly Property SelectedRA As Double
        Get
            Return MySelectedRA
        End Get
    End Property
    Private MySelectedRA As Double = Double.NaN

    Public ReadOnly Property SelectedDec As Double
        Get
            Return MySelectedDec
        End Get
    End Property
    Private MySelectedDec As Double = Double.NaN

    Dim MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Dim Objects As New Dictionary(Of String, String())

    Private Sub frmGetObject_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Load catalogs copied from PixInsight
        Dim DoubleEntries As Integer = 0
        DoubleEntries += AddToCat(GetResourceLines("AstroCoordinates.messier.txt"))
        DoubleEntries += AddToCat(GetResourceLines("AstroCoordinates.namedStars.txt"))
        DoubleEntries += AddToCat(GetResourceLines("AstroCoordinates.ngc2000.txt"))

        'Load custom objects
        Dim CustomCat As String = System.IO.Path.Combine(MyPath, "CustomCatalog.txt")
        If System.IO.File.Exists(CustomCat) = True Then
            DoubleEntries += AddToCat(System.IO.File.ReadAllLines(CustomCat))
        End If

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
        If lbResults.Items.Count = 1 Then lbResults.SelectedIndex = 0
        tsslSelectionLength.Text = FoundEntries.Count.ToString.Trim & " entries filtered"
    End Sub

    Private Function FormatElement(ByVal Element As String()) As String
        'Make a nice table
        Element(0) = Element(0).Trim.PadRight(15)
        Element(1) = Element(1).Trim.PadLeft(10)
        Element(2) = Element(2).Trim.PadLeft(10)
        Return Join(Element, ColSep)
    End Function

    Private Sub tsmiLoadCat_Click(sender As Object, e As EventArgs) Handles tsmiLoadCat.Click
        'Load catalogs
        Dim Loader As New cHenryDraper
        Loader.DownloadData()
    End Sub

    Private Sub lbResults_DoubleClick(sender As Object, e As EventArgs) Handles lbResults.DoubleClick
        ParseSelection()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ParseSelection()
        Dim Selection As String() = Split(CStr(lbResults.SelectedItem), ColSep)
        MySelectedObject = Selection(0).Trim
        If Selection.Length >= 6 Then MySelectedObject &= " (" & Selection(Selection.GetUpperBound(0)) & ")"
        MySelectedRA = AstroParser.ParseRA(Selection(1))
        MySelectedDec = AstroParser.ParseDeclination(Selection(2))
    End Sub

    Private Sub tUpdateDetails_Tick(sender As Object, e As EventArgs) Handles tUpdateDetails.Tick
        Try
            'Display selected object information
            ParseSelection()
            Dim CurrentUTC As DateTime = DateTime.UtcNow
            Dim CurrentLocation As New Ato.AstroCalc.sLatLong(MyLat, MyLong)
            Dim ObjectCoord As New Ato.AstroCalc.sRADec(SelectedRA, SelectedDec)
            Dim ObjectHourAngle As Double = Double.NaN
            Dim Pos As Ato.AstroCalc.sAzAlt = GetObjectPosition(CurrentUTC, CurrentLocation, ObjectCoord)
            Dim Details As New List(Of String)
            Details.Add("Details:")
            Details.Add(" Object: " & Split(CStr(lbResults.SelectedItem), ColSep)(0).Trim)
            Details.Add("   RA                : " & Ato.AstroCalc.FormatHMS(SelectedRA))
            Details.Add("   Dec               : " & Ato.AstroCalc.Format360Degree(SelectedDec))
            Details.Add("   Alt               :  " & Ato.AstroCalc.Format360Degree(Pos.ALT))
            Details.Add("   Az                :  " & Ato.AstroCalc.Format360Degree(Pos.AZ))
            Details.Add("   Hour angle        :  " & Ato.AstroCalc.FormatHMS(ObjectHourAngle * (24 / 360)))
            Details.Add("═══════════════════════════════════════════════════════")
            Details.Add(" UTC time            : " & Format(CurrentUTC, "HH:mm:ss zzz"))
            Details.Add(" Local Siderial Time : " & Ato.AstroCalc.LST(CurrentUTC, MyLong).ValRegIndep)
            Details.Add(" Local Siderial Time : " & Ato.AstroCalc.LSTFormated(CurrentUTC, MyLong))
            Details.Add(" Location latitude   : " & Ato.AstroCalc.Format360Degree(MyLat))
            Details.Add(" Location longitude  : " & Ato.AstroCalc.Format360Degree(MyLong))
            tbDetails.Text = Join(Details.ToArray, System.Environment.NewLine)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnLocationDSC_Click(sender As Object, e As EventArgs) Handles btnLocationDSC.Click
        MyLat = -(30 + (31 / 60) + (34.7 / 3600))       'according to Overview.md
        MyLong = -(70 + (51 / 60) + (11.8 / 3600))      'according to Overview.md
    End Sub

    Private Sub btnLocationHolz_Click(sender As Object, e As EventArgs) Handles btnLocationHolz.Click
        MyLat = 47.878503874990805
        MyLong = 11.691537333035741
    End Sub

    Private Function GetObjectPosition(ByVal Moment As DateTime, ByVal CurrentLocation As Ato.AstroCalc.sLatLong, ByVal ObjectCoord As Ato.AstroCalc.sRADec) As Ato.AstroCalc.sAzAlt

        'Use dynamically ASCOM calls

        Dim Util As New COMInterop.COMObj("ASCOM.Utilities.Util")
        Dim Trans As New COMInterop.COMObj("ASCOM.Astrometry.Transform.Transform")

        'Dim UTCDate As Object = Util.Get("UTCDate")
        Dim Julian As Object = Util.Call("DateUTCToJulian", New Object() {Moment})

        Trans.Set("JulianDateUTC", Julian)
        Trans.Set("SiteLatitude", CurrentLocation.Latitude)
        Trans.Set("SiteLongitude", CurrentLocation.Longitude)
        Trans.Set("SiteElevation", CDbl(1700))
        Trans.Call("SetApparent", New Object() {ObjectCoord.RA, ObjectCoord.DEC})

        Dim RetVal As New Ato.AstroCalc.sAzAlt
        RetVal.ALT = CDbl(Trans.Get("ElevationTopocentric"))
        RetVal.AZ = CDbl(Trans.Get("AzimuthTopocentric"))
        Return RetVal

    End Function

End Class