Option Explicit On
Option Strict On

Public Class frmImageModifier

    Public FormToModify As frmImage

    Private Sub frmImageModifier_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        DisplayCurrentProps()
    End Sub

    Private Sub pgMain_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles pgMain.PropertyValueChanged
        'Special handling for certain property changes that need additional calculation
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
            Case "MinCutOff_pct"
                'ImageFromData.MinCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(ImageFromData.MinCutOff_pct)
            Case "MaxCutOff_pct"
                'ImageFromData.MaxCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(ImageFromData.MaxCutOff_pct)
        End Select
        'Update the property and display the (recalculated) image
        DisplayCurrentProps()
        FormToModify.DisplayImageData()
    End Sub

    Private Sub DisplayCurrentProps()
        pgMain.SelectedObject = FormToModify.ImageFromData
    End Sub

End Class