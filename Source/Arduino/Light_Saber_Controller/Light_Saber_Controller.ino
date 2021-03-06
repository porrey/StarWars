// Copyright © 2016 Daniel Porrey
//
// This file is part of the Interactive Star Wars 3D Art project
// on hackster.io.
//
// This is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This software is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this software. If not, see http://www.gnu.org/licenses/.
//

// ***
// *** Include libraries
// ***
#include <Arduino.h>
#include <Wire.h>
#include "LightSaber.h"

// ***
// *** Define the slave address of the device
// ***
#define I2C_SLAVE_ADDRESS 0x5

// ***
// *** Light Saber parameters
// ***
#define PIN 6
#define ON_OFF_DELAY 3

// ***
// *** This is the current command that is running. 0
// *** indicates that there is not an active command.
// ***
byte _command = 0;
byte _data = 0;

// ***
// *** Indicats to the loop that a
// *** command was received.
// ***
bool _commandReceived = false;

// ***
// ***
// ***
LightSaber _lightSaber = LightSaber();

void setup()
{
  // ***
  // *** Initialize the light saber
  // ***
  _lightSaber.begin(PIN, ON_OFF_DELAY);

  // ***
  // *** Initialize this device as I2C slave
  // ***
  Wire.begin(I2C_SLAVE_ADDRESS);

  // ***
  // *** Setup ythe recieve event handler
  // ***
  Wire.onRequest(onRequest);
  Wire.onReceive(onReceive);
}

void loop()
{
  if (_commandReceived)
  {
    switch (_command)
    {
      case 1:
        // ***
        // *** Start the theater chase
        // ***
        _lightSaber.turnOn();
        break;
      case 2:
        _lightSaber.turnOff();
        break;
    }

    // ***
    // *** Reset the command and data
    // ***
    _command = 0;
    _data = 0;
    _commandReceived = false;
  }
}

void onReceive(int byteCount)
{
  Serial.print(F("Received: "));
  Serial.println(byteCount);

  // ***
  // *** Always expecting two bytes
  // ***
  if (byteCount == 2)
  {
    // ***
    // ***
    // ***
    _command = Wire.read();
    Serial.print(F("\tCommand = "));
    Serial.println(_command);

    _data = Wire.read();
    Serial.print(F("\tData = "));
    Serial.println(_data);

    if (_command < 0)
    {
      _command = 0;
      _data = 0;
      _commandReceived = false;
    }
    else
    {
      _commandReceived = true;
    }
  }
}

// ***
// *** The Master is requesting data. Only sending back
// *** the current command is supported.
// ***
void onRequest()
{
  Wire.write(_command);
}
