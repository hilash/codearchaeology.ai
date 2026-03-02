#include <iostream>
#include "JackAnalyzer.h"
#include "JackTokenizer.h"
using namespace std;

// Class JackAnalyzer functions

JackAnalyzer::JackAnalyzer() //c'tor
{
	argc=0;
	argv=NULL;
}

JackAnalyzer::JackAnalyzer(int argc2, char *argv2[]) //c'tor
{
	argc=argc2;
	argv=argv2;
}

void JackAnalyzer::RunJackAnalyzer()
{
	RunJackTokenizer(argv[1]);
}

void JackAnalyzer::RunJackTokenizer(char* file_name)
{
	JackTokenizer tokenizer(file_name);
	tokenizer.RunJackTokenizer();
}