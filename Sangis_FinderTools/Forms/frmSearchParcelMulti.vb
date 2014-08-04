Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports System.Windows.Forms
Imports Sangis_FinderTools.Globals

''' <summary>
''' Designer class of the dockable window add-in. It contains user interfaces that
''' make up the dockable window.
''' </summary>
Public Class frmSearchParcelMulti
    Dim pFLayer As IFeatureLayer

#Region "Primaries"

    Public Sub New(ByVal hook As Object)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Hook = hook
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

            'Populate the Prefix Direction ComboBox
            With cboPreDir
                .Items.Add("N")
                .Items.Add("S")
                .Items.Add("E")
                .Items.Add("W")
            End With

            'Populate the Suffix ComboBox
            sSuffList = "AL,AV,BL,BR,CE,CG,CR,CS,CT,CV,CY,DR,DY,FY,GL,HY,LN,LO,LP," & _
                        "ML,PA,PE,PL,PS,PT,PY,PZ,RA,RD,RW,SQ,ST,TL,TR,TT,WK,WY"
            With cboRDSuf
                For Each vSuff In Split(sSuffList, ",")
                    .Items.Add(vSuff)
                Next vSuff
            End With

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

    Private m_hook As Object
    ''' <summary>
    ''' Host object of the dockable window
    ''' </summary> 
    ''' 
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

        Private m_windowUI As frmSearchParcelMulti

        Protected Overrides Function OnCreateChild() As System.IntPtr
            m_windowUI = New frmSearchParcelMulti(Me.Hook)
            Return m_windowUI.Handle
        End Function

        Protected Overrides Sub Dispose(ByVal Param As Boolean)
            If m_windowUI IsNot Nothing Then
                m_windowUI.Dispose(Param)
            End If

            MyBase.Dispose(Param)
        End Sub

    End Class

#End Region

#Region "Form"

    Private Sub frmSearchParcelMulti_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        'if the parcel layers do not exist, then disable in case they were remove after the menu was open
        If LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.PARCELS_ALL_NPC") Is Nothing Then
            If LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.PARCELS_ALL") Is Nothing Then
                lblStatusIssue.Visible = False
                Me.Enabled = False
            Else
                Me.Enabled = True
                CheckEnabled()
            End If
        Else
            Me.Enabled = True
            CheckEnabled()
        End If
    End Sub

#End Region

#Region "Buttons"

    Private Sub btnUpdateFromMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateFromMap.Click
        Try
            If pFLayer Is Nothing Then
                'Get the ParcelLayer NPC if available
                pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.PARCELS_ALL_NPC")
                'If NPC is not available, then get the Parcel All layer
                If pFLayer Is Nothing Then
                    pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.PARCELS_ALL")
                End If
            End If
            'skip if the parcel layer was not found but somehow it made it this far
            If Not pFLayer Is Nothing Then
                Dim pFeatureSelect As IFeatureSelection
                Dim pSelectionSet As ISelectionSet
                Dim pFeatcursor As IFeatureCursor
                Dim pFeature As IFeature
                'Get the selected features from the layer in the map
                pFeatureSelect = pFLayer
                lstbxParcels.Items.Clear()
                pSelectionSet = pFeatureSelect.SelectionSet
                pSelectionSet.Search(Nothing, False, pFeatcursor)
                Dim pSelCnt As Integer = pSelectionSet.Count
                'if there is an unusable quantity, then alert
                If pSelCnt > 500 Then
                    If MsgBoxResult.No = MessageBox.Show("Over 200 records selected, are you sure you want to do this?", "LOTS OF RECORDS SELECTED", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) Then
                        Exit Sub
                    End If
                End If
                pFeature = pFeatcursor.NextFeature
                'Get the APNS and add them to the list
                Do While Not pFeature Is Nothing
                    lstbxParcels.Items.Add(pFeature.Value(pFeature.Fields.FindField("APN")))
                    pFeature = pFeatcursor.NextFeature
                Loop
                'turn off the warning label
                lblStatusIssue.Visible = False
                CheckEnabled()
            End If
            lblStatusIssue.Visible = False
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

    Private Sub btnClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        ResetSelection()
    End Sub

    Private Sub btnZoomAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZoomAll.Click
        Try
            Dim pEnv As IEnvelope
            'get the extent of all selected features in layer and zoom to them
            pEnv = SelectedExtent(pFLayer)
            ZoomToEnvelope(pEnv)
            'turn on the layer if it isn't visible
            If Not pFLayer.Visible Then pFLayer.Visible = True
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

    Private Sub btnFindNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindNew.Click
        'select the single new feature 
        GetSelection(False)
    End Sub

    Private Sub btnAddToSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToSel.Click
        'Add the feature to the currently selected
        GetSelection(True)
    End Sub

    Private Sub btnDelSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelSel.Click
        Try
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
            'if there are any in the list selected then enable it
            If lstbxParcels.SelectedItems.Count > 0 Then
                Dim pFeatSel As IFeatureSelection
                Dim pSelectionSet As ISelectionSet
                Dim pFeatCursor As IFeatureCursor
                Dim pFeature As IFeature
                Dim pOID As Integer
                Dim iOIDListCount As Integer = 0
                Dim pOIDList() As Integer
                Dim pQF As IQueryFilter
                Dim pFndAPN
                'set the query based on the selected item
                pFndAPN = lstbxParcels.SelectedItem.ToString
                pQF = New QueryFilter
                pQF.WhereClause = "APN =  '" & pFndAPN & "'"
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
                lstbxParcels.Items.Remove(lstbxParcels.SelectedItem())
                'refresh the map
                Dim pAV As IActiveView
                pAV = My.ArcMap.Document.FocusMap
                pAV.Refresh()
                'turn on the layer if its off
                If Not pFLayer.Visible Then pFLayer.Visible = True
            End If
            CheckEnabled()
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

