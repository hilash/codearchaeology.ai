#include <iostream>
#include "Person.h"
using namespace std;

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
	
	void DeleteNode(Node* tmp); //d'tor
};

ostream& operator << (ostream& os,const List& ls);