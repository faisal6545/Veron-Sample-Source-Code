' Developer: S.M.A.Faisal

Imports System.IO
Imports AForge.Imaging.Filters
Imports ImageMagick
Public Class Image_Pro
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim xxt As System.Drawing.Image
                Using str As Stream = IO.File.OpenRead(OpenFileDialog1.FileName)
                    xxt = System.Drawing.Image.FromStream(str)
                End Using
                PictureBox1.Image = xxt
            End If
        Catch
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim filter As Grayscale = New Grayscale(0.2125, 0.7154, 0.0721)
            PictureBox1.Image = filter.Apply(PictureBox1.Image)
        Catch
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim filter As Sepia = New Sepia
            PictureBox1.Image = filter.Apply(PictureBox1.Image)
        Catch
        End Try
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim gm As Bitmap = New Bitmap(PictureBox1.Image)
            Dim imx As MagickImage = New MagickImage(gm)
            imx.Swirl(70)
            PictureBox1.Image = imx.ToBitmap
            gm.Dispose()
        Catch
        End Try
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim gm As Bitmap = New Bitmap(PictureBox1.Image)
            Dim imx As MagickImage = New MagickImage(gm)
            imx.RotationalBlur(10)
            PictureBox1.Image = imx.ToBitmap
            gm.Dispose()
        Catch
        End Try
    End Sub
End Class