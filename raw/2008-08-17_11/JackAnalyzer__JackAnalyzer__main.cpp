#include <windows.h> 
#include <stdio.h>
#include <tchar.h>
#include "JackAnalyzer.h"
#include "List.h"

#define BUFSIZE MAX_PATH

void WcharToString(char * str,TCHAR* pTchar);
void ChartoTChar(TCHAR *CBuff,char *Char);
bool IsJackFile(char* file_name);

 
// the program: take jack file/s and translate them to .xml
void _tmain(int argc, TCHAR **argv) 
{ 

   TCHAR Buffer[BUFSIZE];
   DWORD dwRet;
   char onefile[1000];

   // Set the path in argv[1] as the current directory.
   // or type the name of the file - XXX.JACK
   // then comile all jack file in it (RunJackAnalyzer()).
   // then set the current directory as it was in the begining.
   if(argc != 2)
   {
      _tprintf(TEXT("Usage: %s <dir>\n"), argv[0]);
      return;
   }

   // if the user want to compile only one file
   WcharToString(onefile,argv[1]);
   if( IsJackFile(onefile) )
   {
	   JackAnalyzer analyzer(argc, argv); 
	   analyzer.RunJackAnalyzer(argv[1]);
	   return;
   }
   //else if the user want to compile a directory
   dwRet = GetCurrentDirectory(BUFSIZE, Buffer);

   if( dwRet == 0 )
   {
      printf("GetCurrentDirectory failed (%d)\n", GetLastError());
      return;
   }
   if(dwRet > BUFSIZE)
   {
      printf("Buffer too small; need %d characters\n", dwRet);
      return;
   }

   if( !SetCurrentDirectory(argv[1]))
   {
      printf("SetCurrentDirectory failed (%d)\n", GetLastError());
      return;
   }
   _tprintf(TEXT("Set current directory to %s\n"), argv[1]);

   JackAnalyzer analyzer(argc, argv); 
   analyzer.HandleFiles();

   if( !SetCurrentDirectory(Buffer) )
   {
      printf("SetCurrentDirectory failed (%d)\n", GetLastError());
      return;
   }

   _tprintf(TEXT("Restored previous directory (%s)\n"), Buffer);
	  
/*
	List l;
	l.InsertBack("nAccounts","int","static");
	l.InsertBack("bank","int","static");
	l.InsertBack("id","int","field");
	l.InsertBack("owner","String","field");
	l.InsertBack("balance","int","field");
	l.Print();*/


}

void WcharToString(char * str,TCHAR* pTchar)
{
	while( (*str++ = (char)*pTchar++) != '\0') ;
}

void ChartoTChar(TCHAR *CBuff,char *Char)
{
while((*CBuff++ = (TCHAR) *Char++) != '\0');
}

bool IsJackFile(char* file_name)
{
	char * pch;
	pch=strrchr(file_name,'.');
	if (pch == NULL)
		return false;
	if ( (!strncmp(pch,".JACK",5)) || (!strncmp(pch,".Jack",5)) || (!strncmp(pch,".jack",5)) )
		return true;
	else return false;
}
 
 