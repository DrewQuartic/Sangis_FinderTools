Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports System.Windows.Forms
Imports Sangis_FinderTools.Globals

Public Class frmSDELayers

    Private m_bFormLoaded As Boolean

#Region "Primaries"

    Private Sub frmSDELayers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim pFWS As IFeatureWorkspace
        'Dim pQueryDef As IQueryDef

        'pFWS = GetFeatureWorkSpace()

        'pQueryDef = pFWS.CreateQueryDef

            'Populate the Category ComboBox
            'Category List
            Dim pFldArray(43)
            pFldArray(1) = "Address"
            pFldArray(2) = "Agriculture"
            pFldArray(3) = "Airport"
            pFldArray(4) = "Annotation"
            pFldArray(5) = "Assessor"
            pFldArray(6) = "Business"
            pFldArray(7) = "Census"
            pFldArray(8) = "CIP"
            pFldArray(9) = "Community"
            pFldArray(10) = "Cultural"
            pFldArray(11) = "District"
            pFldArray(12) = "Ecology"
            pFldArray(13) = "Facility"
            pFldArray(14) = "Fiber"
            pFldArray(15) = "Fire"
            pFldArray(16) = "Geology"
            pFldArray(17) = "Grid"
            pFldArray(18) = "Health"
            pFldArray(19) = "Hydrology"
            pFldArray(20) = "Jurisdiction"
            pFldArray(21) = "Law"
            pFldArray(22) = "Layer"
            pFldArray(23) = "Logo"
            pFldArray(24) = "Miscellaneous"
            pFldArray(25) = "Parcel"
            pFldArray(26) = "Park"
            pFldArray(27) = "Place"
            pFldArray(28) = "Public Land"
            pFldArray(29) = "Public Safety"
            pFldArray(30) = "Raster"
            pFldArray(31) = "Reclaimed Water"
            pFldArray(32) = "Road"
            pFldArray(33) = "Seal"
            pFldArray(34) = "Sewer"
            pFldArray(35) = "Slope"
            pFldArray(36) = "StormDrain"
            pFldArray(37) = "Subdivision"
            pFldArray(38) = "Survey"
            pFldArray(39) = "SurveyDoc"
            pFldArray(40) = "Topography"
            pFldArray(41) = "Transit"
            pFldArray(42) = "Water"
            pFldArray(43) = "Zoning"

            cboCategories.Items.Add("ALL")

            Dim fldi As Integer
            For fldi = 1 To 43
                cboCategories.Items.Add(pFldArray(fldi))
            Next
            'Set the ComboBoxes to the first item
            cboCategories.Text = "ALL"

            'Disable the Open button (until the user selects a layer to add)
            btnOpen.Enabled = False

            'Set form loaded to true
            m_bFormLoaded = True
    End Sub

#End Region

#Region "Buttons"

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        Dim LayersSelected As ListView.CheckedListViewItemCollection = lvLayers.CheckedItems
        Dim list As ListViewItem
        For Each list In LayersSelected
            If list.Checked Then
                AddSDELayer(list.SubItems(0).Text)
            End If
        Next
    End Sub

#End Region

#Region "Cbos and Lbxs"

    Private Sub cboCategories_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCategories.SelectedIndexChanged
        FillListBoxes(cboCategories.Text)
    End Sub

    Private Sub cboFeatures_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not m_bFormLoaded Then Exit Sub
        FillListBoxes(cboCategories.Text)
    End Sub


    Private Sub lvLayers_ItemChecked(sender As Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lvLayers.ItemChecked
        'Enable the Open button
        If lvLayers.CheckedItems.Count > 0 Then
            btnOpen.Enabled = True
        Else
            btnOpen.Enabled = False
        End If
    End Sub

#End Region

#Region "Text Boxes"

#End Region

#Region "Custom Subs and Functions"

    Private Sub FillListBoxes(ByVal CategoryName As String)
        Dim pTableSort As ITableSort
        pTableSort = New TableSort
        Dim pQueryFilter As IQueryFilter
        pQueryFilter = New QueryFilter

        Dim pCursor As ICursor
        Dim pRow As IRow
        Dim iIdx As Long
        Dim pTable As ITable
        Dim sWhere As String

        'Clear the current contents of the Layers ListBox
        lvLayers.Clear()
        lvLayers.Columns.Add("LAYER_NAME", 250, HorizontalAlignment.Left)
        lvLayers.Columns.Add("CATEGORY", 200, HorizontalAlignment.Left)

        'Get the layers data
        CategoryName = CategoryName
        sWhere = "LAYER_LOCATION = 'SDE' AND (IS_DELETED <> 'Y' OR IS_DELETED IS NULL) AND (RESTRICTED <> 'Y' OR RESTRICTED IS NULL) AND DATA_TYPE = 'FeatureClass'"
        If CategoryName <> "ALL" Then
            sWhere = sWhere & " AND CATEGORY = '" & CategoryName & "'"
        End If

        'Get the layers data  (ITableSort allows for sorting data
        pTable = GetWorkspaceTable("SANGIS.DATA_PUBLICATION_INFO")
        pQueryFilter.WhereClause = sWhere

        'Fill the ListBox
        pCursor = pTable.Search(pQueryFilter, True)
        pRow = pCursor.NextRow
        Dim str(2) As String
        Dim itm As ListViewItem
        Do While Not pRow Is Nothing
            str(0) = pRow.Value(pRow.Fields.FindField("LAYER_NAME"))
            str(1) = pRow.Value(pRow.Fields.FindField("CATEGORY"))
            itm = New ListViewItem(pRow.Value(pRow.Fields.FindField("LAYER_NAME")).ToString)
            itm.SubItems.Add(pRow.Value(pRow.Fields.FindField("CATEGORY")).ToString)
            itm.ImageIndex = 0
            lvLayers.Items.Add(itm)
            pRow = pCursor.NextRow
            iIdx = iIdx + 1
        Loop
        Exit Sub

    End Sub

    Private Sub AddSDELayer(ByVal LayerName As String)
        Dim pFWS As IFeatureWorkspace
        Dim pFClass As IFeatureClass
        Dim MxDoc As IMxDocument
        Dim pMap As IMap
        Dim pFLayer As IFeatureLayer = New FeatureLayerClass


        'Create and setup the Layer from the FeatureWorkSpace
        pFWS = GetFeatureWorkSpace()
        pFClass = pFWS.OpenFeatureClass("SANGIS." & LayerName)
        ' pFLayer = New FeatureLayer
        With pFLayer
            .FeatureClass = pFClass
            .Name = pFClass.AliasName             'set the display name
            .Visible = False                'turn it off
        End With

        'Setup the layer and add it to the map
        MxDoc = My.ArcMap.Document
        pMap = MxDoc.FocusMap
        pMap.AddLayer(pFLayer)
        MxDoc.UpdateContents()
        Exit Sub


    End Sub

#End Region

End Class