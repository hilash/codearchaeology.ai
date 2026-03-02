#ifndef __LSE_H__
#define __LSE_H__

#include <iostream>
#include "Forest.h"

enum Dir { LEFT = 0, RIGHT = 1};

//intermediate left subforest enumeration

class Lse // polymorfisem, vectors in c++
{
	Forest W;
	vector<vector<Tree*> >  *items;

public:
	Lse(Tree* G);
	//~Lse();
	void find_Lse(Tree* G);
	void NumberVertex(Tree* G);
	int j(int i) {return getWi(0)->getSize() - i - getWi(i)->getSize(); } ;
	Tree* getWi(int i) {return W.getTree(i);};
	void PrintW() {W.PrintForest();};

	
};

#endif //__LSE_H__