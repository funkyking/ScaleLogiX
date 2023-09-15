<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Settings))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Main = New System.Windows.Forms.TabPage()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Restrt_btn = New System.Windows.Forms.Button()
        Me.clseApp_btn = New System.Windows.Forms.Button()
        Me.Debug = New System.Windows.Forms.TabPage()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.AutoRandom_btn = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.send_btn = New System.Windows.Forms.Button()
        Me.testRndm_btn = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.clrLogs_btn = New System.Windows.Forms.Button()
        Me.Port = New System.Windows.Forms.TabPage()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.handshake_cmbx = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dataBits_cmbx = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.stopBits_cmbx = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.parity_cmbx = New System.Windows.Forms.ComboBox()
        Me.reset_Btn = New System.Windows.Forms.Button()
        Me.Save_btn = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.bdrIn_cmbx = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.comIn_cmbx = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.comOut_cmbx = New System.Windows.Forms.ComboBox()
        Me.bdrOut_cmbx = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Misc = New System.Windows.Forms.TabPage()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.togglePorts_btn = New System.Windows.Forms.Button()
        Me.RandomLoopTimer = New System.Windows.Forms.Timer(Me.components)
        Me.clr_receiver = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.Main.SuspendLayout()
        Me.Debug.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Port.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Misc.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Main)
        Me.TabControl1.Controls.Add(Me.Debug)
        Me.TabControl1.Controls.Add(Me.Port)
        Me.TabControl1.Controls.Add(Me.Misc)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(478, 430)
        Me.TabControl1.TabIndex = 14
        '
        'Main
        '
        Me.Main.BackColor = System.Drawing.SystemColors.Control
        Me.Main.Controls.Add(Me.Label12)
        Me.Main.Controls.Add(Me.Label11)
        Me.Main.Controls.Add(Me.Restrt_btn)
        Me.Main.Controls.Add(Me.clseApp_btn)
        Me.Main.Location = New System.Drawing.Point(4, 22)
        Me.Main.Margin = New System.Windows.Forms.Padding(2)
        Me.Main.Name = "Main"
        Me.Main.Size = New System.Drawing.Size(470, 404)
        Me.Main.TabIndex = 2
        Me.Main.Text = "Main"
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label12.Location = New System.Drawing.Point(292, 380)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(170, 19)
        Me.Label12.TabIndex = 37
        Me.Label12.Text = "Closing Revokes Admin Access"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Lime
        Me.Label11.Location = New System.Drawing.Point(3, 380)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(195, 19)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "Minimize to Maintain Admin Access"
        '
        'Restrt_btn
        '
        Me.Restrt_btn.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Restrt_btn.Font = New System.Drawing.Font("Bahnschrift Condensed", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Restrt_btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Restrt_btn.Location = New System.Drawing.Point(169, 103)
        Me.Restrt_btn.Name = "Restrt_btn"
        Me.Restrt_btn.Size = New System.Drawing.Size(131, 67)
        Me.Restrt_btn.TabIndex = 34
        Me.Restrt_btn.Text = "Restart"
        Me.Restrt_btn.UseVisualStyleBackColor = False
        '
        'clseApp_btn
        '
        Me.clseApp_btn.BackColor = System.Drawing.Color.IndianRed
        Me.clseApp_btn.Font = New System.Drawing.Font("Bahnschrift Condensed", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clseApp_btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.clseApp_btn.Location = New System.Drawing.Point(169, 189)
        Me.clseApp_btn.Name = "clseApp_btn"
        Me.clseApp_btn.Size = New System.Drawing.Size(131, 67)
        Me.clseApp_btn.TabIndex = 33
        Me.clseApp_btn.Text = "Exit"
        Me.clseApp_btn.UseVisualStyleBackColor = False
        '
        'Debug
        '
        Me.Debug.BackColor = System.Drawing.SystemColors.Control
        Me.Debug.Controls.Add(Me.Label13)
        Me.Debug.Controls.Add(Me.Label14)
        Me.Debug.Controls.Add(Me.send_btn)
        Me.Debug.Controls.Add(Me.GroupBox6)
        Me.Debug.Controls.Add(Me.GroupBox5)
        Me.Debug.Controls.Add(Me.GroupBox3)
        Me.Debug.Location = New System.Drawing.Point(4, 22)
        Me.Debug.Margin = New System.Windows.Forms.Padding(2)
        Me.Debug.Name = "Debug"
        Me.Debug.Padding = New System.Windows.Forms.Padding(2)
        Me.Debug.Size = New System.Drawing.Size(470, 404)
        Me.Debug.TabIndex = 1
        Me.Debug.Text = "Debug"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label13.Location = New System.Drawing.Point(294, 378)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(170, 19)
        Me.Label13.TabIndex = 39
        Me.Label13.Text = "Closing Revokes Admin Access"
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Lime
        Me.Label14.Location = New System.Drawing.Point(5, 378)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(195, 19)
        Me.Label14.TabIndex = 38
        Me.Label14.Text = "Minimize to Maintain Admin Access"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.clr_receiver)
        Me.GroupBox6.Controls.Add(Me.TextBox2)
        Me.GroupBox6.Location = New System.Drawing.Point(144, 197)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox6.Size = New System.Drawing.Size(271, 166)
        Me.GroupBox6.TabIndex = 4
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Receiver"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(26, 26)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox2.Size = New System.Drawing.Size(223, 90)
        Me.TextBox2.TabIndex = 3
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.AutoRandom_btn)
        Me.GroupBox5.Controls.Add(Me.TextBox1)
        Me.GroupBox5.Controls.Add(Me.testRndm_btn)
        Me.GroupBox5.Location = New System.Drawing.Point(144, 11)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox5.Size = New System.Drawing.Size(271, 171)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Sending"
        '
        'AutoRandom_btn
        '
        Me.AutoRandom_btn.Location = New System.Drawing.Point(148, 128)
        Me.AutoRandom_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.AutoRandom_btn.Name = "AutoRandom_btn"
        Me.AutoRandom_btn.Size = New System.Drawing.Size(101, 25)
        Me.AutoRandom_btn.TabIndex = 3
        Me.AutoRandom_btn.Text = "Auto Loop"
        Me.AutoRandom_btn.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(26, 26)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(223, 90)
        Me.TextBox1.TabIndex = 3
        '
        'send_btn
        '
        Me.send_btn.Location = New System.Drawing.Point(33, 102)
        Me.send_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.send_btn.Name = "send_btn"
        Me.send_btn.Size = New System.Drawing.Size(59, 25)
        Me.send_btn.TabIndex = 1
        Me.send_btn.Text = "Send"
        Me.send_btn.UseVisualStyleBackColor = True
        Me.send_btn.Visible = False
        '
        'testRndm_btn
        '
        Me.testRndm_btn.Location = New System.Drawing.Point(26, 128)
        Me.testRndm_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.testRndm_btn.Name = "testRndm_btn"
        Me.testRndm_btn.Size = New System.Drawing.Size(101, 25)
        Me.testRndm_btn.TabIndex = 0
        Me.testRndm_btn.Text = "Send Random"
        Me.testRndm_btn.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.clrLogs_btn)
        Me.GroupBox3.Location = New System.Drawing.Point(11, 11)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Size = New System.Drawing.Size(113, 75)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Logs"
        '
        'clrLogs_btn
        '
        Me.clrLogs_btn.Location = New System.Drawing.Point(22, 26)
        Me.clrLogs_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.clrLogs_btn.Name = "clrLogs_btn"
        Me.clrLogs_btn.Size = New System.Drawing.Size(70, 25)
        Me.clrLogs_btn.TabIndex = 0
        Me.clrLogs_btn.Text = "Clear Logs"
        Me.clrLogs_btn.UseVisualStyleBackColor = True
        '
        'Port
        '
        Me.Port.BackColor = System.Drawing.SystemColors.Control
        Me.Port.Controls.Add(Me.Label15)
        Me.Port.Controls.Add(Me.Label16)
        Me.Port.Controls.Add(Me.GroupBox4)
        Me.Port.Controls.Add(Me.reset_Btn)
        Me.Port.Controls.Add(Me.Save_btn)
        Me.Port.Controls.Add(Me.GroupBox1)
        Me.Port.Controls.Add(Me.GroupBox2)
        Me.Port.Location = New System.Drawing.Point(4, 22)
        Me.Port.Margin = New System.Windows.Forms.Padding(2)
        Me.Port.Name = "Port"
        Me.Port.Padding = New System.Windows.Forms.Padding(2)
        Me.Port.Size = New System.Drawing.Size(470, 404)
        Me.Port.TabIndex = 0
        Me.Port.Text = "Port"
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label15.Location = New System.Drawing.Point(298, 380)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(170, 19)
        Me.Label15.TabIndex = 39
        Me.Label15.Text = "Closing Revokes Admin Access"
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Lime
        Me.Label16.Location = New System.Drawing.Point(9, 380)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(195, 19)
        Me.Label16.TabIndex = 38
        Me.Label16.Text = "Minimize to Maintain Admin Access"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.ComboBox1)
        Me.GroupBox4.Controls.Add(Me.handshake_cmbx)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.dataBits_cmbx)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.stopBits_cmbx)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.parity_cmbx)
        Me.GroupBox4.Location = New System.Drawing.Point(26, 179)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Size = New System.Drawing.Size(191, 191)
        Me.GroupBox4.TabIndex = 16
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Universal"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 97)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Parity Replace"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(92, 94)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(86, 21)
        Me.ComboBox1.TabIndex = 24
        '
        'handshake_cmbx
        '
        Me.handshake_cmbx.FormattingEnabled = True
        Me.handshake_cmbx.Location = New System.Drawing.Point(92, 153)
        Me.handshake_cmbx.Margin = New System.Windows.Forms.Padding(2)
        Me.handshake_cmbx.Name = "handshake_cmbx"
        Me.handshake_cmbx.Size = New System.Drawing.Size(86, 21)
        Me.handshake_cmbx.TabIndex = 22
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(14, 156)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Handshake"
        '
        'dataBits_cmbx
        '
        Me.dataBits_cmbx.FormattingEnabled = True
        Me.dataBits_cmbx.Location = New System.Drawing.Point(92, 36)
        Me.dataBits_cmbx.Margin = New System.Windows.Forms.Padding(2)
        Me.dataBits_cmbx.Name = "dataBits_cmbx"
        Me.dataBits_cmbx.Size = New System.Drawing.Size(86, 21)
        Me.dataBits_cmbx.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 39)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "DataBits"
        '
        'stopBits_cmbx
        '
        Me.stopBits_cmbx.FormattingEnabled = True
        Me.stopBits_cmbx.Location = New System.Drawing.Point(92, 124)
        Me.stopBits_cmbx.Margin = New System.Windows.Forms.Padding(2)
        Me.stopBits_cmbx.Name = "stopBits_cmbx"
        Me.stopBits_cmbx.Size = New System.Drawing.Size(86, 21)
        Me.stopBits_cmbx.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 127)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "StopBits"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 68)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Parity"
        '
        'parity_cmbx
        '
        Me.parity_cmbx.FormattingEnabled = True
        Me.parity_cmbx.Location = New System.Drawing.Point(92, 65)
        Me.parity_cmbx.Margin = New System.Windows.Forms.Padding(2)
        Me.parity_cmbx.Name = "parity_cmbx"
        Me.parity_cmbx.Size = New System.Drawing.Size(86, 21)
        Me.parity_cmbx.TabIndex = 8
        '
        'reset_Btn
        '
        Me.reset_Btn.BackColor = System.Drawing.Color.IndianRed
        Me.reset_Btn.FlatAppearance.BorderSize = 0
        Me.reset_Btn.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.reset_Btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.reset_Btn.Location = New System.Drawing.Point(356, 13)
        Me.reset_Btn.Margin = New System.Windows.Forms.Padding(2)
        Me.reset_Btn.Name = "reset_Btn"
        Me.reset_Btn.Size = New System.Drawing.Size(66, 44)
        Me.reset_Btn.TabIndex = 17
        Me.reset_Btn.Text = "Reset"
        Me.reset_Btn.UseVisualStyleBackColor = False
        '
        'Save_btn
        '
        Me.Save_btn.BackColor = System.Drawing.SystemColors.Highlight
        Me.Save_btn.FlatAppearance.BorderSize = 0
        Me.Save_btn.Font = New System.Drawing.Font("Bahnschrift Condensed", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Save_btn.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Save_btn.Location = New System.Drawing.Point(265, 247)
        Me.Save_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.Save_btn.Name = "Save_btn"
        Me.Save_btn.Size = New System.Drawing.Size(148, 87)
        Me.Save_btn.TabIndex = 16
        Me.Save_btn.Text = "Save Changes"
        Me.Save_btn.UseVisualStyleBackColor = False
        Me.Save_btn.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.bdrIn_cmbx)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.comIn_cmbx)
        Me.GroupBox1.Location = New System.Drawing.Point(26, 61)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(191, 114)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Input"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 36)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Baudrate"
        '
        'bdrIn_cmbx
        '
        Me.bdrIn_cmbx.FormattingEnabled = True
        Me.bdrIn_cmbx.Location = New System.Drawing.Point(92, 36)
        Me.bdrIn_cmbx.Margin = New System.Windows.Forms.Padding(2)
        Me.bdrIn_cmbx.Name = "bdrIn_cmbx"
        Me.bdrIn_cmbx.Size = New System.Drawing.Size(86, 21)
        Me.bdrIn_cmbx.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 69)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Com"
        '
        'comIn_cmbx
        '
        Me.comIn_cmbx.FormattingEnabled = True
        Me.comIn_cmbx.Location = New System.Drawing.Point(92, 69)
        Me.comIn_cmbx.Margin = New System.Windows.Forms.Padding(2)
        Me.comIn_cmbx.Name = "comIn_cmbx"
        Me.comIn_cmbx.Size = New System.Drawing.Size(86, 21)
        Me.comIn_cmbx.TabIndex = 6
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.comOut_cmbx)
        Me.GroupBox2.Controls.Add(Me.bdrOut_cmbx)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(248, 61)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(178, 114)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Output"
        '
        'comOut_cmbx
        '
        Me.comOut_cmbx.FormattingEnabled = True
        Me.comOut_cmbx.Location = New System.Drawing.Point(68, 69)
        Me.comOut_cmbx.Margin = New System.Windows.Forms.Padding(2)
        Me.comOut_cmbx.Name = "comOut_cmbx"
        Me.comOut_cmbx.Size = New System.Drawing.Size(83, 21)
        Me.comOut_cmbx.TabIndex = 9
        '
        'bdrOut_cmbx
        '
        Me.bdrOut_cmbx.FormattingEnabled = True
        Me.bdrOut_cmbx.Location = New System.Drawing.Point(69, 36)
        Me.bdrOut_cmbx.Margin = New System.Windows.Forms.Padding(2)
        Me.bdrOut_cmbx.Name = "bdrOut_cmbx"
        Me.bdrOut_cmbx.Size = New System.Drawing.Size(82, 21)
        Me.bdrOut_cmbx.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 36)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Baudrate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 69)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Com"
        '
        'Misc
        '
        Me.Misc.BackColor = System.Drawing.SystemColors.Control
        Me.Misc.Controls.Add(Me.Label17)
        Me.Misc.Controls.Add(Me.Label18)
        Me.Misc.Controls.Add(Me.togglePorts_btn)
        Me.Misc.Location = New System.Drawing.Point(4, 22)
        Me.Misc.Name = "Misc"
        Me.Misc.Size = New System.Drawing.Size(470, 404)
        Me.Misc.TabIndex = 3
        Me.Misc.Text = "Misc"
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label17.Location = New System.Drawing.Point(294, 379)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(170, 19)
        Me.Label17.TabIndex = 39
        Me.Label17.Text = "Closing Revokes Admin Access"
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Lime
        Me.Label18.Location = New System.Drawing.Point(5, 379)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(195, 19)
        Me.Label18.TabIndex = 38
        Me.Label18.Text = "Minimize to Maintain Admin Access"
        '
        'togglePorts_btn
        '
        Me.togglePorts_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.togglePorts_btn.AutoSize = True
        Me.togglePorts_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.togglePorts_btn.FlatAppearance.BorderSize = 0
        Me.togglePorts_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.togglePorts_btn.ForeColor = System.Drawing.Color.DimGray
        Me.togglePorts_btn.Image = Global.AMKOR_Weight_Measurement.My.Resources.Resources.Off_Icon
        Me.togglePorts_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.togglePorts_btn.Location = New System.Drawing.Point(167, 142)
        Me.togglePorts_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.togglePorts_btn.Name = "togglePorts_btn"
        Me.togglePorts_btn.Size = New System.Drawing.Size(131, 71)
        Me.togglePorts_btn.TabIndex = 32
        Me.togglePorts_btn.Text = "Force Open Ports"
        Me.togglePorts_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.togglePorts_btn.UseVisualStyleBackColor = False
        '
        'RandomLoopTimer
        '
        Me.RandomLoopTimer.Interval = 1000
        '
        'clr_receiver
        '
        Me.clr_receiver.Location = New System.Drawing.Point(81, 127)
        Me.clr_receiver.Margin = New System.Windows.Forms.Padding(2)
        Me.clr_receiver.Name = "clr_receiver"
        Me.clr_receiver.Size = New System.Drawing.Size(101, 25)
        Me.clr_receiver.TabIndex = 4
        Me.clr_receiver.Text = "Clear"
        Me.clr_receiver.UseVisualStyleBackColor = True
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(478, 430)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Settings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.TabControl1.ResumeLayout(False)
        Me.Main.ResumeLayout(False)
        Me.Main.PerformLayout()
        Me.Debug.ResumeLayout(False)
        Me.Debug.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.Port.ResumeLayout(False)
        Me.Port.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Misc.ResumeLayout(False)
        Me.Misc.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Port As TabPage
    Friend WithEvents reset_Btn As Button
    Friend WithEvents Save_btn As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents bdrIn_cmbx As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents comIn_cmbx As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents comOut_cmbx As ComboBox
    Friend WithEvents bdrOut_cmbx As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Debug As TabPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents clrLogs_btn As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents testRndm_btn As Button
    Friend WithEvents send_btn As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Main As TabPage
    Friend WithEvents AutoRandom_btn As Button
    Friend WithEvents RandomLoopTimer As Timer
    Friend WithEvents clseApp_btn As Button
    Friend WithEvents Restrt_btn As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents parity_cmbx As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents stopBits_cmbx As ComboBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Misc As TabPage
    Friend WithEvents togglePorts_btn As Button
    Friend WithEvents dataBits_cmbx As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents handshake_cmbx As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents clr_receiver As Button
End Class
