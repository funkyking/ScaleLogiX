'Option Strict On

Imports System.Data.SqlClient

Imports System.IO
Imports System.Data
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Microsoft.Office.Interop
Imports System.Drawing.Printing
Imports Microsoft.Office.Interop.Excel
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports QRCoder



Imports System.Net

Public Class frmProduction
    Implements System.IDisposable

    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim localpath = (ReadValue("System", "localpath", IniPath))
    Dim palletcount As Integer
    Dim totalcount As Integer
    Dim maxpalletno As Integer = 0
    Dim Suffix As String
    Dim BarcodeLength As Integer
    Dim WID As Guid
    Dim done As Boolean
    Dim newlimit As Integer
    Dim m_currentPageIndex As Integer
    Dim m_streams As IList(Of Stream)
    Dim txtBoxes(9)
    Dim scanOption
    Dim ConnectionString As String
    Dim cn As New SqlConnection(ConnectionString)
    Dim dr As SqlDataAdapter
    Dim checkCarton As String
    Dim SkipCarton As Integer

    Private Sub frmProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Initialize()

    End Sub

    Private Function Initialize()
        AddBtn.Visible = False
        PalletBox.Items.Clear()
        cmbWorkOrderBox.Text = ""
        cmbWorkOrderBox.Items.Clear()
        LoadComboBox()
        txtS1.Text = ""
        Carton.Text = ""
        part.Text = ""
        lblLine.Text = ""
        statuslbl.Text = ""
        lblError.Text = ""
        lblOption.Text = ""
        txtS1.Enabled = False
        Carton.Enabled = False
        AddBtn.Enabled = False
        PalletBox.Text = ""
        PalletBox.Enabled = False
        Shift.Enabled = False
        btnSearch.Enabled = False
        cmbSubGroup.Enabled = False
        CancelBtn.Visible = False
        Label13.Visible = False
        scanstatuslbl.Visible = False
        ScanBtn.Enabled = False
        PrintOrderBtn.Enabled = False
        PrintReportBtn.Enabled = False
        btnQCcheck.Enabled = False
        btnQCin.Enabled = False
        btnQCout.Enabled = False
        lblOption.Visible = False
        Model.Text = ""
        qty.Text = 0
        count.Text = 0
        totalordercount.Text = 0
        lblDescription.Text = ""
        Order.Text = 0
        If TimeOfDay < #6:00:00PM# And TimeOfDay > #6:00:00AM# Then
            Shift.SelectedItem = "DAY"
        Else
            Shift.SelectedItem = "NIGHT"
        End If
        txtBoxes(0) = txtS1
        txtBoxes(1) = Carton
        txtBoxes(2) = txtS2
        txtBoxes(3) = txtC2
        txtBoxes(4) = txtS3
        txtBoxes(5) = txtC3
        txtBoxes(6) = txtS4
        txtBoxes(7) = txtC4
        txtBoxes(8) = txtS5
        txtBoxes(9) = txtC5
        For Each item In txtBoxes
            item.enabled = False
        Next




        SkipCarton = ReadValue("System", "SkipCarton", IniPath)
    End Function

    Private Sub LoadComboBox()

        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT DISTINCT [Work Order]
                              FROM [CRICUT].[CUPID].[WorkOrderMaster] WHERE [Delete]='False'"

            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    cmbWorkOrderBox.Items.Add(ds.Item("Work Order"))
                End While
            End If
            cmbWorkOrderBox.Sorted = True
            ds.Close()

        End If

        Conn.Close()

    End Sub

    Private Sub WorkOrderBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbWorkOrderBox.SelectedIndexChanged
        If Not cmbWorkOrderBox.SelectedItem Is Nothing Then
            'LoadInfo()
            PalletBox.Enabled = True
            Shift.Enabled = True
            btnSearch.Enabled = False
            btnQCcheck.Enabled = True
            btnQCin.Enabled = True
            btnQCout.Enabled = True
            btnSearch.Enabled = True
            txtSearch.Enabled = True
            cmbSubGroup.Enabled = True
        End If
        EnableScanPrint()
        done = False

        'Dim da As New SqlDataAdapter
        'Dim Conn = New SqlConnection(connstr)
        'Conn.Open()
        'If Conn.State = ConnectionState.Open Then
        '    Dim SQLcmd = New SqlCommand
        '    SQLcmd.Connection = Conn
        '    SQLcmd.CommandText = "SELECT [Sub Group]
        '                      From [CRICUT].[CUPID].[WorkOrderMaster] Where [Work Order] = '" + WorkOrderBox.Text + "'"

        '    SQLcmd.Parameters.AddWithValue("Sub Group", WorkOrderBox.SelectedValue)
        '    Dim ds = SQLcmd.ExecuteReader
        '    da.SelectCommand = SQLcmd
        '    If ds.HasRows Then
        '        While ds.Read
        '            SubGroup.Items.Add(ds.Item("Sub Group"))
        '        End While
        '    End If
        '    SubGroup.Sorted = True
        '    ds.Close()

        'End If

        'Conn.Close()

        Dim Conn = New SqlConnection(connstr)

        'Dim con1 As SqlConnection = New SqlConnection("Data Source=DESKTOP-7SPF34K\SQLEXPRESS2014;Initial Catalog=CRICUT;Integrated Security=True")

        Conn.Open()
        Dim strsql As String
        strsql = "Select [Sub Group] from [CRICUT].[CUPID].[WorkOrderMaster] where [Work Order]='" + cmbWorkOrderBox.Text + "'" 'And [Sub Group] is not null Order By [Sub Group] ASC"

        Dim cmd As New SqlCommand(strsql, Conn)
        Dim ds = cmd.ExecuteReader
        cmbSubGroup.Items.Clear()
        'cmbSubGroup = New ComboBox
        While ds.Read
            cmbSubGroup.Items.Add(ds.Item(0))
        End While
        Conn.Close()



    End Sub

    Private Function LoadInfo()
        PalletBox.Items.Clear()
        maxpalletno = 0
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "Select w.[Work Order ID] ,
                                         m.[Model],
                                         m.[Suffix],
                                         m.[BarcodeLength],
                                         w.[Quantity],
                                         w.[Count],
                                         w.[Total Order Count],
                                         p.[Part Name],
                                         l.[Line],
                                         w.[ScanOption],
                                         w.[Description],
                                         w.[Work Order]
                              FROM [CRICUT].[CUPID].[WorkOrderMaster] w
                              INNER JOIN [CRICUT].[CUPID].[ModelMaster] m
                                ON m.[Model ID]=w.[Model ID]
                              INNER JOIN [CRICUT].[CUPID].[PartMaster] p
                                ON p.[Part ID]=w.[Part ID]
                              INNER JOIN [CRICUT].[CUPID].[LineMaster] l
                                ON l.[LineID] = w.[LineID]
                                 WHERE [Sub Group]='" & cmbSubGroup.SelectedItem & "' AND w.[Work Order]='" & cmbWorkOrderBox.Text & "'" 'AND [Pallet No]='" & PalletBox.SelectedItem & "' 'AND [Work Order]='" & cmbWorkOrderBox.SelectedItem & "'"

            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    WID = ds.Item("Work Order ID")
                    Model.Text = ds.Item("Model")
                    Suffix = ds.Item("Suffix")
                    BarcodeLength = ds.Item("BarcodeLength")
                    qty.Text = ds.Item("Quantity")
                    Order.Text = ds.Item("Total Order Count")
                    part.Text = ds.Item("Part Name")
                    totalordercount.Text = ds.Item("Count")
                    lblLine.Text = ds.Item("Line")
                    scanOption = ds.Item("ScanOption")
                    lblDescription.Text = ds.Item("Description")
                End While
            End If
            ds.Close()
            SQLcmd.CommandText = "SELECT [Pallet No]
                              FROM [CRICUT].[CUPID].[WorkOrder] p
                              INNER JOIN [CRICUT].[CUPID].[WorkOrderMaster] w
                              ON p.[Work Order ID]=w.[Work Order ID]
                              WHERE [Sub Group] = '" & cmbSubGroup.Text & "' AND w.[Work Order]='" & cmbWorkOrderBox.Text & "'"


            '"Select [Pallet No]
            '                  From [CRICUT].[CUPID].[WorkOrder] p
            '                  INNER JOIN [CRICUT].[CUPID].[WorkOrderMaster] w
            '                   ON p.[Work Order ID]=w.[Work Order ID]
            '                    WHERE [Work Order ID]='" & WID.ToString & "' AND [Sub Group]='" & cmbSubGroup.SelectedItem & "'"
            ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    For Each items In PalletBox.Items
                        If items = ds.Item("Pallet No") Then
                            GoTo skippallet
                        End If

                    Next

                    PalletBox.Items.Add(ds.Item("Pallet No"))
                    If maxpalletno < ds.Item("Pallet No") Then
                        maxpalletno = ds.Item("Pallet No")
                    End If
