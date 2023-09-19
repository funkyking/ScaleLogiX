Imports System.Data.SqlClient


Public Class frmModelMaster
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim localpath = ReadValue("System", "localpath", IniPath)

    Private Sub frmModelMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub

    Private Function LoadGrid()
        Try
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            Dim SQLcmd = New SqlCommand
            Dim strsql = "Select [Index] As [No],
                               [Model] As [Model],
                                [Bin] As [Bin],
                                [Suffix] As [Suffix],
                                [BarcodeLength] As [BarcodeLength],
                                [Description] As [Desc],
                                [Modified Date] As [Modified Date]
                      FROM [CRICUT].[CUPID].[ModelMaster] WHERE [Delete]='False'"

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
        Dim f1 = New frmAddModel
        f1.Text = "Add New Model"
        f1.ShowDialog()
        LoadGrid()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click, DataGridView1.CellContentDoubleClick
        Dim f1 = New frmAddModel
        f1.Text = "Update Model"
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
        Try
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            Dim SQLcmd = New SqlCommand
            Dim strsql = "Select [Index] As [No],
                                [Model] As [Model],
                                [Bin] As [Bin],
                                [Suffix] As [Suffix],
                                [BarcodeLength] As [BarcodeLength],
                                [Description] As [Desc],
                                [Modified Date] As [Modified Date]
                      FROM [CRICUT].[CUPID].[ModelMaster] WHERE ([Index] LIKE '%" & TextBox1.Text & "%' OR [Model] LIKE '%" & TextBox1.Text & "%') AND [Delete]='False'"

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
        Dim res = MessageBox.Show("Comfirm Delete Part Info?" & vbCrLf & "Model No: " & DataGridView1.CurrentRow.Cells("No").Value.ToString & vbCrLf & "Model Name: " & DataGridView1.CurrentRow.Cells("Model").Value.ToString, "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "UPDATE  [CRICUT].[CUPID].[ModelMaster]
                                             SET [Delete] = 'True',
                                              [Modified Date] = '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                     WHERE [Index] = '" & DataGridView1.CurrentRow.Cells("No").Value.ToString & "'"
                SQLcmd.ExecuteNonQuery()
                Conn.Close()
            End If
        End If
        LoadGrid()

    End Sub
End Class