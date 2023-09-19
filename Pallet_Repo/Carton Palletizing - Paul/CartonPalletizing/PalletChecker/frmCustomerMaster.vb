Imports System.Data.SqlClient

Public Class frmCustomerMaster
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)

    Private Sub frmCustomerMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub

    Private Function LoadGrid()
        Try
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            Dim SQLcmd = New SqlCommand
            Dim strsql = "Select [Index] As [No],
                                [Customer Name] As [Customer Name],
                                [Company Name] As [Company],
                                [Contact No] As [Contact No],
                                [Email] As [Email],
                                [Country] As [Country],
                                [Country Code] As [Country Code],
                                [Modified Date] As [Modified Date]
                      FROM [CRICUT].[CUPID].[CustomerMaster] WHERE [Delete]='False'"

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
                                [Customer Name] As [Customer Name],
                                [Company Name] As [Company],
                                [Contact No] As [Contact No],
                                [Email] As [Email],
                                [Country] As [Country],
                                [Country Code] As [Country Code],
                                [Modified Date] As [Modified Date]
                      FROM [CRICUT].[CUPID].[CustomerMaster] WHERE ([Index] LIKE '%" & TextBox1.Text & "%' OR [Customer Name] LIKE '%" & TextBox1.Text & "%' OR [Company Name] LIKE '%" & TextBox1.Text & "%'OR [Contact No] LIKE '%" & TextBox1.Text & "%'OR [Email] LIKE '%" & TextBox1.Text & "%' OR [Country] LIKE '%" & TextBox1.Text & "%' OR [Country Code] LIKE '%" & TextBox1.Text & "%' )AND [Delete]='False'"

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

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim f1 = New frmAddCustomer
        f1.Text = "Add New Customer"
        f1.ShowDialog()
        LoadGrid()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click, DataGridView1.CellContentDoubleClick
        Dim f1 = New frmAddCustomer
        f1.Text = "Update Customer"
        f1.ShowDialog()
        LoadGrid()
    End Sub


    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Dim res = MessageBox.Show("Confirm Delete Customer?" & vbCrLf & "Customer: " & DataGridView1.CurrentRow.Cells("Customer Name").Value.ToString & vbCrLf & "Company: " & DataGridView1.CurrentRow.Cells("Company").Value.ToString, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "UPDATE [CUPID].[CustomerMaster]
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

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        
        DataGridView1.Columns.Item("No").Width = 40
        DataGridView1.Columns.Item("Modified Date").MinimumWidth = 120
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim f1 = New frmHelp

        f1.helptitle = "cusmas"

        f1.Owner = Me
        f1.ShowDialog()
    End Sub
End Class