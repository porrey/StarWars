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
#include "HC-SR04.h"
#include "SensorSmoothing.h"

// ***
// *** These are the two pins the HC-SR04 is connected to.
// ***
#define TRIGGER_PIN D2
#define ECHO_PIN D3

// ***
// *** These are the two pins the motion sensor is connected to.
// ***
#define MOTION_OUT D4
#define MOTION_ENABLED D5

// ***
// *** Photoresistor Pin
// ***
#define PHOTORESISTOR_PIN A0

// ***
// *** Numbers of sampels to take for averaging.
// ***
#define NUMBER_OF_SAMPLES 5

// ***
// *** Default volume level
// ***
#define DEFALT_VOLUME 15

// ***
// *** This is the main controller to send commands
// *** to all of the devices.
// ***
retained int _volume = DEFALT_VOLUME;

// ***
// *** Define the controller
// ***
Controller _controller = Controller();

// ***
// *** Create the SImplePing object for the
// *** HC-SR04 (Ultrasonic Ranging Module).
// ***
HcSr04 _ranging = HcSr04();

// ***
// *** This variable indicates if there is current
// *** motion detected.
// ***
bool _motion = false;

// ***
// *** This variable indicates if there is current
// *** motion detected.
// ***
double _distance = 0.0;

// ***
// *** Analog value on photoresistor pin
// ***
int _light = 0;

// ***
// *** This object will be used to keep a running average
// *** of readings with a sample size of NUMBER_OF_SAMPLES.
// *** This is used to keep the ultrasonic sensor from being 
// *** too sesnsitive or jumpy.
// ***
SensorSmoothing<double> _distanceSample = SensorSmoothing<double>(NUMBER_OF_SAMPLES);

// ***
// *** Timer used to monitor the sensor
// ***
Timer _sensorTimer(1000, onSensorTimer);

// ***
// *** Last time of an event
// ***
int _lastEventTime = 0;

// ***
// *** Light Threshold
// ***
#define DEFAULT_LIGHT_THRESHOLD 1000
retained int _lightThreshold = DEFAULT_LIGHT_THRESHOLD;
bool _lightBelowThreshold = false;
bool _deathStarIsOn = false;

// ***
// *** Motion detecting parameters
// ***
retained int _minimumEventTime = 20;    // seconds
retained int _minimumDistance = 127;    // (50 inches) parameter is cm

void setup()
{
    // ***
    // *** Publish a setup message
    // ***
    Particle.publish("Setup", "Started");
    
    // ***
    // *** Enable backup RAM
    // ***
    STARTUP(System.enableFeature(FEATURE_RETAINED_MEMORY));
    
    // ***
    // *** Initialize the HC-SR04 (Ultrasonic Ranging Module)
    // ***
    _ranging.begin(TRIGGER_PIN, ECHO_PIN);
    
    // ***
    // *** Setup I2C
    // ***
    Wire.begin();
  
    // ***
    // *** Initializwe the controller
    // ***
    _controller.begin(_volume);
    
    // ***
    // *** Use the D7 LED indicate when the Light
    // *** Saber is active.
    // ***
    pinMode(D7, OUTPUT);
    digitalWrite(D7, LOW);
    
    // ***
    // *** Set of the public variables
    // ***
    Particle.variable("motion", _motion);
    Particle.variable("distance", _distance);
    Particle.variable("light", _light);

    // ***
    // *** Publish functions
    // ***
    Particle.function("controller", controllerWebApi);
    Particle.function("parameter", parameterWebApi);
    Particle.function("test", testWebApi);
    
    // ***
    // *** Set the last event time to
    // *** the current time
    // ***
    _lastEventTime = Time.now();
    
    // ***
    // *** Start the timer
    // ***
    _sensorTimer.start();

    // ***
    // *** Gives the other boards time to start
    // *** just in case this is a power up.
    // ***
    delay(3000);

    // ***
    // *** Reset
    // ***
    _controller.darthVaderOff();
    _controller.lightSaberOff();
    _controller.deathStarOff();
    
    // ***
    // *** Publish a setup message
    // ***
    Particle.publish("Setup", "Completed");
    
    delay(2000);
    testWebApi("");
}

