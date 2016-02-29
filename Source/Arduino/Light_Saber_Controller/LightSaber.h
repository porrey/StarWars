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

#ifndef LIGHT_SABER_H
#define LIGHT_SABER_H

#include <Arduino.h>
#include <Adafruit_NeoPixel.h>

class LightSaber
{
  public:
    LightSaber();
    void begin(unsigned int pin, unsigned long onOffDelay);
    void turnOn();
    void turnOff();
    void cancel();

  private:
    unsigned int _pin = 0;
    Adafruit_NeoPixel _strip;
    bool _cancel = false;
    unsigned long _onOffDelay;
    unsigned long _color;

    void glow(byte lowerColor, byte higherColor, unsigned long wait);
    void lightSaberWarble(unsigned long onColor, unsigned long offColor, unsigned long wait, byte groupSize);
};
#endif
