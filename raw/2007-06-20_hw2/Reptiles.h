#ifndef __REPTILES_H__
#define __REPTILES_H__

#include "Animals.h"
#include <iostream>
using namespace std;

class Reptiles : public Animals {

protected:
	double length;
	ClassType ct;

public:
	Reptiles(char* nm="", int ag=0, double lng=0);
	~Reptiles();
	Reptiles(const Reptiles& other);

	virtual void Print();
	virtual double CalculatedAge()=0;

	virtual ClassType GetClassType() {return ct;}
	virtual AnimalType GetAnimalType()=0;
	virtual double GetLength() {return length;}
	virtual double GetWeight() {return 0;}

};

/////////////////////////////////

class Snake : public Reptiles {

protected:
	AnimalType at;

public:
	Snake(char* nm="", int ag=0, double lng=0);
	~Snake();
	Snake(const Snake& other);

	virtual void Print();
	virtual double CalculatedAge();

	virtual AnimalType GetAnimalType() {return at;}
};

/////////////////////////////////

class Crocodile : public Reptiles {

protected:
	AnimalType at;

public:
	Crocodile(char* nm="", int ag=0, double lng=0);
	~Crocodile();
	Crocodile(const Crocodile& other);

	virtual void Print();
	virtual double CalculatedAge();

	virtual AnimalType GetAnimalType() {return at;}
};

#endif //__REPTILES_H__