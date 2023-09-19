Imports ZXing

Public Class Main

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GlobalVariables.MainInstance = Me
    End Sub


    'Adds item to the List of Barcode Items
    Private Sub addMasterBarcode_btn_Click(sender As Object, e As EventArgs) Handles addMasterBarcode_btn.Click
        Try
            'PictureBox1.Image = generateBarcode()
            newMasterItem()
        Catch ex As Exception
        End Try
    End Sub


    Private Function newMasterItem()
        Try
            'Create a new Master Display
            Dim SerialNumber = masterBarcode_txtbx.Text
            Dim masterItem = New BarcodeItem()
            masterItem.BarcodeImage = createBarcodeImage(SerialNumber)
            masterItem.BarcodeText = SerialNumber
            masterItem.PalletNo = GlobalVariables.palletcounter

            ' Create a new instance of your custom UserControl
            Dim listItem As New ListItem()
            listItem.BarcodeImage = masterItem.BarcodeImage
            listItem.PalletCount = GlobalVariables.palletcounter

            ' Add the UserControl to the FlowLayoutPanel
            FlowLayoutPanel1.Controls.Add(listItem)

            'Add the item the the barcode list
            GlobalVariables.MasterList.Add(masterItem)

            'Add Pallet Counter by 1 once adding the item
            GlobalVariables.palletcounter += 1


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function


    'Generates Barcode Image
    Private Function createBarcodeImage(ByVal input)
        Try
            Dim barcode = New BarcodeWriter()
            barcode.Format = BarcodeFormat.CODE_128
            Dim bimg = barcode.Write(input)
            Return bimg
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
