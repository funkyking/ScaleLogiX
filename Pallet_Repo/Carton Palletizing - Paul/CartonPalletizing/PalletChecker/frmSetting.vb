
Imports System.Data.SqlClient
    Public Class frmSetting
        Dim localpath
        Dim connstr
        Dim userlvl As String
        'Dim tech As New Setting
        'Dim admin As New Setting
        'Dim staff As New Setting
        'Dim engineer As New Setting
        Dim setarr(100) As Setting
        Dim ptr As Integer = 0
        Dim arrsize As Integer = 0
        Private Sub InitializeSystem()
            ptr = 0
            arrsize = 0
            localpath = (ReadValue("System", "localpath", IniPath))
            connstr = (ReadValue("System", "SQLconnstr", IniPath))
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "Select [User lvl]
            ,[Production]
            ,[Master]
            ,[Setting]
        FROM [CRICUT].[CUPID].[Access]"

                Dim ds = SQLcmd.ExecuteReader
                If ds.HasRows Then
                    While ds.Read
                        'If ds.Item("User lvl") = "Admin" Then
                        '    admin.inv = ds.Item("Inventory")
                        '    admin.log = ds.Item("Log")
                        '    admin.rep = ds.Item("Report")
                        '    admin.mas = ds.Item("Master")
                        '    admin.setting = ds.Item("Setting")
                        'ElseIf ds.Item("User lvl") = "Technician" Then
                        '    tech.inv = ds.Item("Inventory")
                        '    tech.log = ds.Item("Log")
                        '    tech.rep = ds.Item("Report")
                        '    tech.mas = ds.Item("Master")
                        '    tech.setting = ds.Item("Setting")
                        'ElseIf ds.Item("User lvl") = "Staff" Then
                        '    staff.inv = ds.Item("Inventory")
                        '    staff.log = ds.Item("Log")
                        '    staff.rep = ds.Item("Report")
                        '    staff.mas = ds.Item("Master")
                        '    staff.setting = ds.Item("Setting")
                        'ElseIf ds.Item("User lvl") = "Engineer" Then
                        '    engineer.inv = ds.Item("Inventory")
                        '    engineer.log = ds.Item("Log")
                        '    engineer.rep = ds.Item("Report")
                        '    engineer.mas = ds.Item("Master")
                        '    engineer.setting = ds.Item("Setting")
                        'End If
                        setarr(ptr) = New Setting
                        setarr(ptr).user = ds.Item("User lvl")
                    setarr(ptr).pro = ds.Item("Production")
                    setarr(ptr).mas = ds.Item("Master")
                        setarr(ptr).setting = ds.Item("Setting")
                        ptr += 1
                        arrsize += 1



                    End While
                End If
            End If
            Conn.Close()

        End Sub

    Private Sub frmSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReadfromIni()
        If ReadValue("System", "SkipCarton", IniPath) = 1 Then
            cbxSkipCarton.Checked = True
        Else
            cbxSkipCarton.Checked = False
        End If


    End Sub

    Private Function ReadfromIni()
            InitializeSystem()

            TextBox1.Text = localpath
            TextBox2.Text = connstr
        End Function

        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            Dim res = MessageBox.Show(ControlChars.Tab & "Comfirm Update Configuration Setting?", "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
            If res = DialogResult.Yes Then
                WriteValue("System", "localpath", TextBox1.Text, IniPath)
                WriteValue("System", "SQLconnstr", TextBox2.Text, IniPath)
            Else
                Exit Sub
            End If
        End Sub

        Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
            If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
                TextBox1.Text = FolderBrowserDialog1.SelectedPath
            End If
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            Dim res = MessageBox.Show("Comfirm Restore To Previous Configuration Settings?", "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
            If res = DialogResult.Yes Then
                ReadfromIni()
            Else
                Exit Sub
            End If
        End Sub

        Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

            CheckBox1.Checked = CheckState.Unchecked
        CheckBox2.Checked = CheckState.Unchecked
        CheckBox5.Checked = CheckState.Unchecked
            Loadcheckbox()

        End Sub

        Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.Click

            'If ComboBox1.SelectedItem = "Admin" Then
            '    If CheckBox1.CheckState = CheckState.Checked Then
            '        admin.inv = True
            '    Else
            '        admin.inv = False
            '    End If
            'ElseIf ComboBox1.SelectedItem = "Technician" Then
            '    If CheckBox1.CheckState = CheckState.Checked Then
            '        tech.inv = True
            '    Else
            '        tech.inv = False
            '    End If
            'ElseIf ComboBox1.SelectedItem = "Engineer" Then
            '    If CheckBox1.CheckState = CheckState.Checked Then
            '        engineer.inv = True
            '    Else
            '        engineer.inv = False
            '    End If
            'ElseIf ComboBox1.SelectedItem = "Staff" Then
            '    If CheckBox1.CheckState = CheckState.Checked Then
            '        staff.inv = True
            '    Else
            '        staff.inv = False
            '    End If
            'End If
            For i = 0 To ptr - 1
                If ComboBox1.SelectedItem = setarr(i).user Then
                    If CheckBox1.CheckState = CheckState.Checked Then
                    setarr(i).pro = True
                Else
                    setarr(i).pro = False
                End If
                End If
            Next

        End Sub
        Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.Click

            'If ComboBox1.SelectedItem = "Admin" Then
            '    If CheckBox2.CheckState = CheckState.Checked Then
            '        admin.mas = True
            '    Else
            '        admin.mas = False
            '    End If
            'ElseIf ComboBox1.SelectedItem = "Technician" Then
            '    If CheckBox2.CheckState = CheckState.Checked Then
            '        tech.mas = True
            '    Else
            '        tech.mas = False
            '    End If
            'ElseIf ComboBox1.SelectedItem = "Engineer" Then
            '    If CheckBox2.CheckState = CheckState.Checked Then
            '        engineer.mas = True
            '    Else
            '        engineer.mas = False
            '    End If
            'ElseIf ComboBox1.SelectedItem = "Staff" Then
            '    If CheckBox2.CheckState = CheckState.Checked Then
            '        staff.mas = True
            '    Else
            '        staff.mas = False
            '    End If
            'End If
            For i = 0 To ptr - 1
                If ComboBox1.SelectedItem = setarr(i).user Then
                    If CheckBox2.CheckState = CheckState.Checked Then
                        setarr(i).mas = True
                    Else
                        setarr(i).mas = False
                    End If
                End If
            Next
        End Sub
    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.Click

        'If ComboBox1.SelectedItem = "Admin" Then
        '    If CheckBox5.CheckState = CheckState.Checked Then
        '        admin.setting = True
        '    Else
        '        admin.setting = False
        '    End If
        'ElseIf ComboBox1.SelectedItem = "Technician" Then
        '    If CheckBox5.CheckState = CheckState.Checked Then
        '        tech.setting = True
        '    Else
        '        tech.setting = False
        '    End If
        'ElseIf ComboBox1.SelectedItem = "Engineer" Then
        '    If CheckBox5.CheckState = CheckState.Checked Then
        '        engineer.setting = True
        '    Else
        '        engineer.setting = False
        '    End If
        'ElseIf ComboBox1.SelectedItem = "Staff" Then
        '    If CheckBox5.CheckState = CheckState.Checked Then
        '        staff.setting = True
        '    Else
        '        staff.setting = False
        '    End If
        'End If
        For i = 0 To ptr - 1
            If ComboBox1.SelectedItem = setarr(i).user Then
                If CheckBox5.CheckState = CheckState.Checked Then
                    setarr(i).setting = True
                Else
                    setarr(i).setting = False
                End If
            End If
        Next
    End Sub
    Private Function Loadcheckbox()
            'If ComboBox1.SelectedItem = "Admin" Then
            '    If admin.inv Then
            '        CheckBox1.Checked = CheckState.Checked
            '    End If
            '    If admin.log Then
            '        CheckBox3.Checked = CheckState.Checked
            '    End If
            '    If admin.rep Then
            '        CheckBox4.Checked = CheckState.Checked
            '    End If
            '    If admin.mas Then
            '        CheckBox2.Checked = CheckState.Checked
            '    End If
            '    If admin.setting Then
            '        CheckBox5.Checked = CheckState.Checked
            '    End If
            'ElseIf ComboBox1.SelectedItem = "Technician" Then
            '    If tech.inv Then
            '        CheckBox1.Checked = CheckState.Checked
            '    End If
            '    If tech.log Then
            '        CheckBox3.Checked = CheckState.Checked
            '    End If
            '    If tech.rep Then
            '        CheckBox4.Checked = CheckState.Checked
            '    End If
            '    If tech.mas Then
            '        CheckBox2.Checked = CheckState.Checked
            '    End If
            '    If tech.setting Then
            '        CheckBox5.Checked = CheckState.Checked
            '    End If
            'ElseIf ComboBox1.SelectedItem = "Staff" Then
            '    If staff.inv Then
            '        CheckBox1.Checked = CheckState.Checked
            '    End If
            '    If staff.log Then
            '        CheckBox3.Checked = CheckState.Checked
            '    End If
            '    If staff.rep Then
            '        CheckBox4.Checked = CheckState.Checked
            '    End If
            '    If staff.mas Then
            '        CheckBox2.Checked = CheckState.Checked
            '    End If
            '    If staff.setting Then
            '        CheckBox5.Checked = CheckState.Checked
            '    End If
            'ElseIf ComboBox1.SelectedItem = "Engineer" Then
            '    If engineer.inv Then
            '        CheckBox1.Checked = CheckState.Checked
            '    End If
            '    If engineer.log Then
            '        CheckBox3.Checked = CheckState.Checked
            '    End If
            '    If engineer.rep Then
            '        CheckBox4.Checked = CheckState.Checked
            '    End If
            '    If engineer.mas Then
            '        CheckBox2.Checked = CheckState.Checked
            '    End If
            '    If engineer.setting Then
            '        CheckBox5.Checked = CheckState.Checked
            '    End If
            'End If
            For i = 0 To ptr - 1
                If ComboBox1.SelectedItem = setarr(i).user Then
                If setarr(i).pro Then
                    CheckBox1.Checked = CheckState.Checked
                End If

                If setarr(i).mas Then
                        CheckBox2.Checked = CheckState.Checked
                    End If
                    If setarr(i).setting Then
                        CheckBox5.Checked = CheckState.Checked
                    End If
                End If

            Next

        End Function

        Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
            Dim res = MessageBox.Show("Comfirm restore default settings which will allow EVERY user to access EVERYTHING?", "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
            If res = DialogResult.Yes Then
                'admin.inv = True
                'admin.log = True
                'admin.rep = True
                'admin.mas = True
                'admin.setting = True

                'tech.inv = True
                'tech.log = True
                'tech.rep = True
                'tech.mas = True
                'tech.setting = True

                'staff.inv = True
                'staff.log = True
                'staff.rep = True
                'staff.mas = True
                'staff.setting = True

                'engineer.inv = True
                'engineer.log = True
                'engineer.rep = True
                'engineer.mas = True
                'engineer.setting = True
                'admin.UpdateTable("Admin")
                'tech.UpdateTable("Technician")
                'staff.UpdateTable("Staff")
                'engineer.UpdateTable("Engineer")
                For i = 0 To ptr - 1
                setarr(i).pro = True
                setarr(i).mas = True
                    setarr(i).setting = True
                    setarr(i).UpdateTable(setarr(i).user)
                Next

            Else
                Exit Sub
            End If

        End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim res = MessageBox.Show("Comfirm Update User Access Settings?", "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            'admin.UpdateTable("Admin")
            'tech.UpdateTable("Technician")
            'staff.UpdateTable("Staff")
            'engineer.UpdateTable("Engineer")
            For i = 0 To ptr - 1
                setarr(i).UpdateTable(setarr(i).user)
            Next
            If cbxSkipCarton.CheckState = 1 Then
                WriteValue("System", "SkipCarton", 1, IniPath)
            Else
                WriteValue("System", "SkipCarton", 0, IniPath)
            End If

        Else
            Exit Sub
        End If

    End Sub

    Private Sub HelpBtn_Click(sender As Object, e As EventArgs) Handles HelpBtn.Click
            Dim f1 = New frmHelp
        f1.helptitle = "access"
        f1.Owner = Me
            f1.ShowDialog()
        End Sub

        Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
            Dim f1 = New frmHelp
        f1.helptitle = "config"
        f1.Owner = Me
            f1.ShowDialog()
        End Sub

        Private Sub Access_Click(sender As Object, e As EventArgs) Handles Access.Click

        End Sub




    'Private Sub CheckBox1_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    '    If Not ComboBox1.SelectedIndex >= 0 Then
    '        Exit Sub
    '    Else

    '    End If


    'End Sub
End Class


Public Class Setting
        Public user As String
    Public pro As Boolean
    Public mas As Boolean
        Public setting As Boolean

        Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
        Public Function UpdateTable(userlvl As String)
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            Dim SQLcmd = New SqlCommand
            If Conn.State = ConnectionState.Open Then
                SQLcmd.Connection = Conn
            SQLcmd.CommandText = "UPDATE [CRICUT].[CUPID].[Access]
                                                   SET 
                                                   [Production] = '" & pro & "'
                                                   ,[Master] = '" & mas & "'
                                                   ,[Setting] ='" & setting & "'
                                                   WHERE [User lvl] = '" & userlvl & "'"
            Dim cmd = New SqlCommand(SQLcmd.CommandText, Conn)
                cmd.ExecuteNonQuery()

                Conn.Close()
            End If
        End Function
    End Class
