#ifndef __NODE_H__
#define __NODE_H__

#include <iostream>

using namespace std;

class Node 
{
	int label;
	int weight;
	bool heavy;			// is in heavy path

public:
	Node();
	Node(int label1, int weight1, bool heavy1);
	Node(const Node& other);			// c'ctor
	~Node();

	int getLabel() {return label;};
	int getWeight() {return weight;};
	bool getHeavy() {return heavy;};

	void setLabel(int label1) {label=label1;};
	void setWeight(int weight1) {weight=weight1;};
	void setHeavy(bool heavy1) {heavy=heavy1;};

	void PrintNode();

};

#endif //__NODE_H__*/