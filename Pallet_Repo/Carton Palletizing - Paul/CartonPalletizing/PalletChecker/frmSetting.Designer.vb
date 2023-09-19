<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetting
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Access = New System.Windows.Forms.TabPage()
        Me.cbxSkipCarton = New System.Windows.Forms.CheckBox()
        Me.HelpBtn = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Config = New System.Windows.Forms.TabPage()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.TabControl1.SuspendLayout()
        Me.Access.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Config.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Access)
        Me.TabControl1.Controls.Add(Me.Config)
        Me.TabControl1.Location = New System.Drawing.Point(9, 10)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(551, 335)
        Me.TabControl1.TabIndex = 1
        '
        'Access
        '
        Me.Access.Controls.Add(Me.cbxSkipCarton)
        Me.Access.Controls.Add(Me.HelpBtn)
        Me.Access.Controls.Add(Me.Button4)
        Me.Access.Controls.Add(Me.Button5)
        Me.Access.Controls.Add(Me.CheckBox5)
        Me.Access.Controls.Add(Me.CheckBox1)
        Me.Access.Controls.Add(Me.Label2)
        Me.Access.Controls.Add(Me.Label1)
        Me.Access.Controls.Add(Me.ComboBox1)
        Me.Access.Controls.Add(Me.Panel1)
        Me.Access.Location = New System.Drawing.Point(4, 22)
        Me.Access.Margin = New System.Windows.Forms.Padding(2)
        Me.Access.Name = "Access"
        Me.Access.Padding = New System.Windows.Forms.Padding(2)
        Me.Access.Size = New System.Drawing.Size(543, 309)
        Me.Access.TabIndex = 1
        Me.Access.Text = "Access"
        Me.Access.UseVisualStyleBackColor = True
        '
        'cbxSkipCarton
        '
        Me.cbxSkipCarton.AutoSize = True
        Me.cbxSkipCarton.Location = New System.Drawing.Point(227, 177)
        Me.cbxSkipCarton.Margin = New System.Windows.Forms.Padding(2)
        Me.cbxSkipCarton.Name = "cbxSkipCarton"
        Me.cbxSkipCarton.Size = New System.Drawing.Size(81, 17)
        Me.cbxSkipCarton.TabIndex = 10
        Me.cbxSkipCarton.Text = "Skip Carton"
        Me.cbxSkipCarton.UseVisualStyleBackColor = True
        '
        'HelpBtn
        '
        Me.HelpBtn.BackgroundImage = Global.CartonPalletizing.My.Resources.Resources.help
        Me.HelpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.HelpBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.HelpBtn.Location = New System.Drawing.Point(518, 5)
        Me.HelpBtn.Margin = New System.Windows.Forms.Padding(2)
        Me.HelpBtn.Name = "HelpBtn"
        Me.HelpBtn.Size = New System.Drawing.Size(24, 24)
        Me.HelpBtn.TabIndex = 9
        Me.HelpBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.HelpBtn.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Image = Global.CartonPalletizing.My.Resources.Resources.update
        Me.Button4.Location = New System.Drawing.Point(177, 217)
        Me.Button4.Margin = New System.Windows.Forms.Padding(2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(86, 35)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Restore Default"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Image = Global.CartonPalletizing.My.Resources.Resources.save
        Me.Button5.Location = New System.Drawing.Point(286, 217)
        Me.Button5.Margin = New System.Windows.Forms.Padding(2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(86, 35)
        Me.Button5.TabIndex = 6
        Me.Button5.Text = "Update"
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button5.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(326, 120)
        Me.CheckBox5.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(64, 17)
        Me.CheckBox5.TabIndex = 4
        Me.CheckBox5.Text = "Settings"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(167, 120)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(77, 17)
        Me.CheckBox1.TabIndex = 2
        Me.CheckBox1.Text = "Production"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(136, 67)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "User Level:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(317, 32)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "System Access Settings"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Admin", "Engineer", "Staff", "Technician"})
        Me.ComboBox1.Location = New System.Drawing.Point(201, 64)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(193, 21)
        Me.ComboBox1.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.CheckBox2)
        Me.Panel1.Location = New System.Drawing.Point(158, 102)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(236, 54)
        Me.Panel1.TabIndex = 4
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(87, 18)
        Me.CheckBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(58, 17)
        Me.CheckBox2.TabIndex = 3
        Me.CheckBox2.Text = "Master"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Config
        '
        Me.Config.Controls.Add(Me.Button6)
        Me.Config.Controls.Add(Me.Label5)
        Me.Config.Controls.Add(Me.Button3)
        Me.Config.Controls.Add(Me.Label4)
        Me.Config.Controls.Add(Me.Button1)
        Me.Config.Controls.Add(Me.Button2)
        Me.Config.Controls.Add(Me.TextBox1)
        Me.Config.Controls.Add(Me.TextBox2)
        Me.Config.Controls.Add(Me.Label3)
        Me.Config.Location = New System.Drawing.Point(4, 22)
        Me.Config.Margin = New System.Windows.Forms.Padding(2)
        Me.Config.Name = "Config"
        Me.Config.Padding = New System.Windows.Forms.Padding(2)
        Me.Config.Size = New System.Drawing.Size(543, 309)
        Me.Config.TabIndex = 2
        Me.Config.Text = "Configuration"
        Me.Config.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.BackgroundImage = Global.CartonPalletizing.My.Resources.Resources.help
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button6.Location = New System.Drawing.Point(518, 5)
        Me.Button6.Margin = New System.Windows.Forms.Padding(2)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(25, 24)
        Me.Button6.TabIndex = 12
        Me.Button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 11)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(398, 32)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "System Configuration Settings"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(458, 98)
        Me.Button3.Margin = New System.Windows.Forms.Padding(2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(56, 28)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Browse"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 77)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(153, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Local Directory Path for Image:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(314, 203)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(86, 35)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Restore Default"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = Global.CartonPalletizing.My.Resources.Resources.save
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.Location = New System.Drawing.Point(428, 203)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 35)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Update"
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(20, 102)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(431, 20)
        Me.TextBox1.TabIndex = 1
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(20, 165)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(494, 20)
        Me.TextBox2.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 140)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "SQL Connection String:"
        '
        'frmSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(566, 350)
        Me.Controls.Add(Me.TabControl1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmSetting"
        Me.Text = "frmSetting"
        Me.TabControl1.ResumeLayout(False)
        Me.Access.ResumeLayout(False)
        Me.Access.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Config.ResumeLayout(False)
        Me.Config.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Access As TabPage
    Friend WithEvents HelpBtn As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Config As TabPage
    Friend WithEvents Button6 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents cbxSkipCarton As CheckBox
End Class
