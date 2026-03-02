#include "Permutation.h"

Permutation::Permutation(char* str, ifstream* file)
{
	lenght = strlen(str);

	string = new char[lenght+1];
	strcpy(string,str);

	IsInParm = new bool[lenght];
	Vstr.resize(lenght);
	for (int i=0; i<lenght; i++)
	{
		IsInParm[i]=false;
		Vstr[i] = string[i];
	}
	sort (Vstr.begin(), Vstr.end());

	dictionary = file;
}

void Permutation::print()
{
	for (int i=0; i<lenght; i++)
		cout << Vstr[i];
}

// calc all parmutations & search for them in the dictionary:
// kind of like how we search for sorted words (by lexical order) in the dictioray:
// for example: aaa abb abf df gh gl glll
// we first search where are the words that starts with 'a'. then search for 'aa'...
// and since the dictionary is by lexical order thats costs only one pass on the dic.
// that's not optimal - it would be optimal to creat a trie.
void Permutation::FindNPrint(int k)
// k - the current place in the parmutation array (string);
{
	if (k == lenght)	// then we created a parmutation
	{
		char* s = new char[lenght+1];
		strcpy(s,string);
		s[lenght]='\0';
		AllParm.push_back(s);
	}
	else
	{
		for (int i=0; i<lenght; i++)
		{
			if (IsInParm[i]==false)		// that means the the letter that is in the i'th place in the sorted array of letters "Vstr" hasnt been used in that parmutation
			{
				IsInParm[i]=true;
				string[k]=Vstr[i];
				FindNPrint(k+1);
				IsInParm[i]=false;
			}
		}
	}
}

bool Permutation::FindInDic()
{
	// search for (char* string) in (ifstream* dictionary);	
	vector<char*>::iterator it;
	
	char* buffer = new char[100];
	
	while(dictionary->getline(buffer,100))
	{
		for ( it=AllParm.begin() ; it < AllParm.end(); it++ )
		{
			if (strcmp(*it,buffer)==0)
				printf("%s\n",*it);
		}
	}

	return true;
}