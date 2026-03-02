#ifndef __LIST_H__
#define __LIST_H__

#include "ListPointer.h"
#include <iostream>

using namespace std;

class List {
public:
	List();
	~List();

	void InsertFront(Animals& anmli);
	void InsertBack(Animals& anmli);

	void RemoveFront();
	void RemoveBack();

	Animals& Front();
	Animals& Back();

	bool IsEmpty();
	int Size();

	bool Find(Animals& anmli);
	void Remove(Animals& anmli);
	void Sort();
	bool IsSorted();

	Node* GetHead() const  {return head;}
	Node* GetTail() const  {return tail;}

	void Begin(ListPointer& lp) {*(lp.GetPTR2PTR())=head;}
	void End(ListPointer& lp) {*(lp.GetPTR2PTR())=tail;}

private:
	Node* head;
	Node* tail;
};

ostream& operator << (ostream& os,const List& ls);

#endif //__LIST_H__*/