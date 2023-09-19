Imports System.Data.SqlClient


Public Class frmLineMaster
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)

    Private Sub frmLineMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub

    Private Function LoadGrid()
        Try
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            Dim SQLcmd = New SqlCommand
            Dim strsql = "Select [Index] As [No],
                               [Line] As [Line],
                                [Description] As [Desc],
                                [ModifiedDate] As [Modified Date]
                      FROM [CRICUT].[CUPID].[LineMaster] WHERE [Delete]='False'"

            Dim dt = New DataTable
            dt.Clear()
            Dim da = New SqlDataAdapter(strsql, Conn)
            da.Fill(dt)
            da.Dispose()
            DataGridView1.DataSource = dt
            DataGridView1.ClearSelection()
            Conn.Close()
        Catch ex As Exception

        End Try
    End Function

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim f1 = New frmAddLine
        f1.Text = "Add New Line"
        f1.ShowDialog()
        LoadGrid()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click, DataGridView1.CellContentDoubleClick
        Dim f1 = New frmAddLine
        f1.Text = "Update Line"
        f1.ShowDialog()
        LoadGrid()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        CheckQuery()
    End Sub

    Private Function CheckQuery()
        If TextBox1.Text = "" Then
            LoadGrid()
            Exit Function
        End If

        Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            Dim SQLcmd = New SqlCommand
        Dim strsql = "Select [Index] As [No],
                                [Line] As [Line],[Description] As [Desc],
                                [ModifiedDate] As [Modified Date]
                      FROM [CRICUT].[CUPID].[LineMaster] WHERE ([Index] LIKE '%" & TextBox1.Text & "%' OR [Line] LIKE '%" & TextBox1.Text & "%') AND [Delete]='False'"

        Dim dt = New DataTable
            dt.Clear()
            Dim da = New SqlDataAdapter(strsql, Conn)
            da.Fill(dt)
            da.Dispose()
            DataGridView1.DataSource = dt
            DataGridView1.ClearSelection()

            Conn.Close()

    End Function

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting

        DataGridView1.Columns.Item("No").Width = 40

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim f1 = New frmHelp

        f1.helptitle = "modelmas"

        f1.Owner = Me
        f1.ShowDialog()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Dim res = MessageBox.Show("Comfirm Delete Part Info?" & vbCrLf & "Line No: " & DataGridView1.CurrentRow.Cells("No").Value.ToString & vbCrLf & "Line: " & DataGridView1.CurrentRow.Cells("Line").Value.ToString, "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "UPDATE  [CRICUT].[CUPID].[LineMaster]
                                             SET [Delete] = 'True',
                                              [ModifiedDate] = '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                     WHERE [Index] = '" & DataGridView1.CurrentRow.Cells("No").Value.ToString & "'"
                SQLcmd.ExecuteNonQuery()
                Conn.Close()
            End If
        End If
        LoadGrid()
    End Sub
End Class