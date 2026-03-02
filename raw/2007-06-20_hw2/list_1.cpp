#include <iostream>
#include "List.h"
#include "Animals.h"
#include "Mammals.h"
using namespace std;

// Class List functions

List::List() //c'tor
{
	head=NULL;
	tail=NULL;
}

List::~List() //d'tor
{
	delete head;
	delete tail;
}

//Insert a new Animals object into the begining of the list
void List::InsertFront(Animals& anmli) { 
	Node *tmp = new Node;
	tmp->anml=&anmli;
	tmp->prev=NULL;
	tmp->next=head;
	head=tmp;
	if (tail==NULL)
		tail=head;// =tmp
	else
		tmp->next->prev=tmp;
}

//Insert a new Animals object into the end of the list
void List::InsertBack(Animals& anmli) {
	Node *tmp = new Node;
	tmp->anml=&anmli;
	tmp->next=NULL;
	tmp->prev=tail;
	tail=tmp;
	if (head==NULL) 
		head=tail;// =tmp
	else
		tmp->prev->next=tmp;
}

//Remove a Animals object from the beginig of the list
void List::RemoveFront() { 
	Node* temp;
	if (IsEmpty() == false){ // only if the list isn't empty remove the first anml, otherwise do nothing
		if (Size()== 1)
			tail=NULL;
		else
			head->next->prev=NULL;
		temp=head;
		head=head->next;
		delete temp;
	}
}

//Remove a Animals object from the end of the list
void List::RemoveBack() { 
	Node* temp;
	if (IsEmpty() == false){ // only if the list isn't empty remove the last anml, otherwise do nothing
		if (Size()== 1)
			head=NULL;
		else
			tail->prev->next=NULL;
		temp=tail;
		tail=tail->prev;
		delete temp;
	}
}

//Return the Animals object who is in the begining of the list
Animals& List::Front() {
	if (IsEmpty() == true)
		return (Cat()); // if the list is empty, return anml with empty name and age 0
	return *(head->anml);
}

//Return the Animals object who is in the end of the list
Animals& List::Back() {
	if (IsEmpty()==true)
		return (Cat()); // if the list is empty, return anml with empty name and age 0
	return *(tail->anml);
}

//Return if the List is empty (false/true)
bool List::IsEmpty() {
	if ((head==NULL) && (tail==NULL))
		return true;
	else return false;
}

//Return  the List length
int List::Size() {
	Node* tmp=head;
	int counter=0;
	while (tmp!=NULL){
		counter++;
		tmp=tmp->next;
	}
	return counter;
}

//Return if a specific Animals is in the List (false/true)
bool List::Find(Animals& anmli) {
	Node* tmp=head;
	while (tmp!=NULL){
		if (anmli == *(tmp->anml))
			return true;
		tmp=tmp->next;
	}
	return false;	
}

//Remove a specific Animals from the List (only is he's in it, otherwise do nothing)
void List::Remove(Animals& anmli) {
	if (IsEmpty()==false){
		Node* tmp=head;
		while (tmp!=NULL){
			if (anmli == *(tmp->anml)){
				if (tmp==head)
					RemoveFront();
				else if (tmp==tail)
					RemoveBack();
				else {
					Node* tmp2=tmp->next;
					tmp->prev->next=tmp->next;
					tmp->prev->next->prev=tmp->prev;
					delete tmp;
					Node* tmp=tmp2;	
				}
				return;
			}
			else tmp=tmp->next;
		}
	}
}

//Sort the List by lexical order (first by last name, then by first name, and then by age(from the smaller to the bigger)) 
void List::Sort()
{
	ListPointer lp;
	ListPointer lp2;
	while (IsSorted()==false) {
		Begin(lp);
		Begin(lp2);
		lp2++;
		while (lp.IsNextNull()==false) {
			if ((*(*lp))>(*(*lp2))) { // change
				Node **a,**b;
				Animals* c;
				a=lp.GetPTR2PTR();
				b=lp2.GetPTR2PTR();
				c=*lp;
				((*a)->anml)=*lp2;
				((*b)->anml)=c;
			}				
			lp++;
			lp2++;
		}
	}
}

//Return if the List is sorted (false/true)
bool List::IsSorted()
{
	ListPointer lp;
	ListPointer lp2;
	if (Size()<=1)
		return true;
	Begin(lp);
	Begin(lp2);
	lp2++;
	while (lp.IsNextNull()==false) {
		if ((*(*lp))>(*(*lp2)))
			return false;
		lp++;
		lp2++;
	}
	return true;
}

//Print List
ostream& operator << (ostream& os,const List& ls)
{
	Node* tmp=ls.GetHead();
	while (tmp!=NULL){
		tmp->anml->Print(); //Print Animals
		tmp=tmp->next;
	}
	return os;
}