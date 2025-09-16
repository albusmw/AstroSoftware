Option Explicit On
Option Strict On

Imports AstroCoordinates.cAstroInView

Public Class frmGetObject

    Public Property MinVisibleHours As Double = 4

    '''<summary>My executable folder.</summary>
    Private ReadOnly MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    Public SelectedObject As cObjectInfo = Nothing

    Public ObjectAvailability As New Dictionary(Of Tuple(Of Integer, Integer), Double)

    '''<summary>Display to do calculation and show full properties.</summary>
    Private InView As New frmInView
    '''<summary>ZEDGraph plotter.</summary>
    Private Plotter As cZEDGraph

    Private TimeCalc As cTimeZoneCalc

    '''<summary>All present objects loaded from catalogs.</summary>
    Dim AllObjects As New List(Of cObjectInfo)

    Dim FoundEntries As New List(Of cObjectInfo)

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

        Plotter = New cZEDGraph(zgcMain)

        'Load catalogs copied from PixInsight
        Dim DoubleEntries As Integer = LoadCatalogs()

        'Display info on loaded objects
        tsslLoaded.Text = AllObjects.Count.ToString.Trim & " objects loaded, " & DoubleEntries.ToString.Trim & " double enties"

        'Set default location
        InView.Props.SetObservatory(Ato.AstroCalc.KnownLocations.DSC)
        UpdateInView()

    End Sub

    Private Sub ApplySearchString(sender As Object, e As EventArgs) Handles tbSearchString.TextChanged, tsmiOptions_FilterUnavailable.CheckedChanged

        Dim SearchString = tbSearchString.Text.Trim.ToUpper
        Dim Cat_Own = ComponentModelEx.EnumDesciptionConverter.GetEnumDescription(eCatMode.Own)
        FoundEntries.Clear()

        For Each Entry In AllObjects

            'Search all name fields
            Dim ObjectFound As Boolean = False
            If Entry.FullName(True).ToUpper.Contains(SearchString) Then ObjectFound = True
            If Entry.AliasName.ToUpper.Contains(SearchString) Then ObjectFound = True
            If Entry.HD.ToString.Trim.Contains(SearchString) Then ObjectFound = True
            If Entry.HIP.ToString.Trim.Contains(SearchString) Then ObjectFound = True

            'Decide if object should be displayed
            Dim DisplayItem As Boolean = False
            If ObjectFound Then
                If Entry.Catalog = Cat_Own Then
                    DisplayItem = True                          'always display own catalog items
                Else
                    If tsmiOptions_FilterUnavailable.Checked = False Then
                        DisplayItem = True                      'do not judge availability
                    Else
                        Dim TuppleToSearch As New Tuple(Of Integer, Integer)(CInt(Entry.RA), CInt(Entry.Dec))
                        If ObjectAvailability.ContainsKey(TuppleToSearch) = False Then
                            DisplayItem = True                  'availability not judgable -> default is show
                        Else
                            Dim VisibleHours = ObjectAvailability(TuppleToSearch)
                            If VisibleHours >= MinVisibleHours Then
                                DisplayItem = True
                            End If
                        End If
                    End If
                End If
            End If

            'Add if found
            If DisplayItem Then
                FoundEntries.Add(Entry)
            End If
        Next Entry
        lbResults.Items.Clear()
        Dim NewlbResultsContent As New List(Of String)
        For Each Item In FoundEntries
            NewlbResultsContent.Add(Item.FullName(True))
        Next Item
        lbResults.Items.AddRange(NewlbResultsContent.ToArray)
        'If there is only 1 entry, auto-select it and update the calculation
        If lbResults.Items.Count = 1 Then lbResults.SelectedIndex = 0
        tsslSelectionLength.Text = FoundEntries.Count.ToString.Trim & " entries filtered (" & (AllObjects.Count - FoundEntries.Count).ToString.Trim & " entries removed"
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
        DoubleEntries += AddToCat(eCatMode.M, GetResourceLines("AstroCoordinates.messier.txt"), Sep)
        DoubleEntries += AddToCat(eCatMode.Stars, GetResourceLines("AstroCoordinates.namedStars.txt"), Sep)
        DoubleEntries += AddToCat(eCatMode.NGC, GetResourceLines("AstroCoordinates.ngc2000.txt"), Sep)

        'Load all VizierR catalogs
        Dim Vizier As New cVizier("C:\!AstroCat")
        Vizier.InspectCatalog(False)
        Dim CatData As Dictionary(Of String, List(Of cObjectInfo)) = Vizier.GetCatData()
        For Each Catalog As String In CatData.Keys
            For Each Obj As cObjectInfo In CatData(Catalog)
                'Not finished!
                AllObjects.Add(New cObjectInfo(eCatMode.Vizier, Obj.FullName(False), Obj.RA, Obj.Dec))
            Next Obj
        Next Catalog

        'Load custom objects
        Sep = "|"c
        If System.IO.File.Exists(CustomCat) = True Then
            DoubleEntries += AddToCat(eCatMode.Own, System.IO.File.ReadAllLines(CustomCat), Sep)
        End If

        Return DoubleEntries

    End Function

    '''<summary>Load the PixInsight catalog data (tab separated).</summary>
    '''<param name="Mode">Which catalog to use.</param>
    '''<param name="FileContent">Content of the file.</param>
    '''<param name="Splitter">Split character.</param>
    '''<returns>Number of double entries.</returns>
    Private Function AddToCat(ByVal Mode As eCatMode, ByRef FileContent As String(), ByVal Splitter As Char) As Integer
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

    '''<summary>New object or location selected.</summary>
    Private Sub NewObjectOrLocationSelected()
        SelectedObject = FoundEntries.Item(lbResults.SelectedIndex)
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
                Dim ObservatoryTZ As GeoTimeZone.TimeZoneResult = GeoTimeZone.TimeZoneLookup.GetTimeZone(InView.Props.Observatory_Latitude, InView.Props.Observatory_Longitude)
                Dim TZI As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(ObservatoryTZ.Result)
                Dim TimeCalc As New cTimeZoneCalc("W. Europe Standard Time", TZI.Id)
                Dim ObjectHourAngle As Double = Double.NaN
                Dim CurrentLocation As New Ato.AstroCalc.sLatLong(InView.Props.Observatory_Latitude, InView.Props.Observatory_Longitude)
                Dim Pos As Ato.AstroCalc.sAzAlt = GetObjectPosition_ASCOM(CurrentUTC, CurrentLocation, New Ato.AstroCalc.sRADec(SelectedObject.RA, SelectedObject.Dec))
                Dim Details As New List(Of String)
                Details.Add(SelectedObject.FullName(True) & " - Catalog: " & SelectedObject.Catalog)
                Details.Add(Sep)
                Details.Add(" mag".PadLeft(LeftWidth) & ": " & SelectedObject.Mag.ValRegIndep)
                If SelectedObject.Diameter > 0 Then Details.Add(" diameter".PadLeft(LeftWidth) & ": " & SelectedObject.Diameter.ValRegIndep & " '")
                If SelectedObject.HIP > 0 Then Details.Add(" HIP".PadLeft(LeftWidth) & ": " & SelectedObject.HIP.ToString.Trim)
                If SelectedObject.HD > 0 Then Details.Add(" HD".PadLeft(LeftWidth) & ": " & SelectedObject.HD.ToString.Trim)
                Details.Add("   RA".PadLeft(LeftWidth) & ": " & SelectedObject.RA.ToHMS)
                Details.Add("   Dec".PadLeft(LeftWidth) & ": " & SelectedObject.Dec.ToDegMinSec)
                Details.Add("   Alt".PadLeft(LeftWidth) & ":  " & Pos.Alt.ToDegMinSec)
                Details.Add("   Az".PadLeft(LeftWidth) & ":  " & Pos.AZ.ToDegMinSec)
                Details.Add("   Hour angle".PadLeft(LeftWidth) & ":  " & (ObjectHourAngle * (24 / 360)).ToHMS)
                Details.Add(Sep)
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
            'Display observability results
            Dim TuppleToSearch As New Tuple(Of Integer, Integer)(CInt(SelectedObject.RA), CInt(SelectedObject.Dec))
            Try

                tsslObsCalcResult.Text = TuppleToSearch.Item1.ToString.Trim & ":" & TuppleToSearch.Item2.ToString.Trim & " -> " & ObjectAvailability(TuppleToSearch).ToString.Trim & " hours visible"
            Catch ex As Exception
                tsslObsCalcResult.Text = TuppleToSearch.Item1.ToString.Trim & ":" & TuppleToSearch.Item2.ToString.Trim & " -> ??? (entry not found)"
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
        UpdateInView(False)
    End Sub

    '''<summary>Update location and object in the InView display window and the build-in graph.</summary>
    Private Sub UpdateInView(ByVal ForceUpdate As Boolean)
        If IsNothing(SelectedObject) Then Exit Sub
        'Update build-in graph
        Dim Props As New cAstroInView.cProps
        With Props
            .ObjectName = SelectedObject.Name
            .RightAscension = SelectedObject.RA.ToHMS
            .Declination = SelectedObject.Dec.ToDegMinSec
        End With
        If IsNothing(InView) = False Then
            Dim UpdateRequired As Boolean = ForceUpdate
            If ForceUpdate = True Then UpdateRequired = True
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

    Private Sub tsmiTools_Recalc_Click(sender As Object, e As EventArgs) Handles tsmiTools_Recalc.Click
        UpdateInView(True)
    End Sub

    Private Sub tsmiTools_GetBestObjects_Click(sender As Object, e As EventArgs) Handles tsmiTools_GetBestObjects.Click

        'Calculate which objects are best observable for the selected time range and location
        Dim Moon_Alt() As Double = Array.Empty(Of Double)()
        Dim Sun_Altitude() As Double = Array.Empty(Of Double)()

        Dim InViewCalc As New cAstroInView
        Dim TestObject As cObjectInfo = Nothing
        Dim Result As New cAstroInView.cVectors
        Dim Limits As New cPlotConfig

        Limits.Limit_MoonMaxHeigth = 100
        Limits.Limit_ObjectMinHeigth = 20
        Limits.Limit_SunMaxHeigth = -6
        Dim MinVisTime As Double = 4

        'Store settings
        Dim Old_Calc_Moon As Boolean = InView.Props.Calc_Moon
        Dim Old_Calc_Sun As Boolean = InView.Props.Calc_Sun
        Dim Old_ObjectName As String = InView.Props.ObjectName

        'Calculate sun and moon only for 1st run
        InView.Props.Calc_Moon = True
        InView.Props.Calc_Sun = True
        InView.Props.ObjectName = "Visibility test"

        ObjectAvailability = New Dictionary(Of Tuple(Of Integer, Integer), Double)
        tspgMain.Maximum = (25 * 180) + 1
        tspgMain.Value = 0

        For RA As Integer = 0 To 24                                     'rounding can result in 24 ...
            'For RA As Integer = 7 To 9                                 'test range
            For Dec As Integer = -90 To 90
                'For Dec As Integer = -7 To -5                          'test range

                If tspgMain.Value < tspgMain.Maximum Then
                    tspgMain.Value += 1 : De()
                End If

                'Set the object properties
                InView.Props.RightAscension = CDbl(RA).ToHMS
                InView.Props.Declination = CDbl(Dec).ToDegMinSec
                Dim ObsTuple As New Tuple(Of Integer, Integer)(RA, Dec)

                'Copy sun and moon vectors if not calculated
                If InView.Props.Calc_Sun = False Then Result.Sun_Altitude = Sun_Altitude.CreateCopy
                If InView.Props.Calc_Moon = False Then Result.Moon_Alt = Moon_Alt.CreateCopy

                'Run calculation for given RS and DEC
                InViewCalc.CalculateVectors(InView.Props, Result)

                'Get calculated moon and sun positions from the 1st run
                If InView.Props.Calc_Moon = True Then Moon_Alt = Result.Moon_Alt.CreateCopy
                If InView.Props.Calc_Sun = True Then Sun_Altitude = Result.Sun_Altitude.CreateCopy

                'Copy sun and moon vectors in any case to have it for the CalcObservable function
                Result.Sun_Altitude = Sun_Altitude.CreateCopy
                Result.Moon_Alt = Moon_Alt.CreateCopy

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
                    If (LastVisible - FirstVisible).TotalHours > MinVisTime Then
                        ObjectAvailability.Add(ObsTuple, (LastVisible - FirstVisible).TotalHours)
                    End If
                End If

                'Test code
                'If (RA = 8) And (Dec = -6) Then
                '    MsgBox("!!!")
                'End If

                'Do not calculate sun and moon any more
                InView.Props.Calc_Sun = False
                InView.Props.Calc_Moon = False

            Next Dec
        Next RA

        'Re-store settings
        InView.Props.Calc_Moon = Old_Calc_Moon
        InView.Props.Calc_Sun = Old_Calc_Sun
        InView.Props.ObjectName = Old_ObjectName

        tspgMain.Value = 0

    End Sub

    Private Sub De()
        System.Windows.Forms.Application.DoEvents()
    End Sub

End Class