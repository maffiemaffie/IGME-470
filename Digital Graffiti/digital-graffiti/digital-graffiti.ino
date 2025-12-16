const int sensorPin = A1;
const int bluePin = 10;
const int greenPin = 11;

// button not pressed
int sensorMin = 1024;
// button pressed all the way down
int sensorMax = 0;

void setup() {
  pinMode(sensorPin, INPUT);
  pinMode(bluePin, OUTPUT);
  pinMode(greenPin, OUTPUT);
  Serial.begin(9600);
}

void loop() {
  int sensorValue = analogRead(sensorPin);

  // calibration!
  if (millis() < 5000) {
    // blinking light to indicate we're calibrating
    if (millis() % 1000 > 500) {
      analogWrite(greenPin, 64);
      analogWrite(bluePin, 0);
    } else {
      analogWrite(bluePin, 64);
      analogWrite(greenPin, 0);
    }

    // calibrate sensor min and sensor max
    if (sensorValue > sensorMax) sensorMax = sensorValue;
    if (sensorValue < sensorMin) sensorMin = sensorValue;
    Serial.println(sensorValue);
    return;
  }

  int normalizedSensorValue = map(sensorValue, sensorMin, sensorMax, 0, 255);

  normalizedSensorValue = max(normalizedSensorValue, 10);
  normalizedSensorValue = min(normalizedSensorValue, 245);

  // Serial.println(normalizedSensorValue);

  analogWrite(greenPin, 245 - normalizedSensorValue);
  analogWrite(bluePin, normalizedSensorValue);
}