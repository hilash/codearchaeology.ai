// graph problem,
// Hila Shmuel
// 
// 13.5.2010

#include <iostream>
#include <bitset>
#include <set>
#include <vector>
#include <list>
using namespace std;
#define N 10000000

vector<set<int>> vertex;
vector<set<int>> black_child;  // number all colored close children if i'm black
vector<int> black; // number all colored children in my sub tree if i'm black (including me)
vector<int> white; // number all colored children in my sub tree if i'm white

int n,m;

void print_black(int v);
void print_white(int v);
void makeTree(int v);
void foo(int v);

int main()
{
	cin >> n >> m;
	vertex.resize(n+1);
	black_child.resize(n+1);
	black.resize(n+1);
	white.resize(n+1);

	for (int i=0; i<m; i++)
	{
		int u,v;
		cin >> u >> v;
		vertex[u].insert(v);
		vertex[v].insert(u);
	}

	makeTree(1);
	foo(1);
	if (black[1]<white[1])
		print_black(1);
	else
		print_white(1);
	return 0;
}

void makeTree(int v)
{
	for (set<int>::iterator k = vertex[v].begin(); k!=vertex[v].end(); k++)
		vertex[*k].erase(v);
	for (set<int>::iterator k = vertex[v].begin(); k!=vertex[v].end(); k++)
		makeTree(*k);
}

void foo(int v)
{
	if (vertex[v].size()==0) // a leaf
	{
		black[v] = 1;
		white[v] = 0;
		return;
	}

	for (set<int>::iterator k = vertex[v].begin(); k!=vertex[v].end(); k++)
	{
		foo(*k);
	}
	// case i'm (v) white, all my child must be black
	int sum = 0;
	for (set<int>::iterator k = vertex[v].begin(); k!=vertex[v].end(); k++)
	{
		sum +=  black[*k];
	}
	white[v] = sum;

	// case i'm (v) black, not all my child must be black, will take the min
	sum = 0;
	for (set<int>::iterator k = vertex[v].begin(); k!=vertex[v].end(); k++)
	{
		if (black[*k]<white[*k])
		{
			black_child[v].insert(*k);
			sum += black[*k];
		}
		else
		{
			sum += white[*k];
		}
	}
	black[v] = sum + 1;
}

void print_white(int v)
{
	// if i'm white, i don't need to print myself.
	// but, all my children are black
	for (set<int>::iterator k = vertex[v].begin(); k!=vertex[v].end(); k++)
	{
		print_black(*k);
	}
}
void print_black(int v)
{
	// if i'm black, i  print myself.
	cout << v << endl;
	for (set<int>::iterator k = vertex[v].begin(); k!=vertex[v].end(); k++)
	{
		if (black_child[v].count(*k)==1) // k is a black child
			print_black(*k);
		else
			print_white(*k);
	}
}