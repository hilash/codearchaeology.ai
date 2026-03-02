#include "Reptiles.h"
#include <iostream>
using namespace std;

Reptiles :: Reptiles(char* nm, int ag, double lng) : Animals(nm,ag)
{
	length = lng;
	ct = RPT;
}

Reptiles :: ~Reptiles()
{
	Animals::~Animals();
	length = 0;
	ct = RPT;
}

Reptiles :: Reptiles(const Reptiles &other) : Animals(other)
{
	length = other.length;
	ct = other.ct;
}
void Reptiles :: Print()
{
	Animals::Print();
	cout << "\n length : " << length;
	cout << "\n Class  : Reptiles";
}

/////////////////////////////////

Snake :: Snake(char* nm, int ag, double lng) : Reptiles(nm, ag, lng)
{
	at = SNK;
}
	
Snake :: ~Snake()
{
	Reptiles::~Reptiles();
	at = SNK;
}

Snake :: Snake(const Snake& other) : Reptiles(other)
{
	at = other.at;
}

void Snake::Print()
{
	Reptiles::Print();
	cout << "\n Type   : Snake";
	cout << "\n Age in human years:" << CalculatedAge() <<"\n";
}

double Snake::CalculatedAge() ////// doesn't work
{
	return 5*age;
}


/////////////////////////////////

Crocodile :: Crocodile(char* nm, int ag, double lng) : Reptiles(nm, ag, lng)
{
	at = CRC;
}
	
Crocodile :: ~Crocodile()
{
	Reptiles::~Reptiles();
	at = CRC;
}

Crocodile :: Crocodile(const Crocodile& other) : Reptiles(other)
{
	at = other.at;
}

void Crocodile::Print()
{
	Reptiles::Print();
	cout << "\n Type   : Crocodile";
	cout << "\n Age in human years:" << CalculatedAge() <<"\n";
}

double Crocodile::CalculatedAge()
{
	return 0.75*age;
}

