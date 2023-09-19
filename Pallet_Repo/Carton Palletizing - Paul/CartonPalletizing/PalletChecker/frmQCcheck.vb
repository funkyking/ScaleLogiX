Imports System.Data.SqlClient
Public Class frmQCcheck
    Public WID As Guid
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)

    Private Sub frmQCcheck_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub

    Private Function LoadGrid()
        Try
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            Dim SQLcmd = New SqlCommand
            Dim strsql = "Select 
                               [Serial No] As [Serial No], [Pallet No],
                                [OutUser] As [OutUser],
                                [OutDate] As [OutDate],
                                [InUser] As [InUser],
                                [InDate] As [InDate]
                      FROM [CRICUT].[CUPID].[WorkOrder] WHERE [QCout]='True' AND [Work Order ID]='" & WID.ToString & "' Order By [Pallet No]"

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



    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        DataGridView1.Columns.Item("Serial No").Width = 75
        DataGridView1.Columns.Item("Pallet No").Width = 75
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells("InUser").Value.ToString = "" Then
                DataGridView1.Rows(i).Cells("Serial No").Style.BackColor = Color.PaleVioletRed
                DataGridView1.Rows(i).Cells("Pallet No").Style.BackColor = Color.PaleVioletRed
                DataGridView1.Rows(i).Cells("OutUser").Style.BackColor = Color.PaleVioletRed
                DataGridView1.Rows(i).Cells("OutDate").Style.BackColor = Color.PaleVioletRed
                DataGridView1.Rows(i).Cells("InUser").Style.BackColor = Color.PaleVioletRed
                DataGridView1.Rows(i).Cells("InDate").Style.BackColor = Color.PaleVioletRed
            End If
        Next
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim res = MessageBox.Show("Comfirm Delete QC Record?" & vbCrLf & "Serial No: " & DataGridView1.CurrentRow.Cells("Serial No").Value.ToString, "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "UPDATE  [CRICUT].[CUPID].[WorkOrder]
                                             SET [QCout] = 'False',
                                                [OutUser]=NULL,
                                                [OutDate]=NULL,
                                                [QCin]='False',
                                                [InUser]=NULL,
                                                [InDate]=NULL
                                     WHERE [Serial No] = '" & DataGridView1.CurrentRow.Cells("Serial No").Value.ToString & "'"
                SQLcmd.ExecuteNonQuery()
                Conn.Close()
            End If
        End If
        LoadGrid()
    End Sub


End Class


