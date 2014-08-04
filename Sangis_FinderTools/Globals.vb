Imports System
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display

Public Class Globals
    'used to fill combo boxes
    'Public Shared sJurList As String = "CB,CN,CO,CV,DM,EC,EN,ES,IB,LG,LM,NC,OC,PW,SD,SM,SO,ST,VS"
    'Public Shared sJurList As System.Array = ("CB","CN","CO",CV,DM,EC,EN,ES,IB,LG,LM,NC,OC,PW,SD,SM,SO,ST,VS)
    Public Shared sJurList = {"CB", "CN", "CO", "CV", "DM", "EC", "EN", "ES", "IB", "LG", "LM", "NC", "OC", "PW", "SD", "SM", "SO", "ST", "VS"}
    Public Shared sDBName As String = "SDEP"
    'Public Shared sDBName As String = "SDE"


#Region "Loop Through Layers"

    Shared Sub LoopThroughLayersAndZoomToExtent(ByVal map As IMap, ByVal layerCLSID As String, ByVal layerName As String)

        If map Is Nothing OrElse layerCLSID Is Nothing Then
            Return
        End If

        Dim uid As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass
        uid.Value = layerCLSID ' Example: "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer

        Try
            Dim pDataset As IDataset
            Dim pActiveView As IActiveView
            pActiveView = My.ArcMap.Document.ActiveView
            Dim enumLayer As IEnumLayer = map.Layers((CType(uid, ESRI.ArcGIS.esriSystem.UID)), True) ' Explicit Cast
            enumLayer.Reset()
            Dim layer As ILayer = enumLayer.Next
            Do While Not (layer Is Nothing)
                If layer.Valid Then
                    pDataset = layer
                    If pDataset.BrowseName = UCase(layerName) Then
                        'in case its in layout view and data view, need to grab focusmap
                        Dim pAV As IActiveView
                        pAV = My.ArcMap.Document.FocusMap
                        pAV.Extent = layer.AreaOfInterest
                        pAV.Refresh()
                        Exit Do
                    End If
                End If
                layer = enumLayer.Next()
            Loop

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Sub

    Shared Function LoopThroughLayersAndGetFL(ByVal map As IMap, ByVal layerCLSID As String, ByVal layerName As String) As IFeatureLayer

        If map Is Nothing OrElse layerCLSID Is Nothing Then
            Return Nothing
        End If

        Dim uid As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass
        uid.Value = layerCLSID ' Example: "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer

        Try
            Dim pDataset As IDataset
            Dim enumLayer As IEnumLayer = map.Layers((CType(uid, ESRI.ArcGIS.esriSystem.UID)), True) ' Explicit Cast
            enumLayer.Reset()
            Dim layer As ILayer = enumLayer.Next
            Do While Not (layer Is Nothing)
                If layer.Valid Then
                    pDataset = layer
                    If pDataset.BrowseName = UCase(layerName) Or pDataset.BrowseName = UCase(sDBName + "." + layerName) Then
                        Return pDataset
                        Exit Function
                    End If
                End If
                layer = enumLayer.Next()
            Loop
            Return Nothing
        Catch ex As Exception
            Return Nothing
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Function

    Shared Sub LoopThroughLayersAndTurnOffAll(ByVal map As IMap, ByVal layerCLSID As String)

        If map Is Nothing OrElse layerCLSID Is Nothing Then
            Return
        End If

        Dim uid As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass
        uid.Value = layerCLSID ' Example: "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer

        Try
            Dim enumLayer As IEnumLayer = map.Layers((CType(uid, ESRI.ArcGIS.esriSystem.UID)), True) ' Explicit Cast
            enumLayer.Reset()
            Dim layer As ILayer = enumLayer.Next
            Do While Not (layer Is Nothing)
                If layer.Valid Then
                    layer.Visible = False
                End If
                layer = enumLayer.Next()
            Loop
            'in case its in layout view and data view, need to grab focusmap
            Dim pAV As IActiveView
            pAV = My.ArcMap.Document.FocusMap
            pAV.Refresh()
            My.ArcMap.Document.UpdateContents()

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Sub

