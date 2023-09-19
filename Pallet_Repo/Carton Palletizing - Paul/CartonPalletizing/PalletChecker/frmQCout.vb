Imports System.Data.SqlClient

Public Class frmQCout

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

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If Username.Text = "" Then
            statuslbl.Text = "Username Field Missing"
        End If
        If ListBox1.Items.Count = 0 Then
            statuslbl.Text = "Empty Serial Number"
        End If
        If statuslbl.Text <> "" Then
            Exit Sub
        End If


        Dim res = MessageBox.Show("Confirm Add QC out?" & vbCrLf & "Username:" & Username.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            QCout()
        Else
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Function QCout()
        For Each item In ListBox1.Items
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "Update [CRICUT].[CUPID].[WorkOrder]
                                     SET
                                          [QCout]='True'
                                          ,[OutUser] = '" & Username.Text & "'
                                          ,[OutDate] =  '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'         
                                 WHERE [Serial No] ='" & item & "' AND [Work Order ID]='" & WID.ToString & "'"


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
            If Serial.Text = "" And ListBox1.Items.Count > 0 Then
                Username.Select()
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
        Else
            statuslbl.Text = ""
        End If
        If CheckDuplicate() And Serial.Text <> "" Then
            statuslbl.Text = "Item is already out"
        End If
        For Each item In ListBox1.Items
            If item = Serial.Text Then
                statuslbl.Text = "Duplicate Serial Number"
            End If
        Next
    End Sub

    Private Sub frmQCout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Serial.Select()
    End Sub

    Private Sub Username_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Username.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If Username.Text = "" Then
                statuslbl.Text = "Username Field Missing"
            End If
            If ListBox1.Items.Count = 0 Then
                statuslbl.Text = "Empty Serial Number"
            End If
            If statuslbl.Text <> "" Then
                Exit Sub
            End If


            Dim res = MessageBox.Show("Confirm Add QC out?" & vbCrLf & "Username:" & Username.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
            If res = DialogResult.Yes Then
                QCout()
            Else
                Exit Sub
            End If
            Me.Close()
        End If
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        If ListBox1.SelectedIndex >= 0 Then
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Function CheckDuplicate()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select [Serial No]
                          FROM [CRICUT].[CUPID].[WorkOrder] WHERE [Work Order ID] = '" & WID.ToString & "' AND [Serial No]='" & Serial.Text & "' AND [QCout]='True'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            Return True
        Else
            Return False
        End If
    End Function
End Class