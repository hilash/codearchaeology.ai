/*	ASSEMBLER PROGRAM					*/
/*	input: a proper prog.asm file			*/
/*	output: a proper prog.hack file			*/

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

typedef enum { false, true } bool;
typedef enum { A_COMMAND, C_COMMAND, L_COMMAND } commandType;

#define N 1000
#define isdigit(x) ((x) >= '0' && (x) <= '9')
#define isletter(c) ((c >= 'A') && (c <= 'Z')) || ((c >= 'a') && (c <= 'z'))

/* SYMBOLS ARRAY */
struct symbol_node {
    char name[N];
	int number;
}symbols[90000]={
	{"SP",0},{"LCL",1},{"ARG",2},{"THIS",3},{"THAT",4},

	{"R0",0},{"R1",1},{"R2",2},{"R3",3},{"R4",4},{"R5",5},
	{"R6",6},{"R7",7},{"R8",8},{"R9",9},{"R10",10},{"R11",11},
	{"R12",12},{"R13",13},{"R14",14},{"R15",15},
	
	{"SCREEN",16384},{"KBD",24576}
};

/*SYMBOLS FUNCTIONS*/
int GetAddress(char*);
void AddEntry(char*,int);

/*PARSER FUNCTIONS*/
void Parser(FILE*,FILE*);
void FirstPass(FILE*);

void initializer(FILE*,FILE*);
void DeleteWhiteSpacesAndComments(FILE*,FILE*);
void DeleteEmptyLines(FILE*,FILE*);

void Translate(char*, char*);
void pdest(char*,char*);
void pcomp(char*,char*);
void pjump(char*,char*);
commandType CommandType(char*);

char* fix_Lcmd(char*,char*);
char* copy(char*,char*);

/*CODE FUNCTIONS*/
char* cdest(char*);
char* ccomp(char*);
char* cjump(char*);



int main(int argc, char *argv[]) 
{
	FILE *f1, *f2;

	if (argc != 2){	/* not exactly one argument file*/
		printf("Incorrect number of arguments, enter Assembler path_of_the_file\n");
		return 1;
	}
	if ((fopen_s(&f1, argv[1], "r" ))!=0){
		printf("Can't open file %s\n",argv[1]);
		return 1;
	}

	fopen_s(&f2, "hila.hack", "w" );
	Parser(f1,f2); /* translate from .asm to hck*/
	fclose(f1);
	fclose(f2);
	return 0;
}

/*PARSER SECTION*/
void Parser(FILE *stream,FILE *hack)
/*	input: prog.asm			*/
/*	output: prog.hack		*/
{
	char command[N],hck[N]=""; 
	FILE* progAsm;
	fopen_s(&progAsm, "progAsm.asm", "w" );
	initializer(stream, progAsm); /* progAsm - the .asm program without whitespaces and etc. */
	fopen_s(&progAsm, "progAsm.asm", "r" );
	FirstPass(progAsm); /* construct the symbole table */
	if( fopen_s( &progAsm, "progAsm.asm", "r" ) == 0 )
	{ 
		while( fgets( command, N, progAsm ) != NULL)/*go through all the lines and translate each line*/
		{
			Translate(command,hck);
			fputs(hck,hack);
		}		
	}
	fclose(progAsm);
	remove("progAsm.asm");
}
void initializer(FILE *stream,FILE *progAsm) 
{
/*	input: file stream
	output: file progAsm - the stream file without whitespaces, comments and empty lines*/
	FILE *tmp;
	fopen_s(&tmp, "tmp.asm", "w" );
	DeleteWhiteSpacesAndComments(stream, tmp);/*put the program without spaces and comments in tmp*/
	fopen_s(&tmp, "tmp.asm", "r" );
	DeleteEmptyLines(tmp, progAsm);/*put the program without spaces and comments and empty lines in progAsm*/
	fclose(tmp);
	remove("tmp.asm");
	fclose(progAsm);
	fclose(stream);
}
void DeleteWhiteSpacesAndComments(FILE *stream,FILE* temp)/*delete all white spaces and comments*/
{
/*	input: file stream
	output: file temp - the stream file without whitespaces, comments*/
	char c,d;
	while ( (c=fgetc(stream))!=EOF )/*creating a file with no comments and white spaces*/
	{
		if ( (c=='/') && ((d=fgetc(stream))=='/') ){
			while ( (c!='\n') && (c!=EOF) )
				c=fgetc(stream);
			if ( (c=='\n'))
				fputc(c,temp);
		}
		else if ( (c!=' ') && (c!='	'))
				fputc(c,temp);
	}
	fclose(temp);
	fclose(stream);
}