skippallet:
                End While
                PalletBoxOrder()
            End If
        End If

        Conn.Close()
        CheckPalletCount(maxpalletno)
        NextPallet()
        UpdateScanOption()


    End Function

    Private Function PalletBoxOrder()
        PalletBox.Sorted = True
    End Function

    Private Function EnableScanPrint()
        If Not cmbWorkOrderBox.SelectedItem Is Nothing And Not PalletBox.SelectedItem Is Nothing Then
            ScanBtn.Enabled = True
            PrintOrderBtn.Enabled = True
            PrintReportBtn.Enabled = True
            LoadGrid()
        End If
    End Function


    Private Sub ScanBtn_Click(sender As Object, e As EventArgs) Handles ScanBtn.Click
        CheckComplete()

        If Integer.Parse(totalordercount.Text) >= Integer.Parse(Order.Text) Then
            MessageBox.Show("Order Already Completed!", "Order Complete", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If Order.Text = 0 Or qty.Text = 0 Then
            statuslbl.Text = "The order or pallet quantity is not set. Please Try Again."
            Exit Sub
        End If
        If cmbWorkOrderBox.Text <> Nothing Then
            For Each item In cmbWorkOrderBox.Items
                If item = cmbWorkOrderBox.Text Then
                    GoTo here
                End If
            Next
            MessageBox.Show("Work Order, " & cmbWorkOrderBox.Text & " unavailable." & vbCrLf & vbTab & "Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Initialize()
            Exit Sub
        End If
here:
        PalletBox.SelectedItem = 1
        Timer1.Start()
        'added here
        PalletBox.SelectedItem = maxpalletno
        CheckPalletMax()


        cmbWorkOrderBox.Enabled = False
        PalletBox.Enabled = False
        Shift.Enabled = False
        CancelBtn.Visible = True
        ScanBtn.Visible = False
        Label13.Visible = True
        scanstatuslbl.Visible = True
        txtS1.Enabled = True

        If SkipCarton = 1 Then
            Carton.Text = 0
            Carton.Enabled = False
            txtC2.Text = 0
            txtC2.Enabled = False
            txtC3.Text = 0
            txtC3.Enabled = False
            txtC4.Text = 0
            txtC4.Enabled = False
            txtC5.Text = 0
            txtC5.Enabled = False
        Else
            Carton.Enabled = True
        End If


        txtS1.Focus()
        GetID()

    End Sub

    Private Sub Shift_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Shift.SelectedIndexChanged
        EnableScanPrint()
    End Sub

    Private Sub PalletBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PalletBox.SelectedIndexChanged
        EnableScanPrint()
        CheckPalletCount(PalletBox.SelectedItem)
    End Sub


    Private Function CheckPalletCount(query As String)
        If query = 0 Then
            maxpalletno = 1
            PalletBox.Items.Add(maxpalletno)
            PalletBox.SelectedItem = maxpalletno
            Exit Function
        End If
        Dim tempcnt As Integer
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT COUNT (w.[Serial No]) As [cnt]
                              FROM [CRICUT].[CUPID].[WorkOrderMaster] wm
                              INNER JOIN [CRICUT].[CUPID].[WorkOrder] w
                              ON wm.[Work Order ID]=w.[Work Order ID]
                                WHERE wm.[Work Order]='" & cmbWorkOrderBox.SelectedItem & "' AND wm.[Sub Group]='" & cmbSubGroup.SelectedItem & "' AND w.[Pallet No]='" & query & "' AND wm.[Delete]='False'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    tempcnt = ds.Item("cnt")
                End While
            End If
        End If
        PalletBox.SelectedItem = query
        count.Text = tempcnt
        'NextPallet()
    End Function

    Private Function NextPallet()
        Dim tempcnt As Integer
        If Integer.Parse(totalordercount.Text) < Integer.Parse(Order.Text) Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "SELECT COUNT (w.[Serial No]) As [cnt]
                                      FROM [CRICUT].[CUPID].[WorkOrderMaster] wm
                                      INNER JOIN [CRICUT].[CUPID].[WorkOrder] w
                                      ON wm.[Work Order ID]=w.[Work Order ID]
                                        WHERE wm.[Work Order]='" & cmbWorkOrderBox.SelectedItem & "' AND wm.[Sub Group]='" & cmbSubGroup.SelectedItem & "' AND w.[Pallet No]='" & PalletBox.Text & "' AND wm.[Delete]='False'"
                Dim ds = SQLcmd.ExecuteReader
                If ds.HasRows Then
                    While ds.Read
                        tempcnt = ds.Item("cnt")

                    End While
                End If
            End If
            If tempcnt >= Integer.Parse(qty.Text) And count.Text >= qty.Text Then
                maxpalletno += 1
                PalletBox.Items.Add(maxpalletno)
                PalletBox.SelectedItem = maxpalletno
                count.Text = 0


            End If

        End If

    End Function

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        cmbWorkOrderBox.Enabled = True
        PalletBox.Enabled = True
        Shift.Enabled = True
        CancelBtn.Visible = False
        ScanBtn.Visible = True
        Label13.Visible = False
        scanstatuslbl.Visible = False
        Timer1.Stop()
        statuslbl.Text = ""
        lblError.Text = ""
    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'add after clicking start scanning btn

        'this add in txt chg also or can add in timer
        scanstatuslbl.Text = "Scanning In Progress... (" & totalordercount.Text & "/" & Order.Text & ")"
        CheckPalletMax()
        'add in txt chg 

    End Sub


    Private Function PrintOrder()
        Dim report = New LocalReport
        Dim r1 = New ReportData
        r1 = GetReportData()
        Dim param(11) As ReportParameter
        param(0) = New ReportParameter("PalletNo", r1.PalletNo)
        param(1) = New ReportParameter("PartNo", r1.PartNo)
        param(2) = New ReportParameter("WorkOrder", r1.WorkOrder)
        param(3) = New ReportParameter("ProdDate", DateTime.Now.ToString)
        param(4) = New ReportParameter("Model", r1.Model)
        param(5) = New ReportParameter("qty", r1.qty)
        param(6) = New ReportParameter("startcarton", r1.startcarton)
        param(7) = New ReportParameter("endcarton", r1.endcarton)
        param(8) = New ReportParameter("Line", r1.Line)
        param(9) = New ReportParameter("totalcarton", r1.totalcarton)
        param(10) = New ReportParameter("Shift", r1.Shift)
        param(11) = New ReportParameter("Desc", r1.Desc)
        report.ReportEmbeddedResource = "CartonPalletizing.reportCUPID.rdlc"
        report.SetParameters(param)
        Export(report)
        Print()
    End Function

    Private Function GetID()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT w.[Work Order ID]
    
                              FROM [CRICUT].[CUPID].[WorkOrderMaster] w
                              INNER JOIN [CRICUT].[CUPID].[PartMaster] p
                                ON w.[Part ID]= p.[Part ID]
                                WHERE w.[Work Order]='" & cmbWorkOrderBox.SelectedItem & "' AND  w.[Sub Group]='" & cmbSubGroup.SelectedItem & "' AND p.[Part Name]='" & part.Text & "' AND p.[Delete]='False' AND w.[Delete]='False'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    WID = ds.Item("Work Order ID")
                End While
            End If
        End If
        Conn.Close()
    End Function



    'error 2
    Private Function CheckDuplicateSerial(SerialNo As String)
        Dim res As Boolean
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            'SQLcmd.CommandText = "SELECT *
            'From [CRICUT].[CUPID].[WorkOrder] WHERE [Work Order ID]='" & WID.ToString & "'AND  [Serial No]='" & SerialNo & "' "
            SQLcmd.CommandText = "SELECT w.[Pallet No],w.[Carton],w.[Serial No],wm.[Work Order],wm.[Sub Group]
                        
                                      FROM [CRICUT].[CUPID].[WorkOrderMaster] wm
                                      INNER JOIN [CRICUT].[CUPID].[WorkOrder] w
                                      ON wm.[Work Order ID]=w.[Work Order ID]
                                      WHERE w.[Serial No] = '" & SerialNo & "'"

            'remove wm.[Work Order] = '" & cmbWorkOrderBox.Text & "'And wm.[Sub Group]='" & cmbSubGroup.Text & "' And
            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                ds.Read()
                res = False
                'statuslbl.Text = (" Serial No : " & (ds.Item("Serial No")))
                statuslbl.Text = "Duplicate Serial No Found..." & vbCr & "PO No : " & ds.Item("Work Order") & vbCr & "MR No. : " & ds.Item("Sub Group") & vbCr & "Pallet No : " & ds.Item("Pallet No") & vbCr & "Carton No : " & ds.Item("Carton")
                'statuslbl.Text = "Duplicate Carton"
            Else
                res = True
                statuslbl.Text = ""
                lblError.Text = ""
            End If
        End If
        Conn.Close()
        Return res
    End Function


    Private Function CheckDuplicateCarton(carton As String)
        Dim res As Boolean
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "Select [Serial No]
                              FROM [CRICUT].[CUPID].[WorkOrder] WHERE [Work Order ID]='" & WID.ToString & "' AND [Carton]='" & carton & "' "
            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                res = False
            Else
                res = True
            End If
        End If
        Conn.Close()
        Return res
    End Function

    Private Function InsertDataSQL(serial As String, carton As String)
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        If Conn.State = ConnectionState.Open Then
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "INSERT INTO [CRICUT].[CUPID].[WorkOrder]
                                               ([Work Order ID],
                                                [Line]
                                               ,[Serial No]
                                               ,[Carton]
                                               ,[Pallet No]
                                               ,[Production Date]
                                               ,[Shift]
                                               ,[QCout]
                                               ,[QCin])
                                             VALUES
                                               ('" & WID.ToString & "'
                                                ,'" & lblLine.Text & "'
                                                ,'" & serial & "'
                                                ,'" & carton & "'
                                                ,'" & PalletBox.SelectedItem & "'
                                                ,'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                                ,'" & Shift.SelectedItem & "'
                                                ,'False'
                                                ,'False')"
            Dim cmd = New SqlCommand(SQLcmd.CommandText, Conn)
            cmd.ExecuteNonQuery()

            Conn.Close()
        End If
    End Function

    Private Function UpdateCountSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "UPDATE  [CRICUT].[CUPID].[WorkOrderMaster] 
                                             SET [Count]='" & Integer.Parse(totalordercount.Text) & "'
                                     
                                     WHERE [Work Order ID] = '" & WID.ToString & "' "
            SQLcmd.ExecuteNonQuery()
            Conn.Close()
        End If

    End Function

    Private Function UpdateCompleteSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "UPDATE  [CRICUT].[CUPID].[WorkOrderMaster] 
                                             SET [Completed]='True'
                                     
                                     WHERE [Work Order ID] = '" & WID.ToString & "' "
            SQLcmd.ExecuteNonQuery()
            Conn.Close()
        End If
    End Function

    'newly added
    Private Function CheckComplete()
        If Order.Text > totalordercount.Text Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "UPDATE  [CRICUT].[CUPID].[WorkOrderMaster] 
                                             SET [Completed]='False'
                                     
                                     WHERE [Work Order ID] = '" & WID.ToString & "' "
                SQLcmd.ExecuteNonQuery()
                Conn.Close()
            End If
        End If
    End Function


    Private Function LoadGrid()
        GetID()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        Dim strsql As String
        If SkipCarton = 0 Then
            strsql = "Select [Index] As [No],
                                [Serial No] As [Serial No],
                                [Carton] As [Carton]
                       
                      FROM [CRICUT].[CUPID].[WorkOrder]  
                   
                      WHERE ([Work Order ID]='" & WID.ToString & "' AND [Pallet No]= " & PalletBox.SelectedItem & " )
                      ORDER BY [Production Date] DESC"
        Else
            strsql = "Select [Index] As [No],
                                [Serial No] As [Serial No]
                                
                       
                      FROM [CRICUT].[CUPID].[WorkOrder]  
                   
                      WHERE ([Work Order ID]='" & WID.ToString & "' AND [Pallet No]= " & PalletBox.SelectedItem & " )
                      ORDER BY [Production Date] DESC"
        End If


        Dim dt = New System.Data.DataTable
        dt.Clear()
        Dim da = New SqlDataAdapter(strsql, Conn)
        da.Fill(dt)
        da.Dispose()
        DataGridView1.DataSource = dt
        DataGridView1.ClearSelection()
        Conn.Close()
    End Function


    Private Sub PrintOrderBtn_Click(sender As Object, e As EventArgs) Handles PrintOrderBtn.Click
        Dim frm1 As New frmReport
        frm1.Text = "Order Form"
        frm1.r1 = GetReportData()

        frm1.ShowDialog()
    End Sub

    Private Sub PrintReportBtn_Click(sender As Object, e As EventArgs) Handles PrintReportBtn.Click
        reprint()
        ' Reset the PalletCount and index to 0
        totalpalletcount = 0
        pli = 0
    End Sub


    Dim totalpalletcount = 0 'Total Pallet Count
    Dim pli = 0 'Pallet Index
    Private Function GetReportData() As ReportData
        Dim r1 = New ReportData
        r1.SubGroup = cmbSubGroup.SelectedItem.ToString()
        r1.WID = WID

        totalpalletcount = cmbSubGroup.Items.Count
        If pli IsNot totalpalletcount Then
            pli += 1
            r1.PalletNo = pli.ToString()
        End If

        'r1.PalletNo = Integer.Parse(PalletBox.Text)
        r1.Model = Model.Text
        r1.ProdDate = DateTime.Now
        r1.WorkOrder = cmbWorkOrderBox.Text
        r1.Shift = Shift.Text
        r1.Line = lblLine.Text
        r1.Description = lblDescription.Text
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "Select 
                                P.[Part No],
                                w.[Quantity],
                                w.[Total Order Count]
                      FROM [CRICUT].[CUPID].[WorkOrderMaster]  W
                      INNER JOIN [CRICUT].[CUPID].[PartMaster] P
                      ON W.[Part ID] = P.[Part ID]
                      WHERE (W.[Work Order ID]='" & WID.ToString & "' AND P.[Part Name]='" & part.Text & "' AND W.[Delete]='False' )"

            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    r1.PartNo = ds.Item("Part No")
                    r1.qty = ds.Item("Quantity")
                    r1.totalcarton = ds.Item("Total Order Count")
                End While
            End If
            ds.Close()
            SQLcmd.CommandText =
                                "SELECT [Serial No],
                                        [Carton],
                                        [Pallet NO] 
                            FROM [CUPID].[WorkOrder] 
                            WHERE [Index]= (Select Min([Index]) From [CRICUT].[CUPID].[WorkOrder] 
                            WHERE [Work Order ID]='" & WID.ToString & "' AND [Pallet No]='" & PalletBox.Text & "')"

            '"SELECT P.[Serial No],
            '                        P.[Carton] 
            '                FROM [CUPID].[WorkOrder] P 
            '                INNER JOIN [CRICUT].[CUPID].[WorkOrderMaster]  W
            '                ON P.[Work Order ID] = W.[Work Order ID]

            '              WHERE P.[Work Order ID]='" & WID.ToString & "'AND W.[Sub Group]='" & cmbSubGroup.Text & "'"




            ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    r1.startserial = ds.Item("Serial No")
                    r1.startcarton = Integer.Parse(ds.Item("Carton"))

                End While
            End If
            ds.Close()
            SQLcmd.CommandText =
                                       "SELECT [Serial No],
                                               [Carton] 
                                  FROM [CUPID].[WorkOrder] 
                                  WHERE [Index]= (Select MAX([Index]) From [CRICUT].[CUPID].[WorkOrder]
                                  WHERE [Work Order ID]='" & WID.ToString & "' AND [Pallet No]='" & PalletBox.Text & "')"

            '"SELECT P.[Serial No],
            '                        P.[Carton] 
            '                FROM [CUPID].[WorkOrder] P 
            '                INNER JOIN [CRICUT].[CUPID].[WorkOrderMaster]  W
            '                ON P.[Work Order ID] = W.[Work Order ID]

            '              WHERE P.[Work Order ID]='" & WID.ToString & "'AND W.[Sub Group]='" & cmbSubGroup.Text & "'"



            ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    r1.endserial = ds.Item("Serial No")
                    r1.endcarton = Integer.Parse(ds.Item("Carton"))
                End While
            End If
            ds.Close()

            SQLcmd.CommandText = "SELECT [Description]
                            FROM [CUPID].[ModelMaster] 
                              WHERE [Model]= '" & Model.Text & "'"
            ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    r1.Desc = ds.Item("Description")
                End While
            End If
            ds.Close()

        End If
        Return r1
    End Function

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim tmpcnt
        Dim tmptotalcount
        Dim res = MessageBox.Show("Confirm Delete Item" & vbCrLf & "Serial No: " & DataGridView1.CurrentRow.Cells("Serial No").Value.ToString & vbCrLf & "Carton: " & DataGridView1.CurrentRow.Cells("Carton").Value.ToString, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "DELETE FROM [CUPID].[WorkOrder]
                                 WHERE [Index]='" & DataGridView1.CurrentRow.Cells("No").Value.ToString & "'"
                SQLcmd.ExecuteNonQuery()
                Conn.Close()
            End If
        Else
            Exit Sub
        End If
        tmpcnt = Integer.Parse(count.Text)
        tmptotalcount = Integer.Parse(totalordercount.Text)
        tmpcnt -= 1
        tmptotalcount -= 1
        count.Text = tmpcnt
        totalordercount.Text = tmptotalcount
        UpdateCountSQL()
        LoadGrid()
        txtSearch.Text = ""
        CheckComplete()
    End Sub

    Private Function CheckPalletMax()
        Label13.Text = "Pallet " & PalletBox.SelectedItem & " ...(" & count.Text & "/" & qty.Text & ")"

    End Function

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim f1 = New frmHelp

        f1.helptitle = "prod"

        f1.Owner = Me
        f1.ShowDialog()
    End Sub


    Private WithEvents xlApp As Excel.Application
    Dim sourceWorkBook As Excel.Workbook
    Dim sourceWorkSheet As Excel.Worksheet
    Dim misValue As Object = System.Reflection.Missing.Value

    Public Function PrintReport()
        If Model.Text.ToUpper.Contains("SUMMER") Then
            PrintReportSummer()

        Else

            Dim localdir = IO.Directory.GetParent(IO.Directory.GetParent(Directory.GetCurrentDirectory).ToString).ToString
            Dim r1 As ReportData
            r1 = GetReportData()
            xlApp = New Excel.Application
            sourceWorkBook = xlApp.Workbooks.Open(localdir & "\" & "CartonPalletizingReport.xlsx")
            sourceWorkSheet = sourceWorkBook.Worksheets(1)
            'sourceWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape
            'sourceWorkSheet.PageSetup.TopMargin = 0
            sourceWorkSheet.Range("D3").Value = r1.PalletNo
            sourceWorkSheet.Range("D4").Value = r1.PartNo
            sourceWorkSheet.Range("D5").Value = r1.WorkOrder
            sourceWorkSheet.Range("D6").Value = r1.ProdDate.ToString("dd/MM/yyyy")
            sourceWorkSheet.Range("D7").Value = r1.Shift
            sourceWorkSheet.Range("D8").Value = r1.Model
            sourceWorkSheet.Range("D9").Value = r1.qty
            sourceWorkSheet.Range("D10").Value = r1.Line
            sourceWorkSheet.Range("B10").Value = r1.startserial
            sourceWorkSheet.Range("B11").Value = r1.startcarton
            sourceWorkSheet.Range("E10").Value = r1.endserial
            sourceWorkSheet.Range("E11").Value = r1.endcarton
            sourceWorkSheet.Range("H4").Value = r1.Description
            Dim c1 As Range = sourceWorkSheet.Range("B13:C52")
            Dim c2 As Range = sourceWorkSheet.Range("E13:F52")
            Dim c3 As Range = sourceWorkSheet.Range("H13:I52")

            Dim cnt As Integer = 0
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "SELECT [Serial No],
		                                  [Carton] 
                            FROM [CUPID].[WorkOrder] 
                             WHERE [Work Order ID]='" & WID.ToString & "'  AND [Pallet No]='" & PalletBox.Text & "' ORDER BY [Index] ASC"

                Dim ds = SQLcmd.ExecuteReader
                Dim i1 = 1
                Dim i2 = 1
                Dim i3 = 1
                If ds.HasRows Then
                    While ds.Read
                        cnt += 1
                        If cnt <= 40 Then

                            c1.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 40 And cnt <= 80 Then

                            c2.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 80 And cnt <= 120 Then

                            c3.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        End If
                    End While
                End If
                Conn.Close()
                sourceWorkSheet.Range("B13:C52").Value = c1.Value
                sourceWorkSheet.Range("E13:F52").Value = c2.Value
                sourceWorkSheet.Range("H13:I52").Value = c3.Value
            End If

            sourceWorkBook.Worksheets.PrintOutEx(1, sourceWorkBook.Worksheets.Count, 1, False, Type.Missing, False, False, Type.Missing, True)

            Try
                sourceWorkBook.Close(False)
                xlApp.Quit()
                releaseObject(xlApp)
                releaseObject(sourceWorkBook)
                releaseObject(sourceWorkSheet)
            Catch ex As Exception

            End Try
        End If
    End Function

    Public Function PrintReportSummer()


        Dim localdir = IO.Directory.GetParent(IO.Directory.GetParent(Directory.GetCurrentDirectory).ToString).ToString
            Dim r1 As ReportData
            r1 = GetReportData()
            xlApp = New Excel.Application
        sourceWorkBook = xlApp.Workbooks.Open(localdir & "\" & "CartonPalletizingReportSummer.xlsx")
        sourceWorkSheet = sourceWorkBook.Worksheets(1)
            'sourceWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape
            'sourceWorkSheet.PageSetup.TopMargin = 0
            sourceWorkSheet.Range("D3").Value = r1.PalletNo
            sourceWorkSheet.Range("D4").Value = r1.PartNo
            sourceWorkSheet.Range("D5").Value = r1.WorkOrder
            sourceWorkSheet.Range("D6").Value = r1.ProdDate.ToString("dd/MM/yyyy")
            sourceWorkSheet.Range("D7").Value = r1.Shift
            sourceWorkSheet.Range("D8").Value = r1.Model
            sourceWorkSheet.Range("D9").Value = r1.qty
            sourceWorkSheet.Range("D10").Value = r1.Line
            sourceWorkSheet.Range("B10").Value = r1.startserial
            sourceWorkSheet.Range("B11").Value = r1.startcarton
            sourceWorkSheet.Range("E10").Value = r1.endserial
            sourceWorkSheet.Range("E11").Value = r1.endcarton
            sourceWorkSheet.Range("H4").Value = r1.Description
            Dim c1 As Range = sourceWorkSheet.Range("B13:C52")
            Dim c2 As Range = sourceWorkSheet.Range("E13:F52")
            Dim c3 As Range = sourceWorkSheet.Range("H13:I52")

            Dim cnt As Integer = 0
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "SELECT [Serial No],
		                                  [Carton] 
                            FROM [CUPID].[WorkOrder] 
                             WHERE [Work Order ID]='" & WID.ToString & "'  AND [Pallet No]='" & PalletBox.Text & "' ORDER BY [Index] ASC"

                Dim ds = SQLcmd.ExecuteReader
                Dim i1 = 1
                Dim i2 = 1
                Dim i3 = 1
                If ds.HasRows Then
                    While ds.Read
                        cnt += 1
                        If cnt <= 40 Then

                            c1.Cells(i1, 1).Value = ds.Item("Serial No")
                            c1.Cells(i1, 2).Value = ds.Item("Carton")
                            i1 += 1
                        ElseIf cnt > 40 And cnt <= 80 Then

                            c2.Cells(i2, 1).Value = ds.Item("Serial No")
                            c2.Cells(i2, 2).Value = ds.Item("Carton")
                            i2 += 1
                        ElseIf cnt > 80 And cnt <= 120 Then

                            c3.Cells(i3, 1).Value = ds.Item("Serial No")
                            c3.Cells(i3, 2).Value = ds.Item("Carton")
                            i3 += 1
                        End If
                    End While
                End If
                Conn.Close()
                sourceWorkSheet.Range("B13:C52").Value = c1.Value
                sourceWorkSheet.Range("E13:F52").Value = c2.Value
                sourceWorkSheet.Range("H13:I52").Value = c3.Value
            End If

            sourceWorkBook.Worksheets.PrintOutEx(1, sourceWorkBook.Worksheets.Count, 1, False, Type.Missing, False, False, Type.Missing, True)

            Try
                sourceWorkBook.Close(False)
                xlApp.Quit()
                releaseObject(xlApp)
                releaseObject(sourceWorkBook)
                releaseObject(sourceWorkSheet)
            Catch ex As Exception

            End Try

    End Function

    Public Function PrintReportAndStoreExcel()
        'If CheckBox1.Checked = True Then
        '    reprint()
        '    Exit Function
        'End If
        Dim xlp() As Process = Process.GetProcessesByName("EXCEL")
        For Each Process As Process In xlp
            Process.Kill()
        Next
        Dim datestart As Date = Date.Now

        scanstatuslbl.Visible = True
        scanstatuslbl.Text = "Populating Data and Opening Excel File..."
        ScanBtn.Visible = False
        Dim localdir = IO.Directory.GetParent(IO.Directory.GetParent(Directory.GetCurrentDirectory).ToString).ToString
        Dim r1 As ReportData
        r1 = GetReportData()
        xlApp = New Excel.Application
        sourceWorkBook = xlApp.Workbooks.Open(localdir & "\" & "CartonPalletizingReport.xlsm")
        sourceWorkSheet = sourceWorkBook.Worksheets(1)
        sourceWorkSheet.Name = ("Pallet " & r1.PalletNo)
        sourceWorkSheet.Range("D3").Value = r1.PalletNo
        sourceWorkSheet.Range("D4").Value = r1.PartNo
        sourceWorkSheet.Range("D5").Value = r1.WorkOrder
        sourceWorkSheet.Range("D6").Value = r1.ProdDate.ToString("dd/MM/yyyy")
        sourceWorkSheet.Range("D7").Value = r1.Shift
        sourceWorkSheet.Range("D8").Value = r1.Model
        sourceWorkSheet.Range("D9").Value = r1.qty
        sourceWorkSheet.Range("D10").Value = r1.Line
        sourceWorkSheet.Range("B10").Value = r1.startserial
        sourceWorkSheet.Range("B11").Value = r1.startcarton
        sourceWorkSheet.Range("E10").Value = r1.endserial
        sourceWorkSheet.Range("E11").Value = r1.endcarton
        sourceWorkSheet.Range("H4").Value = r1.Description
        Dim c1 As Range = sourceWorkSheet.Range("B13:C52")
        Dim c2 As Range = sourceWorkSheet.Range("E13:F52")
        Dim c3 As Range = sourceWorkSheet.Range("H13:I52")

        Dim c1_2 As Range = sourceWorkSheet.Range("B55:C104")
        Dim c2_2 As Range = sourceWorkSheet.Range("E55:F104")
        Dim c3_2 As Range = sourceWorkSheet.Range("H55:I104")

        Dim c1_3 As Range = sourceWorkSheet.Range("B109:C158")
        Dim c2_3 As Range = sourceWorkSheet.Range("E109:F158")
        Dim c3_3 As Range = sourceWorkSheet.Range("H109:I158")

        Dim c1_4 As Range = sourceWorkSheet.Range("B163:C212")
        Dim c2_4 As Range = sourceWorkSheet.Range("E163:F212")
        Dim c3_4 As Range = sourceWorkSheet.Range("H163:I212")

        Dim c1_5 As Range = sourceWorkSheet.Range("B217:C266")
        Dim c2_5 As Range = sourceWorkSheet.Range("E217:F266")
        Dim c3_5 As Range = sourceWorkSheet.Range("H217:I266")

        Dim c1_6 As Range = sourceWorkSheet.Range("B271:C320")
        Dim c2_6 As Range = sourceWorkSheet.Range("E271:F320")
        Dim c3_6 As Range = sourceWorkSheet.Range("H271:I320")

        Dim c1_7 As Range = sourceWorkSheet.Range("B325:C374")
        Dim c2_7 As Range = sourceWorkSheet.Range("E325:F374")
        Dim c3_7 As Range = sourceWorkSheet.Range("H325:I374")

        Dim c1_8 As Range = sourceWorkSheet.Range("B379:C428")
        Dim c2_8 As Range = sourceWorkSheet.Range("E379:F428")
        Dim c3_8 As Range = sourceWorkSheet.Range("H379:I428")

        Dim c1_9 As Range = sourceWorkSheet.Range("B433:C482")
        Dim c2_9 As Range = sourceWorkSheet.Range("E433:F482")
        Dim c3_9 As Range = sourceWorkSheet.Range("H433:I482")

        Dim c1_10 As Range = sourceWorkSheet.Range("B487:C536")
        Dim c2_10 As Range = sourceWorkSheet.Range("E487:F536")
        Dim c3_10 As Range = sourceWorkSheet.Range("H487:I536")

        Dim c1_11 As Range = sourceWorkSheet.Range("B541:C590")
        Dim c2_11 As Range = sourceWorkSheet.Range("E541:F590")
        Dim c3_11 As Range = sourceWorkSheet.Range("H541:I590")

        Dim c1_12 As Range = sourceWorkSheet.Range("B595:C644")
        Dim c2_12 As Range = sourceWorkSheet.Range("E595:F644")
        Dim c3_12 As Range = sourceWorkSheet.Range("H595:I644")

        Dim c1_13 As Range = sourceWorkSheet.Range("B649:C698")
        Dim c2_13 As Range = sourceWorkSheet.Range("E649:F698")
        Dim c3_13 As Range = sourceWorkSheet.Range("H649:I698")

        Dim c1_14 As Range = sourceWorkSheet.Range("B703:C752")
        Dim c2_14 As Range = sourceWorkSheet.Range("E703:F752")
        Dim c3_14 As Range = sourceWorkSheet.Range("H703:I752")

        Dim c1_15 As Range = sourceWorkSheet.Range("B757:C806")
        Dim c2_15 As Range = sourceWorkSheet.Range("E757:F806")
        Dim c3_15 As Range = sourceWorkSheet.Range("H757:I806")

        Dim c1_16 As Range = sourceWorkSheet.Range("B811:C860")
        Dim c2_16 As Range = sourceWorkSheet.Range("E811:F860")
        Dim c3_16 As Range = sourceWorkSheet.Range("H811:I860")

        Dim c1_17 As Range = sourceWorkSheet.Range("B865:C914")
        Dim c2_17 As Range = sourceWorkSheet.Range("E865:F914")
        Dim c3_17 As Range = sourceWorkSheet.Range("H865:I914")

        Dim c1_18 As Range = sourceWorkSheet.Range("B919:C968")
        Dim c2_18 As Range = sourceWorkSheet.Range("E919:F968")
        Dim c3_18 As Range = sourceWorkSheet.Range("H919:I968")

        Dim c1_19 As Range = sourceWorkSheet.Range("B973:C1022")
        Dim c2_19 As Range = sourceWorkSheet.Range("E973:F1022")
        Dim c3_19 As Range = sourceWorkSheet.Range("H973:I1022")

        Dim c1_20 As Range = sourceWorkSheet.Range("B1027:C1076")
        Dim c2_20 As Range = sourceWorkSheet.Range("E1027:F1076")
        Dim c3_20 As Range = sourceWorkSheet.Range("H1027:I1076")

        Dim c1_21 As Range = sourceWorkSheet.Range("B1081:C1130")
        Dim c2_21 As Range = sourceWorkSheet.Range("E1081:F1130")
        Dim c3_21 As Range = sourceWorkSheet.Range("H1081:I1130")

        Dim c1_22 As Range = sourceWorkSheet.Range("B1135:C1184")
        Dim c2_22 As Range = sourceWorkSheet.Range("E1135:F1184")
        Dim c3_22 As Range = sourceWorkSheet.Range("H1135:I1184")

        Dim c1_23 As Range = sourceWorkSheet.Range("B1189:C1238")
        Dim c2_23 As Range = sourceWorkSheet.Range("E1189:F1238")
        Dim c3_23 As Range = sourceWorkSheet.Range("H1189:I1238")

        Dim c1_24 As Range = sourceWorkSheet.Range("B1243:C1292")
        Dim c2_24 As Range = sourceWorkSheet.Range("E1243:F1292")
        Dim c3_24 As Range = sourceWorkSheet.Range("H1243:I1292")

        Dim c1_25 As Range = sourceWorkSheet.Range("B1297:C1346")
        Dim c2_25 As Range = sourceWorkSheet.Range("E1297:F1346")
        Dim c3_25 As Range = sourceWorkSheet.Range("H1297:I1346")

        Dim c1_26 As Range = sourceWorkSheet.Range("B1351:C1400")
        Dim c2_26 As Range = sourceWorkSheet.Range("E1351:F1400")
        Dim c3_26 As Range = sourceWorkSheet.Range("H1351:I1400")

        Dim c1_27 As Range = sourceWorkSheet.Range("B1405:C1454")
        Dim c2_27 As Range = sourceWorkSheet.Range("E1405:F1454")
        Dim c3_27 As Range = sourceWorkSheet.Range("H1405:I1454")

        Dim c1_28 As Range = sourceWorkSheet.Range("B1459:C1508")
        Dim c2_28 As Range = sourceWorkSheet.Range("E1459:F1508")
        Dim c3_28 As Range = sourceWorkSheet.Range("H1459:I1508")

        Dim c1_29 As Range = sourceWorkSheet.Range("B1513:C1562")
        Dim c2_29 As Range = sourceWorkSheet.Range("E1513:F1562")
        Dim c3_29 As Range = sourceWorkSheet.Range("H1513:I1562")

        Dim c1_30 As Range = sourceWorkSheet.Range("B1513:C1562")
        Dim c2_30 As Range = sourceWorkSheet.Range("E1513:F1562")
        Dim c3_30 As Range = sourceWorkSheet.Range("H1513:I1562")

        Dim c1_31 As Range = sourceWorkSheet.Range("B1567:C1616")
        Dim c2_31 As Range = sourceWorkSheet.Range("E1567:F1616")
        Dim c3_31 As Range = sourceWorkSheet.Range("H1567:I1616")

        Dim c1_32 As Range = sourceWorkSheet.Range("B1621:C1670")
        Dim c2_32 As Range = sourceWorkSheet.Range("E1621:F1670")
        Dim c3_32 As Range = sourceWorkSheet.Range("H1621:I1670")

        Dim c1_33 As Range = sourceWorkSheet.Range("B1675:C1724")
        Dim c2_33 As Range = sourceWorkSheet.Range("E1675:F1724")
        Dim c3_33 As Range = sourceWorkSheet.Range("H1675:I1724")

        Dim c1_34 As Range = sourceWorkSheet.Range("B1729:C1778")
        Dim c2_34 As Range = sourceWorkSheet.Range("E1729:F1778")
        Dim c3_34 As Range = sourceWorkSheet.Range("H1729:I1778")

        Dim c1_35 As Range = sourceWorkSheet.Range("B1783:C1832")
        Dim c2_35 As Range = sourceWorkSheet.Range("E1783:F1832")
        Dim c3_35 As Range = sourceWorkSheet.Range("H1783:I1832")

        Dim c1_36 As Range = sourceWorkSheet.Range("B1837:C1886")
        Dim c2_36 As Range = sourceWorkSheet.Range("E1837:F1886")
        Dim c3_36 As Range = sourceWorkSheet.Range("H1837:I1886")

        Dim c1_37 As Range = sourceWorkSheet.Range("B1891:C1940")
        Dim c2_37 As Range = sourceWorkSheet.Range("E1891:F1940")
        Dim c3_37 As Range = sourceWorkSheet.Range("H1891:I1940")

        Dim c1_38 As Range = sourceWorkSheet.Range("B1945:C1994")
        Dim c2_38 As Range = sourceWorkSheet.Range("E1945:F1994")
        Dim c3_38 As Range = sourceWorkSheet.Range("H1945:I1994")

        Dim c1_39 As Range = sourceWorkSheet.Range("B1999:C2048")
        Dim c2_39 As Range = sourceWorkSheet.Range("E1999:F2048")
        Dim c3_39 As Range = sourceWorkSheet.Range("H1999:I2048")

        Dim c1_40 As Range = sourceWorkSheet.Range("B2053:C2102")
        Dim c2_40 As Range = sourceWorkSheet.Range("E2053:F2102")
        Dim c3_40 As Range = sourceWorkSheet.Range("H2053:I2102")

        Dim c1_41 As Range = sourceWorkSheet.Range("B2107:C2156")
        Dim c2_41 As Range = sourceWorkSheet.Range("E2107:F2156")
        Dim c3_41 As Range = sourceWorkSheet.Range("H2107:I2156")

        Dim c1_42 As Range = sourceWorkSheet.Range("B2161:C2210")
        Dim c2_42 As Range = sourceWorkSheet.Range("E2161:F2210")
        Dim c3_42 As Range = sourceWorkSheet.Range("H2161:I2210")

        Dim c1_43 As Range = sourceWorkSheet.Range("B2215:C2264")
        Dim c2_43 As Range = sourceWorkSheet.Range("E2215:F2264")
        Dim c3_43 As Range = sourceWorkSheet.Range("H2215:I2264")

        Dim c1_44 As Range = sourceWorkSheet.Range("B2269:C2318")
        Dim c2_44 As Range = sourceWorkSheet.Range("E2269:F2318")
        Dim c3_44 As Range = sourceWorkSheet.Range("H2269:I2318")

        Dim c1_45 As Range = sourceWorkSheet.Range("B2323:C2372")
        Dim c2_45 As Range = sourceWorkSheet.Range("E2323:F2372")
        Dim c3_45 As Range = sourceWorkSheet.Range("H2323:I2372")

        Dim c1_46 As Range = sourceWorkSheet.Range("B2377:C2426")
        Dim c2_46 As Range = sourceWorkSheet.Range("E2377:F2426")
        Dim c3_46 As Range = sourceWorkSheet.Range("H2377:I2426")

        Dim c1_47 As Range = sourceWorkSheet.Range("B2431:C2480")
        Dim c2_47 As Range = sourceWorkSheet.Range("E2431:F2480")
        Dim c3_47 As Range = sourceWorkSheet.Range("H2431:I2480")

        Dim c1_48 As Range = sourceWorkSheet.Range("B2485:C2534")
        Dim c2_48 As Range = sourceWorkSheet.Range("E2485:F2534")
        Dim c3_48 As Range = sourceWorkSheet.Range("H2485:I2534")

        Dim cnt As Integer = 0
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT [Serial No],
                                         [Carton]
                                  From [CUPID].[WorkOrder]                         
                             Where [Work Order ID]='" & WID.ToString & "'  AND [Pallet No]='" & PalletBox.Text & "' ORDER BY [Index] ASC"


            Dim ds = SQLcmd.ExecuteReader
            Dim i1 = 1
            Dim i2 = 1
            Dim i3 = 1
            If ds.HasRows Then
                While ds.Read

                    cnt += 1
                    If cnt <= 40 Then

                        c1.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 40 And cnt <= 80 Then

                        i1 = 1
                        c2.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 80 And cnt <= 120 Then

                        i2 = 1
                        c3.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 120 And cnt <= 170 Then 'page2

                        i3 = 1
                        c1_2.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_2.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 170 And cnt <= 220 Then

                        i1 = 1
                        c2_2.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_2.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 220 And cnt <= 270 Then

                        i2 = 1
                        c3_2.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_2.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 270 And cnt <= 320 Then 'page3

                        i3 = 1
                        c1_3.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_3.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 320 And cnt <= 370 Then

                        i1 = 1
                        c2_3.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_3.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 370 And cnt <= 420 Then

                        i2 = 1
                        c3_3.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_3.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 420 And cnt <= 470 Then 'page4

                        i3 = 1
                        c1_4.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_4.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 470 And cnt <= 520 Then

                        i1 = 1
                        c2_4.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_4.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 520 And cnt <= 570 Then

                        i2 = 1
                        c3_4.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_4.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 570 And cnt <= 620 Then 'page5

                        i3 = 1
                        c1_5.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_5.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 620 And cnt <= 670 Then

                        i1 = 1
                        c2_5.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_5.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 670 And cnt <= 720 Then

                        i2 = 1
                        c3_5.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_5.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 720 And cnt <= 770 Then 'page6

                        i3 = 1
                        c1_6.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_6.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 770 And cnt <= 820 Then

                        i1 = 1
                        c2_6.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_6.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 820 And cnt <= 870 Then

                        i2 = 1
                        c3_6.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_6.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 870 And cnt <= 920 Then 'page7

                        i3 = 1
                        c1_7.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_7.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 920 And cnt <= 970 Then

                        i1 = 1
                        c2_7.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_7.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 970 And cnt <= 1020 Then

                        i2 = 1
                        c3_7.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_7.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 1020 And cnt <= 1070 Then 'page8

                        i3 = 1
                        c1_8.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_8.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 1070 And cnt <= 1120 Then

                        i1 = 1
                        c2_8.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_8.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 1120 And cnt <= 1170 Then

                        i2 = 1
                        c3_8.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_8.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 1170 And cnt <= 1220 Then 'page9

                        i3 = 1
                        c1_9.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_9.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 1220 And cnt <= 1270 Then

                        i1 = 1
                        c2_9.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_9.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 1270 And cnt <= 1320 Then

                        i2 = 1
                        c3_9.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_9.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 1320 And cnt <= 1370 Then 'page10

                        i3 = 1
                        c1_10.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_10.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 1370 And cnt <= 1420 Then

                        i1 = 1
                        c2_10.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_10.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 1420 And cnt <= 1470 Then

                        i2 = 1
                        c3_10.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_10.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 1470 And cnt <= 1520 Then 'page11

                        i3 = 1
                        c1_11.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_11.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 1520 And cnt <= 1570 Then

                        i1 = 1
                        c2_11.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_11.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 1570 And cnt <= 1620 Then

                        i2 = 1
                        c3_11.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_11.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 1620 And cnt <= 1670 Then 'page12

                        i3 = 1
                        c1_12.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_12.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 1670 And cnt <= 1720 Then

                        i1 = 1
                        c2_12.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_12.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 1720 And cnt <= 1770 Then

                        i2 = 1
                        c3_12.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_12.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 1770 And cnt <= 1820 Then 'page13

                        i3 = 1
                        c1_13.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_13.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 1820 And cnt <= 1870 Then

                        i1 = 1
                        c2_13.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_13.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 1870 And cnt <= 1920 Then

                        i2 = 1
                        c3_13.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_13.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 1920 And cnt <= 1970 Then 'page14

                        i3 = 1
                        c1_14.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_14.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 1970 And cnt <= 2020 Then

                        i1 = 1
                        c2_14.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_14.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 2020 And cnt <= 2070 Then

                        i2 = 1
                        c3_14.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_14.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 2070 And cnt <= 2120 Then 'page15

                        i3 = 1
                        c1_15.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_15.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 2120 And cnt <= 2170 Then

                        i1 = 1
                        c2_15.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_15.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 2170 And cnt <= 2220 Then

                        i2 = 1
                        c3_15.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_15.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 2220 And cnt <= 2270 Then 'page16

                        i3 = 1
                        c1_16.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_16.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 2270 And cnt <= 2320 Then

                        i1 = 1
                        c2_16.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_16.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 2320 And cnt <= 2370 Then

                        i2 = 1
                        c3_16.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_16.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 2370 And cnt <= 2420 Then 'page17

                        i3 = 1
                        c1_17.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_17.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 2420 And cnt <= 2470 Then

                        i1 = 1
                        c2_17.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_17.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 2470 And cnt <= 2520 Then

                        i2 = 1
                        c3_17.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_17.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 2520 And cnt <= 2570 Then 'page18

                        i3 = 1
                        c1_18.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_18.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 2570 And cnt <= 2620 Then

                        i1 = 1
                        c2_18.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_18.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 2620 And cnt <= 2670 Then

                        i2 = 1
                        c3_18.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_18.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 2670 And cnt <= 2720 Then 'page19

                        i3 = 1
                        c1_19.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_19.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 2720 And cnt <= 2770 Then

                        i1 = 1
                        c2_19.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_19.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 2770 And cnt <= 2820 Then

                        i2 = 1
                        c3_19.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_19.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 2820 And cnt <= 2870 Then 'page20

                        i3 = 1
                        c1_20.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_20.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 2870 And cnt <= 2920 Then

                        i1 = 1
                        c2_20.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_20.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 2920 And cnt <= 2970 Then

                        i2 = 1
                        c3_20.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_20.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 2970 And cnt <= 3020 Then 'page21

                        i3 = 1
                        c1_21.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_21.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 3020 And cnt <= 3070 Then

                        i1 = 1
                        c2_21.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_21.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 3070 And cnt <= 3120 Then

                        i2 = 1
                        c3_21.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_21.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 3120 And cnt <= 3170 Then 'page22

                        i3 = 1
                        c1_22.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_22.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 3170 And cnt <= 3220 Then

                        i1 = 1
                        c2_22.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_22.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 3220 And cnt <= 3270 Then

                        i2 = 1
                        c3_22.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_22.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 3270 And cnt <= 3320 Then 'page23

                        i3 = 1
                        c1_23.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_23.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 3320 And cnt <= 3370 Then

                        i1 = 1
                        c2_23.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_23.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 3370 And cnt <= 3420 Then

                        i2 = 1
                        c3_23.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_23.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 3420 And cnt <= 3470 Then 'page24

                        i3 = 1
                        c1_24.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_24.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 3470 And cnt <= 3520 Then

                        i1 = 1
                        c2_24.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_24.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 3520 And cnt <= 3570 Then

                        i2 = 1
                        c3_24.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_24.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 3570 And cnt <= 3620 Then 'page25

                        i3 = 1
                        c1_25.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_25.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 3620 And cnt <= 3670 Then

                        i1 = 1
                        c2_25.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_25.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 3670 And cnt <= 3720 Then

                        i2 = 1
                        c3_25.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_25.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 3720 And cnt <= 3770 Then 'page26

                        i3 = 1
                        c1_26.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_26.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 3770 And cnt <= 3820 Then

                        i1 = 1
                        c2_26.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_26.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 3820 And cnt <= 3870 Then

                        i2 = 1
                        c3_26.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_26.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 3870 And cnt <= 3920 Then 'page27

                        i3 = 1
                        c1_27.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_27.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 3920 And cnt <= 3970 Then

                        i1 = 1
                        c2_27.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_27.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 3970 And cnt <= 4020 Then

                        i2 = 1
                        c3_27.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_27.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 4020 And cnt <= 4070 Then 'page28

                        i3 = 1
                        c1_28.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_28.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 4070 And cnt <= 4120 Then

                        i1 = 1
                        c2_28.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_28.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 4120 And cnt <= 4170 Then

                        i2 = 1
                        c3_28.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_28.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 4170 And cnt <= 4220 Then 'page29

                        i3 = 1
                        c1_29.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_29.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 4220 And cnt <= 4270 Then

                        i1 = 1
                        c2_29.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_29.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 4270 And cnt <= 4320 Then

                        i2 = 1
                        c3_29.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_29.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 4320 And cnt <= 4370 Then 'page30

                        i3 = 1
                        c1_30.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_30.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 4370 And cnt <= 4420 Then

                        i1 = 1
                        c2_30.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_30.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 4420 And cnt <= 4470 Then

                        i2 = 1
                        c3_30.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_30.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 4470 And cnt <= 4520 Then 'page31

                        i3 = 1
                        c1_31.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_31.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 4520 And cnt <= 4570 Then

                        i1 = 1
                        c2_31.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_31.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 4570 And cnt <= 4620 Then

                        i2 = 1
                        c3_31.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_31.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 4620 And cnt <= 4670 Then 'page32

                        i3 = 1
                        c1_32.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_32.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 4670 And cnt <= 4720 Then

                        i1 = 1
                        c2_32.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_32.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 4720 And cnt <= 4770 Then

                        i2 = 1
                        c3_32.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_32.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 4770 And cnt <= 4820 Then 'page33

                        i3 = 1
                        c1_33.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_33.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 4820 And cnt <= 4870 Then

                        i1 = 1
                        c2_33.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_33.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 4870 And cnt <= 4920 Then

                        i2 = 1
                        c3_33.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_33.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 4920 And cnt <= 4970 Then 'page34

                        i3 = 1
                        c1_34.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_34.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 4970 And cnt <= 5020 Then

                        i1 = 1
                        c2_34.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_34.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 5020 And cnt <= 5070 Then

                        i2 = 1
                        c3_34.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_34.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 5070 And cnt <= 5120 Then 'page35

                        i3 = 1
                        c1_35.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_35.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 5120 And cnt <= 5170 Then

                        i1 = 1
                        c2_35.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_35.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 5170 And cnt <= 5220 Then

                        i2 = 1
                        c3_35.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_35.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 5220 And cnt <= 5270 Then 'page36

                        i3 = 1
                        c1_36.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_36.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 5270 And cnt <= 5320 Then

                        i1 = 1
                        c2_36.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_36.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 5320 And cnt <= 5370 Then

                        i2 = 1
                        c3_36.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_36.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 5370 And cnt <= 5420 Then 'page37

                        i3 = 1
                        c1_37.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_37.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 5420 And cnt <= 5470 Then

                        i1 = 1
                        c2_37.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_37.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 5470 And cnt <= 5520 Then

                        i2 = 1
                        c3_37.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_37.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 5520 And cnt <= 5570 Then 'page38

                        i3 = 1
                        c1_38.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_38.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 5570 And cnt <= 5620 Then

                        i1 = 1
                        c2_38.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_38.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 5620 And cnt <= 5670 Then

                        i2 = 1
                        c3_38.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_38.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 5670 And cnt <= 5720 Then 'page39

                        i3 = 1
                        c1_39.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_39.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 5720 And cnt <= 5770 Then

                        i1 = 1
                        c2_39.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_39.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 5770 And cnt <= 5820 Then

                        i2 = 1
                        c3_39.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_39.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 5820 And cnt <= 5870 Then 'page40

                        i3 = 1
                        c1_40.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_40.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 5870 And cnt <= 5920 Then

                        i1 = 1
                        c2_40.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_40.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 5920 And cnt <= 5970 Then

                        i2 = 1
                        c3_40.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_40.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 5970 And cnt <= 6020 Then 'page41

                        i3 = 1
                        c1_41.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_41.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 6020 And cnt <= 6070 Then

                        i1 = 1
                        c2_41.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_41.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 6070 And cnt <= 6120 Then

                        i2 = 1
                        c3_41.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_41.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 6120 And cnt <= 6170 Then 'page42

                        i3 = 1
                        c1_42.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_42.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 6170 And cnt <= 6220 Then

                        i1 = 1
                        c2_42.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_42.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 6220 And cnt <= 6270 Then

                        i2 = 1
                        c3_43.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_43.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 6270 And cnt <= 6320 Then 'page43

                        i3 = 1
                        c1_43.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_43.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 6320 And cnt <= 6370 Then

                        i1 = 1
                        c2_43.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_43.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 6370 And cnt <= 6420 Then

                        i2 = 1
                        c3_43.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_43.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 6420 And cnt <= 6470 Then 'page44

                        i3 = 1
                        c1_44.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_44.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 6470 And cnt <= 6520 Then

                        i1 = 1
                        c2_44.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_44.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 6520 And cnt <= 6570 Then

                        i2 = 1
                        c3_44.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_44.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 6570 And cnt <= 6620 Then 'page45

                        i3 = 1
                        c1_45.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_45.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 6620 And cnt <= 6670 Then

                        i1 = 1
                        c2_45.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_45.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 6670 And cnt <= 6720 Then

                        i2 = 1
                        c3_45.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_45.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 6720 And cnt <= 6770 Then 'page46

                        i3 = 1
                        c1_46.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_46.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 6770 And cnt <= 6820 Then

                        i1 = 1
                        c2_46.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_46.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 6820 And cnt <= 6870 Then

                        i2 = 1
                        c3_46.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_46.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    ElseIf cnt > 6870 And cnt <= 6920 Then 'page47

                        i3 = 1
                        c1_47.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1_47.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 6920 And cnt <= 6970 Then

                        i1 = 1
                        c2_47.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2_47.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 6970 And cnt <= 7020 Then

                        i2 = 1
                        c3_47.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3_47.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    End If

                End While

            End If
            ds.Close()
            SQLcmd.CommandText = "SELECT [Sub Group]
                                 FROM [CUPID].[WorkOrderMaster] 
                                 Where [Work Order ID]='" & WID.ToString & "'"
            ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    r1.SubGroup = ds.Item("Sub Group")
                End While

                If r1.PalletNo = 1 Then
                    'If IO.File.Exists(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls") Then
                    '    File.Delete(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls")
                    'End If

                    If IO.File.Exists(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls") Then
                        File.Delete(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls")
                    End If
                    sourceWorkSheet.SaveAs(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls")

                Else
                    If IO.File.Exists(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls") Then
                        File.Delete(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls")
                    End If
                    sourceWorkSheet.SaveAs(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls")
                End If
                sourceWorkBook.Close()
                sourceWorkBook = Nothing
                xlApp = Nothing
                'sourceWorkBook = Nothing
            End If

            If r1.PalletNo > 1 Then

                'Dim filePath As String = (localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls")
                'Dim filePath1 As String = (localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls")
                'Dim xlobj As New Excel.Application
                'Dim w As Workbook
                'Dim w1 As Workbook
                'Dim s As New Worksheet
                'Dim s1 As New Worksheet

                'w = xlobj.Workbooks.Open(filePath)
                'w1 = xlobj.Workbooks.Open(filePath1)
                's = w.Worksheets(1)

                ''If r1.PalletNo = 2 Then
                ''    s1 = w1.Worksheets("Pallet " & r1.PalletNo - 1)
                ''End If
                's1 = w1.Worksheets("Pallet " & r1.PalletNo - 1)
                's.Name = ("Pallet " & r1.PalletNo)
                's.Copy(After:=s1)

                'w.Close(False)
                'w1.Close(True)

                'File.Delete(filePath)

                'xlobj.Quit()
                'scanstatuslbl.Visible = False
                'scanstatuslbl.Text = ""

                Dim filePath As String = (localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls")
                Dim filePath1 As String = (localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls")
                Dim xlobj As New Excel.Application
                Dim xlobj1 As New Excel.Application
                Dim w As Workbook
                Dim w1 As Workbook
                Dim s As New Worksheet
                Dim s1 As New Worksheet

                w1 = xlobj1.Workbooks.Open(filePath1)

                xlobj1.DisplayAlerts = False
                's1 = w1.Worksheets("Pallet " & r1.PalletNo)
                's1.Delete()
                For Each s1 In w1.Worksheets
                    If s1.Name = ("Pallet " & r1.PalletNo) Then
                        s1.Delete()
                    End If
                Next
                'w1.Save()
                w1.Close(True)
                xlobj1.Quit()
                xlobj1 = Nothing

                w = xlobj.Workbooks.Open(filePath)
                w1 = xlobj.Workbooks.Open(filePath1)
                s = w.Worksheets(1)

                s1 = w1.Worksheets("Pallet " & r1.PalletNo - 1)
                s.Name = ("Pallet " & r1.PalletNo)
                s.Copy(After:=s1)

                w.Close(False)
                w1.Close(True)


                File.Delete(filePath)

                scanstatuslbl.Visible = True
                scanstatuslbl.Text = ""
                ScanBtn.Visible = False

                xlobj.Quit()
                'xlobj1.Quit()
                xlobj = Nothing
                'xlobj1 = Nothing
            End If
            Conn.Close()
            'sourceWorkSheet.Range("B13:C52").Value = c1.Value
            'sourceWorkSheet.Range("E13:F52").Value = c2.Value
            'sourceWorkSheet.Range("H13:I52").Value = c3.Value
        End If
        Dim dateEnd As Date = Date.Now
        End_Excel_App(datestart, dateEnd)
        'xlApp.Visible = True
        'If xlApp.Visible = True Then
        '    scanstatuslbl.Visible = False
        '    scanstatuslbl.Text = ""
        '    ScanBtn.Visible = True
        'End If

    End Function

    Private Sub xlApp_WorkbookBeforeClose(ByVal Wb As Excel.Workbook,
         ByRef Cancel As Boolean) Handles xlApp.WorkbookBeforeClose
        Debug.WriteLine("WithEvents: Closing the workbook.")
        Wb.Saved = True 'Set the dirty flag to true so there is no prompt to save
        xlApp.Workbooks.Close()
        xlApp.Quit()
        releaseObject(xlApp)
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Export(ByVal report As LocalReport)
        'MsgBox(“Start of Export”)   ‘Reaches this line on EBC Rise TS
        'then before “End of Export” get “Error occurred during local report processing”        
        Dim deviceInfo = “<DeviceInfo>” &
            “<OutputFormat>EMF</OutputFormat>” &
            “<PageWidth>29.7cm</PageWidth>” &
            “<PageHeight>21cm</PageHeight>” &
            “<MarginTop>0.25in</MarginTop>” &
            “<MarginLeft>0.25in</MarginLeft>” &
            “<MarginRight>0.25in</MarginRight>” &
            “<MarginBottom>0.25in</MarginBottom>” &
            “</DeviceInfo>”
        Dim warnings As Warning() = Nothing
        m_streams = New List(Of Stream)()
        'See my blog. For this to work the Build Action must be changed from Embedded Resource to Content and a Copy policy chosen
        'For this to work from a different project. Add this file as a link in that project and then do above on linked file.
        report.Render(“Image”, deviceInfo, AddressOf CreateStream, warnings)

        'Try

        '    report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)

        'Catch e As System.Exception

        '    Dim inner As Exception = e.InnerException

        'While Not (inner Is Nothing)

        '    MsgBox(inner.Message)

        '    inner = inner.InnerException

        'End While

        'End Try
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next

        'MsgBox(“End of Export”)
    End Sub

    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As System.Text.Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function

    ' Handler for PrintPageEvents
    Private Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim pageImage As New Metafile(m_streams(m_currentPageIndex))        ' Adjust rectangular area with printer margins.
        Dim adjustedRect As New System.Drawing.Rectangle(ev.PageBounds.Left - CInt(ev.PageSettings.HardMarginX), ev.PageBounds.Top - CInt(ev.PageSettings.HardMarginY), ev.PageBounds.Width,
           ev.PageBounds.Height)
        ' Draw a white background for the report
        ev.Graphics.FillRectangle(Brushes.White, adjustedRect)

        ' Draw the report 
        ev.Graphics.DrawImage(pageImage, adjustedRect)

        ' Prepare for the next page. Make sure we haven’t hit the end.
        m_currentPageIndex += 1
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count)
        ev.HasMorePages = False '#1074 srs fudge. Routecard was on 2 pages, should always be 1
    End Sub

    Private Sub Print()
        'MsgBox(“Start of Print”)
        If m_streams Is Nothing OrElse m_streams.Count = 0 Then
            Throw New Exception(“Error: no stream to print.”)
        End If
        Dim printDoc As New PrintDocument()

        printDoc.DefaultPageSettings.Margins = New Margins(20, 20, 20, 20)
        printDoc.DefaultPageSettings.Landscape = True
        If Not printDoc.PrinterSettings.IsValid Then
            Throw New Exception(“Error: cannot find the default printer.”)
        Else
            AddHandler printDoc.PrintPage, AddressOf PrintPage
            m_currentPageIndex = 0
            printDoc.Print()
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub QCout_Click(sender As Object, e As EventArgs) Handles btnQCout.Click
        Dim frm = New frmQCout
        frm.Text = "QC Out"
        frm.WID = WID
        frm.ShowDialog()
    End Sub

    Private Sub QCcheck_Click(sender As Object, e As EventArgs) Handles btnQCcheck.Click
        Dim frm = New frmQCcheck
        frm.Text = "View QC Record"
        frm.WID = WID
        frm.ShowDialog()
    End Sub

    Private Sub btnQCin_Click(sender As Object, e As EventArgs) Handles btnQCin.Click
        Dim frm = New frmQCin
        frm.WID = WID
        frm.Text = "QC In"
        frm.ShowDialog()
    End Sub


    ' Check Pallet Status
    Private Function checkPalleteStatus(ByVal serialnumber As String)
        Try
            '[Cricut_MES].[dbo].[ProductUnit] database
            Dim Conn = New SqlConnection(connstr)
            Conn.Open() 'Open the Connection
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand

                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "SELECT TOP 1000 ProductUnit.ID, TestResult.StationName
                                          FROM (([Cricut_MES].[dbo].[ProductUnit]
                                          INNER JOIN  [Cricut_MES].[dbo].[TestResult] ON ProductUnit.ID = TestResult.ProductUnitID))
                                          WHERE UnitSerialNumber ='" & serialnumber & "' and TestResult.StationName LIKE '%PRINTING3%'"
                'Dim ds = SQLcmd.ExecuteNonQuery()
                Dim rowsreturned As Integer
                rowsreturned = SQLcmd.ExecuteScalar()
                If rowsreturned = 0 Then
                    Return False
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Serial_TextChanged(sender As Object, e As EventArgs) Handles txtS1.TextChanged
        lblError.Text = ""

        ' Checking the TestResult Database to determine if the Pallete is Complete
        If checkPalleteStatus(txtS1.Text) = False Then
            serialStatusLbl.ForeColor = Color.Red
            serialStatusLbl.Text = "✖-Incomplete"
        Else
            serialStatusLbl.ForeColor = Color.Black
            serialStatusLbl.Text = "✓-Complete"
        End If

        If CheckDuplicateSerial(txtS1.Text) = False Then
            'statuslbl.Text = "Duplicated Serial No"
        Else
            statuslbl.Text = ""
            lblError.Text = ""
        End If
    End Sub


    Private Sub Serial_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtS1.KeyPress
        Dim url As String
        Dim method As String
        Dim dataSetStr As String
        Dim para1 As String
        Dim postData As String
        Dim check As String

        Dim url2 As String
        Dim method2 As String
        Dim dataSetStr2 As String
        Dim para2 As String
        Dim postData2 As String
        Dim check2 As String

        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtS1.Text = "" Then
                    statuslbl.Text = "Missing serial"
                Else
                    If (txtS1.Text).Substring(0, 1) <> Suffix Or (txtS1.Text).Length > BarcodeLength Then
                        statuslbl.Text = "Barcode Error"
                    End If
                End If


                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If SkipCarton = 0 Then


                        method = "POST"
                        url = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                        dataSetStr = "CHECK_CARTON_STATE"
                        para1 = txtS1.Text
                        postData = "dataSetStr=" & dataSetStr & "&para1=" & para1

                        check = WebrequestWithPost(url, Encoding.UTF8, postData, "application/x-www-form-urlencoded")
                        If (check = "OK") Then
                            method = "POST"
                            url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                            dataSetStr2 = "GET_PACKAGING_INFO"
                            para2 = txtS1.Text

                            postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2
                            'Carton.Text = ""
                            Carton.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")


                        Else
                            'MsgBox(check)
                            lblError.Text = check

                        End If
                    End If
                    FinishScan1()

                    If txtS2.Enabled = True Then
                        txtS2.Select()
                    Else
                        txtS1.Select()
                    End If
                End If

            End If
        End If

    End Sub

    Private Function FinishScan2()
        Dim tmp As Integer
        Dim totaltmp As Integer
        If Timer1.Enabled Then

            If txtC2.Text = "" Or Carton.Text = "" Then
                statuslbl.Text = "Missing carton"
            End If

            If statuslbl.Text <> "" Then
                Exit Function

            Else
                If lblOption.Text = "2" Then

                    If CheckDuplicateSerial(txtS1.Text) = False Or CheckDuplicateSerial(txtS2.Text) = False Then
                        statuslbl.Text = "Duplicated Serial"
                        Exit Function
                    End If
                    If (CheckDuplicateCarton(Carton.Text) = False Or CheckDuplicateCarton(txtC2.Text) = False) And SkipCarton = 0 Then
                        statuslbl.Text = "Duplicate Carton"
                        Exit Function
                    End If
                    tmp = Integer.Parse(count.Text)
                    totaltmp = Integer.Parse(totalordercount.Text)
                    totaltmp += lblOption.Text
                    tmp += lblOption.Text
                    count.Text = tmp
                    totalordercount.Text = totaltmp
                    InsertDataSQL(txtS1.Text, Carton.Text)
                    InsertDataSQL(txtS2.Text, txtC2.Text)
                    UpdateCountSQL()
                    LoadGrid()

                    For Each item In txtBoxes
                        item.Text = ""
                    Next

                    If SkipCarton = 1 Then
                        Carton.Text = 0
                        Carton.Enabled = False
                        txtC2.Text = 0
                        txtC2.Enabled = False
                        txtC3.Text = 0
                        txtC3.Enabled = False
                        txtC4.Text = 0
                        txtC4.Enabled = False
                        txtC5.Text = 0
                        txtC5.Enabled = False
                    End If

                    txtS1.Select()
                Else
                    txtC3.Select()
                End If

            End If
        End If
    End Function
    Public Class MyCarton
        Public Property UnitState As String
        Public Property CartonNumber As String
    End Class
    Public Shared Function WebrequestWithPost(ByVal url As String, ByVal dataEncoding As Encoding, ByVal dataToPost As String, ByVal contentType As String) As String
        Dim postDataAsByteArray As Byte() = dataEncoding.GetBytes(dataToPost)
        Dim returnValue As String = String.Empty
        Try
            Dim webRequest As HttpWebRequest = webRequest.CreateHttp(url)  'change to: dim webRequest as var = DirectCast(WebRequest.Create(url), HttpWebRequest) if you are your .NET Version is lower than 4.5
            If (Not (webRequest) Is Nothing) Then
                webRequest.AllowAutoRedirect = False
                webRequest.Method = "POST"
                webRequest.ContentType = contentType
                webRequest.ContentLength = postDataAsByteArray.Length
                Dim requestDataStream As Stream = webRequest.GetRequestStream
                requestDataStream.Write(postDataAsByteArray, 0, postDataAsByteArray.Length)
                requestDataStream.Close()
                Dim response As WebResponse = webRequest.GetResponse
                Dim responseDataStream As Stream = response.GetResponseStream
                If (Not (responseDataStream) Is Nothing) Then
                    Dim responseDataStreamReader As StreamReader = New StreamReader(responseDataStream)
                    returnValue = responseDataStreamReader.ReadToEnd
                    Dim jss As New JavaScriptSerializer()
                    Dim model() As MyCarton = jss.Deserialize(Of MyCarton())(returnValue)
                    returnValue = model(0).UnitState()
                    responseDataStreamReader.Close()
                    responseDataStream.Close()
                End If
                response.Close()
                requestDataStream.Close()
            End If
        Catch ex As WebException
            If Not IsNothing(ex.Response) Then
                Dim responseData = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
                returnValue += responseData
            End If
            returnValue += Environment.NewLine
            returnValue += ex.Message
            If Not IsNothing(ex.InnerException) Then
                returnValue += Environment.NewLine
                returnValue += "Inner Exception : " + ex.InnerException.Message
            End If
            If (ex.Status = WebExceptionStatus.ProtocolError) Then
                Dim response As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                returnValue += Environment.NewLine
                returnValue += String.Format("Webexception! Statuscode: {0}, Description: {1}", CType(response.StatusCode, Integer), response.StatusDescription)
            End If
        Catch ex As Exception
            returnValue += ex.Message
            If Not IsNothing(ex.InnerException) Then
                returnValue += Environment.NewLine
                returnValue += "Inner Exception : " + ex.InnerException.Message
            End If
        End Try
        Return returnValue
    End Function

    Public Shared Function WebrequestWithPost2(ByVal url As String, ByVal dataEncoding As Encoding, ByVal dataToPost As String, ByVal contentType As String) As String
        Dim postDataAsByteArray As Byte() = dataEncoding.GetBytes(dataToPost)
        Dim returnValue As String = String.Empty
        Try
            Dim webRequest As HttpWebRequest = webRequest.CreateHttp(url)  'change to: dim webRequest as var = DirectCast(WebRequest.Create(url), HttpWebRequest) if you are your .NET Version is lower than 4.5
            If (Not (webRequest) Is Nothing) Then
                webRequest.AllowAutoRedirect = False
                webRequest.Method = "POST"
                webRequest.ContentType = contentType
                webRequest.ContentLength = postDataAsByteArray.Length
                Dim requestDataStream As Stream = webRequest.GetRequestStream
                requestDataStream.Write(postDataAsByteArray, 0, postDataAsByteArray.Length)
                requestDataStream.Close()
                Dim response As WebResponse = webRequest.GetResponse
                Dim responseDataStream As Stream = response.GetResponseStream
                If (Not (responseDataStream) Is Nothing) Then
                    Dim responseDataStreamReader As StreamReader = New StreamReader(responseDataStream)
                    returnValue = responseDataStreamReader.ReadToEnd
                    Dim jss As New JavaScriptSerializer()
                    Dim model() As MyCarton = jss.Deserialize(Of MyCarton())(returnValue)
                    returnValue = model(0).CartonNumber()
                    responseDataStreamReader.Close()
                    responseDataStream.Close()
                End If
                response.Close()
                requestDataStream.Close()
            End If
        Catch ex As WebException
            If Not IsNothing(ex.Response) Then
                Dim responseData = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
                returnValue += responseData
            End If
            returnValue += Environment.NewLine
            returnValue += ex.Message
            If Not IsNothing(ex.InnerException) Then
                returnValue += Environment.NewLine
                returnValue += "Inner Exception : " + ex.InnerException.Message
            End If
            If (ex.Status = WebExceptionStatus.ProtocolError) Then
                Dim response As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                returnValue += Environment.NewLine
                returnValue += String.Format("Webexception! Statuscode: {0}, Description: {1}", CType(response.StatusCode, Integer), response.StatusDescription)
            End If
        Catch ex As Exception
            returnValue += ex.Message
            If Not IsNothing(ex.InnerException) Then
                returnValue += Environment.NewLine
                returnValue += "Inner Exception : " + ex.InnerException.Message
            End If
        End Try
        Return returnValue
    End Function

    Private Function FinishScan1()
        Dim tmp As Integer
        Dim totaltmp As Integer
        If Timer1.Enabled Then

            If Carton.Text = "" Then
                statuslbl.Text = "Missing carton"
            End If

            If statuslbl.Text <> "" Then
                Exit Function
            Else
                If lblOption.Text = "1" Then
                    If CheckDuplicateSerial(txtS1.Text) = False Then
                        statuslbl.Text = "Duplicated Serial"
                        Exit Function
                    End If
                    If CheckDuplicateCarton(Carton.Text) = False And SkipCarton = 0 Then
                        statuslbl.Text = "Duplicate Carton"
                        Exit Function
                    End If
                    tmp = Integer.Parse(count.Text)
                    totaltmp = Integer.Parse(totalordercount.Text)
                    totaltmp += lblOption.Text
                    tmp += lblOption.Text
                    count.Text = tmp
                    totalordercount.Text = totaltmp
                    InsertDataSQL(txtS1.Text, Carton.Text)
                    UpdateCountSQL()
                    LoadGrid()

                    For Each item In txtBoxes
                        item.Text = ""
                    Next

                    If SkipCarton = 1 Then
                        Carton.Text = 0
                        Carton.Enabled = False
                        txtC2.Text = 0
                        txtC2.Enabled = False
                        txtC3.Text = 0
                        txtC3.Enabled = False
                        txtC4.Text = 0
                        txtC4.Enabled = False
                        txtC5.Text = 0
                        txtC5.Enabled = False
                    End If

                    txtS1.Select()
                Else
                    txtC2.Select()
                End If

            End If

        End If
    End Function
    Private Sub txtS2_TextChanged(sender As Object, e As EventArgs) Handles txtS2.TextChanged

        If SkipCarton = 1 Then
            If CheckDuplicateSerial(txtS2.Text) = False Then
                statuslbl.Text = "Duplicated Serial No"
            Else
                statuslbl.Text = ""
                lblError.Text = ""
            End If
            Exit Sub
        End If


        ' Checking the TestResult Database to determine if the Pallete is Complete
        If checkPalleteStatus(txtS2.Text) = False Then
            serialStatusLbl.ForeColor = Color.Red
            serialStatusLbl.Text = "✖-Incomplete"
        Else
            serialStatusLbl.ForeColor = Color.Black
            serialStatusLbl.Text = "✓-Complete"
        End If

        lblError.Text = ""
        If CheckDuplicateSerial(txtS2.Text) = False Or txtS2.Text = txtS1.Text Then
            If statuslbl.Text = "" Then statuslbl.Text = "Duplicate Carton Scanned..."
        Else
            statuslbl.Text = ""
            lblError.Text = ""
        End If
    End Sub

    Private Sub txtS2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtS2.KeyPress
        Dim url As String
        Dim method As String
        Dim dataSetStr As String
        Dim para1 As String
        Dim postData As String
        Dim check As String

        Dim url2 As String
        Dim method2 As String
        Dim dataSetStr2 As String
        Dim para2 As String
        Dim postData2 As String
        Dim check2 As String
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtS2.Text = "" Then
                    statuslbl.Text = "Missing serial"
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else

                    If SkipCarton = 0 Then

                        method = "POST"
                        url = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                        dataSetStr = "CHECK_CARTON_STATE"
                        para1 = txtS2.Text
                        para2 = txtS1.Text
                        postData = "dataSetStr=" & dataSetStr & "&para1=" & para1
                        postData2 = "dataSetStr=" & dataSetStr & "&para1=" & para2
                    End If
                    If lblOption.Text = "2" Then

                        If SkipCarton = 0 Then

                            check = WebrequestWithPost(url, Encoding.UTF8, postData, "application/x-www-form-urlencoded")
                            check2 = WebrequestWithPost(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            If (check = "OK") Then

                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS2.Text

                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC2.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else

                                lblError.Text = check
                            End If

                            If (check2 = "OK") Then

                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS1.Text

                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                Carton.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else

                                lblError.Text = check
                            End If
                        End If
                        FinishScan2()
                    Else

                        If SkipCarton = 0 Then

                            check = WebrequestWithPost(url, Encoding.UTF8, postData, "application/x-www-form-urlencoded")
                            If (check = "OK") Then
                                method = "POST"
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS2.Text

                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC2.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check
                            End If
                        End If

                        FinishScan2()
                        End If


                        If txtS3.Enabled = True Then

                            txtS3.Select()
                        Else
                            txtS1.Select()
                        End If
                    End If
                End If
        End If
    End Sub

    Private Sub txtS3_TextChanged(sender As Object, e As EventArgs) Handles txtS3.TextChanged

        If SkipCarton = 1 Then
            If CheckDuplicateSerial(txtS3.Text) = False Then
                statuslbl.Text = "Duplicated Serial No"
            Else
                statuslbl.Text = ""
                lblError.Text = ""
            End If
            Exit Sub
        End If



        ' Checking the TestResult Database to determine if the Pallete is Complete
        If checkPalleteStatus(txtS3.Text) = False Then
            serialStatusLbl.ForeColor = Color.Red
            serialStatusLbl.Text = "✖-Incomplete"
        Else
            serialStatusLbl.ForeColor = Color.Black
            serialStatusLbl.Text = "✓-Complete"
        End If

        lblError.Text = ""
        If CheckDuplicateSerial(txtS3.Text) = False Or txtS3.Text = txtS2.Text Or txtS3.Text = txtS1.Text Then
            If statuslbl.Text = "" Then statuslbl.Text = "Duplicate Carton Scanned..."
        Else
            statuslbl.Text = ""
            lblError.Text = ""
        End If
    End Sub

    Private Sub txtS3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtS3.KeyPress
        Dim url As String
        Dim method As String
        Dim dataSetStr As String
        Dim para1 As String
        Dim postData, postdata3 As String
        Dim check As String

        Dim url2 As String
        Dim method2 As String
        Dim dataSetStr2 As String
        Dim para2, para3 As String
        Dim postData2 As String
        Dim check2, check3 As String
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtS3.Text = "" Then
                    statuslbl.Text = "Missing serial"
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else

                    If SkipCarton = 0 Then

                        method = "POST"
                        url = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                        dataSetStr = "CHECK_CARTON_STATE"
                        para1 = txtS1.Text
                        para2 = txtS2.Text
                        para3 = txtS3.Text
                        postData = "dataSetStr=" & dataSetStr & "&para1=" & para1
                        postData2 = "dataSetStr=" & dataSetStr & "&para1=" & para2
                        postdata3 = "dataSetStr=" & dataSetStr & "&para1=" & para3
                    End If
                    If lblOption.Text = "3" Then

                        If SkipCarton = 0 Then

                            check = WebrequestWithPost(url, Encoding.UTF8, postData, "application/x-www-form-urlencoded")
                            check2 = WebrequestWithPost(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")
                            check3 = WebrequestWithPost(url, Encoding.UTF8, postdata3, "application/x-www-form-urlencoded")

                            If (check = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS1.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2
                                Carton.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check
                            End If
                            If (check2 = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS2.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC2.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check2
                            End If
                            If (check3 = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS3.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC3.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check3
                            End If
                        End If
                        FinishScan3()
                    Else

                        If SkipCarton = 0 Then

                            check3 = WebrequestWithPost(url, Encoding.UTF8, postdata3, "application/x-www-form-urlencoded")
                            If (check3 = "OK") Then
                                method = "POST"
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS3.Text

                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC3.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check3
                            End If
                        End If

                        FinishScan3()
                        End If

                        If txtS4.Enabled = True Then

                            txtS4.Select()
                        Else
                            txtS1.Select()
                        End If
                    End If
                End If
        End If
    End Sub

    Private Function FinishScan3()
        Dim tmp As Integer
        Dim totaltmp As Integer
        If Timer1.Enabled Then

            If txtC3.Text = "" Or txtC2.Text = "" Or Carton.Text = "" Then
                statuslbl.Text = "Missing carton"
            End If
            If statuslbl.Text <> "" Then
                Exit Function

            Else
                If lblOption.Text = "3" Then
                    If CheckDuplicateSerial(txtS1.Text) = False Or CheckDuplicateSerial(txtS2.Text) = False Or CheckDuplicateSerial(txtS3.Text) = False Then
                        statuslbl.Text = "Duplicated Serial"
                        Exit Function
                    End If
                    If (CheckDuplicateCarton(Carton.Text) = False Or CheckDuplicateCarton(txtC2.Text) = False Or CheckDuplicateCarton(txtC3.Text) = False) And SkipCarton = 0 Then
                        statuslbl.Text = "Duplicate Carton"
                        Exit Function
                    End If
                    tmp = Integer.Parse(count.Text)
                    totaltmp = Integer.Parse(totalordercount.Text)
                    totaltmp += lblOption.Text
                    tmp += lblOption.Text
                    count.Text = tmp
                    totalordercount.Text = totaltmp
                    InsertDataSQL(txtS1.Text, Carton.Text)
                    InsertDataSQL(txtS2.Text, txtC2.Text)
                    InsertDataSQL(txtS3.Text, txtC3.Text)
                    UpdateCountSQL()
                    LoadGrid()

                    For Each item In txtBoxes
                        item.Text = ""
                    Next

                    If SkipCarton = 1 Then
                        Carton.Text = 0
                        Carton.Enabled = False
                        txtC2.Text = 0
                        txtC2.Enabled = False
                        txtC3.Text = 0
                        txtC3.Enabled = False
                        txtC4.Text = 0
                        txtC4.Enabled = False
                        txtC5.Text = 0
                        txtC5.Enabled = False
                    End If

                    txtS1.Select()
                Else
                    txtC4.Select()
                End If

            End If
        End If
    End Function
    Private Sub txtS4_TextChanged(sender As Object, e As EventArgs) Handles txtS4.TextChanged

        If SkipCarton = 1 Then
            If CheckDuplicateSerial(txtS4.Text) = False Then
                statuslbl.Text = "Duplicated Serial No"
            Else
                statuslbl.Text = ""
                lblError.Text = ""
            End If
            Exit Sub
        End If


        ' Checking the TestResult Database to determine if the Pallete is Complete
        If checkPalleteStatus(txtS4.Text) = False Then
            serialStatusLbl.ForeColor = Color.Red
            serialStatusLbl.Text = "✖-Incomplete"
        Else
            serialStatusLbl.ForeColor = Color.Black
            serialStatusLbl.Text = "✓-Complete"
        End If

        lblError.Text = ""
        If CheckDuplicateSerial(txtS4.Text) = False Or txtS4.Text = txtS3.Text Or txtS4.Text = txtS2.Text Or txtS4.Text = txtS1.Text Then
            If statuslbl.Text = "" Then statuslbl.Text = "Duplicate Carton Scanned..."
        Else
            statuslbl.Text = ""
            lblError.Text = ""
        End If
    End Sub

    Private Sub txtS4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtS4.KeyPress
        Dim url As String
        Dim method As String
        Dim dataSetStr As String
        Dim para1, para3, para4 As String
        Dim postData As String
        Dim check As String

        Dim url2 As String
        Dim method2 As String
        Dim dataSetStr2 As String
        Dim para2 As String
        Dim postData2, postData3, postData4 As String
        Dim check2, check3, check4 As String

        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtS4.Text = "" Then
                    statuslbl.Text = "Missing serial"
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else

                    If SkipCarton = 0 Then

                        method = "POST"
                        url = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                        dataSetStr = "CHECK_CARTON_STATE"
                        para1 = txtS1.Text
                        para2 = txtS2.Text
                        para3 = txtS3.Text
                        para4 = txtS4.Text
                        postData = "dataSetStr=" & dataSetStr & "&para1=" & para1
                        postData2 = "dataSetStr=" & dataSetStr & "&para1=" & para2
                        postData3 = "dataSetStr=" & dataSetStr & "&para1=" & para3
                        postData4 = "dataSetStr=" & dataSetStr & "&para1=" & para4
                    End If

                    If lblOption.Text = "4" Then
                        If SkipCarton = 0 Then
                            check = WebrequestWithPost(url, Encoding.UTF8, postData, "application/x-www-form-urlencoded")
                            check2 = WebrequestWithPost(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")
                            check3 = WebrequestWithPost(url, Encoding.UTF8, postData3, "application/x-www-form-urlencoded")
                            check4 = WebrequestWithPost(url, Encoding.UTF8, postData4, "application/x-www-form-urlencoded")

                            If (check = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS1.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2
                                Carton.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check
                            End If
                            If (check2 = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS2.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC2.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check2
                            End If
                            If (check3 = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS3.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC3.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check3
                            End If
                            If (check4 = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS4.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC4.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check4
                            End If

                        End If
                        FinishScan4()
                    Else
                        If SkipCarton = 0 Then
                            check4 = WebrequestWithPost(url, Encoding.UTF8, postData4, "application/x-www-form-urlencoded")
                            If (check4 = "OK") Then
                                method = "POST"
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS4.Text

                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC4.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check4
                            End If
                        End If

                        FinishScan4()
                        End If


                        If txtS5.Enabled = True Then

                            txtS5.Select()
                        Else
                            txtS1.Select()
                        End If
                    End If
                End If
        End If
    End Sub
    Private Function FinishScan4()
        Dim tmp As Integer
        Dim totaltmp As Integer
        If Timer1.Enabled Then
            If txtC4.Text = "" Or txtC2.Text = "" Or txtC3.Text = "" Or Carton.Text = "" Then
                statuslbl.Text = "Missing carton"
            End If
            If statuslbl.Text <> "" Then
                Exit Function

            Else
                If lblOption.Text = "4" Then
                    If CheckDuplicateSerial(txtS1.Text) = False Or CheckDuplicateSerial(txtS2.Text) = False Or CheckDuplicateSerial(txtS3.Text) = False Or CheckDuplicateSerial(txtS4.Text) = False Then
                        statuslbl.Text = "Duplicated Serial"
                        Exit Function
                    End If
                    If (CheckDuplicateCarton(Carton.Text) = False Or CheckDuplicateCarton(txtC2.Text) = False Or CheckDuplicateCarton(txtC3.Text) = False Or CheckDuplicateCarton(txtC4.Text) = False) And SkipCarton = 0 Then
                        statuslbl.Text = "Duplicate Carton"
                        Exit Function
                    End If
                    tmp = Integer.Parse(count.Text)
                    totaltmp = Integer.Parse(totalordercount.Text)
                    totaltmp += lblOption.Text
                    tmp += lblOption.Text
                    count.Text = tmp
                    totalordercount.Text = totaltmp
                    InsertDataSQL(txtS1.Text, Carton.Text)
                    InsertDataSQL(txtS2.Text, txtC2.Text)
                    InsertDataSQL(txtS3.Text, txtC3.Text)
                    InsertDataSQL(txtS4.Text, txtC4.Text)
                    UpdateCountSQL()
                    LoadGrid()

                    For Each item In txtBoxes
                        item.Text = ""
                    Next

                    If SkipCarton = 1 Then
                        Carton.Text = 0
                        Carton.Enabled = False
                        txtC2.Text = 0
                        txtC2.Enabled = False
                        txtC3.Text = 0
                        txtC3.Enabled = False
                        txtC4.Text = 0
                        txtC4.Enabled = False
                        txtC5.Text = 0
                        txtC5.Enabled = False
                    End If

                    txtS1.Select()
                Else
                    txtC5.Select()
                End If

            End If

        End If
    End Function
    Private Sub txtS5_TextChanged(sender As Object, e As EventArgs) Handles txtS5.TextChanged

        If SkipCarton = 1 Then
            If CheckDuplicateSerial(txtS5.Text) = False Then
                statuslbl.Text = "Duplicated Serial No"
            Else
                statuslbl.Text = ""
                lblError.Text = ""
            End If
            Exit Sub
        End If


        ' Checking the TestResult Database to determine if the Pallete is Complete
        If checkPalleteStatus(txtS5.Text) = False Then
            serialStatusLbl.ForeColor = Color.Red
            serialStatusLbl.Text = "✖-Incomplete"
        Else
            serialStatusLbl.ForeColor = Color.Black
            serialStatusLbl.Text = "✓-Complete"
        End If

        lblError.Text = ""
        If CheckDuplicateSerial(txtS5.Text) = False Or txtS5.Text = txtS4.Text Or txtS5.Text = txtS3.Text Or txtS5.Text = txtS2.Text Or txtS5.Text = txtS1.Text Then
            If statuslbl.Text = "" Then statuslbl.Text = "Duplicate Carton Scanned..."
        Else
            statuslbl.Text = ""
            lblError.Text = ""
        End If
    End Sub

    Private Sub txtS5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtS5.KeyPress
        Dim url As String
        Dim method As String
        Dim dataSetStr As String
        Dim para1 As String
        Dim postData As String
        Dim check As String

        Dim url2 As String
        Dim method2 As String
        Dim dataSetStr2 As String
        Dim para2, para3, para4, para5 As String
        Dim postData2, postData3, postData4, postData5 As String
        Dim check2, check3, check4, check5 As String

        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtS5.Text = "" Then
                    statuslbl.Text = "Missing serial"
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else

                    If SkipCarton = 0 Then
                        method = "POST"
                        url = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                        dataSetStr = "CHECK_CARTON_STATE"
                        para1 = txtS1.Text
                        para2 = txtS2.Text
                        para3 = txtS3.Text
                        para4 = txtS4.Text
                        para5 = txtS5.Text
                        postData = "dataSetStr=" & dataSetStr & "&para1=" & para1
                        postData2 = "dataSetStr=" & dataSetStr & "&para1=" & para2
                        postData3 = "dataSetStr=" & dataSetStr & "&para1=" & para3
                        postData4 = "dataSetStr=" & dataSetStr & "&para1=" & para4
                        postData5 = "dataSetStr=" & dataSetStr & "&para1=" & para5
                    End If
                    If lblOption.Text = "5" Then

                        If SkipCarton = 0 Then

                            check = WebrequestWithPost(url, Encoding.UTF8, postData, "application/x-www-form-urlencoded")
                            check2 = WebrequestWithPost(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")
                            check3 = WebrequestWithPost(url, Encoding.UTF8, postData3, "application/x-www-form-urlencoded")
                            check4 = WebrequestWithPost(url, Encoding.UTF8, postData4, "application/x-www-form-urlencoded")
                            check5 = WebrequestWithPost(url, Encoding.UTF8, postData5, "application/x-www-form-urlencoded")

                            If (check = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS1.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2
                                Carton.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check
                            End If
                            If (check2 = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS2.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC2.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check2
                            End If
                            If (check3 = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS3.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC3.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check3
                            End If
                            If (check4 = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS4.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC4.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check4
                            End If
                            If (check5 = "OK") Then
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS5.Text
                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC5.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check5
                            End If
                        End If
                        FinishScan5()
                    Else
                        If SkipCarton = 0 Then
                            check5 = WebrequestWithPost(url, Encoding.UTF8, postData5, "application/x-www-form-urlencoded")
                            If (check5 = "OK") Then
                                method = "POST"
                                url2 = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                                dataSetStr2 = "GET_PACKAGING_INFO"
                                para2 = txtS5.Text

                                postData2 = "dataSetStr=" & dataSetStr2 & "&para1=" & para2

                                txtC5.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                            Else
                                lblError.Text = check5
                            End If
                        End If
                        FinishScan5()

                        txtS1.Select()
                    End If



                End If
            End If
        End If
    End Sub

    Private Function FinishScan5()
        Dim tmp As Integer
        Dim totaltmp As Integer
        If Timer1.Enabled Then

            If txtC5.Text = "" Or txtC2.Text = "" Or txtC3.Text = "" Or txtC4.Text = "" Or Carton.Text = "" Then
                statuslbl.Text = "Missing carton"
            End If
            If statuslbl.Text <> "" Then
                Exit Function

            Else
                If CheckDuplicateSerial(txtS1.Text) = False Or CheckDuplicateSerial(txtS2.Text) = False Or CheckDuplicateSerial(txtS3.Text) = False Or CheckDuplicateSerial(txtS4.Text) = False Or CheckDuplicateSerial(txtS5.Text) = False Then
                    statuslbl.Text = "Duplicated Serial"
                    Exit Function
                End If
                If (CheckDuplicateCarton(Carton.Text) = False Or CheckDuplicateCarton(txtC2.Text) = False Or CheckDuplicateCarton(txtC3.Text) = False Or CheckDuplicateCarton(txtC4.Text) = False Or CheckDuplicateCarton(txtC5.Text) = False) And SkipCarton = 0 Then
                    statuslbl.Text = "Duplicate Carton"
                    Exit Function
                End If
                tmp = Integer.Parse(count.Text)
                totaltmp = Integer.Parse(totalordercount.Text)
                totaltmp += lblOption.Text
                tmp += lblOption.Text
                count.Text = tmp
                totalordercount.Text = totaltmp
                InsertDataSQL(txtS5.Text, txtC5.Text)
                InsertDataSQL(txtS4.Text, txtC4.Text)
                InsertDataSQL(txtS3.Text, txtC3.Text)
                InsertDataSQL(txtS2.Text, txtC2.Text)
                InsertDataSQL(txtS1.Text, Carton.Text)
                UpdateCountSQL()
                LoadGrid()

                For Each item In txtBoxes
                    item.Text = ""
                Next

                If SkipCarton = 1 Then
                    Carton.Text = 0
                    Carton.Enabled = False
                    txtC2.Text = 0
                    txtC2.Enabled = False
                    txtC3.Text = 0
                    txtC3.Enabled = False
                    txtC4.Text = 0
                    txtC4.Enabled = False
                    txtC5.Text = 0
                    txtC5.Enabled = False
                End If

                txtS1.Select()
            End If

        End If
    End Function


    Private Sub Carton_TextChanged(sender As Object, e As EventArgs) Handles Carton.TextChanged
        'If Not IsNumeric(Carton.Text) And Not Carton.Text = "" Then
        '    statuslbl.Text = "Invalid Carton Number"
        'Else
        'If Carton.Text.Length < 1 OrElse Carton.Text.Length > 4 And Carton.Text <> "" Then
        '    statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        'Else
        'If (CheckDuplicateCarton(Carton.Text) = False Or Carton.Text = Carton.Text) And Not Carton.Text = "" Then
        '        statuslbl.Text = "Duplicate carton"
        '    Else
        '        statuslbl.Text = ""
        '    End If
        'End If
        'End If
        If Carton.Text = "0" Then
            Exit Sub
        End If

        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT w.[Pallet No],w.[Carton],w.[Serial No],wm.[Work Order],wm.[Sub Group]
                        
                                      FROM [CRICUT].[CUPID].[WorkOrderMaster] wm
                                      INNER JOIN [CRICUT].[CUPID].[WorkOrder] w
                                      ON wm.[Work Order ID]=w.[Work Order ID]
                                      WHERE wm.[Work Order] = '" & cmbWorkOrderBox.Text & "'And wm.[Sub Group]='" & cmbSubGroup.Text & "' And w.[Carton] = '" & Carton.Text & "'"
            'And wm.[Sub Group] = '" & cmbSubGroup.Text & "'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.Read Then
                'statuslbl.Text = (" Carton : " & (ds.GetValue("2").ToString))
                statuslbl.Text = "Duplicate Carton No Found..." & vbCr & "PO No. : " & ds.Item("Work Order") & vbCr & "MR No : " & ds.Item("Sub Group") & vbCr & "Pallet No : " & ds.Item("Pallet No") & vbCr & "Serial No : " & ds.Item("Serial No")
            Else
                statuslbl.Text = ""
                lblError.Text = ""
            End If
        End If
        Conn.Close()

        If Carton.Text.Length < 1 OrElse Carton.Text.Length > 4 And Carton.Text <> "" Then
            statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        End If

    End Sub

    Private Sub Carton_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Carton.KeyPress

        Dim tmp As Integer
        Dim totaltmp As Integer
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If Carton.Text = "" Then
                    statuslbl.Text = "Missing carton"
                End If

                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If lblOption.Text = "1" Then
                        If CheckDuplicateSerial(txtS1.Text) = False Then
                            statuslbl.Text = "Duplicated Serial"
                            Exit Sub
                        End If
                        If CheckDuplicateCarton(Carton.Text) = False And SkipCarton = 0 Then
                            statuslbl.Text = "Duplicate Carton"
                            Exit Sub
                        End If
                        tmp = Integer.Parse(count.Text)
                        totaltmp = Integer.Parse(totalordercount.Text)
                        totaltmp += lblOption.Text
                        tmp += lblOption.Text
                        count.Text = tmp
                        totalordercount.Text = totaltmp
                        InsertDataSQL(txtS1.Text, Carton.Text)
                        UpdateCountSQL()
                        LoadGrid()

                        For Each item In txtBoxes
                            item.Text = ""
                        Next

                        If SkipCarton = 1 Then
                            Carton.Text = 0
                            Carton.Enabled = False
                            txtC2.Text = 0
                            txtC2.Enabled = False
                            txtC3.Text = 0
                            txtC3.Enabled = False
                            txtC4.Text = 0
                            txtC4.Enabled = False
                            txtC5.Text = 0
                            txtC5.Enabled = False
                        End If

                        txtS1.Select()
                    Else
                        txtC2.Select()
                    End If


                End If
            End If

        End If

    End Sub

    Private Sub txtC5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtC5.KeyPress
        Dim tmp As Integer
        Dim totaltmp As Integer
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtC5.Text = "" Then
                    statuslbl.Text = "Missing carton"
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If CheckDuplicateSerial(txtS1.Text) = False Or CheckDuplicateSerial(txtS2.Text) = False Or CheckDuplicateSerial(txtS3.Text) = False Or CheckDuplicateSerial(txtS4.Text) = False Or CheckDuplicateSerial(txtS5.Text) = False Then
                        statuslbl.Text = "Duplicated Serial"
                        Exit Sub
                    End If
                    If CheckDuplicateCarton(Carton.Text) = False Or CheckDuplicateCarton(txtC2.Text) = False Or CheckDuplicateCarton(txtC3.Text) = False Or CheckDuplicateCarton(txtC4.Text) = False Or CheckDuplicateCarton(txtC5.Text) = False Then
                        statuslbl.Text = "Duplicate Carton"
                        Exit Sub
                    End If
                    tmp = Integer.Parse(count.Text)
                    totaltmp = Integer.Parse(totalordercount.Text)
                    totaltmp += lblOption.Text
                    tmp += lblOption.Text
                    count.Text = tmp
                    totalordercount.Text = totaltmp
                    InsertDataSQL(txtS5.Text, txtC5.Text)
                    InsertDataSQL(txtS4.Text, txtC4.Text)
                    InsertDataSQL(txtS3.Text, txtC3.Text)
                    InsertDataSQL(txtS2.Text, txtC2.Text)
                    InsertDataSQL(txtS1.Text, Carton.Text)
                    UpdateCountSQL()
                    LoadGrid()

                    For Each item In txtBoxes
                        item.Text = ""
                    Next

                    If SkipCarton = 1 Then
                        Carton.Text = 0
                        Carton.Enabled = False
                        txtC2.Text = 0
                        txtC2.Enabled = False
                        txtC3.Text = 0
                        txtC3.Enabled = False
                        txtC4.Text = 0
                        txtC4.Enabled = False
                        txtC5.Text = 0
                        txtC5.Enabled = False
                    End If

                    txtS1.Select()
                End If
            End If
        End If
    End Sub

    Private Sub txtC2_TextChanged(sender As Object, e As EventArgs) Handles txtC2.TextChanged
        'If Not IsNumeric(txtC2.Text) And Not txtC2.Text = "" Then
        '    statuslbl.Text = "Invalid Carton Number"
        'Else
        '    If txtC2.Text.Length < 1 OrElse txtC2.Text.Length > 4 And txtC2.Text <> "" Then
        '        statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        '    Else
        '        If (CheckDuplicateCarton(txtC2.Text) = False Or txtC2.Text = Carton.Text) And Not txtC2.Text = "" Then
        '            statuslbl.Text = "Duplicate carton"
        '        Else
        '            statuslbl.Text = ""
        '        End If
        '    End If
        'End If
        If SkipCarton = 1 Then Exit Sub

        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT w.[Pallet No],w.[Carton],w.[Serial No],wm.[Work Order],wm.[Sub Group]
                        
                                      FROM [CRICUT].[CUPID].[WorkOrderMaster] wm
                                      INNER JOIN [CRICUT].[CUPID].[WorkOrder] w
                                      ON wm.[Work Order ID]=w.[Work Order ID]
                                      WHERE wm.[Work Order] = '" & cmbWorkOrderBox.Text & "'And wm.[Sub Group]='" & cmbSubGroup.Text & "' And w.[Carton] = '" & txtC2.Text & "'"
            'And wm.[Sub Group] = '" & cmbSubGroup.Text & "'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.Read Then
                'statuslbl.Text = (" Carton: " & (ds.GetValue("2").ToString))
                'statuslbl.Text = "Duplicate Carton Found..." & vbCr & "PO No : " & ds.Item("Work Order") & vbCr & "Sub Group:" & ds.Item("Sub Group") & vbCr & "Pallet No:" & ds.Item("Pallet No") & vbCr & "Serial No:" & ds.Item("Serial No")
                statuslbl.Text = "Duplicate Carton No Found..." & vbCr & "PO No. : " & ds.Item("Work Order") & vbCr & "MR No : " & ds.Item("Sub Group") & vbCr & "Pallet No : " & ds.Item("Pallet No") & vbCr & "Serial No : " & ds.Item("Serial No")
            Else
                statuslbl.Text = ""
                lblError.Text = ""
            End If
        End If
        Conn.Close()

        If txtC2.Text.Length < 1 OrElse txtC2.Text.Length > 4 And txtC2.Text <> "" Then
            statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        Else
            If txtC2.Text = Carton.Text And Not txtC2.Text = "" Then
                statuslbl.Text = "Duplicate Carton Scanned..."
            End If
        End If

    End Sub

    Private Sub txtC2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtC2.KeyPress

        Dim tmp As Integer
        Dim totaltmp As Integer
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtC2.Text = "" Then
                    statuslbl.Text = "Missing carton"
                End If

                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If lblOption.Text = "2" Then
                        If CheckDuplicateSerial(txtS1.Text) = False Or CheckDuplicateSerial(txtS2.Text) = False Then
                            statuslbl.Text = "Duplicated Serial"
                            Exit Sub
                        End If
                        If CheckDuplicateCarton(Carton.Text) = False Or CheckDuplicateCarton(txtC2.Text) = False Then
                            statuslbl.Text = "Duplicate Carton"
                            Exit Sub
                        End If
                        tmp = Integer.Parse(count.Text)
                        totaltmp = Integer.Parse(totalordercount.Text)
                        totaltmp += lblOption.Text
                        tmp += lblOption.Text
                        count.Text = tmp
                        totalordercount.Text = totaltmp
                        InsertDataSQL(txtS1.Text, Carton.Text)
                        InsertDataSQL(txtS2.Text, txtC2.Text)
                        UpdateCountSQL()
                        LoadGrid()

                        For Each item In txtBoxes
                            item.Text = ""
                        Next

                        If SkipCarton = 1 Then
                            Carton.Text = 0
                            Carton.Enabled = False
                            txtC2.Text = 0
                            txtC2.Enabled = False
                            txtC3.Text = 0
                            txtC3.Enabled = False
                            txtC4.Text = 0
                            txtC4.Enabled = False
                            txtC5.Text = 0
                            txtC5.Enabled = False
                        End If

                        txtS1.Select()
                    Else
                        txtC3.Select()
                    End If


                End If
            End If
        End If
    End Sub


    Private Sub txtC3_TextChanged(sender As Object, e As EventArgs) Handles txtC3.TextChanged
        'If Not IsNumeric(txtC3.Text) And Not txtC3.Text = "" Then
        '    statuslbl.Text = "Invalid Carton Number"
        'Else
        '    If txtC3.Text.Length < 1 OrElse txtC3.Text.Length > 4 And txtC3.Text <> "" Then
        '        statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        '    Else
        '        If (CheckDuplicateCarton(txtC3.Text) = False Or txtC3.Text = Carton.Text Or txtC3.Text = txtC2.Text) And Not txtC3.Text = "" Then
        '            statuslbl.Text = "Duplicate carton"
        '        Else
        '            statuslbl.Text = ""
        '        End If
        '    End If
        'End If



        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT w.[Pallet No],w.[Carton],w.[Serial No],wm.[Work Order],wm.[Sub Group]
                        
                                      FROM [CRICUT].[CUPID].[WorkOrderMaster] wm
                                      INNER JOIN [CRICUT].[CUPID].[WorkOrder] w
                                      ON wm.[Work Order ID]=w.[Work Order ID]
                                      WHERE wm.[Work Order] = '" & cmbWorkOrderBox.Text & "'And wm.[Sub Group]='" & cmbSubGroup.Text & "' And w.[Carton] = '" & txtC3.Text & "'"
            'And wm.[Sub Group] = '" & cmbSubGroup.Text & "'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.Read Then
                'statuslbl.Text = (" Carton: " & (ds.GetValue("2").ToString))
                'statuslbl.Text = "Duplicate Carton:" & vbCr & "Work Order:" & ds.Item("Work Order") & vbCr & "Sub Group:" & ds.Item("Sub Group") & vbCr & "Pallet No:" & ds.Item("Pallet No") & vbCr & "Serial No:" & ds.Item("Serial No")
                statuslbl.Text = "Duplicate Carton No Found..." & vbCr & "PO No. : " & ds.Item("Work Order") & vbCr & "MR No : " & ds.Item("Sub Group") & vbCr & "Pallet No : " & ds.Item("Pallet No") & vbCr & "Serial No : " & ds.Item("Serial No")
            Else
                statuslbl.Text = ""
                lblError.Text = ""
            End If
        End If
        Conn.Close()

        If txtC3.Text.Length < 1 OrElse txtC3.Text.Length > 4 And txtC3.Text <> "" Then
            statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        Else
            If (txtC3.Text = Carton.Text Or txtC3.Text = txtC2.Text) And Not txtC3.Text = "" Then
                statuslbl.Text = "Duplicate Carton Scanned..."
            End If

        End If

    End Sub

    Private Sub txtC4_TextChanged(sender As Object, e As EventArgs) Handles txtC4.TextChanged
        'If Not IsNumeric(txtC4.Text) And Not txtC4.Text = "" Then
        '    statuslbl.Text = "Invalid Carton Number"
        'Else
        '    If txtC4.Text.Length < 1 OrElse txtC4.Text.Length > 4 And Not txtC4.Text = "" Then
        '        statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        '    Else
        '        If (CheckDuplicateCarton(txtC4.Text) = False Or txtC4.Text = Carton.Text Or txtC4.Text = txtC2.Text Or txtC4.Text = txtC3.Text) And Not txtC4.Text = "" Then
        '            statuslbl.Text = "Duplicate carton"
        '        Else
        '            statuslbl.Text = ""
        '        End If
        '    End If
        'End If

        If SkipCarton = 1 Then Exit Sub

        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT w.[Pallet No],w.[Carton],w.[Serial No],wm.[Work Order],wm.[Sub Group]
                        
                                      FROM [CRICUT].[CUPID].[WorkOrderMaster] wm
                                      INNER JOIN [CRICUT].[CUPID].[WorkOrder] w
                                      ON wm.[Work Order ID]=w.[Work Order ID]
                                      WHERE wm.[Work Order] = '" & cmbWorkOrderBox.Text & "'And wm.[Sub Group]='" & cmbSubGroup.Text & "' And w.[Carton] = '" & txtC4.Text & "'"
            'And wm.[Sub Group] = '" & cmbSubGroup.Text & "'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.Read Then
                'statuslbl.Text = (" Carton: " & (ds.GetValue("2").ToString))
                'statuslbl.Text = "Duplicate Carton:" & vbCr & "Work Order:" & ds.Item("Work Order") & vbCr & "Sub Group:" & ds.Item("Sub Group") & vbCr & "Pallet No:" & ds.Item("Pallet No") & vbCr & "Serial No:" & ds.Item("Serial No")
                statuslbl.Text = "Duplicate Carton No Found..." & vbCr & "PO No. : " & ds.Item("Work Order") & vbCr & "MR No : " & ds.Item("Sub Group") & vbCr & "Pallet No : " & ds.Item("Pallet No") & vbCr & "Serial No : " & ds.Item("Serial No")
            Else
                statuslbl.Text = ""
                lblError.Text = ""
            End If
        End If
        Conn.Close()

        If txtC4.Text.Length < 1 OrElse txtC4.Text.Length > 4 And txtC4.Text <> "" Then
            statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        Else
            If (txtC4.Text = Carton.Text Or txtC4.Text = txtC2.Text Or txtC4.Text = txtC3.Text) And Not txtC4.Text = "" Then
                statuslbl.Text = "Duplicate Carton Scanned..."
            End If
        End If

    End Sub

    Private Sub txtC5_TextChanged(sender As Object, e As EventArgs) Handles txtC5.TextChanged
        'If Not IsNumeric(txtC5.Text) And Not txtC5.Text = "" Then
        '    statuslbl.Text = "Invalid Carton Number"
        'Else
        '    If txtC5.Text.Length < 1 OrElse txtC5.Text.Length > 4 And Not txtC5.Text = "" Then
        '        statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        '    Else
        '        If (CheckDuplicateCarton(txtC5.Text) = False Or txtC5.Text = Carton.Text Or txtC5.Text = txtC2.Text Or txtC5.Text = txtC3.Text Or txtC5.Text = txtC4.Text) And Not txtC5.Text = "" Then
        '            statuslbl.Text = "Duplicate carton"
        '        Else
        '            statuslbl.Text = ""
        '        End If
        '    End If
        'End If

        If SkipCarton = 1 Then Exit Sub

        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT w.[Pallet No],w.[Carton],w.[Serial No],wm.[Work Order],wm.[Sub Group]
                        
                                      FROM [CRICUT].[CUPID].[WorkOrderMaster] wm
                                      INNER JOIN [CRICUT].[CUPID].[WorkOrder] w
                                      ON wm.[Work Order ID]=w.[Work Order ID]
                                      WHERE wm.[Work Order] = '" & cmbWorkOrderBox.Text & "'And wm.[Sub Group]='" & cmbSubGroup.Text & "' And w.[Carton] = '" & txtC5.Text & "'"
            'And wm.[Sub Group] = '" & cmbSubGroup.Text & "'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.Read Then
                'statuslbl.Text = (" Carton: " & (ds.GetValue("2").ToString))
                'statuslbl.Text = "Duplicate Carton:" & vbCr & "Work Order:" & ds.Item("Work Order") & vbCr & "Sub Group:" & ds.Item("Sub Group") & vbCr & "Pallet No:" & ds.Item("Pallet No") & vbCr & "Serial No:" & ds.Item("Serial No")
                statuslbl.Text = "Duplicate Carton No Found..." & vbCr & "PO No. : " & ds.Item("Work Order") & vbCr & "MR No : " & ds.Item("Sub Group") & vbCr & "Pallet No : " & ds.Item("Pallet No") & vbCr & "Serial No : " & ds.Item("Serial No")
            Else
                statuslbl.Text = ""
                lblError.Text = ""
            End If
        End If
        Conn.Close()

        If txtC5.Text.Length < 1 OrElse txtC5.Text.Length > 4 And txtC5.Text <> "" Then
            statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        Else
            If (txtC5.Text = Carton.Text Or txtC5.Text = txtC2.Text Or txtC5.Text = txtC3.Text Or txtC5.Text = txtC4.Text) And Not txtC5.Text = "" Then
                statuslbl.Text = "Duplicate Carton Scanned..."
            End If
        End If

    End Sub

    Private Sub txtC3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtC3.KeyPress

        Dim tmp As Integer
        Dim totaltmp As Integer
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtC3.Text = "" Then
                    statuslbl.Text = "Missing carton"
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If lblOption.Text = "3" Then
                        If CheckDuplicateSerial(txtS1.Text) = False Or CheckDuplicateSerial(txtS2.Text) = False Or CheckDuplicateSerial(txtS3.Text) = False Then
                            statuslbl.Text = "Duplicated Serial"
                            Exit Sub
                        End If
                        If CheckDuplicateCarton(Carton.Text) = False Or CheckDuplicateCarton(txtC2.Text) = False Or CheckDuplicateCarton(txtC3.Text) = False Then
                            statuslbl.Text = "Duplicate Carton"
                            Exit Sub
                        End If
                        tmp = Integer.Parse(count.Text)
                        totaltmp = Integer.Parse(totalordercount.Text)
                        totaltmp += lblOption.Text
                        tmp += lblOption.Text
                        count.Text = tmp
                        totalordercount.Text = totaltmp
                        InsertDataSQL(txtS1.Text, Carton.Text)
                        InsertDataSQL(txtS2.Text, txtC2.Text)
                        InsertDataSQL(txtS3.Text, txtC3.Text)
                        UpdateCountSQL()
                        LoadGrid()

                        For Each item In txtBoxes
                            item.Text = ""
                        Next

                        If SkipCarton = 1 Then
                            Carton.Text = 0
                            Carton.Enabled = False
                            txtC2.Text = 0
                            txtC2.Enabled = False
                            txtC3.Text = 0
                            txtC3.Enabled = False
                            txtC4.Text = 0
                            txtC4.Enabled = False
                            txtC5.Text = 0
                            txtC5.Enabled = False
                        End If

                        txtS1.Select()
                    Else
                        txtC4.Select()
                    End If


                End If
            End If
        End If
    End Sub

    Private Sub txtC4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtC4.KeyPress
        Dim tmp As Integer
        Dim totaltmp As Integer
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtC4.Text = "" Then
                    statuslbl.Text = "Missing carton"
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If lblOption.Text = "4" Then
                        If CheckDuplicateSerial(txtS1.Text) = False Or CheckDuplicateSerial(txtS2.Text) = False Or CheckDuplicateSerial(txtS3.Text) = False Or CheckDuplicateSerial(txtS4.Text) = False Then
                            statuslbl.Text = "Duplicated Serial"
                            Exit Sub
                        End If
                        If CheckDuplicateCarton(Carton.Text) = False Or CheckDuplicateCarton(txtC2.Text) = False Or CheckDuplicateCarton(txtC3.Text) = False Or CheckDuplicateCarton(txtC4.Text) = False Then
                            statuslbl.Text = "Duplicate Carton"
                            Exit Sub
                        End If
                        tmp = Integer.Parse(count.Text)
                        totaltmp = Integer.Parse(totalordercount.Text)
                        totaltmp += lblOption.Text
                        tmp += lblOption.Text
                        count.Text = tmp
                        totalordercount.Text = totaltmp
                        InsertDataSQL(txtS1.Text, Carton.Text)
                        InsertDataSQL(txtS2.Text, txtC2.Text)
                        InsertDataSQL(txtS3.Text, txtC3.Text)
                        InsertDataSQL(txtS4.Text, txtC4.Text)
                        UpdateCountSQL()
                        LoadGrid()

                        For Each item In txtBoxes
                            item.Text = ""
                        Next

                        If SkipCarton = 1 Then
                            Carton.Text = 0
                            Carton.Enabled = False
                            txtC2.Text = 0
                            txtC2.Enabled = False
                            txtC3.Text = 0
                            txtC3.Enabled = False
                            txtC4.Text = 0
                            txtC4.Enabled = False
                            txtC5.Text = 0
                            txtC5.Enabled = False
                        End If

                        txtS1.Select()
                    Else
                        txtC5.Select()
                    End If


                End If
            End If
        End If
    End Sub



    Private Sub lblOption_TextChanged(sender As Object, e As EventArgs) Handles lblOption.TextChanged
        Select Case lblOption.Text
            Case "1"
                For i = 0 To 1
                    txtBoxes(i).enabled = True
                Next
                For i = 2 To 9
                    txtBoxes(i).enabled = False
                Next
            Case "2"
                For i = 0 To 3
                    txtBoxes(i).enabled = True
                Next
                For i = 4 To 9
                    txtBoxes(i).enabled = False
                Next
            Case "3"
                For i = 0 To 5
                    txtBoxes(i).enabled = True
                Next
                For i = 6 To 9
                    txtBoxes(i).enabled = False
                Next
            Case "4"
                For i = 0 To 7
                    txtBoxes(i).enabled = True
                Next
                For i = 8 To 9
                    txtBoxes(i).enabled = False
                Next
            Case "5"
                For i = 0 To 9
                    txtBoxes(i).enabled = True
                Next


        End Select
    End Sub

    Private Sub UpdateScanOption()
        Dim diffOrder = Integer.Parse(Order.Text) - Integer.Parse(totalordercount.Text)
        Dim diffQty = Integer.Parse(qty.Text) - Integer.Parse(count.Text)
        Dim tmpScanOption
        tmpScanOption = scanOption
        If tmpScanOption > diffOrder Then
            tmpScanOption = diffOrder
        End If
        If tmpScanOption > diffQty Then
            tmpScanOption = diffQty
        End If
        lblOption.Text = tmpScanOption
    End Sub

    Private Async Sub count_TextChanged(sender As Object, e As EventArgs) Handles count.TextChanged
        Await Task.Delay(500)
        UpdateScanOption()
        If ScanBtn.Visible = False Then

            If Integer.Parse(count.Text) >= Integer.Parse(qty.Text) Then
                If Integer.Parse(totalordercount.Text) < Integer.Parse(Order.Text) Then
                    PrintReportAndStoreExcel()
                    Dim res = MessageBox.Show("Do you want to print order and report for pallet " & PalletBox.SelectedItem & " for this work order?", "Print Order", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    If res = DialogResult.Yes Then
                        PrintOrder()
                        PrintReport()
                    End If
                    NextPallet()
                End If
            End If
            If Integer.Parse(totalordercount.Text) >= Integer.Parse(Order.Text) Then
                PrintReportAndStoreExcel()
                Await Task.Delay(500)
                UpdateCompleteSQL()
                Dim res = MessageBox.Show("Do you want to print order and report for pallet " & PalletBox.SelectedItem & " for this work order?", "Print Order", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If res = DialogResult.Yes Then
                    Label13.Visible = False
                    scanstatuslbl.Text = "Populating Data and Printing Report.."
                    CancelBtn.Visible = False
                    PrintOrder()
                    PrintReport()

                End If
                MessageBox.Show("Work Order Complete", "Complete Operation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                scanstatuslbl.Visible = False
                ScanBtn.Visible = True
                CancelBtn.Visible = False

                PalletBox.Enabled = True
                cmbWorkOrderBox.Enabled = True
                Shift.Enabled = True
                btnSearch.Enabled = True
                Label13.Visible = False
                For Each item In txtBoxes
                    item.text = ""
                    item.enabled = False
                Next
                Exit Sub
            Else

            End If
        End If
    End Sub



    Private Sub totalordercount_TextChanged(sender As Object, e As EventArgs) Handles totalordercount.TextChanged
        If Not qty.Text = 0 Then
            UpdateScanOption()
        End If
        'If ScanBtn.Visible = False Then

        '    If Integer.Parse(count.Text) >= Integer.Parse(qty.Text) Then
        '        Dim res = MessageBox.Show("Do you want to print order and report for pallet " & PalletBox.SelectedItem & " for this work order?", "Print Order", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        '        If res = DialogResult.Yes Then
        '            PrintOrder()
        '            PrintReport()
        '        End If

        '        NextPallet()
        '    End If
        '    If Integer.Parse(totalordercount.Text) >= Integer.Parse(Order.Text) Then
        '        UpdateCompleteSQL()
        '        MsgBox("Work Order Complete")
        '        Dim res = MessageBox.Show("Do you want to print order and report for pallet " & PalletBox.SelectedItem & " for this work order?", "Print Order", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        '        If res = DialogResult.Yes Then
        '            Label13.Visible = False
        '            scanstatuslbl.Text = "Populating Data and Printing Report.."
        '            CancelBtn.Visible = False
        '            PrintOrder()
        '            PrintReport()


        '        End If
        '        scanstatuslbl.Visible = False
        '        ScanBtn.Visible = True
        '        CancelBtn.Visible = False

        '        PalletBox.Enabled = True
        '        WorkOrderBox.Enabled = True
        '        Shift.Enabled = True
        '        Label13.Visible = False
        '        Exit Sub
        '    Else

        '    End If
        'End If

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtSearch.Text = "" Then
            LoadGrid()
        Else
            search()
            SearchGrid()
            txtSearch.Text = ""
        End If

    End Sub

    Private Sub search()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT [Pallet No]
                              FROM [CRICUT].[CUPID].[WorkOrder] WHERE [Work Order ID]='" & WID.ToString & "' AND [Serial No]='" & txtSearch.Text & "'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    PalletBox.Text = ds.Item("Pallet No")
                End While
            End If
            cmbWorkOrderBox.Sorted = True
            ds.Close()
        End If
        Conn.Close()
    End Sub

    Private Function SearchGrid()
        GetID()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        Dim strsql = "Select [Index] As [No],
                                [Serial No] As [Serial No],
                                [Carton] As [Carton]
                       
                      FROM [CRICUT].[CUPID].[WorkOrder]  
                   
                      WHERE ([Work Order ID]='" & WID.ToString & "' AND [Pallet No]= " & PalletBox.SelectedItem & " AND [Serial No]='" & txtSearch.Text & "' )"

        Dim dt = New System.Data.DataTable
        dt.Clear()
        Dim da = New SqlDataAdapter(strsql, Conn)
        da.Fill(dt)
        da.Dispose()
        DataGridView1.DataSource = dt
        DataGridView1.ClearSelection()

        Conn.Close()
    End Function

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If txtSearch.Text = "" Then
                LoadGrid()
            Else
                search()
                SearchGrid()
            End If
        End If
    End Sub

    Private Sub SubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSubGroup.SelectedIndexChanged
        LoadInfo()
    End Sub

    Private Function printnew()
        scanstatuslbl.Visible = True
        scanstatuslbl.Text = "Populating Data and Opening Excel File..."
        ScanBtn.Visible = False
        Dim localdir = IO.Directory.GetParent(IO.Directory.GetParent(Directory.GetCurrentDirectory).ToString).ToString
        Dim r1 As ReportData
        r1 = GetReportData()
        xlApp = New Excel.Application
        sourceWorkBook = xlApp.Workbooks.Open(localdir & "\" & "CartonPalletizingReport.xlsm")
        sourceWorkSheet = sourceWorkBook.Worksheets(1)
        sourceWorkSheet.Name = ("Pallet " & r1.PalletNo)
        sourceWorkSheet.Range("D3").Value = r1.PalletNo
        sourceWorkSheet.Range("D4").Value = r1.PartNo
        sourceWorkSheet.Range("D5").Value = r1.WorkOrder
        sourceWorkSheet.Range("D6").Value = r1.ProdDate.ToString("dd/MM/yyyy")
        sourceWorkSheet.Range("D7").Value = r1.Shift
        sourceWorkSheet.Range("D8").Value = r1.Model
        sourceWorkSheet.Range("D9").Value = r1.qty
        sourceWorkSheet.Range("D10").Value = r1.Line
        sourceWorkSheet.Range("B10").Value = r1.startserial
        sourceWorkSheet.Range("B11").Value = r1.startcarton
        sourceWorkSheet.Range("E10").Value = r1.endserial
        sourceWorkSheet.Range("E11").Value = r1.endcarton
        sourceWorkSheet.Range("H4").Value = r1.Description
        Dim c1 As Range = sourceWorkSheet.Range("B13:C52")
        Dim c2 As Range = sourceWorkSheet.Range("E13:F52")
        Dim c3 As Range = sourceWorkSheet.Range("H13:I52")

        Dim cnt As Integer = 0
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT [Serial No],
                                         [Carton]
                                  From [CUPID].[WorkOrder]   
                                                
                             Where [Work Order ID]='" & WID.ToString & "' AND [Pallet No]='" & PalletBox.Text & "' ORDER BY [Index] ASC"


            Dim ds = SQLcmd.ExecuteReader
            Dim i1 = 1
            Dim i2 = 1
            Dim i3 = 1
            If ds.HasRows Then
                While ds.Read

                    cnt += 1
                    If cnt <= 40 Then

                        c1.Cells(i1, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c1.Cells(i1, 2).Value = ds.Item("Carton")
                        End If

                        i1 += 1
                    ElseIf cnt > 40 And cnt <= 80 Then

                        c2.Cells(i2, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c2.Cells(i2, 2).Value = ds.Item("Carton")
                        End If

                        i2 += 1
                    ElseIf cnt > 80 And cnt <= 120 Then

                        c3.Cells(i3, 1).Value = ds.Item("Serial No")
                        If SkipCarton = 0 Then
                            c3.Cells(i3, 2).Value = ds.Item("Carton")
                        End If

                        i3 += 1
                    End If

                End While

            End If
            ds.Close()
            SQLcmd.CommandText = "SELECT [Sub Group]
                                 FROM [CUPID].[WorkOrderMaster] 
                                 Where [Work Order ID]='" & WID.ToString & "'"
            ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    r1.SubGroup = ds.Item("Sub Group")
                End While

                If r1.PalletNo = 1 Then
                    'sourceWorkSheet.SaveAs(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls")
                    sourceWorkSheet.SaveAs(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls")
                Else
                    sourceWorkSheet.SaveAs(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls")
                End If

            End If
            sourceWorkBook.Close()
            'xlApp.Quit()
            sourceWorkBook = Nothing
            xlApp = Nothing

            If r1.PalletNo > 1 Then

                Dim filePath As String = (localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls")
                Dim filePath1 As String = (localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls")
                Dim xlobj As New Excel.Application
                Dim xlobj1 As New Excel.Application
                Dim w As Workbook
                Dim w1 As Workbook
                Dim s As New Worksheet
                Dim s1 As New Worksheet

                w1 = xlobj1.Workbooks.Open(filePath1)

                xlobj1.DisplayAlerts = False
                s1 = w1.Worksheets("Pallet " & r1.PalletNo)
                s1.Delete()
                'For Each s1 In w1.Worksheets
                '    If s1.Name = ("Pallet " & r1.PalletNo) Then
                '        s1.Delete()
                '    End If
                'Next s1
                'w1.Save()
                w1.Close(True)
                xlobj1.Quit()
                w1 = Nothing


                w = xlobj.Workbooks.Open(filePath)
                w1 = xlobj.Workbooks.Open(filePath1)
                s = w.Worksheets(1)

                s1 = w1.Worksheets("Pallet " & r1.PalletNo - 1)
                s.Name = ("Pallet " & r1.PalletNo)
                s.Copy(After:=s1)

                w.Close(False)
                w1.Close(True)


                File.Delete(filePath)

                scanstatuslbl.Visible = False
                scanstatuslbl.Text = ""
                ScanBtn.Visible = True

                xlobj.Quit()
                xlobj1.Quit()
                w = Nothing
                w1 = Nothing

            End If

            If r1.PalletNo = 1 Then
                Dim filePath As String = (localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & r1.PalletNo & ".xls")
                Dim filePath1 As String = (localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls")
                Dim xlobj As New Excel.Application
                Dim xlobj1 As New Excel.Application
                Dim w As Workbook
                Dim w1 As Workbook
                Dim s As New Worksheet
                Dim s1 As New Worksheet

                w1 = xlobj1.Workbooks.Open(filePath1)

                xlobj1.DisplayAlerts = False
                s1 = w1.Worksheets("Pallet " & r1.PalletNo)
                s1.Delete()
                'For Each s1 In w1.Worksheets
                '    If s1.Name = ("Pallet " & r1.PalletNo) Then
                '        s1.Delete()
                '    End If
                'Next s1
                'w1.Save()
                w1.Close(True)
                xlobj1.Quit()

                w = xlobj.Workbooks.Open(filePath)
                w1 = xlobj.Workbooks.Open(filePath1)
                s = w.Worksheets(1)

                s1 = w1.Worksheets("Pallet " & r1.PalletNo + 1)
                s.Name = ("Pallet " & r1.PalletNo)
                s.Copy(Before:=s1)

                w.Close(False)
                w1.Close(True)


                File.Delete(filePath)

                scanstatuslbl.Visible = False
                scanstatuslbl.Text = ""
                ScanBtn.Visible = True

                xlobj.Quit()
                xlobj1.Quit()
                xlobj = Nothing
                xlobj1 = Nothing
            End If
            Conn.Close()
            'sourceWorkSheet.Range("B13:C52").Value = c1.Value
            'sourceWorkSheet.Range("E13:F52").Value = c2.Value
            'sourceWorkSheet.Range("H13:I52").Value = c3.Value
        End If
        'xlApp.Visible = True
        'If xlApp.Visible = True Then
        '    scanstatuslbl.Visible = False
        '    scanstatuslbl.Text = ""
        '    ScanBtn.Visible = True
        'End If
        'sourceWorkBook.Close()
        'sourceWorkBook = Nothing

    End Function

    Private Function reprint()
        LoadInfo()
        Dim i As Integer
        Dim xlp() As Process = Process.GetProcessesByName("EXCEL")
        For Each Process As Process In xlp
            Process.Kill()
        Next
        Dim datestart As Date = Date.Now
        For i = 1 To PalletBox.Text

            scanstatuslbl.Visible = True
            scanstatuslbl.Text = "Populating Data and Opening Excel File Pallet " & i & "..."
            ScanBtn.Visible = False
            Dim localdir = IO.Directory.GetParent(IO.Directory.GetParent(Directory.GetCurrentDirectory).ToString).ToString
            Dim r1 As ReportData
            r1 = GetReportData()
            xlApp = New Excel.Application
            sourceWorkBook = xlApp.Workbooks.Open(localdir & "\" & "CartonPalletizingReport.xlsm")
            sourceWorkSheet = sourceWorkBook.Worksheets(1)

            ' Create a QR Code with QrCoder
            Dim qrStrdata = $"{r1.WorkOrder}, {r1.SubGroup}, {r1.PalletNo}, {r1.Shift}"
            Dim qrgen As New QRCoder.QRCodeGenerator()
            Dim qrdata = qrgen.CreateQrCode(qrStrdata, QRCodeGenerator.ECCLevel.Q)
            Dim qrcode As New QRCode(qrdata)
            Dim qrImage = qrcode.GetGraphic(2)
            Dim cellRange As Excel.Range = CType(sourceWorkSheet.Range("I1:I1"), Excel.Range)

            ' Paste the QR code image at the top-right of cell "I1" without resizing the column or row
            Dim leftPosition As Double = cellRange.Left + cellRange.Width - qrImage.Width
            Dim topPosition As Double = cellRange.Top
            Clipboard.Clear()
            Clipboard.SetDataObject(qrImage, True)
            sourceWorkSheet.Paste(sourceWorkSheet.Cells(1, "I"), qrImage) ' Paste into cell "I1"
            Dim qrImageShape = sourceWorkSheet.Shapes.Item(1) ' Assumes the QR code image is the first shape in the worksheet
            qrImageShape.Left = leftPosition + 20.0
            qrImageShape.Top = topPosition





            'Dim cellRange As Excel.Range = CType(sourceWorkSheet.Range("A2:B2"), Excel.Range)
            'cellRange.Merge()
            '' If GetGraphics is 3 then -20.  If 2 then -10
            'cellRange.RowHeight = qrImage.Height - 12
            'Try
            '    Clipboard.Clear()
            '    Clipboard.SetDataObject(qrImage, True)
            '    sourceWorkSheet.Paste(sourceWorkSheet.Cells(cellRange.Row, cellRange.Column), qrImage)
            'Catch ex As System.Runtime.InteropServices.COMException When ex.ErrorCode = &H800A03EC
            '    System.Threading.Thread.Sleep(1000)
            '    sourceWorkSheet.Paste(sourceWorkSheet.Cells(cellRange.Row, cellRange.Column), qrImage)
            '    Clipboard.Clear()
            'End Try
            '' Assumes the QR code image is the first shape in the worksheet
            'Dim cellTop As Double = cellRange.Top + 1
            'Dim qrImageShape As Excel.Shape = sourceWorkSheet.Shapes.Item(1)
            'qrImageShape.Top = cellTop



            sourceWorkSheet.Name = ("Pallet " & i)
            sourceWorkSheet.Range("D3").Value = i
            sourceWorkSheet.Range("D4").Value = r1.PartNo
            sourceWorkSheet.Range("D5").Value = r1.WorkOrder
            sourceWorkSheet.Range("D6").Value = r1.ProdDate.ToString("dd/MM/yyyy")
            sourceWorkSheet.Range("D7").Value = r1.Shift
            sourceWorkSheet.Range("D8").Value = r1.Model
            sourceWorkSheet.Range("D9").Value = r1.qty
            sourceWorkSheet.Range("D10").Value = r1.Line
            sourceWorkSheet.Range("B10").Value = r1.startserial
            'sourceWorkSheet.Range("B11").Value = r1.startcarton
            sourceWorkSheet.Range("E10").Value = r1.endserial
            'sourceWorkSheet.Range("E11").Value = r1.endcarton
            sourceWorkSheet.Range("H4").Value = r1.Description
            Dim c1 As Range = sourceWorkSheet.Range("B13:C52")
            Dim c2 As Range = sourceWorkSheet.Range("E13:F52")
            Dim c3 As Range = sourceWorkSheet.Range("H13:I52")

            Dim c1_2 As Range = sourceWorkSheet.Range("B55:C104")
            Dim c2_2 As Range = sourceWorkSheet.Range("E55:F104")
            Dim c3_2 As Range = sourceWorkSheet.Range("H55:I104")

            Dim c1_3 As Range = sourceWorkSheet.Range("B109:C158")
            Dim c2_3 As Range = sourceWorkSheet.Range("E109:F158")
            Dim c3_3 As Range = sourceWorkSheet.Range("H109:I158")

            Dim c1_4 As Range = sourceWorkSheet.Range("B163:C212")
            Dim c2_4 As Range = sourceWorkSheet.Range("E163:F212")
            Dim c3_4 As Range = sourceWorkSheet.Range("H163:I212")

            Dim c1_5 As Range = sourceWorkSheet.Range("B217:C266")
            Dim c2_5 As Range = sourceWorkSheet.Range("E217:F266")
            Dim c3_5 As Range = sourceWorkSheet.Range("H217:I266")

            Dim c1_6 As Range = sourceWorkSheet.Range("B271:C320")
            Dim c2_6 As Range = sourceWorkSheet.Range("E271:F320")
            Dim c3_6 As Range = sourceWorkSheet.Range("H271:I320")

            Dim c1_7 As Range = sourceWorkSheet.Range("B325:C374")
            Dim c2_7 As Range = sourceWorkSheet.Range("E325:F374")
            Dim c3_7 As Range = sourceWorkSheet.Range("H325:I374")

            Dim c1_8 As Range = sourceWorkSheet.Range("B379:C428")
            Dim c2_8 As Range = sourceWorkSheet.Range("E379:F428")
            Dim c3_8 As Range = sourceWorkSheet.Range("H379:I428")

            Dim c1_9 As Range = sourceWorkSheet.Range("B433:C482")
            Dim c2_9 As Range = sourceWorkSheet.Range("E433:F482")
            Dim c3_9 As Range = sourceWorkSheet.Range("H433:I482")

            Dim c1_10 As Range = sourceWorkSheet.Range("B487:C536")
            Dim c2_10 As Range = sourceWorkSheet.Range("E487:F536")
            Dim c3_10 As Range = sourceWorkSheet.Range("H487:I536")

            Dim c1_11 As Range = sourceWorkSheet.Range("B541:C590")
            Dim c2_11 As Range = sourceWorkSheet.Range("E541:F590")
            Dim c3_11 As Range = sourceWorkSheet.Range("H541:I590")

            Dim c1_12 As Range = sourceWorkSheet.Range("B595:C644")
            Dim c2_12 As Range = sourceWorkSheet.Range("E595:F644")
            Dim c3_12 As Range = sourceWorkSheet.Range("H595:I644")

            Dim c1_13 As Range = sourceWorkSheet.Range("B649:C698")
            Dim c2_13 As Range = sourceWorkSheet.Range("E649:F698")
            Dim c3_13 As Range = sourceWorkSheet.Range("H649:I698")

            Dim c1_14 As Range = sourceWorkSheet.Range("B703:C752")
            Dim c2_14 As Range = sourceWorkSheet.Range("E703:F752")
            Dim c3_14 As Range = sourceWorkSheet.Range("H703:I752")

            Dim c1_15 As Range = sourceWorkSheet.Range("B757:C806")
            Dim c2_15 As Range = sourceWorkSheet.Range("E757:F806")
            Dim c3_15 As Range = sourceWorkSheet.Range("H757:I806")

            Dim c1_16 As Range = sourceWorkSheet.Range("B811:C860")
            Dim c2_16 As Range = sourceWorkSheet.Range("E811:F860")
            Dim c3_16 As Range = sourceWorkSheet.Range("H811:I860")

            Dim c1_17 As Range = sourceWorkSheet.Range("B865:C914")
            Dim c2_17 As Range = sourceWorkSheet.Range("E865:F914")
            Dim c3_17 As Range = sourceWorkSheet.Range("H865:I914")

            Dim c1_18 As Range = sourceWorkSheet.Range("B919:C968")
            Dim c2_18 As Range = sourceWorkSheet.Range("E919:F968")
            Dim c3_18 As Range = sourceWorkSheet.Range("H919:I968")

            Dim c1_19 As Range = sourceWorkSheet.Range("B973:C1022")
            Dim c2_19 As Range = sourceWorkSheet.Range("E973:F1022")
            Dim c3_19 As Range = sourceWorkSheet.Range("H973:I1022")

            Dim c1_20 As Range = sourceWorkSheet.Range("B1027:C1076")
            Dim c2_20 As Range = sourceWorkSheet.Range("E1027:F1076")
            Dim c3_20 As Range = sourceWorkSheet.Range("H1027:I1076")

            Dim c1_21 As Range = sourceWorkSheet.Range("B1081:C1130")
            Dim c2_21 As Range = sourceWorkSheet.Range("E1081:F1130")
            Dim c3_21 As Range = sourceWorkSheet.Range("H1081:I1130")

            Dim cnt As Integer = 0
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "SELECT [Serial No],[Sub Group],p.[Work Order ID],p.[Index],
                                         [Carton]
                                  From [CUPID].[WorkOrder] p   
                               INNER JOIN [CRICUT].[CUPID].[WorkOrderMaster] w
                              ON p.[Work Order ID]=w.[Work Order ID]   
                             Where p.[Work Order ID]='" & WID.ToString & "'AND [Sub Group]='" & cmbSubGroup.Text & "' AND [Pallet No]='" & i & "' ORDER BY [Index] ASC"

                Dim ds = SQLcmd.ExecuteReader
                Dim i1 = 1
                Dim i2 = 1
                Dim i3 = 1
                If ds.HasRows Then
                    While ds.Read

                        cnt += 1
                        If cnt <= 40 Then

                            c1.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 40 And cnt <= 80 Then

                            i1 = 1
                            c2.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 80 And cnt <= 120 Then

                            i2 = 1
                            c3.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 120 And cnt <= 170 Then 'page2

                            i3 = 1
                            c1_2.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_2.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 170 And cnt <= 220 Then

                            i1 = 1
                            c2_2.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_2.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 220 And cnt <= 270 Then

                            i2 = 1
                            c3_2.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_2.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 270 And cnt <= 320 Then 'page3

                            i3 = 1
                            c1_3.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_3.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 320 And cnt <= 370 Then

                            i1 = 1
                            c2_3.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_3.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 370 And cnt <= 420 Then

                            i2 = 1
                            c3_3.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_3.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 420 And cnt <= 470 Then 'page4

                            i3 = 1
                            c1_4.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_4.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 470 And cnt <= 520 Then

                            i1 = 1
                            c2_4.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_4.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 520 And cnt <= 570 Then

                            i2 = 1
                            c3_4.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_4.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 570 And cnt <= 620 Then 'page5

                            i3 = 1
                            c1_5.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_5.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 620 And cnt <= 670 Then

                            i1 = 1
                            c2_5.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_5.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 670 And cnt <= 720 Then

                            i2 = 1
                            c3_5.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_5.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 720 And cnt <= 770 Then 'page6

                            i3 = 1
                            c1_6.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_6.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 770 And cnt <= 820 Then

                            i1 = 1
                            c2_6.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_6.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 820 And cnt <= 870 Then

                            i2 = 1
                            c3_6.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_6.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 870 And cnt <= 920 Then 'page7

                            i3 = 1
                            c1_7.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_7.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 920 And cnt <= 970 Then

                            i1 = 1
                            c2_7.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_7.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 970 And cnt <= 1020 Then

                            i2 = 1
                            c3_7.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_7.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 1020 And cnt <= 1070 Then 'page8

                            i3 = 1
                            c1_8.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_8.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 1070 And cnt <= 1120 Then

                            i1 = 1
                            c2_8.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_8.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 1120 And cnt <= 1170 Then

                            i2 = 1
                            c3_8.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_8.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 1170 And cnt <= 1220 Then 'page9

                            i3 = 1
                            c1_9.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_9.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 1220 And cnt <= 1270 Then

                            i1 = 1
                            c2_9.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_9.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 1270 And cnt <= 1320 Then

                            i2 = 1
                            c3_9.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_9.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 1320 And cnt <= 1370 Then 'page10

                            i3 = 1
                            c1_10.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_10.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 1370 And cnt <= 1420 Then

                            i1 = 1
                            c2_10.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_10.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 1420 And cnt <= 1470 Then

                            i2 = 1
                            c3_10.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_10.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 1470 And cnt <= 1520 Then 'page11

                            i3 = 1
                            c1_11.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_11.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 1520 And cnt <= 1570 Then

                            i1 = 1
                            c2_11.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_11.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 1570 And cnt <= 1620 Then

                            i2 = 1
                            c3_11.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_11.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 1620 And cnt <= 1670 Then 'page12

                            i3 = 1
                            c1_12.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_12.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 1670 And cnt <= 1720 Then

                            i1 = 1
                            c2_12.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_12.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 1720 And cnt <= 1770 Then

                            i2 = 1
                            c3_12.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_12.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 1770 And cnt <= 1820 Then 'page13

                            i3 = 1
                            c1_13.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_13.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 1820 And cnt <= 1870 Then

                            i1 = 1
                            c2_13.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_13.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 1870 And cnt <= 1920 Then

                            i2 = 1
                            c3_13.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_13.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 1920 And cnt <= 1970 Then 'page14

                            i3 = 1
                            c1_14.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_14.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 1970 And cnt <= 2020 Then

                            i1 = 1
                            c2_14.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_14.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 2020 And cnt <= 2070 Then

                            i2 = 1
                            c3_14.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_14.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 2070 And cnt <= 2120 Then 'page15

                            i3 = 1
                            c1_15.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_15.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 2120 And cnt <= 2170 Then

                            i1 = 1
                            c2_15.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_15.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 2170 And cnt <= 2220 Then

                            i2 = 1
                            c3_15.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_15.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 2220 And cnt <= 2270 Then 'page16

                            i3 = 1
                            c1_16.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_16.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 2270 And cnt <= 2320 Then

                            i1 = 1
                            c2_16.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_16.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 2320 And cnt <= 2370 Then

                            i2 = 1
                            c3_16.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_16.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 2370 And cnt <= 2420 Then 'page17

                            i3 = 1
                            c1_17.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_17.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 2420 And cnt <= 2470 Then

                            i1 = 1
                            c2_17.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_17.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 2470 And cnt <= 2520 Then

                            i2 = 1
                            c3_17.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_17.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 2520 And cnt <= 2570 Then 'page18

                            i3 = 1
                            c1_18.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_18.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 2570 And cnt <= 2620 Then

                            i1 = 1
                            c2_18.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_18.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 2620 And cnt <= 2670 Then

                            i2 = 1
                            c3_18.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_18.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 2670 And cnt <= 2720 Then 'page19

                            i3 = 1
                            c1_19.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_19.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 2720 And cnt <= 2770 Then

                            i1 = 1
                            c2_19.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_19.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 2770 And cnt <= 2820 Then

                            i2 = 1
                            c3_19.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_19.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 2820 And cnt <= 2870 Then 'page20

                            i3 = 1
                            c1_20.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_20.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 2870 And cnt <= 2920 Then

                            i1 = 1
                            c2_20.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_20.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 2920 And cnt <= 2970 Then

                            i2 = 1
                            c3_20.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_20.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        ElseIf cnt > 2970 And cnt <= 3020 Then 'page21

                            i3 = 1
                            c1_21.Cells(i1, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c1_21.Cells(i1, 2).Value = ds.Item("Carton")
                            End If

                            i1 += 1
                        ElseIf cnt > 3020 And cnt <= 3070 Then

                            i1 = 1
                            c2_21.Cells(i2, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c2_21.Cells(i2, 2).Value = ds.Item("Carton")
                            End If

                            i2 += 1
                        ElseIf cnt > 3070 And cnt <= 3120 Then

                            i2 = 1
                            c3_21.Cells(i3, 1).Value = ds.Item("Serial No")
                            If SkipCarton = 0 Then
                                c3_21.Cells(i3, 2).Value = ds.Item("Carton")
                            End If

                            i3 += 1
                        End If

                    End While

                End If
                ds.Close()
                SQLcmd.CommandText =
                                "SELECT [Serial No],
                                        [Carton],
                                        [Pallet NO] 
                            FROM [CUPID].[WorkOrder] 
                            WHERE [Index]= (Select Min([Index]) From [CRICUT].[CUPID].[WorkOrder] 
                            WHERE [Work Order ID]='" & WID.ToString & "' AND [Pallet No]='" & i & "')"


                ds = SQLcmd.ExecuteReader
                If ds.HasRows Then
                    While ds.Read

                        sourceWorkSheet.Range("B11").Value = Integer.Parse(ds.Item("Carton"))

                    End While
                End If
                ds.Close()

                SQLcmd.CommandText =
                                "SELECT [Serial No],
                                        [Carton],
                                        [Pallet NO] 
                            FROM [CUPID].[WorkOrder] 
                            WHERE [Index]= (Select Max([Index]) From [CRICUT].[CUPID].[WorkOrder] 
                            WHERE [Work Order ID]='" & WID.ToString & "' AND [Pallet No]='" & i & "')"


                ds = SQLcmd.ExecuteReader
                If ds.HasRows Then
                    While ds.Read
                        sourceWorkSheet.Range("E11").Value = Integer.Parse(ds.Item("Carton"))
                    End While
                End If
                ds.Close()

                SQLcmd.CommandText = "SELECT [Sub Group]
                                 FROM [CUPID].[WorkOrderMaster] 
                                 Where [Work Order ID]='" & WID.ToString & "'"
                ds = SQLcmd.ExecuteReader
                If ds.HasRows Then
                    While ds.Read
                        r1.SubGroup = ds.Item("Sub Group")
                    End While

                    'If i = 1 Then
                    If i = 1 Then
                        If IO.File.Exists(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls") Then
                            File.Delete(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls")
                        End If
                        sourceWorkSheet.SaveAs(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls")
                        'sourceWorkSheet.SaveAs(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & i & ".xls")
                    Else
                        If IO.File.Exists(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & i & ".xls") Then
                            File.Delete(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & i & ".xls")
                        End If
                        sourceWorkSheet.SaveAs(localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & i & ".xls")
                    End If

                End If
                sourceWorkBook.Close()
                'xlApp.Quit()
                sourceWorkBook = Nothing
                xlApp = Nothing

                If i > 1 Then

                    Dim filePath As String = (localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & "_" & ("Pallet ") & i & ".xls")
                    Dim filePath1 As String = (localdir & "\bin\Result\" & r1.WorkOrder & "_" & r1.SubGroup & ".xls")
                    Dim xlobj As New Excel.Application
                    Dim xlobj1 As New Excel.Application
                    Dim w As Workbook
                    Dim w1 As Workbook
                    Dim s As New Worksheet
                    Dim s1 As New Worksheet

                    w1 = xlobj1.Workbooks.Open(filePath1)

                    xlobj1.DisplayAlerts = False
                    's1 = w1.Worksheets("Pallet " & i)
                    's1.Delete()
                    For Each s1 In w1.Worksheets
                        If s1.Name = ("Pallet " & i) Then
                            s1.Delete()
                        End If
                    Next
                    'w1.Save()
                    w1.Close(True)
                    xlobj1.Quit()
                    w1 = Nothing


                    w = xlobj.Workbooks.Open(filePath)
                    w1 = xlobj.Workbooks.Open(filePath1)
                    s = w.Worksheets(1)
                    'If i = 1 Then
                    's1 = w1.Worksheets("Pallet " & i)
                    'Else
                    s1 = w1.Worksheets("Pallet " & i - 1)
                    'End If
                    s.Name = ("Pallet " & i)
                    s.Copy(After:=s1)

                    w.Close(False)
                    w1.Close(True)


                    File.Delete(filePath)

                    scanstatuslbl.Visible = False
                    scanstatuslbl.Text = ""
                    ScanBtn.Visible = True

                    xlobj.Quit()
                    xlobj1.Quit()
                    w = Nothing
                    w1 = Nothing

                End If


            End If
            Conn.Close()

        Next
        Dim dateEnd As Date = Date.Now
        End_Excel_App(datestart, dateEnd)
    End Function



    Private Sub End_Excel_App(datestart As Date, dateEnd As Date)
        Dim xlp() As Process = Process.GetProcessesByName("EXCEL")
        For Each Process As Process In xlp
            If Process.StartTime >= datestart And Process.StartTime <= dateEnd Then
                Process.Kill()
                'Exit For
            End If
        Next
    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub count_Click(sender As Object, e As EventArgs) Handles count.Click

    End Sub

    Private Sub lblError_Click(sender As Object, e As EventArgs) Handles lblError.Click

    End Sub


    'Private Function GetReportDataALL()
    '    Dim r2 = New ReportData
    '    r2.WID = WID
    '    'r1.PalletNo = Integer.Parse(PalletBox.Text)
    '    r2.Model = Model.Text
    '    r2.ProdDate = DateTime.Now
    '    r2.WorkOrder = cmbWorkOrderBox.Text
    '    r2.Shift = Shift.Text
    '    r2.Line = lblLine.Text
    '    r2.Description = lblDescription.Text
    '    Dim Conn = New SqlConnection(connstr)
    '    Conn.Open()
    '    If Conn.State = ConnectionState.Open Then
    '        Dim SQLcmd = New SqlCommand
    '        SQLcmd.Connection = Conn
    '        SQLcmd.CommandText = "Select 
    '                                P.[Part No],
    '                                w.[Quantity],
    '                                w.[Total Order Count]
    '                      FROM [CRICUT].[CUPID].[WorkOrderMaster]  W
    '                      INNER JOIN [CRICUT].[CUPID].[PartMaster] P
    '                      ON W.[Part ID] = P.[Part ID]
    '                      WHERE (W.[Work Order ID]='" & WID.ToString & "' AND P.[Part Name]='" & part.Text & "' AND W.[Delete]='False' )"

    '        Dim ds = SQLcmd.ExecuteReader
    '        If ds.HasRows Then
    '            While ds.Read
    '                r2.PartNo = ds.Item("Part No")
    '                r2.qty = ds.Item("Quantity")
    '                r2.totalcarton = ds.Item("Total Order Count")
    '            End While
    '        End If
    '        ds.Close()
    '        SQLcmd.CommandText =
    '                        "SELECT P.[Serial No],
    '                                    P.[Carton],
    '                                    P.[Pallet No],
    '                                    W.[Sub Group],
    '                                    P.[Index]
    '                            FROM [CUPID].[WorkOrder] P 
    '                            INNER JOIN [CRICUT].[CUPID].[WorkOrderMaster]  W
    '                            ON P.[Work Order ID] = W.[Work Order ID]
    '                             WHERE P.[Index]= (Select Min([Index]) From [CRICUT].[CUPID].[WorkOrder]
    '                          WHERE P.[Work Order ID]='" & WID.ToString & "'And W.[Sub Group]='" & cmbSubGroup.Text & "')"

    '        '    And W.[Sub Group]='" & cmbSubGroup.Text & "' 
    '        '"Select [Serial No],
    '        '                       [Carton],
    '        '                             [Pallet NO] 
    '        '                FROM [CUPID].[WorkOrder] 
    '        '                WHERE [Index]= (Select Min([Index]) From [CRICUT].[CUPID].[WorkOrder] 
    '        '                WHERE [Work Order ID]='" & WID.ToString & "' AND [Pallet No]='" & PalletBox.Text & "')"



    '        ds = SQLcmd.ExecuteReader
    '        If ds.HasRows Then
    '            While ds.Read
    '                r2.startserial = ds.Item("Serial No")
    '                r2.startcarton = Integer.Parse(ds.Item("Carton"))
    '            End While
    '        End If
    '        ds.Close()
    '        SQLcmd.CommandText = "SELECT P.[Serial No],
    '                                    P.[Carton] 
    '                            FROM [CUPID].[WorkOrder] P 
    '                            INNER JOIN [CRICUT].[CUPID].[WorkOrderMaster]  W
    '                            ON P.[Work Order ID] = W.[Work Order ID]

    '                          WHERE P.[Work Order ID]='" & WID.ToString & "'AND W.[Sub Group]='" & cmbSubGroup.Text & "'"


    '        '"SELECT [Serial No],
    '        '                             [Carton] 
    '        '                      FROM [CUPID].[WorkOrder] 
    '        '                      WHERE [Index]= (Select MAX([Index]) From [CRICUT].[CUPID].[WorkOrder]
    '        '                      WHERE [Work Order ID]='" & WID.ToString & "' AND [Pallet No]='" & PalletBox.Text & "')"


    '        ds = SQLcmd.ExecuteReader
    '        If ds.HasRows Then
    '            While ds.Read
    '                r2.endserial = ds.Item("Serial No")
    '                r2.endcarton = Integer.Parse(ds.Item("Carton"))
    '            End While
    '        End If
    '        ds.Close()

    '        SQLcmd.CommandText = "SELECT [Description]
    '                            FROM [CUPID].[ModelMaster] 
    '                              WHERE [Model]= '" & Model.Text & "'"
    '        ds = SQLcmd.ExecuteReader
    '        If ds.HasRows Then
    '            While ds.Read
    '                r2.Desc = ds.Item("Description")
    '            End While
    '        End If
    '        ds.Close()

    '    End If
    '    Return r2
    'End Function


End Class