#End Region

#Region "Cbos and Lbxs"

    Private Sub lstbxParcels_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstbxParcels.SelectedIndexChanged
        'zoom map to selected parcel
        Try
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
            CheckEnabled()
            'check if any selected, if so zoom to it
            If lstbxParcels.SelectedItems.Count > 0 Then
                Dim pEnv As IEnvelope
                Dim pQF As IQueryFilter
                Dim pFndAPN As String
                'build the query
                pFndAPN = lstbxParcels.SelectedItem.ToString
                pQF = New QueryFilter
                pQF.WhereClause = "APN =  '" & pFndAPN & "'"
                'in case the selection in the map is different or cleared, need to
                'reselect the feature before trying to zoom to it and flashng it
                Dim pFeatSel As IFeatureSelection
                Dim FLayer As IFeatureLayer
                FLayer = pFLayer
                pFeatSel = FLayer
                pFeatSel.SelectFeatures(pQF, esriSelectionResultEnum.esriSelectionResultAdd, False)
                'get the extent
                pEnv = QueryFilterExtent(pFLayer, pQF)
                'zoom to it
                ZoomToEnvelope(pEnv)
                'makes sure the zoom is done before it flashes
                Application.DoEvents()
                FlashGeometry(pFLayer, pQF)
                If Not pFLayer.Visible Then pFLayer.Visible = True
            End If
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

#End Region

#Region "Text Boxes"

    Private Sub txtAPN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAPN.KeyPress
        'Only allow numbers and backspace
        Dim keyInput As String = e.KeyChar.ToString()
        If Char.IsNumber(e.KeyChar) Then
        ElseIf e.KeyChar = vbBack Then
        ElseIf e.KeyChar = vbTab Then
        ElseIf keyInput = "-" Then
        ElseIf keyInput = " " Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtAPN_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAPN.TextChanged
        CheckEnabled()
        lblStatusIssue.Visible = False
    End Sub

#End Region

