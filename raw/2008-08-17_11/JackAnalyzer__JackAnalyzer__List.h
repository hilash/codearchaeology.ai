#ifndef __LIST_H__
#define __LIST_H__

#include <iostream>
#include <string.h>
#include <stdio.h>
using namespace std;

// Class List represents a doubly-linked list of Person 
//type objecets, whom represent details about one person. 

struct Node{
		char Name[500];
		char Type[500];
		char Kind[500];
		int Num;
		Node* next;
		Node* prev;
	};

class List {
public:
	List(); //c'tor
	~List(); //d'tor

	void InsertBack(char *name2, char *type2, char *kind2);

	bool IsEmpty();
	int Size();

	Node* Find(char *name2);
	int HowManyOfKind(char *kind2);

	Node* GetHead() const  {return head;}
	Node* GetTail() const  {return tail;}

	void Print();

private:
	Node* head;
	Node* tail;
};

#endif //__LIST_H__