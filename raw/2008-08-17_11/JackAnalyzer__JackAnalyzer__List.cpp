#include "List.h"

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

//Insert a new Person object into the end of the list
void List::InsertBack(char *name2, char *type2, char *kind2){

	static int local_counter = 0;
	static int static_counter = 0;
	static int field_counter = 0;
	static int argument_counter = 0;

	Node *tmp = new Node;
	strcpy(tmp->Name,name2);
	strcpy(tmp->Type,type2);
	strcpy(tmp->Kind,kind2);
	if ( strcmp(kind2,"null") == 0 ){
		local_counter = 0;
		static_counter = 0;
		field_counter = 0;
		argument_counter = 0;
		return;
	}
	else if ( strcmp(kind2,"local") == 0 ){
		tmp->Num = local_counter;
		local_counter++;
	}
	else if ( strcmp(kind2,"static") == 0 ){
		tmp->Num = static_counter;
		static_counter++;
	}
	else if ( strcmp(kind2,"field") == 0 ){
		tmp->Num = field_counter;
		field_counter++;
	}
	else if ( strcmp(kind2,"argument") == 0 ){
		tmp->Num = argument_counter;
		argument_counter++;
	}

	tmp->next=NULL;
	tmp->prev=tail;
	tail=tmp;
	if (head==NULL) 
		head=tail;// =tmp
	else
		tmp->prev->next=tmp;
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
Node* List::Find(char *name2) {
	Node* tmp=head;
	while (tmp!=NULL){
		if ( strcmp(name2,tmp->Name) == 0 )
			return tmp;
		tmp=tmp->next;
	}
	return NULL;	
}

//Return how many of type in the list
int List::HowManyOfKind(char *kind2) {
	Node* tmp=head;
	int i= 0;
	while (tmp!=NULL){
		if ( strcmp(kind2,tmp->Kind) == 0 )
			i++;
		tmp=tmp->next;
	}
	return i;	
}

void List::Print() {
	Node* tmp=head;
	while (tmp!=NULL){
		printf("Name: %s\n",tmp->Name);
		printf("Type: %s\n",tmp->Type);
		printf("Kind: %s\n",tmp->Kind);
		printf("Num.: %d\n",tmp->Num);
		printf("-----------------------------------------\n");
		tmp=tmp->next;
	}
}