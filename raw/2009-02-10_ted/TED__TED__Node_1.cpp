#include "Node.h"

Node::Node()
{
	label=0;
	weight=0;
	heavy=false;	
}

Node::Node(int label1, int weight1, bool heavy1)
{
	label=label1;
	weight=weight1;
	heavy=heavy1;	
}

Node::Node(const Node& other)
{
	label=other.label;
	weight=other.weight;
	heavy=other.heavy;
}

Node::~Node()
{
}

void Node::PrintNode()
{
	cout << "label:  " << label << "\n";
	cout << "weight: " << weight << "\n";
	cout << "heavy:  " << heavy << "\n";
}