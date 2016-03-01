#ifndef SENSOR_SMOOTHING_H
#define SENSOR_SMOOTHING_H

#include <application.h>
#include <math.h>

// ***
// *** The definition of the SensorSmoothing class.
// ***
template<typename T>
class SensorSmoothing
{
  public:
    // ***
    // *** Initialie the library
    // ***
    SensorSmoothing(int sampleSize);

    // ***
    // *** Desctructor
    // ***
    ~SensorSmoothing();

    // ***
    // *** Add a new sample to the array.
    // ***
    void addSample(const T i);

    // ***
    // *** Get last sample placed in the array.
    // ***
    T lastestSample () const;

    // ***
    // *** Get the number of items in the array.
    // ***
    int sampleCount;

    // ***
    // *** The size of the array.
    // ***
    int sampleSize;

    // ***
    // ***
    // ***
    T average() const;

    // ***
    // ***
    // ***
    T stddev() const;

    // ***
    // *** Returns a value from 0 and up indicating how stable (or unstable)
    // *** the reading is. Unstable indicates that repeating readings are 
    // *** changing a lot. The lower the value the greater the stability.
    // ***
    double stability();

  private:
    // ***
    // *** The array of the array.
    // ***
    T *_samples;

    // ***
    // *** The top index of the array.
    // ***
    int _currentIndex;

    // ***
    // ***
    // ***
    void incrementIndex();
};

template<typename T>
SensorSmoothing<T>::SensorSmoothing(int sampleSize)
{
  // ***
  // *** Set the initial current index to 0.
  // ***
  this->_currentIndex = 0;

  // ***
  // *** Allocate enough memory for the array.
  // ***
  this->_samples = (T *) malloc (sizeof(T) * sampleSize);

  // ***
  // *** Check if there is a memory allocation error.
  // ***
  if (this->_samples != NULL)
  {
    // ***
    // *** set the initial size of the array.
    // ***
    this->sampleSize = sampleSize;
  }
  else
  {
    this->sampleSize = 0;
  }
}

template<typename T>
SensorSmoothing<T>::~SensorSmoothing()
{
  // ***
  // *** Deallocate the array of the internal array.
  // ***
  if (this->_samples != NULL)
  {
    free (this->_samples);
  }

  // ***
  // *** Set the internal array's array pointer to nowhere.
  // ***
  this->_samples = NULL;

  // ***
  // *** Set the size of the internal array to zero.
  // ***
  this->sampleSize = 0;

  // ***
  // *** Set the initial top index of the internal array.
  // ***
  this->_currentIndex = 0;
}

template<typename T>
void SensorSmoothing<T>::addSample(const T i)
{
  if (this->sampleSize > 0)
  {
    // ***
    // *** store the item to the array.
    // ***
    this->_samples[this->_currentIndex] = i;

    // ***
    // *** Increments to the next storage position
    // *** in the array.
    // ***
    this->incrementIndex();

    // ***
    // *** The sample size will increment until
    // *** it reaches the beginning.
    // ***
    if (this->sampleCount < this->sampleSize)
    {
      this->sampleCount++;
    }
  }
}

template<typename T>
void SensorSmoothing<T>::incrementIndex()
{
  // ***
  // *** Increment to the next position in the array.
  // ***
  this->_currentIndex++;

  // ***
  // *** If we go past the end of the array
  // *** wrap back to the beginning.
  // ***
  if (this->_currentIndex == this->sampleSize)
  {
    this->_currentIndex = 0;
  }
}

template<typename T>
T SensorSmoothing<T>::average() const
{
  T returnValue = 0;

  if (this->sampleCount > 0)
  {
    T sum = 0;

    for (int i = 0; i < this->sampleCount; i++)
    {
      sum += this->_samples[i];
    }

    returnValue = sum / this->sampleCount;
  }

  return returnValue;
}

template<typename T>
T SensorSmoothing<T>::stddev() const
{
  T returnValue = 0;

  if (this->sampleCount > 0)
  {
    T sum = 0;

    // ***
    // *** Get the average
    // ***
    T average = this->average();

    for (int i = 0; i < this->sampleCount; i++)
    {
      // ***
      // *** Sum the square of the difference of
      // *** the average to the value.
      // ***
      T difference = average - this->_samples[i];
      sum += (difference * difference);
    }

    returnValue = sqrt(sum);
  }

  return returnValue;
}

template<typename T>
T SensorSmoothing<T>::lastestSample () const
{
  T returnValue = 0;

  // ***
  // *** Check if the array is empty.
  // ***
  if (this->sampleCount > 0)
  {
    // ***
    // *** Get the top item from the array.
    // ***
    returnValue = this->_samples[this->_currentIndex];
  }

  return returnValue;
}

template<typename T>
double SensorSmoothing<T>::stability()
{
  double returnValue = 1.0;

  // ***
  // *** Determine the Coefficient of Variance (normalized
  // *** standard deviation)
  // ***
  if (this->average() > 0)
  {
    returnValue = this->stddev() / this->average();
  }

  return returnValue;
}
#endif