#include <iostream>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <errno.h>

#include "CompliationEngine.h"
#define N 1000
using namespace std;

// Class JackAnalyzer functions

CompliationEngine::CompliationEngine() //c'tor
{
	Indent=0;
}

CompliationEngine::CompliationEngine(char* file_name2) //c'tor
{
char tmp[1000];
	//get the file name - "XXX.xml".
	strcpy(tmp,file_name2);
	strcpy(input_file, tmp);
	strcpy(output_file, tmp);
	strcat(input_file, "T.xml");
	strcat(output_file, ".vm");

	//open the input and output files for reading&writing.
	fopen_s(&stream, input_file, "r");
	fopen_s(&output, output_file, "w");


	Indent=0;
}

CompliationEngine::~CompliationEngine() //d'tor
{
	fclose(stream);
	fclose(output);
}

// go throught each line and decide how to tranlate it.
void CompliationEngine::RunCompliationEngine(char* stop)
{

	char line[1000];
	static bool flag=true;

	if (flag == true) {
		fgets( line,N, stream ); //first line - tokens.
		flag = false;
	}

	while ( (fgets( line,N, stream )!=NULL) && ( strncmp(line,"</tokens>",9) != 0 ))
	{
		if ( strcmp(GetToken(line),"class") == 0 )
			CompileClass(line);
		else if ( ( strcmp(GetToken(line),"static") == 0 ) || ( strcmp(GetToken(line),"field") == 0 ) )
			CompileClassVarDec(line);
		else if ( ( strcmp(GetToken(line),"constructor") == 0 ) || ( strcmp(GetToken(line),"function") == 0 ) 
			|| ( strcmp(GetToken(line),"method") == 0 ))
			CompileSubroutine(line);
		else PrintIndentedLine(line);
		if ( strcmp(stop,GetToken(line))==0 )
			break;
	}
	if ( strncmp(line,"</tokens>",9) == 0 )
	{
		flag = true;
		return;
	}
}
void CompliationEngine::CompileClass(char* classline)
{
	PrintIndentedLine("<class>");
	Indent++;
	PrintIndentedLine(classline); // <keyword> class </keyword>
	RunCompliationEngine("");
	Indent--;
	PrintIndentedLine("</class>");
}

void CompliationEngine::CompileClassVarDec(char* classvarline)
{
	PrintIndentedLine("<classVarDec>");
	Indent++;
	PrintIndentedLine(classvarline); //
	RunCompliationEngine(";");
	Indent--;
	PrintIndentedLine("</classVarDec>");
}

void CompliationEngine::CompileSubroutine(char* subline)
{
	char line[N];
	PrintIndentedLine("<subroutineDec>");
	Indent++;
	PrintIndentedLine(subline); 
	RunCompliationEngine("("); // print parameter list.
	CompileParameterList();
	RunCompliationEngine(")");
	PrintIndentedLine("<subroutineBody>");
	Indent++;
	RunCompliationEngine("{");
	while ( (fgets( line,N, stream )!=NULL ) && ( strcmp(GetToken(line),"var") == 0 ) )
		CompileVarDec(line);
	CompileStatements(line);
	RunCompliationEngine("}");
	Indent--;
	PrintIndentedLine("</subroutineBody>");
	Indent--;
	PrintIndentedLine("</subroutineDec>");
}

void CompliationEngine::CompileParameterList()
{
	char line[N];
	long file_pointer;
	bool flag=false;

	PrintIndentedLine("<parameterList>");
	Indent++;
	
	file_pointer = ftell(stream);
	fgets(line,N,stream);
	if (  strcmp(GetToken(line),")") == 0 ) // empty list
	{
		fseek(stream,file_pointer,SEEK_SET);
	}
	else {
		do {
			PrintIndentedLine(line);// type
			fgets(line,N,stream);
			PrintIndentedLine(line);// varName

			file_pointer = ftell(stream);
			fgets(line,N,stream);

			if (  strcmp(GetToken(line),")") == 0 ) 
			{
				flag=false;
				fseek(stream,file_pointer,SEEK_SET);
			}
			else if (  strcmp(GetToken(line),",") == 0 ) 
			{
				flag=true;
				PrintIndentedLine(line);
				fgets(line,N,stream);
			}
		}
		while(flag);
	}

	Indent--;
	PrintIndentedLine("</parameterList>");
}

void CompliationEngine::CompileVarDec(char* varline)
{
	PrintIndentedLine("<varDec>");
	Indent++;
	PrintIndentedLine(varline); // <keyword> var </keyword>
	RunCompliationEngine(";");
	Indent--;
	PrintIndentedLine("</varDec>");
}

