Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Public Class frmLogin
    Dim UID As String
    Dim userlvl As String
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim uname As String
    Dim visib As Boolean
    Dim logged As Boolean
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        logged = False

        Dim main = New frmMain
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "Select [User ID],
                            [User Name], 
                            [User Level],
                            [Password]
                FROM [CRICUT].[CUPID].[UserMaster] WHERE  [Delete]='False'"



            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    If ds.Item("User ID") = username.Text Or ds.Item("User Name") = username.Text Then
                        Dim temppw As String
                        temppw = ds.Item("Password")
                        If pw.Text = Decrypt(temppw) Then
                            Me.Hide()
                            UID = ds.Item("User ID")
                            uname = ds.Item("User Name")
                            userlvl = ds.Item("User Level")
                            main.userid = UID
                            main.userlvl = userlvl
                            main.username = uname
                            main.ToolStripLabel1.Text = "User:" & uname
                            main.WindowState = FormWindowState.Maximized
                            main.ShowDialog()
                            pw.Text = ""
                            username.Text = ""
                            logged = True

                        End If
                    End If


                End While
            End If
        End If
        Conn.Close()
        If Not logged Then
            statuslbl.Text = "Invalid Username or Password"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub





    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If visib = True Then
            Button3.BackgroundImage = My.Resources.Resources.visibility
            pw.UseSystemPasswordChar = True
            visib = False
        Else
            Button3.BackgroundImage = My.Resources.Resources.invisible
            pw.UseSystemPasswordChar = False
            visib = True
        End If
    End Sub

    Private Sub statuslbl_Click(sender As Object, e As EventArgs) Handles statuslbl.Click

    End Sub

    Private Sub pw_TextChanged(sender As Object, e As EventArgs) Handles pw.TextChanged
        statuslbl.Text = ""

    End Sub

    Private Sub username_TextChanged(sender As Object, e As EventArgs) Handles username.TextChanged
        statuslbl.Text = ""
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        username.Text = "admin"
        pw.Text = "admin"
    End Sub

    Private Sub pw_KeyPress(sender As Object, e As KeyPressEventArgs) Handles pw.KeyPress
        logged = False

        Dim main = New frmMain
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "Select [User ID],
                            [User Name], 
                            [User Level],
                            [Password]
                FROM [CRICUT].[CUPID].[UserMaster] WHERE  [Delete]='False'"



            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    If ds.Item("User ID") = username.Text Or ds.Item("User Name") = username.Text Then
                        Dim temppw As String
                        temppw = ds.Item("Password")
                        If pw.Text = Decrypt(temppw) Then
                            Me.Hide()
                            UID = ds.Item("User ID")
                            uname = ds.Item("User Name")
                            userlvl = ds.Item("User Level")
                            main.userid = UID
                            main.userlvl = userlvl
                            main.username = uname
                            main.ToolStripLabel1.Text = "User:" & uname
                            main.WindowState = FormWindowState.Maximized
                            main.ShowDialog()
                            pw.Text = ""
                            username.Text = ""
                            logged = True

                        End If
                    End If


                End While
            End If
        End If
        Conn.Close()
        If Not logged Then
            statuslbl.Text = "Invalid Username or Password"
        End If
    End Sub

    Private Sub username_KeyPress(sender As Object, e As KeyPressEventArgs) Handles username.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            pw.Select()
        End If
    End Sub
End Class