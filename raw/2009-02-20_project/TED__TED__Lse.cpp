#include "Lse.h"



Lse::Lse(Tree* G)
{
	int rows,cols;
	rows=cols=G->getSize(); // N*N matrix
	a=new Tree**[rows];
	for(int i=0;i<rows;i++)
	*(a+i)=new Tree*[cols];
	for (int p = 0; p<rows; p++)
		for (int q = 0; q<cols; q++)
			a[p][q] = NULL;
	W = new Forest();
	NumberVertex(G);
	find_Lse(G);
}

// give every vertex a number. preorder left to right
void Lse::NumberVertex(Tree* G)
{
	static int i = 0;
	W->AddRightTree(G);
	G->SetWi(i++);
	if (G->getRight() != NULL)
		NumberVertex(G->getRight());
	if (G->getLeft() != NULL)
		NumberVertex(G->getLeft());
	return;
}

void Lse::find_Lse(Tree* G)
{
	for (int i=getWi(0)->getSize(); i>=0; i--)
	{
		int k=0;
		DeleteLeftVertex(i,G,&k);
		cout << endl;
	}
}

void Lse::DeleteLeftVertex(int i,Tree* G, int* k)
{
	if (G->getWi() >= i)
	{
		cout << ++(*k);
		//a[i][*k] = G;/////////// not working. maybe to switch back to vectors?
						/// to check it.
	}

	if (G->getLeft() != NULL)
		DeleteLeftVertex(i,G->getLeft(),k);

	if (G->getRight() != NULL)
		DeleteLeftVertex(i,G->getRight(),k);

	return;	
}
