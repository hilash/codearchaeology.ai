#ifndef __MAMMALS_H__
#define __MAMMALS_H__

#include "Animals.h"
#include <iostream>
using namespace std;

class Mammals : public Animals {

protected:
	double weight;
	ClassType ct;

public:
	Mammals(char* nm="", int ag=0, double wg=0);
	~Mammals();
	Mammals(const Mammals& other);

	virtual ClassType GetClassType() {return ct;}
	virtual AnimalType GetAnimalType()=0;
	virtual double GetWeight() {return weight;}
	virtual double GetLength() {return 0;}

	virtual void Print();
	virtual double CalculatedAge()=0;

};

/////////////////////////////////

class Cat : public Mammals {

protected:
	AnimalType at;

public:
	Cat(char* nm="", int ag=0, double wg=0);
	~Cat();
	Cat(const Cat& other);

	virtual void Print();
	virtual double CalculatedAge();

	virtual AnimalType GetAnimalType() {return at;}
};

/////////////////////////////////

class Elephant : public Mammals {

protected:
	AnimalType at;

public:
	Elephant(char* nm="", int ag=0, double wg=0);
	~Elephant();
	Elephant(const Elephant& other);

	virtual void Print();
	virtual double CalculatedAge();

	virtual AnimalType GetAnimalType() {return at;}
};

#endif //__MAMMALS_H__