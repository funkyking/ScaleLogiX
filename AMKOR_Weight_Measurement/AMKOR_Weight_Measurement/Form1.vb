Imports System.ComponentModel
Imports System.IO
Imports System.Media
Imports System.Net.Security
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class Form1

    'Background Worker For DataGrid
    Private WithEvents bgWorker As New BackgroundWorker()


    'Initialzation
    Public Sub New()
        InitializeComponent()

        ' Initialize the BackgroundWorker
        bgWorker.WorkerReportsProgress = True
        bgWorker.WorkerSupportsCancellation = True

        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None


    End Sub

    'Form Load
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            loadValues()
            toggleBtnfunc()
        Catch ex As Exception
        End Try
    End Sub


#Region "Variables"

    'Instances
    Private stgsFrm As Settings

    'Transimission Data Cache
    Dim latestAddedTime As String = ""
    Dim LastestAddedID As String = ""
    Dim LatestAddedNet As String = ""
    Dim TotalScanned As Integer = 0

    'Port Connection Indicator
    Dim allPortsOpen As Boolean = False
    Dim IsTxOpen As Boolean = False
    Dim IsRxOpen As Boolean = False

#End Region


#Region "Functions"

    'Loads with the values from our Ini Files and Global Variables
    Public Sub loadValues()
        Try
            If (allPortsOpen) Then
                toggleBtnfunc()
            End If

            EnsureDirectories()
            functions.loadConfig()


            receiver_Port.PortName = Variables.ComPortIn
            receiver_Port.BaudRate = Variables.BaudrateIn
            transmitter_Port.PortName = Variables.ComPortOut
            transmitter_Port.BaudRate = Variables.BaudrateOut


            If Variables.DataBits IsNot "" Then
                receiver_Port.DataBits = Variables.DataBits
                transmitter_Port.DataBits = Variables.DataBits
            End If

            If Variables.Parity IsNot "" Then
                receiver_Port.Parity = Variables.Parity
                transmitter_Port.Parity = Variables.Parity
            End If

            If Variables.ParityReplace IsNot "" Then
                receiver_Port.ParityReplace = Variables.ParityReplace
                transmitter_Port.ParityReplace = Variables.ParityReplace
            End If

            If Variables.StopBits IsNot "" Then
                receiver_Port.StopBits = Variables.StopBits
                transmitter_Port.StopBits = Variables.StopBits
            End If

            If Variables.Handshake IsNot "" Then
                receiver_Port.Handshake = Variables.Handshake
                transmitter_Port.Handshake = Variables.Handshake
            End If


            'LoadLogsToDataGrid()
            LoadLogsToDataGridAsync()

        Catch ex As Exception
        End Try
    End Sub

    Dim open_flag As Boolean = False
    'Toggles All Ports and Ui Elements
    Public Sub toggleBtnfunc()
        If Not open_flag Then
            open_flag = True
            Label1.Text = "Force Open"
        Else
            open_flag = False
            Label1.Text = "No Force"
        End If

        Try
            If Not transmitter_Port.IsOpen Then
                IsTxOpen = True
                transmitter_Port.Open()
            Else
                IsTxOpen = False
                transmitter_Port.Close()
            End If

            If Not receiver_Port.IsOpen Then
                IsRxOpen = True
                receiver_Port.Open()
            Else
                IsRxOpen = False
                receiver_Port.Close()
            End If

            UpdateUIBasedOnPortStatusAsync()

        Catch ex As Exception
        End Try
    End Sub


    'interval checking of ports if they are open
    Public Sub CheckingPortStatus()
        If open_flag Then
            If Not transmitter_Port.IsOpen Then
                Try
                    transmitter_Port.Open()
                Catch ex As Exception
                End Try
                If transmitter_Port.IsOpen Then
                    IsTxOpen = True
                Else
                    IsTxOpen = False
                End If
            End If
            If Not receiver_Port.IsOpen Then
                Try
                    receiver_Port.Open()
                Catch ex As Exception
                End Try
                If receiver_Port.IsOpen Then
                    IsRxOpen = True
                Else
                    IsRxOpen = False
                End If
            End If
        Else
            If Not transmitter_Port.IsOpen Then
                IsTxOpen = False
            Else
                IsTxOpen = True
            End If

            If Not receiver_Port.IsOpen Then
                IsRxOpen = False
            Else
                IsRxOpen = True
            End If
        End If
    End Sub

    'Updating the ui to indicate the status of the ports or connections
    Private Async Sub UpdateUIBasedOnPortStatusAsync()
        Await Task.Run(Sub()
                           ' Perform background tasks
                           CheckingPortStatus()
                           allPortsOpen = IsTxOpen AndAlso IsRxOpen
                       End Sub)

        ' Update UI controls on the UI thread
        Me.Invoke(Sub()
                      allStatus_pbx.Image = If(allPortsOpen, My.Resources.In_Progress4, My.Resources.error_1)
                      rxStatus_pbx.Image = If(IsRxOpen, My.Resources.In_Progress, My.Resources.No_Network)
                      txStatus_pbx.Image = If(IsTxOpen, My.Resources.In_Progress, My.Resources.No_Network)

                      Dim txtBdr As String = "Baudrate"
                      Dim cmlbl As String = "COM"

                      inNm_lbl.Text = If(IsRxOpen, $"{receiver_Port.PortName}", $"{cmlbl}")
                      inBdr_lbl.Text = If(IsRxOpen, $"{receiver_Port.BaudRate.ToString}", $"{txtBdr}")

                      trNm_lbl.Text = If(IsTxOpen, $"{transmitter_Port.PortName}", $"{cmlbl}")
                      trBdr_lbl.Text = If(IsTxOpen, transmitter_Port.BaudRate.ToString, $"{txtBdr}")
                  End Sub)
    End Sub


