#ifndef __LISTPOINTER_H__
#define __LISTPOINTER_H__

#include "Animals.h"
#include <iostream>

using namespace std;

struct Node{
		Animals* anml;
		Node* next;
		Node* prev;
	};

class ListPointer {

private:
	Node* ptr;

public:
	ListPointer(Node* p = NULL) : ptr(p) {} //c'tor
	ListPointer(const ListPointer& lp) : ptr(lp.ptr) {} //copy c'tor
	~ListPointer(){ptr = NULL;};//d'tor

	Node** GetPTR2PTR() {return (&(this->ptr));}
	bool IsNextNull() {return (ptr->next==NULL);}

	Animals* operator* ();
	Animals* operator++ ();
	Animals* operator++ (int);
	Animals* operator-- ();
	Animals* operator-- (int);
};


#endif //__LISTPOINTER_H__