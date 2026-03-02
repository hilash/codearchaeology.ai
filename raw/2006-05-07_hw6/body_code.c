/*****************************
* ID: 
* Assignment number: 06
* Exercise number: 1
*****************************/


#include <stdio.h>
#include <stdlib.h>

#define SIZE 10

int main(){
	
	int i, c, dotcounter=0, minussigncounter=0, dotindex=0, stringend=0;
	double sum, multiplycounter;
	char str[SIZE];

	/*INPUT RECIEVING  - Number a string with 9 chars, the last is '\0'*/
	printf("Please enter a number with 9 chars:\n");
	for (i=0; i<SIZE-1 && (c=getchar()) != EOF && c != '\n';  i++)
		str[i] = c;
	str[i] = '\0';
	stringend=i;

	/*INPUT CHECKING*/
	for(i=0; str[i]!='\0'; i++)
	{
		/*checking the input is digits and/or minus signs and/or dots*/
		if (! ( ((str[i]>='0') && (str[i]<='9')) || (str[i]=='.') || (str[i]=='-')))
		{
			printf("\nilligal chars. bye!\n");
			exit(1);
		}
		/*checking that there is no more then one dot in the input*/
		else if (str[i]=='.')
		{
			dotindex=i;
			dotcounter++;
			if (dotcounter>1){
				printf("\nYou inserted more then one dot. bye!\n");
				exit(1);}	
		}
		
		/*checking that there is no more then one minus sign and if there is so it's on the begining od the input*/
			else if (str[i]=='-')
		{
			minussigncounter++;
			if (minussigncounter>1){
				printf("\nYou inserted more then one minus sign. bye!\n");
				exit(1);}
			if ((minussigncounter==1) && (str[0]!='-')){
				printf("\nYou inserted minus sign not in the begining of the number. bye!\n");
				exit(1);}	
		}
	}



	/*CONVERTING THE STIRING NUMBER INTO NUMBER TYPE DOUBLE*/
	
	/*in case there is not a dot:*/
	if (dotcounter==0)
	{

		/*digits in case there is no minus sign*/
		if(minussigncounter==0)
			for (sum=0, i=stringend-1, multiplycounter=1 ; 0<=i ; i--, multiplycounter*=10)
				sum+=((str[i]-48)*multiplycounter);
		
		/*digits in case there is minus sign*/
		else if(minussigncounter==1)
		{
			for (sum=0, i=stringend-1, multiplycounter=1 ; 1<=i ; i--, multiplycounter*=10)
				sum+=((str[i]-48)*multiplycounter);
			sum*=(-1);
		}	
	}


	/*in case there is a dot:*/
	else
	{
		/*digits after the dot*/
		for (i=dotindex+1,sum=0, multiplycounter=10.0 ; i<stringend ; i++, multiplycounter*=10)
			sum+=((str[i]-48)/multiplycounter);

		/*digits before the dot in case there is no minus sign*/
		if(minussigncounter==0)
			for (i=dotindex-1, multiplycounter=1 ; 0<=i ; i--, multiplycounter*=10)
				sum+=((str[i]-48)*multiplycounter);
		
		/*digits before the dot in case there is minus sign*/
		else if(minussigncounter==1)
		{
			for (i=dotindex-1, multiplycounter=1 ; 1<=i ; i--, multiplycounter*=10)
				sum+=((str[i]-48)*multiplycounter);
			sum*=(-1);
		}	
	}

	printf("\nYour number in double type is:%f\n", sum);

	return 0;
}
