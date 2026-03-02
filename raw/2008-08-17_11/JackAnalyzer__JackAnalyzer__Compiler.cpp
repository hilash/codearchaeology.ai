/* PROBLEMS:
 * do Output.printInt(1 + ((2) * 3));		DOESNT WORK!
 * do Output.printInt(1 + (2 * (3)));		WORKS!
 */



#include <iostream>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <errno.h>

#include "Compiler.h"
#define N 1000
using namespace std;

// Class Compiler functions

Compiler::Compiler () //c'tor
{
}

Compiler::Compiler(char* file_name2) //c'tor
{
char tmp[1000];
	//get the file name - "XXX.xml".
	strcpy(tmp,file_name2);
	strcpy(input_file, tmp);
	strcpy(output_file, tmp);
	strcat(input_file, ".xml");
	strcat(output_file, ".vm");

	//open the input and output files for reading&writing.
	fopen_s(&stream, input_file, "r");
	fopen_s(&output, output_file, "w");
}

Compiler::~Compiler () //d'tor
{
	fclose(stream);
	fclose(output);
}

void Compiler::RunCompiler()
{
	char line[200];
	while( fgets( line,N, stream ) != NULL ) 
		Handle( GetToken(line), GetTokenType(line) );	
}

void Compiler::Handle(char* token, char* token_type){
	if ( strcmp (token_type,"class") == 0 ) //<class>
		CompileClass();
}

char* Compiler::GetToken(char* line)
{
	// line = <TokenType> Token </TokenType>
	// returns: Token
	char tmp[N];
	char* token;
	int i;
	strcpy(tmp,line);
	strtok( tmp, "<>\n");
	if ( (token = strtok( NULL, "<>\n")) != NULL )
	{
		*(token+strlen(token)-1) = '\0';
		for (i=0; i<=strlen(token); i++)
			*(token+i)=*(token+i+1);
		return token;	
	}
	else return "";
}

char* Compiler::GetTokenType(char* line)
{
	// line = <TokenType> Token </TokenType>
	// returns: TokenType
	char tmp[N];
	strcpy(tmp,line);
	return strtok( tmp, "<>\n");
}

void Compiler::CompileClass()
{
	char line[200];
	long file_pointer;
	fgets( line,N, stream );//<keyword>class</keyword> 
	fgets( ClassName,N, stream );//<identifier>ClassName</identifier> 
	strcpy(ClassName,GetToken(ClassName));
	fgets( line,N, stream );//<symbol>{</symbol> 
	//classVarDec*
	file_pointer=ftell(stream);
	while ( (fgets( line,N, stream )!=NULL) ){
		if ( strcmp (GetTokenType(line),"classVarDec") == 0 ) //<classVarDec>
			CompileClassVarDec();
		else
		{
			fseek(stream,file_pointer,SEEK_SET);
			break;
		}
		file_pointer=ftell(stream);
	}
	//subroutineDec*
	file_pointer=ftell(stream);
	while ( (fgets( line,N, stream )!=NULL)){
		if ( strcmp (GetTokenType(line),"subroutineDec") == 0 ) //<subroutineDec>
			CompileSubroutineDec();
		else
		{
			fseek(stream,file_pointer,SEEK_SET);
			break;
		}
		file_pointer=ftell(stream);
	}
	fgets( line,N, stream );//<symbol>}</symbol> 
	fgets( line,N, stream );//</class>
}

void Compiler::CompileClassVarDec()
{
}

