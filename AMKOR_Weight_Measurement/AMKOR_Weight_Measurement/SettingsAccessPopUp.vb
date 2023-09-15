Imports System.Runtime.CompilerServices

Public Class SettingsAccessPopUp

    Private ReadOnly mainForm As Form1



    Public Sub New(mainFormInstance As Form1)
        InitializeComponent()
        mainForm = mainFormInstance
        displaySettingsForm(Variables.adminAccess)
    End Sub

    Private Sub SettingsAccessPopUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Visible = False
    End Sub




#Region "Check Admin Privilege"

    Private Sub checkPrivilege()
        Try
            If Label2.Visible = False Then
                Label2.Visible = True
            End If
            Dim password = TextBox1.Text
            If (password.Contains("asdf1234!@#$") Or password.Contains("amkoradmin123") Or Variables.adminAccess) Then
                Label2.Text = "Correct Credentials"
                Label2.ForeColor = Color.Green
                Variables.adminAccess = True
                displaySettingsForm(Variables.adminAccess)
            Else
                Label2.ForeColor = Color.Red
                Label2.Text = "Invalid Credentials"
            End If
        Catch ex As Exception
        End Try
    End Sub

    'When Trigger check if Password allows admin access
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            checkPrivilege()
        Catch ex As Exception
        End Try
    End Sub

    'Enter Key Trigger
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            checkPrivilege()
        End If
    End Sub

#End Region




    Public Sub displaySettingsForm(ByVal res As Boolean)
        Try
            If res Then
                If Variables.stgsFrm Is Nothing OrElse Variables.stgsFrm.IsDisposed Then
                    Variables.stgsFrm = New Settings(mainForm) ' Create a new instance if not already open
                    Variables.stgsFrm.Show()
                    Variables.stgsFrm.BringToFront()
                Else
                    If Variables.stgsFrm.WindowState = FormWindowState.Minimized Then
                        Variables.stgsFrm.WindowState = FormWindowState.Normal ' Restore the form if minimized
                        Variables.stgsFrm.BringToFront() ' Bring the existing instance to the front
                    End If
                End If
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub


End Class