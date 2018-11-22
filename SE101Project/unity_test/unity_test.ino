
#define SW_pin 2
#define X_pin 1
#define Y_pin 2

#define LED_PIN 10
#define RED_BUTTON 7
#define GREEN_BUTTON 6
#define YELLOW_BUTTON 5

#define RED_B 1
#define GREEN_B 2
#define YELLOW_B 3

int R_B = 0;
int G_B = 0;
int Y_B = 0;
int R_C = 0;
int G_C = 0;
int Y_C = 0;
int R_start_t = 0;
int G_start_t = 0;
int Y_start_t = 0;
int J_start_t = 0;
bool J_move = false;

int inputByte[2] = {-1,-1};
int checkByte[2] = {-1, -1};
//int joyByte[2] = {-1,-1};

void setup() {
  pinMode(SW_pin, INPUT);
  digitalWrite(SW_pin, HIGH);
  
  pinMode(LED_PIN, OUTPUT);
  pinMode(RED_BUTTON, INPUT);
  pinMode(GREEN_BUTTON, INPUT);
  pinMode(YELLOW_BUTTON, INPUT);
  digitalWrite(LED_PIN, HIGH);
  
  Serial.begin(9600);

}

void loop() {
  if (digitalRead(RED_BUTTON)){
    if (R_B){
      int t = millis() - R_start_t;
      Serial.println(t);
      Serial.flush();
      R_B = 0;
      //digitalWrite(LED_PIN, LOW);
    } else if (R_C) {
      Serial.println("BP1");
      Serial.flush();
      delay(10);
      R_C = 0;
      //digitalWrite(LED_PIN, LOW);
    }
  }
  if (digitalRead(GREEN_BUTTON)){
    if (G_B){
      int t = millis() - G_start_t;
      Serial.println(t);
      Serial.flush();
      G_B = 0;
      //digitalWrite(LED_PIN, LOW);
    }
    if (G_C){
      Serial.println("BP2");
      Serial.flush();
      delay(10);
      G_C = 0;
      //digitalWrite(LED_PIN, LOW);
    }
  }
  if (digitalRead(YELLOW_BUTTON)){
    if (Y_B){
      int t = millis() - Y_start_t;
      Serial.println(t);
      Serial.flush();
      Y_B = 0;
      //digitalWrite(LED_PIN, LOW);
    }
    if (Y_C){
      Serial.println("BP3");
      Serial.flush();
      delay(10);
      Y_C = 0;
      //digitalWrite(LED_PIN, LOW);
    }
  }
  if (Serial.available() > 0){
    /*
    // Button Time
    while (Serial.peek() == 'B'){
      Serial.read();
      inputByte[0] = Serial.parseInt();
      if (inputByte[0] == RED_B){
        digitalWrite(LED_PIN, HIGH);
        R_start_t = millis();
        R_B = 1;
      } else if (inputByte[0] == GREEN_B){
        digitalWrite(LED_PIN, HIGH);
        G_start_t = millis();
        G_B = 1;        
      } else if (inputByte[0] == YELLOW_B){
        digitalWrite(LED_PIN, HIGH);
        Y_start_t = millis();
        Y_B = 1;        
      }
    }
    */
    // Button Check
    while (Serial.peek() == 'C'){
      Serial.read();
      checkByte[0] = Serial.parseInt();
      if (Serial.available() > 0){
        Serial.read(); 
      }
      if (checkByte[0] == RED_B){
        if (digitalRead(RED_BUTTON) == LOW){
          //digitalWrite(LED_PIN, HIGH);
          R_C = 1;
        }
      }
      if (checkByte[0] == GREEN_B){
        if (digitalRead(GREEN_BUTTON) == LOW){
          //digitalWrite(LED_PIN, HIGH);
          G_C = 1;
        }
      }
      if (checkByte[0] == YELLOW_B){
        if (digitalRead(YELLOW_BUTTON) == LOW){
          //digitalWrite(LED_PIN, HIGH);
          Y_C = 1;
        }
      }
      } if (checkByte[0] == 4) {
        R_C = 0;
        G_C = 0;
        Y_C = 0;
      }
    

    // Joystick
    while (Serial.peek() == 'J'){
      Serial.read();
      //joyByte[0] = Serial.parseInt();
      //J_start_t = millis();
      //while (millis() - J_start_t <= joyByte[0]){
        //digitalWrite(LED_PIN, HIGH);
        //if ((analogRead(X_pin) < 550 && analogRead(X_pin) > 450) && (analogRead(Y_pin) < 550 && analogRead(Y_pin) > 450)){
          // joystick in middle
          //J_move = false;
        //} else {
          //J_move = true;
        //}
        //if (J_move == true){
          Serial.println(analogRead(X_pin));
          Serial.flush();
          Serial.println(analogRead(Y_pin));
          Serial.flush();
          delay(10);
        //}
      //}
      //digitalWrite(LED_PIN, LOW);
      //Serial.println(-1);
      //Serial.flush();
    }
    /*
    while (Serial.peek() == 'L'){
      Serial.read();
      inputByte[0] = Serial.parseInt();
      if (inputByte[0] == 1){
        digitalWrite(LED_PIN, HIGH);
      }
      if (inputByte[0] == 2){
        digitalWrite(LED_PIN, LOW);
      }
      inputByte[0] = 0;
    }
    */
    while (Serial.available() > 0){
      Serial.read();
    }
  }
}
