const int SW_pin = 2;
const int X_pin = 1;
const int Y_pin = 2;

#define MERCY 100
#define MAX_ARR 100
int joystick[MAX_ARR][2];
int joystick_count = 0;
int start = 0;

int curve(int joystick[MAX_ARR][2]){
  int check[9][2] = {{0,500},{0,300},{0,0},{300,0},{500,0},{800,0},{1000,0},{1000,300},{1000,500}};
  int check_count = 0;
  int x_check = 0;
  int y_check = 0;
  for (int i = 0; i < MAX_ARR; i++){
    if (joystick[i][0] >= check[check_count][0] - MERCY && joystick[i][0] <= check[check_count][0] + MERCY){
      x_check = 1;
    }
    if (joystick[i][1] >= check[check_count][1] - MERCY && joystick[i][1] <= check[check_count][1] + MERCY){
      y_check = 1;
    }
    if (x_check == 1 && y_check == 1 && check_count < 9){
      check_count++;
    }
    x_check = 0;
    y_check = 0;
  }
  if (check_count >= 9){
    Serial.println("YOU DID IT");
  } else {
    Serial.println("YOU FAILED");
  }
  Serial.println("NEW GAME");
  
}

void printarr(){
  for (int i = 0; i < MAX_ARR; i++){
    Serial.print("X: ");
    Serial.print(joystick[i][0]);
    Serial.print(" Y: ");
    Serial.println(joystick[i][1]);
  }
}

void setup() {
  pinMode(SW_pin, INPUT);
  digitalWrite(SW_pin, HIGH);
  Serial.begin(115200);
  Serial.print("Curve Clockwise Down!");
}

void loop() {
  if (digitalRead(SW_pin) == 0){
    start = 1;
    Serial.println("\n");
    delay(500);
  }
  if (start == 1 && joystick_count < MAX_ARR){
    joystick[joystick_count][0] = analogRead(X_pin);
    joystick[joystick_count][1] = analogRead(Y_pin);
    joystick_count++;
    delay(6);
  }
  if (joystick_count == MAX_ARR){
    curve(joystick);
    joystick_count = 0;
    start = 0;
    printarr();
  }
  
  /*
  Serial.print("Switch: ");
  Serial.print(digitalRead(SW_pin));
  Serial.print("\n");
  Serial.print("X-axis: ");
  Serial.print(analogRead(X_pin));
  Serial.print("\n");
  Serial.print("Y-axis: ");
  Serial.println(analogRead(Y_pin));
  Serial.print("\n\n");
  delay(500);
  */
}