#End Region



#Region "Receiver"

    Private dataProcessing As Boolean = False
    Public Shared DuplicateOutput As String = ""
    Public Shared ReceiverBuffer As String = ""
    Public Shared ReceiverOutput As String = ""


    ' Receive Data From Weighing Scale, Then Once it hits chr(4), it will send save to logging files and Send Data to Printer
    Private Sub receiver_Port_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs) Handles receiver_Port.DataReceived
        Try
            ReceiverBuffer = receiver_Port.ReadExisting()

            If Len(ReceiverBuffer) > 0 AndAlso Not dataProcessing Then Me.Invoke(New EventHandler(AddressOf DoUpdateComOut))
            Application.DoEvents()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DoUpdateComOut(sender As Object, e As EventArgs)
        Try
            dataProcessing = True

            ReceiverOutput += ReceiverBuffer

            ' Cause Delay Error, So not using it
            'If ReceiverBuffer = Chr(4) Then
            'End If
            If ReceiverOutput.Contains(Chr(1)) AndAlso ReceiverOutput.Contains(Chr(4)) Then
                Dim startIndex As Integer = ReceiverOutput.IndexOf(Chr(1))
                Dim endIndex As Integer = ReceiverOutput.IndexOf(Chr(4))

                Dim dataChunk As String = ReceiverOutput.Substring(startIndex, endIndex - startIndex + 1)

                'If dataChunk = DuplicateOutput Then
                '    ReceiverOutput = vbEmpty
                '    ReceiverBuffer = vbEmpty
                'Else
                '    Me.Invoke(Sub()
                '                  usReceiving_txtbx.Text = dataChunk
                '                  LogWeightToFile(dataChunk)
                '                  usSending_txtbx.Text = dataChunk
                '                  SendDataToPrinter(dataChunk)
                '                  DuplicateOutput = dataChunk
                '              End Sub)
                '    ReceiverOutput = vbEmpty
                'End If

                Me.Invoke(Sub()
                              usReceiving_txtbx.Text = dataChunk
                              LogWeightToFile(dataChunk)
                              usSending_txtbx.Text = dataChunk
                              SendDataToPrinter(dataChunk)
                          End Sub)
                ReceiverOutput = ""



            End If
        Catch ex As Exception
        Finally
            dataProcessing = False
        End Try
    End Sub

    'Send Data To transmitter_Port
    Private Sub SendDataToPrinter(input As String)
        'printerReceiving_txtbx.Clear()
        'If receiver_Port.IsOpen Then
        '    receiver_Port.Write(ReceiverOutput)
        'End If

        Try
            If transmitter_Port.IsOpen Then
                transmitter_Port.Write(input)
            End If
            PlayBackgroundSoundFile()
        Catch ex As Exception
        End Try
    End Sub

    Sub PlayBackgroundSoundFile()
        Try
            Dim soundPlayer As New SoundPlayer(My.Resources.ding)
            soundPlayer.Play()
        Catch ex As Exception
        End Try
    End Sub




