Imports System.Data.SqlClient


Public Class frmUserMaster
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Private Sub frmUserMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub

    Private Function LoadGrid()
        Try
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            Dim SQLcmd = New SqlCommand
            Dim strsql = "Select [Index] As [No],
                                [User ID] As [User ID],
                                [User Name] As [User Name],
                                [User Level] As [User Level],
                                [Modified Date] As [Modified Date]
                      FROM [CRICUT].[CUPID].[UserMaster] WHERE [Delete]='False'"

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
            MsgBox(ex.ToString)
        End Try
    End Function

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click

        frmAddUser.Text = "Add New User"
        frmAddUser.ShowDialog()

        LoadGrid()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click, DataGridView1.CellContentDoubleClick
        If DataGridView1.RowCount > 0 Then
            frmAddUser.Text = "Update User"
            frmAddUser.ShowDialog()
        Else
            MsgBox("There are no data available yet. Please try again later", MsgBoxStyle.Exclamation)
        End If
        LoadGrid()
    End Sub
    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting

        DataGridView1.Columns.Item("No").Width = 40
        DataGridView1.Columns.Item("Modified Date").MinimumWidth = 120
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim f1 = New frmHelp

        f1.helptitle = "usermas"
        f1.Owner = Me
        f1.ShowDialog()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        CheckQuery()
    End Sub

    Private Function CheckQuery()
        Dim cond As String
        If TextBox1.Text = "" Then
            LoadGrid()
            Exit Function
        Else
            cond = "AND ([Index] LIKE '%" & TextBox1.Text & "%' OR [User ID] LIKE '%" & TextBox1.Text & "%' OR [User Name] LIKE '%" & TextBox1.Text & "%' OR [User Level] LIKE '%" & TextBox1.Text & "%')"
        End If
        Dim strsql = "Select    [Index] As [No],
                                [User ID] As [User ID],
                                [User Name] As [User Name],
                                [User Level] As [User Level],
                                 [Modified Date] As [Modified Date] 
                      FROM [CRICUT].[CUPID].[UserMaster] WHERE [Delete]='False'" & cond


        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim dt = New DataTable
        dt.Clear()
        Dim da = New SqlDataAdapter(strsql, Conn)
        da.Fill(dt)
        da.Dispose()
        DataGridView1.DataSource = dt
        DataGridView1.ClearSelection()
    End Function

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Dim res = MessageBox.Show("Confirm Delete User?" & vbCrLf & "User: " & DataGridView1.CurrentRow.Cells("User Name").Value.ToString, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "UPDATE [CRICUT].[CUPID].[UserMaster]
                                   SET
                                        [Delete] = 'True'
                                      ,[Modified Date] =  '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                 WHERE [Index]='" & DataGridView1.CurrentRow.Cells("No").Value.ToString & "'"
                SQLcmd.ExecuteNonQuery()
                Conn.Close()
            End If
        Else
            Exit Sub
        End If
        LoadGrid()
    End Sub
End Class