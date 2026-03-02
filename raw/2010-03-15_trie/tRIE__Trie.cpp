#include "Trie.h"

long Trie::lastline=0;
ifstream* Trie::Dictionary=NULL;
ofstream* Trie::TrieDictionary=NULL;

Trie::Trie()
{
	IsWord = false;
	Address = 0;
	Table = new map<char,Trie*>;
}

void Trie::Init(char* dicName)
{
	Dictionary = new ifstream;
	TrieDictionary = new ofstream;
	Dictionary->open(dicName);
	TrieDictionary->open("Dic.txt");
}

void Trie::Fin()
{
	Dictionary->close();
	TrieDictionary->close();
}

void Trie::AddWord(char* Word)
{
	if ((*Table).find(*Word)==(*Table).end()) // if we didn't came across that letter before
			((*Table)[*Word]) = new Trie();
	if (*(Word+1)=='\0')
		((*Table)[*Word])->IsWord=true;
	else ((*Table)[*Word])->AddWord(Word+1);
	return;
}

void Trie::Print()
{
	map<char,Trie*>::iterator it;

	
	for ( it=Table->begin() ; it != Table->end(); it++ )
	{
		printf("%c\n",it->first);
		if (it->second->IsWord==true)
			printf("*\n");
		it->second->Print();
	}
}

bool Trie::FindWord(char* Word)
{
	if ((*Table).find(*Word)==(*Table).end()) // if we didn't came across that letter before
			return false;
	else if ( (*(Word+1)=='\0') && (((*Table)[*Word])->IsWord==true) )
		return true;
	else return ((*Table)[*Word])->FindWord(Word+1);
}

void Trie::MakeTrieFile()
{
	map<char,Trie*>::iterator it,it2;

	for ( it=Table->begin() ; it != Table->end(); it++ )
	{
		it->second->MakeTrieFile();
		it->second->Address=TrieDictionary->tellp();
		*TrieDictionary << it->first << " " << it->second->IsWord << " : ";
		for ( it2=it->second->Table->begin() ; it2 != it->second->Table->end(); it2++ )
		{
			(*TrieDictionary) << it2->first << " " << it2->second->Address << " ";
		}
		*TrieDictionary << endl;
		lastline=TrieDictionary->tellp();
	}
}

void Trie::MakeTrieFile_()
{
	map<char,Trie*>::iterator it;
	MakeTrieFile();
	// print the first table
	for ( it=Table->begin() ; it != Table->end(); it++ )
	{
		*TrieDictionary << it->first << " " << it->second->Address << " ";
	}

	TrieDictionary->close();
}

bool Trie::FindWordInFile(char* Word)
{
	char letter;
	long address;
	int k=0;
	int isword;
	char* buf;

	ifstream* TrieFile = new ifstream;
	TrieFile->open("Dic.txt");
	TrieFile->seekg(lastline,ios::beg);

	buf = new char[1000];
	TrieFile->getline(buf,1000);
	while ( (*(buf+k)!=*Word) && (*(buf+k)!='\0') )
		k++;
	if (*(buf+k)=='\0')
		return false;
	if (*(buf+k)==*Word)
	{
		TrieFile->seekg(lastline+k+1,ios::beg);
		*TrieFile >> address;

		// search the dictionary trie file letter by letter
		while(1)
		{
			TrieFile->seekg(address,ios::beg);
			*TrieFile >> letter >> isword;
			if (*(Word+1)=='\0')
				if (isword==1)
					return true;
				else
					return false;
			*TrieFile >> letter;
			Word++;
			// search the line
			while ((letter!=*Word) && (letter!='\n')&& (letter!='\0')&& (letter!=10))
			{
				letter=TrieFile->get();
				if (('a'<=letter) && (letter<='z') )
					*TrieFile >> address;
			}
			if ((letter=='\n')|| (letter=='\0') || (letter==10))
				return false;
		}
	}
	TrieFile->close();
	return true;
}

void Trie::MakeTrieFileDic()
{
	char buf[100];
	long i =0;
	Init("dic-0294.txt");
	while(Dictionary->getline(buf,100))
	{
		if((i++)%10000==0)
			cout << buf << endl;
		this->AddWord(buf);
	}
	//t.AddWord("abyx");
	//t.Print();
	MakeTrieFile_();
	Fin();
}