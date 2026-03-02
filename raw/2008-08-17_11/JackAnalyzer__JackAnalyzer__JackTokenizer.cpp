//CHECK: WHAT IF WE HAVE IN THE FIRST LINE TAB? "				sss dd". don't get the first word


#include <iostream>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <errno.h>

#include "JackTokenizer.h"
#define N 1000
using namespace std;

// Class JackTokenizer functions

JackTokenizer::JackTokenizer() //c'tor
{
}

JackTokenizer::JackTokenizer(char* file_name2) //c'tor
{
	char tmp[1000];
	//get the file name - "XXX.xml".
	strcpy(tmp,file_name2);
	strcpy(file_name, tmp);
	strcpy(xml_file_name, tmp);
	strcat(file_name, ".JACK");
	strcat(xml_file_name, "T.xml");

	//open the input and output files for reading&writing.
	fopen_s(&stream, file_name, "r");
	fopen_s(&output, xml_file_name, "w");
}

JackTokenizer::~JackTokenizer() //d'tor
{
	delete file_name;
	delete xml_file_name;
	fclose(stream);
	fclose(output);
}

//put the output of the of JackTokenizer in the output file.
void JackTokenizer::RunJackTokenizer()
{
	char line[1000];
	
	//go through every line in the input file and
	//put its tokens in the output file.
	fprintf(output,"<tokens>\n");
	while( fgets( line,N, stream ) != NULL)
		DivideLineToTokens(line);
	fprintf(output,"</tokens>\n");
}

//receive line and put its token in the output file 
void JackTokenizer::DivideLineToTokens(char* line)
{
	char* token;
	//fprintf(output,"%s\n",line);
	while (hasMoreToknes(line)){
		token=Advance(line);
		if (token!="")
			PrintToken(token);
	}
}

//do we have more tokens in the input? + drop comments
bool JackTokenizer::hasMoreToknes(char* line3)
{
	static bool InComment=false;
	char *token,*line2;
	char quot[]={'"','\0'};

	char line[1000];
	strcpy(line,line3);


	//to avoid cases like: printf("rrr/* */").
	if ( strncmp(quot,line,1) == 0 )
		return true;

	// it's a "/*.." comment - check.
	else if ( strncmp("/*",line,2) == 0 )
	{
		// if the line has "/*..*/" comment.
		if ( (line2 = strstr (line,"*/"))!=NULL )
		{
			line2++;line2++;
			strcpy(line3,line2); // in line - the line without the comment "/*..*/".
			InComment=false;
			return hasMoreToknes(line3);
		}
		// there is no "*/" symbol - the comment includes more than one line.
		else {
			InComment=true;
			return false;
		}
	}

	// there is "*/" symbol - the comment ends. 
	else if ( (line2 = strstr (line,"*/"))!=NULL )
	{
		line2++;line2++;
		strcpy(line3,line2); // in line - the line without the comment "..*/".
		InComment=false;
		return hasMoreToknes(line3);
	}

	// it's a "//.." comment - no tokens.
	else if ( strncmp("//",line,2) == 0 )
		return false;

	// it's an empty line - no tokens.
	else if  ( (token = strtok( line, "		\n")) == NULL )
			return false;

	// case of: /*... 
	//		    .....  <-this line is a comment - no tokens.
	//		    ....*/
	else if (InComment)
			return false;

	 // there's a token.
	else return true;
}

