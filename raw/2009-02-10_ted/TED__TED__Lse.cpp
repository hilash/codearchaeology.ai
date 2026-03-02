#include "Lse.h"



Lse::Lse(Tree* G)
{
	NumberVertex(G);
	find_Lse(G);
}

// give every vertex a number. preorder left to right
void Lse::NumberVertex(Tree* G)
{
	W.AddRightTree(G);
	if (G->getRight() != NULL)
		NumberVertex(G->getRight());
	if (G->getLeft() != NULL)
		NumberVertex(G->getLeft());

}

void Lse::find_Lse(Tree* G)
{
	for (int i=getWi(0)->getSize(); i>=0; i--)
		for (int k=j(i); k>=0; k--)
		{

		}

}