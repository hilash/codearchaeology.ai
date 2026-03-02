#include "Forest.h"

Forest::Forest(int arrSize)
{
	Frst = new Tree*[arrSize];
	int j = sizeof(Tree*);
	Tree** tmp = Frst;
	/*for (i=0; i<arrSize-1; i++)
	{
		*tmp = NULL;
		tmp+=j;
	}*/
	i=0;
}

void Forest::AddRightTree(Tree* T)
{
	*(Frst+(i++)*sizeof(Tree*)) = T; 
}

void Forest::PrintForest()
{
	Tree** ptr = Frst;
	int j = sizeof(Tree*);
	/*while (*ptr!=NULL)
	{
		(*ptr)->getData()->PrintNode();
		ptr+=j;
	}*/
	(*ptr)->getData()->PrintNode();
		ptr+=j;
		(*ptr)->getData()->PrintNode();

}