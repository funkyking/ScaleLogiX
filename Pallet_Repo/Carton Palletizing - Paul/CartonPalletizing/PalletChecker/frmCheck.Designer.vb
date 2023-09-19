<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCheck
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbSubGroup = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.WorkOrderBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PalletBox = New System.Windows.Forms.ComboBox()
        Me.CancelBtn = New System.Windows.Forms.Button()
        Me.ScanBtn = New System.Windows.Forms.Button()
        Me.scanstatuslbl = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtC5 = New System.Windows.Forms.TextBox()
        Me.txtS1 = New System.Windows.Forms.TextBox()
        Me.txtS5 = New System.Windows.Forms.TextBox()
        Me.Carton = New System.Windows.Forms.TextBox()
        Me.txtC4 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtS4 = New System.Windows.Forms.TextBox()
        Me.txtC3 = New System.Windows.Forms.TextBox()
        Me.txtS2 = New System.Windows.Forms.TextBox()
        Me.txtS3 = New System.Windows.Forms.TextBox()
        Me.txtC2 = New System.Windows.Forms.TextBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.serialNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cartonNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.statuslbl = New System.Windows.Forms.Label()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.lblError = New System.Windows.Forms.Label()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbSubGroup)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.WorkOrderBox)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.PalletBox)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 25)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(356, 129)
        Me.GroupBox2.TabIndex = 57
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Work Order"
        '
        'cmbSubGroup
        '
        Me.cmbSubGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSubGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSubGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSubGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSubGroup.FormattingEnabled = True
        Me.cmbSubGroup.Location = New System.Drawing.Point(101, 55)
        Me.cmbSubGroup.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbSubGroup.Name = "cmbSubGroup"
        Me.cmbSubGroup.Size = New System.Drawing.Size(233, 28)
        Me.cmbSubGroup.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(35, 58)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 20)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "MR No:"
        '
        'WorkOrderBox
        '
        Me.WorkOrderBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.WorkOrderBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.WorkOrderBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.WorkOrderBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WorkOrderBox.FormattingEnabled = True
        Me.WorkOrderBox.Location = New System.Drawing.Point(101, 23)
        Me.WorkOrderBox.Margin = New System.Windows.Forms.Padding(2)
        Me.WorkOrderBox.Name = "WorkOrderBox"
        Me.WorkOrderBox.Size = New System.Drawing.Size(233, 28)
        Me.WorkOrderBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(38, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 20)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "PO No:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(24, 89)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 20)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Pallet No:"
        '
        'PalletBox
        '
        Me.PalletBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PalletBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PalletBox.FormattingEnabled = True
        Me.PalletBox.Location = New System.Drawing.Point(101, 87)
        Me.PalletBox.Margin = New System.Windows.Forms.Padding(2)
        Me.PalletBox.Name = "PalletBox"
        Me.PalletBox.Size = New System.Drawing.Size(233, 28)
        Me.PalletBox.TabIndex = 2
        '
        'CancelBtn
        '
        Me.CancelBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CancelBtn.Image = Global.CartonPalletizing.My.Resources.Resources.cancel
        Me.CancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBtn.Location = New System.Drawing.Point(157, 206)
        Me.CancelBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.CancelBtn.Name = "CancelBtn"
        Me.CancelBtn.Size = New System.Drawing.Size(116, 42)
        Me.CancelBtn.TabIndex = 59
        Me.CancelBtn.Text = "Cancel"
        Me.CancelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CancelBtn.UseVisualStyleBackColor = True
        '
        'ScanBtn
        '
        Me.ScanBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ScanBtn.Image = Global.CartonPalletizing.My.Resources.Resources.scan1
        Me.ScanBtn.Location = New System.Drawing.Point(122, 166)
        Me.ScanBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.ScanBtn.Name = "ScanBtn"
        Me.ScanBtn.Size = New System.Drawing.Size(184, 50)
        Me.ScanBtn.TabIndex = 58
        Me.ScanBtn.Text = "Start Scanning"
        Me.ScanBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ScanBtn.UseVisualStyleBackColor = True
        '
        'scanstatuslbl
        '
        Me.scanstatuslbl.AutoSize = True
        Me.scanstatuslbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.scanstatuslbl.Location = New System.Drawing.Point(55, 178)
        Me.scanstatuslbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.scanstatuslbl.Name = "scanstatuslbl"
        Me.scanstatuslbl.Size = New System.Drawing.Size(258, 26)
        Me.scanstatuslbl.TabIndex = 60
        Me.scanstatuslbl.Text = "Scanning In Progress... ()"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnManual)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.txtC5)
        Me.GroupBox3.Controls.Add(Me.txtS1)
        Me.GroupBox3.Controls.Add(Me.txtS5)
        Me.GroupBox3.Controls.Add(Me.Carton)
        Me.GroupBox3.Controls.Add(Me.txtC4)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.txtS4)
        Me.GroupBox3.Controls.Add(Me.txtC3)
        Me.GroupBox3.Controls.Add(Me.txtS2)
        Me.GroupBox3.Controls.Add(Me.txtS3)
        Me.GroupBox3.Controls.Add(Me.txtC2)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 315)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(356, 265)
        Me.GroupBox3.TabIndex = 66
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Insert Serial Number "
        '
        'btnManual
        '
        Me.btnManual.Location = New System.Drawing.Point(275, 38)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(75, 23)
        Me.btnManual.TabIndex = 71
        Me.btnManual.Text = "Add"
        Me.btnManual.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(7, 16)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 20)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Serial No:"
        '
        'txtC5
        '
        Me.txtC5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtC5.Location = New System.Drawing.Point(149, 226)
        Me.txtC5.Margin = New System.Windows.Forms.Padding(2)
        Me.txtC5.Name = "txtC5"
        Me.txtC5.Size = New System.Drawing.Size(112, 26)
        Me.txtC5.TabIndex = 64
        '
        'txtS1
        '
        Me.txtS1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtS1.Location = New System.Drawing.Point(11, 38)
        Me.txtS1.Margin = New System.Windows.Forms.Padding(2)
        Me.txtS1.Name = "txtS1"
        Me.txtS1.Size = New System.Drawing.Size(112, 26)
        Me.txtS1.TabIndex = 8
        '
        'txtS5
        '
        Me.txtS5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtS5.Location = New System.Drawing.Point(11, 226)
        Me.txtS5.Margin = New System.Windows.Forms.Padding(2)
        Me.txtS5.Name = "txtS5"
        Me.txtS5.Size = New System.Drawing.Size(112, 26)
        Me.txtS5.TabIndex = 63
        '
        'Carton
        '
        Me.Carton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Carton.Location = New System.Drawing.Point(149, 38)
        Me.Carton.Margin = New System.Windows.Forms.Padding(2)
        Me.Carton.Name = "Carton"
        Me.Carton.Size = New System.Drawing.Size(112, 26)
        Me.Carton.TabIndex = 9
        '
        'txtC4
        '
        Me.txtC4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtC4.Location = New System.Drawing.Point(149, 179)
        Me.txtC4.Margin = New System.Windows.Forms.Padding(2)
        Me.txtC4.Name = "txtC4"
        Me.txtC4.Size = New System.Drawing.Size(112, 26)
        Me.txtC4.TabIndex = 62
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(145, 16)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 20)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "Carton:"
        '
        'txtS4
        '
        Me.txtS4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtS4.Location = New System.Drawing.Point(11, 179)
        Me.txtS4.Margin = New System.Windows.Forms.Padding(2)
        Me.txtS4.Name = "txtS4"
        Me.txtS4.Size = New System.Drawing.Size(112, 26)
        Me.txtS4.TabIndex = 61
        '
        'txtC3
        '
        Me.txtC3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtC3.Location = New System.Drawing.Point(149, 132)
        Me.txtC3.Margin = New System.Windows.Forms.Padding(2)
        Me.txtC3.Name = "txtC3"
        Me.txtC3.Size = New System.Drawing.Size(112, 26)
        Me.txtC3.TabIndex = 60
        '
        'txtS2
        '
        Me.txtS2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtS2.Location = New System.Drawing.Point(11, 85)
        Me.txtS2.Margin = New System.Windows.Forms.Padding(2)
        Me.txtS2.Name = "txtS2"
        Me.txtS2.Size = New System.Drawing.Size(112, 26)
        Me.txtS2.TabIndex = 57
        '
        'txtS3
        '
        Me.txtS3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtS3.Location = New System.Drawing.Point(11, 132)
        Me.txtS3.Margin = New System.Windows.Forms.Padding(2)
        Me.txtS3.Name = "txtS3"
        Me.txtS3.Size = New System.Drawing.Size(112, 26)
        Me.txtS3.TabIndex = 59
        '
        'txtC2
        '
        Me.txtC2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtC2.Location = New System.Drawing.Point(149, 85)
        Me.txtC2.Margin = New System.Windows.Forms.Padding(2)
        Me.txtC2.Name = "txtC2"
        Me.txtC2.Size = New System.Drawing.Size(112, 26)
        Me.txtC2.TabIndex = 58
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.serialNo, Me.cartonNo, Me.time})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView2.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView2.Location = New System.Drawing.Point(5, 18)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersWidth = 51
        Me.DataGridView2.RowTemplate.Height = 24
        Me.DataGridView2.Size = New System.Drawing.Size(471, 199)
        Me.DataGridView2.TabIndex = 67
        '
        'serialNo
        '
        Me.serialNo.HeaderText = "Serial No"
        Me.serialNo.Name = "serialNo"
        Me.serialNo.ReadOnly = True
        '
        'cartonNo
        '
        Me.cartonNo.HeaderText = "Carton"
        Me.cartonNo.Name = "cartonNo"
        Me.cartonNo.ReadOnly = True
        '
        'time
        '
        Me.time.HeaderText = "Time"
        Me.time.Name = "time"
        Me.time.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAdd)
        Me.GroupBox1.Controls.Add(Me.DataGridView2)
        Me.GroupBox1.Location = New System.Drawing.Point(425, 318)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(481, 262)
        Me.GroupBox1.TabIndex = 68
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "New Serial"
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(5, 222)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(470, 33)
        Me.btnAdd.TabIndex = 69
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblCount)
        Me.GroupBox4.Controls.Add(Me.btnDel)
        Me.GroupBox4.Controls.Add(Me.DataGridView1)
        Me.GroupBox4.Location = New System.Drawing.Point(425, 25)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(481, 287)
        Me.GroupBox4.TabIndex = 69
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Serial Number"
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(6, 21)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(38, 13)
        Me.lblCount.TabIndex = 69
        Me.lblCount.Text = "Count:"
        '
        'btnDel
        '
        Me.btnDel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDel.Location = New System.Drawing.Point(6, 240)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(470, 33)
        Me.btnDel.TabIndex = 68
        Me.btnDel.Text = "Delete"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.Location = New System.Drawing.Point(4, 41)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(471, 194)
        Me.DataGridView1.TabIndex = 67
        '
        'Timer1
        '
        '
        'statuslbl
        '
        Me.statuslbl.AutoSize = True
        Me.statuslbl.ForeColor = System.Drawing.Color.Red
        Me.statuslbl.Location = New System.Drawing.Point(217, 285)
        Me.statuslbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.statuslbl.Name = "statuslbl"
        Me.statuslbl.Size = New System.Drawing.Size(145, 13)
        Me.statuslbl.TabIndex = 70
        Me.statuslbl.Text = "00000000000000000000000"
        '
        'Timer3
        '
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ToolStrip1.Size = New System.Drawing.Size(939, 27)
        Me.ToolStrip1.TabIndex = 71
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.CartonPalletizing.My.Resources.Resources.help
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(56, 24)
        Me.ToolStripButton1.Text = "Help"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'Timer2
        '
        '
        'lblError
        '
        Me.lblError.AutoSize = True
        Me.lblError.ForeColor = System.Drawing.Color.Red
        Me.lblError.Location = New System.Drawing.Point(20, 285)
        Me.lblError.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(145, 13)
        Me.lblError.TabIndex = 72
        Me.lblError.Text = "00000000000000000000000"
        '
        'frmCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(939, 610)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.statuslbl)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.CancelBtn)
        Me.Controls.Add(Me.ScanBtn)
        Me.Controls.Add(Me.scanstatuslbl)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "frmCheck"
        Me.Text = "Start Checking"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents WorkOrderBox As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents PalletBox As ComboBox
    Friend WithEvents CancelBtn As Button
    Friend WithEvents ScanBtn As Button
    Friend WithEvents scanstatuslbl As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtC5 As TextBox
    Friend WithEvents txtS1 As TextBox
    Friend WithEvents txtS5 As TextBox
    Friend WithEvents Carton As TextBox
    Friend WithEvents txtC4 As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtS4 As TextBox
    Friend WithEvents txtC3 As TextBox
    Friend WithEvents txtS2 As TextBox
    Friend WithEvents txtS3 As TextBox
    Friend WithEvents txtC2 As TextBox
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Timer1 As Timer
    Friend WithEvents statuslbl As Label
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnDel As Button
    Friend WithEvents Timer3 As Timer
    Friend WithEvents serialNo As DataGridViewTextBoxColumn
    Friend WithEvents cartonNo As DataGridViewTextBoxColumn
    Friend WithEvents time As DataGridViewTextBoxColumn
    Friend WithEvents btnManual As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents lblCount As Label
    Friend WithEvents Timer2 As Timer
    Friend WithEvents cmbSubGroup As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblError As Label
End Class
