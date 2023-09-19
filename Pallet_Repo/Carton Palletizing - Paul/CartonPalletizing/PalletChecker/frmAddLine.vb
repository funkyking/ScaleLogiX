Imports System.Data.SqlClient

Public Class frmAddLine
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim MID As Guid
    Private Sub frmAddLine_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MID = Guid.NewGuid
        If Text.Contains("Update") Then

            GroupBox1.Text = "Update Line"
            SaveBtn.Visible = False
            UpdateBtn.Visible = True
            UpdateBtn.Location = SaveBtn.Location
            LoadDataSQL()
        Else
            GroupBox1.Text = "Add New Line"
            SaveBtn.Visible = True
            UpdateBtn.Visible = False
        End If
    End Sub

    Private Function LoadDataSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select 
                                  [LineID]
                                  ,[Line]
                                  ,[Description]
                                
                          FROM [CRICUT].[CUPID].[LineMaster] WHERE [Index] = '" & frmLineMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "'AND [Delete]='False'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            While ds.Read
                MID = ds.Item("LineID")
                Line.Text = ds.Item("Line")
                Desc.Text = ds.Item("Description")


            End While
        End If
        Conn.Close()
    End Function

    Private Function CheckDuplicateItem() As Boolean
        Dim cond As String
        If Text.Contains("Update") Then
            cond = "Not [Index]='" & frmLineMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "' AND "
        End If
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select *
                          FROM [CRICUT].[CUPID].[LineMaster] WHERE " & cond & "[Line] = '" & Line.Text & "' AND [Delete]='False'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            statuslbl.Text = "Duplicate Item Found. Please Try Again."
            Return True
        Else
            statuslbl.Text = ""
            Return False
        End If
    End Function
    Private Function CheckDuplicateID()
startingplace:
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select *
                          FROM [CRICUT].[CUPID].[LineMaster] WHERE [LineID] = '" & MID.ToString & "'AND [Delete]='False'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            MID = Guid.NewGuid
            Conn.Close()
            GoTo startingplace
        End If
        Conn.Close()

    End Function

    Private Sub Model_TextChanged(sender As Object, e As EventArgs)
        CheckDuplicateItem()
    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If Line.Text = "" Then
            statuslbl.Text = "Line field missing"
        End If
        If statuslbl.Text <> "" Then
            Exit Sub
        End If
        CheckDuplicateID()
        If CheckDuplicateItem() Then
            Exit Sub
        End If
        Dim res = MessageBox.Show("Confirm Add Line?" & vbCrLf & "Line: " & Line.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            InsertDataSQL()
        Else
            Exit Sub
        End If
        Me.Close()
    End Sub



    Private Function InsertDataSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "INSERT INTO [CRICUT].[CUPID].[LineMaster]
                                              ([LineID]
                                               ,[Line]
                                               ,[Description]
                                               ,[ModifiedDate]
                                               ,[Delete])
                                         VALUES
                                               ('" & MID.ToString & "'
                                               ,'" & Line.Text & "'
                                               ,'" & Desc.Text & "'
                                               ,' " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                               ,'False')"


            SQLcmd.ExecuteNonQuery()
            Conn.Close()
        End If

    End Function

    Private Function UpdateDataSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "UPDATE [CRICUT].[CUPID].[LineMaster]
                                     SET
                                          [Line] = '" & Line.Text & "'
                                          ,[Description] = '" & Desc.Text & "'
                                          ,[ModifiedDate] =  '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                          ,[Delete] = 'False'
                                   
                                 WHERE [Index]='" & frmLineMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "'"
            SQLcmd.ExecuteNonQuery()
            Conn.Close()
        End If
    End Function

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Me.Close()
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim f1 = New frmHelp
        If Text.Contains("Update") Then
            f1.helptitle = "updatemodel"

        Else
            f1.helptitle = "addmodel"

        End If
        f1.Owner = Me
        f1.ShowDialog()
    End Sub

    Private Sub UpdateBtn_Click_1(sender As Object, e As EventArgs) Handles UpdateBtn.Click
        If Line.Text = "" Then
            statuslbl.Text = "Line field missing"
        End If
        If statuslbl.Text <> "" Then
            Exit Sub
        End If
        CheckDuplicateID()
        If CheckDuplicateItem() Then
            Exit Sub
        End If
        Dim res = MessageBox.Show("Confirm Update Line?" & vbCrLf & "Line: " & Line.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            UpdateDataSQL()
        Else
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub Line_TextChanged(sender As Object, e As EventArgs) Handles Line.TextChanged
        statuslbl.Text = ""
    End Sub

    Private Sub Line_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Line.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            Desc.Select()

        End If
    End Sub

    Private Sub Desc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Desc.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If Text.Contains("Update") Then

                If Line.Text = "" Then
                    statuslbl.Text = "Line field missing"
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                End If
                CheckDuplicateID()
                If CheckDuplicateItem() Then
                    Exit Sub
                End If
                Dim res = MessageBox.Show("Confirm Update Line?" & vbCrLf & "Line: " & Line.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                If res = DialogResult.Yes Then
                    UpdateDataSQL()
                Else
                    Exit Sub
                End If
                Me.Close()
            Else
                If Line.Text = "" Then
                    statuslbl.Text = "Line field missing"
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                End If
                CheckDuplicateID()
                If CheckDuplicateItem() Then
                    Exit Sub
                End If
                Dim res = MessageBox.Show("Confirm Add Line?" & vbCrLf & "Line: " & Line.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                If res = DialogResult.Yes Then
                    InsertDataSQL()
                Else
                    Exit Sub
                End If
                Me.Close()
            End If
        End If
    End Sub

    Private Sub Desc_TextChanged(sender As Object, e As EventArgs) Handles Desc.TextChanged

    End Sub
End Class