void Compiler::CompileSubroutineDec()
{
	char line[200];
	char subroutine_type[200];
	char subroutine_Name[200];
	long file_pointer;
	int x;
	List SubList;
	SubList.InsertBack("null","null","null"); /*Update static varibles to 0*/

	fgets( subroutine_type,N, stream ); //<keyword> (constructor |function| method) </keyword>
	strcpy(subroutine_type,GetToken(subroutine_type));
	
	fgets( subroutine_returned_type,N, stream ); //<keyword> (void|int|char|boolean|className) </keyword> 
	strcpy(subroutine_returned_type,GetToken(subroutine_returned_type));
	
	fgets( subroutine_Name,N, stream ); // <identifier>subroutineName</identifier> 
	strcpy(subroutine_Name,GetToken(subroutine_Name));
	
	fgets( line,N, stream ); //<symbol>(</symbol>

	if ( strcmp (subroutine_type, "method") == 0)// push the first argument - this.
		SubList.InsertBack("this",ClassName,"argument");
	CompileParameterList(&SubList);
	fgets( line,N, stream );//<symbol>)</symbol>
	fgets( line,N, stream );//<subroutineBody>
	fgets( line,N, stream );//<symbol> { </symbol>
	file_pointer=ftell(stream);
	while ( (fgets( line,N, stream )!=NULL ) && ( strncmp(line,"<varDec>",8) == 0 ) )
	{
		fseek(stream,file_pointer,SEEK_SET);
		CompileVarDec(&SubList);
		file_pointer=ftell(stream);
	}
	fseek(stream,file_pointer,SEEK_SET);
	//function declaration
	x=SubList.HowManyOfKind("local");
	fprintf(output,"function %s.%s %d\n",ClassName,subroutine_Name,x);/////////////////////////////////
	CompileStatements(&SubList);
	fgets( line,N, stream );//<symbol> } </symbol>
	fgets( line,N, stream );//</subroutineBody>
	fgets( line,N, stream );//</subroutineDec>
	SubList.Print();

}

void Compiler::CompileParameterList(List* SubList)
{
	char line[N];
	long file_pointer;
	bool flag=false;
	char Name[500];
	char Type[500];

	
	file_pointer = ftell(stream);
	fgets(line,N,stream); //<parameterList>
	fgets(line,N,stream);
	while (  strcmp(GetTokenType(line),"/parameterList")  != 0 ) //</parameterList>?
	{
			strcpy( Type,GetToken(line) );//<keyword> (int|char|boolean|className) </keyword> 
			fgets(line,N,stream);
			strcpy( Name,GetToken(line) );//<identifier> varName </identifier>
			(*SubList).InsertBack(Name,Type,"argument");

			file_pointer = ftell(stream);
			fgets(line,N,stream);

			if (  strcmp(GetToken(line),",") == 0 ) 
				fgets(line,N,stream);
	}
}
void Compiler::CompileVarDec(List* SubList)
{
	char Name[500];
	char Type[500];

	fgets(Name,N,stream);// <varDec>
	fgets(Name,N,stream);// <keyword> var </keyword>
	fgets(Type,N,stream);//<keyword> (int|char|boolean|className) </keyword> 
	strcpy( Type,GetToken(Type) );

	fgets(Name,N,stream);
	while (  strcmp(GetToken(Name),";")  != 0 ) //<varName>
	{
			strcpy( Name,GetToken(Name) );//<identifier> varName </identifier>
			(*SubList).InsertBack(Name,Type,"local");

			fgets(Name,N,stream);

			if (  strcmp(GetToken(Name),",") == 0 ) 
				fgets(Name,N,stream);
	}
	fgets(Name,N,stream); //</varDec>
}

void Compiler::CompileStatements(List* SubList)
{
	char line[500];
	long file_pointer;

	
	fgets(line,N,stream); //<statements>

	file_pointer= ftell(stream);
	fgets(line,N,stream);
	while ( strncmp(line,"</statements>",11) != 0 ){
		if ( strncmp(line,"<doStatement>",13) == 0 ){
			fseek(stream,file_pointer,SEEK_SET);
			CompileDo(SubList);
		}
		else if ( strncmp(line,"<letStatement>",14) == 0 ){
			fseek(stream,file_pointer,SEEK_SET);
			CompileLet(SubList);
		}
		else if ( strncmp(line,"<ifStatement>",13) == 0 ){
			fseek(stream,file_pointer,SEEK_SET);
			CompileIf(SubList);
		}
		else if ( strncmp(line,"<whileStatement>",16) == 0 ){
			fseek(stream,file_pointer,SEEK_SET);
			CompileWhile(SubList);
		}
		else if ( strncmp(line,"<returnStatement>",17) == 0 ){
			fseek(stream,file_pointer,SEEK_SET);
			CompileReturn(SubList);
		}
		file_pointer=ftell(stream);
		fgets( line,N, stream );
	}
	//</statements>
}