void loop()
{
    // ***
    // *** Temporarily cache the _lightBelowThreshold value.
    // ***
    bool previousLightBelowThreshold = _lightBelowThreshold;

    // ***
    // *** Check for motion
    // ***
    if (_motion)
    {
        // ***
        // *** Check the light level and see if
        // *** it is dark enough to turn the LED
        // *** on.
        // ***
        if (_light < _lightThreshold)
        {
            if(!previousLightBelowThreshold)
            {
                Particle.publish("Light", "Below Threshold");
            }
            
            _lightBelowThreshold = true;
            
            // ***
            // *** Turn the Death Star LED on
            // ***
            _deathStarIsOn = true;
            _controller.deathStarOn();
        }
        else
        {
            if(previousLightBelowThreshold)
            {
                Particle.publish("Light", "Above Threshold");
            }
            
            _lightBelowThreshold = false;
        }

        // ***
        // *** Check if an object is within three feet.
        // ***
        if (_distance < _minimumDistance)
        {
            // ***
            // *** Check if the minimum amount
            // *** of time has passed since the
            // *** last update.
            // ***
            if ((Time.now() - _lastEventTime) > _minimumEventTime)
            {
                Particle.publish("Object", "Detected");

                _lastEventTime = Time.now();
                _controller.randomEvent();
            }
        }
    }
    else
    {
        // ***
        // *** No motion detected
        // ***
        if (_deathStarIsOn)
        {
            // ***
            // *** When the light is above the threshold, turn the
            // *** Death Star LED off.
            // ***
            _deathStarIsOn = false;
            _controller.deathStarOff();
        }
    }
    
    // ***
    // *** Delay
    // ***
    delay(150);
}

void onSensorTimer()
{
    // ***
    // *** Check the motion sensor
    // ***
    bool previousMotion = _motion;
    
    // ***
    // *** Get the current motion sesnsor value.
    // ***
    _motion = (digitalRead(MOTION_OUT) == HIGH);
    
    // ***
    // *** Only publish and event when the status of the motion
    // *** sensor hs changed.
    // ***
    if (!previousMotion && _motion)
    {
        Particle.publish("Motion", "Detected");
        
        if (_lightBelowThreshold)
        {
            Particle.publish("Light Level", "Below Threshold: " + String(_light) + "/" + String(_lightThreshold));
        }
        else
        {
            Particle.publish("Light Level", "Above Threshold: " + String(_light) + "/" + String(_lightThreshold));
        }

        // ***
        // *** Publish an event showing the distance of the
        // *** object in front of the board.
        // ***
        Particle.publish("Distance", String(String(_distance / 2.54).toInt()) + " in / " + String(String(_minimumDistance / 2.54).toInt()) + " in");
        
        // ***
        // *** Publish an event showing the amount of time until 
        // *** the next event can be triggered.
        // ***
        Particle.publish("Time", "Last Event: " + String(Time.now() - _lastEventTime) + " second(s) ago");
        
    }
    else if (previousMotion && !_motion)
    {
        Particle.publish("Motion", "Reset");
    }
    
    // ***
    // *** Get the distance from the Ultrasonic
    // *** sensor.
    // ***
    _distance = getRangeEx();
    
    // ***
    // *** Get the photoresistor value
    // ***
    _light = analogRead(PHOTORESISTOR_PIN);
}

// ***
// *** Web API
// ***
int testWebApi(String command)
{
    // ***
    // *** Test
    // ***
    Particle.publish("Test", "Running");
    
    _controller.darthVaderOn();
    delay(200);
    _controller.deathStarOn();
    
    delay(2000);
    //_controller.darthVaderVoice(3);
    //delay(5000);
    
    _controller.lightSaberOn(7000);
    delay(2000);
    
    _controller.darthVaderOff();
    _controller.deathStarOff();

    Particle.publish("Test", "Completed");
    return 1;
}

