Imports System.Data.SqlClient

Public Class frmAddModel
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim MID As Guid
    Private Sub frmAddModel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MID = Guid.NewGuid
        statuslbl2.Text = ""
        If Text.Contains("Update") Then
            GroupBox2.Text = "Update Model"
            btnSave.Visible = False
            btnUpdate.Visible = True
            btnUpdate.Location = btnSave.Location
            LoadDataSQL()
        Else
            GroupBox2.Text = "Add New Model"
            btnSave.Visible = True
            btnUpdate.Visible = False
        End If
    End Sub

    Private Function LoadDataSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select 
                                  [Model ID]
                                  ,[Model]
                                  ,[Bin]
                                  ,[Suffix]
                                  ,[BarcodeLength]
                                  ,[Description]
                                
                          FROM [CRICUT].[CUPID].[ModelMaster] WHERE [Index] = '" & frmModelMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "'AND [Delete]='False'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            While ds.Read
                MID = ds.Item("Model ID")
                txtModel.Text = ds.Item("Model")
                txtBin.Text = ds.Item("Bin")
                txtSuffix.Text = ds.Item("Suffix").ToString
                txtBarcodeLength.Text = ds.Item("BarcodeLength").ToString
                txtDesc.Text = ds.Item("Description")

            End While
        End If
        Conn.Close()
    End Function

    Private Function CheckDuplicateItem() As Boolean
        Dim cond As String
        If Text.Contains("Update") Then
            cond = "Not [Index]='" & frmModelMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "' AND "
        End If
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select *
                          FROM [CRICUT].[CUPID].[ModelMaster] WHERE " & cond & "[Model] = '" & txtModel.Text & "' AND [Delete]='False'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            statuslbl2.Text = "Duplicate Item Found"
            Return True
        Else
            statuslbl2.Text = ""
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
                          FROM [CRICUT].[CUPID].[ModelMaster] WHERE [Model ID] = '" & MID.ToString & "'AND [Delete]='False'"

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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtModel.Text = "" Then
            statuslbl2.Text = "Model field missing"
        End If
        If statuslbl2.Text <> "" Then
            Exit Sub
        End If
        CheckDuplicateID()
        If CheckDuplicateItem() Then
            Exit Sub
        End If
        Dim res = MessageBox.Show("Confirm Add Model?" & vbCrLf & "Model: " & txtModel.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            InsertDataSQL()
        Else
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If txtModel.Text = "" Then
            statuslbl2.Text = "Model field missing"
        End If
        If statuslbl2.Text <> "" Then
            Exit Sub
        End If
        CheckDuplicateID()
        If CheckDuplicateItem() Then
            Exit Sub
        End If
        Dim res = MessageBox.Show("Confirm Update Model?" & vbCrLf & "Model: " & txtModel.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            UpdateDataSQL()
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
            SQLcmd.CommandText = "INSERT INTO [CRICUT].[CUPID].[ModelMaster]
                                              ([Model ID]
                                               ,[Model]
                                               ,[Bin]
                                               ,[Suffix]
                                               ,[BarcodeLength]
                                               ,[Description]
                                               ,[Modified Date]
                                               ,[Delete])
                                         VALUES
                                               ('" & MID.ToString & "'
                                               ,'" & txtModel.Text & "'
                                               ,'" & txtBin.Text & "'
                                               ,'" & txtSuffix.Text & "'
                                               ,'" & txtBarcodeLength.Text & "'
                                               ,'" & txtDesc.Text & "'
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
            SQLcmd.CommandText = "UPDATE [CRICUT].[CUPID].[ModelMaster]
                                     SET
                                          [Model] = '" & txtModel.Text & "'
                                          ,[Bin] = '" & txtBin.Text & "'
                                          ,[Suffix] = '" & txtSuffix.Text & "'
                                          ,[BarcodeLength] = '" & txtBarcodeLength.Text & "'
                                          ,[Description] = '" & txtDesc.Text & "'
                                          ,[Modified Date] =  '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                          ,[Delete] = 'False'
                                   
                                 WHERE [Index]='" & frmModelMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "'"
            SQLcmd.ExecuteNonQuery()
            Conn.Close()
        End If
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub



    Private Sub txtModel_TextChanged_1(sender As Object, e As EventArgs) Handles txtModel.TextChanged
        statuslbl2.Text = ""
    End Sub

    Private Sub frmAddModel_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        Dim f1 = New frmHelp
        If Text.Contains("Update") Then
            f1.helptitle = "updatemodel"

        Else
            f1.helptitle = "addmodel"

        End If
        f1.Owner = Me
        f1.ShowDialog()
    End Sub

    Private Sub txtModel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtModel.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            txtBin.Select()

        End If
    End Sub


    Private Sub txtBin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBin.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            txtDesc.Select()

        End If
    End Sub

    Private Sub txtDesc_TextChanged(sender As Object, e As EventArgs) Handles txtDesc.TextChanged

    End Sub

    Private Sub txtDesc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesc.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If Text.Contains("Update") Then

                If txtModel.Text = "" Then
                    statuslbl2.Text = "Model field missing"
                End If
                If statuslbl2.Text <> "" Then
                    Exit Sub
                End If
                CheckDuplicateID()
                If CheckDuplicateItem() Then
                    Exit Sub
                End If
                Dim res = MessageBox.Show("Confirm Update Model?" & vbCrLf & "Model: " & txtModel.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                If res = DialogResult.Yes Then
                    UpdateDataSQL()
                Else
                    Exit Sub
                End If
                Me.Close()
            Else
                If txtModel.Text = "" Then
                    statuslbl2.Text = "Model field missing"
                End If
                If statuslbl2.Text <> "" Then
                    Exit Sub
                End If
                CheckDuplicateID()
                If CheckDuplicateItem() Then
                    Exit Sub
                End If
                Dim res = MessageBox.Show("Confirm Add Model?" & vbCrLf & "Model: " & txtModel.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                If res = DialogResult.Yes Then
                    InsertDataSQL()
                Else
                    Exit Sub
                End If
                Me.Close()
            End If
        End If
    End Sub
End Class