Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports Sangis_FinderTools.Globals
Imports System.Windows.Forms

''' <summary>
''' Designer class of the dockable window add-in. It contains user interfaces that
''' make up the dockable window.
''' </summary>
''' 

Public Class frmSearchRoadMulti

    Dim pFLayer As IFeatureLayer

#Region "Primaries"

    Public Sub New(ByVal hook As Object)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Hook = hook

        Try
            'Get the ParcelLayer NPC if available
            'pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.ROADS_ALL")

            CheckEnabled()
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Sub

    Private m_hook As Object
    ''' <summary>
    ''' Host object of the dockable window
    ''' </summary> 
    Public Property Hook() As Object
        Get
            Return m_hook
        End Get
        Set(ByVal value As Object)
            m_hook = value
        End Set
    End Property

    ''' <summary>
    ''' Implementation class of the dockable window add-in. It is responsible for
    ''' creating and disposing the user interface class for the dockable window.
    ''' </summary>
    Public Class AddinImpl
        Inherits ESRI.ArcGIS.Desktop.AddIns.DockableWindow

        Private m_windowUI As frmSearchRoadMulti

        Protected Overrides Function OnCreateChild() As System.IntPtr
            m_windowUI = New frmSearchRoadMulti(Me.Hook)
            Return m_windowUI.Handle
        End Function

        Protected Overrides Sub Dispose(ByVal Param As Boolean)
            If m_windowUI IsNot Nothing Then
                m_windowUI.Dispose(Param)
            End If

            MyBase.Dispose(Param)
        End Sub

    End Class

    Private Sub frmSearchRoadMulti_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Set up the tooltips
            Dim toolTip1 As New ToolTip()
            ' Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000
            toolTip1.InitialDelay = 1000
            toolTip1.ReshowDelay = 500
            ' Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = True

            ' Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(Me.btnAddToSel, "Add item to the current selection")
            toolTip1.SetToolTip(Me.btnClearAll, "Clear all selected records")
            toolTip1.SetToolTip(Me.btnDelSel, "Remove current selected record from Selection")
            toolTip1.SetToolTip(Me.btnFindNew, "Create new selection from item")
            toolTip1.SetToolTip(Me.btnUpdateFromMap, "Update Selection List from the Map")
            toolTip1.SetToolTip(Me.btnZoomAll, "Zoom to All Selected")
 
            CheckEnabled()
            Dim sSuffList As String, vSuff As Object
            'Dim sJurList As String, 
            Dim vJur As Object

            'Disable the search button (until user has entered at least road name)
            btnFindNew.Enabled = False

            'Populate the Prefix Direction ComboBox
            With cboPreDir
                .Items.Add("N")
                .Items.Add("S")
                .Items.Add("E")
                .Items.Add("W")
            End With

            'check for road layer in case loaded later
            If pFLayer Is Nothing Then
                'Get the ParcelLayer ROAD if available
                pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.ROADS_ALL")
            End If
            If Not pFLayer Is Nothing Then

                Dim pField = pFLayer.FeatureClass.Fields.Field(pFLayer.FeatureClass.Fields.FindField("RD30SFX"))
                Dim pCursos As ESRI.ArcGIS.Geodatabase.Cursor
                pCursos = CType(pFLayer.Search(Nothing, False), ICursor)
                Dim dataStatistics = New DataStatistics
                dataStatistics.Field = pFLayer.FeatureClass.Fields.Field(pFLayer.FeatureClass.Fields.FindField("RD30SFX")).Name
                dataStatistics.Cursor = pCursos

                Dim pEnum = dataStatistics.UniqueValues

                Dim suffval As String
                suffval = pEnum.MoveNext
                While suffval
                    cboRDSuf.Items.Add(pEnum.Current.ToString)
                    suffval = pEnum.MoveNext
                End While
                cboRDSuf.Sorted = True
            End If


            '    sSuffList

            '    'Populate the Suffix ComboBox
            'sSuffList = "AL,AV,BL,BR,CE,CG,CR,CS,CT,CV,CY,DR,DY,FY,GL,HY,LN,LO,LP," & _
            '            "ML,PA,PE,PL,PS,PT,PY,PZ,RA,RD,RW,SQ,ST,TL,TR,TT,WK,WY"
            'With cboRDSuf
            '    For Each vSuff In Split(sSuffList, ",")
            '        .Items.Add(vSuff)
            '    Next vSuff
            'End With

        'Populate the Jurisdiction ComboBox

            'With cboRDJur
            '    For Each vJur In Split(sJurList, ",")
            '        .Items.Add(vJur)
            '    Next vJur
            'End With
            For index = 0 To sJurList.GetUpperBound(0)
                cboRDJur.Items.Add(sJurList(index))
            Next

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

