Imports Sangis_FinderTools.Globals
Imports ESRI.ArcGIS.Framework

Public Class btnTurnOffAllLayers
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        My.ArcMap.Application.CurrentTool = Nothing
        'turn off all the layers
        LoopThroughLayersAndTurnOffAll(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}")
        'Set map to save
        Dim pDocDirty As IDocumentDirty
        pDocDirty = My.ArcMap.Document
        pDocDirty.SetDirty()

    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

 

End Class