void Compiler::CompileLet(List* SubList)
{
	char line[N];
	char varName[500];
	Node* var;
	fgets(line,N,stream);		//<letStatement>
	fgets(line,N,stream);		// <keyword> let </keyword>
	fgets(varName,N, stream );	// <identifier> varName </identifier> 
	var=SubList->Find(GetToken(varName)); // get all details about varName
	fgets(line,N, stream );		//<symbol>=</symbol> 
	/*
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
	else PrintIndentedLine(line);	// =*/
	CompileExpression(SubList);
	fprintf(output,"pop %s %d\n",var->Kind,var->Num);
	fgets(line,N, stream );		//<symbol>;</symbol> 
	fgets(line,N,stream);		//</letStatement>
}

void Compiler::CompileIf(List* SubList)
{
	static int label_counter=0;
	/*	if (cond)
		{
			s1
		}
		(else s2)*
	*/
	char line[N];
	long file_pointer;

	fgets(line,N,stream);		// <ifStatement>
	fgets(line,N,stream);		// <keyword>if</keyword>
	fgets(line,N,stream);		// <symbol>(</symbol>
	CompileExpression(SubList);	// compile expression cond
	fputs("not\n",output);		// push to stack (~cond)
	fgets(line,N,stream);		// <symbol>)</symbol>
	fprintf(output,"if-goto IF%d\n",label_counter++);	// if-goto L1
	fgets(line,N,stream);		// <symbol>{</symbol>
	CompileStatements(SubList);		// Statement S1
	fgets(line,N,stream);		// <symbol>}</symbol>
	fprintf(output,"goto IF%d\n",label_counter--);		// goto L2
	fprintf(output,"label IF%d\n",label_counter++);		// label L1
	// check if there is else
	file_pointer = ftell(stream);
	fgets(line,N,stream);		
	if ( strncmp (line, "else",4) == 0 ) { //<keyword>else</keyword>
		fgets(line,N,stream);	// <symbol>{</symbol>
		CompileStatements(SubList);	// Statement S1
		fgets(line,N,stream);	// <symbol>}</symbol>
	}
	else fseek(stream,file_pointer,SEEK_SET);
	fprintf(output,"label IF%d\n",label_counter++);		// label L2
	fgets(line,N,stream);		// </ifStatement>
}

void Compiler::CompileWhile(List* SubList)
{
	char line[N];
	static int label_counter = 0;

	fprintf(output,"label WHILE%d\n",label_counter++);		// label L1
	fgets(line,N,stream);		// <whileStatement>
	fgets(line,N,stream);		// <keyword>while</keyword>
	fgets(line,N,stream);		// <symbol>(</symbol>
	CompileExpression(SubList);	// compile expression cond
	fputs("not\n",output);		// push to stack (~cond)
	fgets(line,N,stream);		// <symbol>)</symbol>
	fprintf(output,"if-goto WHILE%d\n",label_counter--);	// if-goto L2

	fgets(line,N,stream);		// <symbol>{</symbol>
	CompileStatements(SubList);		// Statement S1
	fgets(line,N,stream);		// <symbol>}</symbol>
	fprintf(output,"goto WHILE%d\n",label_counter++);		// goto L1
	fprintf(output,"label WHILE%d\n",label_counter++);		// label L2
	fgets(line,N,stream);		// </whileStatement>
}

void Compiler::CompileDo(List* SubList)
{

	char line[N];
	fgets(line,N,stream); // <doStatement>
	fgets(line,N,stream); // <keyword> do </keyword>
	CompileSubroutineCall(SubList);
	fgets(line,N,stream); // <symbol> ; </symbol>
	fgets(line,N,stream); // </doStatement>
}

