Option Explicit On
Option Strict On

Public Class frmGetObject

    Public SelectedObject As cObjectInfo = Nothing

    '''<summary>Display to do calculation and show full properties.</summary>
    Private InView As New frmInView
    '''<summary>ZEDGraph plotter.</summary>
    Private Plotter As cZEDGraph

    Private TimeCalc As cTimeZoneCalc

    '''<summary>All present objects loaded from catalogs.</summary>
    Dim Objects As New List(Of cObjectInfo)

    Dim FoundEntries As New List(Of cObjectInfo)

    Private ReadOnly MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    Public ReadOnly Property CurrentUTC() As DateTime
        Get
            Return DateTime.UtcNow
        End Get
    End Property

    Public ReadOnly Property LocationTime() As DateTimeOffset
        Get
            Return New DateTimeOffset(CurrentUTC.Year, CurrentUTC.Month, CurrentUTC.Day, CurrentUTC.Hour, CurrentUTC.Minute, CurrentUTC.Second, New TimeSpan(CInt(InView.Props.Observatory_UTCOffset), 0, 0))
        End Get
    End Property

    Private Sub frmGetObject_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Plotter = New cZEDGraph(zgcMain)

        'Load catalogs copied from PixInsight
        Dim DoubleEntries As Integer = LoadCatalogs()

        'Display info on loaded objects
        tsslLoaded.Text = Objects.Count.ToString.Trim & " objects loaded, " & DoubleEntries.ToString.Trim & " double enties"

        'Set default location
        InView.Props.SetObservatory(Ato.AstroCalc.KnownLocations.DSC)
        UpdateInView()

    End Sub

    Private Sub ApplySearchString(sender As Object, e As EventArgs) Handles tbSearchString.TextChanged, cbCustomOnly.CheckStateChanged
        Dim SearchString As String = tbSearchString.Text.Trim.ToUpper
        Dim Cat_Custom As String = ComponentModelEx.EnumDesciptionConverter.GetEnumDescription(eCatMode.Custom)
        Dim Cat_MyOwn As String = ComponentModelEx.EnumDesciptionConverter.GetEnumDescription(eCatMode.MyOwn)
        FoundEntries.Clear()
        For Each Entry As cObjectInfo In Objects
            Dim Found As Boolean = False
            'Search name
            If (Found = False) And Entry.Name.ToUpper.Contains(SearchString) Then Found = True
            'Search alias
            If (Found = False) And Entry.AliasName.ToUpper.Contains(SearchString) Then Found = True
            'Search HD
            If (Found = False) And Entry.HD.ToString.Trim.Contains(SearchString) Then Found = True
            'Search HIP
            If (Found = False) And Entry.HIP.ToString.Trim.Contains(SearchString) Then Found = True
            'Remove if not custom and only custom is checked
            If (Found = True) And (cbCustomOnly.Checked) Then
                If (Entry.Catalog = Cat_Custom) Or (Entry.Catalog = Cat_MyOwn) Then
                    Found = True
                Else
                    Found = False
                End If
            End If
            'Add if found
            If Found Then
                FoundEntries.Add(Entry)
            End If
        Next Entry
        lbResults.Items.Clear()
        Dim NewlbResultsContent As New List(Of String)
        For Each Item As cObjectInfo In FoundEntries
            NewlbResultsContent.Add(Item.VerboseName)
        Next Item
        lbResults.Items.AddRange(NewlbResultsContent.ToArray)
        'If there is only 1 entry, auto-select it and update the calculation
        If lbResults.Items.Count = 1 Then lbResults.SelectedIndex = 0
        tsslSelectionLength.Text = FoundEntries.Count.ToString.Trim & " entries filtered"
    End Sub

    Private Sub lbResults_DoubleClick(sender As Object, e As EventArgs) Handles lbResults.DoubleClick
        AcceptAndClose()
    End Sub

    Private Sub tUpdateDetails_Tick(sender As Object, e As EventArgs) Handles tUpdateDetails.Tick
        UpdateObjectCurrentInfo()
    End Sub

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
        InView.Show()
        UpdateInViewLocation()
        UpdateInView()
    End Sub

    Private Sub tsmiData_Accept_Click(sender As Object, e As EventArgs) Handles tsmiData_Accept.Click
        AcceptAndClose()
    End Sub

    Private Sub tsmiData_SetLocation_Click(sender As Object, e As EventArgs) Handles tsmiData_SetLocation.Click
        Dim LocationSelector As New frmSelectFromList({Ato.AstroCalc.KnownLocations.DSC.Name, Ato.AstroCalc.KnownLocations.Holzkirchen.Name}, {InView.Props.Observatory_Name})
        If LocationSelector.ShowDialog <> DialogResult.OK Then Exit Sub
        InView.Props.Observatory_Name = LocationSelector.Selected.First
        Select Case InView.Props.Observatory_Name
            Case "Deep Sky Chile"
                InView.Props.SetObservatory(Ato.AstroCalc.KnownLocations.DSC)
            Case "Holzkirchen"
                InView.Props.SetObservatory(Ato.AstroCalc.KnownLocations.Holzkirchen)
        End Select
        UpdateInViewLocation()
    End Sub

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
    ' Functions
    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    Private Function LoadCatalogs() As Integer

        Dim DoubleEntries As Integer = 0
        DoubleEntries += AddToCat(eCatMode.M, GetResourceLines("AstroCoordinates.messier.txt"))
        DoubleEntries += AddToCat(eCatMode.Stars, GetResourceLines("AstroCoordinates.namedStars.txt"))
        DoubleEntries += AddToCat(eCatMode.NGC, GetResourceLines("AstroCoordinates.ngc2000.txt"))
        DoubleEntries += AddToCat(eCatMode.MyOwn, GetResourceLines("AstroCoordinates.MyOwn.txt"), ";")

        'Load custom objects
        Dim CustomCat As String = System.IO.Path.Combine(MyPath, "CustomCatalog.txt")
        If System.IO.File.Exists(CustomCat) = True Then
            DoubleEntries += AddToCat(eCatMode.Custom, System.IO.File.ReadAllLines(CustomCat))
        End If

        Return DoubleEntries

    End Function

    '''<summary>Load the PixInsight catalog data (tab separated).</summary>
    '''<param name="FileName">File to load.</param>
    '''<returns>Number of double entries.</returns>
    Private Function AddToCat(ByVal Mode As eCatMode, ByRef FileContent As String()) As Integer
        Return AddToCat(Mode, FileContent, vbTab)
    End Function

    '''<summary>Load the PixInsight catalog data (tab separated).</summary>
    '''<param name="FileName">File to load.</param>
    '''<returns>Number of double entries.</returns>
    Private Function AddToCat(ByVal Mode As eCatMode, ByRef FileContent As String(), ByVal Splitter As String) As Integer
        Dim RetVal As Integer = 0
        For Idx As Integer = 1 To FileContent.GetUpperBound(0)
            Dim FileLine As String = FileContent(Idx).Trim
            If FileLine.Length > 0 Then
                Dim Splitted As String() = Split(FileLine, Splitter)
                Dim Key As String = Splitted(0)
                'Currently no check for double entries
                Objects.Add(New cObjectInfo(Mode, Splitted))
            End If
        Next Idx
        Return RetVal
    End Function

    Private Function GetResourceLines(ByVal Name As String) As String()
        Return Split((New System.IO.StreamReader(System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream(Name))).ReadToEnd, vbLf)
    End Function

    '''<summary>New object or location selected.</summary>
    Private Sub NewObjectOrLocationSelected()
        SelectedObject = FoundEntries.Item(lbResults.SelectedIndex)
        UpdateObjectCurrentInfo()
        UpdateInView()
    End Sub

    Private Sub UpdateObjectCurrentInfo()
        Dim LeftWidth As Integer = 18
        If IsNothing(SelectedObject) = True Then
            tbDetails.Text = "No object selected ..."
            Exit Sub
        Else
            Try
                'Display selected object information
                Dim TimeCalc As New cTimeZoneCalc("W. Europe Standard Time", "America/Santiago")
                Dim ObjectHourAngle As Double = Double.NaN
                Dim CurrentLocation As New Ato.AstroCalc.sLatLong(InView.Props.Observatory_Latitude, InView.Props.Observatory_Longitude)
                Dim Pos As Ato.AstroCalc.sAzAlt = GetObjectPosition_ASCOM(CurrentUTC, CurrentLocation, New Ato.AstroCalc.sRADec(SelectedObject.RA, SelectedObject.Dec))
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
                Details.Add("Observatory".PadRight(LeftWidth) & ": " & InView.Props.Observatory_Name)
                Details.Add("    Time".PadRight(LeftWidth) & ": " & TimeCalc.ObservatoryString)
                Details.Add("    Siderial Time".PadRight(LeftWidth) & ": " & Ato.AstroCalc.LST(CurrentUTC, InView.Props.Observatory_Longitude).ValRegIndep)
                Details.Add("    Siderial Time".PadRight(LeftWidth) & ": " & Ato.AstroCalc.LSTFormated(CurrentUTC, InView.Props.Observatory_Longitude))
                Details.Add("    Latitude".PadRight(LeftWidth) & ": " & InView.Props.Observatory_Latitude.ToDegMinSec)
                Details.Add("    Longitude".PadRight(LeftWidth) & ": " & InView.Props.Observatory_Longitude.ToDegMinSec)
                tbDetails.Text = Join(Details.ToArray, System.Environment.NewLine)
            Catch ex As Exception
                tbDetails.Text = "Calculation error: <" & ex.Message & ">"
            End Try
        End If
    End Sub

    '''<summary>Update the InView location.</summary>
    Private Sub UpdateInViewLocation()
        If IsNothing(InView) = False Then
            InView.Recalc()
        End If
    End Sub

    '''<summary>Update location and object in the InView display window and the build-in graph.</summary>
    Private Sub UpdateInView()
        If IsNothing(SelectedObject) Then Exit Sub
        'Update build-in graph
        Dim Props As New cAstroInView.cProps
        With Props
            .ObjectName = SelectedObject.Name
            .RightAscension = SelectedObject.RA.ToHMS
            .Declination = SelectedObject.Dec.ToDegMinSec
        End With

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
            If UpdateRequired Then
                InView.Recalc()
                InView.PlotTraces(Plotter)
            End If
        End If

    End Sub

    Private Sub AcceptAndClose()
        SelectedObject = FoundEntries.Item(lbResults.SelectedIndex)
        Me.DialogResult = DialogResult.OK
        Me.Close()
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
        RetVal.Alt = CDbl(Trans.Get("ElevationTopocentric"))
        RetVal.AZ = CDbl(Trans.Get("AzimuthTopocentric"))
        Return RetVal

    End Function

End Class