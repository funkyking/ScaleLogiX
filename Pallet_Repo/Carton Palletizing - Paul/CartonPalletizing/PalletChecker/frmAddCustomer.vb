Imports System.Data.SqlClient



Public Class frmAddCustomer
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim CID As Guid

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs)
        statuslbl.Text = ""
        If Contact.Text <> "" And Not IsNumeric(Contact.Text) Then
            statuslbl.Text = "Invalid Contact No"
        End If
        If Postcode.Text <> "" And Not IsNumeric(Postcode.Text) Then
            statuslbl.Text = "Invalid Postcode"
        End If
        If Email.Text <> "" And (Not Email.Text.Contains("@") Or Not Email.Text.Contains(".com")) Then
            statuslbl.Text = "Invalid email"
        End If
        If CountryCode.Text <> "" And Not IsNumeric(CountryCode.Text) Then
            statuslbl.Text = "Invalid Country Code. Please Try Again."
        End If
    End Sub



    Private Sub frmAddCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CID = Guid.NewGuid
        Customer.Text = ""
        Company.Text = ""
        Contact.Text = ""
        Email.Text = ""
        Country.Text = ""
        CountryCode.Text = ""
        Street.Text = ""
        City.Text = ""
        Postcode.Text = ""
        Address.Text = ""
        If Text.Contains("Update") Then
            SaveBtn.Visible = False
            UpdateBtn.Visible = True
            UpdateBtn.Location = SaveBtn.Location
            LoadDataSQL()
            GroupBox1.Text = "Update Customer"
        Else
            SaveBtn.Visible = True
            UpdateBtn.Visible = False
            GroupBox1.Text = "Add New Customer"

        End If
    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs)
        CheckMissing()
        If statuslbl.Text = "" Then
            CheckDuplicateItem()
        Else
            Exit Sub
        End If
        If CheckDuplicateItem() Then
            Exit Sub
        End If
        Dim res = MessageBox.Show("Confirm Add Item?" & vbCrLf & "Customer: " & Customer.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            InsertDataSQL()
        Else
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Function LoadDataSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select 
                                  [Customer ID]
                                  ,[Company Name]
                                  ,[Customer Name]
                                  ,[Contact No]
                                  ,[Email]
                                  ,[Country]
                                  ,[Country Code]
                                  ,[Street]
                                  ,[City]
                                  ,[Postcode]
                                  ,[Address]
                          FROM [CRICUT].[CUPID].[CustomerMaster] WHERE [Index] = '" & frmCustomerMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "'AND [Delete]='False'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            While ds.Read
                CID = ds.Item("Customer ID")
                Customer.Text = ds.Item("Customer Name")
                Company.Text = ds.Item("Company Name")
                Contact.Text = ds.Item("Contact No")
                Email.Text = ds.Item("Email")
                Country.Text = ds.Item("Country")
                CountryCode.Text = ds.Item("Country Code")
                Street.Text = ds.Item("Street")
                City.Text = ds.Item("City")
                Postcode.Text = ds.Item("Postcode")
                Address.Text = ds.Item("Address")

            End While
        End If
        Conn.Close()
    End Function

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub UpdateBtn_Click(sender As Object, e As EventArgs)
        CheckMissing()
        If statuslbl.Text <> "" Then
            Exit Sub
        End If
        Dim res = MessageBox.Show("Confirm Update Item?" & vbCrLf & "Customer: " & Customer.Text, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
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
            SQLcmd.CommandText = "INSERT INTO [CUPID].[CustomerMaster]
                                               ([Customer ID]
                                               ,[Company Name]
                                               ,[Customer Name]
                                               ,[Contact No]
                                               ,[Email]
                                               ,[Country]
                                               ,[Country Code]
                                               ,[Street]
                                               ,[City]
                                               ,[Postcode]
                                               ,[Address]
                                               ,[Delete]
                                               ,[Modified Date])
                                         VALUES
                                               ('" & CID.ToString & "'
                                               ,'" & Company.Text & "'
                                               ,'" & Customer.Text & "'
                                               ,'" & Contact.Text & "'
                                               ,'" & Email.Text & "'
                                               ,'" & Country.Text & "'
                                               ,'" & CountryCode.Text & "'
                                               ,'" & Street.Text & "'
                                               ,'" & City.Text & "'
                                               ,'" & Postcode.Text & "'
                                               ,'" & Address.Text & "'
                                               ,'False'
                                               ,  ' " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"


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
            SQLcmd.CommandText = "UPDATE [CUPID].[CustomerMaster]
                                   SET
                                      [Company Name] = '" & Company.Text & "'
                                      ,[Customer Name] ='" & Customer.Text & "'
                                      ,[Contact No] = '" & Contact.Text & "'
                                      ,[Email] = '" & Email.Text & "'
                                      ,[Country] = '" & Country.Text & "'
                                      ,[Country Code] = '" & CountryCode.Text & "'
                                      ,[Street] = '" & Street.Text & "'
                                      ,[City] = '" & City.Text & "'
                                      ,[Postcode] = '" & Postcode.Text & "'
                                      ,[Address] = '" & Address.Text & "'
                                      ,[Modified Date] =  '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                 WHERE [Customer ID]='" & CID.ToString & "'"
            SQLcmd.ExecuteNonQuery()
            Conn.Close()
        End If
    End Function



    Private Function CheckDuplicateItem() As Boolean
        statuslbl.Text = ""
        Dim cond
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        If Text.Contains("Update") Then
            cond = "Not [Index]='" & frmCustomerMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "'AND"
        End If
        SQLcmd.CommandText = "Select *
                          FROM [CRICUT].[CUPID].[CustomerMaster] WHERE " & cond & "([Company Name] = '" & Company.Text & "'OR [Customer Name] = '" & Customer.Text & "'OR [Email] = '" & Email.Text & "') AND [Delete]='False'"

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
        SQLcmd.CommandText = "Select [Customer ID]
                          FROM [CRICUT].[CUPID].[CustomerMaster] WHERE [Customer ID] = '" & CID.ToString & "'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            CID = Guid.NewGuid
            Conn.Close()
            GoTo startingplace
        End If
        Conn.Close()

    End Function

    Private Sub Customer_TextChanged(sender As Object, e As EventArgs)
        CheckDuplicateItem()
    End Sub

    Private Function CheckMissing()
        statuslbl.Text = ""
        If Customer.Text = "" Then
            statuslbl.Text = "Customer field left empty. Please Try Again."

        End If
        If Company.Text = "" Then
            statuslbl.Text = "Company field left empty. Please Try Again."

        End If
        If Email.Text = "" Then
            statuslbl.Text = "Email field left empty. Please Try Again."

        End If

    End Function

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click

        Dim f1 = New frmHelp
        If Text.Contains("Update") Then
            f1.helptitle = "updatecus"

        Else
            f1.helptitle = "addcus"

        End If
        f1.Owner = Me
        f1.ShowDialog()
    End Sub
End Class