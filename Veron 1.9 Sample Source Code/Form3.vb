' Developer: S.M.A.Faisal

Public Class Form3
    Dim posh As Long
    Dim ps As Process = Nothing
    Delegate Sub ChangeTextsSafe(ByVal percent As String)
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ps = New Process()
        ps.StartInfo.ErrorDialog = False
        ps.StartInfo.UseShellExecute = False
        ps.StartInfo.RedirectStandardInput = True
        ps.StartInfo.RedirectStandardOutput = True
        ps.StartInfo.FileName = "mplayer.exe"
        ps.StartInfo.CreateNoWindow = True
        BackgroundWorker1.RunWorkerAsync()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Pause" Then
            Button1.Text = "Start"
            posh = 0
        Else
            Button1.Text = "Pause"
            posh = 1
        End If
        SendCommand("pause")
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim safedelegate As New ChangeTextsSafe(AddressOf ChangeTexts)
        Do
            System.Threading.Thread.Sleep(1)
            Try
                If posh = 1 Then
                    Dim percent As String = ps.StandardOutput.ReadLine.ToString.Replace("ICY Info: ", "")
                    Me.Invoke(safedelegate, percent)
                End If
            Catch
            End Try
        Loop
    End Sub
    Public Sub ChangeTexts(ByVal percent As String)
        Try
            If posh = 1 Then
                If percent.Contains("StreamTitle=") Then
                    TextBox1.Text = vbNewLine & percent.Replace("'", "").Replace("=", " = ").Replace("StreamUrl = ", "").Replace(";", "")
                End If
            End If
        Catch
        End Try
    End Sub
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        SendCommand("set_property volume " & TrackBar1.Value)
    End Sub
    Public Sub SendCommand(ByVal cmd As String)
        Try
            If ps IsNot Nothing AndAlso ps.HasExited = False Then
                ps.StandardInput.Write(cmd & vbLf)
            End If
        Catch
        End Try
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            posh = 0
            Try
                ps.Kill()
            Catch
            End Try
            Button1.Text = "Pause"
            TextBox1.Text = vbNewLine & "Please Wait ......"
            ps.StartInfo.Arguments = "-nofs -noquiet -identify -slave -volume " & TrackBar1.Value & " -nomouseinput -sub-fuzziness 1 " _
                & TextBox2.Text & " -capture"
            ps.Start()
            posh = 1
            Button4.Enabled = False
        Catch
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        posh = 0
        Try
            ps.Kill()
        Catch
        End Try
        Button1.Text = "Pause"
        Button3.Text = "Record"
        TextBox1.Text = ""
        Button4.Enabled = True
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If Button3.Text = "Record" Then
                Try
                    System.IO.File.Delete(Application.StartupPath & "\stream.dump")
                Catch
                End Try
                SendCommand("capturing")
                Button3.Text = "Stop"
            Else
                SendCommand("capturing 0")
                Button3.Text = "Record"
            End If
        Catch
        End Try
    End Sub
    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ps.Kill()
        Catch
        End Try
    End Sub
End Class