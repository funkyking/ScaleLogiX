
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Data.SqlClient
Module param
    Public connstr = ReadValue("System", "SQLconnstr", IniPath)
    Public SystemIniPath = My.Application.Info.DirectoryPath.ToString & "\System.ini"
    Public IniPath = My.Application.Info.DirectoryPath.ToString & "\system.ini"
    'Public localpath As String = ConfigurationManager.AppSettings("localpath")
    'Public connstr As String = System.Configuration.ConfigurationManager.ConnectionStrings("Default").ConnectionString

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
    Public Function ReadValue(ByVal section As String, ByVal key As String, ByVal path As String) As String
        Dim sb As New System.Text.StringBuilder(255)
        Dim i = GetPrivateProfileString(section, key, "", sb, 255, path)
        Return sb.ToString()
    End Function


    ''' <summary>
    ''' Write value to INI file
    ''' </summary>
    ''' <param name="section">The section of the file to write in</param>
    ''' <param name="key">The key in the section to write</param>
    ''' <param name="value">The value to write for the key</param>
    Public Sub WriteValue(ByVal section As String, ByVal key As String, ByVal value As String, ByVal path As String)
        WritePrivateProfileString(section, key, value, path)
    End Sub

    Public Function Decrypt(ByVal cipherText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function


End Module
