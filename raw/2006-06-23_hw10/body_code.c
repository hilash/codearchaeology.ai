/*
HW10
*/

#include <stdio.h>
#include <stdlib.h>
# define TRUE 1
# define FALSE 0

typedef struct node {
	char num;
	struct node* next;
}Node;

Node* CreateList(char c,Node *tail);
void print_list(Node *tail);
Node* CopyList(Node *tail1copy, Node *tail1);
Node* Addition(Node *tail1, Node *tail2, Node *sum);
Node* Subtraction(Node *tail1, Node *tail2, Node *difference);
int IfMinus(Node *list1, Node *list2);
void free_list(Node *list);
Node* CutZero(Node *list);
int print_listRec(Node *tail);

int main()
{
	Node *tail1=NULL;
	Node *tail2=NULL;
	Node *tail1copy=NULL;
	Node *tail2copy=NULL;
	Node *tail1_2, *tail2_2;
	Node *sum=NULL;
	Node *difference=NULL;
	int ifminus;

	char c;

	printf("Please enter two positive numbers:\n");
	while ((c=getchar())!='\n'){/*get number 1 [as list]*/
 		tail1=CreateList(c, tail1);
	}
	while ((c=getchar())!='\n'){/*get number 2 [as list]*/
		tail2=CreateList(c, tail2);
	}
	/*creat new list and copy list 1 into it [do this to not change the originals list values*/
	tail1_2=CopyList(tail1copy,tail1); /*copyed pointers to the new lists*/
	tail2_2=tail2;

	while (tail1_2!=NULL || tail2_2!=NULL){/*the addition*/
		sum=Addition(tail1_2, tail2_2, sum);
		tail1_2=tail1_2->next;
		tail2_2=tail2_2->next;
	}

	free_list(tail1copy); /*free the copies lists*/
	tail1copy=CopyList(tail1copy,tail1);/*creat new lists again*/
	tail2copy=CopyList(tail2copy,tail2);
	tail1_2=tail1copy;/*copyed pointers to the new lists*/
	tail2_2=tail2copy;

	ifminus=IfMinus(tail1_2, tail2_2);/*chack if the Subtraction will be negative*/
	tail1_2=tail1copy;/*copyed pointers to the new lists again*/
	tail2_2=tail2copy;
	if(ifminus==TRUE){/*if the first number is smaller then the second, it subtract the first num from the second*/
		while (tail1_2!=NULL || tail2_2!=NULL){
			difference=Subtraction(tail2_2, tail1_2, difference);
			tail1_2=tail1_2->next;
			tail2_2=tail2_2->next;
		}
	}
	else if (ifminus==FALSE){/*subtract the second num from the first*/
		while (tail1_2!=NULL || tail2_2!=NULL){
			difference=Subtraction(tail1_2, tail2_2, difference);
			tail1_2=tail1_2->next;
			tail2_2=tail2_2->next;
		}
	}
	free_list(tail1copy);/*free memory*/
	free_list(tail2copy);


	/*cuting unneccesry zeros*/
	difference=CutZero(difference);
	sum=CutZero(sum);

	/*Print*/
	putchar('\n');
	print_listRec(tail1);/*print the numbers*/
	printf(" + ");
	print_listRec(tail2);
	printf(" = ");
	print_list(sum);/*print sum*/

	putchar('\n');
	print_listRec(tail1);/*print the numbers*/
	printf(" - ");
	print_listRec(tail2);
	printf(" = ");
	if(ifminus==TRUE)
		putchar('-');
	print_list(difference); /*print sum*/
	
	return 0;
}
Node* CreateList(char c, Node *tail)
{

 	Node *current_cell;
	if(c>'9' || c<'0')
	{
		printf("invalid input.bye!\n");
		exit(1);
	}
	/* allocate memory for new record */
  	if ((current_cell = (Node *)malloc (sizeof (Node))) == NULL)
	{
    		printf ("Cannot allocate memory\n");
    		exit (1);
  	}
	current_cell->num=c;/* store input in new record */
	current_cell->next=tail;
	tail=current_cell;
	return tail;
}
Node* CopyList(Node *tail1copy, Node *tail1)
{
	Node *p, *q;
	for(q=tail1; q!=NULL;  p=tail1copy, q=q->next)/*create storage*/
	{
		/* allocate memory for new record */
  		if ((p = (Node *)malloc (sizeof (Node))) == NULL)
		{
    			printf ("Cannot allocate memory\n");
    			exit (1);
  		}
		p->num='1';/* store input in new record */
		p->next=tail1copy;
		tail1copy=p;
	}
	/*store input*/
	for(q=tail1, p=tail1copy; p!=NULL; p=p->next, q=q->next)
		p->num=q->num;

	return tail1copy;
}
void print_list(Node *tail)
{
  	Node *p;
  	for (p=tail; p!=NULL; p=p->next){
    		printf ("%c", p->num);
  	}
  	printf ("\n");
}
int print_listRec(Node *tail)
{
  	Node *p;
	p=tail;
	if(p==NULL)
		return 1;
	if(p!=NULL)
		print_listRec(p->next);
    printf ("%c", p->num);
	return 1;
}
Node* Addition(Node *tail1, Node *tail2, Node *sum)
{
 	Node *current_cell;
	Node *current_cell2;
	Node *zero_cell;
  	/* allocate memory for new record */
  	if ((current_cell = (Node *)malloc (sizeof (Node))) == NULL)
	{
    		printf ("Cannot allocate memory\n");
    		exit (1);
  	}
	if((tail1->next!=NULL) && (tail2->next==NULL))
	{
		if ((zero_cell = (Node *)malloc (sizeof (Node))) == NULL)
		{
			printf ("Cannot allocate memory\n");
    		exit (1);
		}
		zero_cell->next=tail2->next;
		zero_cell->num='0';
		tail2->next=zero_cell;
	}
	else if((tail1->next==NULL) && (tail2->next!=NULL))
	{
		if ((zero_cell = (Node *)malloc (sizeof (Node))) == NULL)
		{
			printf ("Cannot allocate memory\n");
    		exit (1);
		}
		zero_cell->next=tail1->next;
		zero_cell->num='0';
		tail1->next=zero_cell;
	}
	
	current_cell->num=tail1->num+tail2->num-48;

	if( current_cell->num > '9' )
	{
		if(tail2->next==NULL)
		{
			current_cell->next=sum;
			sum=current_cell;
			if ((current_cell2 = (Node *)malloc (sizeof (Node))) == NULL)
			{
    		printf ("Cannot allocate memory\n");
    		exit (1);
			}
			current_cell2->next=sum;
			sum=current_cell2;
			current_cell2->num='1';
			current_cell->num-=10;
			return sum;
		}
		tail1->next->num+=1;
		current_cell->num-=10;
	}
	current_cell->next=sum;
	sum=current_cell;
	return sum;
}
Node* Subtraction(Node *tail1, Node *tail2, Node *difference)
{
	Node *current_cell;
	Node *zero_cell;
  	/* allocate memory for new record */
  	if ((current_cell = (Node *)malloc (sizeof (Node))) == NULL)
	{
    		printf ("Cannot allocate memory\n");
    		exit (1);
  	}
	if((tail1->next!=NULL) && (tail2->next==NULL))
	{
		if ((zero_cell = (Node *)malloc (sizeof (Node))) == NULL)
		{
			printf ("Cannot allocate memory\n");
    		exit (1);
		}
		zero_cell->next=tail2->next;
		zero_cell->num='0';
		tail2->next=zero_cell;
	}
	
	
	if( tail1->num < tail2->num )
	{
		tail1->next->num-=1;/*becaouse the addiction add one digot more*/
		tail1->num+=10;
	}
	current_cell->num=tail1->num-tail2->num+48;
	current_cell->next=difference;
	difference=current_cell;
	return difference;
}