#End Region

#Region "Form"

    Private Sub frmSearchRoadMulti_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        'enable if the layer exists, disable incase the layer is removed at some point
        If LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.ROADS_ALL") Is Nothing Then
            Me.Enabled = False
        Else
            Me.Enabled = True
            CheckEnabled()
        End If
    End Sub

#End Region

#Region "Buttons"

    Private Sub btnAddToSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToSel.Click
        'add to selection
        GetSelection(True)
    End Sub

    Private Sub btnDelSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelSel.Click
        Try
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
            'check if any are selected
            If lstbxRoad.SelectedItems.Count > 0 Then
                Dim pFeatSel As IFeatureSelection
                Dim pSelectionSet As ISelectionSet
                Dim pFeatCursor As IFeatureCursor
                Dim pFeature As IFeature
                Dim pOID As Integer
                Dim iOIDListCount As Integer = 0
                Dim pOIDList() As Integer
                Dim pQF As IQueryFilter
                Dim pFndRD As String
                Dim pwhereclause As String
                'create the query string based on road or segment
                If lstbxRoad.SelectedItem.ToString.Contains("ROAD:") Then
                    pFndRD = Trim(lstbxRoad.SelectedItem.ToString.Replace("ROAD: ", ""))
                    pFndRD = pFndRD.Substring(0, pFndRD.IndexOf(" : "))
                    pwhereclause = "ROADID = " & pFndRD
                Else
                    pFndRD = Trim(lstbxRoad.SelectedItem.ToString.Replace("SEGMENT: ", ""))
                    pFndRD = pFndRD.Substring(0, pFndRD.IndexOf(" : "))
                    pwhereclause = "ROADSEGID = " & pFndRD
                End If
                pFndRD = lstbxRoad.SelectedItem.ToString
                pQF = New QueryFilter
                pQF.WhereClause = pwhereclause
                'Get a reference to the parcel Layer
                pFeatSel = pFLayer
                pSelectionSet = pFeatSel.SelectionSet
                pSelectionSet.Search(pQF, False, pFeatCursor)
                Dim pSelCnt As Integer = pSelectionSet.Count
                ReDim pOIDList(pSelCnt)
                pFeature = pFeatCursor.NextFeature
                'Get the oids and remove them from the selection and the list
                Do While Not pFeature Is Nothing
                    pOID = pFeature.Value(pFeature.Fields.FindField("ObjectID"))
                    pOIDList(iOIDListCount) = pOID
                    iOIDListCount = iOIDListCount + 1
                    pFeature = pFeatCursor.NextFeature
                Loop
                If iOIDListCount <> 0 Then
                    pFeatSel.SelectionSet.RemoveList(iOIDListCount, pOIDList(0)) '(1, pOID)
                End If
                lstbxRoad.Items.Remove(lstbxRoad.SelectedItem())
                'refresh focusmap
                Dim pAV As IActiveView
                pAV = My.ArcMap.Document.FocusMap
                pAV.Refresh()
                'make visible if its not
                If Not pFLayer.Visible Then pFLayer.Visible = True
            End If
            CheckEnabled()
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

    Private Sub btnFindNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindNew.Click
        'get single selection
        GetSelection(False)
    End Sub

    Private Sub btnZoomAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnZoomAll.Click
        Try
            Dim pEnv As IEnvelope
            'get the selection and zoom to the extent
            pEnv = SelectedExtent(pFLayer)
            ZoomToEnvelope(pEnv)
            'make visible if its not
            If Not pFLayer.Visible Then pFLayer.Visible = True
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

    Private Sub btnUpdateFromMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateFromMap.Click
        Try
            If pFLayer Is Nothing Then
                'Get the Road Layerif available
                pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.ROADS_ALL")
            End If
            If Not pFLayer Is Nothing Then
                Dim pFeatureSelect As IFeatureSelection
                Dim pSelectionSet As ISelectionSet
                Dim pFeatcursor As IFeatureCursor
                Dim pFeature As IFeature
                Dim lSegIDCol As Long, lRDIDCol As Long
                Dim lPrefCol As Long, lNameCol As Long, lSuffCol As Long
                Dim lJurisCol As Long, lZipCol As Long
                Dim sSegIDVal As String, sRDIDVal As String
                Dim sPrefVal As String, sNameVal As String, sSuffVal As String
                Dim sJurisVal As String, sZipVal As String, sFullVal As String
                Dim sCheckVal As String
                Dim colRoads As New Collection
                Dim RdExist As Boolean

                'Change the cursor to hourglass
                Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor

                'Get the column numbers
                lRDIDCol = pFLayer.FeatureClass.FindField("ROADID")
                lSegIDCol = pFLayer.FeatureClass.FindField("ROADSEGID")
                lPrefCol = pFLayer.FeatureClass.FindField("RD30PRED")
                lNameCol = pFLayer.FeatureClass.FindField("RD30NAME")
                lSuffCol = pFLayer.FeatureClass.FindField("RD30SFX")
                lJurisCol = pFLayer.FeatureClass.FindField("LJURISDIC")
                lZipCol = pFLayer.FeatureClass.FindField("L_ZIP")
                'get the layer
                pFeatureSelect = pFLayer
                'claer all listbox items then update from the map
                lstbxRoad.Items.Clear()
                pSelectionSet = pFeatureSelect.SelectionSet
                pSelectionSet.Search(Nothing, False, pFeatcursor)
                Dim pSelCnt As Integer = pSelectionSet.Count
                'warn if there are a ton returned
                If pSelCnt > 500 Then
                    If MsgBoxResult.No = MessageBox.Show("Over 200 records selected, are you sure you want to do this?", "LOTS OF RECORDS SELECTED", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) Then
                        Exit Sub
                    End If
                End If
                pFeature = pFeatcursor.NextFeature
                'Get the Road segments and add them to the list
                Do While Not pFeature Is Nothing
                    'Get the attribute values of the feature
                    sRDIDVal = pFeature.Value(lRDIDCol)
                    sSegIDVal = pFeature.Value(lSegIDCol)
                    sPrefVal = Trim(NVL(pFeature.Value(lPrefCol)))
                    sNameVal = Trim(pFeature.Value(lNameCol))
                    sSuffVal = Trim(NVL(pFeature.Value(lSuffCol)))
                    sJurisVal = Trim(NVL(pFeature.Value(lJurisCol)))
                    sZipVal = Trim(pFeature.Value(lZipCol))

                    'If road is unique add it to the ListBox (in user-friendly format)
                    'Compile the full value
                    sFullVal = ""
                    If sPrefVal <> "" Then sFullVal = sPrefVal & " "
                    sFullVal = sFullVal & sNameVal
                    If sSuffVal <> "" Then sFullVal = sFullVal & " " & sSuffVal
                    If sJurisVal <> "" Then sFullVal = sFullVal & ", " & sJurisVal
                    If sZipVal <> "" Then sFullVal = sFullVal & "  " & sZipVal

                    'Add the full street string to the ListBox
                    sCheckVal = "SEGMENT: " & sSegIDVal & " : " & sFullVal

                    RdExist = False
                    'loop through the listbox so we don't get duplicate entries
                    If lstbxRoad.Items.Count > 0 Then
                        For i = 0 To lstbxRoad.Items.Count - 1
                            If sCheckVal = lstbxRoad.Items.Item(i) Then
                                RdExist = True
                            End If
                        Next
                    End If
                    If Not RdExist Then
                        lstbxRoad.Items.Add(sCheckVal)
                    End If
                    pFeature = pFeatcursor.NextFeature
                Loop
                CheckEnabled()
            End If
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

    Private Sub btnClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        'reset all controls
        ResetSelection()
        txtRDID.Text = ""
        txtRDSegID.Text = ""
        cboRDJur.Text = ""
        txtHouseNum.Text = ""
        cboPreDir.Text = ""
        txtRDName.Text = ""
        cboRDSuf.Text = ""
    End Sub


