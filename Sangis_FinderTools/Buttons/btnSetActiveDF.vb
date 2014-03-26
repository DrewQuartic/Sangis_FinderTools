Imports Sangis_FinderTools.Globals
Public Class btnSetActiveDF
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        My.ArcMap.Application.CurrentTool = Nothing
        'Check if the parcel layer exists, if not then do not open form
        If Not ActivateLayerFrame("PARCELS") Then
            Exit Sub
        End If
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

End Class
