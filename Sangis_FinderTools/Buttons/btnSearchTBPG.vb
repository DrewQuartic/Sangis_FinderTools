Imports Sangis_FinderTools.Globals
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto

Public Class btnSearchTBPG

    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()

        My.ArcMap.Application.CurrentTool = Nothing
        'Check if the thomas brothers grid exists, if not, do not open dialog 
        If Not ActivateLayerFrame("SANGIS.GRID_PAGE_TB") Then
            Exit Sub
        End If

        Dim sPage As String
        Dim MxDoc As IMxDocument
        Dim pActiveView As IActiveView
        Dim pFLayerTB As IFeatureLayer
        Dim pFeatSelTB As IFeatureSelection
        Dim pQFilterTB As IQueryFilter
        Dim pFeature As IFeature
        Dim pFeatCursor As IFeatureCursor
        Dim pEnv As IEnvelope
        Dim pDocDirty As IDocumentDirty

        'Prompt the user for the page number and check that it's numeric
        sPage = ""
        Do While sPage = ""
            sPage = InputBox("Enter page number (953-1354)", "Thomas Brothers Search")
            If sPage = "" Then Exit Sub
            If Not IsNumeric(sPage) Then
                MsgBox("'" & sPage & "' is not numeric", vbExclamation, _
                       "Thomas Brothers Search Error")
                sPage = ""
            ElseIf CInt(sPage) < 953 Or CInt(sPage) > 1354 Then
                MsgBox(sPage & " is not between 953 and 1354", vbExclamation, _
                       "Thomas Brothers Search Error")
                sPage = ""
            End If
        Loop

        'Get the layer and feature selection
        MxDoc = My.ArcMap.Document
        pActiveView = MxDoc.FocusMap
        pFLayerTB = LoopThroughLayersAndGetFL(pActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.GRID_PAGE_TB")
        If pFLayerTB.Visible = False Then
            pFLayerTB.Visible = True
            MxDoc.UpdateContents()
        End If

        'Setup the query and find the feature by page number
        pQFilterTB = New QueryFilter
        pQFilterTB.SubFields = "*"
        pQFilterTB.WhereClause = "PAGE LIKE '" & sPage & "%'"
        pFeatSelTB = pFLayerTB
        pFeatSelTB.SelectFeatures(pQFilterTB, esriSelectionResultEnum.esriSelectionResultNew, False)
        pFeatCursor = pFLayerTB.Search(pQFilterTB, False)

        'If no page was found, stop now
        pFeature = pFeatCursor.NextFeature
        If pFeature Is Nothing Then
            MsgBox("Page " & sPage & " not found", vbExclamation, _
                   "Thomas Brother Search Error")
            pActiveView.Refresh()
            Exit Sub
        End If

        'Get the extent of the selected feature(s)
        pEnv = New Envelope
        Do Until pFeature Is Nothing
            pEnv.Union(pFeature.Extent)
            pFeature = pFeatCursor.NextFeature
        Loop
        'if its empty then bad features, throw message
        If pEnv.IsEmpty Then
            MsgBox("Unable to zoom to the Thomas Brothers page", vbExclamation, _
                   "Thomas Brothers Search Error")
            pActiveView.Refresh()
            Exit Sub
        End If

        'Zoom to the new extent
        pEnv.Expand(1.1, 1.1, True)
        pActiveView.Extent = pEnv
        pActiveView.Refresh()

        pDocDirty = My.ArcMap.Document
        pDocDirty.SetDirty()
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub
End Class
