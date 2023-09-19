<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfirmOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfirmOrder))
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Address = New System.Windows.Forms.RichTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CountryCode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CustomerBox = New System.Windows.Forms.ComboBox()
        Me.WorkOrder = New System.Windows.Forms.Label()
        Me.PalletNo = New System.Windows.Forms.Label()
        Me.Shift = New System.Windows.Forms.Label()
        Me.PartNo = New System.Windows.Forms.Label()
        Me.Quantity = New System.Windows.Forms.Label()
        Me.Model = New System.Windows.Forms.Label()
        Me.PrintOrderBtn = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Line = New System.Windows.Forms.TextBox()
        Me.CancelBtn = New System.Windows.Forms.Button()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Century Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(296, 47)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Confirm Order"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(25, 115)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 17)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Pallet No:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(491, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 17)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "Part No:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(486, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 17)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Quantity:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(494, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 17)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Model:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(55, 160)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 17)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Shift:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 17)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Work Order:"
        '
        'Address
        '
        Me.Address.Location = New System.Drawing.Point(101, 256)
        Me.Address.Name = "Address"
        Me.Address.Size = New System.Drawing.Size(714, 111)
        Me.Address.TabIndex = 49
        Me.Address.Text = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(25, 259)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 17)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Address:"
        '
        'CountryCode
        '
        Me.CountryCode.Location = New System.Drawing.Point(561, 201)
        Me.CountryCode.Name = "CountryCode"
        Me.CountryCode.Size = New System.Drawing.Size(254, 22)
        Me.CountryCode.TabIndex = 52
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(446, 201)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(98, 17)
        Me.Label9.TabIndex = 53
        Me.Label9.Text = "Country Code:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(25, 201)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 17)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "Customer:"
        '
        'CustomerBox
        '
        Me.CustomerBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CustomerBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CustomerBox.FormattingEnabled = True
        Me.CustomerBox.Location = New System.Drawing.Point(104, 201)
        Me.CustomerBox.Name = "CustomerBox"
        Me.CustomerBox.Size = New System.Drawing.Size(306, 24)
        Me.CustomerBox.TabIndex = 56
        '
        'WorkOrder
        '
        Me.WorkOrder.AutoSize = True
        Me.WorkOrder.Location = New System.Drawing.Point(101, 78)
        Me.WorkOrder.Name = "WorkOrder"
        Me.WorkOrder.Size = New System.Drawing.Size(78, 17)
        Me.WorkOrder.TabIndex = 57
        Me.WorkOrder.Text = "WorkOrder"
        '
        'PalletNo
        '
        Me.PalletNo.AutoSize = True
        Me.PalletNo.Location = New System.Drawing.Point(100, 115)
        Me.PalletNo.Name = "PalletNo"
        Me.PalletNo.Size = New System.Drawing.Size(61, 17)
        Me.PalletNo.TabIndex = 58
        Me.PalletNo.Text = "PalletNo"
        '
        'Shift
        '
        Me.Shift.AutoSize = True
        Me.Shift.Location = New System.Drawing.Point(101, 160)
        Me.Shift.Name = "Shift"
        Me.Shift.Size = New System.Drawing.Size(36, 17)
        Me.Shift.TabIndex = 59
        Me.Shift.Text = "Shift"
        '
        'PartNo
        '
        Me.PartNo.AutoSize = True
        Me.PartNo.Location = New System.Drawing.Point(558, 78)
        Me.PartNo.Name = "PartNo"
        Me.PartNo.Size = New System.Drawing.Size(52, 17)
        Me.PartNo.TabIndex = 60
        Me.PartNo.Text = "PartNo"
        '
        'Quantity
        '
        Me.Quantity.AutoSize = True
        Me.Quantity.Location = New System.Drawing.Point(558, 117)
        Me.Quantity.Name = "Quantity"
        Me.Quantity.Size = New System.Drawing.Size(61, 17)
        Me.Quantity.TabIndex = 61
        Me.Quantity.Text = "Quantity"
        '
        'Model
        '
        Me.Model.AutoSize = True
        Me.Model.Location = New System.Drawing.Point(560, 160)
        Me.Model.Name = "Model"
        Me.Model.Size = New System.Drawing.Size(46, 17)
        Me.Model.TabIndex = 62
        Me.Model.Text = "Model"
        '
        'PrintOrderBtn
        '
        Me.PrintOrderBtn.Image = Global.CartonPalletizing.My.Resources.Resources.print
        Me.PrintOrderBtn.Location = New System.Drawing.Point(242, 432)
        Me.PrintOrderBtn.Name = "PrintOrderBtn"
        Me.PrintOrderBtn.Size = New System.Drawing.Size(168, 52)
        Me.PrintOrderBtn.TabIndex = 51
        Me.PrintOrderBtn.Text = "Print Order"
        Me.PrintOrderBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrintOrderBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.PrintOrderBtn.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(50, 389)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(39, 17)
        Me.Label11.TabIndex = 64
        Me.Label11.Text = "Line:"
        '
        'Line
        '
        Me.Line.Location = New System.Drawing.Point(101, 389)
        Me.Line.Name = "Line"
        Me.Line.Size = New System.Drawing.Size(714, 22)
        Me.Line.TabIndex = 63
        '
        'CancelBtn
        '
        Me.CancelBtn.Image = Global.CartonPalletizing.My.Resources.Resources.cancel
        Me.CancelBtn.Location = New System.Drawing.Point(442, 432)
        Me.CancelBtn.Name = "CancelBtn"
        Me.CancelBtn.Size = New System.Drawing.Size(168, 52)
        Me.CancelBtn.TabIndex = 65
        Me.CancelBtn.Text = "Cancel"
        Me.CancelBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CancelBtn.UseVisualStyleBackColor = True
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton4})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ToolStrip2.Size = New System.Drawing.Size(838, 27)
        Me.ToolStrip2.TabIndex = 66
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Image = Global.CartonPalletizing.My.Resources.Resources.help
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(65, 24)
        Me.ToolStripButton4.Text = "Help"
        Me.ToolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'frmConfirmOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(838, 501)
        Me.Controls.Add(Me.ToolStrip2)
        Me.Controls.Add(Me.CancelBtn)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Line)
        Me.Controls.Add(Me.Model)
        Me.Controls.Add(Me.Quantity)
        Me.Controls.Add(Me.PartNo)
        Me.Controls.Add(Me.Shift)
        Me.Controls.Add(Me.PalletNo)
        Me.Controls.Add(Me.WorkOrder)
        Me.Controls.Add(Me.CustomerBox)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CountryCode)
        Me.Controls.Add(Me.PrintOrderBtn)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Address)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmConfirmOrder"
        Me.Text = "Confirm Order"
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Address As RichTextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents PrintOrderBtn As Button
    Friend WithEvents CountryCode As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents CustomerBox As ComboBox
    Friend WithEvents WorkOrder As Label
    Friend WithEvents PalletNo As Label
    Friend WithEvents Shift As Label
    Friend WithEvents PartNo As Label
    Friend WithEvents Quantity As Label
    Friend WithEvents Model As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Line As TextBox
    Friend WithEvents CancelBtn As Button
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents ToolStripButton4 As ToolStripButton
End Class
