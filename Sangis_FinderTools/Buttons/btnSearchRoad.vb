Imports System.Windows.Forms
Imports Sangis_FinderTools.Globals
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework

Public Class btnSearchRoad
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        My.ArcMap.Application.CurrentTool = Nothing
        'Check if the parcel layer exists, if not then do not open form
        If Not ActivateLayerFrame("SANGIS.ROADS_ALL") Then
            Exit Sub
        End If
        'Open the dockable window form
        Dim dockWinID As UID = New UIDClass()
        dockWinID.Value = "SanGIS_Sangis_FinderTools_frmSearchRoadMulti"
        Dim dockWindow As IDockableWindow = My.ArcMap.DockableWindowManager.GetDockableWindow(dockWinID)
        dockWindow.Show(Not dockWindow.IsVisible())
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub
End Class
