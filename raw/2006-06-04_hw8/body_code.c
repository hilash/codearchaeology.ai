/*****************************
* ID: 
* Assignment number: 8
* Exercise number: 1
*****************************/

/*This program gets a string and prints it after cutting the spaces from the string 
[instead of spaces the program prints one single space].*/

#include <stdio.h>

#define TRUE 1
#define FALSE 0
#define MAX 100

int CutSpaces(char *s);

int main()
{
	char s[MAX];
	char *ptr = s;
	char c;

	/*get input*/
	printf("enter string:");
	for(; (c=getchar())!='\n'; ptr++)
		*ptr=c;
	*ptr='\0';

	/*calling to the function that print the string without spaces*/
	CutSpaces(s);
	printf("\n");
	return 0;
}
int CutSpaces(char *s)
{
	for( ; *s ; s++){
		if ((*s!=' ')&&(*s!='\t'))
			printf("%c", *s);
		else if ( ((*s==' ') || (*s=='\t'))  &&  (*(s-1)!=' ')  &&  (*(s-1)!='\t') )
			printf(" ");
	}
	return 0;
}
