#ifndef __JACKTOKENIZER_H__
#define __JACKTOKENIZER_H__

#include <iostream>
typedef enum { KEYWORD, SYMBOL, IDENTIFIER,  INT_CONST, STRING_CONST} tokenType;
#define isdigit(x) ((x) >= '0' && (x) <= '9')
#define isletter(c) ((c >= 'A') && (c <= 'Z')) || ((c >= 'a') && (c <= 'z'))
using namespace std;

// Class JackTokenizer. 

class JackTokenizer {
public:
	JackTokenizer(); //c'tor
	JackTokenizer(char* file_name2); //c'tor
	JackTokenizer::~JackTokenizer(); //d'tor

	void RunJackTokenizer();
	void JackTokenizer::DivideLineToTokens(char* line);
	bool JackTokenizer::hasMoreToknes(char* line3);
	char* JackTokenizer::Advance(char* line3);
	tokenType JackTokenizer::tokenType(char* token);
	char* JackTokenizer::keyWord(char* token);
	char* JackTokenizer::symbol(char* token);
	char* JackTokenizer::stringVal(char* token);
	int JackTokenizer::intVal(char* token);
	char* JackTokenizer:: identifier(char* token);
	void JackTokenizer::PrintToken(char* token);

private:
	char file_name[1000];
	char xml_file_name[1000];
	FILE* stream;
	FILE* output;
};


#endif //__JACKTOKENIZER_H__