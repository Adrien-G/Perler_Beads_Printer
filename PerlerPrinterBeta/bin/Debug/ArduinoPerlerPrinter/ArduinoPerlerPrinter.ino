/////////////////generated Setup part code
#include <Servo.h>
#include <Stepper.h>

#define STEP_ORIGIN_X 1300
#define STEP_ORIGIN_Y 450
#define STEP_ORIGIN_CH 1100
#define STEP1X 170
#define STEP1Y 170
#define STEP1CH 270
#define CHARGER_TOTAL_CAPACITY 20
#define STEPS  48*64

int ChargerActualCapacity = 20; //movable value
int ChargerNumber = 0; //actual number of the charger
int previousX = 0;
int previousY = 0;

Servo myservo180;
Stepper StepperX(STEPS,2,3,4,5);
Stepper StepperY(STEPS,6,7,8,9);
Stepper StepperCh(STEPS,10,11,12,13);

void setup() {
    Serial.begin(9600);
    pinMode(23, INPUT); //bouton X
    pinMode(25, INPUT);//bouton Y
    pinMode(27, INPUT);//bouton chargeur
    myservo180.attach(52);//moteur 180deg
    myservo180.write(180);
}

/////////////////end generated Setup part code
/////////////////generated Functions part code
void CalibratePrinter(){
    StepperX.setSpeed(3);
    StepperY.setSpeed(3);
    StepperCh.setSpeed(3);
    //decalage X
    Serial.println("Calibrage X");
    if(digitalRead(23)){
        while(digitalRead(23))
            StepperX.step(1);
        StepperX.step(20);
    }
    while(digitalRead(23) == 0)
        StepperX.step(-1);
    //decalage Y
    Serial.println("Calibrage Y");
    if(digitalRead(25)){
        while(digitalRead(25))
            StepperY.step(1);
        StepperY.step(20);
    }
    while(digitalRead(25) == 0)
        StepperY.step(-1);
    //decalage Charger
    Serial.println("Calibrage Chargeur");
    if(digitalRead(27)){
        while(digitalRead(27))
            StepperCh.step(1);
        StepperCh.step(20);
    }
    while(digitalRead(27) == 0)
        StepperCh.step(-1);
    Serial.println("Calibrage placement ch");
    StepperCh.step(-750);
    
    Serial.println("Calibrage placement Y");
    StepperY.step(160);
    
    Serial.println("Calibrage placement X");
    StepperX.step(-6500);
    
    StepperX.setSpeed(9);
    StepperY.setSpeed(9);
    StepperCh.setSpeed(5);
}

void GoToLocationAndReleasePerl(int x,int y,int perlNo){
    if(perlNo == 0){ //déplacement perle 1
        TakePerl();
        StepperX.step(STEP_ORIGIN_X + STEP1X * x);
        StepperY.step(-(STEP_ORIGIN_Y + STEP1Y * y));
        ReleasePerl(0);
        previousX = x;
        previousY = y;
    } else { // déplacement perle 2
        StepperX.step((STEP1X * x) - (previousX * STEP1X));
        StepperY.step(-((STEP1Y * y) - (previousY * STEP1Y)));
        ReleasePerl(1);
        StepperY.step(STEP_ORIGIN_Y + STEP1Y * y);
        StepperX.step(-STEP_ORIGIN_X -STEP1X * x);
    }
}

void ShakeIt(int strength){
    StepperCh.step(strength);
    StepperCh.step(-strength);
    StepperCh.step(strength);
    StepperCh.step(-strength);
}

void TakePerl(){
    StepperCh.step(STEP_ORIGIN_CH + ChargerNumber * STEP1CH);
    ShakeIt(80);
    delay(500);
    StepperCh.step(-(STEP_ORIGIN_CH + ChargerNumber * STEP1CH));
    ShakeIt(80);
    delay(500);
}

void ReleasePerl(int nb){
    if(nb == 0){
        for (int pos = 180; pos >= 100; pos -= 1) {
            myservo180.write(pos);
            delay(50);
        }
       StepperY.step(-100);
       delay(50);
       StepperY.step(100);
       delay(1000);
    }
    else {
        for (int pos = 100; pos >= 55; pos -= 1) {
           myservo180.write(pos);
           delay(50);
        }
       StepperY.step(-100);
       delay(50);
       StepperY.step(100);
       delay(1000);
       myservo180.write(180);
    }
}

void NextColor(){
    ChargerNumber+=1;
}

void GoToNextCharger(){
    ChargerNumber+=1;
}

void OperateOnePerl(int x,int y,int noPerl){
    Serial.print(x);
    Serial.print(",");
    Serial.println(y);
    GoToLocationAndReleasePerl(x,y,noPerl);
}

/////////////////end generated Functions part code
/////////////////generated StartLoop part code
void loop() {
    Serial.println("Initialisation...");
    delay(1000);

    Serial.println("Calibrage...");
    CalibratePrinter();

    while (Serial.available() == 0) { //attente d'une entrée sur le port serie
        int val = Serial.read() - '0'; 
        if (val == 1) { 
            delay(100);

            Serial.println("Initialisation...");
            delay(1000);

            //Serial.println("Calibrage...");
            //CalibratePrinter();

            Serial.println("Envoi des donnees\r\n");
            break;
        }
    }
/////////////////end generated StartLoop part code
/////////////////generated LoopCore code
    OperateOnePerl(0,0,0);
    OperateOnePerl(0,28,1);
    OperateOnePerl(28,0,0);
    OperateOnePerl(28,28,1);
/////////////////end generated LoopCore code
/////////////////generated EndLoop part code
    Serial.println("Operation Termine !");
    while(1){
    delay(1000);
    }
}/////////////////end generated EndLoop part code
