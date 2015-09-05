#include "LPD8806.h"
#include "SPI.h"

// Example to control LPD8806-based RGB LED Modules in a strip

/*****************************************************************************/

// Chose 2 pins for output; can be any valid output pins:
int dataPin2  = 5; //PWM
int clockPin2 = 8;
int dataPin1  = 11; //PWM
int clockPin1 = 3;

// First parameter is the number of LEDs in the strand.  The LED strips
// are 32 LEDs per meter but you can extend or cut the strip.  Next two
// parameters are SPI data and clock pins:
LPD8806 strip1 = LPD8806(48, dataPin1, clockPin1);
LPD8806 strip2 = LPD8806(32, dataPin2, clockPin2);

int command = 0;
int stripSelect = 0;
int pos = 0;
int red = 0;
int green = 0;
int blue = 0;

void setup() {
  // Start up the LED strip
  Serial.begin(19200);
  strip1.begin();
  // Update the strip, to start they are all 'off'
  strip1.show();
  
  strip2.begin();
  // Update the strip, to start they are all 'off'
  strip2.show();
}

void SpeedTest()
{
  while(true)
  {
    Serial.print("dsd\n");
    for(int i = 0; i < 32; i++)
    {
      strip1.setPixelColor(i, strip1.Color(0,127,0));
      strip1.show();
    }
    for(int i = 0; i < 32; i++)
    {
      strip2.setPixelColor(i, strip2.Color(127,0,0));
      strip2.show();
    }
  }
}


void loop() {
  
  //SpeedTest();
  
  while (Serial.available() > 0) {     
    //char incomingByte = Serial.read();
    char bytes[4] = {0};
    Serial.readBytes(bytes, 4);
    Serial.write("\nStart\n1: ");   
    byte cmd_byte = byte(bytes[0]);    
    Serial.print(cmd_byte, BIN);
    
    int cmd = bitRead(cmd_byte, 7);
    int strip = bitRead(cmd_byte, 6);
    int pos = cmd_byte;    
    if(cmd == 1)
     pos = pos - 128;     
    if(strip == 1)
     pos = pos - 64;
    
    Serial.print("\nCmd: ");
    Serial.print(cmd);
    
    Serial.print("\nstrip: ");
    Serial.print(strip);
    
    Serial.print("\npos: ");
    Serial.print(pos);
   
    Serial.write("\n2: "); 
    Serial.print(bytes[1], BIN);
    int red = bytes[1];
    
    Serial.write("\n3: ");
    Serial.print(bytes[2], BIN);
    int green = bytes[2];
    
    Serial.write("\n4: ");
    Serial.print(bytes[3], BIN);
    int blue = bytes[3];
    
    if(cmd==0)
    {
      //Serial.print("\n LIGHT");
      if(strip == 0)
      {  
        //Serial.print(" STRIP 1");        
        strip1.setPixelColor(pos, strip1.Color(red,green,blue));
      }
      else
      {
        Serial.print(" STRIP 2");        
        strip2.setPixelColor(pos, strip2.Color(red,green,blue));
      }
    }
    else
    {
      Serial.print("\n SHOW");
      if(strip == 0)
      {  
        Serial.print(" STRIP 1");
        strip1.show();
      }
      else
      {
        Serial.print(" STRIP 2");       
        strip2.show();
      }
    }
    
    
  }

}

// 1 bit  [0] strip select
//  6 bits [1 -   6] position
// 7 bits [7 -  13] r
// 7 bits [14 - 20] g
// 7 bits [21 - 27] b
// total = 28



