// start with one pass over the two trees that will mark
// for every node a pointer to its heaviest child.

#ifndef __TREE_H__
#define __TREE_H__

#include <iostream>//node
#include <vector> 
#include "Node.h"//node

class Tree 
{
	int Size;			// Size of sub tree (0 = leave)
	int Wi;
	Node* data;			// Node data
	Tree* left;			// pointer to left child
	Tree* right;		// pointer to right child
	Tree* heavyChild;	// pointer to the heavy child

	static int treeSize;		//size of the whole tree
	static vector<Tree*> HeavyPath; 
	static vector<Tree*> TopLight; 

public:
	Tree();
	Tree(Node* data1, Tree* left1, Tree* right1);
	Tree(const Tree& other);
	~Tree();

	int getSize() {return Size;};
	Node* getData() {return data;};
	Tree* getLeft() {return left;};
	Tree* getRight() {return right;};

	int getWi() {return Wi;};
	void SetWi(int x) {Wi=x;};

	void FindHeavyPath();	//mark the heaviest nodes & fill HeavyPath[n] & TopLight[n]
	int Postorder();		//postorder traversal

	// Print the arrays of HeavyPath and TopLight
	void PrintHeavyPath();
	void PrintTopLight();
	void PrintTree();
};

#endif //__TREE_H__