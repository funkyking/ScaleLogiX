Imports System.IO

Public Class Variables

    Public Shared adminAccess As Boolean = False
    Public Shared sapu As SettingsAccessPopUp
    Public Shared stgsFrm As Settings

    'Baudrate And COM
    Public Shared ComPortIn = ""
    Public Shared BaudrateIn = ""
    Public Shared ComPortOut = ""
    Public Shared BaudrateOut = ""
    Public Shared DataBits = ""
    Public Shared Parity = ""
    Public Shared ParityReplace = ""
    Public Shared StopBits = ""
    Public Shared Handshake = ""


    'Directories
    Public Shared currentDate As String = System.DateTime.Now.ToString("yyyyMMdd")

    'Config File Path
    Public Shared IniPath As String = "C:\GWWeight\Config\system.ini"

    'Main Directory and Weight
    Public Shared MaindirectoryPath As String = "C:\GWWeight"
    Public Shared WeightFilePath As String = Path.Combine(MaindirectoryPath, "Weight_" & currentDate & ".txt")

    'Normal Format Log
    Public Shared logDirectoryPath As String = "C:\GWWeight\Logs"
    Public Shared logfilePath As String = Path.Combine(logDirectoryPath, "Weight_" & currentDate & "_Log" + ".txt")

    'Csv Format Log
    Public Shared csvLogDirectoryPath As String = "C:\GWWeight\Logs\csv"
    Public Shared csvLogFilePath As String = Path.Combine(csvLogDirectoryPath, "Weight_" & currentDate & "_csv_Log" + ".txt")

End Class
