Imports Sangis_FinderTools.Globals
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Framework

Public Class btnZoomCounty
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        Try
            My.ArcMap.Application.CurrentTool = Nothing
            'make sure the county is in the dataframe and make it active
            If Not ActivateLayerFrame("SANGIS.JUR_COUNTY") Then
                Exit Sub
            End If

            Dim pActiveView As IActiveView
            pActiveView = My.ArcMap.Document.FocusMap

            'Reset the extent of the Focus Map to the county boundary
            LoopThroughLayersAndZoomToExtent(pActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.JUR_COUNTY")

            'Refresh view and TOC
            pActiveView.Refresh()
            My.ArcMap.Document.UpdateContents()

            'Set map to save
            Dim pDocDirty As IDocumentDirty
            pDocDirty = My.ArcMap.Document
            pDocDirty.SetDirty()

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub


End Class
