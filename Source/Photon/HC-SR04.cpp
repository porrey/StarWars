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

#include "HC-SR04.h"

// ***
// *** Initializes the HC-SR04 object
// ***
void HcSr04::begin()
{
  // ***
  // *** Set the Trigger pin up for output
  // ***
  pinMode(this->_triggerPin, OUTPUT);

  // ***
  // *** Set the Echo pin up for input.
  // ***
  pinMode(this->_echoPin, INPUT);

  // ***
  // *** Set the trigger pin to LOW
  // ***
  digitalWrite(this->_triggerPin, LOW);
}

// ***
// *** Initializes the SimplePing object
// ***
void HcSr04::begin(int triggerPin, int echoPin)
{
  // ***
  // *** Store the non-default trigger pin.
  // ***
  this->_triggerPin = triggerPin;

  // ***
  // *** Store the non-default echo pin.
  // ***
  this->_echoPin = echoPin;

  // ***
  // *** Call the default constructor.
  // ***
  this->begin();
}

bool HcSr04::getRange(Range *range)
{
  bool returnValue = false;

  // ***
  // *** Initiate a ping and get the time.
  // ***
  PingTime pingTime;

  // ***
  // *** If the time is positive then the result
  // *** is good.
  // ***
  if (this->getTime(&pingTime))
  {
    // ***
    // *** The formual for range is given in the datasheet as
    // *** Range = t * v / 2 where velecity v is 340M/s and t
    // *** is the measured time. The return value is in
    // *** centimeters (cm).
    // ***
    double distance = 100 * ((pingTime.time / 1000.0 / 1000.0) * 340.0) / 2.0;

    if (distance < HCSR04_LOWER_RANGE)
    {
      range->result = underRange;
      range->pingTime = pingTime;
      range->distance = HCSR04_LOWER_RANGE;
      returnValue = true;
    }
    else if (distance > HCSR04_UPPER_RANGE)
    {
      range->result = overRange;
      range->pingTime = pingTime;
      range->distance = HCSR04_UPPER_RANGE;
      returnValue = true;
    }
    else
    {
      range->result = distanceSuccess;
      range->pingTime = pingTime;
      range->distance = distance;
      returnValue = true;
    }
  }
  else
  {
    // ***
    // *** This will pass through one fo the error constants
    // ***
    range->result = timeFailed;
    range->pingTime = pingTime;
    range->distance = 0;
    returnValue = false;
  }

  return returnValue;
}

bool HcSr04::getTime(PingTime *pingTime)
{
  bool returnValue = false;

  // ***
  // *** Only allow one reading at a time.
  // ***
  if (!_active)
  {
    // ***
    // *** Set the Active flag
    // ***
    _active = true;

    // ***
    // *** Set the trigger pin high for at last 10µS
    // *** to trigger the sensor to take a reading.
    // ***
    digitalWrite(this->_triggerPin, HIGH);

    // ***
    // *** Wait 10µS
    // ***
    delayMicroseconds(10);

    // ***
    // *** Set the pin back to LOW
    // ***
    digitalWrite(this->_triggerPin, LOW);

    // ***
    // *** Wait for the echo pin to go HIGH. The pulseIN()
    // *** function will return the elapsed time in microseconds.
    // ***
    unsigned long timeResult = pulseIn(this->_echoPin, HIGH);

    // ***
    // *** Check for a timeout (value of 0)
    // ***
    if (timeResult == 0)
    {
      pingTime->time = 0;
      pingTime->result = timeout;
      returnValue = false;
    }
    else
    {
      pingTime->time = timeResult;
      pingTime->result = pingTimeSuccess;
      returnValue = true;
    }

    // ***
    // *** Reet the Active flag
    // ***
    _active = false;
  }
  else
  {
    // ***
    // *** Already in the middle of a reading.
    // ***
    pingTime->time = 0;
    pingTime->result = alreadyActive;
    returnValue = false;
  }

  return returnValue;
}