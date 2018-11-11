// Code Assumes there's 3 lights but can be modified to match any number of lights

// Number of lights in the system
#define NUM_LIGHTS 3

// Pin numbers that is connected to the Red pin, Green pin, and Blue pin
// ex) Pin1[3] = {3,4,5}; 3 is red pin, 4 is green pin, 5 is blue pin
// Assume pin numbers for sake of example but can be changed to suit actual setup
#define PINa {2,3,4}   // Change Numbers here
#define PINb {5,6,7}   // Change Numbers here
#define PINc {8,9,10}    // Change Numbers here


// Values for the colors
// Set as {red value, green value, blue value}
// Ex) {255,0,0} for red, {0, 255, 0} for green, and {0, 0, 255} for blue
// Assumes colors for the sake of example but can be changed to suit actual setup
#define COLORa {255, 0, 0}   // Change Numbers here
#define COLORb {0, 255, 0}   // Change Numbers here
#define COLORc {0, 0, 255}   // Change Numbers here


// Orgainized pins
int Pins[NUM_LIGHTS][3] = {PINa, PINb, PINc};

// Organized Colors
int Colors[NUM_LIGHTS][3] = {COLORa, COLORb, COLORc};




// Sets the color for the light
// Input: arrary of the pin to be set, array of the color to be set to
void LEDLight(int P[NUM_LIGHTS][3], int C[NUM_LIGHTS][3]){
  for (int i = 0; i < NUM_LIGHTS; i++){
    analogWrite(P[i][0], C[i][0]);
    analogWrite(P[i][1], C[i][1]);
    analogWrite(P[i][2], C[i][2]);
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
      swap(P, i, max_index);
    }
  }
  LEDLight(P, C);
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
  SetRandomLight(Pins, Colors);
  delay(1000);
}
