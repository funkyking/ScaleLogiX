<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.create_btn = New System.Windows.Forms.Button()
        Me.masterBarcode_txtbx = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.containerNo_txtbx = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.clrMasterBarcodeTxtbx_btn = New System.Windows.Forms.Button()
        Me.addMasterBarcode_btn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'create_btn
        '
        Me.create_btn.Location = New System.Drawing.Point(23, 330)
        Me.create_btn.Name = "create_btn"
        Me.create_btn.Size = New System.Drawing.Size(281, 44)
        Me.create_btn.TabIndex = 0
        Me.create_btn.Text = "Create"
        Me.create_btn.UseVisualStyleBackColor = True
        '
        'masterBarcode_txtbx
        '
        Me.masterBarcode_txtbx.Location = New System.Drawing.Point(26, 162)
        Me.masterBarcode_txtbx.Name = "masterBarcode_txtbx"
        Me.masterBarcode_txtbx.Size = New System.Drawing.Size(209, 30)
        Me.masterBarcode_txtbx.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 23)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Master Barcode"
        '
        'containerNo_txtbx
        '
        Me.containerNo_txtbx.Location = New System.Drawing.Point(26, 85)
        Me.containerNo_txtbx.Name = "containerNo_txtbx"
        Me.containerNo_txtbx.Size = New System.Drawing.Size(209, 30)
        Me.containerNo_txtbx.TabIndex = 6
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.clrMasterBarcodeTxtbx_btn)
        Me.GroupBox2.Controls.Add(Me.addMasterBarcode_btn)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.containerNo_txtbx)
        Me.GroupBox2.Controls.Add(Me.masterBarcode_txtbx)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(23, 55)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(309, 251)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Input"
        '
        'clrMasterBarcodeTxtbx_btn
        '
        Me.clrMasterBarcodeTxtbx_btn.Location = New System.Drawing.Point(241, 200)
        Me.clrMasterBarcodeTxtbx_btn.Name = "clrMasterBarcodeTxtbx_btn"
        Me.clrMasterBarcodeTxtbx_btn.Size = New System.Drawing.Size(55, 35)
        Me.clrMasterBarcodeTxtbx_btn.TabIndex = 9
        Me.clrMasterBarcodeTxtbx_btn.Text = "Clear"
        Me.clrMasterBarcodeTxtbx_btn.UseVisualStyleBackColor = True
        '
        'addMasterBarcode_btn
        '
        Me.addMasterBarcode_btn.Location = New System.Drawing.Point(241, 159)
        Me.addMasterBarcode_btn.Name = "addMasterBarcode_btn"
        Me.addMasterBarcode_btn.Size = New System.Drawing.Size(55, 35)
        Me.addMasterBarcode_btn.TabIndex = 6
        Me.addMasterBarcode_btn.Text = "Add"
        Me.addMasterBarcode_btn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 23)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Container No."
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.FlowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(367, 70)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(338, 236)
        Me.FlowLayoutPanel1.TabIndex = 6
        Me.FlowLayoutPanel1.WrapContents = False
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 386)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.create_btn)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Bahnschrift SemiBold Condensed", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "Main"
        Me.Text = "Cricut Shipping"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents create_btn As Button
    Friend WithEvents masterBarcode_txtbx As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents containerNo_txtbx As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents clrMasterBarcodeTxtbx_btn As Button
    Friend WithEvents addMasterBarcode_btn As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
End Class
