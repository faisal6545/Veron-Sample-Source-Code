' Developer: S.M.A.Faisal

Imports System.IO
Public Class Convert_Media_file
    Dim es1, cmd, filn As String
    Dim proc As Process
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not TextBox1.Text = "" Then
            Button2.Enabled = False
            Button3.Enabled = False
            filn = TextBox1.Text
            BackgroundWorker1.RunWorkerAsync()
        End If
    End Sub
    Private Sub Convert_Media_file_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 2
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Control.CheckForIllegalCrossThreadCalls = False
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        cmd = " -i """ + TextBox1.Text + """ -vol " & ComboBox3.SelectedItem.ToString & " -b:a " & ComboBox2.SelectedItem.ToString & "k -y """ + es1 + """"
        Dim ffmpegOutput As String = ""
        proc = New Process
        startinfo.FileName = Path.Combine(Application.StartupPath, "ffmpeg.exe")
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        proc.StartInfo = startinfo
        proc.Start()
        sr = proc.StandardError
        Do
            ffmpegOutput = sr.ReadLine
            TextBox1.Text = ffmpegOutput
        Loop Until proc.HasExited And ffmpegOutput = Nothing Or ffmpegOutput = ""
        Button2.Enabled = True
        Button3.Enabled = True
        TextBox1.Text = filn
        MsgBox("Done")
        Control.CheckForIllegalCrossThreadCalls = True
        BackgroundWorker1.CancelAsync()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            es1 = ""
            es1 = OpenFileDialog1.FileName.ToString.Replace(OpenFileDialog1.SafeFileName.ToString, "")
            es1 = es1 & "New _ " & Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName) & ".mp3"
            TextBox1.Text = OpenFileDialog1.FileName.ToString
        End If
    End Sub
    Private Sub Convert_Media_file_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            proc.Kill()
        Catch 
        End Try
    End Sub
End Class