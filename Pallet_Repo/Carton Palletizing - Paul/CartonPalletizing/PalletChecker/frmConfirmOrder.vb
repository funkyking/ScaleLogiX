Imports System.Data.SqlClient

Public Class frmConfirmOrder
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Public WID As Guid
    Private Sub PrintOrderBtn_Click(sender As Object, e As EventArgs) Handles PrintOrderBtn.Click
        Dim f1 = New frmReport
        f1.Text = "Order Form"
        f1.r1 = GetReportData()
        f1.ShowDialog()
    End Sub

    Private Function GetReportData() As ReportData
        Dim r1 = New ReportData
        r1.PalletNo = Integer.Parse(PalletNo.Text)
        r1.Model = Model.Text
        r1.ProdDate = DateTime.Now.ToShortDateString
        r1.WorkOrder = WorkOrder.Text
        r1.Shift = Shift.Text
        r1.qty = Integer.Parse(Quantity.Text)
        r1.Address = Address.Text
        r1.CountryCode = CountryCode.Text
        r1.Line = Line.Text

        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "Select 
                                P.[Part No]
                      FROM [CRICUT].[CUPID].[WorkOrderMaster]  W
                      INNER JOIN [CRICUT].[CUPID].[PartMaster] P
                      ON W.[Part ID] = P.[Part ID]
                      WHERE (W.[Work Order ID]='" & WID.ToString & "' AND P.[Part Name]='" & PartNo.Text & "' AND W.[Delete]='False' )"

            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    r1.PartNo = ds.Item("Part No")
                End While
            End If
            ds.Close()
            SQLcmd.CommandText = "SELECT [Serial No],
		                                  [Carton] 
                            FROM [CUPID].[WorkOrder] 
                           
                            WHERE [Index]= (Select Min([Index]) From [CRICUT].[CUPID].[WorkOrder]
                                            WHERE [Work Order ID]='" & WID.ToString & "'  AND [Pallet No]='" & PalletNo.Text & "')"

            ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    r1.startserial = ds.Item("Serial No")
                    r1.startcarton = Integer.Parse(ds.Item("Carton"))
                End While
            End If
            ds.Close()
            SQLcmd.CommandText = "SELECT [Serial No],
		                                    [Carton] 
                            FROM [CUPID].[WorkOrder] 
                              WHERE [Index]= (Select Max([Index]) From [CRICUT].[CUPID].[WorkOrder]
                                            WHERE [Work Order ID]='" & WID.ToString & "'  AND [Pallet No]='" & PalletNo.Text & "')"
            ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    r1.endserial = ds.Item("Serial No")
                    r1.endcarton = Integer.Parse(ds.Item("Carton"))
                End While
            End If
            ds.Close()

        End If
        Return r1
    End Function

    Private Sub EditBtn_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub frmConfirmOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomerBox()

    End Sub

    Private Function LoadCustomerBox()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "Select 
                                [Customer Name]
                    FROM [CRICUT].[CUPID].[CustomerMaster] WHERE [Delete]='False'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    CustomerBox.Items.Add(ds.Item("Customer Name"))
                End While
            End If
        End If
        Conn.Close()
    End Function

    Private Sub CustomerBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CustomerBox.SelectedIndexChanged
        LoadCustomerInfo()
    End Sub

    Private Function LoadCustomerInfo()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "Select 
                                [Customer Name],
                                [Address],
                                [Country Code]
                    FROM [CRICUT].[CUPID].[CustomerMaster] WHERE [Customer Name]='" & CustomerBox.Text & "' AND[Delete]='False'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    Address.Text = ds.Item("Address")
                    CountryCode.Text = ds.Item("Country Code")
                End While
            End If
        End If
        Conn.Close()
    End Function

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Me.Close()
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click

    End Sub
End Class