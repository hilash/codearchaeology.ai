#ifndef __FOREST_H__
#define __FOREST_H__


#include "Tree.h"
#include <vector> 
using namespace std; 


class  Forest
{
	vector<Tree*> Frst;
	vector<Tree*>::iterator iter;
			
public:
	Forest() {iter = Frst.begin();};
	~Forest() {Frst.~vector();};
	

	void AddRightTree(Tree* T) {Frst.push_back(T);};
	//void AddLeftTree(Tree* T) {Frst.insert(iter,T);};
	void PrintForest();			// Print the roots in the Forest
	Tree* getTree (int i) {return Frst[i];};

};

#endif //__FOREST_H__
