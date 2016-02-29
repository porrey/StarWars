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

#ifndef HC_SR04_H
#define HC_SR04_H

#include <application.h>

// ***
// *** The default pins to assume if they
// *** are not specified.
// ***
#define DEFAULT_TRIGGER_PIN 2
#define DEFAULT_ECHO_PIN 3

// ***
// *** The maximum amount of time to await a
// *** response from the sensor.
// ***
#define TIME_OUT_MILLISECONDS 60

// ***
// *** This is the devices range capability
// *** in centimneters.
// ***
#define HCSR04_LOWER_RANGE 2
#define HCSR04_UPPER_RANGE 400

// ***
// *** Possible values for
// *** Range.result.
// ***
enum PingTimeResultCode 
{
  pingTimeSuccess,
  alreadyActive,
  timeout
};

// ***
// *** Possible values for
// *** PingTime.result.
// ***
enum DistanceResultCode 
{
  timeFailed,
  distanceSuccess,
  underRange,
  overRange
};

// ***
// *** Defines a struture to hold the result
// *** of a call to getTime().
// ***
struct PingTime
{
  PingTimeResultCode result;
  unsigned long time;
};

// ***
// *** Defines a struture to hold the result
// *** of a call to getRange().
// ***
struct Range
{
  DistanceResultCode result;
  PingTime pingTime;
  double distance;
};

class HcSr04
{
  public:
    // ***
    // *** Initializes the SimplePing object with default pins.
    // ***
    void begin();

    // ***
    // *** Initializes the SimplePing object with the specified pins.
    // ***
    void begin(int triggerPin, int echoPin);

    // ***
    // *** Initiates a ping and returns the distance in cm.
    // ***
    bool getRange(Range *range);

    // ***
    // *** Initiates a ping and returns the time in µS.
    // ***
    bool getTime(PingTime *pingTime);

  private:
    int _triggerPin = DEFAULT_TRIGGER_PIN;
    int _echoPin = DEFAULT_ECHO_PIN;
    bool _active = false;

};
#endif