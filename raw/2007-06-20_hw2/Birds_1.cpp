#include "Birds.h"
#include <iostream>
using namespace std;

Birds :: Birds(char* nm, int ag) : Animals(nm,ag)
{
	ct = BRD;
}

Birds :: ~Birds()
{
	Animals::~Animals();
	ct = BRD;
}

Birds :: Birds(const Birds &other) : Animals(other)
{
	ct = other.ct;
}
void Birds :: Print()
{
	Animals::Print();
	cout << "\n Class  : Birds";
}

/////////////////////////////////

Eagle :: Eagle(char* nm, int ag) : Birds(nm, ag)
{
	at = EGL;
}
	
Eagle :: ~Eagle()
{
	Birds::~Birds();
	at = EGL;
}

Eagle :: Eagle(const Eagle& other) : Birds(other)
{
	at = other.at;
}

void Eagle::Print()
{
	Birds::Print();
	cout << "\n Type   : Eagle";
	cout << "\n Age in human years:" << CalculatedAge() <<"\n";
}

double Eagle::CalculatedAge()
{
	return 2*age;
}


/////////////////////////////////

Parrot :: Parrot(char* nm, int ag) : Birds(nm, ag)
{
	at = PRT;
}
	
Parrot :: ~Parrot()
{
	Birds::~Birds();
	at = PRT;
}

Parrot :: Parrot(const Parrot& other) : Birds(other)
{
	at = other.at;
}

void Parrot::Print()
{
	Birds::Print();
	cout << "\n Type   : Parrot";
	cout << "\n Age in human years:" << CalculatedAge() <<"\n";
}

double Parrot::CalculatedAge()
{
	return 4*age;
}
