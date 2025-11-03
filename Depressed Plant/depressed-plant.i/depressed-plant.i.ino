#include <LiquidCrystal.h>

int ledPins[] = { 5, 6, 10, 11 };
int buttonPin = 2;
int sensorPin = A0;

const int sensorLow = 415;
const int sensorHigh = 80;

const int happyMax = 40;
const int heaterMin = 60;


bool isHeating = false;
const long heaterCooldown = 300000; // 5 * 60 * 1000
long cooldownStartedAt = -1 * heaterCooldown;

const int rs = 3, en = 7, d4 = 8, d5 = 9, d6 = 12, d7 = 13;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

void setup() {
  for (int pin : ledPins) {
    pinMode(pin, OUTPUT);
    analogWrite(pin, 255);
  }

  pinMode(buttonPin, INPUT);
  pinMode(sensorPin, INPUT);

  Serial.begin(9600);
  lcd.begin(16, 2);

  frown(lcd);
}

void loop() {
  int moisture = map(analogRead(sensorPin), sensorLow, sensorHigh, 0, 100);

  // turn heater leds on and off
  if (!isHeating) {
    shutOffHeater();
  } else {
    updateHeater();
  }

  // during cooldown
  if (millis() - cooldownStartedAt < heaterCooldown) {
    // Serial.println("Cooldown activated... pissed off");
    scowl(lcd);
    int secondsLeftOnCooldown = (heaterCooldown - (millis() - cooldownStartedAt)) / 1000;
    // Serial.print("Still on cooldown for ");
    // Serial.print(secondsLeftOnCooldown);
    // Serial.println(" seconds");
  }

  // not on cooldown
  if (millis() - cooldownStartedAt > heaterCooldown) {
    if (moisture > happyMax) {
      // Serial.println("Moisture exceeded happy levels... not happy");
      frown(lcd);
    } else {
      // Serial.println("Currently dying... am happy");
      smile(lcd);
    }

    if (moisture > heaterMin) {
      // Serial.println("Very moist, turning on heater");
      isHeating = true;
    } else {
      // Serial.println("Dying again, shutting heater off");
      isHeating = false;
    }
  }

  if (digitalRead(buttonPin) == HIGH) {
    // Serial.println("shutting off heater...");
    isHeating = false;
    cooldownStartedAt = millis();
    scowl(lcd);
  }

  // Serial.println(moisture);
}

void updateHeater() {
  for (int pin : ledPins) {
    analogWrite(pin, random(0, 100));
  }
}

void shutOffHeater() {
  for (int pin : ledPins) {
    analogWrite(pin, 0);
    delay(50);
  }
}
