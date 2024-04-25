Option Explicit On
Option Strict On

Public Class frmGetObject

    Public SelectedObject As sObjectInfo

    Public Enum eCatMode
        M
        NGC
        Stars
        Custom
    End Enum

    'Local coordinates
    Private MyLocation As String = "LOCATION UNKNOWN"
    Private MyLat As Double = Double.NaN
    Private MyLong As Double = Double.NaN

    Public Structure sObjectInfo
        Public Catalog As String
        Public Name As String
        Public RA As Double
        Public Dec As Double
        Public Diameter As Double
        Public Mag As Double
        Public HD As UInt32
        Public HIP As UInt32
        Public AliasName As String
        Public Sub Init()
            Catalog = String.Empty
            Name = String.Empty
            RA = Double.NaN
            Dec = Double.NaN
            Diameter = Double.NaN
            Mag = Double.NaN
            HD = 0
            HIP = 0
            AliasName = String.Empty
        End Sub
        Public Sub New(ByVal Mode As eCatMode, ByVal Content As String())
            Init()
            'These parameters are always the same ...
            Name = Content(0).Trim
            RA = AstroParser.ParseRA(Content(1))
            Dec = AstroParser.ParseDeclination(Content(2))
            'Depending on the catalog there are additional parameters
            Select Case Mode
                Case eCatMode.M
                    'Messier
                    Catalog = "Messier"
                    Diameter = Content(3).ValRegIndep
                    Mag = Content(4).ValRegIndep
                    If Content.GetUpperBound(0) >= 5 Then AliasName = Content(Content.GetUpperBound(0)).Trim
                Case eCatMode.NGC
                    'NGC
                    Catalog = "NGC2000"
                    Diameter = Content(3).ValRegIndep
                    Mag = Content(4).ValRegIndep
                    If Content.GetUpperBound(0) >= 5 Then AliasName = Content(Content.GetUpperBound(0)).Trim
                Case eCatMode.Stars
                    'Stars
                    Catalog = "Stars"
                    Diameter = Content(3).ValRegIndep
                    Mag = Content(4).ValRegIndep
                    If Content.GetUpperBound(0) >= 5 Then UInt32.TryParse(Content(5).Trim, HD)
                    If Content.GetUpperBound(0) >= 6 Then UInt32.TryParse(Content(6).Trim, HIP)
                    If Content.GetUpperBound(0) >= 7 Then AliasName = Content(Content.GetUpperBound(0)).Trim
                Case eCatMode.Custom
                    'Custom catalog
                    Catalog = "Custom"
                    If Content.GetUpperBound(0) >= 3 Then AliasName = Content(Content.GetUpperBound(0)).Trim
            End Select
        End Sub
        Public ReadOnly Property VerboseName() As String
            Get
                Dim RetVal As String = Name
                If String.IsNullOrEmpty(AliasName) = False Then RetVal &= " (" & AliasName & ")"
                Return RetVal
            End Get
        End Property
    End Structure

    Dim MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Dim Objects As New List(Of sObjectInfo)
    Dim FoundEntries As New List(Of sObjectInfo)

    Private Sub frmGetObject_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Load catalogs copied from PixInsight
        Dim DoubleEntries As Integer = 0
        DoubleEntries += AddToCat(eCatMode.M, GetResourceLines("AstroCoordinates.messier.txt"))
        DoubleEntries += AddToCat(eCatMode.Stars, GetResourceLines("AstroCoordinates.namedStars.txt"))
        DoubleEntries += AddToCat(eCatMode.NGC, GetResourceLines("AstroCoordinates.ngc2000.txt"))

        'Load custom objects
        Dim CustomCat As String = System.IO.Path.Combine(MyPath, "CustomCatalog.txt")
        If System.IO.File.Exists(CustomCat) = True Then
            DoubleEntries += AddToCat(eCatMode.Custom, System.IO.File.ReadAllLines(CustomCat))
        End If

        'Display info on loaded objects
        tsslLoaded.Text = Objects.Count.ToString.Trim & " objects loaded, " & DoubleEntries.ToString.Trim & " double enties"

        'Set location
        btnLocationDSC_Click(Nothing, Nothing)

    End Sub

    Private Function GetResourceLines(ByVal Name As String) As String()
        Return Split((New System.IO.StreamReader(System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream(Name))).ReadToEnd, vbLf)
    End Function

    '''<summary>Load the PixInsight catalog data (tab separated).</summary>
    '''<param name="FileName">File to load.</param>
    '''<returns>Number of double entries.</returns>
    Private Function AddToCat(ByVal Mode As eCatMode, ByRef FileContent As String()) As Integer
        Dim RetVal As Integer = 0
        For Idx As Integer = 1 To FileContent.GetUpperBound(0)
            Dim FileLine As String = FileContent(Idx).Trim
            If FileLine.Length > 0 Then
                Dim Splitted As String() = Split(FileLine, vbTab)
                Dim Key As String = Splitted(0)
                'Currently no check for double entries
                Objects.Add(New sObjectInfo(Mode, Splitted))
            End If
        Next Idx
        Return RetVal
    End Function

    Private Sub tbSearchString_TextChanged(sender As Object, e As EventArgs) Handles tbSearchString.TextChanged
        Dim SearchString As String = tbSearchString.Text.Trim.ToUpper
        FoundEntries.Clear()
        For Each Entry As sObjectInfo In Objects
            Dim Found As Boolean = False
            'Search name
            If (Found = False) And Entry.Name.ToUpper.Contains(SearchString) Then
                FoundEntries.Add(Entry) : Found = True
            End If
            'Search alias
            If (Found = False) And Entry.AliasName.ToUpper.Contains(SearchString) Then
                FoundEntries.Add(Entry) : Found = True
            End If
            'Search HD
            If (Found = False) And Entry.HD.ToString.Trim.Contains(SearchString) Then
                FoundEntries.Add(Entry) : Found = True
            End If
            'Search HIP
            If (Found = False) And Entry.HIP.ToString.Trim.Contains(SearchString) Then
                FoundEntries.Add(Entry) : Found = True
            End If
        Next Entry
        lbResults.Items.Clear()
        Dim NewlbResultsContent As New List(Of String)
        For Each Item As sObjectInfo In FoundEntries
            NewlbResultsContent.Add(Item.VerboseName)
        Next Item
        lbResults.Items.AddRange(NewlbResultsContent.ToArray)
        If lbResults.Items.Count = 1 Then
            lbResults.SelectedIndex = 0
            DisplaySelectedObject()
        End If
        tsslSelectionLength.Text = FoundEntries.Count.ToString.Trim & " entries filtered"
    End Sub

    Private Sub lbResults_DoubleClick(sender As Object, e As EventArgs) Handles lbResults.DoubleClick
        SelectedObject = FoundEntries.Item(lbResults.SelectedIndex)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub tUpdateDetails_Tick(sender As Object, e As EventArgs) Handles tUpdateDetails.Tick
        DisplaySelectedObject()
    End Sub

    Private Sub DisplaySelectedObject()
        Dim LeftWidth As Integer = 20
        Try
            'Display selected object information
            SelectedObject = FoundEntries.Item(lbResults.SelectedIndex)
            Dim CurrentUTC As DateTime = DateTime.UtcNow
            Dim CurrentLocation As New Ato.AstroCalc.sLatLong(MyLat, MyLong)
            Dim ObjectCoord As New Ato.AstroCalc.sRADec(SelectedObject.RA, SelectedObject.Dec)
            Dim ObjectHourAngle As Double = Double.NaN
            Dim Pos As Ato.AstroCalc.sAzAlt = GetObjectPosition(CurrentUTC, CurrentLocation, ObjectCoord)
            Dim Details As New List(Of String)
            Details.Add(SelectedObject.VerboseName & " - Catalog: " & SelectedObject.Catalog)
            Details.Add("═══════════════════════════════════════════════════════")
            Details.Add(" mag".PadLeft(LeftWidth) & ": " & SelectedObject.Mag.ValRegIndep)
            If SelectedObject.Diameter > 0 Then Details.Add(" diameter".PadLeft(LeftWidth) & ": " & SelectedObject.Diameter.ValRegIndep & " '")
            If SelectedObject.HIP > 0 Then Details.Add(" HIP".PadLeft(LeftWidth) & ": " & SelectedObject.HIP.ToString.Trim)
            If SelectedObject.HD > 0 Then Details.Add(" HD".PadLeft(LeftWidth) & ": " & SelectedObject.HD.ToString.Trim)
            Details.Add("   RA".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.FormatHMS(SelectedObject.RA))
            Details.Add("   Dec".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.Format360Degree(SelectedObject.Dec))
            Details.Add("   Alt".PadLeft(LeftWidth) & ":  " & Ato.AstroCalc.Format360Degree(Pos.ALT))
            Details.Add("   Az".PadLeft(LeftWidth) & ":  " & Ato.AstroCalc.Format360Degree(Pos.AZ))
            Details.Add("   Hour angle".PadLeft(LeftWidth) & ":  " & Ato.AstroCalc.FormatHMS(ObjectHourAngle * (24 / 360)))
            Details.Add("═══════════════════════════════════════════════════════")
            Details.Add(" UTC time".PadLeft(LeftWidth) & ": " & Format(CurrentUTC, "HH:mm:ss zzz"))
            Details.Add(" Location".PadLeft(LeftWidth) & ": " & MyLocation)
            Details.Add(" Local Siderial Time".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.LST(CurrentUTC, MyLong).ValRegIndep)
            Details.Add(" Local Siderial Time".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.LSTFormated(CurrentUTC, MyLong))
            Details.Add(" Location latitude".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.Format360Degree(MyLat))
            Details.Add(" Location longitude".PadLeft(LeftWidth) & ": " & Ato.AstroCalc.Format360Degree(MyLong))
            tbDetails.Text = Join(Details.ToArray, System.Environment.NewLine)
        Catch ex As Exception
            tbDetails.Text = String.Empty
        End Try
    End Sub

    Private Sub btnLocationDSC_Click(sender As Object, e As EventArgs) Handles btnLocationDSC.Click
        MyLocation = "Deep Sky Chile"
        MyLat = -(30 + (31 / 60) + (34.7 / 3600))       'according to Overview.md
        MyLong = -(70 + (51 / 60) + (11.8 / 3600))      'according to Overview.md
    End Sub

    Private Sub btnLocationHolz_Click(sender As Object, e As EventArgs) Handles btnLocationHolz.Click
        MyLocation = "Holzkirchen"
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

    Private Sub lbResults_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbResults.SelectedIndexChanged
        DisplaySelectedObject()
    End Sub

End Class