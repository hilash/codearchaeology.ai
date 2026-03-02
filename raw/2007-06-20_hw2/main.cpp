#include "List.h"
#include "Animals.h"
#include "Mammals.h"
#include "Reptiles.h"
#include "Birds.h"
#include <iostream>

using namespace std;

void FindOlder(List& ls, int age);

int main()
{
	Cat c1 ("Mitzi", 1, 5);
	Cat c2 ("Kity", 12, 8);
	Elephant e1 ("Yosi", 4, 100);
	Elephant e2 ("Doris", 41, 132);
	Snake s1 ("Jhon", 10, 2.5);
	Snake s2 ("Jack", 4, 3);
	Crocodile cr("Dandie", 80, 2.2);
	Eagle egl ("Nesher", 2);
	Parrot p1 ("Jacko", 21);
	Parrot p2 ("Sara", 13);

	List zoo;
	zoo.InsertFront(c2);
	zoo.InsertFront(c1);
	zoo.InsertBack(e1);
	zoo.InsertBack(e2);
	zoo.InsertFront(s2);
	zoo.InsertFront(s1);
	zoo.InsertBack(cr);
	zoo.InsertFront(egl);
	zoo.InsertBack(p2);
	zoo.InsertFront(p1);

	// the list will be: p1-egl-s1-s2-c1-c2-e1-e2-cr-p2

	zoo.Sort();

	// now the list is: e2-c2-c1-e1-cr-s2-s1-p1-egl-p2

	if(zoo.Front() != e2) // 
		cout << "something wrong..\n";
	else
		cout << "looks ok!\n";

	cout << zoo; // should use the Print() function for each animal in the list


	
	FindOlder(zoo,40);	// will print c2,e2,s1,cr,p1

	return 0;
}

void FindOlder(List& ls, int age)
{
	cout << "The animals their age in human years bigger than " << age << " are:\n";
	ListPointer lp;
	ls.Begin(lp);
	while (lp.IsNextNull()==false) {
		if ( ((*lp)->CalculatedAge()) > age ){
			(*lp)->Print();
			cout << '\n';
		}
		++lp;
	}
}