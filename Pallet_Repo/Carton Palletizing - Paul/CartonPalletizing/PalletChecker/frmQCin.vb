
Imports System.Data.SqlClient

Public Class frmQCin
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Public WID As Guid

    Public Function CheckExist()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select [Serial No]
                          FROM [CRICUT].[CUPID].[WorkOrder] WHERE [Work Order ID] = '" & WID.ToString & "' AND [Serial No]='" & Serial.Text & "'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            Return False
        Else
            Return True
        End If
    End Function


    Private Sub UpdateBtn_Click(sender As Object, e As EventArgs) Handles UpdateBtn.Click
        If Username.Text = "" Then
            statuslbl.Text = "Missing Username Field"
        Else
            statuslbl.Text = ""
        End If
        If ListBox1.Items.Count = 0 Then
            statuslbl.Text = "Empty Serial Number"
        End If
        If statuslbl.Text <> "" Then
            Exit Sub
        End If
        Dim res = MessageBox.Show("Confirm Add QC In?" & vbCrLf & "Username:" & Username.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            QCin()
            LoadGrid()
            Serial.Text = ""
            ListBox1.Items.Clear()
            Username.Text = ""
            Serial.Select()

        Else
            Exit Sub
            Me.Close()
        End If
    End Sub



    Private Function CheckQC()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select [Serial No]
                          FROM [CRICUT].[CUPID].[WorkOrder] WHERE [Work Order ID] = '" & WID.ToString & "' AND [QCout]='True' AND [Serial No]='" & Serial.Text & "'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function QCin()
        For Each item In ListBox1.Items
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "Update [CRICUT].[CUPID].[WorkOrder]
                                     SET
                                          [QCin]='True'
                                          ,[InUser] = '" & Username.Text & "'
                                          ,[InDate] =  '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'         
                                 WHERE [Serial No] ='" & item & "' AND [Work Order ID] = '" & WID.ToString & "'"


                SQLcmd.ExecuteNonQuery()
                Conn.Close()
            End If
        Next


    End Function

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Me.Close()
    End Sub

    Private Sub Serial_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Serial.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If Serial.Text = "" And ListBox1.Items.Count <> 0 Then
                Username.Select()
                Exit Sub
            Else
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If Serial.Text <> "" Then
                        ListBox1.Items.Add(Serial.Text)
                        Serial.Text = ""
                        Serial.Select()
                    End If
                End If

                End If
        End If


    End Sub

    Private Sub Username_TextChanged(sender As Object, e As EventArgs) Handles Username.TextChanged
        statuslbl.Text = ""
    End Sub

    Private Sub Serial_TextChanged(sender As Object, e As EventArgs) Handles Serial.TextChanged
        If CheckExist() And Serial.Text <> "" Then
            statuslbl.Text = "Invalid Serial Number"
            Exit Sub
        End If
        If CheckQC() And Serial.Text <> "" Then
            statuslbl.Text = "Invalid Serial Number. Item Not Out"
            Exit Sub
        Else
            statuslbl.Text = ""
        End If
        If CheckState() = False Then
            statuslbl.Text = "This Item is Already In"
        End If
        For Each item In ListBox1.Items
            If item = Serial.Text Then
                statuslbl.Text = "Duplicate Serial Number"
            End If
        Next

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

    Private Sub frmQCin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
        Serial.Select()
    End Sub

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

    Private Sub Username_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Username.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            If Username.Text = "" Then
                statuslbl.Text = "Username Field Missing"
            Else
                statuslbl.Text = ""
            End If
            If ListBox1.Items.Count = 0 Then
                statuslbl.Text = "Empty Serial Number"
            End If
            If statuslbl.Text <> "" Then
                Exit Sub
            End If
            Dim res = MessageBox.Show("Confirm Add QC In?" & vbCrLf & "Username:" & Username.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
            If res = DialogResult.Yes Then
                QCin()
                LoadGrid()
                Serial.Text = ""
                ListBox1.Items.Clear()
                Username.Text = ""
                Serial.Select()

            Else
                Exit Sub
                Me.Close()
            End If
        End If
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        If ListBox1.SelectedIndex >= 0 Then
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)

        End If
    End Sub

    Private Function CheckState()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select [Serial No]
                          FROM [CRICUT].[CUPID].[WorkOrder] WHERE [Work Order ID] = '" & WID.ToString & "' AND [Serial No]='" & Serial.Text & "' AND [QCin]='True'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            Return False
        Else
            Return True
        End If
    End Function
End Class