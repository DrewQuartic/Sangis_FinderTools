Imports System.Windows.Forms
Imports Sangis_FinderTools.Globals
Imports ESRI.ArcGIS.Geodatabase

Public Class btnAddSangisPubDataToMap
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        My.ArcMap.Application.CurrentTool = Nothing
        'try to get access to the info table
        Try
            Dim pTable As ITable
            pTable = GetWorkspaceTable("SANGIS.DATA_PUBLICATION_INFO")
            If pTable Is Nothing Then
                MessageBox.Show("Can not access SANGIS.DATA_PUBLICATION_INFO table.  Closing Form")
                Exit Sub
            End If
            'loose the reference as it was just a test
            pTable = Nothing
        Catch ex As Exception
            MessageBox.Show("Can not access SANGIS.DATA_PUBLICATION_INFO table.  Closing Form")
            Exit Sub
        End Try
        'check if parcels exist and activate dataframe or don't load form
        If Not ActivateLayerFrame("PARCELS") Then
            Exit Sub
        End If
        'Load layer form
        Dim frmAddLayers As Form
        frmAddLayers = New frmSDELayers
        frmAddLayers.Show()
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

End Class
