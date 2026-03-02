#ifndef __PERMUTATION_H__
#define __PERMUTATION_H__
#include <iostream>
#include <fstream>
#include <string.h>
#include <vector>
#include <algorithm>
using namespace std;

class Permutation {
public:
	Permutation(char*,ifstream*);
	Permutation();
	//~Permutation();
	void print();
	void FindNPrint(int);
	bool FindInDic(); // need to be in a seperate class but i'm lazy

private:
	int lenght;
	char* string;
	bool* IsInParm;
	vector<char> Vstr;
	ifstream* dictionary;
	vector<char*> AllParm;
};

#endif
