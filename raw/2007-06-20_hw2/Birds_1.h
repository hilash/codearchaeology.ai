#ifndef __BIRDS_H__
#define __BIRDS_H__

#include "Animals.h"
#include <iostream>
using namespace std;

class Birds : public Animals {

protected:
	ClassType ct;

public:
	Birds(char* nm="", int ag=0);
	~Birds();
	Birds(const Birds& other);

	virtual void Print();
	virtual double CalculatedAge()=0;

	virtual ClassType GetClassType() {return ct;}
	virtual AnimalType GetAnimalType()=0;
	virtual double GetLength() {return 0;}
	virtual double GetWeight() {return 0;}

};

/////////////////////////////////

class Eagle : public Birds {

protected:
	AnimalType at;

public:
	Eagle(char* nm="", int ag=0);
	~Eagle();
	Eagle(const Eagle& other);

	virtual void Print();
	virtual double CalculatedAge();

	virtual AnimalType GetAnimalType() {return at;}

};

/////////////////////////////////

class Parrot : public Birds {

protected:
	AnimalType at;

public:
	Parrot(char* nm="", int ag=0);
	~Parrot();
	Parrot(const Parrot& other);

	virtual void Print();
	virtual double CalculatedAge();

	virtual AnimalType GetAnimalType() {return at;}
};

///////////////////////////
#endif //__BIRDS_H__