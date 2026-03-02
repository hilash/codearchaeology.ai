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

void Parser ( FILE* stream, FILE* Asm )
{
	char command[N];
	FILE* progVM;

	progVM=fopen("progVM.vm", "w" );
	initializer(stream, progVM);
	fclose(progVM);
	if( (progVM=fopen("progVM.vm", "r")) != NULL )
		while( fgets( command, N, progVM ) != NULL)
			Translate(command,Asm);
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
	fputc('\n',temp);
}

void Translate ( char* command, FILE* Asm)
{
	commandType cmd_t=cmdType(command);
	if ( cmd_t == C_ARITHMETIC )
		Trans_C_Arithmetic ( command, Asm );
	else if ( cmd_t == C_PUSH )
		Trans_C_Push ( command, Asm );
	else if ( cmd_t == C_POP )
		Trans_C_Pop ( command, Asm ); 
	else if ( cmd_t == C_LABEL )
		Trans_C_Label ( command, Asm ); 
	else if ( cmd_t == C_GOTO )
		Trans_C_Goto ( command, Asm );
	else if ( cmd_t == C_IF )
		Trans_C_If ( command, Asm ); 
	else if ( cmd_t == C_FUNCTION )
		Trans_C_Function ( command, Asm ); 
	else if ( cmd_t == C_RETURN )
		Trans_C_Return ( command, Asm ); 
	else if ( cmd_t == C_CALL )
		Trans_C_Call ( command, Asm ); 
	else
		printf("error!\n"); 
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
  else if ( strcmp(pch,"if-goto")==0 )
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
	char* pch;
	char cmd[N];
	static int eq_i=0,gt_i=0,lt_i=0;
	strcpy(cmd,command);
	pch = strtok (cmd," \n");
	if ( strcmp ( pch, "add" ) == 0 )
	{
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("D=M\n",Asm);
		fputs("A=A-1\n",Asm);
		fputs("M=M+D\n",Asm);
		fputs("D=A+1\n",Asm);
		fputs("@0\n",Asm);
		fputs("M=D\n",Asm);
	}
	if ( strcmp ( pch, "sub" ) == 0 )
	{
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("D=M\n",Asm);
		fputs("A=A-1\n",Asm);
		fputs("M=M-D\n",Asm);
		fputs("D=A+1\n",Asm);
		fputs("@0\n",Asm);
		fputs("M=D\n",Asm);
	}
	if ( strcmp ( pch, "neg" ) == 0 )
	{
		fputs("@0\n",Asm);
		fputs("AD=M-1\n",Asm);
		fputs("M=-M\n",Asm);
		fputs("@0\n",Asm);
		fputs("M=D+1\n",Asm);
	}
	if ( strcmp ( pch, "eq" ) == 0 )
	{
		fputs("@0\n",Asm);
		fputs("AM=M-1\n",Asm);
		fputs("D=M\n",Asm);
		fputs("A=A-1\n",Asm);
		fputs("D=M-D\n",Asm);
		fprintf( Asm, "@EQUAL_TRUE_%d\n", eq_i);
		fputs("D;JEQ\n",Asm);
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("M=0\n",Asm);
		fprintf( Asm, "@EQUAL_END_%d\n", eq_i);
		fputs("0;JMP\n",Asm);
		fprintf( Asm, "(EQUAL_TRUE_%d)\n", eq_i);
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("M=-1\n",Asm);
		fprintf( Asm, "(EQUAL_END_%d)\n", eq_i++);
	}
	if ( strcmp ( pch, "gt" ) == 0 )
	{
		fputs("@0\n",Asm);
		fputs("AM=M-1\n",Asm);
		fputs("D=M\n",Asm);
		fputs("A=A-1\n",Asm);
		fputs("D=M-D\n",Asm);
		fprintf( Asm, "@GREATER_TRUE_%d\n", gt_i);
		fputs("D;JGT\n",Asm);
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("M=0\n",Asm);
		fprintf( Asm, "@GREATER_END_%d\n", gt_i);
		fputs("0;JMP\n",Asm);
		fprintf( Asm, "(GREATER_TRUE_%d)\n", gt_i);
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("M=-1\n",Asm);
		fprintf( Asm, "(GREATER_END_%d)\n", gt_i++);
	}
	if ( strcmp ( pch, "lt" ) == 0 )
	{
		fputs("@0\n",Asm);
		fputs("AM=M-1\n",Asm);
		fputs("D=M\n",Asm);
		fputs("A=A-1\n",Asm);
		fputs("D=M-D\n",Asm);
		fprintf( Asm, "@LESS_TRUE_%d\n", lt_i);
		fputs("D;JLT\n",Asm);
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("M=0\n",Asm);
		fprintf( Asm, "@LESS_END_%d\n", lt_i);
		fputs("0;JMP\n",Asm);
		fprintf( Asm, "(LESS_TRUE_%d)\n", lt_i);
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("M=-1\n",Asm);
		fprintf( Asm, "(LESS_END_%d)\n", lt_i++);
	}
	if ( strcmp ( pch, "and" ) == 0 )
	{
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("D=M\n",Asm);
		fputs("A=A-1\n",Asm);
		fputs("M=D&M\n",Asm);
		fputs("D=A+1\n",Asm);
		fputs("@0\n",Asm);
		fputs("M=D\n",Asm);
	}
	if ( strcmp ( pch, "or" ) == 0 )
	{
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("D=M\n",Asm);
		fputs("A=A-1\n",Asm);
		fputs("M=D|M\n",Asm);
		fputs("D=A+1\n",Asm);
		fputs("@0\n",Asm);
		fputs("M=D\n",Asm);
	}
	if ( strcmp ( pch, "not" ) == 0 )
	{
		fputs("@0\n",Asm);
		fputs("AD=M-1\n",Asm);
		fputs("M=!M\n",Asm);
		fputs("@0\n",Asm);
		fputs("M=D+1\n",Asm);
	}
	if ( strcmp ( pch, "xor" ) == 0 )
	{
		/*  pXORq = (p|q) & !(p&q)  */
		/*  (p|q)  */
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("D=M\n",Asm);
		fputs("A=A-1\n",Asm);
		fputs("D=D|M\n",Asm);
		fputs("@12\n",Asm);
		fputs("M=D\n",Asm);
		/* !(p&q) */
		fputs("@0\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("D=M\n",Asm);
		fputs("A=A-1\n",Asm);
		fputs("D=D&M\n",Asm);
		fputs("D=!D\n",Asm);
		/*  (p|q) & !(p&q)  */
		fputs("@12\n",Asm);
		fputs("A=M\n",Asm);
		fputs("D=A&D\n",Asm);
		/*  update stack  */
		fputs("@0\n",Asm);
		fputs("M=M-1\n",Asm);
		fputs("A=M-1\n",Asm);
		fputs("M=D\n",Asm);
	}
}

void Trans_C_Push ( char* command, FILE* Asm)
{
	char* segment, *i;
	char cmd[N];
	strcpy(cmd,command);
	segment = strtok (cmd," \n");
	segment = strtok (NULL," \n");
	i = strtok (NULL," \n");
	if ( strcmp(segment,"constant") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
	}
	if ( strcmp(segment,"local") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@1\n",Asm);
		fputs("A=D+M\n",Asm);
		fputs("D=M\n",Asm);
	}
	if ( strcmp(segment,"argument") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@2\n",Asm);
		fputs("A=D+M\n",Asm);
		fputs("D=M\n",Asm);
	}
	if ( strcmp(segment,"this") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@3\n",Asm);
		fputs("A=D+M\n",Asm);
		fputs("D=M\n",Asm);
	}
	if ( strcmp(segment,"that") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@4\n",Asm);
		fputs("A=D+M\n",Asm);
		fputs("D=M\n",Asm);
	}
	if ( strcmp(segment,"temp") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@5\n",Asm);
		fputs("A=A+D\n",Asm);
		fputs("D=M\n",Asm);
	}
	if ( strcmp(segment,"pointer") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@3\n",Asm);
		fputs("A=A+D\n",Asm);
		fputs("D=M\n",Asm);
	}
	if ( strcmp(segment,"static") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@16\n",Asm);
		fputs("A=A+D\n",Asm);
		fputs("D=M\n",Asm);
	}
	/*push the value in D into stack, same for all*/
	fputs("@0\n",Asm);
	fputs("A=M\n",Asm);
	fputs("M=D\n",Asm);
	fputs("@0\n",Asm);
	fputs("M=M+1\n",Asm);
}

void Trans_C_Pop ( char* command, FILE* Asm)
{
	/*if we need another memory cell,use the stack*/
	char* segment, *i;
	char cmd[N];
	strcpy(cmd,command);
	segment = strtok (cmd," \n");
	segment = strtok (NULL," \n");
	i = strtok (NULL," \n");
	/* in D - the adress where to store */
	if ( strcmp(segment,"local") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@1\n",Asm);
		fputs("D=M+D\n",Asm);
	}
	if ( strcmp(segment,"argument") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@2\n",Asm);
		fputs("D=M+D\n",Asm);
	}
	if ( strcmp(segment,"this") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@3\n",Asm);
		fputs("D=M+D\n",Asm);
	}
	if ( strcmp(segment,"that") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@4\n",Asm);
		fputs("D=M+D\n",Asm);
	}
	if ( strcmp(segment,"temp") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@5\n",Asm);
		fputs("D=A+D\n",Asm);
	}
	if ( strcmp(segment,"pointer") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@3\n",Asm);
		fputs("D=A+D\n",Asm);
	}
	if ( strcmp(segment,"static") == 0 )
	{
		fputs("@",Asm);fputs(i,Asm);fputs("\n",Asm);
		fputs("D=A\n",Asm);
		fputs("@16\n",Asm);
		fputs("D=A+D\n",Asm);
	}
	fputs("@0\n",Asm);
	fputs("A=M\n",Asm);
	fputs("M=D\n",Asm);
	fputs("A=A-1\n",Asm);
	fputs("D=M\n",Asm);
	fputs("A=A+1\n",Asm);
	fputs("A=M\n",Asm);
	fputs("M=D\n",Asm);
	fputs("@0\n",Asm);
	fputs("M=M-1\n",Asm);
}

void Trans_C_Label ( char* command, FILE* Asm)
{
	char* label;
	char cmd[N];
	strcpy(cmd,command);
	label = strtok (cmd," \n");
	label = strtok (NULL," \n");
	fprintf( Asm, "(%s)\n", label);
}

void Trans_C_Goto ( char* command, FILE* Asm)
{
	char* label;
	char cmd[N];
	strcpy(cmd,command);
	label = strtok (cmd," \n");
	label = strtok (NULL," \n");
	fprintf( Asm, "@%s\n", label);
	fputs("0;JMP\n",Asm);
}

void Trans_C_If ( char* command, FILE* Asm)
{
	char* label;
	char cmd[N];
	strcpy(cmd,command);
	label = strtok (cmd," \n");
	label = strtok (NULL," \n");
	fputs("@0\n",Asm);
	fputs("M=M-1\n",Asm);
	fputs("A=M\n",Asm);
	fputs("D=M\n",Asm);
	fprintf( Asm, "@%s\n", label);
	fputs("D;JNE\n",Asm);
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