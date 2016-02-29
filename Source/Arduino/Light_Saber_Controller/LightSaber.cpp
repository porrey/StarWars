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

#include "LightSaber.h";

LightSaber::LightSaber()
{
};

void LightSaber::begin(unsigned int pin, unsigned long onOffDelay)
{
  this->_pin = pin;
  this->_onOffDelay = onOffDelay;

  // ***
  // *** Initialize the NeoPixel strip and turn
  // *** all of the LED's off.
  // ***
  //this->_strip = Adafruit_NeoPixel(16, this->_pin, NEO_GRB + NEO_KHZ800);
  this->_strip = Adafruit_NeoPixel(100, this->_pin, NEO_RGBW + NEO_KHZ800);
  this->_strip.begin();
  this->_strip.show();
  
  this->_color = this->_strip.Color(0, 255, 0, 0); // RGBW - Red
  //this->_color = this->_strip.Color(255, 0, 0); // RGB - Red
};

// ***
// *** Stops any animate currently executing.
// ***
void LightSaber::cancel()
{
  this->_cancel = true;
};

// ***
// *** Simulate the Light Saber turning on. The delay
// *** specifies how fast it illuminates. A higher number
// *** is slower.
// ***
void LightSaber::turnOn()
{
  for (short i = 0; i < this->_strip.numPixels(); i++)
  {
    this->_strip.setPixelColor(i, this->_color);
    this->_strip.show();

    // ***
    // *** Check for a cancel
    // ***
    if (this->_cancel)
    {
      this->_cancel = false;
      break;
    }

    delay(this->_onOffDelay);
  }
};

void LightSaber::turnOff()
{
  for (short i = this->_strip.numPixels() - 1; i >= 0; i--)
  {
    this->_strip.setPixelColor(i, this->_strip.Color(0, 0, 0, 0));
    this->_strip.show();

    // ***
    // *** Check for a cancel
    // ***
    if (this->_cancel)
    {
      this->_cancel = false;
      break;
    }

    delay(this->_onOffDelay);
  }
};

void LightSaber::glow(byte lowerColor, byte higherColor, unsigned long wait)
{
  for (short k = 0; k < 7; k++)
  {
    for (short j = higherColor; j >= lowerColor; j--)
    {
      for (short i = this->_strip.numPixels() - 1; i >= 0; i--)
      {
        this->_strip.setPixelColor(i, this->_strip.Color(0, j, 0, 0));
      }

      this->_strip.show();

      // ***
      // *** Check for a cancel
      // ***
      if (this->_cancel)
      {
        break;
      }

      delay(wait);
    }

    for (short j = lowerColor; j <= higherColor; j++)
    {
      for (short i = this->_strip.numPixels() - 1; i >= 0; i--)
      {
        this->_strip.setPixelColor(i, this->_strip.Color(0, j, 0, 0));
      }

      // ***
      // *** Check for a cancel
      // ***
      if (this->_cancel)
      {
        break;
      }

      this->_strip.show();
      delay(wait);
    }

    // ***
    // *** Check for a cancel
    // ***
    if (this->_cancel)
    {
      this->_cancel = false;
      break;
    }
  }
};

void LightSaber::lightSaberWarble(unsigned long onColor, unsigned long offColor, unsigned long wait, byte groupSize)
{
  for (short k = 0; k < 7; k++)
  {
    // ***
    // *** Animate the group to the end of the strip
    // ***
    for (short i = 0; i < this->_strip.numPixels() + groupSize; i++)
    {
      this->_strip.setPixelColor(i - groupSize, offColor);
      this->_strip.setPixelColor(i, onColor);
      this->_strip.show();

      // ***
      // *** Check for a cancel
      // ***
      if (this->_cancel)
      {
        break;
      }

      delay(wait);
    }

    // ***
    // *** Animate the group to the end of the strip
    // ***
    for (short i = this->_strip.numPixels() + groupSize; i >= 0; i--)
    {
      this->_strip.setPixelColor(i, offColor);
      this->_strip.setPixelColor(i - groupSize, onColor);
      this->_strip.show();

      // ***
      // *** Check for a cancel
      // ***
      if (this->_cancel)
      {
        break;
      }

      delay(wait);
    }

    // ***
    // *** Check for a cancel
    // ***
    if (this->_cancel)
    {
      this->_cancel = false;
      break;
    }
  }
};