void DeleteEmptyLines(FILE *stream,FILE* temp)
/*	input: file strem
	output: file temp - the stream file without empty lines	*/

{
	char c=fgetc(stream),d;
	int flag=0; /*beging of the file*/
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
	fclose(temp);
	fclose(stream);
}

commandType CommandType(char* str)
{
/*	input: command as a string
	output: the command type*/
	if (*str=='@')
		return A_COMMAND;
	else if (*str=='(')
		return L_COMMAND;
	else return C_COMMAND; 
}

void Translate(char* com, char* binary)
{
/*	input: command as a string in asm language
	output: the command type in binary*/
	char tmp[N]="",desti[N]="",compi[N]="",jumpi[N]="";
	int num,i;
	static int counter=16; /*the current ram adress filles*/
	strcpy(binary,"");
	if (CommandType(com)==A_COMMAND)
	{
		strcat(binary,"0");
		com++; /*skip the '@'*/
		if (isdigit(*com) != 0){/*if @number*/
			num=atoi(com);
			itoa(num,tmp,2);/*tmp - string of the binary value of xxx (@xxx)*/
		}
		else /*is @LABEL*/ 
		{
			com--;
			fix_Lcmd(tmp,com);/*in tmp - the label without () */
			i=GetAddress(tmp);
			if (i==-1) /*if its a varible*/
				AddEntry(tmp,counter++);
			i=GetAddress(tmp);	
			itoa(symbols[i].number,tmp,2);
		}
		if ((num=strlen(tmp))<15)
			while ((strlen(binary))<(16-num))
				strcat(binary,"0");

		strcat(binary,tmp);
		strcat(binary,"\n");
	}
	else if (CommandType(com)==C_COMMAND)
	{
		strcat(binary,"111");

		pcomp(com,compi);
		strcat(binary,ccomp(compi));

		pdest(com,desti);
		strcat(binary,cdest(desti));

		pjump(com,jumpi);
		strcat(binary,cjump(jumpi));
		strcat(binary,"\n");
	}
}

void pdest(char* com,char* des)
/*	input: command as a string in asm language
	output: the command dest as a string*/
{
	int i;
	bool flag=true;

	strcpy(des,"");
	for (i=0; (i<N) && (flag==true) ;i++)
	{
		if (com[i] == '=')
			flag=false; /*we can finish - there is a dest field*/
		else if  ((com[i] == ';') || (com[i] == '\0') || (com[i] == '\n')) /*we can finish - there is no dest field*/
		{
			strcpy(des,"");
			flag=false;
		}
		else des[i]=com[i];;
	}
}
void pcomp(char* com,char* comp)
/*	input: command as a string in asm language
	output: the command comp as a string*/
{
	int i=0,j=0;
	char tmp[N]="";
	strcpy(comp,"");
	pdest(com,tmp);
	if (strlen(tmp)!=0)/*there is dest feild - go to comp*/{
		while ( com[i] != '='){
			i++;
		}
		i++;
	}
	while ( (i<N) && (com[i] != ';') && (com[i] != '\0') && (com[i] != '\n')){
		comp[j]=com[i];
		i++;
		j++;
	}
}
void pjump(char* com,char* jmp)
/*	input: command as a string in asm language
	output: the command comp as a string*/
{
	int j=0,i=0;
	bool flag=true;
	strcpy(jmp,"");
	while ((i<N) && (flag==true))
	{
		if  ((com[i] == '\0') || (com[i] == '\n')) 
		{
			strcpy(jmp,"");
			flag=false;
		}
		else if (com[i] == ';')
		{
			i++;
			while ( (i<N) && (com[i] != '\0') && (com[i] != '\n') )
			{
				jmp[j]=com[i];
				i++;
				j++;
			}
			flag=false;
		}
		i++;
	}
	j++;
}


void FirstPass(FILE *prog)
{
	int pc=0;
	char cmd[N],label[N],tmp[N];
	while( fgets( cmd, N, prog ) != NULL)
	{
		if (CommandType(cmd)==L_COMMAND)
		{
			fix_Lcmd(label,cmd);
			AddEntry(label,pc);
			if (pc>0)
				--pc;
		}
		pc++;
	}
}

char *fix_Lcmd(char* dest,char* src)//return label without ()
{
   const char *p;
   char *q; 
 
   for(p = ++src, q = dest; (*p != '\0') &&  (*p != EOF) && (*p != '\n' ) && (*p != ')'); p++, q++)
	   *q = *p;
   *q = '\0';
 
   return dest;
}