#End Region



#Region "logging"


    'Log Data To WeightFolder and LogFolder at [C:\GWWeight]
    Private Sub LogWeightToFile(InputData As String)

        Try
            ' Trim any leading and trailing non-printable characters
            InputData = InputData.Trim(ControlChars.NullChar)

            Dim startIndex As Integer = CInt(InputData.IndexOf("Net:") + "Net:".Length)
            Dim endIndex As Integer = CInt(InputData.IndexOf("g", startIndex))

            If startIndex >= 0 AndAlso endIndex >= 0 Then
                Dim extractedValue As String = CType(InputData.Substring(startIndex, endIndex - startIndex).Trim(), String)
                Dim csvline As String = ""

                'Check if Directories or files exist or created
                EnsureDirectories()

                ' If the Weight_log file exists, add data to newline; otherwise, create a new file
                Using writelog As StreamWriter = New StreamWriter(Variables.logfilePath, True)

                    ' Remove the first and last characters
                    InputData = InputData.Substring(1, InputData.Length - 2)

                    ' Remove empty lines
                    Dim lines() As String = InputData.Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
                    Dim filteredData As String = String.Join(vbCrLf, lines)

                    ' Extract required values from specific lines
                    Dim dateLine As String = lines(0).Substring(lines(0).IndexOf(":") + 2)
                    Dim timeLine As String = lines(1).Substring(lines(1).IndexOf(":") + 2)
                    Dim idLine As String = lines(2).Substring(lines(2).IndexOf(":") + 2)
                    Dim netLine As String = lines(3).Substring(lines(3).IndexOf(":") + 2).Trim()


                    ' For Status as latest Added Value to display to user
                    latestAddedTime = DateTime.Now.ToShortTimeString
                    LastestAddedID = idLine
                    LatestAddedNet = netLine


                    csvline = $"{dateLine},{timeLine},{idLine},{netLine}"


                    writelog.WriteLine("--------------------------")
                    writelog.Write(filteredData)
                    writelog.WriteLine()
                End Using

                Using writecsvlog As StreamWriter = New StreamWriter(Variables.csvLogFilePath, True)
                    writecsvlog.WriteLine(csvline)
                End Using
                'LoadLogsToDataGrid() 'Update Datagrid With New Value
                LoadLogsToDataGridAsync()

                Dim allLines As String() = File.ReadAllLines(Variables.csvLogFilePath)
                TotalScanned = allLines.Length 'Gets the total scanned for the day from the file


                For Each deleteFile In Directory.GetFiles(Variables.MaindirectoryPath, "*.txt", SearchOption.TopDirectoryOnly)
                    File.Delete(deleteFile)
                Next

                ' If the Weight file exists, overwrite it; otherwise, create a new file
                Using writer As StreamWriter = New StreamWriter(Variables.WeightFilePath, False)
                    writer.Write(extractedValue)
                End Using
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try


    End Sub

    Public Sub EnsureDirectories()
        Try
            ' Create the Weight directory if it doesn't exist
            If Not Directory.Exists(Variables.MaindirectoryPath) Then
                Directory.CreateDirectory(Variables.MaindirectoryPath)
            End If

            ' Create the Log directory if it doesn't exist
            If Not Directory.Exists(Variables.logDirectoryPath) Then
                Directory.CreateDirectory(Variables.logDirectoryPath)
            End If

            If Not Directory.Exists(Variables.csvLogDirectoryPath) Then
                Directory.CreateDirectory(Variables.csvLogDirectoryPath)
            End If

            If Not File.Exists(Variables.IniPath) Then
                'Default Values
                Dim path As String = Variables.IniPath
                functions.WriteValue("System", "ComPortIn", "COM3", path)
                functions.WriteValue("System", "BaudRateIn", "4800", path)
                functions.WriteValue("System", "ComPortOut", "COM4", path)
                functions.WriteValue("System", "BaudRateOut", "4800", path)
            End If

        Catch ex As Exception
        End Try

    End Sub


    'Updates the DataGridView Table
    Public Async Sub LoadLogsToDataGrid()

        Try
            DataGridView1.Rows.Clear()

            ' Read all lines from the file asynchronously
            Dim lines() As String = Await Task.Run(Function() File.ReadAllLines(Variables.csvLogFilePath))

            ' Reverse the lines array to display the latest data first
            Array.Reverse(lines)

            ' Loop through the lines and split each line into columns
            For Each line As String In lines
                Dim columns() As String = line.Split(","c)
                DataGridView1.Rows.Add(columns)
            Next
        Catch ex As Exception
        End Try

    End Sub


