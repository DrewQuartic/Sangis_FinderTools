<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchRoadMulti
  Inherits System.Windows.Forms.UserControl

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearchRoadMulti))
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboRDJur = New System.Windows.Forms.ComboBox()
        Me.cboRDSuf = New System.Windows.Forms.ComboBox()
        Me.txtRDName = New System.Windows.Forms.TextBox()
        Me.cboPreDir = New System.Windows.Forms.ComboBox()
        Me.txtHouseNum = New System.Windows.Forms.TextBox()
        Me.txtRDID = New System.Windows.Forms.TextBox()
        Me.txtRDSegID = New System.Windows.Forms.TextBox()
        Me.lstbxRoad = New System.Windows.Forms.ListBox()
        Me.btnClearAll = New System.Windows.Forms.Button()
        Me.btnZoomAll = New System.Windows.Forms.Button()
        Me.btnAddToSel = New System.Windows.Forms.Button()
        Me.btnFindNew = New System.Windows.Forms.Button()
        Me.btnUpdateFromMap = New System.Windows.Forms.Button()
        Me.lblSelCnt = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblTotalSelCnt = New System.Windows.Forms.Label()
        Me.btnDelSel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 155)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(28, 13)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "Juris"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Pre Dir"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Suffix"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "RoadName"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "House #"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Road ID:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Seg ID:"
        '
        'cboRDJur
        '
        Me.cboRDJur.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRDJur.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRDJur.FormattingEnabled = True
        Me.cboRDJur.Location = New System.Drawing.Point(79, 154)
        Me.cboRDJur.Name = "cboRDJur"
        Me.cboRDJur.Size = New System.Drawing.Size(93, 21)
        Me.cboRDJur.TabIndex = 6
        '
        'cboRDSuf
        '
        Me.cboRDSuf.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRDSuf.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRDSuf.FormattingEnabled = True
        Me.cboRDSuf.Location = New System.Drawing.Point(79, 128)
        Me.cboRDSuf.Name = "cboRDSuf"
        Me.cboRDSuf.Size = New System.Drawing.Size(93, 21)
        Me.cboRDSuf.TabIndex = 5
        '
        'txtRDName
        '
        Me.txtRDName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRDName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRDName.Location = New System.Drawing.Point(90, 102)
        Me.txtRDName.Name = "txtRDName"
        Me.txtRDName.Size = New System.Drawing.Size(139, 20)
        Me.txtRDName.TabIndex = 4
        '
        'cboPreDir
        '
        Me.cboPreDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPreDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPreDir.FormattingEnabled = True
        Me.cboPreDir.Location = New System.Drawing.Point(79, 74)
        Me.cboPreDir.Name = "cboPreDir"
        Me.cboPreDir.Size = New System.Drawing.Size(93, 21)
        Me.cboPreDir.TabIndex = 3
        '
        'txtHouseNum
        '
        Me.txtHouseNum.Location = New System.Drawing.Point(79, 51)
        Me.txtHouseNum.Name = "txtHouseNum"
        Me.txtHouseNum.Size = New System.Drawing.Size(93, 20)
        Me.txtHouseNum.TabIndex = 2
        '
        'txtRDID
        '
        Me.txtRDID.Location = New System.Drawing.Point(79, 25)
        Me.txtRDID.Name = "txtRDID"
        Me.txtRDID.Size = New System.Drawing.Size(93, 20)
        Me.txtRDID.TabIndex = 1
        '
        'txtRDSegID
        '
        Me.txtRDSegID.Location = New System.Drawing.Point(79, 3)
        Me.txtRDSegID.Name = "txtRDSegID"
        Me.txtRDSegID.Size = New System.Drawing.Size(93, 20)
        Me.txtRDSegID.TabIndex = 0
        '
        'lstbxRoad
        '
        Me.lstbxRoad.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstbxRoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstbxRoad.FormattingEnabled = True
        Me.lstbxRoad.HorizontalScrollbar = True
        Me.lstbxRoad.Location = New System.Drawing.Point(6, 265)
        Me.lstbxRoad.Name = "lstbxRoad"
        Me.lstbxRoad.Size = New System.Drawing.Size(222, 56)
        Me.lstbxRoad.Sorted = True
        Me.lstbxRoad.TabIndex = 12
        '
        'btnClearAll
        '
        Me.btnClearAll.Image = CType(resources.GetObject("btnClearAll.Image"), System.Drawing.Image)
        Me.btnClearAll.Location = New System.Drawing.Point(92, 191)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(27, 23)
        Me.btnClearAll.TabIndex = 9
        Me.btnClearAll.UseVisualStyleBackColor = True
        '
        'btnZoomAll
        '
        Me.btnZoomAll.Enabled = False
        Me.btnZoomAll.Image = CType(resources.GetObject("btnZoomAll.Image"), System.Drawing.Image)
        Me.btnZoomAll.Location = New System.Drawing.Point(166, 191)
        Me.btnZoomAll.Name = "btnZoomAll"
        Me.btnZoomAll.Size = New System.Drawing.Size(29, 23)
        Me.btnZoomAll.TabIndex = 10
        Me.btnZoomAll.UseVisualStyleBackColor = True
        '
        'btnAddToSel
        '
        Me.btnAddToSel.Enabled = False
        Me.btnAddToSel.Image = CType(resources.GetObject("btnAddToSel.Image"), System.Drawing.Image)
        Me.btnAddToSel.Location = New System.Drawing.Point(46, 191)
        Me.btnAddToSel.Name = "btnAddToSel"
        Me.btnAddToSel.Size = New System.Drawing.Size(29, 23)
        Me.btnAddToSel.TabIndex = 8
        Me.btnAddToSel.UseVisualStyleBackColor = True
        '
        'btnFindNew
        '
        Me.btnFindNew.Enabled = False
        Me.btnFindNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFindNew.Image = CType(resources.GetObject("btnFindNew.Image"), System.Drawing.Image)
        Me.btnFindNew.Location = New System.Drawing.Point(9, 191)
        Me.btnFindNew.Name = "btnFindNew"
        Me.btnFindNew.Size = New System.Drawing.Size(31, 23)
        Me.btnFindNew.TabIndex = 7
        Me.btnFindNew.UseVisualStyleBackColor = True
        '
        'btnUpdateFromMap
        '
        Me.btnUpdateFromMap.Image = CType(resources.GetObject("btnUpdateFromMap.Image"), System.Drawing.Image)
        Me.btnUpdateFromMap.Location = New System.Drawing.Point(201, 191)
        Me.btnUpdateFromMap.Name = "btnUpdateFromMap"
        Me.btnUpdateFromMap.Size = New System.Drawing.Size(28, 23)
        Me.btnUpdateFromMap.TabIndex = 11
        Me.btnUpdateFromMap.UseVisualStyleBackColor = True
        '
        'lblSelCnt
        '
        Me.lblSelCnt.AutoSize = True
        Me.lblSelCnt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelCnt.Location = New System.Drawing.Point(89, 242)
        Me.lblSelCnt.Name = "lblSelCnt"
        Me.lblSelCnt.Size = New System.Drawing.Size(14, 13)
        Me.lblSelCnt.TabIndex = 38
        Me.lblSelCnt.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 242)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 13)
        Me.Label8.TabIndex = 37
        Me.Label8.Text = "Items in List: "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 220)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(102, 13)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "Segments Selected:"
        '
        'lblTotalSelCnt
        '
        Me.lblTotalSelCnt.AutoSize = True
        Me.lblTotalSelCnt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalSelCnt.Location = New System.Drawing.Point(146, 220)
        Me.lblTotalSelCnt.Name = "lblTotalSelCnt"
        Me.lblTotalSelCnt.Size = New System.Drawing.Size(14, 13)
        Me.lblTotalSelCnt.TabIndex = 40
        Me.lblTotalSelCnt.Text = "0"
        '
        'btnDelSel
        '
        Me.btnDelSel.Image = CType(resources.GetObject("btnDelSel.Image"), System.Drawing.Image)
        Me.btnDelSel.Location = New System.Drawing.Point(125, 191)
        Me.btnDelSel.Name = "btnDelSel"
        Me.btnDelSel.Size = New System.Drawing.Size(29, 23)
        Me.btnDelSel.TabIndex = 41
        Me.btnDelSel.UseVisualStyleBackColor = True
        '
        'frmSearchRoadMulti
        '
        Me.AutoScroll = True
        Me.Controls.Add(Me.btnDelSel)
        Me.Controls.Add(Me.lblTotalSelCnt)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblSelCnt)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnUpdateFromMap)
        Me.Controls.Add(Me.btnClearAll)
        Me.Controls.Add(Me.btnZoomAll)
        Me.Controls.Add(Me.btnAddToSel)
        Me.Controls.Add(Me.btnFindNew)
        Me.Controls.Add(Me.lstbxRoad)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboRDJur)
        Me.Controls.Add(Me.cboRDSuf)
        Me.Controls.Add(Me.txtRDName)
        Me.Controls.Add(Me.cboPreDir)
        Me.Controls.Add(Me.txtHouseNum)
        Me.Controls.Add(Me.txtRDID)
        Me.Controls.Add(Me.txtRDSegID)
        Me.MaximumSize = New System.Drawing.Size(500, 0)
        Me.MinimumSize = New System.Drawing.Size(235, 350)
        Me.Name = "frmSearchRoadMulti"
        Me.Size = New System.Drawing.Size(235, 350)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboRDJur As System.Windows.Forms.ComboBox
    Friend WithEvents cboRDSuf As System.Windows.Forms.ComboBox
    Friend WithEvents txtRDName As System.Windows.Forms.TextBox
    Friend WithEvents cboPreDir As System.Windows.Forms.ComboBox
    Friend WithEvents txtHouseNum As System.Windows.Forms.TextBox
    Friend WithEvents txtRDID As System.Windows.Forms.TextBox
    Friend WithEvents txtRDSegID As System.Windows.Forms.TextBox
    Friend WithEvents lstbxRoad As System.Windows.Forms.ListBox
    Friend WithEvents btnClearAll As System.Windows.Forms.Button
    Friend WithEvents btnZoomAll As System.Windows.Forms.Button
    Friend WithEvents btnAddToSel As System.Windows.Forms.Button
    Friend WithEvents btnFindNew As System.Windows.Forms.Button
    Friend WithEvents btnUpdateFromMap As System.Windows.Forms.Button
    Friend WithEvents lblSelCnt As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblTotalSelCnt As System.Windows.Forms.Label
    Friend WithEvents btnDelSel As System.Windows.Forms.Button

End Class
