/*
  Arduino LSM6DS3 - Simple Accelerometer

  This example reads the acceleration values from the LSM6DS3
  sensor and continuously prints them to the Serial Monitor
  or Serial Plotter.

  The circuit:
  - Arduino Uno WiFi Rev 2 or Arduino Nano 33 IoT

  created 10 Jul 2019
  by Riccardo Rizzo

  This example code is in the public domain.

  edited 07 Nov 2025
  by Elia Cohen
*/

#include <Arduino_LSM6DS3.h>

float last = 0;
float increasing = false;

float shakeThresholds[] = { 0.5, 2, 3.5, 4.0 };

const int buzzerPin = 10;

void setup() {
  pinMode(buzzerPin, OUTPUT);
  startCountdown();

  Serial.begin(9600);
  while (!Serial)
    ;

  if (!IMU.begin()) {
    Serial.println("Failed to initialize IMU!");

    while (1)
      ;
  }
}

void loop() {
  if (Serial.available() > 0) {
    int incomingByte = Serial.read();

    startCountdown();
  }

  float x, y, z;

  float totalAbsoluteAcceleration = 0;

  if (IMU.accelerationAvailable()) {
    IMU.readAcceleration(x, y, z);

    bool wasIncreasing = increasing;

    if (x >= last) {
      increasing = true;
    } else {
      increasing = false;
    }

    last = x;
    // delay(10);

    if (!wasIncreasing && increasing) {
      float shakeIntensity = abs(x);

      if (shakeIntensity > shakeThresholds[2]) {
        Serial.println(lerp(shakeIntensity, shakeThresholds[2], shakeThresholds[3], 2, 3));
        return;
      }

      if (shakeIntensity > shakeThresholds[1]) {
        Serial.println(lerp(shakeIntensity, shakeThresholds[1], shakeThresholds[2], 1, 2));
        return;
      }

      if (shakeIntensity > shakeThresholds[0]) {
        Serial.println(lerp(shakeIntensity, shakeThresholds[0], shakeThresholds[1], 0, 1));
        return;
      }

      Serial.println(0.0);
    }
  }
}

float lerp(float value, float inputMinimum, float inputMaximum, float outputMinimum, float outputMaximum) {
  float ratio = (value - inputMinimum) / (inputMaximum - inputMinimum);
  return ratio * (outputMaximum - outputMinimum) + outputMinimum;
}

void startCountdown() {
  tone(buzzerPin, 440, 500);
  delay(1000);
  tone(buzzerPin, 440, 500);
  delay(1000);
  tone(buzzerPin, 440, 500);
  delay(1000);
  tone(buzzerPin, 660, 500);
}