#Region "DataTable BackgroundWorker"

    Private Sub bgWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgWorker.DoWork
        Try
            Dim lines() As String = File.ReadAllLines(Variables.csvLogFilePath)
            Array.Reverse(lines)

            For Each line As String In lines
                If bgWorker.CancellationPending Then
                    e.Cancel = True
                    Exit For
                End If

                Dim columns() As String = line.Split(","c)
                bgWorker.ReportProgress(0, columns)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub bgWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgWorker.ProgressChanged
        Dim columns() As String = DirectCast(e.UserState, String())
        DataGridView1.Rows.Add(columns)
    End Sub

    Private Sub LoadLogsToDataGridAsync()
        If Not bgWorker.IsBusy Then

            ' During Load there is an issue with the autosize column, so we set its value here
            If (DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None) Then
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If

            DataGridView1.Rows.Clear() ' Clear existing data
            bgWorker.RunWorkerAsync()
        End If
    End Sub

#End Region

#End Region


#Region "Timer"

    'Timer for updating with any connection made or changes
    Private Sub appStatus_Timer_Tick(sender As Object, e As EventArgs) Handles appStatus_Timer.Tick
        ' Clear the ListBox
        ListBox1.Items.Clear()

        UpdateUIBasedOnPortStatusAsync()

        Dim temp As String = ""
        If allPortsOpen Then
            temp = "All Ports Active"
        Else
            If Not IsRxOpen Then
                temp = $"{receiver_Port.PortName.ToString} is Off"
            End If

            If Not IsTxOpen Then
                temp = $"{transmitter_Port.PortName.ToString} is Off"
            End If

            If Not IsRxOpen AndAlso Not IsTxOpen Then
                temp = "All Ports Off"
            End If

        End If


        ' Add the current date and time to the ListBox
        ListBox1.Items.Add($"{DateTime.Now.ToShortDateString}                        {temp}                         {DateTime.Now.ToShortTimeString}")
        ListBox1.Items.Add("")
        ListBox1.Items.Add($"**Latest Data**")
        ListBox1.Items.Add($"Time : {If(String.IsNullOrEmpty(latestAddedTime), "-", latestAddedTime)}")
        ListBox1.Items.Add($"ID : {If(String.IsNullOrEmpty(LastestAddedID), "-", LastestAddedID)}")
        ListBox1.Items.Add($"Net : {If(String.IsNullOrEmpty(LatestAddedNet), "-", LatestAddedNet)}")
        ListBox1.Items.Add("")
        ListBox1.Items.Add($"Total Scanned : {TotalScanned}")
    End Sub

#End Region


#Region "Admin Only Access"

    'Open Settings Menu
    Private Sub settings_btn_Click(sender As Object, e As EventArgs) Handles settings_btn.Click
        Try
            If Variables.adminAccess AndAlso Variables.stgsFrm IsNot Nothing Then
                Variables.sapu.displaySettingsForm(Variables.adminAccess)
            Else
                If Variables.sapu Is Nothing OrElse Variables.sapu.IsDisposed Then
                    Variables.sapu = New SettingsAccessPopUp(Me) ' Create a new instance if not already open
                    Variables.sapu.Show()
                    Variables.sapu.BringToFront()
                Else
                    If Variables.sapu.WindowState = FormWindowState.Minimized Then
                        Variables.sapu.WindowState = FormWindowState.Normal ' Restore the form if minimized
                    End If
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

#End Region



End Class
