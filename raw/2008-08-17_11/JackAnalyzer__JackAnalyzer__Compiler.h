#ifndef __COMPLIER_H__
#define __COMPLIER_H__

#include <iostream>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <errno.h>
#include "List.h"
#define N 1000
using namespace std;

// Class CompliationEngine. 

class Compiler {
public:
	Compiler(); //c'tor
	Compiler(char* file_name2); //c'tor
	~Compiler(); //d'tor

	void RunCompiler();
	void Handle(char* token, char* token_type);
	char* GetToken(char* line);
	char* GetTokenType(char* line);
	void CompileClass();
	void CompileClassVarDec();
	void CompileSubroutineDec();
	void CompileParameterList(List* SubList);
	void CompileVarDec(List* SubList);
	void CompileStatements(List* SubList);
	void CompileLet(List* SubList);
	void CompileIf(List* SubList);
	void CompileWhile(List* SubList);
	void CompileDo(List* SubList);
	void CompileReturn(List* SubList);
	void CompileSubroutineCall(List* SubList);
	void CompileExpression(List* SubList);
	void CompileTerm(List* SubList);
	int CompileExpressionList(List* SubList);

private:
	char input_file[1000]; // the tokenizer output
	char output_file[1000];// the vm file
	char ClassName[200];
	char subroutine_returned_type[200];
	long ptr;
	FILE* stream;
	FILE* output;
};


#endif //__COMPLIER_H__