#Region "Custom Subs and Functions"

    Public Sub CheckEnabled()
        'check for parcel layer in case loaded later
        If pFLayer Is Nothing Then
            'Get the ParcelLayer NPC if available
            pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.PARCELS_ALL_NPC")
            'If NPC is not available, then get the Parcel All layer
            If pFLayer Is Nothing Then
                pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.PARCELS_ALL")
            End If
        End If
        'check for selection counts in map vs selection list on tool
        'Check for 0 selections in both to un-enable updates
        If Not pFLayer Is Nothing Then
            Dim pFeatureSelect As IFeatureSelection
            pFeatureSelect = pFLayer
            lblSelCnt.Text = lstbxParcels.Items.Count
            If (Not IsDBNull(pFeatureSelect.SelectionSet)) And lblSelCnt.Text <> pFeatureSelect.SelectionSet.Count Then
                If pFeatureSelect.SelectionSet.Count > 0 Then
                    lblStatusIssue.Visible = True
                Else
                    lblStatusIssue.Visible = False
                End If
                lstbxParcels.Items.Clear()
            Else
                lblStatusIssue.Visible = False
            End If
            If lblSelCnt.Text = "0" And pFeatureSelect.SelectionSet.Count = 0 Then
                btnUpdateFromMap.Enabled = False
            Else
                btnUpdateFromMap.Enabled = True
            End If
        Else
            lblStatusIssue.Visible = False
        End If
        'Check APN search text (doesn't have to be drop down)
        If txtAPN.Text = "" And txtRDName.Text = "" Then
            btnFindNew.Enabled = False
        Else
            btnFindNew.Enabled = True
        End If
        If txtAPN.Text = "" Then
            txtRDName.Enabled = True
            txtHouseNum.Enabled = True
            cboPreDir.Enabled = True
            cboRDSuf.Enabled = True
            cboRDJur.Enabled = True
        Else
            txtRDName.Enabled = False
            txtHouseNum.Enabled = False
            cboPreDir.Enabled = False
            cboRDSuf.Enabled = False
            cboRDJur.Enabled = False
        End If
        'Manage the tools related to items in the listbox
        If lstbxParcels.Items.Count <= 0 Then
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
        lblSelCnt.Text = lstbxParcels.Items.Count
        'need to reset btnclearall if counts are off
        If lblStatusIssue.Visible Then
            btnClearAll.Enabled = True
        End If
    End Sub

    Private Sub ResetSelection()
        If Not pFLayer Is Nothing Then
            'clear the features
            Dim pFeatureSelect As IFeatureSelection
            pFeatureSelect = pFLayer
            pFeatureSelect.Clear()
        End If
        'refresh the focusmap
        Dim pAV As IActiveView
        pAV = My.ArcMap.Document.FocusMap
        pAV.Refresh()

        lstbxParcels.Items.Clear()
        CheckEnabled()
    End Sub

    Private Sub GetSelection(ByVal Multi As Boolean)
        Try

            If pFLayer Is Nothing Then
                'Get the ParcelLayer NPC if available
                pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.PARCELS_ALL_NPC")
                'If NPC is not available, then get the Parcel All layer
                If pFLayer Is Nothing Then
                    pFLayer = LoopThroughLayersAndGetFL(My.ArcMap.Document.ActiveView.FocusMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}", "SANGIS.PARCELS_ALL")
                End If
            End If

            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
            Dim pEnv As IEnvelope
            Dim pFeatSel As IFeatureSelection
            Dim pAPNID As String
            Dim pFeatCursor As IFeatureCursor
            Dim pFeature As IFeature
            Dim crntAPN As String
            Dim apnfld As Integer
            Dim apnexist As Boolean

            'Clear out original selection if its new instead of add to selection
            If Not Multi Then
                ResetSelection()
            End If

            Dim AddrSelect As String = ""
            Dim sWhere As String = ""
            Dim pRdNameTest As String = ""

            'Check the APN
            pAPNID = Trim(txtAPN.Text)
            pRdNameTest = Trim(txtRDName.Text)
            If pAPNID = "" And pRdNameTest = "" Then
                MessageBox.Show("Either APN or RoadName must be entered")
                Exit Sub
            End If
            If pAPNID = "" Then
                'do a search and get the list of values from addrapn table
                AddrSelect = GetSearchString()
                If AddrSelect = "" Then
                    MessageBox.Show("No Matches found")
                    Exit Sub
                End If
                sWhere = "APN IN (" & AddrSelect & ")"
            Else
                'clean up the apn if someone entered -s or spaces
                pAPNID = pAPNID.Trim()
                pAPNID = pAPNID.Replace("-", "")
                pAPNID = pAPNID.Replace(" ", "")
                'Check that the apn is a number now
                Dim pAPNchk As String
                pAPNchk = pAPNID.Replace("%", "")
                pAPNchk = pAPNchk.Replace("*", "")
                If Not IsNumeric(pAPNchk) Then
                    MessageBox.Show("APN " & pAPNchk & " is not numeric, please re-enter", "ERROR IN APN", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If InStr(pAPNID, "*") > 0 Or InStr(pAPNID, "%") > 0 Then
                    sWhere = "APN LIKE '" & pAPNID & "'"
                Else
                    If pAPNID.Length = 8 Then pAPNID = pAPNID & "00"
                    If pAPNID.Length <> 8 And pAPNID.Length <> 10 Then
                        MessageBox.Show("APN must consist of 8 or 10 numbers: " & pAPNID)
                        Exit Sub
                    End If
                    sWhere = "APN = '" & pAPNID & "'"
                End If
            End If

            'Set up selection query
            Dim pQF2 As IQueryFilter
            pQF2 = New QueryFilter
            If sWhere <> "" And sWhere <> " " Then
                pQF2.WhereClause = sWhere
            Else
                MessageBox.Show("No Matches found")
                Exit Sub
            End If



            'Get a reference to the parcel Layer
            pFeatSel = pFLayer

            'Get selection
            If Not Multi Then
                pFeatSel.Clear()
                pFeatSel.SelectFeatures(pQF2, esriSelectionResultEnum.esriSelectionResultNew, False)
            Else
                pFeatSel.SelectFeatures(pQF2, esriSelectionResultEnum.esriSelectionResultAdd, False)
            End If

            If pFeatSel.SelectionSet.Count <= 0 Then
                MsgBox("No Parcel found with that Information")
                Exit Sub
            End If

            'Add Parcels to the listbox of selected
            apnfld = pFLayer.FeatureClass.FindField("APN")
            pFeatCursor = pFLayer.Search(pQF2, False)
            pFeature = pFeatCursor.NextFeature
            Do While Not pFeature Is Nothing
                crntAPN = pFeature.Value(apnfld)
                apnexist = False
                'loop through the listbox so we don't get duplicate entries
                If lstbxParcels.Items.Count > 0 Then
                    For i = 0 To lstbxParcels.Items.Count - 1
                        If crntAPN = lstbxParcels.Items.Item(i) Then
                            apnexist = True
                        End If
                    Next
                End If
                If Not apnexist Then
                    lstbxParcels.Items.Add(crntAPN)
                End If
                pFeature = pFeatCursor.NextFeature
            Loop

            'make it visible if its not
            If Not pFLayer.Visible Then pFLayer.Visible = True

            'Zoom to the selected feature(s)
            pEnv = QueryFilterExtent(pFLayer, pQF2)
            ZoomToEnvelope(pEnv)

            CheckEnabled()

            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

#End Region

    Public Function GetSearchString() As String
        Dim stWhere As String = ""
        Dim APNstr As String = ""
        Try
            Dim sRoadName As String
            Dim sHouseNum As String, sPrefDir As String
            Dim sSuffix As String, sJuris As String

            'Get the user-entered values
            sRoadName = UCase(Trim(txtRDName.Text))
            sHouseNum = Trim(txtHouseNum.Text)
            sPrefDir = UCase(Trim(cboPreDir.Text))
            sSuffix = UCase(Trim(cboRDSuf.Text))
            sJuris = UCase(Trim(cboRDJur.Text))

            'Create the Where clause string based on user-entered criteria

            stWhere = "ADDRNAME like '" & sRoadName & "'"
            If sHouseNum <> "" Then
                stWhere = stWhere & " AND ADDRNMBR = '" & sHouseNum & "'"
            End If
            If sPrefDir <> "" Then stWhere = stWhere & " AND ADDRPDIR = '" & sPrefDir & "'"
            If sSuffix <> "" Then stWhere = stWhere & " AND ADDRSFX = '" & sSuffix & "'"
            If sJuris <> "" Then
                stWhere = stWhere & " AND ADDRJUR = '" & sJuris & "'"
            End If

            Dim pQueryFilter As IQueryFilter
            pQueryFilter = New QueryFilter

            Dim pCursor As ICursor
            Dim pRow As IRow
            Dim pTable As ITable

            pTable = GetWorkspaceTable("SANGIS.ADDRAPN")
            pQueryFilter.WhereClause = stWhere

            pCursor = pTable.Search(pQueryFilter, True)
            pRow = pCursor.NextRow

            Do While Not pRow Is Nothing
                APNstr = APNstr & "'" & pRow.Value(pRow.Fields.FindField("APN")) & "',"
                pRow = pCursor.NextRow
            Loop
            APNstr = APNstr.Trim(",")

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

        Return APNstr

    End Function

    Private Sub txtRDName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtRDName.TextChanged
        CheckEnabled()
    End Sub

    Private Sub lblStatusIssue_Click(sender As System.Object, e As System.EventArgs) Handles lblStatusIssue.Click

    End Sub
End Class