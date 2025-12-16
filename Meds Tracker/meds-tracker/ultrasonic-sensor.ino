/*
 * HC-SR04 example sketch
 *
 * https://create.arduino.cc/projecthub/Isaac100/getting-started-with-the-hc-sr04-ultrasonic-sensor-036380
 *
 * by Isaac100
 *
 * modified by Elia Cohen
 */

const int t1_trigPin = 11;
const int t1_echoPin = 10;

const int t2_trigPin = 9;
const int t2_echoPin = 6;

float t1_duration, t1_distance;
float t2_duration, t2_distance;

bool t1_readyToCheck = true;
bool t2_readyToCheck = false;
int lastChecked = 0;

void initializeUltrasonic() {
  pinMode(t1_trigPin, OUTPUT);
  pinMode(t1_echoPin, INPUT);

  pinMode(t2_trigPin, OUTPUT);
  pinMode(t2_echoPin, INPUT);
}

void checkSensors() {
  if (millis() - lastChecked < 500) return;

  if (t1_readyToCheck) {
    digitalWrite(t1_trigPin, LOW);
    delayMicroseconds(2);
    digitalWrite(t1_trigPin, HIGH);
    delayMicroseconds(10);
    digitalWrite(t1_trigPin, LOW);

    t1_duration = pulseIn(t1_echoPin, HIGH);
    t1_distance = (t1_duration * .0343) / 2;
    if (t1_distance > 4) {
      t1_medTaken = true;
    }

    t1_readyToCheck = false;
    t2_readyToCheck = true;
    lastChecked = millis();
    
    return;
  }

  if (t2_readyToCheck) {
    digitalWrite(t2_trigPin, LOW);
    delayMicroseconds(2);
    digitalWrite(t2_trigPin, HIGH);
    delayMicroseconds(10);
    digitalWrite(t2_trigPin, LOW);

    t2_duration = pulseIn(t2_echoPin, HIGH);
    t2_distance = (t2_duration * .0343) / 2;
    if (t2_distance > 4) {
      t2_medTaken = true;
    }

    t2_readyToCheck = false;
    t1_readyToCheck = true;
    lastChecked = millis();
    
    return;
  }
}