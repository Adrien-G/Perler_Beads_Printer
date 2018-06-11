Imports System
Imports System.IO.Ports
Imports System.IO

Public Class MainForm

    Private Delegate Sub SetLabelDelegate(ByVal text As String)
    Private objDelegate As SetLabelDelegate
    Dim bmpModifieOK As Bitmap
    Dim bmpZoomed As Bitmap
    Dim stream As Stream
    Dim nbPerlesColorPalette = 30
    Dim colorPalette(nbPerlesColorPalette) As Color
    Dim nbPerles(nbPerlesColorPalette) As Integer
    Dim transparencyColorSet As Boolean = 0


    Dim nomPerles As String() = {"Blanc (01)", "Crème (02)", "Jaune (03)", "Orange (04)", "Rouge (05)", "Rose (06)", "Violet (07)", "Bleu (08)", _
    "Bleu foncé (09)", "Vert (10)", "Vert Clair (11)", "Marron (12)", "Gris (17)", "Noir (18)", "Caramel (20)", "Marron clair (21)", "Chair (22)", _
    "Beige (27)", "Vert foncé (28)", "Lit-de-vin (29)", "Bordeaux (30)", "Turquoise (31)", "Vert fluorescent (41)", "Jaune pastel (43)", _
    "Rouge pastel (44)", "Violet pastel (45)", "Bleu pastel (46)", "Vert pastel (47)", "Rose pastel (48)", "Marron nounours (60)"}

    Function GetColorAndCount(ByVal index As Integer)
        If (transparencyColorSet = 0) Then
            ComboBox2.SelectedIndex = index
            transparencyColorSet = 1
        End If
        nbPerles(index) += 1
        Return colorPalette(index)
    End Function

    Public Sub setPixelPropre290()
        bmpZoomed = New Bitmap(290, 290)
        For y = 0 To 289
            For x = 0 To 289
                bmpZoomed.SetPixel(x, y, bmpModifieOK.GetPixel(Math.Floor(x / 10), Math.Floor(y / 10)))
            Next
        Next
        PictureBox1.Image = bmpZoomed
    End Sub

    Public Sub setPixelPropre290OnePixel(ByVal posX, ByVal posY)
        For y = 0 To 9
            For x = 0 To 9
                bmpZoomed.SetPixel(posX * 10 + x, posY * 10 + y, bmpModifieOK.GetPixel(posX, posY))
            Next
        Next
        PictureBox1.Image = bmpZoomed
    End Sub

    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Dim donnees = SerialPort1.ReadLine
        Me.Invoke(New Action(Of String)(AddressOf setLabel), donnees)


        'Dim sp As SerialPort = CType(sender, SerialPort)
        'sp.Write("!11,f" & ControlChars.CrLf)
        ''s'il y a des data à lire 
        'If sp.BytesToRead > 0 Then
        '    Dim InData As String = sp.ReadLine
        '    Me.BeginInvoke(objDelegate, New Object() {InData})
        '    'ou delai d'attente depasse 
        'ElseIf sp.ReadTimeout > 1000 Then
        '    Dim InData As String = "pas de donnees...."
        '    Me.BeginInvoke(objDelegate, New Object() {InData})
        'End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
        If (ComboBox1.Items.Count > 0) Then
            ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
        End If
        ComboBox2.Items.AddRange(nomPerles)
        bmpModifieOK = New Bitmap(30, 30)
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (SerialPort1.IsOpen) Then
            Try
                SerialPort1.Close()
            Catch
            End Try
        End If
    End Sub

    Private Sub Label1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label1.TextChanged
        Dim mystring As String = Label1.Text
        Dim index As Integer = mystring.IndexOf(",")

        If (index > 0) Then
            bmpModifieOK.SetPixel(Mid(mystring, index + 2), Mid(mystring, 1, index + 1), Color.Cyan)
            setPixelPropre290OnePixel(Mid(mystring, index + 2), Mid(mystring, 1, index + 1))
        End If
        ProgressBar1.PerformStep()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ComboBox1.Items.Clear()
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
        If (ComboBox1.Items.Count >= 1) Then
            ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
        End If
    End Sub

    Public Sub ChargerFichierPrl(ByVal path As Stream)
        stream = OpenFileDialog1.OpenFile()

        Dim colorBmp As Bitmap = Image.FromFile("NuancierPerles2.bmp")
        For k = 0 To nbPerlesColorPalette - 1
            colorPalette(k) = colorBmp.GetPixel(k, 0)
            nbPerles(k) = 0
        Next

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
            Label8.Text = "Taille de l'image : " & sizeX & " x " & sizeY

            Dim LectureFichierLigne(sizeY) As String
            Dim LectureFichierCaractere(sizeX) As String

            For y = 0 To CInt(sizeY - 1)
                LectureFichierLigne(y) = monStreamReader.ReadLine()
                LectureFichierCaractere = Split(LectureFichierLigne(y), ";")
                For x = 0 To CInt(sizeX - 1)
                    If (LectureFichierCaractere(x) = "-1") Then
                        MsgBox("Erreur de lecture du fichier, certaines couleurs sont invalides :( (erreur -1 fichier PRL)")
                        Exit Sub
                    End If
                    bmpOriginal.SetPixel(x, y, GetColorAndCount(LectureFichierCaractere(x)))
                Next
            Next
            If (FormConfigure.CheckBox1.Checked = True) Then
                If (sizeX > 29 Or sizeY > 29) Then
                    MsgBox("L'image est superieur a 29x29, l'image ne sera pas imprimé en totalité.", MsgBoxStyle.Exclamation)
                End If
            End If
            bmpModifieOK = bmpOriginal
            setPixelPropre290()
            stream.Dispose()
            stream.Close()
            Label5.Text = "fichier PRL validé et chargé \o/"

        End If
    End Sub
    Public Sub colorOfEachCharger()
        TextBox2.Clear()
        Dim m As Integer
        Dim counter As Integer = 1
        For k = 0 To nbPerlesColorPalette - 1
            If (nbPerles(k) > 0 And k <> ComboBox2.SelectedIndex) Then
                For m = 0 To nbPerles(k) Step FormConfigure.TextBoxChargerCount.Text
                    Dim nbCharger = nbPerles(k)
                    TextBox2.AppendText(counter & " - " & nomPerles(k) & "-> " & nbCharger & " perles" & vbCrLf)
                    counter += 1
                    If (counter > 23) Then
                        Exit Sub
                    End If
                Next
            End If
        Next
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If (OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Label5.Text = "Validation du fichier PRL"
            Try
                stream = OpenFileDialog1.OpenFile()
                ChargerFichierPrl(stream)
                stream.Close()
                GroupBox3.Enabled = True
                colorOfEachCharger()
            Catch Ex As Exception
                MessageBox.Show("Impossible de lire le fichier :(" & vbCrLf & Ex.Message)
            End Try
        End If
    End Sub


    Public Sub SetupPart()
        TextBox1.AppendText("/////////////////generated Setup part code" & vbCrLf)
        TextBox1.AppendText( _
        "#include <Servo.h>" & vbCrLf & _
        "#include <Stepper.h>" & vbCrLf & vbCrLf & _
        "#define STEP_ORIGIN_X " & FormConfigure.TextBox3DeplacementXInitial.Text & vbCrLf & _
        "#define STEP_ORIGIN_Y " & FormConfigure.TextBoxDeplacementYInitial.Text & vbCrLf & _
        "#define STEP_ORIGIN_CH " & FormConfigure.TextBoxInitVersChargeur1.Text & vbCrLf & _
        "#define STEP1X " & FormConfigure.step1X.Text & vbCrLf & _
        "#define STEP1Y " & FormConfigure.step1Y.Text & vbCrLf & _
        "#define STEP1CH " & FormConfigure.TextBoxEspaceEntreChargeur.Text & vbCrLf & _
        "#define CHARGER_TOTAL_CAPACITY " & FormConfigure.TextBoxChargerCapacity.Text & vbCrLf & _
        "#define STEPS  48*64" & vbCrLf & vbCrLf & _
        "int ChargerActualCapacity = " & FormConfigure.TextBoxChargerCapacity.Text & "; //movable value" & vbCrLf & _
        "int ChargerNumber = 0; //actual number of the charger" & vbCrLf & _
        "int previousX = 0;" & vbCrLf & _
        "int previousY = 0;" & vbCrLf & vbCrLf & _
        "Servo myservo180;" & vbCrLf & _
        "Stepper StepperX(STEPS," & FormConfigure.ComboBoxPaPX.Text & ");" & vbCrLf & _
        "Stepper StepperY(STEPS," & FormConfigure.ComboBoxPaPY.Text & ");" & vbCrLf & _
        "Stepper StepperCh(STEPS," & FormConfigure.ComboBoxPaPCh.Text & ");" & vbCrLf & vbCrLf & _
        "void setup() {" & vbCrLf & _
        "    Serial.begin(9600);" & vbCrLf & _
        "    pinMode(" & FormConfigure.ComboBoxBoutonX.Text & ", INPUT); //bouton X" & vbCrLf & _
        "    pinMode(" & FormConfigure.ComboBoxBoutonY.Text & ", INPUT);//bouton Y" & vbCrLf & _
        "    pinMode(" & FormConfigure.ComboBoxBoutonCh.Text & ", INPUT);//bouton chargeur" & vbCrLf & _
        "    myservo180.attach(" & FormConfigure.ComboBoxPaP180.Text & ");//moteur 180deg" & vbCrLf & _
        "    myservo180.write(" & FormConfigure.TextBoxDeplacementServoInitial.Text & ");" & vbCrLf & _
        "}" & vbCrLf & vbCrLf)
        TextBox1.AppendText("/////////////////end generated Setup part code" & vbCrLf)


    End Sub

    Public Sub FuncPart()
        REM voidgotolocation -> -1 et -2 étant l'allez retour vers l'entrée des perles
        REM void take perl//reculer et avancer le stepperCh => baseCh + chargerNumber * Step1Ch
        TextBox1.AppendText("/////////////////generated Functions part code" & vbCrLf)
        TextBox1.AppendText( _
        "void CalibratePrinter(){" & vbCrLf & _
        "    StepperX.setSpeed(" & FormConfigure.ComboBoxVitesseMoteurCalibrage.Text & ");" & vbCrLf & _
        "    StepperY.setSpeed(" & FormConfigure.ComboBoxVitesseMoteurCalibrage.Text & ");" & vbCrLf & _
        "    StepperCh.setSpeed(" & FormConfigure.ComboBoxVitesseMoteurCalibrage.Text & ");" & vbCrLf & _
         "    //decalage X" & vbCrLf & _
        "    Serial.println(""Calibrage X"");" & vbCrLf & _
        "    if(digitalRead(" & FormConfigure.ComboBoxBoutonX.Text & ")){" & vbCrLf & _
        "        while(digitalRead(" & FormConfigure.ComboBoxBoutonX.Text & "))" & vbCrLf & _
        "            StepperX.step(1);" & vbCrLf & _
        "        StepperX.step(20);" & vbCrLf & _
        "    }" & vbCrLf & _
        "    while(digitalRead(" & FormConfigure.ComboBoxBoutonX.Text & ") == 0)" & vbCrLf & _
        "        StepperX.step(-1);" & vbCrLf & _
        "    //decalage Y" & vbCrLf & _
        "    Serial.println(""Calibrage Y"");" & vbCrLf & _
        "    if(digitalRead(" & FormConfigure.ComboBoxBoutonY.Text & ")){" & vbCrLf & _
        "        while(digitalRead(" & FormConfigure.ComboBoxBoutonY.Text & "))" & vbCrLf & _
        "            StepperY.step(1);" & vbCrLf & _
        "        StepperY.step(20);" & vbCrLf & _
        "    }" & vbCrLf & _
        "    while(digitalRead(" & FormConfigure.ComboBoxBoutonY.Text & ") == 0)" & vbCrLf & _
        "        StepperY.step(-1);" & vbCrLf & _
        "    //decalage Charger" & vbCrLf & _
        "    Serial.println(""Calibrage Chargeur"");" & vbCrLf & _
        "    if(digitalRead(" & FormConfigure.ComboBoxBoutonCh.Text & ")){" & vbCrLf & _
        "        while(digitalRead(" & FormConfigure.ComboBoxBoutonCh.Text & "))" & vbCrLf & _
        "            StepperCh.step(1);" & vbCrLf & _
        "        StepperCh.step(20);" & vbCrLf & _
        "    }" & vbCrLf & _
        "    while(digitalRead(" & FormConfigure.ComboBoxBoutonCh.Text & ") == 0)" & vbCrLf & _
        "        StepperCh.step(-1);" & vbCrLf & _
        "    Serial.println(""Calibrage placement ch"");" & vbCrLf & _
        "    StepperCh.step(-" & FormConfigure.TextBoxDecalageCalibrageCh.Text & ");" & vbCrLf & _
        "    " & vbCrLf & _
        "    Serial.println(""Calibrage placement Y"");" & vbCrLf & _
        "    StepperY.step(" & FormConfigure.TextBoxDecalageCalibrageY.Text & ");" & vbCrLf & _
        "    " & vbCrLf & _
        "    Serial.println(""Calibrage placement X"");" & vbCrLf & _
        "    StepperX.step(-" & FormConfigure.TextBoxDecalageCalibrageX.Text & ");" & vbCrLf & _
        "    " & vbCrLf & _
        "    StepperX.setSpeed(" & FormConfigure.ComboBoxVitesseMoteursX.Text & ");" & vbCrLf & _
        "    StepperY.setSpeed(" & FormConfigure.ComboBoxVitesseMoteursY.Text & ");" & vbCrLf & _
        "    StepperCh.setSpeed(" & FormConfigure.ComboBoxVitesseMoteursCh.Text & ");" & vbCrLf & _
        "}" & vbCrLf & vbCrLf & _
        "void GoToLocationAndReleasePerl(int x,int y,int perlNo){" & vbCrLf & _
        "    if(perlNo == 0){ //déplacement perle 1" & vbCrLf & _
        "        TakePerl();" & vbCrLf & _
        "        StepperX.step(STEP_ORIGIN_X + STEP1X * x);" & vbCrLf & _
        "        StepperY.step(-(STEP_ORIGIN_Y + STEP1Y * y));" & vbCrLf & _
        "        ReleasePerl(0);" & vbCrLf & _
        "        previousX = x;" & vbCrLf & _
        "        previousY = y;" & vbCrLf & _
        "    } else { // déplacement perle 2" & vbCrLf & _
        "        StepperX.step((STEP1X * x) - (previousX * STEP1X));" & vbCrLf & _
        "        StepperY.step(-((STEP1Y * y) - (previousY * STEP1Y)));" & vbCrLf & _
        "        ReleasePerl(1);" & vbCrLf & _
        "        StepperY.step(STEP_ORIGIN_Y + STEP1Y * y);" & vbCrLf & _
        "        StepperX.step(-STEP_ORIGIN_X -STEP1X * x);" & vbCrLf & _
        "    }" & vbCrLf & _
        "}" & vbCrLf & vbCrLf & _
        "void ShakeIt(int strength){" & vbCrLf & _
        "    StepperCh.step(strength);" & vbCrLf & _
        "    StepperCh.step(-strength);" & vbCrLf & _
        "    StepperCh.step(strength);" & vbCrLf & _
        "    StepperCh.step(-strength);" & vbCrLf & _
        "}" & vbCrLf & vbCrLf & _
        "void TakePerl(){" & vbCrLf & _
        "    StepperCh.step(STEP_ORIGIN_CH + ChargerNumber * STEP1CH);" & vbCrLf & _
        "    ShakeIt(" & FormConfigure.TextBoxShakerStrenght.Text & ");" & vbCrLf & _
        "    delay(500);" & vbCrLf & _
        "    StepperCh.step(-(STEP_ORIGIN_CH + ChargerNumber * STEP1CH));" & vbCrLf & _
        "    ShakeIt(" & FormConfigure.TextBoxShakerstrenght2.Text & ");" & vbCrLf & _
        "    delay(500);" & vbCrLf & _
        "}" & vbCrLf & vbCrLf & _
        "void ReleasePerl(int nb){" & vbCrLf & _
        "    if(nb == 0){" & vbCrLf & _
        "        for (int pos = " & FormConfigure.TextBoxDeplacementServoInitial.Text & "; pos >= " & FormConfigure.TextBoxDeplacementServo1.Text & "; pos -= 1) {" & vbCrLf & _
        "            myservo180.write(pos);" & vbCrLf & _
        "            delay(50);" & vbCrLf & _
        "        }" & vbCrLf & _
        "       StepperY.step(-100);" & vbCrLf & _
        "       delay(50);" & vbCrLf & _
        "       StepperY.step(100);" & vbCrLf & _
        "       delay(1000);" & vbCrLf & _
        "    }" & vbCrLf & _
        "    else {" & vbCrLf & _
        "        for (int pos = " & FormConfigure.TextBoxDeplacementServo1.Text & "; pos >= " & FormConfigure.TextBoxDeplacementServo2.Text & "; pos -= 1) {" & vbCrLf & _
        "           myservo180.write(pos);" & vbCrLf & _
        "           delay(50);" & vbCrLf & _
        "        }" & vbCrLf & _
        "       StepperY.step(-100);" & vbCrLf & _
        "       delay(50);" & vbCrLf & _
        "       StepperY.step(100);" & vbCrLf & _
        "       delay(1000);" & vbCrLf & _
        "       myservo180.write(" & FormConfigure.TextBoxDeplacementServoInitial.Text & ");" & vbCrLf & _
        "    }" & vbCrLf & _
        "}" & vbCrLf & vbCrLf & _
        "void NextColor(){" & vbCrLf & _
        "    ChargerNumber+=1;" & vbCrLf & _
        "}" & vbCrLf & vbCrLf & _
        "void GoToNextCharger(){" & vbCrLf & _
        "    ChargerNumber+=1;" & vbCrLf & _
        "}" & vbCrLf & vbCrLf & _
        "void OperateOnePerl(int x,int y,int noPerl){" & vbCrLf & _
        "    Serial.print(x);" & vbCrLf & _
        "    Serial.print("","");" & vbCrLf & _
        "    Serial.println(y);" & vbCrLf & _
        "    GoToLocationAndReleasePerl(x,y,noPerl);" & vbCrLf & _
        "}" & vbCrLf & vbCrLf)
        TextBox1.AppendText("/////////////////end generated Functions part code" & vbCrLf)

    End Sub

    Public Sub LoopPart()
        TextBox1.AppendText("/////////////////generated StartLoop part code" & vbCrLf)
        TextBox1.AppendText( _
          "void loop() {" & vbCrLf & _
        "    Serial.println(""Initialisation..."");" & vbCrLf & _
        "    delay(1000);" & vbCrLf & vbCrLf & _
        "    Serial.println(""Calibrage..."");" & vbCrLf & _
        "    CalibratePrinter();" & vbCrLf & vbCrLf & _
        "    while (Serial.available() == 0) { //attente d'une entrée sur le port serie" & vbCrLf & _
        "        int val = Serial.read() - '0'; " & vbCrLf & _
        "        if (val == 1) { " & vbCrLf & _
        "            delay(100);" & vbCrLf & vbCrLf & _
        "            Serial.println(""Initialisation..."");" & vbCrLf & _
        "            delay(1000);" & vbCrLf & vbCrLf & _
        "            //Serial.println(""Calibrage..."");" & vbCrLf & _
        "            //CalibratePrinter();" & vbCrLf & vbCrLf & _
        "            Serial.println(""Envoi des donnees\r\n"");" & vbCrLf & _
        "            break;" & vbCrLf & _
        "        }" & vbCrLf & _
        "    }" & vbCrLf)
        TextBox1.AppendText("/////////////////end generated StartLoop part code" & vbCrLf)
        GenerateCoreCode()
        TextBox1.AppendText("/////////////////generated EndLoop part code" & vbCrLf)
        TextBox1.AppendText( _
        "    Serial.println(""Operation Termine !"");" & vbCrLf & _
        "    while(1){" & vbCrLf & _
        "    delay(1000);" & vbCrLf & _
        "    }" & vbCrLf & _
        "}")
        TextBox1.AppendText("/////////////////end generated EndLoop part code" & vbCrLf)
    End Sub

    Public Sub reverseBool(ByRef numPerl)
        If (numPerl) Then
            numPerl = 0
        Else
            numPerl = 1
        End If
    End Sub

    Public Sub GenerateCoreCode()
        TextBox1.AppendText("/////////////////generated LoopCore code" & vbCrLf)
        Dim numPerl
        Dim incremCharger = 0
        Dim incremChargerCount = 0
        numPerl = 0
        For k = 0 To nbPerlesColorPalette
            If (nbPerles(k) > 0 And k <> ComboBox2.SelectedIndex) Then
                For y = 0 To 28
                    For x = 0 To 28
                        If (bmpModifieOK.GetPixel(x, y) = colorPalette(k)) Then
                            TextBox1.AppendText("    OperateOnePerl(" & y & "," & x & "," & numPerl & ");" & vbCrLf)
                            reverseBool(numPerl)
                            incremCharger += 1
                            If (incremCharger >= FormConfigure.TextBoxChargerCapacity.Text And incremChargerCount <= FormConfigure.TextBoxChargerCount.Text) Then
                                TextBox1.AppendText("    GoToNextCharger();" & vbCrLf)
                                incremCharger = 0
                                incremChargerCount += 1
                            End If
                        End If
                    Next
                Next
                'TextBox1.AppendText("    NextColor();" & vbCrLf)
            End If
        Next
        TextBox1.AppendText("/////////////////end generated LoopCore code" & vbCrLf)
    End Sub




    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        TextBox1.Clear()
        SetupPart()
        FuncPart()
        LoopPart()
        If (Not File.Exists(My.Computer.FileSystem.CurrentDirectory & "\ArduinoPerlerPrinter\ArduinoPerlerPrinter.ino")) Then
            MkDir(My.Computer.FileSystem.CurrentDirectory & "\ArduinoPerlerPrinter")
        End If
        Dim sw As New StreamWriter(My.Computer.FileSystem.CurrentDirectory & "\ArduinoPerlerPrinter\ArduinoPerlerPrinter.ino")
        sw.Write(TextBox1.Text)
        sw.Close()

        Dim ArduinoSoft = Shell("""C:\Program Files (x86)\Arduino\arduino_debug.exe"" --port " & ComboBox1.SelectedItem & " --upload """ & My.Computer.FileSystem.CurrentDirectory & "\ArduinoPerlerPrinter\ArduinoPerlerPrinter.ino""", AppWinStyle.NormalFocus, True)
        Button9.Enabled = True
        Try
            If (SerialPort1.IsOpen() = False) Then
                objDelegate = New SetLabelDelegate(AddressOf setLabel)
                SerialPort1.PortName = ComboBox1.SelectedItem
                SerialPort1.BaudRate = 9600
                SerialPort1.Parity = Parity.None
                SerialPort1.StopBits = StopBits.One
                SerialPort1.DataBits = 8
                SerialPort1.Handshake = Handshake.None
                SerialPort1.Encoding = System.Text.Encoding.Default
                SerialPort1.Open()
                Threading.Thread.Sleep(500)
            End If
        Catch
            MsgBox("Erreur de communication avec l'arduino, est ce le bon port COM qui est sélectionné ?", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxSetForeground)
            ComboBox1.BackColor = Color.AliceBlue
        End Try
    End Sub

    Private Sub setLabel(ByVal donnees As String)
        Me.Label1.Text = donnees
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        ProgressBar1.Value = 0
        If (ComboBox1.SelectedItem = "") Then
            MsgBox("Veuillez sélectionner un port COM dans la liste", MsgBoxStyle.Exclamation)
        Else
            Try
                If (SerialPort1.IsOpen() = False) Then
                    'objDelegate = New SetLabelDelegate(AddressOf setLabel)
                    SerialPort1.PortName = ComboBox1.SelectedItem
                    SerialPort1.BaudRate = 9600
                    SerialPort1.Parity = Parity.None
                    SerialPort1.StopBits = StopBits.One
                    SerialPort1.DataBits = 8
                    SerialPort1.Handshake = Handshake.None
                    SerialPort1.Encoding = System.Text.Encoding.Default
                    SerialPort1.Open()
                    Threading.Thread.Sleep(500)
                End If
                SerialPort1.Write("1")
                setPixelPropre290()
            Catch
                MsgBox("Erreur de communication avec l'arduino, est ce le bon port COM qui est sélectionné ?", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxSetForeground)
                ComboBox1.BackColor = Color.AliceBlue
            End Try
        End If
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        FormCalibrate.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        FormConfigure.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        FormImportPic.show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        FormDraw.show()
    End Sub
End Class