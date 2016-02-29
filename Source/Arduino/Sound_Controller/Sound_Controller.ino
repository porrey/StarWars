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
#include <SPI.h>
#include <Adafruit_VS1053.h>
#include <SD.h>

// ***
// *** Pin definitions for the sound card
// ***
#define BREAKOUT_RESET  9   // *** VS1053 reset pin (output)
#define BREAKOUT_CS     10  // *** VS1053 chip select pin (output)
#define BREAKOUT_DCS    8   // *** VS1053 Data/command select pin (output)
#define CARD_CS 4           // *** Chip select for the SD Card
#define DREQ 3              // *** VS1053 Data request, ideally an Interrupt pin

// ***
// *** Specifies the slave address of this device.
// ***
#define I2C_SLAVE_ADDRESS 0x6

// ***
// ***
// ***
Adafruit_VS1053_FilePlayer _musicPlayer = Adafruit_VS1053_FilePlayer(BREAKOUT_RESET, BREAKOUT_CS, BREAKOUT_DCS, DREQ, CARD_CS);

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

void setup()
{
  Serial.begin(115200);

  // ***
  // *** Initialize the sound card and use interrupts.
  // ***
  _musicPlayer.begin();
  _musicPlayer.setVolume(20, 20);
  _musicPlayer.useInterrupt(VS1053_FILEPLAYER_PIN_INT);  // DREQ int

  // ***
  // *** Initialize the SD card
  // ***
  SD.begin(CARD_CS);

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

// ***
// *** The loop watches for commands
// *** from the I2C bus and executes
// *** them:
// ***
// *** 1: Volume (data is volume level)
// *** 2: Stop (data is ignored)
// *** 3: Pause (data is ignored)
// *** 4: Resume (data is ignored)
// *** 5: Play (data is index of file to play)
// *** 6: Start Light Saber
// *** 7: Stop Light Saber
// ***
void loop()
{
  if (_commandReceived)
  {
    switch (_command)
    {
      case 1:
        // ***
        // *** Set the volume where data is the
        // *** volume level for both channels.
        // ***
        _musicPlayer.setVolume(_data, _data);
        break;

      case 2:
        // ***
        // *** Stop playing if a file is
        // *** currently playing.
        // ***
        stopCurrentPlay();
        break;

      case 3:
        // ***
        // *** Pause playing
        // ***
        if (!_musicPlayer.paused())
        {
          _musicPlayer.pausePlaying(true);
        }
        break;

      case 4:
        // ***
        // *** Resume playing
        // ***
        if (_musicPlayer.paused())
        {
          _musicPlayer.pausePlaying(true);
        }
        break;
      case 5:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the track
        // ***
        switch (_data)
        {
          case 0:
            _musicPlayer.startPlayingFile("On.wav");
            break;
          case 1:
            _musicPlayer.startPlayingFile("Hum.wav");
            break;
          case 2:
            _musicPlayer.startPlayingFile("Off.wav");
            break;
          case 3:
            _musicPlayer.startPlayingFile("Breathe.wav");
            break;
          case 4:
            _musicPlayer.startPlayingFile("Darkside.wav");
            break;
          case 5:
            _musicPlayer.startPlayingFile("Taught.wav");
            break;
          case 6:
            _musicPlayer.startPlayingFile("Last.wav");
            break;
          case 7:
            _musicPlayer.startPlayingFile("Give.wav");
            break;
          case 8:
            _musicPlayer.startPlayingFile("Resist.wav");
            break;
          case 9:
            _musicPlayer.startPlayingFile("Destroy.wav");
            break;
          case 10:
            _musicPlayer.startPlayingFile("Expect.wav");
            break;
        }
        break;

      case 6:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the tracks
        // ***
        Serial.println("Playing On.wav");
        _musicPlayer.playFullFile("On.wav");
        Serial.println("Playing Hum.wav");
        _musicPlayer.startPlayingFile("Hum.wav");
        break;

      case 7:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the track
        // ***
        Serial.println("Playing Off.wav");
        _musicPlayer.playFullFile("Off.wav");
        _musicPlayer.reset();
        break;
      case 8:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the track
        // ***
        _musicPlayer.startPlayingFile("Breathe.wav");
        break;
      case 9:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the track
        // ***
        _musicPlayer.startPlayingFile("Darkside.wav");
        break;
      case 10:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the track
        // ***
        _musicPlayer.startPlayingFile("Taught.wav");
        break;
      case 11:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the track
        // ***
        _musicPlayer.startPlayingFile("Last.wav");
        break;
      case 12:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the track
        // ***
        _musicPlayer.startPlayingFile("Give.wav");
        break;
      case 13:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the track
        // ***
        _musicPlayer.startPlayingFile("Resist.wav");
        break;
      case 14:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the track
        // ***
        _musicPlayer.startPlayingFile("Destroy.wav");
        break;
      case 15:
        // ***
        // *** If playing then stop
        // ***
        stopCurrentPlay();

        // ***
        // *** Play the track
        // ***
        _musicPlayer.startPlayingFile("Expect.wav");
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

void stopCurrentPlay()
{
  // ***
  // *** If playing then stop
  // ***
  if (_musicPlayer.playingMusic)
  {
    Serial.println("Stopping current play...");
    _musicPlayer.stopPlaying();
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