char *copy(char* dest,char* src)
{
   const char *p;
   char *q;
 
   for(p = src, q = dest; (*p != '\0') && (*p != '\n') && (*p != EOF) ; p++, q++)
	   *q = *p;
   *q = '\0';
 
   return dest;
}



int GetAddress(char* label)
{
	int i=0;

	while ( (i<N) && (strcmp(symbols[i].name,label) != 0))
				i++;
	if  (strcmp(symbols[i].name,label) == 0)
		return i;
	else return -1;

}

void AddEntry(char* label, int num)
{
	int static i=23;
	symbols[i].number=num;
	copy(symbols[i].name,label);
	i++;
}
/*CODE SECTION */
char* cdest(char *str)
{
	if ( strcmp(str,"M")==0 ){
		return "001";
	}
	else if ( strcmp(str,"D")==0 ){
		return "010";
	}
	else if ( strcmp(str,"MD")==0 ){
		return "011";
	}
	else if ( strcmp(str,"A")==0 ){
		return "100";
	}
	else if ( strcmp(str,"AM")==0 ){
		return "101";
	}
	else if ( strcmp(str,"AD")==0 ){
		return "110";
	}
	else if ( strcmp(str,"AMD")==0 ){
		return "111";
	}
	else return "000"; /*null*/
}

char* ccomp(char *str)
{
	/*a==0*/
	if ( strcmp(str,"0")==0 ){
		return "0101010";
	}
	else if ( strcmp(str,"1")==0 ){
		return "0111111";
	}
	else if ( strcmp(str,"-1")==0 ){
		return "0111010";
	}
	else if ( strcmp(str,"D")==0 ){
		return "0001100";
	}
	else if ( strcmp(str,"A")==0 ){
		return "0110000";
	}
	else if ( strcmp(str,"!D")==0 ){
		return "0001101";
	}
	else if ( strcmp(str,"!A")==0 ){
		return "0110001";
	}
	else if ( strcmp(str,"-D")==0 ){
		return "0001111";
	}
	else if ( strcmp(str,"-A")==0 ){
		return "0110011";
	}
	else if ( strcmp(str,"D+1")==0 ){
		return "0011111";
	}
	else if ( strcmp(str,"A+1")==0 ){
		return "0110111";
	}
	else if ( strcmp(str,"D-1")==0 ){
		return "0001110";
	}
	else if ( strcmp(str,"A-1")==0 ){
		return "0110010";
	}
	else if ( strcmp(str,"D+A")==0 ){
		return "0000010";
	}
	else if ( strcmp(str,"D-A")==0 ){
		return "0010011";
	}
	else if ( strcmp(str,"A-D")==0 ){
		return "0000111";
	}
	else if ( strcmp(str,"D&A")==0 ){
		return "0000000";
	}
	else if ( strcmp(str,"D|A")==0 ){
		return "0010101";
	}
	/*a==1*/
	else if ( strcmp(str,"M")==0 ){
		return "1110000";
	}
	else if ( strcmp(str,"!M")==0 ){
		return "1110001";
	}
	else if ( strcmp(str,"-M")==0 ){
		return "1110011";
	}
	else if ( strcmp(str,"M+1")==0 ){
		return "1110111";
	}
	else if ( strcmp(str,"M-1")==0 ){
		return "1110010";
	}
	else if ( strcmp(str,"D+M")==0 ){
		return "1000010";
	}
	else if ( strcmp(str,"D-M")==0 ){
		return "1010011";
	}
	else if ( strcmp(str,"M-D")==0 ){
		return "1000111";
	}
	else if ( strcmp(str,"D&M")==0 ){
		return "1000000";
	}
	else if ( strcmp(str,"D|M")==0 ){
		return "1010101";
	}
}

char* cjump(char *str)
{
	if ( strcmp(str,"JGT")==0 ){
		return "001";
	}
	else if ( strcmp(str,"JEQ")==0 ){
		return "010";
	}
	else if ( strcmp(str,"JGE")==0 ){
		return "011";
	}
	else if ( strcmp(str,"JLT")==0 ){
		return "100";
	}
	else if ( strcmp(str,"JNE")==0 ){
		return "101";
	}
	else if ( strcmp(str,"JLE")==0 ){
		return "110";
	}
	else if ( strcmp(str,"JMP")==0 ){
		return "111";
	}
	else return "000"; /*null*/
}