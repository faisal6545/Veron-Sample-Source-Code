' Developer: S.M.A.Faisal

Public Class Form4
    Dim a1 As String = "0"
    Dim a2 As String = "0"
    Dim a3 As String = "0"
    Dim a4 As String = "0"
    Dim a5 As String = "0"
    Dim a6 As String = "0"
    Dim a7 As String = "0"
    Dim a8 As String = "0"
    Dim a9 As String = "0"
    Dim a10 As String = "0"
    Dim a12 As String = "0"
    Dim a13 As String = "0"
    Dim a14 As String = "0"
    Dim a15 As String = "1"
    Dim rati As String = ""
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label18.Text = TrackBar12.Value
        Label17.Text = TrackBar1.Value
        Label22.Text = TrackBar3.Value
        Label21.Text = TrackBar2.Value
        Label20.Text = TrackBar7.Value
        Label19.Text = TrackBar6.Value
        Label28.Text = TrackBar5.Value
        Label27.Text = TrackBar4.Value
        Label26.Text = TrackBar9.Value
        Label25.Text = TrackBar8.Value
        Label29.Text = a12
        Label24.Text = a15
        Label23.Text = a13
        Label31.Text = a14
        a1 = TrackBar12.Value
        a2 = TrackBar1.Value
        a3 = TrackBar3.Value
        a4 = TrackBar2.Value
        a5 = TrackBar7.Value
        a6 = TrackBar6.Value
        a7 = TrackBar5.Value
        a8 = TrackBar4.Value
        a9 = TrackBar9.Value
        a10 = TrackBar8.Value
    End Sub
    Private Sub TrackBar12_Scroll(sender As Object, e As EventArgs) Handles TrackBar9.Scroll, TrackBar8.Scroll, TrackBar7.Scroll, TrackBar6.Scroll, TrackBar5.Scroll, TrackBar4.Scroll, TrackBar3.Scroll, TrackBar2.Scroll, TrackBar12.Scroll, TrackBar1.Scroll
        Timer2.Stop()
        Timer2.Start()
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Stop()
        Form1.SendCommand("af_eq_set_bands " & a1 & ":" & a2 & ":" & a3 & ":" & a4 & ":" & a5 & ":" & a6 & ":" & a7 & ":" & a8 & ":" & a9 & ":" & a10)
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        cmb()
    End Sub
    Private Sub cmb()
        Form1.SendCommand("af_del karaoke,extrastereo")
        If ComboBox2.SelectedIndex = 1 Then
            Form1.SendCommand("af_add extrastereo=0")
        ElseIf ComboBox2.SelectedIndex = 2 Then
            Form1.SendCommand("af_add karaoke")
        ElseIf ComboBox2.SelectedIndex = 3 Then
            Form1.SendCommand("af_add extrastereo")
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form1.SendCommand("brightness -10")
        If a12 <= -100 Then
        Else
            a12 = a12 - 10
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Form1.SendCommand("brightness 10")
        If a12 >= 100 Then
        Else
            a12 = a12 + 10
        End If
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Form1.SendCommand("contrast -10")
        If a13 <= -100 Then
        Else
            a13 = a13 - 10
        End If
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form1.SendCommand("contrast 10")
        If a13 >= 100 Then
        Else
            a13 = a13 + 10
        End If
    End Sub
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Form1.SendCommand("saturation -10")
        If a15 = 0 Then
        Else
            a15 = a15 - 0.1
        End If
    End Sub
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Form1.SendCommand("saturation 10")
        If a15 >= 2 Then
        Else
            a15 = a15 + 0.1
        End If
    End Sub
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Form1.SendCommand("hue -10")
        If a14 <= -100 Then
        Else
            a14 = a14 - 10
        End If
    End Sub
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Form1.SendCommand("hue 10")
        If a14 >= 100 Then
        Else
            a14 = a14 + 10
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            rati = "0"
        ElseIf ComboBox1.SelectedIndex = 1 Then
            rati = "1.33"
        ElseIf ComboBox1.SelectedIndex = 2 Then
            rati = "1.77"
        ElseIf ComboBox1.SelectedIndex = 3 Then
            rati = "1.85"
        ElseIf ComboBox1.SelectedIndex = 4 Then
            rati = "2.35"
        ElseIf ComboBox1.SelectedIndex = 5 Then
            rati = "2.40"
        ElseIf ComboBox1.SelectedIndex = 6 Then
            rati = "2.75"
        ElseIf ComboBox1.SelectedIndex = 7 Then
            rati = "3"
        ElseIf ComboBox1.SelectedIndex = 8 Then
            rati = "4"
        End If
        Form1.SendCommand("switch_ratio " & rati)
    End Sub
    Public Sub ref()
        a12 = 0
        a15 = 1
        a13 = 0
        a14 = 0
        Form1.SendCommand("af_eq_set_bands " & a1 & ":" & a2 & ":" & a3 & ":" & a4 & ":" & a5 & ":" & a6 & ":" & a7 & ":" & a8 & ":" & a9 & ":" & a10)
        cmb()
        Form1.SendCommand("switch_ratio " & rati)
    End Sub
    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub
End Class