//gets the next token from the input
char* JackTokenizer::Advance(char* line3)
{
	char *line2, *stringval,*stringval2;
	char number[6];
	char line[1000];
	int i,j;
	char *KeywordSymbols[] = {"class", "constructor", "function", "method", "field", "var", "int",
							  "char", "boolean", "void", "true", "false", "null", "this",
							  "let", "do", "if", "else", "while", "return",
							  "{", "}", "(", ")", "[", "]", ".", ",", ";", "+", "-", "*", "/",
							  "&", "|", "<", ">", "=", "~"};
	char quot[]={'"','\0'};


	strcpy(line,line3);
	// if we see whitespace/tab(Indent) - delete it and return to see if there's more tokens ahead.
	if ( ( *line3 == ' ' ) || ( *line3 == '	' ) )
	{
		line2++;
		strcpy(line3,line2);
		return "";
	} 

	//if it's a string
	if ( strncmp(quot,line3,1) == 0 ){
		line2=line3;
		stringval2=stringval;
		line2++;
		while ( strncmp(quot,line2,1) != 0 ){
			*stringval2 = *line2;
			line2++;
			stringval2++;
		}
		line2++;
		*(stringval2++) = '"';
		*stringval2 = '\0';
		strcat(quot,stringval);
		strcpy(line3,line2);

		return quot;
	}

	//if it's a number
	if  ( isdigit(*line3) )
	{
		i=0;
		line2=line3;
		while ( isdigit(*line2) )
		{
			i++;
			line2++;
		}
		strncpy(number,line3,i);
		strcpy(line3,line2);
		number[i]='\0';
		return number;
	}

	//if it's a keyword or symbol - return it & return line3 without the token
	i=0;
	while ( (i<39) && ( strncmp(KeywordSymbols[i],line,strlen(KeywordSymbols[i])) != 0 ) )
		i++;
	if ( strncmp(KeywordSymbols[i],line,strlen(KeywordSymbols[i])) == 0 ){
		j=0;
		while ( j < strlen(KeywordSymbols[i]) ){
			line2++;
			j++;
		}
		strcpy(line3,line2); 
		return KeywordSymbols[i];
	}

		//if it's an identifier
	if  ( isletter(*line3) || ( (*line3) == '_' ) )
	{
		line2=line3;
		line2++;
		i=1;
		while (  isletter(*line2) || ( *line2 == '_' ) || isdigit(*line2) )
		{
			i++;
			line2++;
		}
		strncpy(line,line3,i);
		strcpy(line3,line2);
		line[i]='\0';
		return line;
	}
	return "";
}

// get token, return the token type
tokenType JackTokenizer::tokenType(char* token)
{
	int i;
	char *Keyword[] = {"class", "constructor", "function", "method", "field", "var", "int",
					   "char", "boolean", "void", "true", "false", "null", "this",
					   "let", "do", "if", "else", "while", "return"};
	char *Symbol[] = {"{", "}", "(", ")", "[", "]", ".", ",", ";", "+", "-", "*", "/",
					  "&", "|", "<", ">", "=", "~"};

	for (i=0; i<20 ;i++)
		if  ( strncmp(Keyword[i],token,strlen(Keyword[i])) == 0 )
			return KEYWORD;
	for (i=0; i<19 ;i++)
		if  ( strncmp(Symbol[i],token,strlen(Symbol[i])) == 0 )
			return SYMBOL;
	if (*token == '"')
		return STRING_CONST;
	if (isdigit(*token))
		return INT_CONST;
	if (isletter(*token) || ( (*token) == '_' ) )
		return IDENTIFIER;
}

char* JackTokenizer::keyWord(char* token)
{
	return token;
}

char* JackTokenizer::symbol(char* token)
{
	char quot[]={'"','\0'};

	if ( strcmp(token,"<") == 0 )
		return "&lt;";
	else if ( strcmp(token,">") == 0 )
		return "&gt;";
	else if ( strcmp(token,quot) == 0 )
		return "&quot;";
	else if ( strcmp(token,"&") == 0 )
		return "&amp;";
	else return token;
}

char* JackTokenizer::stringVal(char* token)
{
	char* tmp;
	char quot[]={'"','\0'};
	token++;
	tmp = strstr (token,quot);
	*tmp = '\0';
	return token;
}

int JackTokenizer::intVal(char* token)
{
	return  atoi(token);
}

char* JackTokenizer:: identifier(char* token)
{
	return token;
}

void JackTokenizer::PrintToken(char* token)
{
	if ( tokenType(token) == KEYWORD )
		fprintf( output, "<keyword> %s </keyword>\n", keyWord(token));
	else if ( tokenType(token) == SYMBOL )
		fprintf( output, "<symbol> %s </symbol>\n", symbol(token));
	else if ( tokenType(token) == STRING_CONST )
		fprintf( output, "<stringConstant> %s </stringConstant>\n", stringVal(token));
	else if ( tokenType(token) == INT_CONST )
		fprintf( output, "<integerConstant> %d </integerConstant>\n", intVal(token));
	else if ( tokenType(token) == IDENTIFIER )
		fprintf( output, "<identifier> %s </identifier>\n", identifier(token));
}