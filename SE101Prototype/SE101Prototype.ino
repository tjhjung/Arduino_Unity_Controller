#define L0PORT 3
#define L1PORT 2

#define L0BUTTON 8
#define L1BUTTON 10

int pressed0 = 0;
int pressed1 = 0;
int count = 0;


void setup() {
   pinMode(L0PORT, OUTPUT);
   pinMode(L0BUTTON, INPUT);
   pinMode(L1PORT, OUTPUT);
   pinMode(L1BUTTON, INPUT);
   digitalWrite(L0PORT, HIGH);
   digitalWrite(L1PORT, HIGH);
   Serial.begin(9600);
   Serial.println("Start!");
}

void loop () {
    
  //if (count % 2 == 0) {
    if (digitalRead(L0BUTTON) == 1 && pressed0 == 0) {
       digitalWrite(L0PORT, LOW);
       pressed0 = 1;
       count++;
       Serial.print("It took ");
       Serial.print(millis()/1000.0);
       Serial.println(" seconds to press the Red Button");
    } else if (pressed0 == 0) {
       digitalWrite(L0PORT, HIGH);
    }
  //} else {
    if (digitalRead(L1BUTTON) == 1 && pressed1 == 0) {
       digitalWrite(L1PORT, LOW);
       pressed1 = 1;
       count++;
       Serial.print("It took ");
       Serial.print(millis()/1000.0);
       Serial.println(" seconds to press the Green Button");
    } else if (pressed1 == 0) {
       digitalWrite(L1PORT, HIGH);
    }
  //}
}
