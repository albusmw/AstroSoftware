Option Explicit On
Option Strict On
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ListView
Imports AstroCoordinates.Astronomy.Net
Imports AstroCoordinates.Ato
Imports DocumentFormat.OpenXml.Office2013.Drawing.Chart

Public Class frmGetObject

    Private Class Cats
        Public Const Cat3 As String = "3.) New visible filter calculation"
    End Class

    Public Class cVisFilter

        <ComponentModel.DisplayName("1.1) Filter based on visibility")>
        Public Property RemoveUnavailable As Boolean = False

        <ComponentModel.DisplayName("1.2) Minimum hours visible")>
        Public Property MinVisibleHours As Double = 4.0

        <ComponentModel.DisplayName("2.1) Minimum heigth")>
        Public Property MinHeight As Double = 20.0

        <ComponentModel.DisplayName("2.2) Maximum sun heigth")>
        Public Property MaxSunHeigth As Double = -18.0

        <ComponentModel.Category(Cats.Cat3)>
        <ComponentModel.DisplayName("1.) Minimum culmination heigth")>
        Public Property Vis_MinCulmHeigth As Double = 30

    End Class
    Public VisFilter As New cVisFilter

    '''<summary>Different columns in the list.</summary>
    Public Class cColumns
        Public Const FullName As String = "Full name"
        Public Const Catalog As String = "Catalog"
        Public Const Item As String = "Item"
        Public Const ObjSize As String = "Size arcsec"
        Public Const CulmTime As String = "Culm Time"
        Public Const CulmHeight As String = "Culm Height"
        Public Const ObjLowest As String = "Obj lowest"
        Public Const CulmVisible As String = "Visible"
    End Class

    '''<summary>My executable folder.</summary>
    Private ReadOnly MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    Public CurrentLocation As Astronomy.Net.sLatLong

    '''<summary>All present objects loaded from catalogs.</summary>
    Public AllObjects As New List(Of cObjectInfo)

    '''<summary>The current selected object.</summary>
    Public SelectedObject As cObjectInfo = Nothing

    '''<summary>Table objects.</summary>
    Public Objects As New Data.DataTable

    Public ObjectAvailability As New Dictionary(Of Tuple(Of Integer, Integer), Double)

    '''<summary>Display to do calculation and show full properties.</summary>
    Private InView As New frmInView
    '''<summary>ZEDGraph plotter.</summary>
    Private Plotter As cZEDGraph

    Private TimeCalc As cTimeZoneCalc

    '''<summary>Catalog where custom entries are located.</summary>
    Public Property CustomCat As String = System.IO.Path.Combine(MyPath, "CustomCatalog.txt")

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

        CurrentLocation = Ato.AstroCalc.KnownLocations.DSC

        'Configure object data source
        Objects.Columns.Add(cColumns.FullName, GetType(String))
        Objects.Columns.Add(cColumns.Catalog, GetType(String))
        Objects.Columns.Add(cColumns.Item, GetType(String))
        Objects.Columns.Add(cColumns.ObjSize, GetType(String))
        Objects.Columns.Add(cColumns.CulmTime, GetType(DateTime))
        Objects.Columns.Add(cColumns.CulmHeight, GetType(Double))
        Objects.Columns.Add(cColumns.ObjLowest, GetType(Double))
        Objects.Columns.Add(cColumns.CulmVisible, GetType(String))

        'Set default location and calculate the common parameters
        Dim InViewCalc As New cAstroInView
        Dim Result As New cAstroInView.cVectors
        InView.Props.SetObservatory(CurrentLocation)
        InViewCalc.CalculateVectors(InView.Props, Result)

        'Configure GUI
        Plotter = New cZEDGraph(zgcMain)
        pgFilter.SelectedObject = VisFilter

        'Configure data grid
        ConfigDataGridView(adgvObjects)
        'adgvObjects.SetFilterChecklistEnabled(adgvObjects.Columns(cColumns.FullName), False)       'does not work ...
        'adgvObjects.SetFilterChecklistNodesMax(adgvObjects.Columns(cColumns.FullName), 0)          'does not work ...

        'Load catalogs copied from PixInsight
        Dim DoubleEntries As Integer = LoadCatalogs()

        'Display objects
        adgvObjects.DataSource = Nothing
        Dim StartMomentUTC As DateTime = Now.ToUniversalTime
        Objects.Clear()
        For Each Item In AllObjects
            Dim ObjectRaDec As New sRADec(Item.RA, Item.Dec)
            Dim CulmTime As DateTime = Astronomy.Net.GetNextObjectCulminationTime(StartMomentUTC, CurrentLocation, ObjectRaDec)
            Dim CulmHeigth As Double = Astronomy.Net.GetObjectPosition(CulmTime, CurrentLocation, ObjectRaDec).Alt
            Dim ObjLowest As Double = Astronomy.Net.GetObjectPosition(CulmTime.AddHours(12), CurrentLocation, ObjectRaDec).Alt
            Dim Visible As Boolean = True
            If CulmHeigth < VisFilter.Vis_MinCulmHeigth Then Visible = False                    'object culmination height must be "significant"
            Dim VisibleText As String = CStr(IIf(Visible = True, "YES", "NO"))
            Objects.Rows.Add(New Object() {Item.FullName(True), Item.Catalog, Item.Name, Item.Diameter.Describe, CulmTime, Math.Round(CulmHeigth, 1), Math.Round(ObjLowest, 1), VisibleText})
        Next Item
        adgvObjects.DataSource = Objects

        'Display info on loaded objects
        tsslLoaded.Text = AllObjects.Count.ToString.Trim & " objects loaded, " & DoubleEntries.ToString.Trim & " double enties"

        'Enter a search string if there is one
        If IsNothing(Me.Tag) = False Then tbSearchString.Text = CStr(Me.Tag)

        UpdateInView()

    End Sub

    Private Sub tbSearchString_TextChanged(sender As Object, e As EventArgs) Handles tbSearchString.TextChanged
        'Set a custom filter for the full name search, this unfortunatly also disables all other filters
        If tbSearchString.Text.Trim.Length = 0 Then
            adgvObjects.CleanFilterAndSort()
        Else
            adgvObjects.LoadFilterAndSort("([Full name] LIKE '%" & tbSearchString.Text & "%')", adgvObjects.SortString)
        End If
    End Sub

    Private Function AllPartsAreIn(ByVal EntryToCheck As String, ByVal SearchString As String) As Boolean
        For Each SearchPart As String In SearchString.Split(" "c)
            If EntryToCheck.ToUpper.Contains(SearchPart.ToUpper) = False Then Return False
        Next SearchPart
        Return True
    End Function

    Private Sub tUpdateDetails_Tick(sender As Object, e As EventArgs) Handles tUpdateDetails.Tick
        UpdateObjectCurrentInfo()
    End Sub

    Private Sub lbResults_SelectedIndexChanged(sender As Object, e As EventArgs) Handles adgvObjects.SelectionChanged
        NewObjectOrLocationSelected()
    End Sub

    Private Sub tsmiData_AstroBin_Click(sender As Object, e As EventArgs) Handles tsmiData_AstroBin.Click
        'Parse the data from the coordinates given on the AstroBin object page
        Dim ClipContent As String() = Split(Clipboard.GetText, System.Environment.NewLine)
        Dim ClipRA As Double = Double.NaN
        Dim ClipDec As Double = Double.NaN
        For Each Line As String In ClipContent
            If Line.Trim.StartsWith("RA center:") Then
                ClipRA = Line.Trim.Replace("RA center:", String.Empty).ParseRA
            End If
            If Line.Trim.StartsWith("DEC center:") Then
                ClipDec = Line.Trim.Replace("DEC center:", String.Empty).ParseDegree
            End If
        Next Line
    End Sub

    Private Sub tsmiTools_InViewDisplay_Click(sender As Object, e As EventArgs) Handles tsmiTools_InViewDisplay.Click
        InView.Props.Calc_Sun = True
        InView.Show()
        UpdateInViewLocation()
        UpdateInView()
    End Sub

    Private Sub tsmiData_Accept_Click(sender As Object, e As EventArgs) Handles tsmiData_Accept.Click
        AcceptAndClose()
    End Sub

    Private Sub tsmiData_SetLocation_Click(sender As Object, e As EventArgs) Handles tsmiData_SetLocation.Click
        Dim LocationSelector As New frmSelectFromList({Ato.AstroCalc.KnownLocations.DSC.Name, Ato.AstroCalc.KnownLocations.Holzkirchen.Name, Ato.AstroCalc.KnownLocations.LaPalma.Name}, {InView.Props.Observatory_Name})
        If LocationSelector.ShowDialog <> DialogResult.OK Then Exit Sub
        InView.Props.Observatory_Name = LocationSelector.Selected.First
        Select Case InView.Props.Observatory_Name
            Case Ato.AstroCalc.KnownLocations.DSC.Name
                InView.Props.SetObservatory(Ato.AstroCalc.KnownLocations.DSC)
            Case Ato.AstroCalc.KnownLocations.Holzkirchen.Name
                InView.Props.SetObservatory(Ato.AstroCalc.KnownLocations.Holzkirchen)
            Case Ato.AstroCalc.KnownLocations.LaPalma.Name
                InView.Props.SetObservatory(Ato.AstroCalc.KnownLocations.LaPalma)
        End Select
        UpdateInViewLocation()
    End Sub

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
    ' Functions
    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    Private Function LoadCatalogs() As Integer

        Dim DoubleEntries As Integer = 0
        Dim Sep As Char = Chr(9)

        'Load all build-in catalogs
        DoubleEntries += AddToCat(eSpecialCat.M, GetResourceLines("AstroCoordinates.messier.txt"), Sep)
        DoubleEntries += AddToCat(eSpecialCat.Stars, GetResourceLines("AstroCoordinates.namedStars.txt"), Sep)
        DoubleEntries += AddToCat(eSpecialCat.NGC, GetResourceLines("AstroCoordinates.ngc2000.txt"), Sep)

        'Load all VizierR catalogs
        Dim Vizier As New cVizier("C:\!AstroCat")
        Vizier.InspectCatalog(False)
        Dim CatData As Dictionary(Of String, List(Of cObjectInfo)) = Vizier.GetCatData()
        For Each Catalog As String In CatData.Keys
            AllObjects.AddRange(CatData(Catalog))
        Next Catalog

        'Load custom objects
        Sep = "|"c
        If System.IO.File.Exists(CustomCat) = True Then
            DoubleEntries += AddToCat(eSpecialCat.USER, System.IO.File.ReadAllLines(CustomCat), Sep)
        End If

        Return DoubleEntries

    End Function

    '''<summary>Load the PixInsight catalog data (tab separated).</summary>
    '''<param name="Mode">Which catalog to use.</param>
    '''<param name="FileContent">Content of the file.</param>
    '''<param name="Splitter">Split character.</param>
    '''<returns>Number of double entries.</returns>
    Private Function AddToCat(ByVal Mode As eSpecialCat, ByRef FileContent As String(), ByVal Splitter As Char) As Integer
        Dim RetVal As Integer = 0
        For Idx As Integer = 1 To FileContent.GetUpperBound(0)
            Dim FileLine As String = FileContent(Idx).Trim
            If FileLine.Length > 0 Then
                Dim Splitted As String() = FileLine.Split(Splitter)
                Dim Key As String = Splitted(0)
                'Currently no check for double entries
                AllObjects.Add(New cObjectInfo(Mode, Splitted))
            End If
        Next Idx
        Return RetVal
    End Function

    Private Function GetResourceLines(ByVal Name As String) As String()
        Return Split((New System.IO.StreamReader(System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream(Name))).ReadToEnd, vbLf)
    End Function

    '''<summary>Get objects that match the search criterias (name / conditions).</summary>
    'Private Sub GetMatchingObjects()

    '    Dim SearchString = tbSearchString.Text.Trim.ToUpper
    '    Dim Cat_Own = ComponentModelEx.EnumDesciptionConverter.GetEnumDescription(eCatMode.Own)

    '    MatchingEntries.Clear()
    '    For Each Entry In AllObjects

    '        'Search all name fields
    '        Dim ObjectFound = False
    '        If AllPartsAreIn(Entry.FullName(True), SearchString) Then ObjectFound = True
    '        If AllPartsAreIn(Entry.AliasName, SearchString) Then ObjectFound = True
    '        If AllPartsAreIn(Entry.HD.ToString.Trim, SearchString) Then ObjectFound = True
    '        If AllPartsAreIn(Entry.HIP.ToString.Trim, SearchString) Then ObjectFound = True

    '        'Decide if object should be displayed
    '        Dim DisplayItem = False
    '        If ObjectFound Then
    '            If Entry.Catalog = Cat_Own Then
    '                DisplayItem = True                          'always display own catalog items
    '            Else
    '                If VisFilter.RemoveUnavailable = False Then
    '                    DisplayItem = True                      'do not judge availability
    '                Else
    '                    Dim TuppleToSearch As New Tuple(Of Integer, Integer)(CInt(Entry.RA), CInt(Entry.Dec))
    '                    If ObjectAvailability.ContainsKey(TuppleToSearch) = False Then
    '                        DisplayItem = True                  'availability not judgable -> default is show
    '                    Else
    '                        Dim VisibleHours = ObjectAvailability(TuppleToSearch)
    '                        If VisibleHours >= VisFilter.MinVisibleHours Then
    '                            DisplayItem = True
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If

    '        'Add if found
    '        If DisplayItem Then MatchingEntries.Add(Entry)

    '    Next Entry

    '    'Display objects
    '    Objects.Clear()
    '    For Each Item In MatchingEntries
    '        Objects.Rows.Add(New Object() {Item.FullName(True), Item.Catalog, Item.Name})
    '    Next Item

    '    'If there is only 1 entry, auto-select it and update the calculation
    '    'If adgvObjects.DisplayedRowCount(True) = 1 Then adgvObjects.Rows.GetFirstRow = 0

    '    'Display summary
    '    tsslSelectionLength.Text = MatchingEntries.Count.ToString.Trim & " matching entries (" & (AllObjects.Count - MatchingEntries.Count).ToString.Trim & " entries removed)"

    'End Sub

    '''<summary>New object or location selected.</summary>
    Private Sub NewObjectOrLocationSelected()
        Dim X As cObjectInfo = GetSelectedObject()
        If IsNothing(X) = False Then SelectedObject = X
        UpdateObjectCurrentInfo()
        UpdateInView()
    End Sub

    Private Sub UpdateObjectCurrentInfo()
        Dim LeftWidth As Integer = 18
        Dim Sep As String = "══════════════════════════════════════════════"
        If IsNothing(SelectedObject) = True Then
            tbDetails.Text = "No object selected ..."
            Exit Sub
        Else
            Try
                'Display selected object information
                Dim W2nd As Integer = 15
                Dim ObservatoryTZ As GeoTimeZone.TimeZoneResult = GeoTimeZone.TimeZoneLookup.GetTimeZone(InView.Props.Observatory_Latitude, InView.Props.Observatory_Longitude)
                Dim TZI As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(ObservatoryTZ.Result)
                Dim TimeCalc As New cTimeZoneCalc("W. Europe Standard Time", TZI.Id)
                Dim Now_DateOnly As New DateOnly(Now.Year, Now.Month, Now.Day)
                Dim JD As Double = Astronomy.Net.JulianDate(Now)
                Dim ObjectHourAngle As Double = Double.NaN
                Dim CurrentLocation As New Astronomy.Net.sLatLong(InView.Props.Observatory_Latitude, InView.Props.Observatory_Longitude)
                Dim CurrentObject As New Astronomy.Net.sRADec(SelectedObject.RA, SelectedObject.Dec)
                Dim Pos As Astronomy.Net.sAzAlt = Astronomy.Net.GetObjectPosition(CurrentUTC, CurrentLocation, CurrentObject)
                'Dim Pos_ASCOM As Astronomy.Net.sAzAlt = GetObjectPosition_ASCOM(CurrentUTC, CurrentLocation, CurrentObject)
                Dim CulmTime As DateTime = Astronomy.Net.GetNextObjectCulminationTime(DateTime.UtcNow, CurrentLocation, CurrentObject)
                Dim Vis As Astronomy.Net.sVisibility = Astronomy.Net.GetRaiseAndSet(Now_DateOnly, CurrentLocation, CurrentObject)
                Dim Details As New List(Of String)
                Details.Add(SelectedObject.FullName(True) & " - Catalog: " & SelectedObject.Catalog)
                Details.Add(Sep)
                AddEntry(Details, "   mag", SelectedObject.Mag.ValRegIndep)
                AddEntry(Details, "   RA", SelectedObject.RA.ToHMS.PadLeft(W2nd, " "c) & " = " & SelectedObject.RA.ValRegIndep("00.00000") & " = " & (SelectedObject.RA * (180 / 12)).ValRegIndep("00.00000") & "°")
                AddEntry(Details, "   Dec", SelectedObject.Dec.ToDegMinSec.PadLeft(W2nd, " "c) & " = " & SelectedObject.Dec.ValRegIndep("00.00000") & "°")
                AddEntry(Details, "   Alt", Pos.Alt.ToDegMinSec.PadLeft(W2nd, " "c) & " = " & Pos.Alt.ValRegIndep("00.00000") & "°")
                AddEntry(Details, "   Az", Pos.AZ.ToDegMinSec.PadLeft(W2nd, " "c) & " = " & Pos.AZ.ValRegIndep("00.00000") & "°")
                AddEntry(Details, "   Size", SelectedObject.Diameter.Describe.PadLeft(W2nd, " "c))
                AddEntry(Details, "   Hour angle", (ObjectHourAngle * (24 / 360)).ToHMS.PadLeft(W2nd, " "c))
                AddEntry(Details, "   Raise Time", Vis.RaiseTime.ToString("HH:mm:ss").PadLeft(W2nd, " "c))
                AddEntry(Details, "   Culmination", CulmTime.ToString("HH:mm:ss").PadLeft(W2nd, " "c))
                AddEntry(Details, "   Set Time", Vis.SetTime.ToString("HH:mm:ss").PadLeft(W2nd, " "c))
                If SelectedObject.Diameter.IsEmpty = False Then AddEntry(Details, "   Size", SelectedObject.Diameter.Width.ValRegIndep & "x" & SelectedObject.Diameter.Height.ValRegIndep & " '")
                If SelectedObject.HIP > 0 Then AddEntry(Details, "   HIP", SelectedObject.HIP.ToString.Trim)
                If SelectedObject.HD > 0 Then AddEntry(Details, "   HD", SelectedObject.HD.ToString.Trim)
                Details.Add(Sep)
                AddEntry(Details, "UTC time", TimeCalc.UTCNowString)
                AddEntry(Details, "JD", JD.ValRegIndep)
                AddEntry(Details, "JDT", Astronomy.Net.JulianDateTime(Now).ValRegIndep)
                'AddEntry(Details, "GMST", Astronomy.Net.GMST(TimeCalc.UTCNow))
                Details.Add("Observatory".PadRight(LeftWidth) & ": " & InView.Props.Observatory_Name)
                AddEntry(Details, "    Time", TimeCalc.ObservatoryString)
                AddEntry(Details, "    Siderial Time", Astronomy.Net.LST(CurrentUTC, InView.Props.Observatory_Longitude).ValRegIndep & " = " & Astronomy.Net.LSTFormated(CurrentUTC, InView.Props.Observatory_Longitude))
                AddEntry(Details, "    Latitude", InView.Props.Observatory_Latitude.ToDegMinSec.PadLeft(14, " "c) & " = " & InView.Props.Observatory_Latitude.ValRegIndep("00.00000"))
                AddEntry(Details, "    Longitude", InView.Props.Observatory_Longitude.ToDegMinSec.PadLeft(14, " "c) & " = " & InView.Props.Observatory_Longitude.ValRegIndep("00.00000"))
                tbDetails.Text = Join(Details.ToArray, System.Environment.NewLine)
            Catch ex As Exception
                tbDetails.Text = "Calculation error: <" & ex.Message & ">"
            End Try
            'Display observability results
            Dim TuppleToSearch As New Tuple(Of Integer, Integer)(CInt(SelectedObject.RA), CInt(SelectedObject.Dec))
            Try

                tsslObsCalcResult.Text = TuppleToSearch.Item1.ToString.Trim & ":" & TuppleToSearch.Item2.ToString.Trim & " -> " & ObjectAvailability(TuppleToSearch).ToString.Trim & " hours visible"
            Catch ex As Exception
                tsslObsCalcResult.Text = TuppleToSearch.Item1.ToString.Trim & ":" & TuppleToSearch.Item2.ToString.Trim & " -> ??? (entry not found)"
            End Try
        End If
    End Sub

    Private Sub AddEntry(ByRef L As List(Of String), ByVal Header As String, ByVal Value As String)
        Dim LeftWidth As Integer = 18
        L.Add(Header.PadRight(LeftWidth) & ": " & Value)
    End Sub

    '''<summary>Update the InView location.</summary>
    Private Sub UpdateInViewLocation()
        If IsNothing(InView) = False Then
            InView.Recalc()
        End If
    End Sub

    '''<summary>Update location and object in the InView display window and the build-in graph.</summary>
    Private Sub UpdateInView()
        UpdateInView(False)
    End Sub

    '''<summary>Update location and object in the InView display window and the build-in graph.</summary>
    Private Sub UpdateInView(ByVal ForceUpdate As Boolean)
        If IsNothing(SelectedObject) Then Exit Sub
        'Update build-in graph
        Dim Props As New cAstroInView.cProps
        With Props
            .ObjectName = SelectedObject.FullName(True)
            .RightAscension = SelectedObject.RA.ToHMS
            .Declination = SelectedObject.Dec.ToDegMinSec
        End With
        If IsNothing(InView) = False Then
            Dim UpdateRequired As Boolean = ForceUpdate
            If ForceUpdate = True Then UpdateRequired = True
            If InView.Props.ObjectName <> Props.ObjectName Then
                InView.Props.ObjectName = Props.ObjectName
                UpdateRequired = True
            End If
            If InView.Props.RightAscension <> Props.RightAscension Then
                InView.Props.RightAscension = Props.RightAscension
                UpdateRequired = True
            End If
            If InView.Props.Declination <> Props.Declination Then
                InView.Props.Declination = Props.Declination
                UpdateRequired = True
            End If
            If UpdateRequired Then
                InView.Recalc()
                InView.PlotTraces(Plotter)
            End If
        End If

    End Sub

    Private Sub AcceptAndClose()
        SelectedObject = GetSelectedObject()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Function GetObjectPosition_ASCOM(ByVal Moment As DateTime, ByVal CurrentLocation As Astronomy.Net.sLatLong, ByVal ObjectCoord As Astronomy.Net.sRADec) As Astronomy.Net.sAzAlt
        Return GetObjectPosition_ASCOM(Moment, CurrentLocation, 0, ObjectCoord)
    End Function

    Private Function GetObjectPosition_ASCOM(ByVal Moment As DateTime, ByVal CurrentLocation As Astronomy.Net.sLatLong, ByVal SiteElevation As Double, ByVal ObjectCoord As Astronomy.Net.sRADec) As Astronomy.Net.sAzAlt

        'Use dynamically ASCOM calls
        Dim Util As New COMInterop.COMObj("ASCOM.Utilities.Util")
        Dim Trans As New COMInterop.COMObj("ASCOM.Astrometry.Transform.Transform")

        'Dim UTCDate As Object = Util.Get("UTCDate")
        Dim Julian As Object = Util.Call("DateUTCToJulian", Moment)

        Trans.Set("JulianDateUTC", Julian)
        Trans.Set("SiteLatitude", CurrentLocation.Latitude)
        Trans.Set("SiteLongitude", CurrentLocation.Longitude)
        Trans.Set("SiteElevation", SiteElevation)
        Trans.Call("SetApparent", New Object() {ObjectCoord.RA, ObjectCoord.DEC})
        Dim RetVal As New Astronomy.Net.sAzAlt
        RetVal.Alt = CDbl(Trans.Get("ElevationTopocentric"))
        RetVal.AZ = CDbl(Trans.Get("AzimuthTopocentric"))
        Return RetVal

    End Function

    Private Sub tsmiTools_Recalc_Click(sender As Object, e As EventArgs) Handles tsmiTools_Recalc.Click
        UpdateInView(True)
    End Sub

    Private Sub tsmiTools_GetBestObjectsOLD_Click(sender As Object, e As EventArgs) Handles tsmiTools_GetBestObjectsOLD.Click

        Dim InViewProps As cAstroInView.cProps = InView.Props
        Dim InViewCalc As New cAstroInView

        'Calculate which objects are best observable for the selected time range and location
        Dim CommonUTCTimes() As Date = Array.Empty(Of Date)()
        Dim CommonJDs() As Double = Array.Empty(Of Double)()
        Dim CommonLSTs() As Double = Array.Empty(Of Double)()
        Dim CommonHAs() As Double = Array.Empty(Of Double)()
        Dim CommonMoonAltitude() As Double = Array.Empty(Of Double)()
        Dim CommonSunAltitude() As Double = Array.Empty(Of Double)()

        Dim TestObject As cObjectInfo = Nothing
        Dim Result As New cAstroInView.cVectors
        Dim Limits As New cAstroInView.cPlotConfig

        Limits.Limit_MoonMaxHeigth = 100
        Limits.Limit_ObjectMinHeigth = VisFilter.MinHeight
        Limits.Limit_SunMaxHeigth = VisFilter.MaxSunHeigth

        'Store settings
        Dim Old_Calc_Moon As Boolean = InViewProps.Calc_Moon
        Dim Old_Calc_Sun As Boolean = InViewProps.Calc_Sun
        Dim Old_ObjectName As String = InViewProps.ObjectName

        'Calculate sun and moon only for 1st run because they stay the same
        InViewProps.TimeVector_Stepping = New TimeSpan(0, 1, 0)             '1 minute stepping
        InViewProps.Calc_Moon = True
        InViewProps.Calc_Sun = True
        InViewProps.ObjectName = "Visibility test"

        ObjectAvailability = New Dictionary(Of Tuple(Of Integer, Integer), Double)
        tspgMain.Maximum = (25 * 180) + 1
        tspgMain.Value = 0

        Dim FirstRun As Boolean = True
        For RA As Integer = 0 To 24                                     'rounding can result in 24 ...

            Dim FirstRARun As Boolean = True
            For Dec As Integer = -90 To 90

                If tspgMain.Value < tspgMain.Maximum Then
                    tspgMain.Value += 1 : De()
                End If

                'Set the object properties
                InViewProps.RightAscension = CDbl(RA).ToHMS
                InViewProps.Declination = CDbl(Dec).ToDegMinSec
                Dim ObsTuple As New Tuple(Of Integer, Integer)(RA, Dec)

                'Copy sun and moon vectors if not calculated
                If FirstRun = False Then
                    Result.UTCTimes = CommonUTCTimes.CreateCopy
                    Result.JD = CommonJDs.CreateCopy
                    Result.LST = CommonLSTs.CreateCopy
                    Result.Sun_Altitude = CommonSunAltitude.CreateCopy
                    Result.Moon_Alt = CommonMoonAltitude.CreateCopy
                End If

                'RA is only called on first RA loop run
                If FirstRARun = False Then
                    Result.HA = CommonHAs.CreateCopy
                Else
                    Result.HA = Array.Empty(Of Double)()
                End If

                'Run calculation for given RS and DEC
                InViewCalc.CalculateVectors(InViewProps, Result)

                'Get calculated static from the 1st run
                If FirstRun Then
                    CommonUTCTimes = Result.UTCTimes.CreateCopy
                    CommonJDs = Result.JD.CreateCopy
                    CommonLSTs = Result.LST.CreateCopy
                    CommonMoonAltitude = Result.Moon_Alt.CreateCopy
                    CommonSunAltitude = Result.Sun_Altitude.CreateCopy
                End If

                'Store HA from the 1st run of RA
                If FirstRARun Then CommonHAs = Result.HA.CreateCopy

                'Copy sun and moon vectors in any case to have it for the CalcObservable function
                Result.Sun_Altitude = CommonSunAltitude.CreateCopy
                Result.Moon_Alt = CommonMoonAltitude.CreateCopy

                'Calculate the observable vectors
                Dim Observable() As Double = InViewCalc.CalcObservable(Result, Limits)

                'Judge the observability - length of positiv criteria
                Dim FirstVisible As DateTime = DateTime.MinValue
                Dim LastVisible As DateTime = DateTime.MaxValue

                'Move over observable vector and get first and last observable value
                For Idx As Integer = 0 To Observable.GetUpperBound(0)
                    If Double.IsNaN(Observable(Idx)) = False Then
                        If FirstVisible = DateTime.MinValue Then FirstVisible = Result.UTCTimes(Idx)
                        LastVisible = Result.UTCTimes(Idx)
                    End If
                Next Idx

                'Set length of observability
                If (FirstVisible = DateTime.MinValue) And (LastVisible = DateTime.MaxValue) Then
                    ObjectAvailability.Add(ObsTuple, 0)
                Else
                    ObjectAvailability.Add(ObsTuple, (LastVisible - FirstVisible).TotalHours)
                End If

                'Do not calculate sun and moon any more
                If FirstRun Then
                    InViewProps.Calc_Sun = False
                    InViewProps.Calc_Moon = False
                    FirstRun = False
                End If

                FirstRARun = False

            Next Dec
        Next RA

        'Re-store settings
        InViewProps.Calc_Moon = Old_Calc_Moon
        InViewProps.Calc_Sun = Old_Calc_Sun
        InViewProps.ObjectName = Old_ObjectName

        tspgMain.Value = 0

    End Sub

    '''<summary>Get the selected object from the table.</summary>
    Private Function GetSelectedObject() As cObjectInfo
        If adgvObjects.SelectedRows.Count > 0 Then
            Dim TableSelect As String = CStr(adgvObjects.SelectedRows.Item(0).Cells(cColumns.FullName).Value)
            For Each X As cObjectInfo In AllObjects
                If X.FullName(True) = TableSelect Then
                    Return X
                End If
            Next X
        End If
        Return Nothing
    End Function

    '''<summary>Configure the data grid view in a unique way.</summary>
    Public Shared Sub ConfigDataGridView(ByRef adgv As Zuby.ADGV.AdvancedDataGridView)
        With adgv
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = True
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .RowHeadersVisible = False
            .EnableHeadersVisualStyles = False
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersDefaultCellStyle.SelectionBackColor = .ColumnHeadersDefaultCellStyle.BackColor
            .FilterAndSortEnabled = True
        End With
    End Sub

    Private Sub De()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        AcceptAndClose()
    End Sub

    Private Sub tsmiData_ToClipboard_Click(sender As Object, e As EventArgs) Handles tsmiData_ToClipboard.Click
        Clipboard.Clear()
        Clipboard.SetText(tbDetails.Text)
    End Sub

End Class