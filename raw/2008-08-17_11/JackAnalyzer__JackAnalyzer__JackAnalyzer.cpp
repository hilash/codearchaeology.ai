#include <stdio.h>
#include <windows.h>
#include <iostream>
#include <string>

#include "JackAnalyzer.h"
#include "JackTokenizer.h"
#include "CompliationEngine.h"
#include "Compiler.h"
#define BUFSIZE MAX_PATH

using namespace std;

// Class JackAnalyzer functions

JackAnalyzer::JackAnalyzer() //c'tor
{
	argc=0;
	argv=NULL;
}

JackAnalyzer::JackAnalyzer(int argc2, TCHAR **argv2) //c'tor
{
	argc=argc2;
	argv=argv2;
}

// in case where the user wants to compile a directory, handle files
void JackAnalyzer::HandleFiles()
{
	char file_name[1000];
	WIN32_FIND_DATA findFileData;
	HANDLE hFind = FindFirstFile((LPCWSTR)"*", &findFileData);
	while(FindNextFile(hFind, &findFileData)) {
		WcharToString(file_name,findFileData.cFileName);
		if (IsJackFile(file_name))
		{
			RunJackAnalyzer(findFileData.cFileName);
		}
	}
	FindClose(hFind);
}

//comile one file from jack to xml
void JackAnalyzer::RunJackAnalyzer(TCHAR* pTchar)
{
	char name[1000],*tmp;
	WcharToString(name,pTchar);
	tmp = strtok(name,".\n");
	RunJackTokenizer(tmp);
	RunCompliationEngine(tmp);
	//RunCompiler(tmp);
}

//Divide file to tokens
void JackAnalyzer::RunJackTokenizer(char* file_name)
{
	JackTokenizer tokenizer(file_name);
	tokenizer.RunJackTokenizer();
	tokenizer.~JackTokenizer();
}

// compile the file according to the previous tokens file
void JackAnalyzer::RunCompliationEngine(char* file_name)
{
	CompliationEngine CE(file_name);
	CE.RunCompliationEngine("");
	CE.~CompliationEngine();
}

void JackAnalyzer::RunCompiler(char* file_name)
{
	Compiler comp(file_name);
	comp.RunCompiler();
	comp.~Compiler();
}

// get file name see if it has ending ".JACK".
bool JackAnalyzer::IsJackFile(char* file_name)
{
	char * pch;
	pch=strrchr(file_name,'.');
	if (pch == NULL)
		return false;
	if ( (!strncmp(pch,".JACK",5)) || (!strncmp(pch,".Jack",5)) || (!strncmp(pch,".jack",5)) )
		return true;
	else return false;
}
 
