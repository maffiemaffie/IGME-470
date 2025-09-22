int sensorPin = A0;
int piezoPin = 8;

int sensorValue;
int sensorLow = 1023;
int sensorHigh = 0;
const int ledPin = 13;

int pastValues[30] = { };

void setup() {
  pinMode(ledPin, OUTPUT);
  pinMode(piezoPin, OUTPUT);
  pinMode(sensorPin, INPUT);

  // calibrating...
  digitalWrite(ledPin, HIGH);
  while (millis() < 5000) {
    sensorValue = analogRead(sensorPin);
    if (sensorValue > sensorHigh) {
      sensorHigh = sensorValue;
    }
    if (sensorValue < sensorLow) {
      sensorLow = sensorValue;
    }
  }
  digitalWrite(ledPin, LOW);
}

void loop() {
  sensorValue = analogRead(sensorPin);

  for (int i = 1; i < 30; i++) {
    pastValues[i - 1] = pastValues[i];
  }

  pastValues[29] = sensorValue;

  int average = 0;
  for (int value : pastValues) {
    average += value;
  }
  average /= 30;

  int pitch =
    map(average, sensorLow, sensorHigh, 50, 1760);
  tone(piezoPin, pitch, 20);
  delay(10);
}