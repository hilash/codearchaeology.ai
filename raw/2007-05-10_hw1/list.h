#ifndef __LIST_H__
#define __LIST_H__

#include <iostream>
#include "Person.h"
using namespace std;

// Class List represents a doubly-linked list of Person 
//type objecets, whom represent details about one person. 

struct Node{
		Person man;
		Node* next;
		Node* prev;
	};

class List {
public:
	List(); //c'tor
	~List(); //d'tor

	void InsertFront(Person& mani);
	void InsertBack(Person& mani);

	void RemoveFront();
	void RemoveBack();

	Person Front();
	Person Back();

	bool IsEmpty();
	int Size();

	bool Find(Person& mani);
	void Remove(Person& mani);
	void Sort();
	bool IsSorted();

	Node* GetHead() const  {return head;}
	Node* GetTail() const  {return tail;}

private:
	Node* head;
	Node* tail;
};

ostream& operator << (ostream& os,const List& ls);

#endif //__LIST_H__