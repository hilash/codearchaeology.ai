#include <stdio.h>

int ReadYear();
int year;


int main()
{
	ReadYear();
	printf("%d",year);
	return 0;
}

int ReadYear()
{
	printf("Please Enter a year:");
	if(scanf("%d",&year)<1){\
		printf("Wrong input. bye bye!! :P\n");
		exit(1);}
	else return year;
}