// start with one pass over the two trees that will mark
// for every node a pointer to its heaviest child.

#include <iostream>//node
#include "Node.h"//node

class Tree 
{
	int Size;			// Size of sub tree (0 = leave)
	Node* data;			// Node data
	Tree* left;			// pointer to left child
	Tree* right;		// pointer to right child
	Tree* heavyChild;	// pointer to the heavy child

	static int treeSize;		//size of the whole tree
	static Tree* HeavyPath[20];
	static Tree* TopLight[20];

public:
	Tree();
	Tree(Node* data1, Tree* left1, Tree* right1);
	Tree(const Tree& other);
	~Tree();

	int getSize() {return Size;};
	Tree* getLeft() {return left;};
	Tree* getRight() {return right;};

	void FindHeavyPath();	//mark the heaviest nodes & fill HeavyPath[n] & TopLight[n]
	int Postorder();		//postorder traversal

	// Print the arrays of HeavyPath and TopLight
	void PrintTreePTRarray(Tree* arr[]);
	void PrintHeavyPath() {PrintTreePTRarray(HeavyPath);};
	void PrintTopLight() {PrintTreePTRarray(TopLight);};
};