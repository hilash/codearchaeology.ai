/*****************************
* ID:
* Assignment number:03
*****************************/

#include <stdio.h>
int main()
{
	int a,b,c,d;
	
	/*PART A - RECEIVING INPUT*/
	printf("Please enter the 4 digits code:\n"); 
	
	a = getchar(); /*fitness level*/
	b = getchar(); /*together with c - number of students in ten thousands*/
	c = getchar(); /*together with b - number of students in thousands*/
	d = getchar(); /*number of classrooms in hundreds*/

	/*PART B - CHECKING INPUT*/
	if (!((a>='1') && (a<='9'))) /*the fitness must be in range between 1-9*/
		{printf("error!");
		return 1;}
	if (!((b>='0') && (b<='9'))) /*the number of students (in ten thousands) must be in range between 0-9*/
		{printf("error!");
		return 1;}
	if (!((c>='0') && (c<='9'))) /*the number of students (in thousands) must be in range between 0-9*/
		{printf("error!");
		return 1;}
	if ((b=='0') && (c=='0')) /*the number of students cant be 0*/
		{printf("error!");
		return 1;}
	if (!((d>='0') && (d<='9'))) /*the classrooms number(in hundreds) must be in range between 0-9*/
		{printf("error!");
		return 1;}

	/*PART C - PRINTING THE ORDER OF THE RESOURCES OF SCHOOL A*/
	printf("\nThe order of the resources of school A is:\n");
	printf("School A has %d00 Classrooms.\n",d-48); /*-48 because of the asci chars sequence*/
	printf("School A has %d%d000 Students.\n",b-48,c-48);
	printf("The fitness of the students in school A is in level %d.\n\n",a-48);

	/*PART D - CALCULATEING AND PRINTING THE RISK LEVEL*/
	if (((d>='7')&&(b>='6')&&(a>='7'))||((d=='9')&&(b>='8')&&((a>='1') && (a<='9'))))
		printf("The risk level is high.\n");
	else if((d<'7')&&(d>='3')&&(b<'6')&&(b>='3')&&(a<'7')&&(a>='3'))
		printf("The risk level is medium.\n");
	else if ((d<'3')&&(b<'3')&&(a<'3'))
		printf("The risk level is low.\n");
	else printf("The risk level is undefined.\n");

	return 0;
}
