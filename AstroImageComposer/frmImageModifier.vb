Option Explicit On
Option Strict On

'''<summary>Windows containinf the image modifier parameters.</summary>
Public Class frmImageModifier

    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
    '''<summary>GUID of the linked image form.</summary>
    Public ReadOnly Property LinkedGUID As String
        Get
            Return MyLinkedGUID
        End Get
    End Property
    Private MyLinkedGUID As String = String.Empty

    Public Sub SetLinkedGUID(ByVal GUIDToUse As String)
        MyLinkedGUID = GUIDToUse
    End Sub
    '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

    '''<summary>The form this modifier is attached to.</summary>
    Public FormToModify As frmImage

    Private Sub frmImageModifier_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        DisplayCurrentProps()
    End Sub

    Private Sub pgMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pgMain.MouseWheel
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
            Case "ColorMap_Gamma"
                FormToModify.ImageFromData.ColorMap_Gamma += FormToModify.ImageFromData.PctStepSize * Math.Sign(e.Delta)
            Case "ColorMap_LowerEnd"
                'Take next filled bin
                Dim Smaller As Boolean = CBool(IIf(e.Delta < 0, True, False))
                Dim NewValue As Double = FindNextElement(FormToModify.ImgStat.MonochromHistogram_Int.Keys, FormToModify.ImageFromData.ColorMap_LowerEnd, Smaller)
                FormToModify.ImageFromData.ColorMap_LowerEnd = NewValue
            Case "ColorMap_UpperEnd"
                'Take next filled bin
                Dim Smaller As Boolean = CBool(IIf(e.Delta < 0, True, False))
                Dim NewValue As Double = FindNextElement(FormToModify.ImgStat.MonochromHistogram_Int.Keys, FormToModify.ImageFromData.ColorMap_UpperEnd, Smaller)
                FormToModify.ImageFromData.ColorMap_UpperEnd = NewValue
        End Select
        ReactOnChangedProperty()
    End Sub

    Private Sub pgMain_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles pgMain.PropertyValueChanged
        'Special handling for certain property changes that need additional calculation
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
            Case "MinCutOff_pct"
                'ImageFromData.MinCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(ImageFromData.MinCutOff_pct)
            Case "MaxCutOff_pct"
                'ImageFromData.MaxCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(ImageFromData.MaxCutOff_pct)
        End Select
        ReactOnChangedProperty()
    End Sub

    Public Sub ReactOnChangedProperty()
        'Update the property and display the (recalculated) image
        DisplayCurrentProps()
        FormToModify.DisplayImageData()
    End Sub

    Private Sub DisplayCurrentProps()
        pgMain.SelectedObject = FormToModify.ImageFromData
    End Sub

    '''<summary>Find the next smaller or larger element in the passed entries list.</summary>
    '''<param name="Entries">List of entries to search in.</param>
    '''<param name="CurrentElement">Current element value.</param>
    '''<param name="Smaller">TRUE to return the next smaller element, FALSE to return the next bigger element.</param>
    '''<returns>Next element or current element if not smaller / larger could be found.</returns>
    Private Function FindNextElement(ByRef Entries As Dictionary(Of Long, ULong).KeyCollection, ByVal CurrentElement As Double, ByVal Smaller As Boolean) As Double
        Dim ListToSearch As New List(Of Long) : ListToSearch.AddRange(Entries)
        ListToSearch.Sort()
        Dim BestElementIdx As Integer = -1
        Dim SmallestError As Double = Double.MaxValue
        For Idx As Integer = 0 To ListToSearch.Count - 1
            Dim CurrentError As Double = Math.Abs((ListToSearch(Idx) - CurrentElement))
            If CurrentError < SmallestError Then
                BestElementIdx = Idx
                SmallestError = CurrentError
            End If
        Next Idx
        If Smaller = True Then
            If BestElementIdx > 0 Then Return ListToSearch(BestElementIdx - 1)
        Else
            If BestElementIdx < ListToSearch.Count - 1 Then Return ListToSearch(BestElementIdx + 1)
        End If
        Return CurrentElement
    End Function

End Class