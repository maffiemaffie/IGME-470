#include <LiquidCrystal.h>

byte sadLeftEye[8] = {
  0b00000,
  0b11111,
  0b11111,
  0b11111,
  0b11000,
  0b11000,
  0b11000,
  0b00000
};

byte sadRightEye[8] = {
  0b00000,
  0b11000,
  0b11000,
  0b11000,
  0b11111,
  0b11111,
  0b11111,
  0b00000
};

byte sadLeftFrown[8] = {
  0b01100,
  0b01100,
  0b01100,
  0b01100,
  0b01110,
  0b00110,
  0b00111,
  0b00011
};

byte sadRightFrown[8] = {
  0b00011,
  0b00111,
  0b00110,
  0b01110,
  0b01100,
  0b01100,
  0b01100,
  0b01100
};

byte angryLeftEyebrow[8] = {
  0b00001,
  0b00011,
  0b00011,
  0b00111,
  0b00110,
  0b01110,
  0b01100,
  0b01100
};

byte angryRightEyebrow[8] = {
  0b01100,
  0b01100,
  0b01110,
  0b00110,
  0b00111,
  0b00011,
  0b00011,
  0b00001
};

byte angryLeftEye[8] = {
  0b00000,
  0b11111,
  0b11111,
  0b11111,
  0b00000,
  0b00000,
  0b00000,
  0b00000
};

byte angryRightEye[8] = {
  0b00000,
  0b00000,
  0b00000,
  0b00000,
  0b11111,
  0b11111,
  0b11111,
  0b00000
};

byte angryFrown[8] = {
  0b01100,
  0b01100,
  0b01100,
  0b01100,
  0b01100,
  0b01100,
  0b01100,
  0b01100
};

byte happyLeftEye[8] = {
  0b00000,
  0b11111,
  0b11111,
  0b11111,
  0b11100,
  0b01110,
  0b00110,
  0b00000
};

byte happyRightEye[8] = {
  0b00000,
  0b00110,
  0b01110,
  0b11100,
  0b11111,
  0b11111,
  0b11111,
  0b00000
};

byte happyLeftSmile[8] = {
  0b00110,
  0b00110,
  0b00110,
  0b00110,
  0b01110,
  0b01100,
  0b11100,
  0b11000
};

byte happyRightSmile[8] = {
  0b11000,
  0b11100,
  0b01100,
  0b01110,
  0b00110,
  0b00110,
  0b00110,
  0b00110
};

void smile(LiquidCrystal lcd) {
  lcd.createChar(0, happyLeftEye);
  lcd.createChar(1, happyRightEye);
  lcd.createChar(2, happyLeftSmile);
  lcd.createChar(3, happyRightSmile);

  lcd.clear();

  lcd.setCursor(1, 0);
  lcd.write(1);
  lcd.write(3);

  lcd.setCursor(1, 1);
  lcd.write(byte(0));
  lcd.write(2);
}

void frown(LiquidCrystal lcd) {
  lcd.createChar(0, sadLeftEye);
  lcd.createChar(1, sadRightEye);
  lcd.createChar(2, sadLeftFrown);
  lcd.createChar(3, sadRightFrown);
  
  lcd.clear();

  lcd.setCursor(1, 0);
  lcd.write(1);
  lcd.write(3);

  lcd.setCursor(1, 1);
  lcd.write(byte(0));
  lcd.write(2);
}

void scowl(LiquidCrystal lcd) {
  lcd.createChar(0, angryLeftEyebrow);
  lcd.createChar(1, angryRightEyebrow);
  lcd.createChar(2, angryLeftEye);
  lcd.createChar(3, angryRightEye);
  lcd.createChar(4, angryFrown);

  lcd.clear();

  lcd.setCursor(0, 0);
  lcd.write(1);
  lcd.write(3);
  lcd.write(4);

  lcd.setCursor(0, 1);
  lcd.write(byte(0));
  lcd.write(2);
  lcd.write(4);
}