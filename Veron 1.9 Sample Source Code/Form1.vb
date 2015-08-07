' Developer: S.M.A.Faisal

Public Class Form1
    Delegate Sub ChangeTextsSafe(ByVal percent As String)
    Dim args, time_length, time_pos, buffer, CDP As String
    Dim posh, tdr As Long
    Dim ps As Process = Nothing
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form4.Hide()
        ps = New Process()
        ps.StartInfo.ErrorDialog = False
        ps.StartInfo.UseShellExecute = False
        ps.StartInfo.RedirectStandardInput = True
        ps.StartInfo.RedirectStandardOutput = True
        ps.StartInfo.FileName = "mplayer.exe"
        ps.StartInfo.CreateNoWindow = True
        BackgroundWorker1.RunWorkerAsync()
    End Sub
    Public Sub ChangeTexts(ByVal per As String)
        Try
            If posh = 1 Then
                If per.Contains("ANS_LENGTH") Then
                    ConvertTimeHHMMSS(per.Replace("ANS_LENGTH=", ""), 1)
                Else
                    ConvertTimeHHMMSS(per.Replace("ANS_TIME_POSITION=", ""), 0)
                End If
            End If
        Catch
        End Try
    End Sub
    Public Sub ConvertTimeHHMMSS(ByVal timeInSeconds As Double, ByVal strx As Long)
        Try
            If timeInSeconds >= 0 Then
                Dim iSecond As Double = timeInSeconds
                Dim iSpan As TimeSpan = TimeSpan.FromSeconds(iSecond)
                If strx = 1 Then
                    time_length = iSpan.Hours.ToString.PadLeft(2, "0"c) & ":" & iSpan.Minutes.ToString.PadLeft(2, "0"c) & ":" & iSpan.Seconds.ToString.PadLeft(2, "0"c)
                    ProgressBar1.Maximum = timeInSeconds
                    tdr = 0
                Else
                    time_pos = iSpan.Hours.ToString.PadLeft(2, "0"c) & ":" & iSpan.Minutes.ToString.PadLeft(2, "0"c) & ":" & iSpan.Seconds.ToString.PadLeft(2, "0"c)
                    ProgressBar1.Value = timeInSeconds
                End If
                Label1.Text = time_pos
                Label2.Text = time_length
            End If
        Catch
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim safedelegate As New ChangeTextsSafe(AddressOf ChangeTexts)
        Do
            System.Threading.Thread.Sleep(1)
            Try
                If posh = 1 Then
                    Dim sLine As String = ps.StandardOutput.ReadLine
                    If sLine.Contains("ANS_LENGTH") Or sLine.Contains("ANS_TIME_POSITION") Then
                        Me.Invoke(safedelegate, sLine)
                    End If
                End If
            Catch
            End Try
        Loop
    End Sub
    Public Sub cdplay(ByVal r1 As String, ByVal r2 As String)
        CDP = " cdda://" & r1 & " -cdrom-device " & r2
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            mplay()
        End If
    End Sub
    Public Sub mplay()
        stp()
        args = "-nofs -noquiet -identify -slave -af-add equalizer=0:0:0:0:0:0:0:0:0:0 -volume " & TrackBar1.Value & " -nomouseinput -sub-fuzziness 1 -vo direct3d, -ao dsound -osdlevel 0 -wid " & CInt(Panel1.Handle.ToInt32) & CDP
        ps.StartInfo.Arguments = args & " """ & OpenFileDialog1.FileName & """"
        ps.Start()
        CDP = ""
        posh = 1
        tdr = 1
        Timer1.Start()
        Timer2.Start()
    End Sub
    Public Sub stp()
        posh = 0
        Try
            ps.Kill()
        Catch
        End Try
        time_length = ""
        time_pos = ""
        Button1.Text = "Pause"
        Timer1.Stop()
        ProgressBar1.Value = 0
        Label1.Text = "00:00:00"
        Label2.Text = "00:00:00"
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not Label1.Text = "00:00:00" Then
            If Button1.Text = "Pause" Then
                posh = 0
                Button1.Text = "Play"
                Timer1.Stop()
                SendCommand("pause")
            Else
                posh = 1
                Button1.Text = "Pause"
                SendCommand("pause")
                Timer1.Start()
            End If
        End If
    End Sub
    Public Sub SendCommand(ByVal cmd As String)
        Try
            ' You can send any command to mplayer [ http://www.mplayerhq.hu/DOCS/tech/slave.txt ]
            If ps IsNot Nothing AndAlso ps.HasExited = False Then
                ps.StandardInput.Write(cmd & vbLf)
            End If
        Catch
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        stp()
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ps.Kill()
        Catch
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If tdr = 1 Then
            SendCommand("get_time_length")
        Else
            SendCommand("get_time_pos")
        End If
    End Sub
    Private Sub ProgressBar1_MouseEnter(sender As Object, e As EventArgs) Handles ProgressBar1.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub ProgressBar1_MouseLeave(sender As Object, e As EventArgs) Handles ProgressBar1.MouseLeave
        Cursor = Cursors.Default
    End Sub
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        If Button1.Text = "Pause" Then
            SendCommand("set_property volume " & TrackBar1.Value)
        End If
    End Sub
    Private Sub ProgressBar1_MouseDown(sender As Object, e As MouseEventArgs) Handles ProgressBar1.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Left And Button1.Text = "Pause" Then
                Dim dValue As Double
                dValue = (Convert.ToDouble(e.X) / Convert.ToDouble(ProgressBar1.Width)) * (ProgressBar1.Maximum - ProgressBar1.Minimum)
                ProgressBar1.Value = Convert.ToInt32(dValue)
                SendCommand("seek " & ProgressBar1.Value & " 2")
            End If
        Catch
        End Try
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form2.Show()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form3.Show()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form4.Show()
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Stop()
        Form4.ref()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        SendCommand("screenshot 0")
    End Sub
    Private Sub ConvertCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertCodeToolStripMenuItem.Click
        Convert_Code.Show()
    End Sub
    Private Sub ImageProcessingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImageProcessingToolStripMenuItem.Click
        Image_Pro.Show()
    End Sub
    Private Sub ConvertMediaFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertMediaFileToolStripMenuItem.Click
        Convert_Media_file.Show()
    End Sub
    Private Sub SystemInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SystemInformationToolStripMenuItem.Click
        System_Information.Show()
    End Sub
    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip1.Show(MousePosition)
        End If
    End Sub
End Class