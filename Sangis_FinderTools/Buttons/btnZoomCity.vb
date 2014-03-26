Imports Sangis_FinderTools.Globals
Imports System.Windows.Forms

Public Class btnZoomCity
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()

        My.ArcMap.Application.CurrentTool = Nothing
        'Check that the jur municipal is in the dataframe and make it active
        If Not ActivateLayerFrame("SANGIS.JUR_MUNICIPAL") Then
            Exit Sub
        End If
        Dim frmCitySrch As Form
        frmCitySrch = New frmSearchCity
        frmCitySrch.Show()
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

End Class
