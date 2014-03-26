Imports Sangis_FinderTools.Globals
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry

Public Class btnZoomSDCity
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        My.ArcMap.Application.CurrentTool = Nothing
        'make sure municipal is in the active data frame and make it active
        If Not ActivateLayerFrame("SANGIS.JUR_MUNICIPAL") Then
            Exit Sub
        End If

        Dim MxDoc As IMxDocument
        Dim pActiveView As IActiveView
        Dim pFeatureLayer As IFeatureLayer
        Dim pGeoDataset As IGeoDataset
        Dim pEnvelope As IEnvelope
        Dim pQFilter As IQueryFilter
        Dim pFeatCursor As IFeatureCursor
        Dim pFeature As IFeature
        Dim pDocDirty As IDocumentDirty

        'Get the extent of the City of San Diego
        MxDoc = My.ArcMap.Document
        pActiveView = MxDoc.FocusMap
        pFeatureLayer = LoopThroughLayersAndGetFL(pActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.JUR_MUNICIPAL")
        pGeoDataset = pFeatureLayer
        pQFilter = New QueryFilter
        pQFilter.WhereClause = "NAME = 'SAN DIEGO'"
        pFeatCursor = pFeatureLayer.Search(pQFilter, False)
        pFeature = pFeatCursor.NextFeature
        If pFeature Is Nothing Then
            MsgBox("Unable to find City of San Diego", vbCritical, "Zoom to City")
            pActiveView.Refresh()
            Exit Sub
        End If
        pEnvelope = pFeature.Extent

        'Reset the extent of the Focus Map
        pEnvelope.Expand(1.1, 1.1, True)
        pActiveView.Extent = pEnvelope
        pActiveView.Refresh()

        pDocDirty = My.ArcMap.Document
        pDocDirty.SetDirty()
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

End Class
