#ifndef __JACKANALYZER_H__
#define __JACKANALYZER_H__

#include <iostream>
#include <tchar.h>
using namespace std;

// Class JackAnalyzer. 

class JackAnalyzer {
public:
	JackAnalyzer(); //c'tor
	JackAnalyzer(int argc2, TCHAR **argv2); //c'tor

	void HandleFiles();
	void RunJackAnalyzer(TCHAR* pTchar);
	void RunJackTokenizer(char* file_name);
	void RunCompiler(char* file_name);
	void RunCompliationEngine(char* file_name);
	void WcharToString(char * str,TCHAR* pTchar){while( (*str++ = (char)*pTchar++) != '\0') ;}
	bool IsJackFile(char* file_name);


private:
	int argc;
	TCHAR **argv;
};
#endif //__JACKANALYZER_H__