void CompliationEngine::CompileStatements(char* stateline)
{
	char line[N];
	long file_pointer;
	strcpy(line,stateline);
	PrintIndentedLine("<statements>");
	Indent++;
	while ( strcmp(GetToken(line),"}") != 0 ){
		if ( strcmp(GetToken(line),"let") == 0 )
			CompileLet(line);
		else if ( strcmp(GetToken(line),"if") == 0 )
			CompileIf(line);
		else if ( strcmp(GetToken(line),"while") == 0 )
			CompileWhile(line);
		else if ( strcmp(GetToken(line),"do") == 0 )
			CompileDo(line);
		else if ( strcmp(GetToken(line),"return") == 0 )
			CompileReturn(line);

		file_pointer=ftell(stream);
		fgets( line,N, stream );
	}
	Indent--;
	PrintIndentedLine("</statements>");
	fseek(stream,file_pointer,SEEK_SET);
}
void CompliationEngine::CompileLet(char* stateline)
{
	char line[N];
	PrintIndentedLine("<letStatement>");
	Indent++;
	PrintIndentedLine(stateline);	// <keyword> let </keyword>
	fgets( line,N, stream );		// varName
	PrintIndentedLine(line);
	fgets( line,N, stream );		// check for expressions.
	if ( strcmp(GetToken(line),"[") == 0 )
	{
		PrintIndentedLine(line);
		fgets( line,N, stream );	
		CompileExpression(line);
		fgets( line,N, stream );
		PrintIndentedLine(line);
		fgets( line,N, stream );	
		PrintIndentedLine(line);	// =
	}
	else PrintIndentedLine(line);	// =
	fgets( line,N, stream );
	CompileExpression(line);
	RunCompliationEngine(";");
	Indent--;
	PrintIndentedLine("</letStatement>");
}

void CompliationEngine::CompileIf(char* stateline)
{
	char line[N];
	long file_pointer;
	PrintIndentedLine("<ifStatement>");
	Indent++;
	PrintIndentedLine(stateline);	//if
	RunCompliationEngine("(");		// (
	fgets(line,N,stream);			// expression
	CompileExpression(line);
	RunCompliationEngine("{");		// ) {
	fgets(line,N,stream);	
	CompileStatements(line);		// Statements
	RunCompliationEngine("}");		// }
	file_pointer = ftell (stream);
	fgets(line,N,stream);
	if ( strcmp(GetToken(line),"else") == 0 ) // else
	{
		PrintIndentedLine(line);
		RunCompliationEngine("{");		// {
		fgets(line,N,stream);	
		CompileStatements(line);		// Statements
		RunCompliationEngine("}");		// }
	}
	else fseek(stream,file_pointer,SEEK_SET);
	Indent--;
	PrintIndentedLine("</ifStatement>");
}

void CompliationEngine::CompileWhile(char* stateline)
{
	char line[N];
	PrintIndentedLine("<whileStatement>");
	Indent++;
	PrintIndentedLine(stateline);	//while
	RunCompliationEngine("(");		// (
	fgets(line,N,stream);			// expression
	CompileExpression(line);
	RunCompliationEngine("{");		// ) {
	fgets(line,N,stream);	
	CompileStatements(line);		// Statements
	RunCompliationEngine("}");	
	Indent--;
	PrintIndentedLine("</whileStatement>");
}

void CompliationEngine::CompileDo(char* stateline)
{
	char line[N];
	PrintIndentedLine("<doStatement>");
	Indent++;
	PrintIndentedLine(stateline);
	fgets(line,N,stream);
	CompileSubroutineCall(line);
	RunCompliationEngine(";");
	Indent--;
	PrintIndentedLine("</doStatement>");
}

void CompliationEngine::CompileReturn(char* stateline)
{
	char line[N];
	long file_pointer;
	PrintIndentedLine("<returnStatement>");
	Indent++;
	PrintIndentedLine(stateline);
	file_pointer = ftell(stream);
	fgets(line,N,stream);
	if ( strcmp(GetToken(line),";") != 0 )
		CompileExpression(line);
	else fseek(stream,file_pointer,SEEK_SET);
	RunCompliationEngine(";");
	Indent--;
	PrintIndentedLine("</returnStatement>");
}

void CompliationEngine::CompileExpression(char* expressionline)
{
	char line[N];
	long file_pointer;
	bool flag=false;
	PrintIndentedLine("<expression>");
	Indent++;
	CompileTerm(expressionline);
	do {
		file_pointer = ftell(stream);
		fgets(line,N,stream);
		if ( ( strcmp(GetToken(line),"+") == 0 ) ||
			 ( strcmp(GetToken(line),"-") == 0 ) ||
		     ( strcmp(GetToken(line),"*") == 0 ) ||
			 ( strcmp(GetToken(line),"/") == 0 ) ||
			 ( strcmp(GetToken(line),"&") == 0 ) ||
			 ( strcmp(GetToken(line),"|") == 0 ) ||
		     ( strcmp(GetToken(line),"<") == 0 ) ||
		     ( strcmp(GetToken(line),">") == 0 ) ||
		     ( strcmp(GetToken(line),"=") == 0 ) ||
			 ( strcmp(GetToken(line),"&lt;") == 0 ) ||
		     ( strcmp(GetToken(line),"&gt;") == 0 ) ||
		     ( strcmp(GetToken(line),"&amp;") == 0 ) )
		{
			flag=true;
			PrintIndentedLine(line);
			fgets(line,N,stream);
			CompileTerm(line);
		}
		else flag=false;
	}
	while (flag);
	Indent--;
	PrintIndentedLine("</expression>");
	fseek(stream,file_pointer,SEEK_SET);
}

