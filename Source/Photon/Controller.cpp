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

#include "Controller.h"

void Controller::begin(unsigned int volume)
{
    this->_volume = volume;
    this->setVolume(this->_volume);
};

void Controller::setVolume(unsigned int volume)
{
    // ***
    // *** Set the volume to value
    // ***
    this->_volume = volume;
    this->sendCommand(SOUND_DEVICE, SOUND_COMMAND_VOLUME, this->_volume);
};

void Controller::darthVaderOn()
{
    // ***
    // *** Publish an event
    // ***
    Particle.publish("Darth Vader", "On");
    
    // ***
    // *** Send a command via I2C to the controller board
    // ***
    this->sendCommand(VADER_DEATH_STAR_DEVICE, VADER_EYES_ON, NO_DATA);
};

void Controller::darthVaderOff()
{
    // ***
    // *** Publish an event
    // ***
    Particle.publish("Darth Vader", "Off");
    
    // ***
    // *** Send a command via I2C to the controller board
    // ***
    this->sendCommand(VADER_DEATH_STAR_DEVICE, VADER_EYES_OFF, NO_DATA);
};

void Controller::deathStarOn()
{
    // ***
    // *** Publish an event
    // ***
    Particle.publish("Death Star", "On");
    
    // ***
    // *** Send a command via I2C to the controller board
    // ***
    this->sendCommand(VADER_DEATH_STAR_DEVICE, DEATH_STAR_ON, NO_DATA);
};

void Controller::deathStarOff()
{
    // ***
    // *** Publish an event
    // ***
    Particle.publish("Death Star", "Off");
    
    // ***
    // *** Send a command via I2C to the controller board
    // ***
    this->sendCommand(VADER_DEATH_STAR_DEVICE, DEATH_STAR_OFF, NO_DATA);
};

void Controller::lightSaberOn()
{
    this->lightSaberOn(20);
}

void Controller::lightSaberOn(unsigned int duration)
{
    // ***
    // *** Minimum udration is 2, maximum duration is 25 seconds.
    // ***
    duration = constrain(duration, 2000, 25000);
    
    // ***
    // *** Publish an event
    // ***
    Particle.publish("Light Saber", "On");

    // ***
    // *** Using the LED on D7 to indicate the call was made.
    // *** this is used for debugging purpoises.
    // ***
    digitalWrite(D7, HIGH);

    // ***
    // *** Send a command via I2C to the controller boards
    // ***
    this->sendCommand(SOUND_DEVICE, SOUND_COMMAND_LIGHTSABER_ON, NO_DATA);
    this->sendCommand(LIGHT_SABER_DEVICE, LIGHT_SABER_COMMAND_ON, NO_DATA);
    
    // ***
    // *** Wait the duration
    // ***
    //Particle.publish("Timer ", "Setting");
    delay(duration);
    this->lightSaberOff();
};

void Controller::lightSaberOff()
{
    // ***
    // *** Publish an event
    // ***
    Particle.publish("Light Saber", "Off");
    
    // ***
    // *** turn D7 LED off
    // ***
    digitalWrite(D7, LOW);
    
    // ***
    // *** Send a command via I2C to the controller boards
    // ***
    this->sendCommand(LIGHT_SABER_DEVICE, LIGHT_SABER_COMMAND_OFF, NO_DATA);
    this->sendCommand(SOUND_DEVICE, SOUND_COMMAND_LIGHTSABER_OFF, NO_DATA);
};

void Controller::darthVaderVoice()
{
    this->darthVaderVoice(++this->_lastVoiceIndex);
};

void Controller::darthVaderVoice(unsigned int index)
{
    // ***
    // *** Minimum index = 0, maximum is SOUND_MAX_VADER_VOICE - 1
    // ***
    index = constrain(index, 0, SOUND_MAX_VADER_VOICE - 1);
    this->_lastVoiceIndex = index;

    // ***
    // *** Send a command via I2C to the controller boards
    // ***
    this->sendCommand(SOUND_DEVICE, SOUND_COMMAND_VADER_VOICE_1 + index, NO_DATA);
};

void Controller::randomEvent()
{
    int eventIndex = random(0, 10);
    Particle.publish("Event Index", String(eventIndex));

    int voiceIndex = random(1, SOUND_MAX_VADER_VOICE + 1);
    Particle.publish("Voice Index", String(voiceIndex));
    
    switch(eventIndex)
    {
        case 1:
        case 3:
            // ***
            // *** Turns the eyes on only
            // ***
            this->darthVaderOn();
            delay(random(5000, 10000));
            this->darthVaderOff();
            break;
        case 5:
        case 7:
        case 9:
            // ***
            // *** Turn Darth Vader's eyes on, wait 2 seconds, speak,
            // *** and then turn his eyes off after 5 sconds.
            // ***
            this->darthVaderOn();
            delay(2000);
            this->darthVaderVoice(voiceIndex);
            delay(5000);
            this->darthVaderOff();
            break;
        case 0:
        case 2:
        case 4:
            // ***
            // *** Turn the Light Saber on for
            // *** a random amount fo time
            // *** between 6 and 15 seconds.Also
            // *** turn Darth Vader on.
            // ***
            this->darthVaderOn();
            this->lightSaberOn(random(6000, 15000));
            delay(2000);
            this->darthVaderOff();
            break;  
        case 6:
        case 8:
            // ***
            // *** Turn the Light Saber on for
            // *** a random amount fo time
            // *** between 6 and 15 seconds
            // ***
            this->lightSaberOn(random(6000, 15000));
            break;   
        
    }
}

int Controller::executeCommand(String command)
{
    int returnValue = 0;

    if (command == "darthVaderOn")
    {
        this->darthVaderOn();
        returnValue = 1;
    }
    else if (command == "darthVaderOff")
    {
        this->darthVaderOff();
        returnValue = 2;
    }
    else if (command == "deathStarOn")
    {
        this->deathStarOn();
        returnValue = 3;
    }
    else if (command == "deathStarOff")
    {
        this->deathStarOff();
        returnValue = 4;
    }
    else if (command == "lightSaberOn")
    {
        this->lightSaberOn(6000);
        returnValue = 5;
    }
    else if (command == "lightSaberOff")
    {
        this->lightSaberOff();
        returnValue = 6;
    }
    else if (command == "darthVaderVoice")
    {
        this->darthVaderVoice();
        returnValue = 7;
    }
    
    return returnValue;
}

void Controller::sendCommand(byte address, byte command, byte data)
{
    Wire.beginTransmission(address);
    Wire.write(command);
    Wire.write(data);
    Wire.endTransmission(true);
};

byte Controller::getCommand(byte address)
{
    Wire.requestFrom(address, (byte)1);
    byte command = Wire.read();
    return command;
};