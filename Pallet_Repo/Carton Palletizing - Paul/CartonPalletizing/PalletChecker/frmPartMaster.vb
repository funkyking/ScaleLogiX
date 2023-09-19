Imports System.Data.SqlClient

Public Class frmPartMaster
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim localpath = (ReadValue("System", "localpath", IniPath))
    Dim arrsize = 0
    Dim imgstr(100) As String
    Dim pointer As Integer
    Dim imgpath As String

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim frm = New frmAddPartMaster
        frm.Text = "Add New Part"
        frm.ShowDialog()
        LoadGrid()
    End Sub

    Private Function LoadGrid()
        Try
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            Dim SQLcmd = New SqlCommand
            Dim strsql = "Select [Index] As [No],
                                [Part No] As [Part No],
                                [Part Name] As [Part Name],
                                [Description] As [Desc],
                                [Modified Date] As [Modified Date]
                                
                      FROM [CRICUT].[CUPID].[PartMaster] WHERE [Delete]='False'"

            Dim dt = New DataTable
            dt.Clear()
            Dim da = New SqlDataAdapter(strsql, Conn)
            da.Fill(dt)
            da.Dispose()
            DataGridView1.DataSource = dt
            DataGridView1.ClearSelection()
            'DataGridView1.RowTemplate.Height = 100

            Conn.Close()
        Catch ex As Exception

        End Try
    End Function

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click, DataGridView1.CellContentDoubleClick
        If DataGridView1.RowCount > 0 Then
            Dim frm = New frmAddPartMaster
            frm.Text = "Update Parts"
            frm.ShowDialog()
        Else
            MsgBox("There are no data available yet. Please try again later", MsgBoxStyle.Exclamation)
        End If
        LoadGrid()

    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Dim res = MessageBox.Show("Comfirm Delete Part Info?" & vbCrLf & "Part No:" & DataGridView1.CurrentRow.Cells("Part No").Value.ToString & vbCrLf & "Part Name:" & DataGridView1.CurrentRow.Cells("Part Name").Value.ToString, "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "UPDATE  [CRICUT].[CUPID].[PartMaster]
                                             SET [Delete] = 'True',
                                              [Modified Date] = '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                     WHERE [Index] = '" & DataGridView1.CurrentRow.Cells("No").Value.ToString & "'"
                SQLcmd.ExecuteNonQuery()
                Conn.Close()
            End If
            If System.IO.Directory.Exists(localpath & DataGridView1.CurrentRow.Cells("No").Value & "\") Then
                System.IO.Directory.Delete(localpath & DataGridView1.CurrentRow.Cells("No").Value & "\", True)
            End If

        Else
            Exit Sub
        End If
        LoadGrid()
    End Sub

    Private Sub frmPartMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            LoadGrid()
            Exit Sub
        End If
        Try
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            Dim SQLcmd = New SqlCommand
            Dim strsql = "Select [Index] As [No],
                                [Part No] As [Part No],
                                [Part Name] As [Part Name],
                                [Description] As [Desc],
                                [Modified Date] As [Modified Date]
                      FROM [CRICUT].[CUPID].[PartMaster] WHERE ([Index] LIKE '%" & TextBox1.Text & "%' OR [Part No] LIKE '%" & TextBox1.Text & "%' OR [Part Name] LIKE '%" & TextBox1.Text & "%') AND [Delete]='False'"

            Dim dt = New DataTable
            dt.Clear()
            Dim da = New SqlDataAdapter(strsql, Conn)
            da.Fill(dt)
            da.Dispose()
            DataGridView1.DataSource = dt
            DataGridView1.ClearSelection()
            'DataGridView1.RowTemplate.Height = 100

            Conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        arrsize = 0
        Array.Clear(imgstr, 0, imgstr.Length)
        PictureBox1.Image = Nothing
        PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
        Try
            Dim imagepath As String '= localpath & DataGridView1.CurrentRow.Cells("ID").Value.ToString & "\" & DataGridView1.CurrentRow.Cells("Image").Value
            Dim di As New System.IO.DirectoryInfo(localpath & "\" & DataGridView1.CurrentRow.Cells("Part Name").Value.ToString & "\")
            Dim i = 0
            For Each file In di.GetFiles()
                imgstr(i) = file.FullName
                i += 1
                arrsize += 1
            Next
            pointer = 0
            imgpath = imgstr(pointer)
            imagepath = imgstr(pointer)
            Dim ori = frmAddPartMaster.GetCopyImage(imagepath)
            Dim img As Image = frmAddPartMaster.ResizeImage(ori, New Size(PictureBox1.Width, PictureBox1.Height))
            PictureBox1.Image = img


        Catch ex As Exception
            PictureBox1.Image = Nothing
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles NextBtn.Click
        If arrsize <= 1 Then
            Exit Sub
        End If
        Dim imagepath As String
        If pointer >= arrsize - 1 Then
            pointer = 0
        Else
            pointer += 1
        End If
        imgpath = imgstr(pointer)
        imagepath = imgstr(pointer)
        Dim ori = frmAddPartMaster.GetCopyImage(imagepath)
        Dim img As Image = frmAddPartMaster.ResizeImage(ori, New Size(PictureBox1.Width, PictureBox1.Height))
        PictureBox1.Image = img
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        DataGridView1.Columns.Item("No").Width = 40

    End Sub

    Private Sub Prev_Click(sender As Object, e As EventArgs) Handles Prev.Click
        If arrsize <= 1 Then
            Exit Sub
        End If
        Dim imagepath As String
        If pointer <= 0 Then
            pointer = arrsize - 1
        Else
            pointer -= 1
        End If
        imgpath = imgstr(pointer)
        imagepath = imgstr(pointer)
        Dim ori = frmAddPartMaster.GetCopyImage(imagepath)
        Dim img As Image = frmAddPartMaster.ResizeImage(ori, New Size(PictureBox1.Width, PictureBox1.Height))
        PictureBox1.Image = img
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim f1 = New frmHelp

        f1.helptitle = "partmas"

        f1.Owner = Me
        f1.ShowDialog()
    End Sub


End Class