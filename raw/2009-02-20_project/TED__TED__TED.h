// start with one pass over the two trees that will mark
// for every node a pointer to its heaviest child.

#ifndef __TED_H__
#define __TED_H__

#include <iostream>//node
#include "Lse.h"

class TED
{

public:
	TED(Tree* F,Tree* G);
	~TED();

	void TED_pro(Tree* F,Tree* G);
};

#endif //__TREE_H__