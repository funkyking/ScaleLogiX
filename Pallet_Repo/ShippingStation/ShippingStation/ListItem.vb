Public Class ListItem

    Private tempid As String
    Public Property itemid As String
        Get
            Return tempid
        End Get
        Set(value As String)
            tempid = itemid
        End Set
    End Property

    Public Property PalletCount As String
        Get
            Return Label1.Text
        End Get
        Set(value As String)
            Label1.Text = $"Pallete No : {value}"
        End Set
    End Property

    Public Property BarcodeImage As Image
        Get
            Return PictureBox1.Image
        End Get
        Set(value As Image)
            PictureBox1.Image = value
        End Set
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            GlobalVariables.MasterList.RemoveAll(Function(item) item.BarcodeText = tempid)
            GlobalVariables.MainInstance.FlowLayoutPanel1.Controls.Remove(Me)

        Catch ex As Exception
        End Try
    End Sub
End Class