void CompliationEngine::CompileTerm(char* termline)
{
	char line[N];
	long file_pointer;
	PrintIndentedLine("<term>");
	Indent++;
	// unaryOp term
	if ( ( strcmp(GetToken(termline),"-") == 0 ) || ( strcmp(GetToken(termline),"~") == 0 ) )
	{
		PrintIndentedLine(termline);
		fgets(line,N,stream);
		CompileTerm(line);
	}
	// '('expression')'
	else if ( strcmp(GetToken(termline),"(") == 0 )
	{
		PrintIndentedLine(termline);
		fgets(line,N,stream); 
		CompileExpression(line);
		fgets(line,N,stream); // ')'
		PrintIndentedLine(line);
	}
	//  integerConstant/stringConstant/keywordConstant
	else if (	( strcmp(GetTokenType(termline),"integerConstant") == 0 ) ||
				( strcmp(GetTokenType(termline),"stringConstant")  == 0 ) ||
				( strcmp(GetToken(termline),"true")   == 0 ) ||
				( strcmp(GetToken(termline),"false")  == 0 ) ||
				( strcmp(GetToken(termline),"null")   == 0 ) ||
				( strcmp(GetToken(termline),"this")   == 0 ) ) PrintIndentedLine(termline);

	// distinguish between variable,array and subroutine call.
	else {
		file_pointer = ftell(stream);
		fgets(line,N,stream);
		//array
		
		if ( strcmp(GetToken(line),"[") == 0 )
		{
			PrintIndentedLine(termline);// varName
			PrintIndentedLine(line);	// [
			fgets(line,N,stream);		// expression
			CompileExpression(line);
			fgets(line,N,stream);		// ]
			PrintIndentedLine(line);
		}
		//subroutine call 
		if ( ( strcmp(GetToken(line),"(") == 0 ) || ( strcmp(GetToken(line),".") == 0 ) )
		{
			fseek(stream,file_pointer,SEEK_SET);
			CompileSubroutineCall(termline);
		}
		// varname
		else {
			fseek(stream,file_pointer,SEEK_SET);
			PrintIndentedLine(termline);
		}
	}
	Indent--;
	PrintIndentedLine("</term>");
}

void CompliationEngine::CompileExpressionList()
{
	char line[N];
	long file_pointer;
	bool flag=false;

	PrintIndentedLine("<expressionList>");
	Indent++;
	
	file_pointer = ftell(stream);
	fgets(line,N,stream);
	if (  strcmp(GetToken(line),")") == 0 )
	{
		fseek(stream,file_pointer,SEEK_SET);
	}
	else {
		do {
			CompileExpression(line);
		
			file_pointer = ftell(stream);
			fgets(line,N,stream);

			if (  strcmp(GetToken(line),")") == 0 ) 
			{
				flag=false;
				fseek(stream,file_pointer,SEEK_SET);
			}
			else if (  strcmp(GetToken(line),",") == 0 ) 
			{
				flag=true;
				PrintIndentedLine(line);
				fgets(line,N,stream);
			}
		}
		while(flag);
	}

	Indent--;
	PrintIndentedLine("</expressionList>");

}

void CompliationEngine::CompileSubroutineCall(char* name)
{
	char line[N]; 
	PrintIndentedLine(name);	//subroutineName/className/varName
	fgets( line,N, stream );
	if ( strcmp(GetToken(line),"(") == 0 )
	{
		PrintIndentedLine(line);
		CompileExpressionList();
		RunCompliationEngine(")");
	}
	else if ( strcmp(GetToken(line),".") == 0 )
	{
		PrintIndentedLine(line);
		RunCompliationEngine("(");
		CompileExpressionList();
		RunCompliationEngine(")");
	}
}

char* CompliationEngine::GetTokenType(char* line)
{
	// line = <TokenType> Token </TokenType>
	// returns: TokenType
	char tmp[N];
	strcpy(tmp,line);
	return strtok( tmp, "<>\n");
}

char* CompliationEngine::GetToken(char* line)
{
	// line = <TokenType> Token </TokenType>
	// returns: Token
	char tmp[N];
	char* token;
	int i;
	strcpy(tmp,line);
	strtok( tmp, "<>\n");
	token = strtok( NULL, "<>\n");
	*(token+strlen(token)-1) = '\0';
	for (i=0; i<=strlen(token); i++)
		*(token+i)=*(token+i+1);
	return token;	
}

void CompliationEngine::PrintIndentedLine(char* line)
{
	int i;
	/*for ( i=0; i<Indent; i++)
		fputs("  ",output);*/
	line = strtok( line, "\n");
	fprintf(output,"%s\n",line);
}
