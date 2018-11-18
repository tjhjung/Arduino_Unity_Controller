// Code Assumes there's 3 lights but can be modified to match any number of lights

// Number of lights in the system
#define NUM_LIGHTS 1

// Pin numbers that is connected to the Red pin, Green pin, and Blue pin
// ex) Pin1[3] = {3,4,5}; 3 is red pin, 4 is green pin, 5 is blue pin
// Assume pin numbers for sake of example but can be changed to suit actual setup
#define PINa {10,9,8}   // Change Numbers here
#define PINb {5,6,7}   // Change Numbers here
#define PINc {8,9,10}    // Change Numbers here


// Values for the colors
// Set as {red value, green value, blue value}
// Ex) {255,0,0} for red, {0, 255, 0} for green, and {0, 0, 255} for blue
// Assumes colors for the sake of example but can be changed to suit actual setup
#define COLORr {225,0,0}   // Change Numbers here
#define COLORg {0, 255, 0}   // Change Numbers here
#define COLORb {0, 0, 255}   // Change Numbers here


// Orgainized pins
int Pins[NUM_LIGHTS][3] = {PINa};

// Organized Color
int Colors[3][3] = {COLORr,COLORg,COLORb};



void PinLights(int pin[], int light[]){
  analogWrite(pin[0], light[0]);
  analogWrite(pin[1], light[1]);
  analogWrite(pin[2], light[2]);
}
// Sets the color for the light
// Input: arrary of the pin to be set, array of the color to be set to
void LEDLight(int P[NUM_LIGHTS][3], int C[3][3]){
  for (int i = 0; i < NUM_LIGHTS; i++){
    PinLights(P[i], C[i]);
  }
}

void swap(int arr[NUM_LIGHTS][3], int a, int b){
  if (a != b){
    int temp;
    for(int i = 0; i < NUM_LIGHTS; i++){
      for(int j = 0; j < 3; j++){
        temp = arr[a][j];
        arr[a][j] = arr[b][j];
        arr[b][j] = temp;
      }
    }
  }
}

void SetRandomLight(int P[NUM_LIGHTS][3] , int C[NUM_LIGHTS][3]){
  int RandomOrder[NUM_LIGHTS];
  // Picks random numbers to decide orders
  for (int i = 0; i < NUM_LIGHTS; i++){
    RandomOrder[i] = random(1000);
  }

  for (int i = 0; i < NUM_LIGHTS; i++){
    int max_index = i;
    for (int j = 1; j < 3; j++){
      if (RandomOrder[j] > RandomOrder[max_index]){
        max_index = j;
      }
    }
    if (max_index != i){
      int temp = RandomOrder[i];
      RandomOrder[i] = RandomOrder[max_index];
      RandomOrder[max_index] = temp;
      swap(C, i, max_index);
    }
  }
  LEDLight(P, C);
}

void PickRandomLight(int P[NUM_LIGHTS][3], int C[NUM_LIGHTS][3]){
  for (int i = 0; i < 3; i++){
    C[0][i] = random(255);
  }
  LEDLight(P,C);
}

void PickRGB(int P[NUM_LIGHTS][3], int C[3][3]){
  int choose = random(1000);
  if (choose <= 333){
    PinLights(P[0],C[0]);
  } else if (choose <= 666){
    PinLights(P[0],C[1]);
  } else {
    PinLights(P[0], C[2]);
  }
}

void setup() {
  // Sets random number generator

  // Sets up the pins for the LED lights
  randomSeed(analogRead(0));
  for (int i = 0; i < NUM_LIGHTS; i++){
    for (int j = 0; j < 3; j++){
      pinMode(Pins[i][j], OUTPUT);
    }
  }
}

void loop() {
  //PickRandomLight(Pins, Colors);
  //SetRandomLight(Pins, Colors);
  PickRGB(Pins,Colors);
  delay(1000);
}
