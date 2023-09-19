Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Net
Imports System.IO
Imports System.Text

Public Class frmCheck
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim localpath = (ReadValue("System", "localpath", IniPath))
    Dim txtBoxes(9)
    Dim scanOption As Integer
    Dim statticScanOption As Integer
    Dim scanned As Integer
    Dim WID As Guid
    Dim serialCount
    Dim left As Integer
    Dim line As String
    Dim maxSerial
    Dim dt As DataTable
    Private Sub frmCheck_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeSystem()
    End Sub

    Private Sub InitializeSystem()
        Timer2.Start()
        statuslbl.Text = ""
        lblError.Text = ""
        PalletBox.Items.Clear()
        WorkOrderBox.Text = ""
        WorkOrderBox.Items.Clear()
        LoadComboBox()
        txtS1.Text = ""
        Carton.Text = ""
        txtS1.Enabled = False
        Carton.Enabled = False
        PalletBox.Text = ""
        PalletBox.Enabled = False
        CancelBtn.Visible = False
        scanstatuslbl.Visible = False
        ScanBtn.Enabled = False
        cmbSubGroup.Enabled = False
        Dim serialCount As Integer

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
            item.Enabled = False
        Next

    End Sub

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
                    WorkOrderBox.Items.Add(ds.Item("Work Order"))
                End While
            End If
            WorkOrderBox.Sorted = True
            ds.Close()
        End If

        Conn.Close()
    End Sub

    Private Function LoadInfo()
        PalletBox.Items.Clear()
        Dim maxpalletno = 0
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT w.[Work Order ID] , l.[Line] as [Line],
                                       w.[ScanOption]
                              FROM [CRICUT].[CUPID].[WorkOrderMaster] w 
                              INNER JOIN [CRICUT].[CUPID].[LineMaster] l 
                                ON w.[LineID] = l.[LineID]
                                WHERE w.[Sub Group]='" & cmbSubGroup.SelectedItem & "' And [Work Order]='" & WorkOrderBox.SelectedItem & "' AND w.[Delete]='False'"
            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    WID = ds.Item("Work Order ID")
                    scanOption = ds.Item("ScanOption")
                    statticScanOption = ds.Item("ScanOption")
                    line = ds.Item("Line")
                End While
            End If
            ds.Close()
            SQLcmd.CommandText = "SELECT [Pallet No]
                                    
                              FROM [CRICUT].[CUPID].[WorkOrder]
                                WHERE [Work Order ID]='" & WID.ToString & "'"
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


    End Function

    Private Function PalletBoxOrder()
        PalletBox.Sorted = True
    End Function

    Private Sub UpdateScanOption()
        If scanOption > left Then
            scanOption = left

        End If
    End Sub

    Private Function EnableScanPrint()
        If Not WorkOrderBox.SelectedItem Is Nothing And Not PalletBox.SelectedItem Is Nothing Then
            ScanBtn.Enabled = True
            LoadGrid()
        End If
    End Function

    Private Function LoadGrid()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        Dim strsql = "Select 
                                [Serial No] As [Serial No],
                                [Carton] As [Carton]
                       
                      FROM [CRICUT].[CUPID].[WorkOrder]  
                   
                      WHERE ([Work Order ID]='" & WID.ToString & "' AND [Pallet No]= '" & PalletBox.SelectedItem & "' )
                      ORDER BY [Production Date] DESC"

        dt = New System.Data.DataTable
        dt.Clear()
        Dim da = New SqlDataAdapter(strsql, Conn)
        da.Fill(dt)
        dt.Columns.Add("Time")
        da.Dispose()
        DataGridView1.DataSource = dt
        DataGridView1.ClearSelection()

        Conn.Close()

    End Function

    Private Sub WorkOrderBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WorkOrderBox.SelectedIndexChanged
        'Dim con1 As SqlConnection = New SqlConnection("Data Source=DESKTOP-7SPF34K\SQLEXPRESS2014;Initial Catalog=CRICUT;Integrated Security=True")

        'con1.Open()
        Dim Conn = New SqlConnection(connstr)

        Conn.Open()
        Dim strsql As String
        strsql = "Select [Sub Group] from [CRICUT].[CUPID].[WorkOrderMaster] where [Work Order]='" + WorkOrderBox.Text + "'And [Sub Group] is not null Order By [Sub Group] ASC"

        Dim cmd As New SqlCommand(strsql, Conn)
        Dim ds = cmd.ExecuteReader
        cmbSubGroup.Items.Clear()
        'cmbSubGroup = New ComboBox
        While ds.Read
            cmbSubGroup.Items.Add(ds.Item(0))
        End While
        Conn.Close()

        'LoadInfo()
        'LoadGrid()
        PalletBox.Enabled = True
        cmbSubGroup.Enabled = True

    End Sub

    Private Sub PalletBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PalletBox.SelectedIndexChanged
        LoadGrid()
        scanned = 0
        EnableScanPrint()
        GetSerialCount()
    End Sub

    Private Sub ScanBtn_Click(sender As Object, e As EventArgs) Handles ScanBtn.Click
        If PalletBox.Text <> "" Then
            scanned = 0
            scanOption = statticScanOption
            LoadGrid()
            DataGridView2.Rows.Clear()
            PalletBox.SelectedItem = 1
            Timer1.Start()
            Timer3.Start()
            WorkOrderBox.Enabled = False
            PalletBox.Enabled = False
            CancelBtn.Visible = True
            ScanBtn.Visible = False
            scanstatuslbl.Visible = True
            txtS1.Enabled = True
            Carton.Enabled = True
            txtS1.Focus()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        left = serialCount - scanned
        UpdateScanOption()
        Select Case scanOption
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

        scanstatuslbl.Text = "Scanning In Progress... (" & scanned & "/" & serialCount & ")"

        If Integer.Parse(scanned) >= Integer.Parse(serialCount) Then
            Timer1.Stop()

            MessageBox.Show("Checking complete", "Complete Operation", MessageBoxButtons.OK, MessageBoxIcon.Information)


            scanstatuslbl.Visible = False
            ScanBtn.Visible = True
            CancelBtn.Visible = False
            For Each item In txtBoxes
                item.enabled = False
            Next
            PalletBox.Enabled = True
            WorkOrderBox.Enabled = True
            Exit Sub
        Else

        End If

    End Sub

    Private Sub GetSerialCount()
        serialCount = DataGridView1.RowCount
        maxSerial = DataGridView1.RowCount
    End Sub

    Private Sub Checking(serial As String, carton As String)
        Dim passed As Boolean = False
        For Each item As DataGridViewRow In DataGridView1.Rows
            If item.Cells.Item("Serial No").Value.ToString = serial Then
                If item.Cells("Carton").Value.ToString.Trim = carton Then
                    item.Cells("time").Value = Now.ToShortTimeString
                    DataGridView1.Sort(DataGridView1.Columns("Time"), ListSortDirection.Descending)
                    passed = True
                End If
            End If
        Next

        If passed = False Then
            DataGridView2.Rows.Add(serial, carton, Now.ToShortTimeString)
        End If
    End Sub

    Private Sub txtS1_TextChanged(sender As Object, e As EventArgs) Handles txtS1.TextChanged, Carton.TextChanged, txtS2.TextChanged, txtC2.TextChanged, txtS3.TextChanged, txtC3.TextChanged, txtS4.TextChanged, txtC4.TextChanged, txtS5.TextChanged, txtC5.TextChanged
        statuslbl.Text = ""
        lblError.Text = ""
    End Sub

    Private Sub txtS1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtS1.KeyPress
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

        Dim status As String

        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If CheckDuplicate(txtS1.Text, txtS1) Then
                    statuslbl.Text = "Duplicated serial no..."
                End If
                If txtS1.Text = "" Then
                    statuslbl.Text = "Missing serial no..."
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else

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

                        Carton.Text = WebrequestWithPost2(url, Encoding.UTF8, postData2, "application/x-www-form-urlencoded")

                    Else
                        'MsgBox(check)

                        lblError.Text = check
                    End If
                    FinishScan1()

                    If txtS2.Enabled = True Then
                        txtS2.Select()
                    Else
                        Carton.Select()
                    End If
                End If


            End If
        End If
    End Sub
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
    Private Sub Carton_TextChanged(sender As Object, e As EventArgs) Handles Carton.TextChanged
        statuslbl.Text = ""
        lblError.Text = ""
    End Sub

    Private Sub Carton_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Carton.KeyPress
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If Carton.Text = "" Then
                    statuslbl.Text = "Missing carton..."
                End If
                If CheckDuplicate(Carton.Text, Carton) Then
                    statuslbl.Text = "Duplicated carton..."
                End If
                If Not IsNumeric(Carton.Text) And Not Carton.Text = "" Then
                    statuslbl.Text = "Invalid Carton Number..."
                End If

                If Carton.Text.Length < 1 OrElse Carton.Text.Length > 4 And Carton.Text <> "" Then
                    statuslbl.Text = "Carton Number Must Be 1-4 Digit "
                End If


                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If scanOption = "1" Then

                        Checking(txtS1.Text, Carton.Text)

                        scanned += scanOption
                        For Each item In txtBoxes
                            item.Text = ""
                        Next
                        txtS1.Select()
                    Else
                        txtC2.Select()
                    End If


                End If
            End If

        End If

    End Sub
    Private Function FinishScan1()
        If Timer1.Enabled Then

            If Carton.Text = "" Then
                    statuslbl.Text = "Missing carton..."
                End If
                If CheckDuplicate(Carton.Text, Carton) Then
                    statuslbl.Text = "Duplicated carton..."
                End If
                If Not IsNumeric(Carton.Text) And Not Carton.Text = "" Then
                    statuslbl.Text = "Invalid Carton Number..."
                End If

                If Carton.Text.Length < 1 OrElse Carton.Text.Length > 4 And Carton.Text <> "" Then
                    statuslbl.Text = "Carton Number Must Be 1-4 Digit "
                End If


                If statuslbl.Text <> "" Then
                    Exit Function
                Else
                    If scanOption = "1" Then

                        Checking(txtS1.Text, Carton.Text)

                        scanned += scanOption
                        For Each item In txtBoxes
                            item.Text = ""
                        Next
                        txtS1.Select()
                    Else
                        txtC2.Select()
                    End If

            End If

        End If
    End Function




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
                    statuslbl.Text = "Missing serial no..."
                End If
                If CheckDuplicate(txtS2.Text, txtS2) Then
                    statuslbl.Text = "Duplicated serial no..."
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    method = "POST"
                    url = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                    dataSetStr = "CHECK_CARTON_STATE"
                    para1 = txtS2.Text
                    para2 = txtS1.Text
                    postData = "dataSetStr=" & dataSetStr & "&para1=" & para1
                    postData2 = "dataSetStr=" & dataSetStr & "&para1=" & para2
                    If scanOption = "2" Then
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

                        FinishScan2()
                    Else
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
                        FinishScan2()
                    End If


                    If txtS3.Enabled = True Then
                        txtS3.Select()
                    Else
                        Carton.Select()
                    End If
                End If
            End If
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
                    statuslbl.Text = "Missing serial no..."
                End If
                If CheckDuplicate(txtS3.Text, txtS3) Then
                    statuslbl.Text = "Duplicated serial no..."
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else

                    method = "POST"
                    url = "http://192.168.96.202:3000/api/DMS/postFullLoadDataListJson"
                    dataSetStr = "CHECK_CARTON_STATE"
                    para1 = txtS1.Text
                    para2 = txtS2.Text
                    para3 = txtS3.Text
                    postData = "dataSetStr=" & dataSetStr & "&para1=" & para1
                    postData2 = "dataSetStr=" & dataSetStr & "&para1=" & para2
                    postData3 = "dataSetStr=" & dataSetStr & "&para1=" & para3

                    If scanOption = "3" Then
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

                        FinishScan3()
                    Else
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
                        FinishScan3()
                    End If


                    If txtS4.Enabled = True Then
                        txtS4.Select()
                    Else
                        Carton.Select()
                    End If
                End If
            End If
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
                    statuslbl.Text = "Missing serial no..."
                End If
                If CheckDuplicate(txtS4.Text, txtS4) Then
                    statuslbl.Text = "Duplicated serial no..."
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
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

                    If scanOption = "4" Then
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
                        FinishScan4()
                    Else
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
                        FinishScan4()
                    End If


                    If txtS5.Enabled = True Then
                        txtS5.Select()
                    Else
                        Carton.Select()
                    End If
                End If
            End If
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
                    statuslbl.Text = "Missing serial no..."
                End If
                If CheckDuplicate(txtS5.Text, txtS5) Then
                    statuslbl.Text = "Duplicated serial no..."
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
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

                    If scanOption = "5" Then
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
                        FinishScan5()
                    Else
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
                        FinishScan5()
                    End If
                End If
            End If
        End If
    End Sub



    Private Sub txtC2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtC2.KeyPress
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtC2.Text = "" Then
                    statuslbl.Text = "Missing carton..."
                End If
                If Not IsNumeric(txtC2.Text) And Not txtC2.Text = "" Then
                    statuslbl.Text = "Invalid Carton Number..."
                End If
                If txtC2.Text.Length < 1 OrElse txtC2.Text.Length > 4 And txtC2.Text <> "" Then
                    statuslbl.Text = "Carton Number Must Be 1-4 Digit "
                End If
                If CheckDuplicate(txtC2.Text, txtC2) Then
                    statuslbl.Text = "Duplicated carton..."
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If scanOption = "2" Then

                        Checking(txtS1.Text, Carton.Text)
                        Checking(txtS2.Text, txtC2.Text)

                        scanned += scanOption
                        For Each item In txtBoxes
                            item.Text = ""
                        Next
                        txtS1.Select()
                    Else
                        txtC3.Select()
                    End If


                End If
            End If

        End If
    End Sub

    Private Function FinishScan2()
        If Timer1.Enabled Then

            If txtC2.Text = "" Then
                    statuslbl.Text = "Missing carton..."
                End If
                If Not IsNumeric(txtC2.Text) And Not txtC2.Text = "" Then
                    statuslbl.Text = "Invalid Carton Number..."
                End If
                If txtC2.Text.Length < 1 OrElse txtC2.Text.Length > 4 And txtC2.Text <> "" Then
                    statuslbl.Text = "Carton Number Must Be 1-4 Digit "
                End If
                If CheckDuplicate(txtC2.Text, txtC2) Then
                    statuslbl.Text = "Duplicated carton..."
                End If
                If statuslbl.Text <> "" Then
                Exit Function
            Else
                    If scanOption = "2" Then

                        Checking(txtS1.Text, Carton.Text)
                        Checking(txtS2.Text, txtC2.Text)

                        scanned += scanOption
                        For Each item In txtBoxes
                            item.Text = ""
                        Next
                        txtS1.Select()
                    Else
                        txtC3.Select()
                    End If

                End If

            End If
    End Function


    Private Sub txtC3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtC3.KeyPress
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtC3.Text = "" Then
                    statuslbl.Text = "Missing carton..."
                End If
                If Not IsNumeric(txtC3.Text) And Not txtC3.Text = "" Then
                    statuslbl.Text = "Invalid Carton Number..."
                End If
                If txtC3.Text.Length < 1 OrElse txtC3.Text.Length > 4 And txtC3.Text <> "" Then
                    statuslbl.Text = "Carton Number Must Be 1-4 Digit "
                End If
                If CheckDuplicate(txtC3.Text, txtC3) Then
                    statuslbl.Text = "Duplicated carton..."
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If scanOption = "3" Then

                        Checking(txtS1.Text, Carton.Text)
                        Checking(txtS2.Text, txtC2.Text)
                        Checking(txtS3.Text, txtC3.Text)
                        scanned += scanOption
                        For Each item In txtBoxes
                            item.Text = ""
                        Next
                        txtS1.Select()
                    Else
                        txtC4.Select()
                    End If


                End If
            End If

        End If
    End Sub

    Private Function FinishScan3()
        If Timer1.Enabled Then

            If txtC3.Text = "" Then
                    statuslbl.Text = "Missing carton..."
                End If
                If Not IsNumeric(txtC3.Text) And Not txtC3.Text = "" Then
                    statuslbl.Text = "Invalid Carton Number..."
                End If
                If txtC3.Text.Length < 1 OrElse txtC3.Text.Length > 4 And txtC3.Text <> "" Then
                    statuslbl.Text = "Carton Number Must Be 1-4 Digit "
                End If
                If CheckDuplicate(txtC3.Text, txtC3) Then
                    statuslbl.Text = "Duplicated carton..."
                End If
                If statuslbl.Text <> "" Then
                Exit Function
            Else
                    If scanOption = "3" Then

                        Checking(txtS1.Text, Carton.Text)
                        Checking(txtS2.Text, txtC2.Text)
                        Checking(txtS3.Text, txtC3.Text)
                        scanned += scanOption
                        For Each item In txtBoxes
                            item.Text = ""
                        Next
                        txtS1.Select()
                    Else
                        txtC4.Select()
                    End If

                End If

            End If
    End Function

    Private Sub txtC4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtC4.KeyPress
        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtC4.Text = "" Then
                    statuslbl.Text = "Missing carton..."
                End If
                If Not IsNumeric(txtC4.Text) And Not txtC4.Text = "" Then
                    statuslbl.Text = "Invalid Carton Number..."
                End If
                If txtC4.Text.Length < 1 OrElse txtC4.Text.Length > 4 And txtC4.Text <> "" Then
                    statuslbl.Text = "Carton Number Must Be 1-4 Digit "
                End If
                If CheckDuplicate(txtC4.Text, txtC4) Then
                    statuslbl.Text = "Duplicated carton..."
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    If scanOption = "4" Then

                        Checking(txtS1.Text, Carton.Text)
                        Checking(txtS2.Text, txtC2.Text)
                        Checking(txtS3.Text, txtC3.Text)
                        Checking(txtS4.Text, txtC4.Text)
                        scanned += scanOption
                        For Each item In txtBoxes
                            item.Text = ""
                        Next
                        txtS1.Select()
                    Else
                        txtC5.Select()
                    End If


                End If
            End If

        End If
    End Sub

    Private Function FinishScan4()
        If Timer1.Enabled Then

            If txtC4.Text = "" Then
                    statuslbl.Text = "Missing carton..."
                End If
                If Not IsNumeric(txtC4.Text) And Not txtC4.Text = "" Then
                    statuslbl.Text = "Invalid Carton Number..."
                End If
                If txtC4.Text.Length < 1 OrElse txtC4.Text.Length > 4 And txtC4.Text <> "" Then
                    statuslbl.Text = "Carton Number Must Be 1-4 Digit "
                End If
                If CheckDuplicate(txtC4.Text, txtC4) Then
                    statuslbl.Text = "Duplicated carton..."
                End If
                If statuslbl.Text <> "" Then
                    Exit Function

                Else
                    If scanOption = "4" Then

                        Checking(txtS1.Text, Carton.Text)
                        Checking(txtS2.Text, txtC2.Text)
                        Checking(txtS3.Text, txtC3.Text)
                        Checking(txtS4.Text, txtC4.Text)
                        scanned += scanOption
                        For Each item In txtBoxes
                            item.Text = ""
                        Next
                        txtS1.Select()
                    Else
                        txtC5.Select()
                    End If


                End If


            End If
    End Function
    Private Sub txtC5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtC5.KeyPress

        If Timer1.Enabled Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtC5.Text = "" Then
                    statuslbl.Text = "Missing carton..."
                End If
                If Not IsNumeric(txtC5.Text) And Not txtC5.Text = "" Then
                    statuslbl.Text = "Invalid Carton Number..."
                End If
                If txtC5.Text.Length < 1 OrElse txtC5.Text.Length > 4 And txtC5.Text <> "" Then
                    statuslbl.Text = "Carton Number Must Be 1-4 Digit "
                End If
                If CheckDuplicate(txtC5.Text, txtC5) Then
                    statuslbl.Text = "Duplicated carton..."
                End If
                If statuslbl.Text <> "" Then
                    Exit Sub
                Else
                    Checking(txtS1.Text, Carton.Text)
                    Checking(txtS2.Text, txtC2.Text)
                    Checking(txtS3.Text, txtC3.Text)
                    Checking(txtS4.Text, txtC4.Text)
                    Checking(txtS5.Text, txtC5.Text)
                    scanned += scanOption
                    For Each item In txtBoxes
                        item.Text = ""
                    Next
                    txtS1.Select()
                End If
            End If
        End If
    End Sub

    Private Function FinishScan5()
        If Timer1.Enabled Then
            If txtC5.Text = "" Then
                statuslbl.Text = "Missing carton..."
            End If
            If Not IsNumeric(txtC5.Text) And Not txtC5.Text = "" Then
                statuslbl.Text = "Invalid Carton Number..."
            End If
            If txtC5.Text.Length < 1 OrElse txtC5.Text.Length > 4 And txtC5.Text <> "" Then
                statuslbl.Text = "Carton Number Must Be 1-4 Digit "
            End If
            If CheckDuplicate(txtC5.Text, txtC5) Then
                statuslbl.Text = "Duplicated carton..."
            End If
            If statuslbl.Text <> "" Then
                Exit Function

            Else
                Checking(txtS1.Text, Carton.Text)
                Checking(txtS2.Text, txtC2.Text)
                Checking(txtS3.Text, txtC3.Text)
                Checking(txtS4.Text, txtC4.Text)
                Checking(txtS5.Text, txtC5.Text)
                scanned += scanOption
                For Each item In txtBoxes
                    item.Text = ""
                Next
                txtS1.Select()
            End If

        End If
    End Function
    Private Sub lblTest_Click(sender As Object, e As EventArgs)
        UpdateScanOption()
    End Sub

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        WorkOrderBox.Enabled = True
        PalletBox.Enabled = True
        CancelBtn.Visible = False
        ScanBtn.Visible = True
        scanstatuslbl.Visible = False
        Timer1.Stop()
        statuslbl.Text = ""
        lblError.Text = ""
        For Each item In txtBoxes
            item.Enabled = False
            item.Text = ""
        Next
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        For Each item As DataGridViewRow In DataGridView1.Rows
            If item.Cells.Item("Time").Value.ToString <> "" Then
                item.DefaultCellStyle.BackColor = Color.Lime
            End If
        Next
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim count As Integer = GetTotalCount()
        Dim res = MessageBox.Show("Confirm to delete?" & vbCrLf & "Serial No:" & DataGridView1.CurrentRow.Cells("Serial No").Value.ToString & vbCrLf & "Carton:" & DataGridView1.CurrentRow.Cells("Carton").Value.ToString, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
        If res = DialogResult.Yes Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "DELETE FROM [CUPID].[WorkOrder]
                                 WHERE [Serial No]='" & DataGridView1.CurrentRow.Cells("Serial No").Value.ToString & "'"
                'remove 30082023 WHERE [Work Order ID]='" & WID.ToString & "' AND [Pallet No]='" & PalletBox.Text & "' 
                'And [Serial No]='" & DataGridView1.CurrentRow.Cells("Serial No").Value.ToString & "' AND [Carton]='" & DataGridView1.CurrentRow.Cells("Carton").Value.ToString & "'"

                SQLcmd.ExecuteNonQuery()
                SQLcmd.CommandText = "UPDATE [CUPID].[WorkOrderMaster]
                                 SET [Count]='" & count - 1 & "'
                                 WHERE [Work Order ID]='" & WID.ToString & "' AND [DELETE]='False' "
                SQLcmd.ExecuteNonQuery()

            End If
            Conn.Close()
            DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
        End If

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim count As Integer = GetTotalCount()

        If DataGridView1.RowCount >= maxSerial Then
            MessageBox.Show("Maximum serial no count reached. Please delete serial before adding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim res = MessageBox.Show("Confirm to add serial?" & vbCrLf & "Serial No:" & DataGridView2.CurrentRow.Cells("serialNo").Value.ToString & vbCrLf & "Carton:" & DataGridView2.CurrentRow.Cells("cartonNo").Value.ToString, "Confirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
        If res = DialogResult.Yes Then
            Dim currSerial As String = DataGridView2.CurrentRow.Cells("serialNo").Value.ToString
            Dim currCarton As String = DataGridView2.CurrentRow.Cells("cartonNo").Value.ToString
            Dim currTime As String = DataGridView2.CurrentRow.Cells("time").Value.ToString
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
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
                                                ,'" & line & "'
                                                ,'" & currSerial & "'
                                                ,'" & currCarton & "'
                                                ,'" & PalletBox.SelectedItem & "'
                                                ,'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                                ,'DAY'
                                                ,'False'
                                                ,'False')"
                SQLcmd.ExecuteNonQuery()
                SQLcmd.CommandText = "UPDATE [CUPID].[WorkOrderMaster]
                                 SET [Count]='" & count + 1 & "'
                                 WHERE [Work Order ID]='" & WID.ToString & "' AND [DELETE]='False' "
                SQLcmd.ExecuteNonQuery()

            End If
            Conn.Close()
            DataGridView2.Rows.Remove(DataGridView2.CurrentRow)
            Dim newdt = dt.NewRow()
            dt.Rows.Add(currSerial, currCarton, currTime)
        End If
    End Sub

    Private Function GetTotalCount()
        Dim totalCount As Integer
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        If Conn.State = ConnectionState.Open Then
            Dim SQLcmd = New SqlCommand
            SQLcmd.Connection = Conn
            SQLcmd.CommandText = "SELECT [Count]
                              FROM [CRICUT].[CUPID].[WorkOrderMaster] WHERE [Work Order ID]='" & WID.ToString & "' AND[Delete]='False' "
            Dim ds = SQLcmd.ExecuteReader
            If ds.HasRows Then
                While ds.Read
                    totalCount = ds.Item("Count")
                End While
            End If
            ds.Close()
        End If

        Conn.Close()
        Return totalCount
    End Function

    Private Sub btnManual_Click(sender As Object, e As EventArgs) Handles btnManual.Click
        If Carton.Text = "" Then
            statuslbl.Text = "Missing carton..."
        End If

        If Not IsNumeric(Carton.Text) And Not Carton.Text = "" Then
            statuslbl.Text = "Invalid Carton Number..."
        End If

        If Carton.Text.Length < 1 OrElse Carton.Text.Length > 4 And Carton.Text <> "" Then
            statuslbl.Text = "Carton Number Must Be 1-4 Digit "
        End If
        If CheckDuplicate(txtS1.Text, txtS1) Then
            statuslbl.Text = "Duplicate Serial No..."
        End If
        If CheckDuplicate(Carton.Text, Carton) Then
            statuslbl.Text = "Duplicate Carton No..."
        End If
        If statuslbl.Text <> "" Then
            Exit Sub
        Else
            Checking(txtS1.Text, Carton.Text)

            scanned += 1
            For Each item In txtBoxes
                item.Text = ""
            Next
            txtS1.Select()
        End If

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim f1 = New frmHelp
        f1.helptitle = "check"
        f1.Owner = Me
        f1.ShowDialog()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        lblCount.Text = "Count: " & DataGridView1.RowCount
        If DataGridView2.RowCount = 0 Then
            btnAdd.Enabled = False
        Else
            btnAdd.Enabled = True
        End If
    End Sub

    Private Function CheckDuplicate(input As String, myTxtbox As TextBox)
        For Each item As DataGridViewRow In DataGridView1.Rows
            If item.Cells("Time").Value.ToString <> "" And (input = item.Cells("Serial No").Value.ToString Or input = item.Cells("Carton").Value.ToString) Then
                Return True
            Else
                Dim Conn = New SqlConnection(connstr)
                Conn.Open()
                If Conn.State = ConnectionState.Open Then
                    Dim SQLcmd = New SqlCommand
                    SQLcmd.Connection = Conn
                    SQLcmd.CommandText = "SELECT p.[Serial No],w.[Work Order],w.[Sub Group]
                              From [CRICUT].[CUPID].[WorkOrder] p
                              INNER Join [CRICUT].[CUPID].[WorkOrderMaster] w
                              On p.[Work Order ID]=w.[Work Order ID]
                              WHERE NOT p.[Pallet No] = '" & PalletBox.Text & "' AND w.[Sub Group] = '" & cmbSubGroup.Text & "' AND w.[Work Order] = '" & WorkOrderBox.Text & "' AND ([Serial No]='" & input & "' OR [Carton]='" & input & "')"


                    '"SELECT p.[Serial No],w.[Work Order],w.[Sub Group]
                    '          FROM [CRICUT].[CUPID].[WorkOrder] p
                    '          INNER JOIN [CRICUT].[CUPID].[WorkOrderMaster] w
                    '          ON p.[Work Order ID]=w.[Work Order ID]
                    '          WHERE NOT [Pallet No] = '" & PalletBox.Text & "' AND [Sub Group] = '" & cmbSubGroup.Text & "' AND ([Serial No]='" & input & "' OR [Carton]='" & input & "')"
                    Dim ds = SQLcmd.ExecuteReader
                    If ds.HasRows Then
                        Return True
                    End If
                End If
                Conn.Close()
            End If
        Next

        For Each item As DataGridViewRow In DataGridView2.Rows
            If item.Cells("Time").Value.ToString <> "" And (input = item.Cells("serialNo").Value.ToString Or input = item.Cells("cartonNo").Value.ToString) Then
                Return True
            End If
        Next

        For Each item In txtBoxes
            If item IsNot myTxtbox And input <> "" Then
                If input = item.text Or input = item.text Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Sub SubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSubGroup.SelectedIndexChanged
        LoadInfo()
    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub
End Class