Imports System
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports Sangis_FinderTools.Globals
Imports ESRI.ArcGIS.Framework
Imports System.Windows.Forms

Public Class btnResetView
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        Try
            My.ArcMap.Application.CurrentTool = Nothing
            'check if Jur County Layer exists and activate dataframe or don't load form
            If Not ActivateLayerFrame("SANGIS.JUR_COUNTY") Then
                Exit Sub
            End If
            Dim pActiveView As IActiveView
            pActiveView = My.ArcMap.Document.FocusMap
            'Set visible layers
            LoopThroughLayersAndReSetVisibility(pActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}")
            'Clear all selected features
            pActiveView.FocusMap.ClearSelection()

            'Delete all graphics added to the view (e.g. rectangles, circles, etc.)
            pActiveView.GraphicsContainer.DeleteAllElements()

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
        'Enabled = CheckActiveFrame("Finder Layers")
    End Sub

    Private Sub MapEvents_ContentsChanged()
        'Enabled = CheckActiveFrame("Finder Layers")
    End Sub

    Public Sub LoopThroughLayersAndReSetVisibility(ByVal map As IMap, ByVal layerCLSID As String)

        If map Is Nothing OrElse layerCLSID Is Nothing Then
            Return
        End If

        Dim uid As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass
        uid.Value = layerCLSID ' Example: "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer

        Try
            Dim pDataset As IDataset
            Dim enumLayer As IEnumLayer = map.Layers((CType(uid, ESRI.ArcGIS.esriSystem.UID)), True) ' Explicit Cast
            enumLayer.Reset()
            Dim layer As ILayer = enumLayer.Next
            Do While Not (layer Is Nothing)
                ' TODO - Implement your code here...
                If layer.Valid Then
                    pDataset = layer
                    Select Case pDataset.BrowseName
                        Case "SANGIS.JUR_COUNTY", "SANGIS.JUR_MUNICIPAL", "SANGIS.JUR_VICINITY", _
                             "SANGIS.ROADS_FREEWAY", "SANGIS.ROADS_HIGHWAY", "SANGIS.HYD_LAKE"
                            layer.Visible = True
                        Case Else
                            layer.Visible = False
                    End Select
                End If
                layer = enumLayer.Next()
            Loop
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Sub

End Class
