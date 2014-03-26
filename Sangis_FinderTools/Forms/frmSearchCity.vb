Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry
Imports Sangis_FinderTools.Globals

Public Class frmSearchCity

#Region "Primaries"

    Private Sub frmSearchCity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'load the city jurisdiction list
        'With cboCityList
        '    For Each vJur In Split(sJurList, ",")
        '        .Items.Add(vJur)
        '    Next vJur
        'End With

        For index = 0 To sJurList.GetUpperBound(0)
            cboCityList.Items.Add(sJurList(index))
        Next
    End Sub

#End Region

#Region "Buttons"

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Cbos and Lbxs"

    Private Sub cboCityList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCityList.SelectedIndexChanged
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        Try
            'if the info isn't cleared
            If cboCityList.Text <> "" Then
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
                If pFeatureLayer Is Nothing Then
                    MsgBox("SANGIS.JUR_MUNICIPAL is not in current data frame, tool will close")
                    Me.Close()
                End If
                pGeoDataset = pFeatureLayer
                pQFilter = New QueryFilter
                pQFilter.WhereClause = "CODE = '" & cboCityList.Text & "'"
                pFeatCursor = pFeatureLayer.Search(pQFilter, False)
                pFeature = pFeatCursor.NextFeature
                If pFeature Is Nothing Then
                    MsgBox("Unable to find City: " & cboCityList.Text, vbCritical, "Zoom to City")
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
            End If
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

#End Region

#Region "Text Boxes"

#End Region

#Region "Custom Subs and Functions"

#End Region

End Class