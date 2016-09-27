#define BRAKEVCC 0
#define CW   1
#define CCW  2
#define BRAKEGND 3
#define CS_THRESHOLD 100

int inApin[2] = {7, 4};  // INA: Clockwise input
int inBpin[2] = {8, 9}; // INB: Counter-clockwise input
int pwmpin[2] = {5, 6}; // PWM input
int cspin[2] = {2, 3}; // CS: Current sense ANALOG input
int enpin[2] = {0, 1}; // EN: Status of switches output (Analog pin)
int statpin = 13;
 

int Speed ;
int bufferArray[4];
byte spd, carTopSpeed;

void setup()
{
 //pinMode(11,OUTPUT);???
    
 Serial.begin(9600);
 
 pinMode(statpin, OUTPUT);

 // Initialize digital pins as outputs
 for (int i=0; i<2; i++)
 {
   pinMode(inApin[i], OUTPUT);
   pinMode(inBpin[i], OUTPUT);
   pinMode(pwmpin[i], OUTPUT);
 }

 
}

void loop(){ 
  ReadData();    
  CalcPWM();
                         
  motorGo(0, CW, Speed);        //Motor1
  motorGo(1, CW, Speed);      //Motor2
  if (Speed=0 , motorOff);

}

void motorOff(int motor)
{
 // Initialize braked
 for (int i=0; i<2; i++)
 {
   digitalWrite(inApin[i], LOW);
   digitalWrite(inBpin[i], LOW);
 }
 analogWrite(pwmpin[motor], Speed);
}

void motorGo(uint8_t motor, uint8_t direct, uint8_t Speed)
{
 if (motor <= 1)
 {
   if (direct <=4)
   {
     // Set inA[motor]
     if (direct <=1)
       digitalWrite(inApin[motor], HIGH);
     else
       digitalWrite(inApin[motor], LOW);

     // Set inB[motor]
     if ((direct==0)||(direct==2))
       digitalWrite(inBpin[motor], HIGH);
     else
       digitalWrite(inBpin[motor], LOW);
     analogWrite(pwmpin[motor], Speed);
   }
 }
}

void ReadData(){
  if (Serial.available() > 0) {
    if (Serial.available() > 3) {
      if (Serial.read() == 255) {
        if (Serial.read() == 88) {
          if (Serial.read() == 255) {            
            spd = Serial.read();            
            //carTopSpeed = Serial.read();
          }
        }
      }
    }
  }
}

void CalcPWM() {
      
    //if (carTopSpeed == 0) {
    //  carTopSpeed = 255;
    //}
    
    if (spd > 0){
      //spd = (spd * (255 / carTopSpeed));         
      Speed = map(spd, 0 , 255 , 15 , 255);
      analogWrite(11,Speed);
    }
  
}

