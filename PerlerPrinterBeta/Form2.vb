Imports System.IO
Imports System.IO.Ports

Public Class Form2
    Private Delegate Sub SetLabelDelegate(ByVal text As String)
    Private objDelegate As SetLabelDelegate


    Public Sub SetupPart()
        TextBox1.AppendText("/////////////////generated Setup part code" & vbCrLf)
        TextBox1.AppendText( _
        "#include <Servo.h>" & vbCrLf & _
        "#include <Stepper.h>" & vbCrLf & vbCrLf & _
        "#define STEP_ORIGIN_X " & Form3.TextBox3DeplacementXInitial.Text & vbCrLf & _
        "#define STEP_ORIGIN_Y " & Form3.TextBoxDeplacementYInitial.Text & vbCrLf & _
        "#define STEP_ORIGIN_CH " & Form3.TextBoxInitVersChargeur1.Text & vbCrLf & _
        "#define STEP1X " & Form3.step1X.Text & vbCrLf & _
        "#define STEP1Y " & Form3.step1Y.Text & vbCrLf & _
        "#define STEP1CH " & Form3.TextBoxEspaceEntreChargeur.Text & vbCrLf & _
        "#define CHARGER_TOTAL_CAPACITY " & Form3.TextBoxChargerCapacity.Text & vbCrLf & _
        "#define STEPS  48*64" & vbCrLf & vbCrLf & _
        "int chargerX = 0;" & vbCrLf & _
        "int chargerY = 0;" & vbCrLf & _
        "int chargerCh = 0;" & vbCrLf & _
        "Stepper StepperX(STEPS," & Form3.ComboBoxPaPX.Text & ");" & vbCrLf & _
        "Stepper StepperY(STEPS," & Form3.ComboBoxPaPY.Text & ");" & vbCrLf & _
        "Stepper StepperCh(STEPS," & Form3.ComboBoxPaPCh.Text & ");" & vbCrLf & vbCrLf & _
        "void setup() {" & vbCrLf & _
        "    Serial.begin(9600);" & vbCrLf & _
        "    pinMode(" & Form3.ComboBoxBoutonX.Text & ", INPUT); //bouton X" & vbCrLf & _
        "    pinMode(" & Form3.ComboBoxBoutonY.Text & ", INPUT);//bouton Y" & vbCrLf & _
        "    pinMode(" & Form3.ComboBoxBoutonCh.Text & ", INPUT);//bouton chargeur" & vbCrLf & _
        "    Serial.println(""Initialisation..."");" & vbCrLf & _
        "    delay(1000);" & vbCrLf & vbCrLf & _
        "    Serial.println(""Calibrage..."");" & vbCrLf & _
        "    CalibratePrinter();" & vbCrLf & vbCrLf & _
        "    StepperX.setSpeed(" & Form3.ComboBoxVitesseMoteursX.Text & ");" & vbCrLf & _
        "    StepperY.setSpeed(" & Form3.ComboBoxVitesseMoteursY.Text & ");" & vbCrLf & _
        "    StepperCh.setSpeed(" & Form3.ComboBoxVitesseMoteursCh.Text & ");" & vbCrLf & _
        "    Serial.println(""pret !"");" & vbCrLf & _
        "}" & vbCrLf & vbCrLf)
        TextBox1.AppendText("/////////////////end generated Setup part code" & vbCrLf)


    End Sub

    Public Sub FuncPart()
        REM voidgotolocation -> -1 et -2 étant l'allez retour vers l'entrée des perles
        REM void take perl//reculer et avancer le stepperCh => baseCh + chargerNumber * Step1Ch
        TextBox1.AppendText("/////////////////generated Functions part code" & vbCrLf)
        TextBox1.AppendText( _
        "void CalibratePrinter(){" & vbCrLf & _
        "    StepperX.setSpeed(" & Form3.ComboBoxVitesseMoteurCalibrage.Text & ");" & vbCrLf & _
        "    StepperY.setSpeed(" & Form3.ComboBoxVitesseMoteurCalibrage.Text & ");" & vbCrLf & _
        "    StepperCh.setSpeed(" & Form3.ComboBoxVitesseMoteurCalibrage.Text & ");" & vbCrLf & _
        "    //decalage X" & vbCrLf & _
        "    Serial.println(""Calibrage X"");" & vbCrLf & _
        "    if(digitalRead(" & Form3.ComboBoxBoutonX.Text & ")){" & vbCrLf & _
        "        while(digitalRead(" & Form3.ComboBoxBoutonX.Text & "))" & vbCrLf & _
        "            StepperX.step(1);" & vbCrLf & _
        "        StepperX.step(20);" & vbCrLf & _
        "    }" & vbCrLf & _
        "    while(digitalRead(" & Form3.ComboBoxBoutonX.Text & ") == 0)" & vbCrLf & _
        "        StepperX.step(-1);" & vbCrLf & _
        "    //decalage Y" & vbCrLf & _
        "    Serial.println(""Calibrage Y"");" & vbCrLf & _
        "    if(digitalRead(" & Form3.ComboBoxBoutonY.Text & ")){" & vbCrLf & _
        "        while(digitalRead(" & Form3.ComboBoxBoutonY.Text & "))" & vbCrLf & _
        "            StepperY.step(1);" & vbCrLf & _
        "        StepperY.step(20);" & vbCrLf & _
        "    }" & vbCrLf & _
        "    while(digitalRead(" & Form3.ComboBoxBoutonY.Text & ") == 0)" & vbCrLf & _
        "        StepperY.step(-1);" & vbCrLf & _
        "    //decalage Charger" & vbCrLf & _
        "    Serial.println(""Calibrage Chargeur"");" & vbCrLf & _
        "    if(digitalRead(" & Form3.ComboBoxBoutonCh.Text & ")){" & vbCrLf & _
        "        while(digitalRead(" & Form3.ComboBoxBoutonCh.Text & "))" & vbCrLf & _
        "            StepperCh.step(1);" & vbCrLf & _
        "        StepperCh.step(20);" & vbCrLf & _
        "    }" & vbCrLf & _
        "    while(digitalRead(" & Form3.ComboBoxBoutonCh.Text & ") == 0)" & vbCrLf & _
        "        StepperCh.step(-1);" & vbCrLf & _
        "    Serial.println(""Calibrage placement ch"");" & vbCrLf & _
        "    StepperCh.step(-" & Form3.TextBoxDecalageCalibrageCh.Text & ");" & vbCrLf & _
        "    " & vbCrLf & _
        "    Serial.println(""Calibrage placement Y"");" & vbCrLf & _
        "    StepperY.step(" & Form3.TextBoxDecalageCalibrageY.Text & ");" & vbCrLf & _
        "    " & vbCrLf & _
        "    Serial.println(""Calibrage placement X"");" & vbCrLf & _
        "    StepperX.step(-" & Form3.TextBoxDecalageCalibrageX.Text & ");" & vbCrLf & _
        "    " & vbCrLf & _
        "    StepperX.setSpeed(" & Form3.ComboBoxVitesseMoteursX.Text & ");" & vbCrLf & _
        "    StepperY.setSpeed(" & Form3.ComboBoxVitesseMoteursY.Text & ");" & vbCrLf & _
        "    StepperCh.setSpeed(" & Form3.ComboBoxVitesseMoteursCh.Text & ");" & vbCrLf & _
        "}" & vbCrLf & vbCrLf)
        TextBox1.AppendText("/////////////////end generated Functions part code" & vbCrLf)

    End Sub

    Public Sub LoopPart()
        TextBox1.AppendText("/////////////////generated StartLoop part code" & vbCrLf)
        TextBox1.AppendText( _
          "void loop() {" & vbCrLf & _
        "    while (Serial.available() == 0); //attente d'une entrée sur le port serie" & vbCrLf & _
        "           int val = Serial.read() - '0'; " & vbCrLf & _
        "           if (val == 2){" & vbCrLf & _
        "               StepperX.step(" & TextBox2.Text & ");" & vbCrLf & _
        "               chargerX += " & TextBox2.Text & ";}" & vbCrLf & _
        "           if (val == 3){ " & vbCrLf & _
        "               StepperY.step(" & TextBox2.Text & ");" & vbCrLf & _
        "               chargerY += " & TextBox2.Text & ";}" & vbCrLf & _
        "           if (val == 4){" & vbCrLf & _
        "               StepperCh.step(" & TextBox2.Text & ");" & vbCrLf & _
        "               chargerCh += " & TextBox2.Text & ";}" & vbCrLf & _
        "           if (val == 5){ " & vbCrLf & _
        "               StepperX.step(-" & TextBox2.Text & ");" & vbCrLf & _
        "               chargerX -= " & TextBox2.Text & ";}" & vbCrLf & _
        "           if (val == 6){ " & vbCrLf & _
        "               StepperY.step(-" & TextBox2.Text & ");" & vbCrLf & _
        "               chargerY -= " & TextBox2.Text & ";}" & vbCrLf & _
        "           if (val == 7){ " & vbCrLf & _
        "              StepperCh.step(-" & TextBox2.Text & ");" & vbCrLf & _
        "               chargerCh -= " & TextBox2.Text & ";}" & vbCrLf & _
        "           Serial.print(chargerX);" & vbCrLf & _
        "           Serial.print("","");" & vbCrLf & _
        "           Serial.print(chargerY);" & vbCrLf & _
        "           Serial.print("","");" & vbCrLf & _
        "           Serial.println(chargerCh);" & vbCrLf & _
        "   }")
        TextBox1.AppendText("/////////////////end generated EndLoop part code" & vbCrLf)
    End Sub

    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Dim donnees = SerialPort1.ReadLine
        Me.Invoke(New Action(Of String)(AddressOf DataReceived), donnees)
    End Sub

    Private Sub DataReceived(ByVal donnees As String)
        Me.Label5.Text = donnees
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (SerialPort1.IsOpen()) Then
            SerialPort1.Close()
        End If
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
        Try
            If (SerialPort1.IsOpen() = False) Then
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

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
        If (ComboBox1.Items.Count > 0) Then
            ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SerialPort1.Write("2")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        SerialPort1.Write("5")
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        SerialPort1.Write("3")
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        SerialPort1.Write("6")
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        SerialPort1.Write("4")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        SerialPort1.Write("7")
    End Sub

    Private Sub Label5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label5.TextChanged
        Dim mystring As String = Label5.Text
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click

        ComboBox1.Items.Clear()
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
        If (ComboBox1.Items.Count >= 1) Then
            ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
        End If
    End Sub
End Class

