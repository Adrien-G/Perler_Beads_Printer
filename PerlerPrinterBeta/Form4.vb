Imports System.IO

Public Class Form4

    Dim stream As Stream
    Dim bmp As Bitmap
    Dim convertedToPerlFormat = 0
    Dim nbPerlesColorPalette = 30
    Dim colorPalette(nbPerlesColorPalette) As Color
    Dim nbPerles(nbPerlesColorPalette) As Integer
    Dim incremConverter = 0

    Function GetColor(ByVal color As Color)
        For i = 0 To nbPerlesColorPalette
            If (colorPalette(i).R = color.R And colorPalette(i).G = color.G And colorPalette(i).B = color.B) Then
                Return i
            End If
        Next
        Return -1
    End Function

    Function rechercherCouleur(ByVal color As Color)
        Dim nbrDiff = 1000
        Dim nbrTemp = 0
        Dim indexColor = 0
        For i = 0 To nbPerlesColorPalette
            Select Case ComboBox1.SelectedIndex
                Case 0
                    nbrTemp += Math.Abs(CInt(colorPalette(i).R) - CInt(color.R))
                    nbrTemp += Math.Abs(CInt(colorPalette(i).G) - CInt(color.G))
                    nbrTemp += Math.Abs(CInt(colorPalette(i).B) - CInt(color.B))
                Case 1
                    nbrTemp += Math.Abs(CInt(colorPalette(i).GetHue) - CInt(color.GetHue))
                    nbrTemp += Math.Abs(CInt(colorPalette(i).GetSaturation) - CInt(color.GetSaturation))
                    nbrTemp += Math.Abs(CInt(colorPalette(i).GetBrightness) - CInt(color.GetBrightness))
                Case 2
                    nbrTemp += Math.Abs(CInt(colorPalette(i).R) - CInt(color.R))
                    nbrTemp += Math.Abs(CInt(colorPalette(i).G) - CInt(color.G))
                    nbrTemp += Math.Abs(CInt(colorPalette(i).B) - CInt(color.B))
                    nbrTemp += Math.Abs(CInt(colorPalette(i).GetHue) - CInt(color.GetHue))
                    nbrTemp += Math.Abs(CInt(colorPalette(i).GetSaturation) - CInt(color.GetSaturation))
                    nbrTemp += Math.Abs(CInt(colorPalette(i).GetBrightness) - CInt(color.GetBrightness))
            End Select
            If (nbrTemp < nbrDiff) Then
                nbrDiff = nbrTemp
                indexColor = i
                If (nbrDiff = 0) Then
                    Exit For
                End If
            End If
            nbrTemp = 0
        Next
        nbPerles(indexColor) += 1
        Return indexColor
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Try
                stream = OpenFileDialog1.OpenFile()
                If (stream IsNot Nothing) Then
                    bmp = Image.FromStream(stream)
                End If
            Catch Ex As Exception
                MessageBox.Show("Impossible de lire le fichier :(" & vbCrLf & Ex.Message)
            Finally
                If (stream IsNot Nothing) Then
                    stream.Close()
                End If
            End Try
            PictureBox1.Image = bmp
            Button6.Enabled = False
            GroupBox1.Enabled = True
            GroupBox3.Enabled = True
            GroupBox4.Enabled = True
            PictureBox1.BackColor = SystemColors.Control
        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim destBitmap As New Bitmap(bmp, 29, 29)
        PictureBox1.Image = destBitmap
        bmp = destBitmap
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        SaveFileDialog1.Filter = "Bitmap Image|*.bmp"
        SaveFileDialog1.Title = "enregistrer sous..."
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName <> "" Then
            Dim fs As System.IO.FileStream = CType(SaveFileDialog1.OpenFile(), System.IO.FileStream)
            bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp)
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If (Button4.Text = "Arrêter") Then
            Timer1.Stop()
            Button4.Text = "Format perles"
            ComboBox1.Visible = True
            ProgressBar1.Visible = False
        Else
            Button4.Text = "Arrêter"
            ProgressBar1.Value = 0
            ComboBox1.Visible = False
            ProgressBar1.Visible = True

            Dim colorBmp As Bitmap = Image.FromFile("NuancierPerles.bmp")
            For k = 0 To nbPerlesColorPalette - 1
                colorPalette(k) = colorBmp.GetPixel(k, 0)
            Next
            For i = 0 To nbPerles.Length - 1
                nbPerles(i) = 0
            Next
            ProgressBar1.Maximum = bmp.Height + 1
            incremConverter = 0
            Timer1.Start()
        End If
        Button6.Enabled = True
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        SaveFileDialog1.Filter = "Prl File|*.prl"
        SaveFileDialog1.Title = "Enregistrer l'image modifié au format prl"
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName <> "" Then
            Dim fs As System.IO.FileStream = CType(SaveFileDialog1.OpenFile(), System.IO.FileStream)
            Dim myStream As StreamWriter = New StreamWriter(fs)
            Try
                myStream.WriteLine(bmp.Width & ";" & bmp.Height)
                For y = 0 To bmp.Height - 1
                    For x = 0 To bmp.Width - 1
                        myStream.Write(GetColor(bmp.GetPixel(x, y)) & ";")
                    Next
                    myStream.WriteLine("")
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                myStream.Close()
            End Try
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        For x = 0 To bmp.Width - 1
            If (incremConverter > bmp.Height - 1) Then
                Timer1.Stop()
                Button4.Text = "Format perles"
                ComboBox1.Visible = True
                ProgressBar1.Visible = False
            Else
                bmp.SetPixel(x, incremConverter, colorPalette(rechercherCouleur(bmp.GetPixel(x, incremConverter))))
            End If
        Next
        incremConverter += 1
        ProgressBar1.Value = incremConverter
        PictureBox1.Image = bmp
    End Sub

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SaveFileDialog1.Filter = "Prl File|*.prl"
        SaveFileDialog1.Title = "Enregistrer l'image modifié au format prl"
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName <> "" Then
            Dim fs As System.IO.FileStream = CType(SaveFileDialog1.OpenFile(), System.IO.FileStream)
            Dim myStream As StreamWriter = New StreamWriter(fs)
            Try
                myStream.WriteLine(bmp.Width & ";" & bmp.Height)
                For y = 0 To bmp.Height - 1
                    For x = 0 To bmp.Width - 1
                        myStream.Write(GetColor(bmp.GetPixel(x, y)) & ";")
                    Next
                    myStream.WriteLine("")
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                myStream.Close()
            End Try
        End If
    End Sub
End Class