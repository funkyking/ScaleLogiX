Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class frmMain
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)

    Public username
    Public userlvl
    Public userid

    Private Sub ToolStripDropDownButton1_Click(sender As Object, e As EventArgs) Handles Master.Click
        If ToolStripButton4.Visible = True Then
            ToolStripButton4.Visible = False
            ToolStripButton8.Visible = False
            ToolStripButton7.Visible = False
            ToolStripButton9.Visible = False
            ToolStripButton1.Visible = False
        Else
            ToolStripButton4.Visible = True
            ToolStripButton4.Visible = True
            ToolStripButton8.Visible = True
            ToolStripButton7.Visible = True
            ToolStripButton9.Visible = True
            ToolStripButton1.Visible = True
        End If
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If Now.Month = 8 Then
        '    MsgBox($"Update Required.{vbCr}Contact Your Developer.", MsgBoxStyle.Exclamation, "Error")
        '    Me.Close()
        '    Exit Sub
        'End If

        ToolStripButton4.Visible = False
        ToolStripButton8.Visible = False
        ToolStripButton7.Visible = False
        ToolStripButton9.Visible = False
        ToolStripButton1.Visible = False
        SetUserAccess()
        Me.WindowState = WindowState.Maximized


    End Sub

    Private Function SetUserAccess()
        Dim inv As Boolean
        Dim mas As Boolean
        Dim setting As Boolean
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "Select [User lvl]
                                  ,[Production]
                                  ,[Master]
                                
                                  ,[Setting]
                              FROM [CRICUT].[CUPID].[Access] WHERE [User lvl]='" & userlvl & "'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    inv = ds.Item("Production")
                    mas = ds.Item("Master")
                    setting = ds.Item("Setting")
                End While
            End If
        End If
        Conn.Close()
        If inv Then
            Production.Enabled = True
            btnCheck.Enabled = True
        Else
            Production.Enabled = False
            btnCheck.Enabled = False
        End If
        If mas Then
            Master.Enabled = True
        Else
            Master.Enabled = False
        End If

        If setting Then
            SettingBtn.Enabled = True
        Else
            SettingBtn.Enabled = False

        End If
    End Function

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles Production.Click
        Closeform()
        ShowMDIForm(frmProduction)
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        Closeform()
        Me.Close()
        frmLogin.Close()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles SettingBtn.Click
        Closeform()
        ShowMDIForm(frmSetting)
    End Sub


    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Closeform()
        ShowMDIForm(frmPartMaster)
    End Sub




    Public Function Closeform()

        My.Application.OpenForms.Cast(Of Form)() _
              .Except({Me, frmLogin}) _
              .ToList() _
              .ForEach(Sub(form) form.Close())
    End Function

    Function ShowMDIForm(frm As Form) As Boolean

        Try
            Me.IsMdiContainer = True
            'For Each f As Form In Me.MdiChildren
            '    f.Close()
            'Next


            'frmInventoryPartMaster.ShowDialog()
            'Exit Sub
            'Me.IsMdiContainer = True
            frm.MdiParent = Me
            'frm.ClientSize = Me.ClientRectangle.Size
            Me.WindowState = FormWindowState.Maximized
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            'frm.WindowState = System.Windows.Forms.FormWindowState.Normal
            'frm.Dock = DockStyle.Right
            frm.Anchor = AnchorStyles.Left And AnchorStyles.Top
            frm.Show()
            frm.BringToFront()


            'InitialCon()
            Return True
        Catch ex As Exception
        End Try

    End Function

    Function ShowMDIForm(frm As Form, uname As String) As Boolean
        Try
            Me.IsMdiContainer = True
            Dim frm1 = New frmProduction

            frm1.MdiParent = Me

            Me.WindowState = FormWindowState.Maximized
            frm1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            'frm.WindowState = System.Windows.Forms.FormWindowState.Normal
            'frm.Dock = DockStyle.Right
            frm1.Anchor = AnchorStyles.Left And AnchorStyles.Top
            'frm1.username = uname
            frm1.Show()
            frm1.BringToFront()


            'InitialCon()
            Return True
        Catch ex As Exception
        End Try

    End Function

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        Closeform()
        ShowMDIForm(frmWorkOrderMaster)
    End Sub

    Private Sub ToolStripButton7_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        Closeform()
        ShowMDIForm(frmModelMaster)
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        Closeform()
        ShowMDIForm(frmUserMaster)
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        Dim f1 = New frmHelp
        f1.helptitle = "main"
        f1.Owner = Me
        f1.ShowDialog()
    End Sub

    Private Sub ToolStripButton1_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Closeform()
        ShowMDIForm(frmLineMaster)

    End Sub

    Private Sub frmMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        frmLogin.Close()
    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        Closeform()
        ShowMDIForm(frmCheck)
    End Sub
End Class
