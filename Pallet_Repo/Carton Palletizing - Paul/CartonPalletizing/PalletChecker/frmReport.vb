Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports System.Drawing.Printing

Public Class frmReport
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Public r1 As ReportData
    Private Sub frmReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim rd = New ReportData
        rd = r1
        Dim newPageSettings As New System.Drawing.Printing.PageSettings
        newPageSettings.Margins = New System.Drawing.Printing.Margins(20, 20, 20, 20)
        newPageSettings.Landscape = True

        Label1.Text = "Order Form"
        Dim param(11) As ReportParameter
        param(0) = New ReportParameter("PalletNo", r1.PalletNo)
                param(1) = New ReportParameter("PartNo", r1.PartNo)
                param(2) = New ReportParameter("WorkOrder", r1.WorkOrder)
                param(3) = New ReportParameter("ProdDate", DateTime.Now.ToString)
                param(4) = New ReportParameter("Model", r1.Model)
                param(5) = New ReportParameter("qty", r1.qty)

        param(6) = New ReportParameter("startcarton", r1.startcarton)
        'param(6) = New ReportParameter("startcarton", "1")
        param(7) = New ReportParameter("endcarton", r1.endcarton)
        param(8) = New ReportParameter("Line", r1.Line)
        param(9) = New ReportParameter("totalcarton", r1.totalcarton)
        param(10) = New ReportParameter("Shift", r1.Shift)
        param(11) = New ReportParameter("Desc", r1.Desc)
        newPageSettings.Landscape = True
                ReportViewer1.SetPageSettings(newPageSettings)
        ReportViewer1.LocalReport.ReportEmbeddedResource = "CartonPalletizing.reportCUPID.rdlc"
        'ReportViewer1.LocalReport.ReportEmbeddedResource = "C:\Users\Cricut\Documents\CartonPalletizingNew\CartonPalletizing\PalletChecker\reportCUPID.rdlc"
        'ReportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local
        'ReportViewer1.LocalReport.ReportPath = "CartonPalletizing.reportCUPID.rdlc"
        ReportViewer1.LocalReport.SetParameters(param)
        Me.ReportViewer1.RefreshReport()

                Me.ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth

        'End If
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim f1 = New frmHelp
        If Text.Contains("Report") Then
            f1.helptitle = "report"

        Else
            f1.helptitle = "order"

        End If
        f1.Owner = Me
        f1.ShowDialog()
    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub ReportViewer1_Load(sender As Object, e As EventArgs) Handles ReportViewer1.Load

    End Sub
End Class

Public Class ReportData
    Public WID As Guid
    Public PalletNo As Integer
    Public PartNo As String
    Public WorkOrder As String
    Public ProdDate As DateTime
    Public Shift As String
    Public Model As String
    Public qty As Integer
    Public startserial As String
    Public endserial As String
    Public startcarton As String
    Public endcarton As String
    Public Address As String
    Public CountryCode As String
    Public Line As String
    Public totalcarton As Integer
    Public company As String
    Public Desc As String
    Public Description As String
    Public SubGroup As String
End Class