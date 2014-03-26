Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports Sangis_FinderTools.Globals
Public Class btnDeleteGraphics
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        Try
            My.ArcMap.Application.CurrentTool = Nothing
            Dim pAV As IActiveView
            pAV = My.ArcMap.Document.FocusMap
            'Delete the graphics in the active map
            DeleteGraphicsRefreshActiveView(TryCast(pAV, IActiveView))
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

    Public Sub DeleteGraphicsRefreshActiveView(ByVal activeView As IActiveView)
        Dim graphicsContainer As IGraphicsContainer = activeView.GraphicsContainer
        graphicsContainer.DeleteAllElements()
        activeView.Refresh()
    End Sub

End Class
