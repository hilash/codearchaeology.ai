// start with one pass over the two trees that will mark
// for every node a pointer to its heaviest child.

#include <iostream>//node
#include "Node.h"//node
#include "Tree.h"//node


int main()
{
	Tree A(new Node(1,0,0),NULL,NULL);
	Tree C(new Node(3,0,0),NULL,NULL);
	Tree E(new Node(5,0,0),NULL,NULL);
	Tree H(new Node(7,0,0),NULL,NULL);
	Tree I(new Node(8,0,0),&H,NULL);
	Tree G(new Node(9,0,0),NULL,&I);
	Tree D(new Node(4,0,0),&C,&E);
	Tree B(new Node(2,0,0),&A,&D);	
	Tree F(new Node(6,0,0),&B,&G);

	F.FindHeavyPath();
	F.PrintHeavyPath();
	cout << "\n";
	F.PrintTopLight();

	
	// postorder results
	//A, C, E, D, B, H, I, G, F
	//1, 3, 5, 4, 2, 7, 8, 9, 6

	return 0;
}