#End Region

#Region "Cbos and Lbxs"

    Private Sub lstbxRoad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstbxRoad.SelectedIndexChanged
        'zoom map to selected road
        Try
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
            CheckEnabled()
            'check if any are selected
            If lstbxRoad.SelectedItems.Count > 0 Then
                Dim pEnv As IEnvelope
                Dim pQF As IQueryFilter
                Dim pFndRD As String
                Dim pwhereclause As String
                Dim pmultisearch As Boolean
                'build the query
                If lstbxRoad.SelectedItem.ToString.Contains("ROAD:") Then
                    pmultisearch = True
                    pFndRD = Trim(lstbxRoad.SelectedItem.ToString.Replace("ROAD: ", ""))
                    pFndRD = pFndRD.Substring(0, pFndRD.IndexOf(" : "))
                    pwhereclause = "ROADID = " & pFndRD
                Else
                    pmultisearch = False
                    pFndRD = Trim(lstbxRoad.SelectedItem.ToString.Replace("SEGMENT: ", ""))
                    pFndRD = pFndRD.Substring(0, pFndRD.IndexOf(" : "))
                    pwhereclause = "ROADSEGID = " & pFndRD
                End If
                pFndRD = lstbxRoad.SelectedItem.ToString
                pQF = New QueryFilter
                pQF.WhereClause = pwhereclause
                'in case the selection in the map is different or cleared, need to
                'reselect the feature before trying to zoom to it and flashng it
                Dim pFeatSel As IFeatureSelection
                Dim FLayer As IFeatureLayer
                FLayer = pFLayer
                pFeatSel = FLayer
                pFeatSel.SelectFeatures(pQF, esriSelectionResultEnum.esriSelectionResultAdd, pmultisearch)
                'get the selection and zoom to extent
                pEnv = QueryFilterExtent(pFLayer, pQF)
                ZoomToEnvelope(pEnv)
                'make sure zoom is done before flashing
                Application.DoEvents()
                'make visible if its not
                If Not pFLayer.Visible Then pFLayer.Visible = True
                FlashGeometry(pFLayer, pQF)
            End If
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

