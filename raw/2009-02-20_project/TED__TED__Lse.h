#ifndef __LSE_H__
#define __LSE_H__

#include <iostream>
#include "Ilse.h"


//left subforest enumeration

class Lse 
{
	Forest* W;
	Tree*** a;

public:
	Lse(Tree* G);
	//~Lse();
	void find_Lse(Tree* G);
	void NumberVertex(Tree* G);
	void DeleteLeftVertex(int i,Tree* G, int* k);
	int j(int i) {return getWi(0)->getSize() - i - getWi(i)->getSize(); } ;
	Tree* getWi(int i) {return W->getTree(i);};
	void PrintW() {W->PrintForest();};

	

	
};

#endif //__LSE_H__