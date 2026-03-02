#include <iostream>
#include "List.h"
#include "Person.h"
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

//Insert a new Person object into the begining of the list
void List::InsertFront(Person& mani) { 
	Node *tmp = new Node;
	tmp->man=mani;
	tmp->prev=NULL;
	tmp->next=head;
	head=tmp;
	if (tail==NULL)
		tail=head;// =tmp
	else
		tmp->next->prev=tmp;
}

//Insert a new Person object into the end of the list
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

//Remove a Person object from the beginig of the list
void List::RemoveFront() { 
	Node* temp;
	if (IsEmpty() == false){ // only if the list isn't empty remove the first man, otherwise do nothing
		if (Size()== 1)
			tail=NULL;
		else
			head->next->prev=NULL;
		temp=head;
		head=head->next;
		delete temp;
	}
}

//Remove a Person object from the end of the list
void List::RemoveBack() { 
	Node* temp;
	if (IsEmpty() == false){ // only if the list isn't empty remove the last man, otherwise do nothing
		if (Size()== 1)
			head=NULL;
		else
			tail->prev->next=NULL;
		temp=tail;
		tail=tail->prev;
		delete temp;
	}
}

//Return the Person object who is in the begining of the list
Person List::Front() {
	if (IsEmpty() == true)
		return Person(); // if the list is empty, return man with empty name and age 0
	return head->man;
}

//Return the Person object who is in the end of the list
Person List::Back() {
	if (IsEmpty()==true)
		return Person(); // if the list is empty, return man with empty name and age 0
	return tail->man;
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

//Return if a specific Person is in the List (false/true)
bool List::Find(Person& mani) {
	Node* tmp=head;
	while (tmp!=NULL){
		if (mani == tmp->man)
			return true;
		tmp=tmp->next;
	}
	return false;	
}

//Remove a specific Person from the List (only is he's in it, otherwise do nothing)
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
void List::Sort() {
	Node *tmp;
	Person guy;
	while (IsSorted()==false){//Bubble sort
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
}

//Return if the List is sorted (false/true)
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

//Print List
ostream& operator << (ostream& os,const List& ls)
{
	Node* tmp=ls.GetHead();
	while (tmp!=NULL){
		os << (tmp->man); //Print Person
		tmp=tmp->next;
	}
	return os;
}