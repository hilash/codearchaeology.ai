#include "Animals.h"
#include <iostream>
#include <string.h>

using namespace std;

bool cmp1(char* c1, char* c2) // i've made my own function to compare two strigs (char*) cause the usual operator didn't work
{
	char* a=c1;
	char* b=c2;
	while (((*a)!='\0') && ((*b)!='\0')) {
		if ((*a++)!=(*b++))
			return false;
	}
	return true;
}

Animals::Animals(char* nm, int ag) // c'tor
{
	size_t len = strlen(nm);
	name = new char[len+1];
	strcpy(name, nm);
	age = ag;
}

Animals::~Animals() // d'tor
{
	age=0;
	free(name);
	name = NULL;
}

Animals::Animals(const Animals& other) // copy c'tor
{
	size_t len = strlen(other.name);
	name = new char[len+1];
	strcpy(name, other.name);
	age = other.age;
}

void Animals::Print()
{
	cout <<"\n Name   : " << name;
	cout <<"\n Age    : " << age;
}

bool Animals::operator == (Animals& anml2) // check if two animals are equale, depends on their type
{
	if  ((*this).GetAge() != anml2.GetAge())
		return false;
	if  (!cmp1( (*this).GetName(), anml2.GetName() ))
		return false;
	if  ((*this).GetClassType() != anml2.GetClassType())
		return false;
	if  ((*this).GetAnimalType() != anml2.GetAnimalType()) 
		return false;
	else if ( (((*this).GetClassType()) == MML) && (((*this).GetWeight()) != (anml2.GetWeight())) )
		return false;
	else if ( (((*this).GetClassType()) == RPT) && (((*this).GetLength()) != (anml2.GetLength())) )
		return false;
	else return true;
}
bool Animals::operator != (Animals& anml2)
{
	if  ( (*this) == anml2 )
		return false;
	else return true;
}

bool Animals::operator <(Animals& anml2)
{
	if ((*this)==anml2)
		return false;
	///// check the class type
	if ((((*this).GetClassType()) == MML ) && ((anml2.GetClassType() == RPT) || (anml2.GetClassType() == BRD)))
		return true;
	if ( ((*this).GetClassType() == RPT) && (anml2.GetClassType() == BRD) )
		return true;
	if ((((*this).GetClassType()) == BRD ) && ((anml2.GetClassType() == RPT) || (anml2.GetClassType() == MML)))
		return false;
	if ( ((*this).GetClassType() == RPT) && (anml2.GetClassType() == MML) )
		return false;
//////////// now the animals from the same class type, so check their names
	if (strcmp((*this).GetName(), anml2.GetName())>0)
		return false;
	return true;
}

bool Animals::operator > (Animals& anml2)
{
	return ( (!((*this)<anml2)) && (!((*this)==anml2)) );
}

bool Animals::operator <= (Animals& anml2)
{
	return ( ((*this)<anml2) || ((*this)==anml2) );
}
	
bool Animals::operator >= (Animals& anml2)
{
	return ( ((*this)>anml2) || ((*this)==anml2) );
}

ostream& operator << (ostream& os,const Animals& anml)
{
	return os;
}