bool timeSet = false;
bool settingTime = false;

void setup() {
  // put your setup code here, to run once:
  initializeSevenSegment();
  initializeTrackers();
  initializeUltrasonic();

  doStartupSequence();
  // Serial.begin(9600);
}

void loop() {
  checkSensors();
  showTimes();

  if (timeSet) {
    checkMeds();
  }
}
