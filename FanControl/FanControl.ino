// The Sparkfun Monster Moto Sheild PWM inputs are rated at 20kHz
// and the minimum practical pulse inout is about 4 us.
// sheild pin assignments
//
// Code based on example from Alan0 from https://forum.arduino.cc/index.php?topic=319247.0
//
#define PinInA1 7
#define PinInA2 4
#define PinInB1 8
#define PinInB2 9
#define PinPWM1 5
#define PinPWM2 6
#define PinCS1 A2
#define PinCS2 A3
#define PinEN1 A0
#define PinEN2 A1

// While the motor shield can go lower then this value
// at least my fans don't seem to like it much, when I'm
// putting around the track at 1km/h...
#define MIN_DUTY_CYCLE 15

// If your motor shield is cutting out lower this value, 
// which effectively will limit the heat I believe in the chip.
#define MAX_DUTY_CYCLE 92

// Spin up the fans a bit faster then if we were a 1-1 
// mapping of car speed to fan speed
#define fanCurve 1.00

#define Stopped 0
#define Active 1
#define motorDelay 5
//////////////////////////////
// Interrupt callbacks for PWM control
//////////////////////////////
// Timer2 compare A interrupt service routine
ISR(TIMER2_COMPA_vect)
{
    // Turn on PWM pins 5 and 6
    //      76543210 Arduino digital pin number
    PORTD |= B01100000;
}

ISR(TIMER2_COMPB_vect)      // Timer2 compare B interrupt service routine
{
    // Turn off PWN pins 5 and 6
    //      76543210 Arduino digital pin number
    PORTD &= B10011111;
}

//////////////////////////////
// Setup motorshield and Arduino 
//////////////////////////////
void setup()
{
    Serial.begin(9600);

    // Initialise H-Bridge (Monster Moto)
    pinMode(PinInA1, OUTPUT);
    pinMode(PinInA2, OUTPUT);
    pinMode(PinInB1, OUTPUT);
    pinMode(PinInB2, OUTPUT);
    pinMode(PinPWM1, OUTPUT);
    pinMode(PinPWM2, OUTPUT);
    pinMode(PinCS1, INPUT);
    pinMode(PinCS2, INPUT);
    pinMode(PinEN1, OUTPUT);
    pinMode(PinEN2, OUTPUT);
    digitalWrite(PinEN1, HIGH);
    digitalWrite(PinEN2, HIGH);
    digitalWrite(PinInA1, LOW);
    digitalWrite(PinInA2, LOW);
    digitalWrite(PinInB1, LOW);
    digitalWrite(PinInB2, LOW);
    digitalWrite(PinPWM1, 25);
    digitalWrite(PinPWM2, 25);

    // Initialize timer1
    noInterrupts();         // Disable all interrupts
    TCCR2A = B00000010;     // Disconnect Arduino D3 and D11 pins and set CTC mode (see AVR datasheet S11.11.1)
    TCCR2B = B00000010;     // Set clock prescaler to 8, now clock is 2MHz (see AVR datasheet S11.11.2)
    TCNT2 = B00000000;      // Reset timer (why not?) (see AVR datasheet S18.11.3)
    OCR2A = 99;             // Set compare match register A for 20kHz PWM frequency (see AVR datasheet S18.11.4)
    OCR2B = 7;              // OCR2B (see AVR datasheet S18.11.5) to duty, 1% per step, minimum pulse width is 4 us.
    TIMSK2 = B00000110;     // Enable timer compare interrupt on OCR2A ans 0CR2B (see AVR datasheet S18.11.6)
    interrupts();           // Enable all interrupts

                            // PWM duty percent, valid values are between 8% to 92% (limits are imposed)
    setPWMDuty(0);          // 2% is a good alround value for most motors
}

///////////
// Globals
///////////
int motorState = Stopped;
byte spd;

void loop()
{
    ReadData();
    CalcPWM();

    if(motorState == Stopped) {
        digitalWrite(PinInA1, LOW); digitalWrite(PinInB1, LOW);
        digitalWrite(PinInA2, LOW); digitalWrite(PinInB2, LOW);
    }
    else {
        digitalWrite(PinInA1, HIGH); digitalWrite(PinInB1, LOW);
        digitalWrite(PinInA2, HIGH); digitalWrite(PinInB2, LOW);
    }

    delay(motorDelay);
}

void ReadData() {
    if (Serial.available() < 4) {
        return;
    }
    
    // Not sure what the developer is trying to get here?
    // A start code perhaps?
    if (Serial.read() == 255) {
        if (Serial.read() == 88) {
            if (Serial.read() == 255) {
                spd = Serial.read();                
            }
        }
    }
}

void CalcPWM() {
    if (spd > 0) {
        motorState = Active;  
        int byteToDuty = map(spd, 0, 255, MIN_DUTY_CYCLE, MAX_DUTY_CYCLE) * fanCurve;      
        int speed = constrain(byteToDuty, MIN_DUTY_CYCLE, MAX_DUTY_CYCLE);
        setPWMDuty(speed);
    }
    else {
        setPWMDuty(MIN_DUTY_CYCLE);
        motorState = Stopped;
    }
}

// PWM duty percent 8% to 92%
void setPWMDuty(int duty) {
    OCR2B = constrain(duty, MIN_DUTY_CYCLE, MAX_DUTY_CYCLE);
}

