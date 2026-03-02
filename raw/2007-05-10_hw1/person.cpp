#include <iostream>
#include <string.h>
#include "Person.h"
using namespace std;

// Class Person functions

Person::Person() //c'tor
{
	first_name="";
	last_name="";
	age=0;
}

Person::Person(char* frst, char* lst, int ag) //c'tor
{
	first_name=frst;
	last_name=lst;
	age=ag;
}

Person::Person(const Person& other) //copy c'tor
{
	first_name=other.first_name;
	last_name=other.last_name;
	age=other.age;
}

//Check if the two Person's objects are equal (that it is the same person)
bool operator == (const Person& m1, const Person& m2)
{
	return ((m1.GetLastName() == m2.GetLastName()) && (m1.GetFirstName() == m2.GetFirstName()) && (m1.GetAge() == m2.GetAge()));
}

//Check if the two Person's objects are unequal (that it is not the same person)
bool operator != (const Person& m1, const Person& m2)
{
	return ((m1.GetLastName() != m2.GetLastName()) || (m1.GetFirstName() != m2.GetFirstName()) || (m1.GetAge() != m2.GetAge()));

}

//Check if the first person is smaller or equal to the second person (by lexical oreder) 
bool operator <= (const Person& m1, const Person& m2)
{
	return ((m1 == m2) || (m1 < m2));
}

//Check if the first person is bigger or equal to the second person (by lexical oreder) 
bool operator >= (const Person& m1, const Person& m2)
{
	return ((m1 == m2) || (m1 > m2));
}


//Check if the first person is smaller than the second person (by lexical oreder) 
bool operator < (const Person& m1, const Person& m2)
{
	if (strcmp(m1.GetLastName(), m2.GetLastName())<0)
		return true;
	else if (strcmp(m1.GetLastName(), m2.GetLastName())>0)
		return false;
	// if the last names are equal
	else if (strcmp(m1.GetFirstName(), m2.GetFirstName())<0)
		return true;
	else if (strcmp(m1.GetFirstName(), m2.GetFirstName())>0)
		return false; 
	// if also the first names are equal
	else if (m1.GetAge() < m2.GetAge())
		return true;
	else return false;
}

//Check if the first person is bigger than the second person (by lexical oreder) 
bool operator > (const Person& m1, const Person& m2)
{
	// the operator < can return false only if (m1 == m2) or if (m1 > m2) so:
	return ((!(m1 < m2)) && (m1 != m2));// means that m1 > m2
}

//Print Person
ostream& operator << (ostream& os,const Person& m)
{
	os << m.GetFirstName() << " " << m.GetLastName() << ", " << m.GetAge() << ".\n";
	return os;
}