void free_list(Node *list)
{
    	Node *p;
    	while (list!=NULL){
        	p=list->next;
        	free(list);
        	list=p;
    	}
}

int IfMinus(Node *list1, Node *list2)
{
	Node *p, *q;
	/*incase the number are the same*/
	p=list1;
	q=list2;
	
	if((list1->next==NULL) && (list2->next==NULL)){
		if(list1->num < list2->num)
				return TRUE;
			if(list1->num > list2->num)
				return FALSE;
	}
	while(p->num==q->num){
		if((q->next==NULL) && (p->next==NULL))
			return  FALSE;
		p=p->next;
		q=q->next;
	}
	
	if((list1->next==NULL) && (list2->next!=NULL))
		return TRUE;
	else if((list1->next!=NULL) && (list2->next==NULL))
		return FALSE;

	else if((list1->next->next==NULL) && (list2->next->next==NULL)){
		if(list1->next->num < list2->next->num)
			return TRUE;
		if(list1->next->num > list2->next->num)
			return FALSE;
		if(list1->next->num == list2->next->num){
			if(list1->num < list2->num)
				return TRUE;
			if(list1->num > list2->num)
				return FALSE;
		}		
	}
	else return IfMinus(list1->next, list2->next);
	return FALSE;
}

Node* CutZero(Node *list)
{
	Node *q;
	q=list;
	if ((list->num=='0') && (list->next==NULL)){
		return list;
		}
	else if(list->num!='0'){
		return list;}
	else if(list->num=='0')
	{
		list=list->next;
		free(q);
		return CutZero(list);
	}
	return list;
}
