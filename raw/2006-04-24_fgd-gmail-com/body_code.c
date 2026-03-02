#include <stdio.h>
#include <stdlib.h>

int main()
{
	printf("Please enter a word\n");
	betweenletters();
	printf("\n");
	return 0;
}
int betweenletters()
{
	int c;
	if((c=getchar())=='\n')
		return 1;
	else if(c<'a'||c>'z')
	{
		printf("You didn't typed a letter. bye bye!\n");
		exit(0);
	}
	
	if
		betweenletters();
		putchar(c);
}