const int t1_redLed = 12;
const int t1_greenLed = 13;
const int t1_timeKnob = A0;

const int t2_redLed = 7;
const int t2_greenLed = 8;
const int t2_timeKnob = A1;

int t1_time = 0;
int t2_time = 0;

int timeStarted = 0;
int millisStarted = 0;

bool t1_medTaken = false;
bool t2_medTaken = false;

float timeLastSet = -9999;

void initializeTrackers() {
  pinMode(t1_redLed, OUTPUT);
  pinMode(t1_greenLed, OUTPUT);
  pinMode(t1_timeKnob, INPUT);

  pinMode(t2_redLed, OUTPUT);
  pinMode(t2_greenLed, OUTPUT);
  pinMode(t2_timeKnob, INPUT);

  t1_time = getGranularTimeFromPotentiometer(t1_timeKnob);
  t2_time = getTimeFromPotentiometer(t2_timeKnob);
}

void waitForTimeSet() {
  if (millis() % 2000 > 1000) {
    writeWord("SET     ");
  } else {
    writeWord("TIME    ");
  }
}
void checkTimeSetKnobs() {
  int t1_newTime = 0;
  int t2_newTime = 0;

  if (!timeSet) {
    t1_newTime = getGranularTimeFromPotentiometer(t1_timeKnob);
    t2_newTime = getGranularTimeFromPotentiometer(t2_timeKnob);
  } else {
    t1_newTime = getTimeFromPotentiometer(t1_timeKnob);
    t2_newTime = getTimeFromPotentiometer(t2_timeKnob);
  }

  if (abs(t1_time - t1_newTime) > 2) {
    timeLastSet = millis();
    if (!timeSet && millis() > 2000) {
      settingTime = true;
    }
  }

  if (abs(t2_time - t2_newTime) > 2) {
    timeLastSet = millis();
  }

  t1_time = t1_newTime;
  t2_time = t2_newTime;
}

void showTimes() {
  checkTimeSetKnobs();

  if (!timeSet) {
    if (millis() - timeLastSet < 5000) {
      writeNumber(parseTimeString(t1_time) + "    ");
    } else {
      if (settingTime) {
        timeSet = true;
        timeStarted = t1_time;
        millisStarted = millis();
      }
      waitForTimeSet();
    }
    return;
  }

  if (millis() - timeLastSet < 1000) {
    writeNumber(parseTimeString(t1_time) + parseTimeString(t2_time));
  }
}

int getTimeFromPotentiometer(int sensor) {
  return round(map(analogRead(sensor), 0, 1024, 0, 24 * 4)) * 15;
}

int getGranularTimeFromPotentiometer(int sensor) {
  return round(map(analogRead(sensor), 0, 1024, 0, 24 * 60));
}

String parseTimeString(int time) {
  int hours = time / 60;
  int minutes = time % 60;

  String parsedHours = String(hours);
  if (parsedHours.length() == 1) {
    parsedHours = String(0) + parsedHours;
  }

  String parsedMinutes = String(minutes);
  if (parsedMinutes.length() == 1) {
    parsedMinutes = String(0) + parsedMinutes;
  }

  return parsedHours + parsedMinutes;
}

void blinkLed(int ledPin) {
  if (millis() % 1000 > 500) {
    digitalWrite(ledPin, HIGH);
  } else {
    digitalWrite(ledPin, LOW);
  }
}

void checkMeds() {
  int currentTime = ((millis() - millisStarted) / 1000 / 60 + timeStarted) % 1440;
  if (millis() - timeLastSet > 1500)
    writeNumber(parseTimeString(currentTime) + parseTimeString(currentTime));

  // check med 1
  if (currentTime >= t1_time && !t1_medTaken) {
    blinkLed(t1_redLed);
    digitalWrite(t1_greenLed, LOW);
  } else {
    digitalWrite(t1_redLed, LOW);
    digitalWrite(t1_greenLed, HIGH);
  }

  if (currentTime == 0) {
    t1_medTaken = false;
    t2_medTaken = false;
  }

  // check med 2
  if (currentTime >= t2_time && !t2_medTaken) {
    blinkLed(t2_redLed);
    digitalWrite(t2_greenLed, LOW);
  } else {
    digitalWrite(t2_redLed, LOW);
    digitalWrite(t2_greenLed, HIGH);
  }
}