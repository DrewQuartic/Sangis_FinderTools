<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchParcelMulti
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearchParcelMulti))
        Me.btnUpdateFromMap = New System.Windows.Forms.Button()
        Me.lblStatusIssue = New System.Windows.Forms.Label()
        Me.btnClearAll = New System.Windows.Forms.Button()
        Me.btnZoomAll = New System.Windows.Forms.Button()
        Me.lstbxParcels = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblSelCnt = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAddToSel = New System.Windows.Forms.Button()
        Me.btnFindNew = New System.Windows.Forms.Button()
        Me.txtAPN = New System.Windows.Forms.TextBox()
        Me.btnDelSel = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboRDJur = New System.Windows.Forms.ComboBox()
        Me.cboRDSuf = New System.Windows.Forms.ComboBox()
        Me.txtRDName = New System.Windows.Forms.TextBox()
        Me.cboPreDir = New System.Windows.Forms.ComboBox()
        Me.txtHouseNum = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnUpdateFromMap
        '
        Me.btnUpdateFromMap.Image = CType(resources.GetObject("btnUpdateFromMap.Image"), System.Drawing.Image)
        Me.btnUpdateFromMap.Location = New System.Drawing.Point(174, 175)
        Me.btnUpdateFromMap.Name = "btnUpdateFromMap"
        Me.btnUpdateFromMap.Size = New System.Drawing.Size(18, 23)
        Me.btnUpdateFromMap.TabIndex = 5
        Me.btnUpdateFromMap.UseVisualStyleBackColor = True
        '
        'lblStatusIssue
        '
        Me.lblStatusIssue.AutoSize = True
        Me.lblStatusIssue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusIssue.ForeColor = System.Drawing.Color.Red
        Me.lblStatusIssue.Location = New System.Drawing.Point(33, 269)
        Me.lblStatusIssue.Name = "lblStatusIssue"
        Me.lblStatusIssue.Size = New System.Drawing.Size(139, 52)
        Me.lblStatusIssue.TabIndex = 32
        Me.lblStatusIssue.Text = "List and Map selection " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "counts do not Match! " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Use 'Clear' or 'Update' " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to Rese" & _
    "t."
        Me.lblStatusIssue.Visible = False
        '
        'btnClearAll
        '
        Me.btnClearAll.Image = CType(resources.GetObject("btnClearAll.Image"), System.Drawing.Image)
        Me.btnClearAll.Location = New System.Drawing.Point(79, 175)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(20, 23)
        Me.btnClearAll.TabIndex = 3
        Me.btnClearAll.UseVisualStyleBackColor = True
        '
        'btnZoomAll
        '
        Me.btnZoomAll.Enabled = False
        Me.btnZoomAll.Image = CType(resources.GetObject("btnZoomAll.Image"), System.Drawing.Image)
        Me.btnZoomAll.Location = New System.Drawing.Point(145, 175)
        Me.btnZoomAll.Name = "btnZoomAll"
        Me.btnZoomAll.Size = New System.Drawing.Size(17, 23)
        Me.btnZoomAll.TabIndex = 4
        Me.btnZoomAll.UseVisualStyleBackColor = True
        '
        'lstbxParcels
        '
        Me.lstbxParcels.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstbxParcels.FormattingEnabled = True
        Me.lstbxParcels.Location = New System.Drawing.Point(7, 237)
        Me.lstbxParcels.Name = "lstbxParcels"
        Me.lstbxParcels.Size = New System.Drawing.Size(216, 121)
        Me.lstbxParcels.Sorted = True
        Me.lstbxParcels.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Enter APN"
        '
        'lblSelCnt
        '
        Me.lblSelCnt.AutoSize = True
        Me.lblSelCnt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelCnt.Location = New System.Drawing.Point(123, 211)
        Me.lblSelCnt.Name = "lblSelCnt"
        Me.lblSelCnt.Size = New System.Drawing.Size(14, 13)
        Me.lblSelCnt.TabIndex = 27
        Me.lblSelCnt.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 211)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Total Parcels Found: "
        '
        'btnAddToSel
        '
        Me.btnAddToSel.Enabled = False
        Me.btnAddToSel.Image = CType(resources.GetObject("btnAddToSel.Image"), System.Drawing.Image)
        Me.btnAddToSel.Location = New System.Drawing.Point(41, 175)
        Me.btnAddToSel.Name = "btnAddToSel"
        Me.btnAddToSel.Size = New System.Drawing.Size(18, 23)
        Me.btnAddToSel.TabIndex = 2
        Me.btnAddToSel.UseVisualStyleBackColor = True
        '
        'btnFindNew
        '
        Me.btnFindNew.Enabled = False
        Me.btnFindNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFindNew.Image = CType(resources.GetObject("btnFindNew.Image"), System.Drawing.Image)
        Me.btnFindNew.Location = New System.Drawing.Point(16, 175)
        Me.btnFindNew.Name = "btnFindNew"
        Me.btnFindNew.Size = New System.Drawing.Size(18, 23)
        Me.btnFindNew.TabIndex = 1
        Me.btnFindNew.UseVisualStyleBackColor = True
        '
        'txtAPN
        '
        Me.txtAPN.Location = New System.Drawing.Point(79, 8)
        Me.txtAPN.Name = "txtAPN"
        Me.txtAPN.Size = New System.Drawing.Size(139, 20)
        Me.txtAPN.TabIndex = 0
        '
        'btnDelSel
        '
        Me.btnDelSel.Image = CType(resources.GetObject("btnDelSel.Image"), System.Drawing.Image)
        Me.btnDelSel.Location = New System.Drawing.Point(111, 175)
        Me.btnDelSel.Name = "btnDelSel"
        Me.btnDelSel.Size = New System.Drawing.Size(18, 23)
        Me.btnDelSel.TabIndex = 33
        Me.btnDelSel.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 138)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(28, 13)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "Juris"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Pre Dir"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 111)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Suffix"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "RoadName"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "House #"
        '
        'cboRDJur
        '
        Me.cboRDJur.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRDJur.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRDJur.FormattingEnabled = True
        Me.cboRDJur.ItemHeight = 13
        Me.cboRDJur.Location = New System.Drawing.Point(79, 137)
        Me.cboRDJur.Name = "cboRDJur"
        Me.cboRDJur.Size = New System.Drawing.Size(93, 21)
        Me.cboRDJur.TabIndex = 6
        Me.cboRDJur.TabStop = False
        '
        'cboRDSuf
        '
        Me.cboRDSuf.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRDSuf.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRDSuf.FormattingEnabled = True
        Me.cboRDSuf.ItemHeight = 13
        Me.cboRDSuf.Location = New System.Drawing.Point(79, 111)
        Me.cboRDSuf.Name = "cboRDSuf"
        Me.cboRDSuf.Size = New System.Drawing.Size(93, 21)
        Me.cboRDSuf.TabIndex = 5
        Me.cboRDSuf.TabStop = False
        '
        'txtRDName
        '
        Me.txtRDName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRDName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRDName.Location = New System.Drawing.Point(79, 85)
        Me.txtRDName.Name = "txtRDName"
        Me.txtRDName.Size = New System.Drawing.Size(139, 20)
        Me.txtRDName.TabIndex = 2
        '
        'cboPreDir
        '
        Me.cboPreDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPreDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPreDir.FormattingEnabled = True
        Me.cboPreDir.ItemHeight = 13
        Me.cboPreDir.Location = New System.Drawing.Point(79, 57)
        Me.cboPreDir.Name = "cboPreDir"
        Me.cboPreDir.Size = New System.Drawing.Size(93, 21)
        Me.cboPreDir.TabIndex = 3
        Me.cboPreDir.TabStop = False
        '
        'txtHouseNum
        '
        Me.txtHouseNum.Location = New System.Drawing.Point(79, 34)
        Me.txtHouseNum.Name = "txtHouseNum"
        Me.txtHouseNum.Size = New System.Drawing.Size(93, 20)
        Me.txtHouseNum.TabIndex = 1
        '
        'frmSearchParcelMulti
        '
        Me.AutoScroll = True
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboRDJur)
        Me.Controls.Add(Me.cboRDSuf)
        Me.Controls.Add(Me.txtRDName)
        Me.Controls.Add(Me.cboPreDir)
        Me.Controls.Add(Me.txtHouseNum)
        Me.Controls.Add(Me.btnDelSel)
        Me.Controls.Add(Me.txtAPN)
        Me.Controls.Add(Me.btnUpdateFromMap)
        Me.Controls.Add(Me.lblStatusIssue)
        Me.Controls.Add(Me.btnClearAll)
        Me.Controls.Add(Me.btnZoomAll)
        Me.Controls.Add(Me.lstbxParcels)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblSelCnt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAddToSel)
        Me.Controls.Add(Me.btnFindNew)
        Me.MaximumSize = New System.Drawing.Size(500, 0)
        Me.MinimumSize = New System.Drawing.Size(168, 200)
        Me.Name = "frmSearchParcelMulti"
        Me.Size = New System.Drawing.Size(234, 372)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnUpdateFromMap As System.Windows.Forms.Button
    Friend WithEvents lblStatusIssue As System.Windows.Forms.Label
    Friend WithEvents btnClearAll As System.Windows.Forms.Button
    Friend WithEvents btnZoomAll As System.Windows.Forms.Button
    Friend WithEvents lstbxParcels As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblSelCnt As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAddToSel As System.Windows.Forms.Button
    Friend WithEvents btnFindNew As System.Windows.Forms.Button
    Friend WithEvents txtAPN As System.Windows.Forms.TextBox
    Friend WithEvents btnDelSel As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboRDJur As System.Windows.Forms.ComboBox
    Friend WithEvents cboRDSuf As System.Windows.Forms.ComboBox
    Friend WithEvents txtRDName As System.Windows.Forms.TextBox
    Friend WithEvents cboPreDir As System.Windows.Forms.ComboBox
    Friend WithEvents txtHouseNum As System.Windows.Forms.TextBox

End Class
