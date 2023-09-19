Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO

Public Class frmAddPartMaster
    Dim PID As Guid
    'Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim connstr = ReadValue("System", "SQLconnstr", IniPath)
    Dim localpath = (ReadValue("System", "localpath", IniPath))
    Dim localdir = ""
    Dim cond As String
    Dim imgstr(100)
    Dim arrsize = 0
    Dim original As Image
    Dim resized As Image
    Dim imgpath As String

    Private Function CheckDirectoryExist()
        Dim specpath = localpath & "\" & PartName.Text & "\"
        If Directory.Exists(specpath) Then
            If Not Text.Contains("Update") Then

                For Each deleteFile In Directory.GetFiles(specpath, "*.*", SearchOption.TopDirectoryOnly)
                    File.Delete(deleteFile)
                Next
            End If
        Else

            Directory.CreateDirectory(specpath)

        End If
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If statuslbl.Text <> "" Then
            Exit Sub
        End If
        If PartName.Text = "" Or PartNo.Text = "" Then
            statuslbl.Text = "Part Name or Part No field missing. Please Try Again."
            Exit Sub
        End If
        CheckDuplicateID()
        If CheckDuplicate() Then
            Exit Sub
        End If

        InsertDataSQL()
        CheckDirectoryExist()
        original = Nothing
        resized = Nothing
        For i = 0 To arrsize - 1
            If System.IO.File.Exists(imgstr(i)) = True Then
                Dim newfilename = localpath & "\" & PartName.Text & "\" & IO.Path.GetFileName(imgstr(i))

                System.IO.File.Copy(imgstr(i), newfilename)

            End If
        Next
        Me.Close()
    End Sub

    Private Sub UpdateBtn_Click(sender As Object, e As EventArgs) Handles UpdateBtn.Click
        Dim specpath As String = localpath & "\" & PartName.Text & "\"
        If statuslbl.Text <> "" Then
            Exit Sub
        End If
        If PartName.Text = "" Or PartNo.Text = "" Then
            statuslbl.Text = "Part Name or Part No field missing. Please Try Again."
            Exit Sub
        End If
        CheckDuplicateID()
        If CheckDuplicate() Then
            Exit Sub
        End If

        UpdateDataSQL()
        CheckDirectoryExist()
        Dim di As New DirectoryInfo(specpath)
        Dim fiArr As FileInfo() = di.GetFiles()
        For Each file In fiArr
            For i = 0 To arrsize - 1
                If file.FullName = imgstr(i) Then
                    GoTo skip
                End If
            Next
            System.IO.File.Delete(file.FullName)
skip:
        Next

        For i = 0 To arrsize - 1
            For Each file In fiArr
                If imgstr(i) = file.FullName Then
                    GoTo here
                End If
            Next
            Dim newfilename = specpath & IO.Path.GetFileName(imgstr(i))
            System.IO.File.Copy(imgstr(i), newfilename)
