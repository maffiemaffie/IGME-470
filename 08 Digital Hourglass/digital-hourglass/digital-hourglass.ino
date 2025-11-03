int ledPins[] = { 7, 6, 5, 4, 3, 2 };
int sensorPin = 8;
int led = 2;

unsigned long previousTime = 0;
int switchState = 0;
int prevSwitchState = 0;
long interval = 1000;

void setup() {
  for (int pin : ledPins) {
    pinMode(pin, OUTPUT);
  }

  pinMode(sensorPin, INPUT);

  Serial.begin(9600);
}

void loop() {
  unsigned long currentTime = millis();
  if (currentTime - previousTime > interval) {
    previousTime = currentTime;
    digitalWrite(led, HIGH);
    led++;
    if (led == 7) {
      for (int i = 0; i < 10; i++) {
        for (int pin : ledPins) {
          digitalWrite(pin, LOW);
          delay(25);
        }

        for (int pin : ledPins) {
          digitalWrite(pin, HIGH);
          delay(25);
        }
      }
    }
  }

  switchState = digitalRead(sensorPin);
  if (switchState != prevSwitchState) {
    for (int pin : ledPins) {
      digitalWrite(pin, LOW);
    }
    led = 2;
    previousTime = currentTime;
  }
  prevSwitchState = switchState;
}
