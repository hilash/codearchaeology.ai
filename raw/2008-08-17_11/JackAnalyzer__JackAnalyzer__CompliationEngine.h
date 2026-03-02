#ifndef __COMPLIATIONENGINE_H__
#define __COMPLIATIONENGINE_H__

#include <iostream>
#include <iostream>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <errno.h>
#include "List.h"
#define N 1000
using namespace std;

// Class CompliationEngine. 

class CompliationEngine {
public:
	CompliationEngine(); //c'tor
	CompliationEngine(char* file_name2); //c'tor
	CompliationEngine::~CompliationEngine(); //d'tor

	void RunCompliationEngine(char* stop);

	void CompileClass(char* classline);
	void CompileClassVarDec(char* classvarline);
	void CompileSubroutine(char* subline);
	void CompileParameterList();
	void CompileVarDec(char* varline);
	void CompileStatements(char* stateline);
	void CompileLet(char* stateline);
	void CompileIf(char* stateline);
	void CompileWhile(char* stateline);
	void CompileDo(char* stateline);
	void CompileReturn(char* stateline);
	void CompileExpression(char* expressionline);
	void CompileTerm(char* termline);
	void CompileExpressionList();
	void CompileSubroutineCall(char* name);

	char* GetTokenType(char* line);
	char* GetToken(char* line);
	void PrintIndentedLine(char* line);

private:
	char input_file[1000]; // the tokenizer output
	char output_file[1000];
	FILE* stream;
	FILE* output;
	int Indent; //how many 'tabs' in the begining of new line
	List Class_Scope;
};


#endif //__COMPLIATIONENGINE_H__
