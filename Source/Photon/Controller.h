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

#ifndef CONTROLLER_H
#define CONTROLLER_H

#include <application.h>

// ***
// *** Sound device
// ***
#define SOUND_DEVICE 0x6
#define SOUND_COMMAND_VOLUME 1
#define SOUND_COMMAND_STOP 2
#define SOUND_COMMAND_PAUSE 3
#define SOUND_COMMAND_RESUME 4
#define SOUND_COMMAND_PLAY 5
#define SOUND_COMMAND_LIGHTSABER_ON 6
#define SOUND_COMMAND_LIGHTSABER_OFF 7
#define SOUND_COMMAND_VADER_VOICE_1 8
#define SOUND_COMMAND_VADER_VOICE_2 9
#define SOUND_COMMAND_VADER_VOICE_3 10
#define SOUND_COMMAND_VADER_VOICE_4 11
#define SOUND_COMMAND_VADER_VOICE_5 12
#define SOUND_COMMAND_VADER_VOICE_6 13
#define SOUND_COMMAND_VADER_VOICE_7 14
#define SOUND_COMMAND_VADER_VOICE_8 15

#define SOUND_MAX_VADER_VOICE 8

// ***
// *** Light Saber Device
// ***
#define LIGHT_SABER_DEVICE 0x5
#define LIGHT_SABER_COMMAND_RESET 0
#define LIGHT_SABER_COMMAND_ON 1
#define LIGHT_SABER_COMMAND_OFF 2

// ***
// *** Vader/Death Star Device
// ***
#define VADER_DEATH_STAR_DEVICE 0x7
#define VADER_EYES_ON 1
#define VADER_EYES_OFF 2
#define DEATH_STAR_ON 3
#define DEATH_STAR_OFF 4

#define NO_DATA 0

class Controller
{
    public:
        void begin(unsigned int volume);
        void setVolume(unsigned int volume);
        void darthVaderOn();
        void darthVaderOff();
        void deathStarOn();
        void deathStarOff();
        void dightSaberOn();
        void lightSaberOn();
        void lightSaberOn(unsigned int duration);
        void lightSaberOff();
        void darthVaderVoice();
        void darthVaderVoice(unsigned int index);
        int executeCommand(String command);
        void randomEvent();
        
    private:
        unsigned int _volume =  10;
        unsigned int _lastVoiceIndex = 0;
        void sendCommand(byte address, byte command, byte data);
        byte getCommand(byte address);
        
};
#endif