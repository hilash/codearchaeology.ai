#include "Ilse.h"

// a Preorder traversal
// only if Fvp is father of Fvp1
void Ilse::find_Ilse(Tree* Fvp, Tree *Fvp1, Dir d )
{
	static bool flag = 1;
	Frst->AddRightTree(Fvp);
	if (Fvp == Fvp1)
	{
		flag = 0;
		return;
	}
	if ( d == LEFT)
	{
		if ( Fvp->getLeft()!=NULL && flag)
			find_Ilse(Fvp->getLeft(), Fvp1,d);
		if ( Fvp->getRight()!=NULL && flag)
			find_Ilse(Fvp->getRight(), Fvp1,d);
	}

	else if ( d == RIGHT)
	{
		if ( Fvp->getRight()!=NULL && flag)
			find_Ilse(Fvp->getRight(), Fvp1,d);
		if ( Fvp->getLeft()!=NULL && flag)
			find_Ilse(Fvp->getLeft(), Fvp1,d);
	}
}

Forest*  Ilse::get_Fk(int k)
{
	return Frst;
}