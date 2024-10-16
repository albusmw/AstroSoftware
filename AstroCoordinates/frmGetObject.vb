Option Explicit On
Option Strict On

Public Class frmGetObject

    Public SelectedObject As sObjectInfo = Nothing
    Public CurrentLocation As Ato.AstroCalc.sLatLong = Nothing
    Public ObjectCoord As Ato.AstroCalc.sRADec = Nothing

    Public InView As frmInView

    'Local coordinates
    Private MyLocation As String = "LOCATION UNKNOWN"
    Private MyLat As Double = Double.NaN
    Private MyLong As Double = Double.NaN
    Private MyUTCOffset As Integer = 0

    Private TimeCalc As cTimeZoneCalc

    '''<summary>All present objects loaded from catalogs.</summary>
    Dim Objects As New List(Of sObjectInfo)

    Dim FoundEntries As New List(Of sObjectInfo)

    Private ReadOnly MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    Public ReadOnly Property CurrentUTC() As DateTime
        Get
            Return DateTime.UtcNow
        End Get
    End Property

    Public ReadOnly Property LocationTime() As DateTimeOffset
        Get
            Return New DateTimeOffset(CurrentUTC.Year, CurrentUTC.Month, CurrentUTC.Day, CurrentUTC.Hour, CurrentUTC.Minute, CurrentUTC.Second, New TimeSpan(CInt(MyUTCOffset), 0, 0))
        End Get
    End Property

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
            NewObjectOrLocationSelected()
        End If
        tsslSelectionLength.Text = FoundEntries.Count.ToString.Trim & " entries filtered"
    End Sub

    Private Sub lbResults_DoubleClick(sender As Object, e As EventArgs) Handles lbResults.DoubleClick
        SelectedObject = FoundEntries.Item(lbResults.SelectedIndex)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub tUpdateDetails_Tick(sender As Object, e As EventArgs) Handles tUpdateDetails.Tick
        UpdateObjectCurrentInfo()
    End Sub

    '''<summary>New object or location selected.</summary>
    Private Sub NewObjectOrLocationSelected()
        SelectedObject = FoundEntries.Item(lbResults.SelectedIndex)
        CurrentLocation = New Ato.AstroCalc.sLatLong(MyLat, MyLong)
        ObjectCoord = New Ato.AstroCalc.sRADec(SelectedObject.RA, SelectedObject.Dec)
        UpdateObjectCurrentInfo()
        'Show in InView display
        UpdateInView()
    End Sub

    Private Sub UpdateObjectCurrentInfo()
        Dim LeftWidth As Integer = 18
        If lbResults.SelectedIndex = -1 Then
            tbDetails.Text = "No object selected ..."
            Exit Sub
        End If
        Try
            'Display selected object information
            Dim TimeCalc As New cTimeZoneCalc("W. Europe Standard Time", "America/Santiago")
            Dim ObjectHourAngle As Double = Double.NaN
            Dim Pos As Ato.AstroCalc.sAzAlt = GetObjectPosition_ASCOM(CurrentUTC, CurrentLocation, ObjectCoord)
            Dim Details As New List(Of String)
            Details.Add(SelectedObject.VerboseName & " - Catalog: " & SelectedObject.Catalog)
            Details.Add("═══════════════════════════════════════════════════════")
            Details.Add(" mag".PadLeft(LeftWidth) & ": " & SelectedObject.Mag.ValRegIndep)
            If SelectedObject.Diameter > 0 Then Details.Add(" diameter".PadLeft(LeftWidth) & ": " & SelectedObject.Diameter.ValRegIndep & " '")
            If SelectedObject.HIP > 0 Then Details.Add(" HIP".PadLeft(LeftWidth) & ": " & SelectedObject.HIP.ToString.Trim)
            If SelectedObject.HD > 0 Then Details.Add(" HD".PadLeft(LeftWidth) & ": " & SelectedObject.HD.ToString.Trim)
            Details.Add("   RA".PadLeft(LeftWidth) & ": " & SelectedObject.RA.ToHMS)
            Details.Add("   Dec".PadLeft(LeftWidth) & ": " & SelectedObject.Dec.ToDegMinSec)
            Details.Add("   Alt".PadLeft(LeftWidth) & ":  " & Pos.Alt.ToDegMinSec)
            Details.Add("   Az".PadLeft(LeftWidth) & ":  " & Pos.AZ.ToDegMinSec)
            Details.Add("   Hour angle".PadLeft(LeftWidth) & ":  " & (ObjectHourAngle * (24 / 360)).ToHMS)
            Details.Add("═══════════════════════════════════════════════════════")
            Details.Add("UTC time".PadRight(LeftWidth) & ": " & TimeCalc.UTCNowString)
            Details.Add("Observatory".PadRight(LeftWidth) & ": " & MyLocation)
            Details.Add("    Time".PadRight(LeftWidth) & ": " & TimeCalc.ObservatoryString)
            Details.Add("    Siderial Time".PadRight(LeftWidth) & ": " & Ato.AstroCalc.LST(CurrentUTC, MyLong).ValRegIndep)
            Details.Add("    Siderial Time".PadRight(LeftWidth) & ": " & Ato.AstroCalc.LSTFormated(CurrentUTC, MyLong))
            Details.Add("    Latitude".PadRight(LeftWidth) & ": " & MyLat.ToDegMinSec)
            Details.Add("    Longitude".PadRight(LeftWidth) & ": " & MyLong.ToDegMinSec)
            tbDetails.Text = Join(Details.ToArray, System.Environment.NewLine)
        Catch ex As Exception
            tbDetails.Text = "Calculation error: <" & ex.Message & ">"
        End Try
    End Sub

    Private Sub btnLocationDSC_Click(sender As Object, e As EventArgs) Handles btnLocationDSC.Click
        MyLocation = "Deep Sky Chile"
        MyLat = Ato.AstroCalc.KnownLocations.DSC.Latitude
        MyLong = Ato.AstroCalc.KnownLocations.DSC.Longitude
        MyUTCOffset = Ato.AstroCalc.KnownLocations.DSC.UTCOffset
        UpdateInViewLocation()
    End Sub

    Private Sub btnLocationHolz_Click(sender As Object, e As EventArgs) Handles btnLocationHolz.Click
        MyLocation = "Holzkirchen"
        MyLat = Ato.AstroCalc.KnownLocations.Holzkirchen.Latitude
        MyLong = Ato.AstroCalc.KnownLocations.Holzkirchen.Longitude
        MyUTCOffset = Ato.AstroCalc.KnownLocations.Holzkirchen.UTCOffset
        UpdateInViewLocation()
    End Sub

    Private Function GetObjectPosition_ASCOM(ByVal Moment As DateTime, ByVal CurrentLocation As Ato.AstroCalc.sLatLong, ByVal ObjectCoord As Ato.AstroCalc.sRADec) As Ato.AstroCalc.sAzAlt

        'Use dynamically ASCOM calls
        Dim Util As New COMInterop.COMObj("ASCOM.Utilities.Util")
        Dim Trans As New COMInterop.COMObj("ASCOM.Astrometry.Transform.Transform")

        'Dim UTCDate As Object = Util.Get("UTCDate")
        Dim Julian As Object = Util.Call("DateUTCToJulian", Moment)

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
        NewObjectOrLocationSelected()
    End Sub

    Private Sub tsmiData_AstroBin_Click(sender As Object, e As EventArgs) Handles tsmiData_AstroBin.Click
        'Parse the data from the coordinates given on the AstroBin object page
        Dim ClipContent As String() = Split(Clipboard.GetText, System.Environment.NewLine)
        Dim RA As Double = Double.NaN
        Dim Dec As Double = Double.NaN
        For Each Line As String In ClipContent
            If Line.Trim.StartsWith("RA center:") Then
                RA = Line.Trim.Replace("RA center:", String.Empty).ParseRA
            End If
            If Line.Trim.StartsWith("DEC center:") Then
                Dec = Line.Trim.Replace("DEC center:", String.Empty).ParseDegree
            End If
        Next Line
    End Sub

    Private Sub tsmiTools_InViewDisplay_Click(sender As Object, e As EventArgs) Handles tsmiTools_InViewDisplay.Click
        If IsNothing(InView) Then
            InView = New frmInView
            InView.Show()
        End If
        UpdateInViewLocation()
        UpdateInView()
    End Sub

    '''<summary>Update the InView location.</summary>
    Private Sub UpdateInViewLocation()
        If IsNothing(InView) = False Then
            InView.Props.ObservatoryLocationName = MyLocation
            InView.Props.ObservatoryLatitude = MyLat
            InView.Props.ObservatoryLongitude = MyLong
            InView.Props.ObservatoryUTCOffset = MyUTCOffset
            InView.Recalc()
        End If
    End Sub

    '''<summary>Update location and object in the InView display window.</summary>
    Private Sub UpdateInView()
        If IsNothing(InView) = False Then
            Dim UpdateRequired As Boolean = False
            If InView.Props.ObjectName <> SelectedObject.Name Then
                InView.Props.ObjectName = SelectedObject.Name
                UpdateRequired = True
            End If
            If InView.Props.RightAscension <> SelectedObject.RA.ToHMS Then
                InView.Props.RightAscension = SelectedObject.RA.ToHMS
                UpdateRequired = True
            End If
            If InView.Props.Declination <> SelectedObject.Dec.ToDegMinSec Then
                InView.Props.Declination = SelectedObject.Dec.ToDegMinSec
                UpdateRequired = True
            End If
            If UpdateRequired Then InView.Recalc()
        End If
    End Sub

End Class