// ***
// *** Web API
// ***
int parameterWebApi(String command)
{
    int returnValue = 0;

    // ***
    // *** If the command contains an equal
    // *** sign , assume it is trying to
    // *** set the value of a paramerer.
    // ***
    int equalIndex = command.indexOf("=");

    // ***
    // *** Check the value of command for the type
    // *** of parameter call.
    // ***
    if(equalIndex > 0)
    {
        // ***
        // *** This command is to set a variable value
        // ***
        String parameterName = command.substring(0, equalIndex);
        String value = command.substring(equalIndex + 1, command.length());
        
        if (parameterName == "volume")
        {
            _volume = value.toInt();
            _controller.setVolume(_volume);
        }
        else if (parameterName == "minimumDistance")
        {
            _minimumDistance = value.toInt();
        }
        else if (parameterName == "lightThreshold")
        {
            _lightThreshold = value.toInt();
        }
        else if (parameterName == "minimumEventTime")
        {
            _minimumEventTime = value.toInt();
        }
        
        // ***
        // *** Publish the parameter
        // ***
        Particle.publish("Set Parameter", parameterName + " = " + value);
        
        // ***
        // *** Return the value
        // ***
        returnValue = value.toInt();
    }
    else if(equalIndex == -1)
    {
        // ***
        // *** If the command matches a name
        // *** return the value.
        // ***
        if (command == "volume")
        {
           returnValue =  _volume;
        }
        else if (command == "minimumDistance")
        {
            returnValue = _minimumDistance;
        }
        else if (command == "lightThreshold")
        {
            returnValue = _lightThreshold;
        }
        else if (command == "minimumEventTime")
        {
            returnValue = _minimumEventTime;
        }
        
        // ***
        // *** Publish the parameter
        // ***
        Particle.publish("Get Parameter", command);
    }
    else if (command == "reset")
    {
        // ***
        // *** Reset the parameters to default values
        // ***
        _volume = DEFALT_VOLUME;
        _controller.setVolume(_volume);
        _minimumDistance = 50 * 2.54;
        _lightThreshold = 1000;
        _minimumEventTime = 20;
        _lightThreshold = DEFAULT_LIGHT_THRESHOLD;

        // ***
        // *** Publish the parameter
        // ***
        Particle.publish("Reset Parameters", "");

        returnValue = 1;
    }
    
    return returnValue;
}

// ***
// *** Web API
// ***
int controllerWebApi(String command)
{
    int returnValue = 0;

    // ***
    // *** Publish the parameter
    // ***
    Particle.publish("Controller", command);

    // ***
    // *** Pass the command to the controller.
    // ***
    returnValue = _controller.executeCommand(command);

    return returnValue;
}

double getRangeEx()
{
    double returnValue = 0;
    
    // ***
    // *** Get the distance in centimeters
    // ***
    Range range;

    if (_ranging.getRange(&range))
    {
        // ***
        // *** Add the sample
        // ***
        _distanceSample.addSample(range.distance);
        
        // ***
        // *** Return the average distance
        // ***
        returnValue = _distanceSample.average();
    }
    else
    {
        // ***
        // *** Check the result
        // ***
        if (range.result == timeFailed)
        {
            switch (range.pingTime.result)
            {
                case alreadyActive:
                    Particle.publish("Result", "The sensor is busy.");
                    break;
                case timeout:
                    Particle.publish("Result", "The sensor did not respond.");
                    break;
            }
        }
        else if (range.result == underRange)
        {
            switch (range.result)
            {
                case underRange:
                    Particle.publish("Result", "The object is too close.");
                    break;
                case overRange:
                    Particle.publish("Result", "No object is in range.");
                    break;
            }
        }
        else
        {
            Particle.publish("Result", "Unknown Error.");
        }
    }
    
    return returnValue;
}