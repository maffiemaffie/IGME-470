const int digitClockPin = 4;
const int digitSerialPin = 5;

const int segmentClockPin = 3;
const int segmentSerialPin = 1;
const int segmentDisplayEnablePin = 2;

//   A
// B   F
//   G
// C   E
//   D   .
const int digits[11][8] = {
  //G  F  E  D  C  B  A  .
  { 0, 1, 1, 1, 1, 1, 1, 0 },  // 0
  { 0, 0, 0, 0, 1, 1, 0, 0 },  // 1
  { 1, 0, 1, 1, 0, 1, 1, 0 },  // 2
  { 1, 0, 0, 1, 1, 1, 1, 0 },  // 3
  { 1, 1, 0, 0, 1, 1, 0, 0 },  // 4
  { 1, 1, 0, 1, 1, 0, 1, 0 },  // 5
  { 1, 1, 1, 1, 1, 0, 1, 0 },  // 6
  { 0, 0, 0, 0, 1, 1, 1, 0 },  // 7
  { 1, 1, 1, 1, 1, 1, 1, 0 },  // 8
  { 1, 1, 0, 0, 1, 1, 1, 0 },  // 9
  { 0, 0, 0, 0, 0, 0, 0, 0 }   // no digit
};

const int letters[6][8] = {
  //G  F  E  D  C  B  A  .
  { 1, 1, 1, 1, 0, 0, 0, 0 },  // T
  { 0, 1, 1, 0, 0, 0, 0, 0 },  // I
  { 0, 0, 1, 0, 1, 0, 1, 0 },  // M
  { 1, 1, 1, 1, 0, 0, 1, 0 },  // E
  { 1, 1, 0, 1, 1, 0, 1, 0 },  // S
  { 0, 0, 0, 0, 0, 0, 0, 0 }   // no letter
};

void initializeSevenSegment() {
  // put your setup code here, to run once:
  pinMode(digitClockPin, OUTPUT);
  pinMode(digitSerialPin, OUTPUT);

  pinMode(segmentClockPin, OUTPUT);
  pinMode(segmentSerialPin, OUTPUT);
  pinMode(segmentDisplayEnablePin, OUTPUT);

  for (int i = 0; i < 8; i++) {
    digitalWrite(digitClockPin, LOW);
    delay(0);
    digitalWrite(digitSerialPin, HIGH);
    delay(0);
    digitalWrite(digitClockPin, HIGH);
    delay(0);
  }
}

void writeWord(String word) {
  digitalWrite(digitClockPin, LOW);
  delay(0);
  digitalWrite(digitSerialPin, LOW);
  delay(0);
  digitalWrite(digitClockPin, HIGH);
  delay(0);

  for (int i = 0; i < 8; i++) {
    digitalWrite(digitClockPin, LOW);
    delay(0);
    digitalWrite(digitSerialPin, HIGH);
    delay(0);
    digitalWrite(digitClockPin, HIGH);
    delay(0);

    int charCode = word[i] - 'A';
    int letterIndex;

    switch (charCode) {
      case 19:
        letterIndex = 0;
        break;
      case 8:
        letterIndex = 1;
        break;
      case 12:
        letterIndex = 2;
        break;
      case 4:
        letterIndex = 3;
        break;
      case 18:
        letterIndex = 4;
        break;
      default:
        letterIndex = 5;
    }

    loadLetter(letterIndex);
    delay(2);

    digitalWrite(segmentDisplayEnablePin, HIGH);
  }
}

void writeNumber(String number) {
  digitalWrite(digitClockPin, LOW);
  delay(0);
  digitalWrite(digitSerialPin, LOW);
  delay(0);
  digitalWrite(digitClockPin, HIGH);
  delay(0);

  for (int i = 0; i < 8; i++) {
    digitalWrite(digitClockPin, LOW);
    delay(0);
    digitalWrite(digitSerialPin, HIGH);
    delay(0);
    digitalWrite(digitClockPin, HIGH);
    delay(0);

    int digit = number[i] - '0';

    if ((i % 4) == 0 && digit == 0) {
      loadDigit(10);
    } else {
      loadDigit(digit);
    }

    delay(2);

    digitalWrite(segmentDisplayEnablePin, HIGH);
  }
}

// load each segment into register, then enable display
void loadDigit(int digit) {
  digitalWrite(segmentDisplayEnablePin, HIGH);

  for (int segment : digits[digit]) {
    if (segment == 1) {
      loadSegmentRegisterValue(HIGH);  // enable segment
    } else {
      loadSegmentRegisterValue(LOW);  // disable segment
    }
  }

  // push out last segment
  loadSegmentRegisterValue(LOW);

  digitalWrite(segmentDisplayEnablePin, LOW);
}

// load each segment into register, then enable display
void loadLetter(int letterIndex) {
  digitalWrite(segmentDisplayEnablePin, HIGH);

  for (int segment : letters[letterIndex]) {
    if (segment == 1) {
      loadSegmentRegisterValue(HIGH);  // enable segment
    } else {
      loadSegmentRegisterValue(LOW);  // disable segment
    }
  }

  // push out last segment
  loadSegmentRegisterValue(LOW);

  digitalWrite(segmentDisplayEnablePin, LOW);
}

void loadSegmentRegisterValue(int value) {
  if (!(value == HIGH || value == LOW)) {
    return;
  }

  digitalWrite(segmentClockPin, LOW);
  digitalWrite(segmentSerialPin, value);
  digitalWrite(segmentClockPin, HIGH);
}

void doStartupSequence() {
  // flush registers
  for (int i = 0; i < 9; i++) {
    digitalWrite(digitClockPin, LOW);
    digitalWrite(digitSerialPin, HIGH);
    digitalWrite(digitClockPin, HIGH);

    digitalWrite(segmentClockPin, LOW);
    digitalWrite(segmentSerialPin, LOW);
    digitalWrite(segmentClockPin, HIGH);
  }

  // load 1 digit into the digit clock
  digitalWrite(digitClockPin, LOW);
  digitalWrite(digitSerialPin, LOW);
  digitalWrite(digitClockPin, HIGH);

  for (int digit = 0; digit < 8; digit++) {
    // push bit forward
    digitalWrite(digitClockPin, LOW);
    digitalWrite(digitSerialPin, HIGH);
    digitalWrite(digitClockPin, HIGH);

    // load 1 segment into the segment clock
    digitalWrite(segmentClockPin, LOW);
    digitalWrite(segmentSerialPin, HIGH);
    digitalWrite(segmentClockPin, HIGH);

    // enable display
    digitalWrite(segmentDisplayEnablePin, LOW);

    for (int segment = 0; segment < 7; segment++) {
      // push bit forward
      digitalWrite(segmentClockPin, LOW);
      digitalWrite(segmentSerialPin, LOW);
      digitalWrite(segmentClockPin, HIGH);
      delay(50);
    }

    // disable display
    digitalWrite(segmentDisplayEnablePin, LOW);

    // flush segment register
    digitalWrite(segmentClockPin, LOW);
    digitalWrite(segmentSerialPin, LOW);
    digitalWrite(segmentClockPin, HIGH);
  }

  // flush both registers
  for (int i = 0; i < 9; i++) {
    digitalWrite(digitClockPin, LOW);
    digitalWrite(digitSerialPin, HIGH);
    digitalWrite(digitClockPin, HIGH);

    digitalWrite(segmentClockPin, LOW);
    digitalWrite(segmentSerialPin, LOW);
    digitalWrite(segmentClockPin, HIGH);
  }
}
