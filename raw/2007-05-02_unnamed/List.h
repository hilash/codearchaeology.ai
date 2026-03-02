#ifndef _LIST_H_
#define _LIST_H_

#include <stdlib.h> 
#include <iostream>
#include "Person.h"


struct Node{
	Person current;
	Node* next;
	Node* prev;
} ;


class List {
public:
	void InsertFront(Person man); //
	void InsertBack (Person man); //
	void RemoveFront (); //
	void RemoveBack(); //
	void Front() ;
	void Back ();
	bool IsEmpty() ;
	int Size ();
	bool Find(Person* man);
	void Remove();
	void Sort();
	Node* FindLast();

private:
	Node *first;
	Node *last;

};



#endif