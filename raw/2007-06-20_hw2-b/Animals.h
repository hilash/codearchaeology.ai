#ifndef __ANIMALS_H__
#define __ANIMALS_H__

#include <iostream>
#include <string>

using namespace std;

enum  ClassType {MML, RPT, BRD } ;
enum  AnimalType {CT1, ELP, SNK, CRC, EGL, PRT } ;

class Animals {

protected:
	char* name;
	int age;

public:
	Animals(char* nm="", int ag=0);//c'tor
	~Animals();//d'tor
	Animals(const Animals& other);// copy c'tor

	virtual void Print();
	virtual double CalculatedAge()=0;

	int GetAge() {return age;}
	char* GetName() {return name;}

	virtual ClassType GetClassType()=0;
	virtual AnimalType GetAnimalType()=0;
	virtual double GetLength()=0;
	virtual double GetWeight()=0;

	bool operator == (Animals& anml2);
	bool operator != (Animals& anml2);
	bool operator < (Animals& anml2);
	bool operator > (Animals& anml2);
	bool operator <= (Animals& anml2);
	bool operator >= (Animals& anml2);
};

ostream& operator << (ostream& os,const Animals& anml);

#endif //__ANIMALS_H__