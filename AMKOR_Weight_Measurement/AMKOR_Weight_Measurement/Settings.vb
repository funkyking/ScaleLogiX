Imports System.IO
Imports System.IO.Ports
Imports System.Xml

Public Class Settings


    'Update Stuff in Form1
    Private ReadOnly mainForm As Form1
    Public Sub New(mainFormInstance As Form1)
        InitializeComponent()
        mainForm = mainFormInstance


        ' Attach an event handler to the DataReceived event of receiver_Port
        AddHandler mainForm.receiver_Port.DataReceived, AddressOf Form1_DataReceived
    End Sub

    'Variables
    Dim bdrIN, cmIN, bdrOut, cmOut As String 'Buffer for storing dropdown Baudrate and COM Port
    Dim IsChanges = False 'Check If Any Input is Changed, Allow to save


    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopuldateDropdown()
        reset_Btn_Click(sender, e)
    End Sub



#Region "Functions"


    ' Helper function to add items to a ComboBox without duplicates
    Sub AddItemsToComboBox(comboBox As ComboBox, items As IEnumerable(Of Object))
        If comboBox.Items.Count > 0 Then
            comboBox.Items.Clear()
        End If
        For Each item In items
            If Not comboBox.Items.Contains(item) Then
                comboBox.Items.Add(item)
            End If
        Next
    End Sub


    Private Sub PopuldateDropdown()

        ' Populate ComboBox controls

        Dim baudRates As Integer() = {75, 110, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 57600, 115200, 128000, 256000}
        Dim baudRateStrings As String() = baudRates.Select(Function(rate) rate.ToString()).ToArray()
        AddItemsToComboBox(bdrIn_cmbx, baudRateStrings)
        AddItemsToComboBox(bdrOut_cmbx, baudRateStrings)

        Dim comPorts As String() = SerialPort.GetPortNames()
        AddItemsToComboBox(comIn_cmbx, comPorts)
        AddItemsToComboBox(comOut_cmbx, comPorts)

        Dim dataBitsValues As String() = {"5", "6", "7", "8"}
        AddItemsToComboBox(dataBits_cmbx, dataBitsValues)

        Dim parityValues As String() = [Enum].GetNames(GetType(Parity))
        AddItemsToComboBox(parity_cmbx, parityValues)

        Dim stopBitsValues As String() = [Enum].GetNames(GetType(StopBits))
        AddItemsToComboBox(stopBits_cmbx, stopBitsValues)

        Dim handshakeValues As String() = [Enum].GetNames(GetType(Handshake))
        AddItemsToComboBox(handshake_cmbx, handshakeValues)

        functions.loadConfig() 'Loads the Ini file
        setValues() 'set dropdownvalues
    End Sub


    Private Sub setValues()

        'Set the Values
        comIn_cmbx.SelectedItem = Variables.ComPortIn
        comOut_cmbx.SelectedItem = Variables.ComPortOut

        ' Convert the values from String to Integer for baud rates
        Dim baudrateIn As Integer
        If Integer.TryParse(Variables.BaudrateIn, baudrateIn) Then
            For Each item In bdrIn_cmbx.Items
                If item.ToString() = Variables.BaudrateIn OrElse (TypeOf item Is Integer AndAlso CType(item, Integer) = baudrateIn) Then
                    bdrIn_cmbx.SelectedItem = item
                    Exit For ' Exit the loop once a match is found
                End If
            Next
        End If

        Dim baudrateOut As Integer
        If Integer.TryParse(Variables.BaudrateOut, baudrateOut) Then
            For Each item In bdrOut_cmbx.Items
                If item.ToString() = Variables.BaudrateOut OrElse (TypeOf item Is Integer AndAlso CType(item, Integer) = baudrateOut) Then
                    bdrOut_cmbx.SelectedItem = item
                    Exit For ' Exit the loop once a match is found
                End If
            Next
        End If

    End Sub


    Private Sub SaveConfig()

        Dim path As String = Variables.IniPath

        functions.WriteValue("System", "ComPortIn", cmIN, path)
        functions.WriteValue("System", "BaudRateIn", bdrIN, path)
        functions.WriteValue("System", "ComPortOut", cmOut, path)
        functions.WriteValue("System", "BaudRateOut", bdrOut, path)


    End Sub


    Public Sub anyChanges()
        IsChanges = True
        Save_btn.Visible = True
    End Sub

#End Region


#Region "Button Events"

    'Save the Input values to Config File
    Private Sub Save_btn_Click(sender As Object, e As EventArgs) Handles Save_btn.Click
        SaveConfig()
        mainForm.loadValues()
        Save_btn.Visible = False
        MessageBox.Show("Changes Saved")
    End Sub

    'reset all config to default config file value
    Private Sub reset_Btn_Click(sender As Object, e As EventArgs) Handles reset_Btn.Click
        For Each comboBox In {bdrIn_cmbx, comIn_cmbx, bdrOut_cmbx, comOut_cmbx} _
            .Where(Function(c) c.Items.Count > 0)
            comboBox.Items.Clear()
        Next
        PopuldateDropdown()
        IsChanges = False
        Save_btn.Visible = False
    End Sub

#End Region