#End Region

#Region "Text Boxes"

    Private Sub txtRDSegID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRDSegID.KeyPress
        If Char.IsNumber(e.KeyChar) Then
            'do nothing 
        ElseIf e.KeyChar = vbBack Then
            'do nothing
        ElseIf e.KeyChar = vbTab Then
            If Not IsNumeric(txtRDSegID.Text) And Not txtRDSegID.Text = "" Then
                MsgBox("ID must be numeric")
                Exit Sub
            End If
            txtRDID.Focus()
        Else
            e.KeyChar = ChrW(0)
        End If
    End Sub

    Private Sub txtRDSegID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRDSegID.TextChanged
        'disable search if blanks
        btnFindNew.Enabled = txtRDID.Text <> "" Or txtRDSegID.Text <> "" Or txtRDName.Text <> ""
    End Sub

    Private Sub txtRDID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRDID.KeyPress
        'only allow numbers
        If Char.IsNumber(e.KeyChar) Then
            'do nothing 
        ElseIf e.KeyChar = vbBack Then
            'do nothing
        ElseIf e.KeyChar = vbTab Then
            If Not IsNumeric(txtRDID.Text) And Not txtRDID.Text = "" Then
                MsgBox("ID must be numeric")
                Exit Sub
            End If
            'jumpt to the house number 
            txtHouseNum.Focus()
        Else
            e.KeyChar = ChrW(0)
        End If
    End Sub

    Private Sub txtRDID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRDID.TextChanged
        'disable if all blank
        btnFindNew.Enabled = txtRDID.Text <> "" Or txtRDSegID.Text <> "" Or txtRDName.Text <> ""
    End Sub

    Private Sub txtHouseNum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHouseNum.KeyPress
        'make sure it is all numbers
        If Char.IsNumber(e.KeyChar) Then
            'do nothing 
        ElseIf e.KeyChar = vbBack Then
            'do nothing
        ElseIf e.KeyChar = vbTab Then
            If Not IsNumeric(txtHouseNum.Text) And Not txtHouseNum.Text = "" Then
                MsgBox("Addr must be numeric")
                Exit Sub
            End If
            cboPreDir.Focus()
        Else
            e.KeyChar = ChrW(0)
        End If
    End Sub

    Private Sub txtRDName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRDName.TextChanged
        'disable if blanks
        btnFindNew.Enabled = txtRDID.Text <> "" Or txtRDSegID.Text <> "" Or txtRDName.Text <> ""
    End Sub

