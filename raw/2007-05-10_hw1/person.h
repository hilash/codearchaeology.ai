#ifndef __PERSON_H__
#define __PERSON_H__

#include <iostream>
using namespace std;

// Class Person represents details about one person. 

class Person {
public:
	Person(); //c'tor
	Person(char* frst, char* lst, int ag); //c'tor
	Person(const Person& m); //copy c'tor

	char* GetFirstName() const  {return first_name;}
	char* GetLastName() const {return last_name;}
	int GetAge() const {return age;}

private:
	char* first_name;
	char* last_name;
	int age;
};

bool operator == (const Person& m1, const Person& m2);
bool operator != (const Person& m1, const Person& m2);
bool operator <= (const Person& m1, const Person& m2);
bool operator >= (const Person& m1, const Person& m2);
bool operator < (const Person& m1, const Person& m2);
bool operator > (const Person& m1, const Person& m2);

ostream& operator << (ostream& os,const Person& m);

#endif //__PERSON_H__