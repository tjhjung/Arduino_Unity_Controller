#include "SerialCommand.h"
#include <SoftwareSerial.h>


#define SW_pin 2
#define X_pin 1
#define Y_pin 2

#define LED_PIN 10
#define RED_BUTTON 7
#define GREEN_BUTTON 6
#define YELLOW_BUTTON 5

SerialCommand sCmd;

void setup() {
  pinMode(SW_pin, INPUT);
  digitalWrite(SW_pin, HIGH);
  
  pinMode(LED_PIN, OUTPUT);
  pinMode(RED_BUTTON, INPUT);
  pinMode(GREEN_BUTTON, INPUT);
  pinMode(YELLOW_BUTTON, INPUT);
  digitalWrite(LED_PIN, HIGH);
  
  Serial.begin(9600);
  while (!Serial);

  sCmd.addCommand("PING", pingHandler);
  sCmd.addCommand("ECHO", echoHandler);
  //sCmd.setDefaultHandler(errorHandler);
}
int R_B = 0;
int G_B = 0;
int Y_B = 0;
int start_t = 0;
void loop() {
  if (R_B == 0 && digitalRead(RED_BUTTON)){
    R_B = 1;
  }
    if (G_B == 0 && digitalRead(GREEN_BUTTON)){
    G_B = 1;
  }
    if (Y_B == 0 && digitalRead(YELLOW_BUTTON)){
    Y_B = 1;
  }
  int end_t = millis();
  if (end_t - start_t >= 1000){
    Serial.println("R_B");
    Serial.println(R_B);
    Serial.println("G_B");
    Serial.println(G_B);
    Serial.println("Y_B");
    Serial.println(Y_B);
    Serial.println("X");
    Serial.println(analogRead(X_pin));
    Serial.println("Y");
    Serial.println(analogRead(Y_pin));
    R_B = 0;
    G_B = 0;
    Y_B = 0;
    start_t = end_t;
  }
  if (Serial.available() > 0)
    sCmd.readSerial();
 
}

void pingHandler ()
{
  Serial.println("PONG");
}

void echoHandler ()
{
  char *arg;
  arg = sCmd.next();
  if (arg != NULL)
    Serial.println(arg);
  else
    Serial.println("nothing to echo");
}


int errorHandler (const char *command)
{
  return 0;
}
