#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#define N 256
#define isdigit(x) ((x) >= '0' && (x) <= '9')
#define isletter(c) ((c >= 'A') && (c <= 'Z')) || ((c >= 'a') && (c <= 'z'))

typedef enum { false, true } bool;
typedef enum { C_ARITHMETIC, C_PUSH, C_POP, C_LABEL, C_GOTO, C_IF,C_FUNCTION,C_RETURN, C_CALL, C_NULL} commandType;

char* get_asm_name ( char*, char* );
void Parser ( FILE*, FILE* );
void initializer ( FILE*, FILE* ); 
void DeleteWhiteSpacesAndComments ( FILE*, FILE* );
void DeleteEmptyLines ( FILE*, FILE* );
void Translate ( char*, FILE* );
commandType cmdType ( char*);
void Trans_C_Arithmetic ( char*, FILE* );
void Trans_C_Push ( char*, FILE* );
void Trans_C_Pop ( char*, FILE* );
void Trans_C_Label ( char*, FILE* );
void Trans_C_Goto ( char*, FILE* );
void Trans_C_If ( char*, FILE* );
void Trans_C_Function ( char*, FILE* );
void Trans_C_Return ( char*, FILE* );
void Trans_C_Call ( char*, FILE* );


int main(int argc, char *argv[]) 
{
	FILE *f1, *f2;
	char file_name[N];

	if (argc != 2){
		printf("Incorrect number of arguments, enter Assembler path_of_the_file\n");
		return 1;
	}
	if ((f1=fopen(argv[1], "r" ))==NULL){
		printf("Can't open file %s\n",argv[1]);
		return 1;
	}

	get_asm_name(file_name,argv[1]);
	f2=fopen(file_name, "w" );

	Parser(f1,f2);

	fclose(f1);
	fclose(f2);
	return 0;
}

char* get_asm_name ( char* dest, char* src )
{
   const char *p;
   char *q;
 
   for(p = src, q = dest; (*p != '.') && (*p != '\0') && (*p != '\n') && (*p != EOF) ; p++, q++)
	   *q = *p;

   *(q++) = '.';
   *(q++) = 'a';
   *(q++) = 's';
   *(q++) = 'm';
   *q = '\0';
 
   return dest;
}

void Parser ( FILE* stream, FILE* vm )
{
	char command[N];
	FILE* progVM;

	progVM=fopen("progVM.vm", "w" );
	initializer(stream, progVM);
	fclose(progVM);
	if( (progVM=fopen("progVM.vm", "r")) != NULL )
		while( fgets( command, N, progVM ) != NULL)
			Translate(command,stream);
	fclose(progVM);
	remove("progVM.vm");
}

void initializer( FILE* stream, FILE* progVM ) 
{
	FILE *tmp;
	tmp=fopen("tmp.vm", "w" );
	DeleteWhiteSpacesAndComments(stream, tmp);
	fclose(tmp);
	tmp=fopen("tmp.vm", "r" );
	DeleteEmptyLines(tmp, progVM);
	fclose(tmp);
	remove("tmp.vm");
}

void DeleteWhiteSpacesAndComments( FILE* stream, FILE* temp )
{
	char c,d;
	c=fgetc(stream);
	while ( c!=EOF )
	{
		d=fgetc(stream);
		if  ( (c==' ') &&  ( (isletter(d)) || (isdigit(d)) ) )
			fputc(c,temp);
		else if ( (c=='/') && (d=='/') ){
			while ( (c!='\n') && (c!=EOF) )
				c=fgetc(stream);
			if ( (c=='\n'))
				fputc(c,temp);
		}
		else if ((c!=' ') && (c!='/') )
				fputc(c,temp);
		c=d;
	}
}

void DeleteEmptyLines ( FILE* stream, FILE* temp)
{
	char c=fgetc(stream),d;
	int flag=0;
	while ( c!=EOF )
	{
		if (c!='\n'){
			fputc(c,temp);
			flag=1;
		}
		d=fgetc(stream);
		if ((c=='\n') && (d!=EOF) && (d!='\n') && (flag==1))
			fputc(c,temp);
		c=d;	
	}
}

void Translate ( char* command, FILE* Asm)
{
	commandType cmd_t=cmdType(command);
	switch ( cmd_t )
	{
		case C_ARITHMETIC:
			Trans_C_Arithmetic ( command, Asm );
		case C_PUSH:
			Trans_C_Push ( command, Asm ); 
		case C_POP:
			Trans_C_Pop ( command, Asm );
		case C_LABEL:
			Trans_C_Label ( command, Asm ); 
		case C_GOTO:
			Trans_C_Goto ( command, Asm );
		case C_IF:
			Trans_C_If ( command, Asm ); 
		case C_FUNCTION:
			Trans_C_Function ( command, Asm );
		case C_RETURN:
			Trans_C_Return ( command, Asm );
		case C_CALL:
			Trans_C_Call ( command, Asm ); 
		case C_NULL:
			break;
	}
}

commandType cmdType(char* command)
{
  char* pch;
  char cmd[N];
  strcpy(cmd,command);
  pch = strtok (cmd," \n");
	   /*CAN IT GET "ADD","Add","aDD", and etc.? CHECK IT OUT!*/
  if ( (strcmp(pch,"add")==0) || (strcmp(pch,"sub")==0) || (strcmp(pch,"neg")==0) || (strcmp(pch,"eq")==0) ||
	   (strcmp(pch,"gt" )==0) || (strcmp(pch,"lt" )==0) || (strcmp(pch,"and")==0) || (strcmp(pch,"or")==0) ||
	   (strcmp(pch,"not")==0) || (strcmp(pch,"xor")==0) )
	  return C_ARITHMETIC;
  else if ( strcmp(pch,"pop")==0 )
	  return C_POP;
  else if ( strcmp(pch,"push")==0 )
	  return C_PUSH;
  else if ( strcmp(pch,"label")==0 )
	  return C_LABEL;
  else if ( strcmp(pch,"goto")==0 )
	  return C_GOTO;
  else if ( strcmp(pch,"if")==0 )
	  return C_IF;
  else if ( strcmp(pch,"function")==0 )
	  return C_FUNCTION;
  else if ( strcmp(pch,"call")==0 )
	  return C_CALL;
  else if ( strcmp(pch,"return")==0 )
	  return C_RETURN;
  else return C_NULL;
}

void Trans_C_Arithmetic ( char* command, FILE* Asm)
{
}

void Trans_C_Push ( char* command, FILE* Asm)
{
}

void Trans_C_Pop ( char* command, FILE* Asm)
{
}

void Trans_C_Label ( char* command, FILE* Asm)
{
}

void Trans_C_Goto ( char* command, FILE* Asm)
{
}

void Trans_C_If ( char* command, FILE* Asm)
{
}

void Trans_C_Function ( char* command, FILE* Asm)
{
}

void Trans_C_Return ( char* command, FILE* Asm)
{
}

void Trans_C_Call ( char* command, FILE* Asm)
{
}