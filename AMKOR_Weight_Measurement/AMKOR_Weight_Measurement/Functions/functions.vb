Imports System.IO

Public Class functions

    'Public Shared IniPath = "C:\GWWeight\Config\system.ini"

    Private Declare Ansi Function GetPrivateProfileString Lib "kernel32.dll" Alias "GetPrivateProfileStringA" _
           (ByVal lpApplicationName As String,
            ByVal lpKeyName As String,
            ByVal lpDefault As String,
            ByVal lpReturnedString As System.Text.StringBuilder,
            ByVal nSize As Integer,
            ByVal lpFileName As String) _
        As Integer

    Private Declare Ansi Function WritePrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringA" _
        (ByVal lpApplicationName As String,
         ByVal lpKeyName As String,
         ByVal lpString As String,
         ByVal lpFileName As String) _
    As Integer
    ''' <summary>
    ''' Read value from INI file
    ''' </summary>
    ''' <param name="section">The section of the file to look in</param>
    ''' <param name="key">The key in the section to look for</param>
    Public Shared Function ReadValue(ByVal section As String, ByVal key As String, ByVal path As String) As String
        Dim sb As New System.Text.StringBuilder(3000)
        Dim i = GetPrivateProfileString(section, key, "", sb, 3000, path)
        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Write value to INI file
    ''' </summary>s
    ''' <param name="section">The section of the file to write in</param>
    ''' <param name="key">The key in the section to write</param>
    ''' <param name="value">The value to write for the key</param>
    Public Shared Sub WriteValue(ByVal section As String, ByVal key As String, ByVal value As String, ByVal path As String)
        CheckFileFolderExistance(Variables.IniPath)
        WritePrivateProfileString(section, key, value, path)
    End Sub

    ' Check if Folder Exist
    Public Shared Sub CheckFileFolderExistance(ByVal filepath As String)
        Dim folderPath As String = Path.GetDirectoryName(filepath)
        Dim fileName As String = Path.GetFileName(filepath)

        ' Check if the folder exists or create it
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
            'MessageBox.Show("Folder created: " & folderPath)
        End If

        ' Check if the file exists or create it
        If Not File.Exists(filepath) Then
            File.Create(filepath).Close() ' Create and immediately close the file
            'MessageBox.Show("File created: " & filepath)
        End If
    End Sub

    ' Load Config File Values to Variables
    Public Shared Sub loadConfig()

        Dim path As String = Variables.IniPath
        Variables.ComPortIn = functions.ReadValue("System", "ComPortIn", path)
        Variables.BaudrateIn = functions.ReadValue("System", "BaudRateIn", path)
        Variables.ComPortOut = functions.ReadValue("System", "ComPortOut", path)
        Variables.BaudrateOut = functions.ReadValue("System", "BaudRateOut", path)
        Variables.DataBits = functions.ReadValue("System", "DataBits", path)
        Variables.Parity = functions.ReadValue("System", "Parity", path)
        Variables.ParityReplace = functions.ReadValue("System", "Parity", path)
        Variables.StopBits = functions.ReadValue("System", "StopBits", path)
        Variables.Handshake = functions.ReadValue("System", "Handshake", path)


    End Sub




End Class