#End Region

#Region "Custom Subs and Functions"

    Private Sub GetUniqueRoads(ByVal FLayer As IFeatureLayer, ByVal QFilter As IQueryFilter, ByVal SearchType As String)
        'This routine get a list of unique roads and populates the ListBox in the
        'form (note: there may be only one unique road with multiple
        'segments, in which case the user will not be given the chance to choose)
        Try
            Dim pFeatCursor As IFeatureCursor
            Dim pFeature As IFeature
            Dim lSegIDCol As Long, lRDIDCol As Long
            Dim lPrefCol As Long, lNameCol As Long, lSuffCol As Long
            Dim lJurisCol As Long, lZipCol As Long
            Dim sSegIDVal As String, sRDIDVal As String
            Dim sPrefVal As String, sNameVal As String, sSuffVal As String
            Dim sJurisVal As String, sZipVal As String, sFullVal As String
            Dim sCheckVal As String
            Dim colRoads As New Collection
            Dim RdExist As Boolean

            'Change the cursor to hourglass
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor

            'Get the column numbers
            lRDIDCol = FLayer.FeatureClass.FindField("ROADID")
            lSegIDCol = FLayer.FeatureClass.FindField("ROADSEGID")
            lPrefCol = FLayer.FeatureClass.FindField("RD30PRED")
            lNameCol = FLayer.FeatureClass.FindField("RD30NAME")
            lSuffCol = FLayer.FeatureClass.FindField("RD30SFX")
            lJurisCol = FLayer.FeatureClass.FindField("LJURISDIC")
            lZipCol = FLayer.FeatureClass.FindField("L_ZIP")

            'Check if the records are unique for prefix, suffix, and jurisdiction
            pFeatCursor = FLayer.Search(QFilter, False)
            pFeature = pFeatCursor.NextFeature
            Do While Not pFeature Is Nothing
                'Get the attribute values of the feature
                sRDIDVal = pFeature.Value(lRDIDCol)
                sSegIDVal = pFeature.Value(lSegIDCol)
                sPrefVal = Trim(NVL(pFeature.Value(lPrefCol)))
                sNameVal = Trim(pFeature.Value(lNameCol))
                sSuffVal = Trim(NVL(pFeature.Value(lSuffCol)))
                sJurisVal = Trim(NVL(pFeature.Value(lJurisCol)))
                sZipVal = Trim(pFeature.Value(lZipCol))

                'If road is unique add it to the ListBox (in user-friendly format)
                'Compile the full value
                sFullVal = ""
                If sPrefVal <> "" Then sFullVal = sPrefVal & " "
                sFullVal = sFullVal & sNameVal
                If sSuffVal <> "" Then sFullVal = sFullVal & " " & sSuffVal
                If sJurisVal <> "" Then sFullVal = sFullVal & ", " & sJurisVal
                If sZipVal <> "" Then sFullVal = sFullVal & "  " & sZipVal

                'Add the full street string to the ListBox
                If SearchType = "ROAD" Then
                    'pFrstVal = sPrefVal & "-" & sNameVal & "-" & sSuffVal & _
                    '"-" & sJurisVal & "-" & sZipVal
                    sCheckVal = "ROAD: " & sRDIDVal & " : " & sFullVal
                Else
                    'pFrstVal = sSegIDVal
                    sCheckVal = "SEGMENT: " & sSegIDVal & " : " & sFullVal
                End If
                'sCheckVal = pFrstVal & " " & sFullVal
                RdExist = False
                'loop through the listbox so we don't get duplicate entries
                If lstbxRoad.Items.Count > 0 Then
                    For i = 0 To lstbxRoad.Items.Count - 1
                        If sCheckVal = lstbxRoad.Items.Item(i) Then
                            RdExist = True
                        End If
                    Next
                End If
                If Not RdExist Then
                    lstbxRoad.Items.Add(sCheckVal)
                End If
                pFeature = pFeatCursor.NextFeature
            Loop

            'Reset mouse cursor
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

    Private Function QueryFilterExtent(ByVal FLayer As IFeatureLayer, ByVal QFilter As IQueryFilter) As IEnvelope
        QueryFilterExtent = Nothing
        Try
            Dim pEnv As IEnvelope
            Dim pFeatCursor As IFeatureCursor
            Dim pFeature As IFeature

            'Get the extent of the selected feature(s)
            pEnv = New Envelope
            pFeatCursor = FLayer.Search(QFilter, False)
            pFeature = pFeatCursor.NextFeature
            Do Until pFeature Is Nothing
                pEnv.Union(pFeature.Extent)
                pFeature = pFeatCursor.NextFeature
            Loop

            'Return the extent of the selected features, if able to calculate it
            If Not pEnv.IsEmpty Then
                QueryFilterExtent = pEnv
            Else
                MsgBox("Unable to determine the extent of the selected road(s)", _
                       vbExclamation, "Road Search Error")
                QueryFilterExtent = Nothing
            End If
            Exit Function

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Function

    Public Function GetSearchString() As String
        Dim sWhere As String = ""
        Try
            Dim sRoadName As String
            Dim sHouseNum As String, sPrefDir As String
            Dim sSuffix As String, sJuris As String
            Dim sRDID As String, sRDSegID As String

            'Get the user-entered values
            sRDID = Trim(txtRDID.Text)
            sRDSegID = Trim(txtRDSegID.Text)
            sRoadName = UCase(Trim(txtRDName.Text))
            sHouseNum = Trim(txtHouseNum.Text)
            sPrefDir = UCase(Trim(cboPreDir.Text))
            sSuffix = UCase(Trim(cboRDSuf.Text))
            sJuris = UCase(Trim(cboRDJur.Text))

            'Create the Where clause string based on user-entered criteria
            If sRDSegID <> "" Then
                sWhere = "ROADSEGID = " & sRDSegID
            ElseIf sRDID <> "" Then
                sWhere = "ROADID = " & sRDID
            Else
                sWhere = "RD30NAME like '" & sRoadName & "'"
                If sHouseNum <> "" Then
                    sWhere = sWhere & " AND ABLOADDR <= " & sHouseNum
                    sWhere = sWhere & " AND ABHIADDR >= " & sHouseNum
                End If
                If sPrefDir <> "" Then sWhere = sWhere & " AND RD30PRED = '" & sPrefDir & "'"
                If sSuffix <> "" Then sWhere = sWhere & " AND RD30SFX = '" & sSuffix & "'"
                If sJuris <> "" Then
                    sWhere = sWhere & " AND (LJURISDIC = '" & sJuris & "'"
                    sWhere = sWhere & " OR RJURISDIC = '" & sJuris & "')"
                End If
            End If

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

        Return sWhere

    End Function

    Public Sub CheckEnabled()
        'check for road layer in case loaded later
        If pFLayer Is Nothing Then
            'Get the ParcelLayer ROAD if available
            pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.ROADS_ALL")
        End If
        'Check for 0 selections in both to un-enable updates
        If Not pFLayer Is Nothing Then
            Dim pFeatureSelect As IFeatureSelection
            pFeatureSelect = pFLayer
            lblSelCnt.Text = lstbxRoad.Items.Count
            lblTotalSelCnt.Text = pFeatureSelect.SelectionSet.Count
            If lblSelCnt.Text = "0" And pFeatureSelect.SelectionSet.Count = 0 Then
                btnUpdateFromMap.Enabled = False
            Else
                btnUpdateFromMap.Enabled = True
            End If
            'Check Road search text 
            btnFindNew.Enabled = txtRDID.Text <> "" Or txtRDSegID.Text <> "" Or txtRDName.Text <> ""
            'clear the listbox items if the map has been cleared
            If pFeatureSelect.SelectionSet.Count = 0 Then
                lstbxRoad.Items.Clear()
            End If
            'Manage the tools related to items in the listbox
            If lstbxRoad.Items.Count <= 0 Then
                btnClearAll.Enabled = False
                btnAddToSel.Enabled = False
                btnZoomAll.Enabled = False
                btnDelSel.Enabled = False
            Else
                btnClearAll.Enabled = True
                btnAddToSel.Enabled = True
                btnZoomAll.Enabled = True
                btnDelSel.Enabled = True
            End If
            lblSelCnt.Text = lstbxRoad.Items.Count
            'need to reset btnclearall if counts are off
            If lblSelCnt.Text <> "0" Or pFeatureSelect.SelectionSet.Count <> 0 Then
                btnClearAll.Enabled = True
            End If
        End If
    End Sub

    Private Sub GetSelection(ByVal Multi As Boolean)
        Try
            If pFLayer Is Nothing Then
                'Get the Road Layerif available
                pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.ROADS_ALL")
            End If
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
            Dim pEnv As IEnvelope
            Dim pFeatSel As IFeatureSelection
            Dim pQFilter As IQueryFilter
            Dim FLayer As IFeatureLayer
            FLayer = pFLayer
            pFeatSel = FLayer

            'Clear out original selection if its new instead of add to selection
            If Not Multi Then
                ResetSelection()
            End If

            Dim sWhere As String
            Dim sHouseNum As String
            Dim sRDSegID As String
            Dim SearchType As String
            Dim pmultisearch As Boolean

            'Get the user-entered values
            sRDSegID = Trim(txtRDSegID.Text)
            sHouseNum = Trim(txtHouseNum.Text)

            'Setup the query and find the feature(s) matching the user-entered criteria
            sWhere = GetSearchString()
            pQFilter = New QueryFilter
            pQFilter.SubFields = "*"
            pQFilter.WhereClause = sWhere

            'If house or seg number is given, search for road segment; otherwise search for unique road
            If sHouseNum = "" And sRDSegID = "" Then
                SearchType = "ROAD"
                pmultisearch = False
            Else
                SearchType = "SEGMENT"
                pmultisearch = True
            End If

            'Get selection
            If Not Multi Then
                pFeatSel.Clear()
                pFeatSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultNew, pmultisearch)
            Else
                pFeatSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, pmultisearch)
            End If
            'if none were found, alert
            If pFeatSel.SelectionSet.Count <= 0 Then
                MsgBox("No Roads found matching Search Criteria")
                Exit Sub
            End If

            'Add Roads to the listbox of selected
            'If more than one record is returned, create list of unique roads
            If pFeatSel.SelectionSet.Count > 0 Then
                GetUniqueRoads(pFLayer, pQFilter, SearchType)
            End If

            'Zoom to the selected feature(s)
            pEnv = QueryFilterExtent(pFLayer, pQFilter)
            ZoomToEnvelope(pEnv)

            'make it visible if its not
            If Not pFLayer.Visible Then pFLayer.Visible = True

            CheckEnabled()

            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

    Private Sub ResetSelection()
        'reset
        If Not pFLayer Is Nothing Then
            Dim pFeatureSelect As IFeatureSelection
            pFeatureSelect = pFLayer
            pFeatureSelect.Clear()
        End If
        'clear the checkbox
        lstbxRoad.Items.Clear()
        CheckEnabled()
        'refresh focusmap
        Dim pAV As IActiveView
        pAV = My.ArcMap.Document.FocusMap
        pAV.Refresh()
    End Sub

#End Region

    Private Sub cboRDSuf_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboRDSuf.SelectedIndexChanged

    End Sub
End Class