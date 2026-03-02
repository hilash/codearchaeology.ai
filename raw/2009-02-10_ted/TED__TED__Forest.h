#include "Tree.h"

class  Forest
{
	Tree** Frst;		// a pointer to an array of pointers to trees
	int i;				// where is the rightest tree pointer
			
public:
	Forest(int arrSize);
	~Forest() {delete[] Frst;};
	//Tree** getForest() {return Frst;};

	// a pointer to the left tree in the forest. the first cell in the array.
	//Tree* getLeft() {return *Frst};
	void AddRightTree(Tree* T);
	void PrintForest();			// Print the roots in the Forest

	
};
