#ifndef __TRIE_H__
#define __TRIE_H__
#include <iostream>
#include <fstream>
#include <map>
using namespace std;

class Trie {
public:
	// functions with the Trei Tree
	Trie();
	void Init(char*);
	void Fin();
	void AddWord(char*);
	void Print();
	bool FindWord(char*);
	//functions with the Trei File
	void MakeTrieFile();
	void MakeTrieFile_(); // From trei
	void MakeTrieFileDic(); // From list of words
	bool FindWordInFile(char*);
	

private:
	static long lastline;
	static ifstream* Dictionary;
	static ofstream* TrieDictionary;	
	bool IsWord;
	long Address;
	map<char,Trie*> *Table;		// the table for it's sons
};

#endif
