Public Class frmHelp
    Public helptitle As String
    Private Sub frmHelp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim localprojectpath As String = IO.Directory.GetParent(IO.Directory.GetParent(Application.StartupPath.ToString).ToString).ToString
            If helptitle = "main" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "main.png")
            ElseIf helptitle = "access" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "access.png")

            ElseIf helptitle = "config" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "config.png")



            ElseIf helptitle = "partmas" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "partmas.png")
            ElseIf helptitle = "addpart" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "addpart.png")
            ElseIf helptitle = "updatepart" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "updatepart.png")


            ElseIf helptitle = "cusmas" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "cusmas.png")
            ElseIf helptitle = "addcus" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "addcus.png")
            ElseIf helptitle = "updatecus" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "updatecus.png")


            ElseIf helptitle = "modelmas" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "modelmas.png")
            ElseIf helptitle = "addmodel" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "addmodel.png")
            ElseIf helptitle = "updatemodel" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "updatemodel.png")


            ElseIf helptitle = "usermas" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "usermas.png")
            ElseIf helptitle = "adduser" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "adduser.png")
            ElseIf helptitle = "updateuser" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "updateuser.png")


            ElseIf helptitle = "workordermas" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "workordermas.png")
            ElseIf helptitle = "addmworkorder" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "addworkorder.png")
            ElseIf helptitle = "updateworkorder" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "updateworkorder.png")


            ElseIf helptitle = "order" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "order.png")
            ElseIf helptitle = "report" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "report.jpg")
            ElseIf helptitle = "prod" Then
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "prod.png")
            ElseIf helptitle = "check"
                PictureBox1.Image = Image.FromFile(localprojectpath & "\Help\" & "check.png")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class