here:
        Next
        Me.Close()
    End Sub

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        Me.Close()
    End Sub

    Private Function InsertDataSQL()
        Dim res = MessageBox.Show("Comfirm Save New Part Info?" & vbCrLf & "Part No:" & PartNo.Text & vbCrLf & "Part Name:" & PartName.Text, "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "INSERT INTO  [CRICUT].[CUPID].[PartMaster]
                                          ( [Part ID]
                                           ,[Part No]
                                           ,[Part Name]
                                           ,[Description]
                                           ,[Modified Date]
                                           ,[Delete])
                                     VALUES
                                               ('" & PID.ToString & "'
                                               ,'" & PartNo.Text & "'
                                               ,'" & PartName.Text & "'
                                               ,'" & Desc.Text & "'
                                               ,  ' " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                                ,'False')"


                SQLcmd.ExecuteNonQuery()
                Conn.Close()
            End If
        Else
            Exit Function
        End If
    End Function

    Private Function UpdateDataSQL()
        Dim res = MessageBox.Show("Comfirm Update Part Info?" & vbCrLf & "Part No:" & PartNo.Text & vbCrLf & "Part Name:" & PartName.Text, "Comfirm Operation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If res = DialogResult.Yes Then
            Dim Conn = New SqlConnection(connstr)
            Conn.Open()
            If Conn.State = ConnectionState.Open Then
                Dim SQLcmd = New SqlCommand
                SQLcmd.Connection = Conn
                SQLcmd.CommandText = "UPDATE  [CRICUT].[CUPID].[PartMaster]
                                             SET [Part No]='" & PartNo.Text & "',
                                                 [Part Name]='" & PartName.Text & "',
                                              [Modified Date] = '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'
                                     WHERE [Index] = '" & frmPartMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "'"
                SQLcmd.ExecuteNonQuery()
                Conn.Close()
            End If
        Else
            Exit Function
        End If
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles PartNo.TextChanged, PartName.TextChanged
        statuslbl.Text = ""
        If CheckDuplicate() Then
            statuslbl.Text = "Duplicate Part found"
        End If
    End Sub

    Private Function CheckDuplicate() As Boolean
        Dim condi
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        If Text.Contains("Update") Then
            condi = "NOT [Index]='" & frmPartMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "' AND"
        End If
        SQLcmd.CommandText = "Select [Index] As [No],
                                [Part No] As [Part No],
                                [Part Name] As [Part Name],
                                [Modified Date] As [Modified Date]
                      FROM [CRICUT].[CUPID].[PartMaster] WHERE " & condi & " [Delete]='False'"
        SQLcmd.Connection = Conn
        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            While ds.Read
                If ds.Item("Part No") = PartNo.Text Then
                    statuslbl.Text = "Duplicate Part No."
                End If
                If ds.Item("Part Name") = PartName.Text Then
                    statuslbl.Text = "Duplicate Part Name"
                End If
            End While
        End If
        Conn.Close()
    End Function

    Private Sub frmAddPartMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Items.Clear()
        Array.Clear(imgstr, 0, 100)

        PID = Guid.NewGuid
        PartNo.Text = ""
        PartName.Text = ""
        If Text.Contains("Update") Then
            GroupBox1.Text = "Update Parts"
            UpdateBtn.Visible = True
            UpdateBtn.Location = SaveBtn.Location
            SaveBtn.Visible = False
            LoadDataSQL()
            If Not Directory.Exists(localpath & "\" & PartName.Text & "\") Then
                My.Computer.FileSystem.CreateDirectory(localpath & PartName.Text & "\")
                Exit Sub
            End If
            Dim di As New DirectoryInfo(localpath & "\" & PartName.Text & "\")
            Dim fiArr As FileInfo() = di.GetFiles()
            ' Display the names of the files.
            Dim fri As FileInfo
            Dim ptr As Integer = 0
            For Each fri In fiArr
                ListBox1.Items.Add(fri.Name)
                imgstr(ptr) = fri.FullName
                ptr += 1
                arrsize += 1
            Next

        Else
            GroupBox1.Text = "Add New Parts"
            UpdateBtn.Visible = False
            SaveBtn.Visible = True
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        If PictureBox1.Image IsNot Nothing Then
            Process.Start(imgpath)
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "jpeg files (*.jpg)/png files (*.png)|*.jpg|png files (*.png)|*.png|All files (*.*)|*.*"
        OpenFileDialog1.Multiselect = True
starthere:
        Dim Result As DialogResult = OpenFileDialog1.ShowDialog()
        If Result = DialogResult.Abort Or Result = DialogResult.Cancel Then
            Exit Sub
        End If
        If OpenFileDialog1.FileNames.Count > 100 Then
            MsgBox("Please open not more than 100 files", MsgBoxStyle.Exclamation)
            GoTo starthere
        End If
        Dim fname As String = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
        For Each img In OpenFileDialog1.FileNames  'Read all image file from file dialog
            Dim filename = IO.Path.GetFileName(img)
            For Each item In ListBox1.Items 'Read all items in the ListBox 
                If filename = item Then 'Compare for similar file name
                    Dim msgres = MsgBox("There's already existing file with similar name """ & filename & """ , do you want to replace it?", MsgBoxStyle.YesNoCancel, MsgBoxStyle.Critical)
                    If msgres = DialogResult.Yes Then
                        For i = 0 To arrsize - 1 'From first array index to the last of the array index (arrsize-1)
                            If IO.Path.GetFileName(imgstr(i)) = filename Then
                                imgstr(i) = img
                                GoTo endhere
                            End If
                        Next
                    ElseIf msgres = DialogResult.No Then
                        GoTo starthere
                    Else
                        Exit Sub
                    End If
                End If
            Next
            ListBox1.Items.Add(filename)
            imgstr(arrsize) = img
            arrsize += 1
endhere:
        Next
        original = GetCopyImage(imgstr(0))
        resized = ResizeImage(original, New Size(PictureBox1.Width, PictureBox1.Height))
        PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
        PictureBox1.Image = resized
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim pointer = 0
        Dim delloc = 0

        If ListBox1.SelectedItem = Nothing Then
            MessageBox.Show("Please select an item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            For Each file In imgstr

                If IO.Path.GetFileName(file) = ListBox1.SelectedItem.ToString Then
                    delloc = pointer
                End If
                pointer += 1
            Next
            ' arrsize -= 1
            For i = 1 To arrsize - 1
                If i - 1 >= delloc Then
                    imgstr(i - 1) = imgstr(i)
                End If
            Next

            ListBox1.Items.Remove(ListBox1.SelectedItem)
            arrsize -= 1
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        displayimg()
    End Sub

    Public Function GetCopyImage(path As String) As Image
        Dim bm As Bitmap = Nothing
        Using im As Image = Image.FromFile(path)
            bm = New Bitmap(im)
        End Using

        Return bm
    End Function

    Public Shared Function ResizeImage(ByVal image As Image, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        If preserveAspectRatio Then
            Dim originalWidth As Integer = image.Width
            Dim originalHeight As Integer = image.Height
            Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth, percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = size.Width
            newHeight = size.Height
        End If

        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

    Private Function displayimg()
        PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
        PictureBox1.Image = Nothing
        Dim fpath As String


        Try
            For i = 0 To arrsize - 1
                If IO.Path.GetFileName(imgstr(i)) = ListBox1.SelectedItem Then
                    fpath = imgstr(i)
                    imgpath = fpath
                End If
            Next

            original = GetCopyImage(fpath)
            resized = ResizeImage(original, New Size(PictureBox1.Width, PictureBox1.Height))
            PictureBox1.Image = resized
        Catch ex As Exception
            PictureBox1.Image = Nothing
        End Try
    End Function


    Private Function CheckDuplicateID()
startingplace:
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select *
                          FROM [CRICUT].[CUPID].[PartMaster] WHERE [Part ID] = '" & PID.ToString & "'AND [Delete]='False'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            PID = Guid.NewGuid
            Conn.Close()
            GoTo startingplace
        End If
        Conn.Close()

    End Function

    Private Function LoadDataSQL()
        Dim Conn = New SqlConnection(connstr)
        Conn.Open()
        Dim SQLcmd = New SqlCommand
        SQLcmd.Connection = Conn
        SQLcmd.CommandText = "Select 
                                  [Part ID]
                                  ,[Part No]
                                  ,[Part Name]
                                  ,[Description]
                                  ,[Modified Date]
                            
                           FROM [CRICUT].[CUPID].[PartMaster]
                        WHERE [Index] = '" & frmPartMaster.DataGridView1.CurrentRow.Cells("No").Value.ToString & "'AND [Delete]='False'"

        Dim ds = SQLcmd.ExecuteReader
        If ds.HasRows Then
            While ds.Read
                PID = ds.Item("Part ID")
                PartNo.Text = ds.Item("Part No")
                PartName.Text = ds.Item("Part Name")
                Desc.Text = ds.Item("Description")


            End While
        End If
        Conn.Close()
    End Function

    Private Sub ToolStrip2_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip2.ItemClicked

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim f1 = New frmHelp
        If Text.Contains("Update") Then
            f1.helptitle = "updatepart"

        Else
            f1.helptitle = "addpart"

        End If
        f1.Owner = Me
        f1.ShowDialog()
    End Sub

    Private Sub PartNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PartNo.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            PartName.Select()

        End If
    End Sub

End Class