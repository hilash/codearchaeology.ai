#include "Mammals.h"
#include <iostream>
using namespace std;

Mammals :: Mammals(char* nm, int ag, double wg) : Animals(nm,ag) //c'tor
{
	weight = wg;
	ct = MML;
}

Mammals :: ~Mammals() // d'tor
{
	Animals::~Animals();
	weight = 0;
	ct = MML;
}

Mammals :: Mammals(const Mammals &other) : Animals(other) // copy c'tor
{
	weight = other.weight;
	ct = other.ct;
}
void Mammals :: Print()
{
	Animals::Print();
	cout << "\n Weight : " << weight;
	cout << "\n Class  : Mammals";
}

/////////////////////////////////

Cat :: Cat(char* nm, int ag, double wg) : Mammals(nm, ag, wg)
{
	at = CT1;
}
	
Cat :: ~Cat()
{
	Mammals::~Mammals();
	at = CT1;
}

Cat :: Cat(const Cat& other) : Mammals(other)
{
	at = other.at;
}

void Cat::Print()
{
	Mammals::Print();
	cout << "\n Type   : Cat";
	cout << "\n Age in human years:" << CalculatedAge() <<"\n";
}

double Cat::CalculatedAge()
{
	if (age == 1)
		return 16;
	else if (age == 2) return 24;
	else return (24 + (age - 2) * 4);
}



/////////////////////////////////


Elephant :: Elephant(char* nm, int ag, double wg) : Mammals(nm, ag, wg)
{
	at = ELP;
}
	
Elephant :: ~Elephant()
{
	Mammals::~Mammals();
	at = ELP;
}

Elephant :: Elephant(const Elephant& other) : Mammals(other)
{
	at = other.at;
}

void Elephant::Print()
{
	Mammals::Print();
	cout << "\n Type   : Elephant";
	cout << "\n Age in human years:" << CalculatedAge() <<"\n";
}

double Elephant::CalculatedAge()
{
	return age;
}
