Imports System.Windows.Forms
Imports Sangis_FinderTools.Globals
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework

Public Class btnUpdateDate
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()
        Enabled = TypeOf My.ArcMap.Document.ActiveView Is IPageLayout
    End Sub

    Protected Overrides Sub OnClick()
        My.ArcMap.Application.CurrentTool = Nothing
        Try
            Dim pageLayout As IPageLayout = New PageLayoutClass()
            Dim pElement As ESRI.ArcGIS.Carto.IElement
            Dim HasParagraph As Boolean = False
            Dim pTextElement As ITextElement
            'Check if the current view is Layout View, not Data View
            If TypeOf My.ArcMap.Document.ActiveView Is IPageLayout Then
                'cast the active view to a page layout view
                pageLayout = TryCast(My.ArcMap.Document.ActiveView, IPageLayout)
                'cast tehe page layout to a graphics container to loop through everything in the layout view
                Dim graphicsContainer As IGraphicsContainer = TryCast(pageLayout, IGraphicsContainer)
                'Loop through all the elements in the view to find the text we are looking for.
                graphicsContainer.Reset()
                pElement = graphicsContainer.Next
                While Not pElement Is Nothing
                    'only grab the text items
                    If TypeOf pElement Is ITextElement Then
                        pTextElement = pElement
                        If UCase(Left(pTextElement.Text, 9)) = "PLOT DATE" Then
                            If pTextElement.Text = "Plot Date: " & Format(Today, "MM/dd/yy") Then
                                'nothing it already is up to date
                            Else
                                'update it
                                pTextElement.Text = "Plot Date: " & Format(Today, "MM/dd/yy")
                            End If
                        End If
                    End If
                    pElement = graphicsContainer.Next
                End While

               

                'Changed 03122012 to update the SanGIS logo to fill the frame
                SetDataframeExtenttoLayerExtent("SanGIS Logo and Motto", "SANGIS_MOTTO")

                'just refresh that area
                My.ArcMap.Document.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

                'Change the modified flag
                Dim pDocDirty As IDocumentDirty
                pDocDirty = My.ArcMap.Document
                pDocDirty.SetDirty()
            End If

        Catch ex As Exception
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show(ex.Source + " " + ex.Message + " " + ex.StackTrace + " ")
        End Try
    End Sub

    Protected Overrides Sub OnUpdate()
        'only enable if the view is layout view
        Enabled = My.ArcMap.Application IsNot Nothing And TypeOf My.ArcMap.Document.ActiveView Is IPageLayout
    End Sub
End Class
