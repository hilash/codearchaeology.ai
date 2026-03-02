#include <iostream>
#include "List.h"
#include "Person.h"
using namespace std;


List::List()//c'tor
{
	head=NULL;
	tail=NULL;
}

List::~List()//d'tor
{
	delete head;
	delete tail;
}

void List::InsertFront(Person& mani) { 
	Node *tmp = new Node;
	tmp->man=mani;
	tmp->prev=NULL;
	tmp->next=head;
	head=tmp;
	if (tail==NULL)
		tail=head;// =tmp2 
	else
		tmp->next->prev=tmp;
}
void List::InsertBack(Person& mani) {
	Node *tmp = new Node;
	tmp->man=mani;
	tmp->next=NULL;
	tmp->prev=tail;
	tail=tmp;
	if (head==NULL) 
		head=tail;// =tmp
	else
		tmp->prev->next=tmp;
}

void List::RemoveFront() { 
	Node* temp;
	if (IsEmpty() == false){// only if the list isn't empty we remove the first man
		if (Size()== 1)
			tail=NULL;
		else
			head->next->prev=NULL;
		temp=head;
		head=head->next;
		DeleteNode (temp);// îéîåù ãéñèø÷èåø ìðåã*******************************************************************
	}
}

void List::RemoveBack() { 
	Node* temp;
	if (IsEmpty() == false){// only if the list isn't empty we remove the first man
		if (Size()== 1)
			head=NULL;
		else
			tail->prev->next=NULL;
		temp=tail;
		tail=tail->prev;
		DeleteNode (temp);// îéîåù ãéñèø÷èåø ìðåã********************************************************************
	}
}

Person List::Front() {
	if (IsEmpty() == true)
		return Person();// îéîåù ÷åðñèø÷èåø òí òøëéí øé÷éí ìùí åùí îùôçä å0 ìâéì*****************************
	return head->man;
}

Person List::Back() {
	if (IsEmpty()==true)
		return Person();// îéîåù ÷åðñèø÷èåø òí òøëéí øé÷éí ìùí åùí îùôçä å0 ìâéì*****************************
	return tail->man;
}

bool List::IsEmpty() {
	if ((head==NULL) && (tail==NULL))
		return true;
	else return false;
}

int List::Size() {
	Node* tmp=head;
	int counter=0;
	while (tmp!=NULL){
		counter++;
		tmp=tmp->next;
	}
	return counter;
}
bool List::Find(Person& mani) {
	Node* tmp=head;
	while (tmp!=NULL){
		if (mani == tmp->man)
			return true;
		tmp=tmp->next;
	}
	return false;	
}

void List::Remove(Person& mani) {
	if (IsEmpty()==false){
		Node* tmp=head;
		while (tmp!=NULL){
			if (mani == tmp->man){
				if (tmp==head)
					RemoveFront();
				else if (tmp==tail)
					RemoveBack();
				else {
					Node* tmp2=tmp->next;
					tmp->prev->next=tmp->next;
					tmp->prev->next->prev=tmp->prev;
					DeleteNode(tmp); ///ìòøëéí ùòìéäí èîô îöáéò ãñèø÷èåøøøø
					Node* tmp=tmp2;	
				}
				return;
			}
			else tmp=tmp->next;
		}
	}
}

void List::Sort() {
	Node *tmp;
	Person guy;
	while (IsSorted()==false){
		tmp=head;
		while (tmp->next!=NULL){
			if  ((tmp->man)>=(tmp->next->man)){// then swap
				guy=tmp->man;
				tmp->man=tmp->next->man;
				tmp->next->man=guy;
			}
			tmp=tmp->next;
		}
	}
	/////// if we write here     cout << *this;       it's print the sorted list, but if we put
	/////// in the main:
	///////l.Sort();
	/////// cout << l;
	////// it won't print.
}
bool List::IsSorted(){
	Node *tmp;
	tmp=head;
	if (Size()<=1)
		return true;
	while (tmp->next!=NULL){
		if  ((tmp->man)>(tmp->next->man))
			return false;
		tmp=tmp->next;
	}
	return true;
}
void List::DeleteNode(Node* tmp)//d'tor
{
	delete tmp->next;
	delete tmp->prev;
	tmp->man.~Person();
}

ostream& operator << (ostream& os,const List& ls)
{
	Node* tmp=ls.GetHead();
	while (tmp!=NULL){
		os << (tmp->man);
		tmp=tmp->next;
	}
	return os;
}