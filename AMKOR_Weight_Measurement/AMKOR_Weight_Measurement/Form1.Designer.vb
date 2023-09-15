<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me._Date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Net = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.trNm_lbl = New System.Windows.Forms.Label()
        Me.txStatus_pbx = New System.Windows.Forms.PictureBox()
        Me.trBdr_lbl = New System.Windows.Forms.Label()
        Me.usSending_txtbx = New System.Windows.Forms.TextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.inNm_lbl = New System.Windows.Forms.Label()
        Me.rxStatus_pbx = New System.Windows.Forms.PictureBox()
        Me.inBdr_lbl = New System.Windows.Forms.Label()
        Me.usReceiving_txtbx = New System.Windows.Forms.TextBox()
        Me.receiver_Port = New System.IO.Ports.SerialPort(Me.components)
        Me.appStatus_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.transmitter_Port = New System.IO.Ports.SerialPort(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.allStatus_pbx = New System.Windows.Forms.PictureBox()
        Me.settings_btn = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txStatus_pbx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rxStatus_pbx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.allStatus_pbx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label6.Font = New System.Drawing.Font("Bahnschrift Condensed", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(384, 34)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Overview"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me._Date, Me.Time, Me.ID, Me.Net})
        Me.DataGridView1.Location = New System.Drawing.Point(445, 59)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidth = 62
        Me.DataGridView1.RowTemplate.Height = 28
        Me.DataGridView1.Size = New System.Drawing.Size(425, 616)
        Me.DataGridView1.TabIndex = 27
        '
        '_Date
        '
        Me._Date.HeaderText = "Date"
        Me._Date.MinimumWidth = 8
        Me._Date.Name = "_Date"
        Me._Date.ReadOnly = True
        '
        'Time
        '
        Me.Time.HeaderText = "Time"
        Me.Time.MinimumWidth = 8
        Me.Time.Name = "Time"
        Me.Time.ReadOnly = True
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.MinimumWidth = 8
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        '
        'Net
        '
        Me.Net.HeaderText = "Weight"
        Me.Net.MinimumWidth = 8
        Me.Net.Name = "Net"
        Me.Net.ReadOnly = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.ListBox1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Location = New System.Drawing.Point(22, 59)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(386, 617)
        Me.Panel1.TabIndex = 37
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.trNm_lbl)
        Me.GroupBox2.Controls.Add(Me.txStatus_pbx)
        Me.GroupBox2.Controls.Add(Me.trBdr_lbl)
        Me.GroupBox2.Controls.Add(Me.usSending_txtbx)
        Me.GroupBox2.Font = New System.Drawing.Font("Bahnschrift Condensed", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(16, 419)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(352, 171)
        Me.GroupBox2.TabIndex = 42
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Transmitting"
        '
        'trNm_lbl
        '
        Me.trNm_lbl.AutoSize = True
        Me.trNm_lbl.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trNm_lbl.Location = New System.Drawing.Point(19, 77)
        Me.trNm_lbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.trNm_lbl.Name = "trNm_lbl"
        Me.trNm_lbl.Size = New System.Drawing.Size(39, 19)
        Me.trNm_lbl.TabIndex = 55
        Me.trNm_lbl.Text = "Name"
        '
        'txStatus_pbx
        '
        Me.txStatus_pbx.BackColor = System.Drawing.Color.Transparent
        Me.txStatus_pbx.Image = Global.AMKOR_Weight_Measurement.My.Resources.Resources.No_Network
        Me.txStatus_pbx.Location = New System.Drawing.Point(76, 30)
        Me.txStatus_pbx.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txStatus_pbx.Name = "txStatus_pbx"
        Me.txStatus_pbx.Size = New System.Drawing.Size(23, 21)
        Me.txStatus_pbx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.txStatus_pbx.TabIndex = 36
        Me.txStatus_pbx.TabStop = False
        '
        'trBdr_lbl
        '
        Me.trBdr_lbl.AutoSize = True
        Me.trBdr_lbl.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trBdr_lbl.Location = New System.Drawing.Point(19, 96)
        Me.trBdr_lbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.trBdr_lbl.Name = "trBdr_lbl"
        Me.trBdr_lbl.Size = New System.Drawing.Size(58, 19)
        Me.trBdr_lbl.TabIndex = 56
        Me.trBdr_lbl.Text = "Baudrate"
        '
        'usSending_txtbx
        '
        Me.usSending_txtbx.BackColor = System.Drawing.Color.LightGray
        Me.usSending_txtbx.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.usSending_txtbx.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.usSending_txtbx.Location = New System.Drawing.Point(103, 30)
        Me.usSending_txtbx.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.usSending_txtbx.Multiline = True
        Me.usSending_txtbx.Name = "usSending_txtbx"
        Me.usSending_txtbx.ReadOnly = True
        Me.usSending_txtbx.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.usSending_txtbx.Size = New System.Drawing.Size(237, 129)
        Me.usSending_txtbx.TabIndex = 9
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.ListBox1.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.ForeColor = System.Drawing.Color.Lime
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 19
        Me.ListBox1.Location = New System.Drawing.Point(16, 61)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(353, 137)
        Me.ListBox1.TabIndex = 40
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.inNm_lbl)
        Me.GroupBox1.Controls.Add(Me.rxStatus_pbx)
        Me.GroupBox1.Controls.Add(Me.inBdr_lbl)
        Me.GroupBox1.Controls.Add(Me.usReceiving_txtbx)
        Me.GroupBox1.Font = New System.Drawing.Font("Bahnschrift Condensed", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(16, 233)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(352, 171)
        Me.GroupBox1.TabIndex = 41
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Incoming"
        '
        'inNm_lbl
        '
        Me.inNm_lbl.AutoSize = True
        Me.inNm_lbl.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inNm_lbl.Location = New System.Drawing.Point(19, 75)
        Me.inNm_lbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.inNm_lbl.Name = "inNm_lbl"
        Me.inNm_lbl.Size = New System.Drawing.Size(39, 19)
        Me.inNm_lbl.TabIndex = 40
        Me.inNm_lbl.Text = "Name"
        '
        'rxStatus_pbx
        '
        Me.rxStatus_pbx.BackColor = System.Drawing.Color.Transparent
        Me.rxStatus_pbx.Image = Global.AMKOR_Weight_Measurement.My.Resources.Resources.No_Network
        Me.rxStatus_pbx.Location = New System.Drawing.Point(76, 30)
        Me.rxStatus_pbx.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.rxStatus_pbx.Name = "rxStatus_pbx"
        Me.rxStatus_pbx.Size = New System.Drawing.Size(23, 21)
        Me.rxStatus_pbx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.rxStatus_pbx.TabIndex = 54
        Me.rxStatus_pbx.TabStop = False
        '
        'inBdr_lbl
        '
        Me.inBdr_lbl.AutoSize = True
        Me.inBdr_lbl.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inBdr_lbl.Location = New System.Drawing.Point(19, 95)
        Me.inBdr_lbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.inBdr_lbl.Name = "inBdr_lbl"
        Me.inBdr_lbl.Size = New System.Drawing.Size(58, 19)
        Me.inBdr_lbl.TabIndex = 41
        Me.inBdr_lbl.Text = "Baudrate"
        '
        'usReceiving_txtbx
        '
        Me.usReceiving_txtbx.BackColor = System.Drawing.Color.LightGray
        Me.usReceiving_txtbx.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.usReceiving_txtbx.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.usReceiving_txtbx.Location = New System.Drawing.Point(103, 30)
        Me.usReceiving_txtbx.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.usReceiving_txtbx.Multiline = True
        Me.usReceiving_txtbx.Name = "usReceiving_txtbx"
        Me.usReceiving_txtbx.ReadOnly = True
        Me.usReceiving_txtbx.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.usReceiving_txtbx.Size = New System.Drawing.Size(237, 129)
        Me.usReceiving_txtbx.TabIndex = 1
        '
        'receiver_Port
        '
        '
        'appStatus_Timer
        '
        Me.appStatus_Timer.Enabled = True
        Me.appStatus_Timer.Interval = 3000
        '
        'transmitter_Port
        '
        Me.transmitter_Port.BaudRate = 4800
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(841, 688)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 7)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Label1"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'allStatus_pbx
        '
        Me.allStatus_pbx.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.allStatus_pbx.BackColor = System.Drawing.Color.Transparent
        Me.allStatus_pbx.ErrorImage = Nothing
        Me.allStatus_pbx.Image = Global.AMKOR_Weight_Measurement.My.Resources.Resources.error_1
        Me.allStatus_pbx.Location = New System.Drawing.Point(408, 8)
        Me.allStatus_pbx.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.allStatus_pbx.Name = "allStatus_pbx"
        Me.allStatus_pbx.Size = New System.Drawing.Size(38, 36)
        Me.allStatus_pbx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.allStatus_pbx.TabIndex = 40
        Me.allStatus_pbx.TabStop = False
        '
        'settings_btn
        '
        Me.settings_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.settings_btn.BackColor = System.Drawing.Color.Transparent
        Me.settings_btn.FlatAppearance.BorderSize = 0
        Me.settings_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.settings_btn.Image = Global.AMKOR_Weight_Measurement.My.Resources.Resources.settings_Icon
        Me.settings_btn.Location = New System.Drawing.Point(834, 7)
        Me.settings_btn.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.settings_btn.Name = "settings_btn"
        Me.settings_btn.Size = New System.Drawing.Size(39, 44)
        Me.settings_btn.TabIndex = 39
        Me.settings_btn.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(887, 700)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.allStatus_pbx)
        Me.Controls.Add(Me.settings_btn)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScaleLogix"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txStatus_pbx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rxStatus_pbx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.allStatus_pbx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As Label
    Friend WithEvents _Date As DataGridViewTextBoxColumn
    Friend WithEvents Time As DataGridViewTextBoxColumn
    Friend WithEvents ID As DataGridViewTextBoxColumn
    Friend WithEvents Net As DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents settings_btn As Button
    Friend WithEvents receiver_Port As IO.Ports.SerialPort
    Friend WithEvents txStatus_pbx As PictureBox
    Friend WithEvents usReceiving_txtbx As TextBox
    Friend WithEvents usSending_txtbx As TextBox
    Friend WithEvents allStatus_pbx As PictureBox
    Friend WithEvents appStatus_Timer As Timer
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents inBdr_lbl As Label
    Friend WithEvents inNm_lbl As Label
    Friend WithEvents rxStatus_pbx As PictureBox
    Friend WithEvents trBdr_lbl As Label
    Friend WithEvents trNm_lbl As Label
    Friend WithEvents transmitter_Port As IO.Ports.SerialPort
    Friend WithEvents Label1 As Label
End Class