#End Region

#Region "Workspace"

    Public Function GetPathForALayer(ByVal layer As ILayer) As System.String

        If Not (layer.Valid) OrElse layer Is Nothing OrElse Not (TypeOf layer Is IDataset) Then
            Return Nothing
        End If

        Dim dataset As IDataset = CType(layer, IDataset) ' Explicit Cast

        Return (dataset.Workspace.PathName & "\" & dataset.Name)

    End Function


    Shared Function GetFeatureWorkSpace() As IFeatureWorkspace
        'Get the Feature Workspace (SDE) of the first valid Dataset in TOC

        Dim MxDoc As IMxDocument
        Dim pDataset As IDataset
        Dim iIdx As Integer
        Dim pFWS As IFeatureWorkspace = Nothing

        'Get the workspace from the first SDE/SANGIS layer in the TOC
        MxDoc = My.ArcMap.Document
        For iIdx = 0 To MxDoc.FocusMap.LayerCount - 1
            If MxDoc.FocusMap.Layer(iIdx).Valid Then
                If TypeOf MxDoc.FocusMap.Layer(iIdx) Is IDataset Then
                    pDataset = MxDoc.FocusMap.Layer(iIdx)
                    If pDataset.BrowseName.Contains("SANGIS.") Then
                        pDataset = MxDoc.FocusMap.Layer(iIdx)
                        If TypeOf pDataset.Workspace Is IFeatureWorkspace Then
                            If pDataset.Workspace.Type = esriWorkspaceType.esriRemoteDatabaseWorkspace Then
                                pFWS = pDataset.Workspace
                                Exit For
                            End If
                        End If
                    End If
                End If
            End If
        Next iIdx

        'Return the FeatureWorkSpace found
        GetFeatureWorkSpace = pFWS
    End Function


    Shared Function GetWorkspaceTable(ByVal pTableLoad As String) As ITable
        GetWorkspaceTable = Nothing
        Dim pWkspc As IWorkspace
        Dim pMap As Map
        Dim pEnumLayers As IEnumLayer
        Dim pLayer As ILayer
        Dim pFeatWorkSpace As IFeatureWorkspace
        Dim pTableName As String
        Dim pCheckTable As ITable
        Dim pDataset As IDataset

        pTableName = pTableLoad
        pWkspc = Nothing
        pMap = My.ArcMap.Document.FocusMap
        pEnumLayers = pMap.Layers(Nothing, False)
        pLayer = pEnumLayers.Next
        Do Until pLayer Is Nothing
            If pLayer.Valid Then
                pDataset = pLayer
                If TypeOf pDataset.Workspace Is IFeatureWorkspace Then
                    If pDataset.Workspace.Type = esriWorkspaceType.esriRemoteDatabaseWorkspace Then
                        If pDataset.BrowseName.Contains("SANGIS.") And pLayer.SupportedDrawPhases = 7 Then 'Not pLayer.Name Like "*Topology"
                            'pFLayer = pLayer

                            pWkspc = pDataset.Workspace
                            Exit Do

                        End If
                    End If
                End If
            End If
            pLayer = pEnumLayers.Next
        Loop
        If pWkspc Is Nothing Then
            MsgBox("Could Not Open Workspace, no SANGIS layers found in Map")
            GetWorkspaceTable = Nothing
        Else
            pFeatWorkSpace = pWkspc 'QI

            pCheckTable = pFeatWorkSpace.OpenTable(pTableName)
            If pCheckTable Is Nothing Then
                pCheckTable = pFeatWorkSpace.OpenTable("SDEP." + pTableName)
                MsgBox("Could not Load table: " & pTableLoad)
                GetWorkspaceTable = Nothing
            Else
                GetWorkspaceTable = pCheckTable
            End If
        End If
    End Function

#End Region

#Region "Data Frame"

    Shared Sub SetDataframeExtenttoLayerExtent(ByVal df As String, ByVal layerName As String)

        If df Is Nothing OrElse layerName Is Nothing Then
            Return
        End If
        'Esri_DataLayer() '{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}
        'Esri_GeoFeatureLayer() '{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}
        'Esri_GraphicsLayer() '{34B2EF81-F4AC-11D1-A245-080009B6F22B}
        'Esri_FDOGraphicsLayer() '{5CEAE408-4C0A-437F-9DB3-054D83919850}
        'Esri_CoverageAnnotationLayer() '{0C22A4C7-DAFD-11D2-9F46-00C04F6BC78E}
        'Esri_GroupLayer() '{EDAD6644-1810-11D1-86AE-0000F8751720}

        'Dim uid As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass
        'uid.Value = "{34B2EF81-F4AC-11D1-A245-080009B6F22B}" ' Example: "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer

        Try
            ' Changes extents for the specified dataframe ot the layers full extent
            Dim pMxDoc As IMxDocument
            Dim f As Long
            Dim pDataset As IDataset

            pMxDoc = My.ArcMap.Document

            ' Search for the dataframe with the specified name and make it active if its not
            For f = 0 To pMxDoc.Maps.Count - 1
                If LCase(pMxDoc.Maps.Item(f).Name) = LCase(df) Then

                    Dim enumLayer As IEnumLayer = pMxDoc.Maps.Item(f).Layers(Nothing, True) '((CType(uid, ESRI.ArcGIS.esriSystem.UID)), True) ' Explicit Cast
                    enumLayer.Reset()
                    Dim layer As ILayer = enumLayer.Next
                    Do While Not (layer Is Nothing)
                        If layer.Valid Then
                            pDataset = layer
                            If UCase(pDataset.BrowseName).Contains(UCase(layerName)) Then
                                Dim pAV As IActiveView
                                pAV = pMxDoc.Maps.Item(f)
                                pAV.Extent = layer.AreaOfInterest
                                pAV.Refresh()
                                Exit Do
                            End If
                        End If
                        layer = enumLayer.Next()
                    Loop

                    Exit For
                End If
            Next f

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Sub

    Shared Sub ActivateDataFrame(ByVal sName As String)
        Try
            ' Activates the specified dataframe
            Dim pMxDoc As IMxDocument
            Dim f As Long
            Dim bFound As Boolean

            bFound = False
            pMxDoc = My.ArcMap.Document

            ' Search for the dataframe with the specified name and make it active if its not
            For f = 0 To pMxDoc.Maps.Count - 1
                If LCase(pMxDoc.Maps.Item(f).Name) = LCase(sName) Then
                    If Not pMxDoc.Maps.Item(f) Is pMxDoc.ActiveView Then
                        pMxDoc.ActiveView.FocusMap = pMxDoc.Maps.Item(f)
                    End If
                    bFound = True
                    Exit For
                End If
            Next f

            ' If the name wasn't found then display message
            If Not bFound Then
                MsgBox("Can't find dataframe: " + sName, vbExclamation, "")
            End If
        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Sub

    Shared Function CheckActiveFrame(ByVal sName As String) As Boolean
        Try
            ' Activates the specified dataframe
            Dim pMxDoc As IMxDocument
            Dim f As Long
            pMxDoc = My.ArcMap.Document

            ' Search for the dataframe with the specified name and check if it is active
            For f = 0 To pMxDoc.Maps.Count - 1
                If pMxDoc.ActiveView.FocusMap Is pMxDoc.Maps.Item(f) And UCase(pMxDoc.Maps.Item(f).Name) = UCase(sName) Then
                    CheckActiveFrame = True
                    Exit For
                Else
                    CheckActiveFrame = False
                End If
            Next f

        Catch ex As Exception
            Return False
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Function

    Shared Function FindLayerDataFrame(ByVal sName As String) As String
        Try
            ' Activates the specified dataframe
            Dim pMxDoc As IMxDocument
            Dim pDataset As IDataset
            Dim f As Long
            pMxDoc = My.ArcMap.Document
            FindLayerDataFrame = "None"
            ' Search for the dataframe with the specified name and check if it is active
            For f = 0 To pMxDoc.Maps.Count - 1
                Dim uid As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass
                uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" '= IGeoFeatureLayer

                Dim enumLayer As IEnumLayer = pMxDoc.Maps.Item(f).Layers((CType(uid, ESRI.ArcGIS.esriSystem.UID)), True) ' Explicit Cast
                enumLayer.Reset()
                Dim layer As ILayer = enumLayer.Next
                Do While Not (layer Is Nothing)
                    If layer.Valid Then
                        pDataset = layer
                        If UCase(pDataset.BrowseName.Substring(pDataset.BrowseName.LastIndexOf(".") + 1)) = UCase(sName.Substring(sName.LastIndexOf(".") + 1)) Then
                            FindLayerDataFrame = pMxDoc.Maps.Item(f).Name
                            Exit For
                        End If
                    End If
                    layer = enumLayer.Next()
                Loop
            Next f
            'in case its in layout view and data view, need to grab focusmap
            Dim pAV As IActiveView
            pAV = My.ArcMap.Document.FocusMap
            pAV.Refresh()
            My.ArcMap.Document.UpdateContents()
        Catch ex As Exception
            Return False
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Function


    Shared Function ActivateLayerFrame(ByVal lyr As String) As Boolean
        ActivateLayerFrame = False
  
        Try
            'If TypeOf My.ArcMap.Document.ActiveView Is IPageLayout Then

            '    Try
            '        Dim pageLayout As IPageLayout = New PageLayoutClass()
            '        Dim pElement As ESRI.ArcGIS.Carto.IElement
            '        Dim HasParagraph As Boolean = False
            '        Dim pTextElement As ITextElement

            '        If TypeOf My.ArcMap.Document.ActiveView Is IPageLayout Then
            '            pageLayout = TryCast(My.ArcMap.Document.ActiveView, IPageLayout)

            '            Dim graphicsContainer As IGraphicsContainer = TryCast(pageLayout, IGraphicsContainer)

            '            'Loop through all the elements and move each one inch.
            '            graphicsContainer.Reset()

            '            pElement = graphicsContainer.Next
            '            While Not pElement Is Nothing
            '                If TypeOf pElement Is ITextElement Then
            '                    pTextElement = pElement
            '                    If UCase(Left(pTextElement.Text, 9)) = "PLOT DATE" Then
            '                        pTextElement.Text = "Plot Date: " & Format(Today, "MM/dd/yy")
            '                    End If
            '                End If
            '                pElement = graphicsContainer.Next
            '            End While

            '            My.ArcMap.Document.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

            '            'Change the modified flag
            '            Dim pDocDirty As IDocumentDirty
            '            pDocDirty = My.ArcMap.Document
            '            pDocDirty.SetDirty()
            '        End If

            '    Catch ex As Exception
            '        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            '        Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
            '    End Try
            'Else
            'end if
            Dim dFrame As String
            'if generic parcel search, then search for any of the parcel layers
            If lyr = "PARCELS" Then
                dFrame = FindLayerDataFrame("SANGIS.PARCELS_ALL_NPC")
                If dFrame = "None" Then
                    dFrame = FindLayerDataFrame("SANGIS.PARCELS_ALL")
                    If dFrame = "None" Then
                        MessageBox.Show("PARCELS_ALL_NPC OR PARCELS_ALL is not loaded in any Data Frame.  Please load before using this tool.")
                    Else
                        ActivateDataFrame(dFrame)
                        ActivateLayerFrame = True
                    End If
                Else
                    ActivateDataFrame(dFrame)
                    ActivateLayerFrame = True
                End If
            Else
                dFrame = FindLayerDataFrame(lyr)
                If dFrame = "None" Then
                    MessageBox.Show(lyr & " is not loaded in any Data Frame.  Please load before using this tool.")
                Else
                    ActivateDataFrame(dFrame)
                    ActivateLayerFrame = True
                End If
            End If
        Catch ex As Exception
            Return False
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try

    End Function

#End Region

#Region "Custom Subs and Functions"

    Shared Function NVL(ByVal DatabaseValue As Object) As String
        'check nulls
        If Not IsDBNull(DatabaseValue) Then
            NVL = DatabaseValue
        Else
            NVL = ""
        End If
    End Function

    Shared Function QueryFilterExtent(ByVal FLayer As IFeatureLayer, ByVal QFilter As IQueryFilter) _
           As IEnvelope
        QueryFilterExtent = Nothing
        Try
            Dim pEnv As IEnvelope
            Dim pFeatCursor As IFeatureCursor
            Dim pFeature As IFeature
            Dim ppntEnv As IEnvelope
            'Get the extent of the selected feature(s)
            pEnv = New Envelope
            pFeatCursor = FLayer.Search(QFilter, False)
            pFeature = pFeatCursor.NextFeature
            Do Until pFeature Is Nothing
                'if its a point, there is no good default envelope so have to code differently
                If pFeature.Shape.GeometryType = esriGeometryType.esriGeometryPoint Then
                    Dim pNwPoint As IPoint
                    pNwPoint = pFeature.Shape
                    ppntEnv = pFeature.Shape.Envelope
                    ppntEnv.Expand(100, 100, False)
                    ppntEnv.CenterAt(pNwPoint)
                    pEnv.Union(ppntEnv)
                Else
                    pEnv.Union(pFeature.Extent)
                End If
                pFeature = pFeatCursor.NextFeature
            Loop

            'Return the extent of the selected features, if able to calculate it
            If Not pEnv.IsEmpty Then
                QueryFilterExtent = pEnv
            Else
                MsgBox("Unable to determine the extent of the selected feature(s)", _
                       vbExclamation, "Layer Search Error")
                QueryFilterExtent = Nothing
            End If
            Exit Function

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Function

    Shared Function SelectedExtent(ByVal FLayer As IFeatureLayer) _
          As IEnvelope
        SelectedExtent = Nothing
        Try
            Dim pEnv As IEnvelope
            pEnv = New Envelope
            Dim ppntEnv As IEnvelope
            Dim pFeature As IFeature
            Dim featureSelection As ESRI.ArcGIS.Carto.IFeatureSelection = CType(FLayer, ESRI.ArcGIS.Carto.IFeatureSelection) ' Explicit Cast
            Dim selectionSet As ESRI.ArcGIS.Geodatabase.ISelectionSet = featureSelection.SelectionSet 'layer selection set
            Dim featureClass As ESRI.ArcGIS.Geodatabase.IFeatureClass = FLayer.FeatureClass 'layer featureclass
            Dim shapeField As System.String = featureClass.ShapeFieldName 'need the shapefield
            Dim cursor As ESRI.ArcGIS.Geodatabase.ICursor = Nothing
            'get a cursor of the selection set of the layer
            selectionSet.Search(Nothing, True, cursor)
            Dim featureCursor As ESRI.ArcGIS.Geodatabase.IFeatureCursor = CType(cursor, ESRI.ArcGIS.Geodatabase.IFeatureCursor) ' Explicit Cast
            pFeature = featureCursor.NextFeature
            'loop through the features adding in all envelopes to get the entire envelope
            Do Until pFeature Is Nothing
                If pFeature.Shape.GeometryType = esriGeometryType.esriGeometryPoint Then
                    Dim pNwPoint As IPoint
                    pNwPoint = pFeature.Shape
                    ppntEnv = pFeature.Shape.Envelope
                    ppntEnv.Expand(100, 100, False)
                    ppntEnv.CenterAt(pNwPoint)
                    pEnv.Union(ppntEnv)
                Else
                    pEnv.Union(pFeature.Extent)
                End If
                pFeature = featureCursor.NextFeature
            Loop

            'Return the extent of the selected features, if able to calculate it
            If Not pEnv.IsEmpty Then
                SelectedExtent = pEnv
            Else
                MsgBox("Unable to determine the extent of the selected feature(s)", _
                       vbExclamation, "Layer Search Error")
                SelectedExtent = Nothing
            End If
            Exit Function

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Function

    Shared Sub ZoomToEnvelope(ByVal pEnv As IEnvelope)
        If Not pEnv Is Nothing Then
            If pEnv.Width < 10000 Then
                pEnv.Expand(4, 4, True)
            Else
                'to avoid zooming out too far for long features
                pEnv.Expand(1.2, 1.2, True)
            End If
            'in case its in layout view and data view, need to grab focusmap
            Dim pAV As IActiveView
            pAV = My.ArcMap.Document.FocusMap
            pAV.Extent = pEnv
            pAV.Refresh()
        End If
        My.ArcMap.Document.UpdateContents()
    End Sub

    Shared Function ZoomToEnvelopeWait(ByVal pEnv As IEnvelope) As Boolean
        If Not pEnv Is Nothing Then
            If pEnv.Width < 10000 Then
                pEnv.Expand(4, 4, True)
            Else
                'to avoid zooming out too far for long features
                pEnv.Expand(1.2, 1.2, True)
            End If
            'in case its in layout view and data view, need to grab focusmap
            Dim pAV As IActiveView
            pAV = My.ArcMap.Document.FocusMap
            pAV.Extent = pEnv
            pAV.Refresh()
        End If
        My.ArcMap.Document.UpdateContents()
        Return True
    End Function

    Shared Sub FlashGeometry(ByVal FLayer As IFeatureLayer, ByVal QFilter As IQueryFilter) '(ByVal geometry As IGeometry, ByVal color As IRgbColor, ByVal display As IDisplay, ByVal delay As System.Int32)
        'used ArcGIS Snippet
        Dim pFeature As IFeature
        Dim featureSelection As ESRI.ArcGIS.Carto.IFeatureSelection = CType(FLayer, ESRI.ArcGIS.Carto.IFeatureSelection) ' Explicit Cast
        Dim selectionSet As ESRI.ArcGIS.Geodatabase.ISelectionSet = featureSelection.SelectionSet
        Dim featureClass As ESRI.ArcGIS.Geodatabase.IFeatureClass = FLayer.FeatureClass
        Dim shapeField As System.String = featureClass.ShapeFieldName

        Dim cursor As ESRI.ArcGIS.Geodatabase.ICursor = Nothing
        selectionSet.Search(QFilter, True, cursor)
        Dim featureCursor As ESRI.ArcGIS.Geodatabase.IFeatureCursor = CType(cursor, ESRI.ArcGIS.Geodatabase.IFeatureCursor) ' Explicit Cast
        Dim geometry As IGeometry
        Dim color As IRgbColor
        color = New RgbColor
        color.Green = 128
        Dim display As IScreenDisplay
        Dim delay As System.Int32
        display = My.ArcMap.Document.ActiveView.ScreenDisplay
        display.StartDrawing(display.hDC, CShort(esriScreenCache.esriNoScreenCache))
        delay = 200

        pFeature = featureCursor.NextFeature
        Do Until pFeature Is Nothing

            geometry = pFeature.Shape
            Select Case geometry.GeometryType
                Case esriGeometryType.esriGeometryPolygon

                    'Set the flash geometry's symbol.
                    Dim simpleFillSymbol As ISimpleFillSymbol = New SimpleFillSymbolClass
                    simpleFillSymbol.Color = color
                    Dim symbol As ISymbol = TryCast(simpleFillSymbol, ISymbol) ' Dynamic Cast
                    symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen

                    'Flash the input polygon geometry.
                    display.SetSymbol(symbol)
                    display.DrawPolygon(geometry)
                    System.Threading.Thread.Sleep(delay)
                    display.DrawPolygon(geometry)
                    Exit Select

                Case esriGeometryType.esriGeometryPolyline

                    'Set the flash geometry's symbol.
                    Dim simpleLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass
                    simpleLineSymbol.Width = 4
                    simpleLineSymbol.Color = color
                    Dim symbol As ISymbol = TryCast(simpleLineSymbol, ISymbol) ' Dynamic Cast
                    symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen

                    'Flash the input polyline geometry.
                    display.SetSymbol(symbol)
                    display.DrawPolyline(geometry)
                    System.Threading.Thread.Sleep(delay)
                    display.DrawPolyline(geometry)
                    Exit Select

                Case esriGeometryType.esriGeometryPoint

                    'Set the flash geometry's symbol.
                    Dim simpleMarkerSymbol As ISimpleMarkerSymbol = New SimpleMarkerSymbolClass
                    simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle
                    simpleMarkerSymbol.Size = 12
                    simpleMarkerSymbol.Color = color
                    Dim symbol As ISymbol = TryCast(simpleMarkerSymbol, ISymbol) ' Dynamic Cast
                    symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen

                    'Flash the input point geometry.
                    display.SetSymbol(symbol)
                    display.DrawPoint(geometry)
                    System.Threading.Thread.Sleep(delay)
                    display.DrawPoint(geometry)
                    Exit Select

                Case esriGeometryType.esriGeometryMultipoint

                    'Set the flash geometry's symbol.
                    Dim simpleMarkerSymbol As ISimpleMarkerSymbol = New SimpleMarkerSymbolClass
                    simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle
                    simpleMarkerSymbol.Size = 12
                    simpleMarkerSymbol.Color = color
                    Dim symbol As ISymbol = TryCast(simpleMarkerSymbol, ISymbol) ' Dynamic Cast
                    symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen

                    'Flash the input multipoint geometry.
                    display.SetSymbol(symbol)
                    display.DrawMultipoint(geometry)
                    System.Threading.Thread.Sleep(delay)
                    display.DrawMultipoint(geometry)
                    Exit Select

            End Select
            pFeature = featureCursor.NextFeature
        Loop
        display.FinishDrawing()
    End Sub

    Shared Sub FlashIt(ByVal FlashLayer As IFeatureLayer, ByVal QFilter As IQueryFilter) ' ByVal FlashGeometry As IGeometry, ByVal FlashFID As Long)
        Try
            'this sub will flash a feature (with FlashFID) in the specified layer using the specified geometry to select it
            Dim pID As IIdentify 'an object used to identify features in a layer with a geometry object
            Dim pIDArray As IArray 'an array of features ID'd by the FlashGeometry
            Dim pIDObj As IIdentifyObj, pFIDObj As IRowIdentifyObject, pIDFeature As IFeature   'the identified feature
            Dim i As Integer 'index for accessing identified features
            Dim pFeature As IFeature
            Dim featureSelection As ESRI.ArcGIS.Carto.IFeatureSelection = CType(FlashLayer, ESRI.ArcGIS.Carto.IFeatureSelection) ' Explicit Cast
            Dim selectionSet As ESRI.ArcGIS.Geodatabase.ISelectionSet = featureSelection.SelectionSet
            Dim FlashFID As Long
            Dim cursor As ESRI.ArcGIS.Geodatabase.ICursor = Nothing
            selectionSet.Search(QFilter, True, cursor)
            Dim featureCursor As ESRI.ArcGIS.Geodatabase.IFeatureCursor = CType(cursor, ESRI.ArcGIS.Geodatabase.IFeatureCursor) ' Explicit Cast
            Dim lFieldIndex As Integer

            pID = FlashLayer 'QI
            lFieldIndex = FlashLayer.FeatureClass.Fields.FindField("ObjectID")
            pFeature = featureCursor.NextFeature
            pIDArray = pID.Identify(pFeature.Shape)  'this geometry is used to identify the feature(s) to flash
            Do Until pFeature Is Nothing
                FlashFID = pFeature.Value(lFieldIndex)
                For i = 0 To pIDArray.Count - 1 'loop thru identified features, make sure to only flash the one with the specified FID
                    pIDObj = pIDArray.Element(i) 'get a feature that was identified (may only be one)
                    pFIDObj = pIDObj 'QI
                    pIDFeature = pFIDObj.Row 'get the ID'd feature
                    If pIDFeature.OID = FlashFID Then 'see if the FIDs match
                        pIDObj.Flash(My.ArcMap.Document.ActiveView.ScreenDisplay)  'm_pMxApp.Display   'flash it (pass in the application's display object as a required parameter)
                    End If
                Next i
                pFeature = featureCursor.NextFeature
            Loop
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

#End Region

End Class
