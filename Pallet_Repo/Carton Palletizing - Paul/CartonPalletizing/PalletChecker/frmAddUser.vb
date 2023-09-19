Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Public Class frmAddUser
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim visib As Boolean
    Private Sub frmAddUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label7.Text = ""
        UID.ReadOnly = True
        UID.Text = ""
        UNAME.Text = ""
        ULEVEL.Text = Nothing
        TextBox3.Text = ""
        TextBox4.Text = ""
        If Text.Contains("Update") Then

            Button1.BackgroundImage = My.Resources.Resources.visibility
            TextBox3.UseSystemPasswordChar = True
            TextBox4.UseSystemPasswordChar = True
            Visible = False
            GroupBox1.Text = "Update User"
            UpBtn.Visible = True
            UpBtn.Location = SaveBtn.Location
            SaveBtn.Visible = False
            LoadUserDataSQL()

        Else
            GroupBox1.Text = "Add New User"
            SaveBtn.Visible = True
            UpBtn.Visible = False
            UID.Text = AutoGenerateId()

        End If
    End Sub

    Private Function CheckDuplicateUserIDName(cond As String) As Boolean
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select [User ID] ,
                                        [User Name]
                          FROM [CRICUT].[CUPID].[UserMaster]WHERE [Delete]='False' " & cond

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            While ds.Read
                Dim username = ds.Item("User Name")
                Dim ID = ds.Item("User ID")
                If ID = UID.Text Then
                    Label7.Text = "User ID " + ID + " has been used, please try another one"
                    Conn.Close()
                    Return True
                ElseIf username = UNAME.Text Then
                    Label7.Text = "User Name " + username + " has been used, please try another one"
                    Conn.Close()
                    Return True
                End If

            End While
        End If
        Conn.Close()
        Return False
    End Function


    Private Function LoadUserDataSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "Select [User ID] 
                          ,[User Name]
                          ,[User Level], [Password]
                      FROM [CRICUT].[CUPID].[UserMaster] WHERE [Index]='" & frmUserMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "' AND [Delete]='False'"

            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    UID.Text = ds.Item("User ID")
                    UNAME.Text = ds.Item("User Name")
                    ULEVEL.Text = ds.Item("User Level")
                    TextBox3.Text = Decrypt(ds.Item("Password"))
                    TextBox4.Text = Decrypt(ds.Item("Password"))

                End While
            End If
        End If
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If visib = True Then
            Button1.BackgroundImage = My.Resources.Resources.visibility
            TextBox3.UseSystemPasswordChar = True
            TextBox4.UseSystemPasswordChar = True
            visib = False
        Else
            Button1.BackgroundImage = My.Resources.Resources.invisible
            TextBox3.UseSystemPasswordChar = False
            TextBox4.UseSystemPasswordChar = False
            visib = True
        End If
    End Sub

    Private Function InsertUserDataSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        If Conn.State = ConnectionState.Open Then
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "INSERT INTO [CRICUT].[CUPID].[UserMaster]
                                                   ([User ID]
                                                   ,[User Name]
                                                   ,[User Level]
                                                   ,[Password]
                                                   ,[Modified Date]
                                                    ,[Delete]   )
           
                                             VALUES
                                               ('" & UID.Text & "'
                                                ,'" & UNAME.Text & "'
                                                ,'" & ULEVEL.Text & "'
                                                ,'" & Encrypt(TextBox3.Text) & "'
                                                ,'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                                ,'False'
                                                  )"
            Dim cmd = New SqlCommand(SQLcmd.CommandText, Conn)
            cmd.ExecuteNonQuery()


        End If
        Conn.Close()
    End Function


    Private Function UpdateUserDataSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        If Conn.State = ConnectionState.Open Then
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "UPDATE [CRICUT].[CUPID].[UserMaster]
                                                   SET [User ID] = '" & UID.Text & "'
                                                   ,[User Name] = '" & UNAME.Text & "'
                                                   ,[User Level] = '" & ULEVEL.Text & "'
                                                   ,[Password] = '" & Encrypt(TextBox3.Text) & "'
                                                   ,[Modified Date] ='" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                                   WHERE [Index] = '" & frmUserMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "' AND [Delete]='False'"
            Dim cmd = New SqlCommand(SQLcmd.CommandText, Conn)
            cmd.ExecuteNonQuery()

            Conn.Close()
        End If
    End Function


    Private Function AutoGenerateId() As String
        Dim start As String = ""
        Dim cnt As Integer = 0
        Dim ID As String
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select Max([User ID]) As [UID]
                          FROM [CRICUT].[CUPID].[UserMaster] WHERE [Delete]='False'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            While ds.Read
                ID = ds.Item("UID")
                ID = ID.Replace("U", "")
                Dim val As Integer = Integer.Parse(ID)
                val += 1
                For Each ch In val.ToString
                    cnt += 1
                Next
                While cnt < 4
                    start = start + "0"
                    cnt += 1
                End While
                ID = "U" + start + val.ToString
            End While
        Else
            ID = "U0001"
        End If
        ds.Close()
        Return ID
    End Function



    Private Function Encrypt(ByVal clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        'Dim t = MsgBox(clearText)


        'Decrypt(clearText)
        Return clearText
    End Function

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Me.Close()
    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If Label7.Text <> "" Then
            Exit Sub
        End If
        If UNAME.Text = "" Or UNAME.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            Label7.Text = "Please make sure all the fields are entered"
            Exit Sub
        End If


        If CheckDuplicateUserIDName("") Then
            Exit Sub
        End If

        Dim res = MessageBox.Show("Comfirm Add New User Info?" & Environment.NewLine & "User ID: " & UID.Text & Environment.NewLine & "User Name: " & UNAME.Text & Environment.NewLine & "User Level:" & ULEVEL.Text, "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            InsertUserDataSQL()
            Me.Close()
        Else
            Exit Sub
        End If



    End Sub

    Private Sub UpBtn_Click(sender As Object, e As EventArgs) Handles UpBtn.Click
        If Label7.Text <> "" Then
            Exit Sub
        End If
        If UNAME.Text = "" Or UNAME.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            Label7.Text = "Please make sure all the fields are entered"
            Exit Sub
        End If
        Dim cond = "AND NOT [Index] = '" & frmUserMaster.DataGridView1.CurrentRow.Cells("No").ToString & "'"
        If CheckDuplicateUserIDName(cond) Then
            Exit Sub
        End If


        Dim res = MessageBox.Show("Comfirm Update User Info? " & Environment.NewLine & "User ID: " & UID.Text & Environment.NewLine & "User Name:" & UNAME.Text & Environment.NewLine & "User Level: " & ULEVEL.Text, "Comfirm Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)

        If res = DialogResult.Yes Then
            UpdateUserDataSQL()
        Else
            Exit Sub
        End If
        Me.Close()


    End Sub


    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text <> Nothing And TextBox4.Text <> TextBox3.Text Then
            Label7.Text = "Password does not match"
        Else
            Label7.Text = ""
        End If
    End Sub

    Private Sub UID_TextChanged(sender As Object, e As EventArgs) Handles UID.TextChanged, UNAME.TextChanged
        Label7.Text = ""
        Dim cond = ""
        If UNAME.Text.Contains("U") Then
            Dim tempstr = UNAME.Text
            tempstr = tempstr.Replace("U", "")
            If tempstr <> "" And IsNumeric(tempstr) Then
                Label7.Text = "Invalid Username. Please Try Another One."
            End If
        End If
        If Not Text.Contains("Update") Then
            If CheckDuplicateUserIDName("") Then
                Exit Sub
            End If
        Else
            cond = " AND NOT [Index]='" & frmUserMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "'"
            If CheckDuplicateUserIDName(cond) Then
                Exit Sub
            End If
        End If
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim f1 = New frmHelp
        If Text.Contains("Update") Then
            f1.helptitle = "updateuser"

        Else
            f1.helptitle = "adduser"

        End If
        f1.Owner = Me
        f1.ShowDialog()
    End Sub

    Private Sub UNAME_KeyPress(sender As Object, e As KeyPressEventArgs) Handles UNAME.KeyPress

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            TextBox4.Select()

        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            If Text.Contains("Update") Then
                If Label7.Text <> "" Then
                    Exit Sub
                End If
                If UNAME.Text = "" Or UNAME.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                    Label7.Text = "Please make sure all the fields are entered"
                    Exit Sub
                End If
                Dim cond = "AND NOT [Index] = '" & frmUserMaster.DataGridView1.CurrentRow.Cells("No").ToString & "'"
                If CheckDuplicateUserIDName(cond) Then
                    Exit Sub
                End If


                Dim res = MessageBox.Show("Comfirm Update User Info? " & Environment.NewLine & "User ID: " & UID.Text & Environment.NewLine & "User Name:" & UNAME.Text & Environment.NewLine & "User Level: " & ULEVEL.Text, "Comfirm Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)

                If res = DialogResult.Yes Then
                    UpdateUserDataSQL()
                Else
                    Exit Sub
                End If
                Me.Close()
            Else

                If Label7.Text <> "" Then
                    Exit Sub
                End If
                If UNAME.Text = "" Or UNAME.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                    Label7.Text = "Please make sure all the fields are entered"
                    Exit Sub
                End If


                If CheckDuplicateUserIDName("") Then
                    Exit Sub
                End If

                Dim res = MessageBox.Show("Comfirm Add New User Info?" & Environment.NewLine & "User ID: " & UID.Text & Environment.NewLine & "User Name: " & UNAME.Text & Environment.NewLine & "User Level:" & ULEVEL.Text, "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                If res = DialogResult.Yes Then
                    InsertUserDataSQL()
                    Me.Close()
                Else
                    Exit Sub
                End If
            End If

        End If

    End Sub
End Class