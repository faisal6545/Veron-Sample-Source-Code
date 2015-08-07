' Developer: S.M.A.Faisal

Imports System.IO
Public Class Form2
    Dim ps As Process = Nothing
    Delegate Sub ChangeTextsSafe(ByVal percent As String)
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BackgroundWorker1.RunWorkerAsync()
        Try
            For Each drive As DriveInfo In DriveInfo.GetDrives().Where(Function(d) d.DriveType = DriveType.CDRom)
                ComboBox1.Items.Add(drive.Name)
            Next
            ComboBox1.SelectedIndex = 0
        Catch
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            ListBox1.Items.Clear()
            Dim dii As New IO.DirectoryInfo(ComboBox1.SelectedItem.ToString)
            Dim diiar1 As IO.FileInfo() = dii.GetFiles()
            Dim dra As IO.FileInfo
            For Each dra In diiar1
                ListBox1.Items.Add(dra.FullName.ToString)
            Next
        Catch
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim safedelegate As New ChangeTextsSafe(AddressOf ChangeTexts)
        Do
            System.Threading.Thread.Sleep(1)
            Try
                Dim percent As String = ps.StandardOutput.ReadLine.ToString.Replace("A:", "")
                Me.Invoke(safedelegate, percent)
            Catch
            End Try
        Loop
    End Sub
    Public Sub ChangeTexts(ByVal percent As String)
        Try
            Dim nu As String = percent
            Dim chq As String = ""
            Dim dq As String = ""
            Dim kj As Long = 0
            Dim min As Integer = 0
            Dim max As Integer = 0
            nu = nu.Replace(" ", "")
            For ix = 1 To Len(nu)
                chq = Mid$(nu, ix, 1)
                If (chq = "(") And kj = 0 Then
                    min = dq
                    dq = ""
                    kj = 1
                ElseIf (chq = "f") And kj = 1 Then
                    dq = ""
                    kj = 2
                ElseIf (chq = "(") And kj = 2 Then
                    max = dq
                    dq = ""
                    Exit For
                Else
                    dq += chq
                End If
            Next
            ProgressBar1.Maximum = max
            ProgressBar1.Value = min
        Catch
        End Try
    End Sub
    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ps.Kill()
        Catch
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ps = New Process()
        ps.StartInfo.ErrorDialog = False
        ps.StartInfo.UseShellExecute = False
        ps.StartInfo.RedirectStandardInput = True
        ps.StartInfo.RedirectStandardOutput = True
        ps.StartInfo.FileName = "mplayer.exe"
        ps.StartInfo.CreateNoWindow = True
        ps.StartInfo.Arguments = "-nofs -noquiet -identify -slave -nomouseinput cdda://" & ListBox1.SelectedIndex + 1 & " -cdrom-device " & ComboBox1.SelectedItem.ToString.Replace("\", "") & " -ao pcm"
        ps.Start()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.stp()
        Form1.cdplay(ListBox1.SelectedIndex + 1, ComboBox1.SelectedItem.ToString.Replace("\", ""))
        Form1.mplay()
    End Sub
End Class