#Region "DropDownEvents"

    'baudrate In
    Private Sub bdrIn_cmbx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles bdrIn_cmbx.SelectedIndexChanged
        bdrIN = bdrIn_cmbx.SelectedItem.ToString
        anyChanges()
    End Sub

    'com in
    Private Sub comIn_cmbx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comIn_cmbx.SelectedIndexChanged
        cmIN = comIn_cmbx.SelectedItem.ToString
        anyChanges()
    End Sub


    'baudrate out
    Private Sub bdrOut_cmbx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles bdrOut_cmbx.SelectedIndexChanged
        bdrOut = bdrOut_cmbx.SelectedItem.ToString
        anyChanges()
    End Sub

    'baudrate in
    Private Sub comOut_cmbx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comOut_cmbx.SelectedIndexChanged
        cmOut = comOut_cmbx.SelectedItem.ToString
        anyChanges()
    End Sub

    Private Sub send_btn_Click(sender As Object, e As EventArgs) Handles send_btn.Click
        mainForm.usReceiving_txtbx.Clear()
        Dim Input As String = TextBox1.Text 'mainForm.weightScale_txtbx.Text

        If mainForm.transmitter_Port.IsOpen Then
            mainForm.transmitter_Port.Write(Input)
        End If
    End Sub




#End Region

#Region "Main"


    Private Sub togglePorts_btn_Click(sender As Object, e As EventArgs)
        mainForm.toggleBtnfunc()
    End Sub

    Private Sub RandomLoopTimer_Tick(sender As Object, e As EventArgs) Handles RandomLoopTimer.Tick
        testRndm_btn_Click(testRndm_btn, EventArgs.Empty)
    End Sub

    Private Sub AutoRandom_btn_Click(sender As Object, e As EventArgs) Handles AutoRandom_btn.Click
        If RandomLoopTimer.Enabled Then
            RandomLoopTimer.Enabled = False
            AutoRandom_btn.Text = "Auto Loop"
            AutoRandom_btn.BackColor = (DefaultBackColor)
        Else
            RandomLoopTimer.Enabled = True
            AutoRandom_btn.BackColor = Color.Red
            AutoRandom_btn.Text = "Stop"
        End If
    End Sub

    'Close the whole form
    Private Sub clseApp_btn_Click(sender As Object, e As EventArgs) Handles clseApp_btn.Click
        Try
            Application.Exit()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Restrt_btn_Click(sender As Object, e As EventArgs) Handles Restrt_btn.Click
        Try
            Application.Restart()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Settings_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Variables.adminAccess = False
    End Sub


#End Region



#Region "Debug"

    Private Sub clrLogs_btn_Click(sender As Object, e As EventArgs) Handles clrLogs_btn.Click
        Try

            Dim res As DialogResult = MessageBox.Show("Are you Sure ?", "Deleting Logs", MessageBoxButtons.YesNoCancel)
            If res = DialogResult.Yes Then

                ' Clear normals log file
                File.WriteAllText(Variables.logfilePath, String.Empty)

                ' Clear CSV log file
                File.WriteAllText(Variables.csvLogFilePath, String.Empty)

                MessageBox.Show("Log files cleared successfully.")

                mainForm.LoadLogsToDataGrid()

            Else
                Return
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while clearing log files: " & ex.Message)
        End Try
    End Sub

    Private Sub clr_receiver_Click(sender As Object, e As EventArgs) Handles clr_receiver.Click
        TextBox2.Clear()
    End Sub

    'Test Random
    Private Sub testRndm_btn_Click(sender As Object, e As EventArgs) Handles testRndm_btn.Click
        GenerateRandomEntry()
    End Sub

    Private Sub GenerateRandomEntry()
        TextBox1.Clear()
        TextBox1.Text = ""
        mainForm.ReceiverBuffer = ""
        mainForm.ReceiverOutput = ""



        If TextBox1.Text Is "" Or TextBox1.Text Is Nothing Then
            Dim random As New Random()
            Dim currentDate As String = DateTime.Now.ToString("MM/dd/yyyy")
            Dim currentTime As String = DateTime.Now.ToString("hh:mm:ss")
            Dim randomId As Integer = random.Next(1, 100)
            Dim randomNet As Double = random.NextDouble() * 20 ' Generate a random number between 0 and 20

            Dim entry As String = Chr(1) & vbCrLf &
                              "   Date: " & currentDate & vbCrLf &
                              "   Time: " & currentTime & vbCrLf &
                              "   ID No: " & randomId.ToString() & vbCrLf & vbCrLf &
                              "   Net:    " & randomNet.ToString("0.000") & " g" & vbCrLf & vbCrLf & vbCrLf &
                              Chr(4)

            TextBox1.Text = entry

            'send
            Dim Input As String = TextBox1.Text

            If mainForm.transmitter_Port.IsOpen Then
                mainForm.transmitter_Port.Write(Input)
            End If
        End If


    End Sub

    ' Event handler to handle data received in Form2
    Private Sub Form1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs)

        ' Read the received data from receiver_Port
        Dim receivedData As String = mainForm.receiver_Port.ReadExisting()

        ' Update the TextBox in Form2 with the received data
        Me.Invoke(Sub()
                      TextBox2.Text += receivedData
                  End Sub)
    End Sub

#End Region

End Class