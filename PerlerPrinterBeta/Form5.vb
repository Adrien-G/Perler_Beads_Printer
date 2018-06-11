Imports System.IO

Public Class Form5

    Dim paintDown = 0
    Dim bmpToSave As Bitmap
    Dim brushColor
    Dim brushColor2
    Dim colorOfBrush As Color
    Dim colorOfBrush2 As Color
    Dim tabNb(29, 29) As Integer
    Dim stream As Stream

    Dim nomPerles As String() = {"Blanc (01)", "Crème (02)", "Jaune (03)", "Orange (04)", "Rouge (05)", "Rose (06)", "Violet (07)", "Bleu (08)", _
   "Bleu foncé (09)", "Vert (10)", "Vert Clair (11)", "Marron (12)", "Gris (17)", "Noir (18)", "Caramel (20)", "Marron clair (21)", "Chair (22)", _
   "Beige (27)", "Vert foncé (28)", "Lit-de-vin (29)", "Bordeaux (30)", "Turquoise (31)", "Jaune pastel (43)", _
   "Rouge pastel (44)", "Violet pastel (45)", "Bleu pastel (46)", "Vert pastel (47)", "Rose pastel (48)", "Marron nounours (60)"}

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If (paintDown = True And e.X >= 0 And e.X < 319 And e.Y >= 0 And e.Y < 319) Then
            Dim modX = Math.Floor(e.X / 11) * 11
            Dim modY = Math.Floor(e.Y / 11) * 11
            Dim rect As New Rectangle(modX, modY, 10, 10)
            If (e.Button = Windows.Forms.MouseButtons.Left) Then
                PictureBox1.CreateGraphics.FillRectangle(brushColor, rect)
                bmpToSave.SetPixel(Math.Floor(e.X / 11), Math.Floor(e.Y / 11), colorOfBrush)
                tabNb(Math.Floor(e.X / 11), Math.Floor(e.Y / 11)) = ComboBox1.SelectedIndex
            ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
                PictureBox1.CreateGraphics.FillRectangle(brushColor2, rect)
                bmpToSave.SetPixel(Math.Floor(e.X / 11), Math.Floor(e.Y / 11), colorOfBrush2)
                tabNb(Math.Floor(e.X / 11), Math.Floor(e.Y / 11)) = ComboBox2.SelectedIndex
            End If
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        paintDown = True
        If (e.X >= 0 And e.X < 319 And e.Y >= 0 And e.Y < 319) Then
            Dim modX = Math.Floor(e.X / 11) * 11
            Dim modY = Math.Floor(e.Y / 11) * 11
            Dim rect As New Rectangle(modX, modY, 10, 10)
            If (e.Button = Windows.Forms.MouseButtons.Left) Then
                PictureBox1.CreateGraphics.FillRectangle(brushColor, rect)
                bmpToSave.SetPixel(Math.Floor(e.X / 11), Math.Floor(e.Y / 11), colorOfBrush)
                tabNb(Math.Floor(e.X / 11), Math.Floor(e.Y / 11)) = ComboBox1.SelectedIndex
            ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
                PictureBox1.CreateGraphics.FillRectangle(brushColor2, rect)
                bmpToSave.SetPixel(Math.Floor(e.X / 11), Math.Floor(e.Y / 11), colorOfBrush2)
                tabNb(Math.Floor(e.X / 11), Math.Floor(e.Y / 11)) = ComboBox1.SelectedIndex
            End If
        End If
    End Sub
    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        paintDown = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Interval = 10000
        For i = 10 To 319 Step 11
            PictureBox1.CreateGraphics.FillRectangle(Brushes.Black, i, 0, 1, 319)
        Next
        For i = 10 To 319 Step 11
            PictureBox1.CreateGraphics.FillRectangle(Brushes.Black, 0, i, 319, 1)
        Next
    End Sub


    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.AddRange(nomPerles)
        ComboBox2.Items.AddRange(nomPerles)
        ComboBox1.SelectedIndex = 13
        ComboBox2.SelectedIndex = 0
        bmpToSave = New Bitmap(29, 29)
        For i = 0 To 28
            For j = 0 To 28
                bmpToSave.SetPixel(i, j, Color.White)
            Next
        Next

        brushColor = New SolidBrush(Color.Black)
        brushColor2 = New SolidBrush(Color.White)
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0
                colorOfBrush = Color.White
            Case 1
                colorOfBrush = Color.PapayaWhip
            Case 2
                colorOfBrush = Color.Yellow
            Case 3
                colorOfBrush = Color.FromArgb(255, 128, 0)
            Case 4
                colorOfBrush = Color.Red
            Case 5
                colorOfBrush = Color.FromArgb(255, 128, 255)
            Case 6
                colorOfBrush = Color.Purple
            Case 7
                colorOfBrush = Color.Blue
            Case 8
                colorOfBrush = Color.DarkBlue
            Case 9
                colorOfBrush = Color.Green
            Case 10
                colorOfBrush = Color.LightGreen
            Case 11
                colorOfBrush = Color.Brown
            Case 12
                colorOfBrush = Color.Gray
            Case 13
                colorOfBrush = Color.Black
            Case 14
                colorOfBrush = Color.FromArgb(192, 64, 0)
            Case 15
                colorOfBrush = Color.Chocolate
            Case 16
                colorOfBrush = Color.FromArgb(255, 128, 128)
            Case 17
                colorOfBrush = Color.Moccasin
            Case 18
                colorOfBrush = Color.DarkGreen
            Case 19
                colorOfBrush = Color.Crimson
            Case 20
                colorOfBrush = Color.FromArgb(64, 0, 64)
            Case 21
                colorOfBrush = Color.Turquoise
            Case 22
                colorOfBrush = Color.FromArgb(255, 255, 192)
            Case 23
                colorOfBrush = Color.FromArgb(255, 192, 192)
            Case 24
                colorOfBrush = Color.Plum
            Case 25
                colorOfBrush = Color.FromArgb(192, 255, 255)
            Case 26
                colorOfBrush = Color.FromArgb(192, 255, 192)
            Case 27
                colorOfBrush = Color.FromArgb(255, 192, 255)
            Case 28
                colorOfBrush = Color.FromArgb(192, 64, 0)
        End Select
        brushColor = New SolidBrush(colorOfBrush)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SaveFileDialog1.Filter = "Bitmap Image|*.bmp"
        SaveFileDialog1.Title = "enregistrer sous..."
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName <> "" Then
            Dim fs As System.IO.FileStream = CType(SaveFileDialog1.OpenFile(), System.IO.FileStream)
            bmpToSave.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp)
            fs.Close()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Select Case ComboBox2.SelectedIndex
            Case 0
                colorOfBrush2 = Color.White
            Case 1
                colorOfBrush2 = Color.PapayaWhip
            Case 2
                colorOfBrush2 = Color.Yellow
            Case 3
                colorOfBrush2 = Color.FromArgb(255, 128, 0)
            Case 4
                colorOfBrush2 = Color.Red
            Case 5
                colorOfBrush2 = Color.FromArgb(255, 128, 255)
            Case 6
                colorOfBrush2 = Color.Purple
            Case 7
                colorOfBrush2 = Color.Blue
            Case 8
                colorOfBrush2 = Color.DarkBlue
            Case 9
                colorOfBrush2 = Color.Green
            Case 10
                colorOfBrush2 = Color.LightGreen
            Case 11
                colorOfBrush2 = Color.Brown
            Case 12
                colorOfBrush2 = Color.Gray
            Case 13
                colorOfBrush2 = Color.Black
            Case 14
                colorOfBrush2 = Color.FromArgb(192, 64, 0)
            Case 15
                colorOfBrush2 = Color.Chocolate
            Case 16
                colorOfBrush2 = Color.FromArgb(255, 128, 128)
            Case 17
                colorOfBrush2 = Color.Moccasin
            Case 18
                colorOfBrush2 = Color.DarkGreen
            Case 19
                colorOfBrush2 = Color.Crimson
            Case 20
                colorOfBrush2 = Color.FromArgb(64, 0, 64)
            Case 21
                colorOfBrush2 = Color.Turquoise
            Case 22
                colorOfBrush2 = Color.FromArgb(255, 255, 192)
            Case 23
                colorOfBrush2 = Color.FromArgb(255, 192, 192)
            Case 24
                colorOfBrush2 = Color.Plum
            Case 25
                colorOfBrush2 = Color.FromArgb(192, 255, 255)
            Case 26
                colorOfBrush2 = Color.FromArgb(192, 255, 192)
            Case 27
                colorOfBrush2 = Color.FromArgb(255, 192, 255)
            Case 28
                colorOfBrush2 = Color.FromArgb(192, 64, 0)
        End Select
        brushColor2 = New SolidBrush(colorOfBrush2)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SaveFileDialog1.Filter = "Prl File|*.prl"
        SaveFileDialog1.Title = "Enregistrer l'image modifié au format prl"
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName <> "" Then
            Dim fs As System.IO.FileStream = CType(SaveFileDialog1.OpenFile(), System.IO.FileStream)
            Dim myStream As StreamWriter = New StreamWriter(fs)
            Try
                myStream.WriteLine("29;29")
                For y = 0 To 28
                    For x = 0 To 28
                        myStream.Write(tabNb(x, y) & ";")
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

    Public Sub ChargerFichierPrl(ByVal path As Stream)
        If (stream IsNot Nothing) Then
            Dim monStreamReader As StreamReader = New StreamReader(stream)
            Dim ligneParam As String
            ligneParam = monStreamReader.ReadLine()
            Dim TableauParametres() As String = Split(ligneParam, ";")
            If (TableauParametres.Length <> 2) Then
                MsgBox("Fichier prl invalide !")
                Exit Sub
            End If

            Dim sizeX As Integer = CInt(TableauParametres(0))
            Dim sizeY As Integer = CInt(TableauParametres(1))
            Dim bmpOriginal As Bitmap = New Bitmap(sizeX, sizeY)

            Dim LectureFichierLigne(sizeY) As String
            Dim LectureFichierCaractere(sizeX) As String

            For y = 0 To 28
                LectureFichierLigne(y) = monStreamReader.ReadLine()
                LectureFichierCaractere = Split(LectureFichierLigne(y), ";")
                For x = 0 To 28
                    If (LectureFichierCaractere(x) = "-1") Then
                        MsgBox("Erreur de lecture du fichier, certaines couleurs sont invalides :( (erreur -1 fichier PRL)")
                        Exit Sub
                    End If

                    Dim rect As New Rectangle(x * 11, y * 11, 10, 10)
                    ComboBox1.SelectedIndex = LectureFichierCaractere(x)
                    PictureBox1.CreateGraphics.FillRectangle(brushColor, rect)
                    bmpToSave.SetPixel(x, y, colorOfBrush)
                    tabNb(x, y) = ComboBox1.SelectedIndex
                Next
            Next
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Try
                stream = OpenFileDialog1.OpenFile()
                ChargerFichierPrl(stream)
                stream.Close()
            Catch Ex As Exception
                MessageBox.Show("Impossible de lire le fichier :(" & vbCrLf & Ex.Message)
            End Try
        End If
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 0
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 0
        End If
    End Sub
    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox3.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 1
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 1
        End If
    End Sub
    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox4.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 2
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 2
        End If
    End Sub
    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox5.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 3
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 3
        End If
    End Sub
    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox6.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 4
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 4
        End If
    End Sub
    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox7.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 5
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 5
        End If
    End Sub
    Private Sub PictureBox8_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox8.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 6
            colorOfBrush = Color.Violet
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 6
            colorOfBrush2 = Color.Violet
        End If
    End Sub
    Private Sub PictureBox9_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox9.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 7
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 7
        End If
    End Sub
    Private Sub PictureBox10_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox10.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 8
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 8
        End If
    End Sub
    Private Sub PictureBox11_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox11.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 9
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 9
        End If
    End Sub
    Private Sub PictureBox12_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox12.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 10
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 10
        End If
    End Sub
    Private Sub PictureBox13_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox13.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 11
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 11
        End If
    End Sub
    Private Sub PictureBox14_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox14.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 12
            colorOfBrush = Color.Gray
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 12
            colorOfBrush2 = Color.Gray
        End If
    End Sub
    Private Sub PictureBox15_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox15.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 13
            colorOfBrush = Color.Black
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 13
            colorOfBrush2 = Color.Black
        End If
    End Sub
    Private Sub PictureBox16_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox16.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 14
            colorOfBrush = Color.Brown
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 14
            colorOfBrush2 = Color.Brown
        End If
    End Sub
    Private Sub PictureBox17_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox17.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 15
            colorOfBrush = Color.Chocolate
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 15
            colorOfBrush2 = Color.Chocolate
        End If
    End Sub
    Private Sub PictureBox18_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox18.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 16
            colorOfBrush = Color.Moccasin
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 16
            colorOfBrush2 = Color.Moccasin
        End If
    End Sub
    Private Sub PictureBox19_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox19.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 17
            colorOfBrush = Color.Beige
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 17
            colorOfBrush2 = Color.Beige
        End If
    End Sub
    Private Sub PictureBox20_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox20.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 18
            colorOfBrush = Color.DarkGreen
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 18
            colorOfBrush2 = Color.DarkGreen
        End If
    End Sub
    Private Sub PictureBox21_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox21.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 19
            colorOfBrush = Color.DarkRed
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 19
            colorOfBrush2 = Color.DarkRed
        End If
    End Sub
    Private Sub PictureBox22_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox22.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 20
            colorOfBrush = Color.Crimson
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 20
            colorOfBrush2 = Color.Crimson
        End If
    End Sub
    Private Sub PictureBox23_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox23.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 21
            colorOfBrush = Color.Turquoise
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 21
            colorOfBrush2 = Color.Turquoise
        End If
    End Sub
    Private Sub PictureBox24_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox24.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 22
            colorOfBrush = Color.LightYellow
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 22
            colorOfBrush2 = Color.LightYellow
        End If
    End Sub
    Private Sub PictureBox25_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox25.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 23
            colorOfBrush = Color.LightSalmon
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 23
            colorOfBrush2 = Color.LightSalmon
        End If
    End Sub
    Private Sub PictureBox26_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox26.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 24
            colorOfBrush = Color.Lavender
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 24
            colorOfBrush2 = Color.Lavender
        End If
    End Sub
    Private Sub PictureBox27_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox27.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 25
            colorOfBrush = Color.LightSkyBlue
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 25
            colorOfBrush2 = Color.LightSkyBlue
        End If
    End Sub
    Private Sub PictureBox28_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox28.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 26
            colorOfBrush = Color.LightSeaGreen
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 26
            colorOfBrush2 = Color.LightSeaGreen
        End If
    End Sub
    Private Sub PictureBox29_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox29.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 27
            colorOfBrush = Color.LightPink
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 27
            colorOfBrush2 = Color.LightPink
        End If
    End Sub
    Private Sub PictureBox30_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox30.Click
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            ComboBox1.SelectedIndex = 28
            colorOfBrush = Color.Beige
        ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then
            ComboBox2.SelectedIndex = 28
            colorOfBrush2 = Color.Beige
        End If
    End Sub

    Private Sub PictureBox2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox2.MouseEnter
        Label3.Text = nomPerles(0)
    End Sub
    Private Sub PictureBox3_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox3.MouseEnter
        Label3.Text = nomPerles(1)
    End Sub
    Private Sub PictureBox4_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox4.MouseEnter
        Label3.Text = nomPerles(2)
    End Sub
    Private Sub PictureBox5_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox5.MouseEnter
        Label3.Text = nomPerles(3)
    End Sub
    Private Sub PictureBox6_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox6.MouseEnter
        Label3.Text = nomPerles(4)
    End Sub
    Private Sub PictureBox7_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox7.MouseEnter
        Label3.Text = nomPerles(5)
    End Sub
    Private Sub PictureBox8_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox8.MouseEnter
        Label3.Text = nomPerles(6)
    End Sub
    Private Sub PictureBox9_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox9.MouseEnter
        Label3.Text = nomPerles(7)
    End Sub
    Private Sub PictureBox10_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox10.MouseEnter
        Label3.Text = nomPerles(8)
    End Sub
    Private Sub PictureBox11_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox11.MouseEnter
        Label3.Text = nomPerles(9)
    End Sub
    Private Sub PictureBox12_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox12.MouseEnter
        Label3.Text = nomPerles(10)
    End Sub
    Private Sub PictureBox13_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox13.MouseEnter
        Label3.Text = nomPerles(11)
    End Sub
    Private Sub PictureBox14_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox14.MouseEnter
        Label3.Text = nomPerles(12)
    End Sub
    Private Sub PictureBox15_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox15.MouseEnter
        Label3.Text = nomPerles(13)
    End Sub
    Private Sub PictureBox16_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox16.MouseEnter
        Label3.Text = nomPerles(14)
    End Sub
    Private Sub PictureBox17_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox17.MouseEnter
        Label3.Text = nomPerles(15)
    End Sub
    Private Sub PictureBox18_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox18.MouseEnter
        Label3.Text = nomPerles(16)
    End Sub
    Private Sub PictureBox19_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox19.MouseEnter
        Label3.Text = nomPerles(17)
    End Sub
    Private Sub PictureBox20_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox20.MouseEnter
        Label3.Text = nomPerles(18)
    End Sub
    Private Sub PictureBox21_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox21.MouseEnter
        Label3.Text = nomPerles(19)
    End Sub
    Private Sub PictureBox22_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox22.MouseEnter
        Label3.Text = nomPerles(20)
    End Sub
    Private Sub PictureBox23_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox23.MouseEnter
        Label3.Text = nomPerles(21)
    End Sub
    Private Sub PictureBox24_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox24.MouseEnter
        Label3.Text = nomPerles(22)
    End Sub
    Private Sub PictureBox25_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox25.MouseEnter
        Label3.Text = nomPerles(23)
    End Sub
    Private Sub PictureBox26_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox26.MouseEnter
        Label3.Text = nomPerles(24)
    End Sub
    Private Sub PictureBox27_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox27.MouseEnter
        Label3.Text = nomPerles(25)
    End Sub
    Private Sub PictureBox28_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox28.MouseEnter
        Label3.Text = nomPerles(26)
    End Sub
    Private Sub PictureBox29_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox29.MouseEnter
        Label3.Text = nomPerles(27)
    End Sub
    Private Sub PictureBox30_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox30.MouseEnter
        Label3.Text = nomPerles(28)
    End Sub


End Class