void Compiler::CompileReturn(List* SubList)
{
	char line[500];
	long file_pointer;

	fgets(line,N,stream); // <returnStatement>
	fgets(line,N,stream); // <keyword> return </keyword>
	file_pointer = ftell(stream);
	fgets(line,N,stream); 
	if ( strcmp(line,"<expression>") == 0){
		fseek(stream,file_pointer,SEEK_SET);
		CompileExpression(SubList);
	}
	else fseek(stream,file_pointer,SEEK_SET);
	fgets(line,N,stream); // <symbol> ; </symbol>
	fgets(line,N,stream); // </returnStatement>
	if ( strncmp( subroutine_returned_type,"void",4) == 0 )
		fprintf(output,"push constant 0\n");
	fputs("return\n",output);
}
void Compiler::CompileSubroutineCall(List* SubList)
{
	char Name[N];
	char Name2[N]; 
	char line[N]; 
	int arg;
	fgets( Name,N, stream );	//subroutineName/className/varName
	fgets( line,N, stream );
	if ( strcmp(GetToken(line),"(") == 0 )//call :subroutineName(expressionslist)
	{
		arg = CompileExpressionList(SubList);
		fprintf(output,"call %s %d",GetToken(Name),arg);////////////////////////////////////////
		fgets( line,N, stream ); //<symbol> ) </symbol>
	}
	else if ( strcmp(GetToken(line),".") == 0 ) //call :classvar.subroutineName(expressionslist)
	{
		fgets( Name2,N, stream );// <identifier> classvar </identifier>
		fgets( line,N, stream );// <symbol> ( </symbol>
		arg = CompileExpressionList(SubList);
		fgets( line,N, stream ); //<symbol> ) </symbol>



		fprintf(output,"call %s.",GetToken(Name));
		fprintf(output,"%s %d\n",GetToken(Name2),arg);
	}
}

void Compiler::CompileExpression(List* SubList)
{
	char line[N];
	long file_pointer;
	bool flag=false;


	fgets( line,N, stream );// <expression>
	printf("<expression>: %s\n",line);
	CompileTerm(SubList); // <term>...</term> - push it's value into stack
	do {
		file_pointer = ftell(stream);
		fgets(line,N,stream); // <symbol> op </symbol>
	
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
			CompileTerm(SubList); // push another operand
			// two operands in the stack  - call the function suits the operator
			if ( strcmp(GetToken(line),"+") == 0 ) 
				fputs("add\n",output);
			else if ( strcmp(GetToken(line),"-") == 0 ) 
				fputs("sub\n",output);
			else if ( strcmp(GetToken(line),"*") == 0 )
				fprintf(output,"call Math.multiply 2\n");
			else if ( strcmp(GetToken(line),"/") == 0 )
				fputs("call Math.divide 2\n",output);
			else if ( strcmp(GetToken(line),"&") == 0 ) 
				fputs("and\n",output);
			else if ( strcmp(GetToken(line),"|") == 0 ) 
				fputs("or\n",output);
			else if ( strcmp(GetToken(line),"<") == 0 ) 
				fputs("lt\n",output);
			else if ( strcmp(GetToken(line),">") == 0 ) 
				fputs("gt\n",output);
			else if ( strcmp(GetToken(line),"=") == 0 ) 
				fputs("eq\n",output);
			else if ( strcmp(GetToken(line),"&lt;") == 0 ) 
				fputs("lt\n",output);
			else if ( strcmp(GetToken(line),"&gt;") == 0 ) 
				fputs("gt\n",output);
		}
		else {
			flag=false;
		}
	}
	while (flag);
	// </expression>
	fseek(stream,file_pointer,SEEK_SET);
	fgets(line,N,stream);
	printf("</expression>: %s\n",line);
	ptr = ftell(stream);
}

