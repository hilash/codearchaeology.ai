// basic file operations
#include <iostream>
#include <fstream>
#include "Permutation.h"
#include "Trie.h"
using namespace std;

bool check(char* word)
{
	while (*word!='\0')
	{
		if (!(( (65<=(*word)) && ((*word)<=90) ) || ( (97<=(*word)) && ((*word)<=122) )))
			return false;
		if ((65<=(*word)) && ((*word)<=90) )
			(*word)=(*word)+32;
		word++;
	}
	return true;
}

int main () {
	//ifstream myfile;
	char a[100];
	//myfile.open ("dic-0294.txt");

	Trie t;
	
	//t.MakeTrieFileDic();

	/*cout << "Enter Word: ";
	cin >> a;

	while (check(a)==false)
	{
		cout << "chose another word, containg only letters: a..zA...Z" << endl;
		cin >> a;
	}
	cout << endl << "meaningful parmutations of the word:" << endl;*/
	
	//Permutation vari(a,&myfile);
	//vari.FindNPrint(0);
	//vari.FindInDic();
	//myfile.close();

	cout << t.FindWordInFile("fire") << t.FindWordInFile("rife") << t.FindWordInFile("julsdf")  << endl;
	return 0;
}