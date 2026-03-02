#ifndef __ILSE_H__
#define __ILSE_H__

#include <iostream>
#include "Forest.h"

enum Dir { LEFT = 0, RIGHT = 1};

//intermediate left subforest enumeration

class  Ilse // polymorfisem, vectors in c++
{
	Forest* Frst;
public:
	// ILse gets two trees, returns a Forset - array of pointers to trees (deleted vertex);
	Ilse() {Frst = new Forest();};
	~Ilse() {Frst->~Forest();};
	void find_Ilse(Tree* Fvp, Tree *Fvp1, Dir d = LEFT);
	Forest* get_Ilse() {return Frst;};
	Forest* get_Fk(int k);
};

#endif //__ILSE_H__