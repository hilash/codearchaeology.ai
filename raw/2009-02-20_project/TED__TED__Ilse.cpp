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

/// FIX ITTTTTTT
Forest*  Ilse::get_Fk(int k) /// O(n) time .... check it with oren!!! otherwise it's O(n^2) space
{
	// since the vertexes are in revrsed order, do everything backward

	Forest* Fk = new Forest();
	Tree* tmp1 = new Tree;
	
	if ( k == Frst->Size()-1 )
	{
		Fk->AddRightTree(Frst->getTree(0));
		return Fk;
	}
	//else if ( k == 0 )
	//{
	//	Fk->AddRightTree(Frst->getTree(Frst->Size()-1));
	//	return Fk;
	//}

	for(int i = Frst->Size()-1 ; i >= Frst->Size()-1-k ; i-- )
	{
		if ( Fk->Size() == 0)
			Fk->AddRightTree(Frst->getTree(Frst->Size()-1));
		else
		{
			cout << endl;
			Frst->getTree(i)->getLeft()->getData()->PrintNode();
			//Frst->GetRightTree()->getData()->PrintNode();
			if (Frst->getTree(i)->getLeft() == Frst->GetRightTree())
				Frst->RemoveRightTree();

			if (Frst->getTree(i)->getRight() == Frst->GetRightTree())
				Frst->RemoveRightTree();

			Fk->AddRightTree(Frst->getTree(i));		
		}
	}
	
	return Fk;
}