void Compiler::CompileTerm(List* SubList)
{
	char line[N];
	char line2[N];
	long file_pointer;
	Node* var;

	fgets( line,N, stream );// <term>
	printf("<term>: %s\n",line);
	// unaryOp term
	file_pointer = ftell(stream);
	fgets( line,N, stream );
	if ( strcmp(GetToken(line),"-") == 0 )
	{
		CompileTerm(SubList);
		fputs("neg\n",output);
	}
	else if ( strcmp(GetToken(line),"~") == 0 ) 
	{
		CompileTerm(SubList);
		fputs("not\n",output);
	}
	// '('expression')'
	
	else if ( strcmp(GetTokenType(line),"integerConstant") == 0 )
	{
		fprintf(output,"push constant %s\n",GetToken(line));
		printf("push constant %s\n",GetToken(line));
	}
	else if ( ( strncmp(GetToken(line),"null",4) == 0 ) || ( strncmp(GetToken(line),"false",4)  == 0 ) )
		fprintf(output,"push constant 0\n");
	else if ( strncmp(GetToken(line),"true",4) == 0 ){
		fprintf(output,"push constant 1\n");
		fprintf(output,"neg\n");

	}
	else if ( strncmp(GetToken(line),"(",1) == 0 )// <symbol> ( </symbol>
	{
		printf("*** ( : %s\n",line);
		CompileExpression(SubList);
		//fseek(stream,ptr,SEEK_SET);
		puts("***");
		fgets(line,N,output); // <symbol> ) </symbol>
		printf("*** ) : %s\n",line);
	}

	/*
	//  integerConstant/stringConstant/keywordConstant
	else if (	( strcmp(GetTokenType(termline),"integerConstant") == 0 ) ||
				( strcmp(GetTokenType(termline),"stringConstant")  == 0 ) ||
				( strcmp(GetToken(termline),"true")   == 0 ) ||
				( strcmp(GetToken(termline),"false")  == 0 ) ||
				( strcmp(GetToken(termline),"null")   == 0 ) ||
				( strcmp(GetToken(termline),"this")   == 0 ) ) PrintIndentedLine(termline);
				*/

	// distinguish between variable,array and subroutine call.
	else {
		fgets(line,N,stream);
		/*
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
		else*/ if ( ( strcmp(GetToken(line),"(") == 0 ) || ( strcmp(GetToken(line),".") == 0 ) )
		{
			fseek(stream,file_pointer,SEEK_SET);
			CompileSubroutineCall(SubList);
		}
		// varname
		else {
			fseek(stream,file_pointer,SEEK_SET);
			fgets(line,N,stream);
			var=SubList->Find(GetToken(line));
			if (var!=NULL)
				fprintf(output,"push %s %d\n",var->Kind,var->Num);
		}
	}
	fgets( line,N, stream );// </term>
	printf("</term>: %s\n",line);
}

int Compiler::CompileExpressionList(List* SubList)
{
	char line[N];
	long file_pointer;
	bool flag=false;
	int i = 0;

	fgets( line,N, stream );// <expressionList>
	printf("<expressionList>: %s\n",line);
	
	file_pointer = ftell(stream);
	fgets( line,N, stream );
	if (  strncmp(line,"</expressionList>",17) == 0 )
	{
		fseek(stream,file_pointer,SEEK_SET);
	}
	else {
		do {
			fseek(stream,file_pointer,SEEK_SET);
			i++;
			CompileExpression(SubList);
			file_pointer = ftell(stream);
			fgets(line,N,stream);

			if ( strncmp(line,"</expressionList>",17) == 0 ) 
			{
				flag=false;
				fseek(stream,file_pointer,SEEK_SET);
			}
			else if (  strncmp(GetToken(line),",",1) == 0 ) 
			{
				file_pointer = ftell(stream);
				flag=true;
			}
		}
		while(flag);
	}

	fgets( line,N, stream );// </expressionList>
	printf("</expressionList>: %s\n",line);
	return i;

}