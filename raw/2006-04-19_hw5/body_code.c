/*****************************
* ID: 
* Assignment number: 05
* Exercise number: 01
*****************************/

/*This program get as input two natural positive numbers which represent 
year and month and print the calendar of the month in the specific year.*/

#include <stdio.h>
#include <stdlib.h>
#define TRUE 1
#define FALSE 0

int ReadYear();
int ReadMonth();
int IsLeapYear(int);
int NumOfDays(int);
int YearStartDay(int);
void PrintCalendar(int, int);

int year, month;

int main()
{
	ReadYear();
	ReadMonth();
	PrintCalendar (year, month);
	return 0;
}

/*This function reads a year from the user and returns it.*/
int ReadYear()
{
	printf("Please enter a year between 1 to 9999:\n");
	if(scanf("%d",&year)<1){\
		printf("Wrong input. bye bye!! :P\n");
		exit(1);}
	else if((year<1)||(year>9999)){
		printf("Wrong input. bye bye!! :P\n");
		exit(1);}
	else return year;
}

/*This function reads a month from the user and returns it.*/
int ReadMonth()
{
	printf("Please enter a month between 1 to 12:\n");
	if(scanf("%d",&month)<1){\
		printf("Wrong input. bye bye!! :P\n");
		exit(1);}
	else if((month<1)||(month>12)){
		printf("Wrong input. bye bye!! :P\n");
		exit(1);}
	else return month;
}

/*This function gets a year and returns TRUE if the year is leap or FALSE if isn't.*/
int IsLeapYear(int year)
{
	if (year%4==0 && (! ((year%100==0) && (year%400!=0))) )
	{
		return TRUE;
	}
	else
	{
		return FALSE;
	}
}

/*This function gets a month and return the number of days in the month.*/
int NumOfDays(int monthcounter)
{
	switch(monthcounter)
	{
		case 1: case 3: case 5: case 7: case 8: case 10: case 12:
			return 31;
		case 2:
			if (IsLeapYear(year)==1)
				return 29;
			else if (IsLeapYear(year)==0)
				return 28;
		default: 
			return 30;
	}
}

/*This function gets a year and returns the day in the week when the year starts.*/
int YearStartDay(int year)
{
	int yearcounter=0000; /*Based on that year 0 had startes in day 0 [simple logical sense], or/and on the fact that year 1 started in day 2 (Monday).*/
	int dayscounter=0;
	for(dayscounter=0, yearcounter=0000; yearcounter<year; yearcounter++)
	{
		if(IsLeapYear(yearcounter)==1)
			dayscounter+=2;
		else
			dayscounter++;
	}
	if (dayscounter%7==0)
		return 7;
	else
		return dayscounter%7;
}

/*This function gets year and month and print the calendar of the month in the specific year.*/
void PrintCalendar (int year, int month)
{
	int monthcounter, daysinmonthcounter=0, spacecounter, spacecounter2, dayscounter=1, counter;
	
	printf("\nSu   Mo   Tu   We   Th   Fr   Sa\n");
	
	for(monthcounter=1; monthcounter<month; monthcounter++)
		daysinmonthcounter+=NumOfDays(monthcounter);

	spacecounter=(YearStartDay(year)-1+daysinmonthcounter)%7;
	for(spacecounter2=0; spacecounter2<spacecounter; spacecounter2++)
		printf("     ");
	
	counter=7-spacecounter;
	for(;counter<NumOfDays(month);counter+=7){
		for(; dayscounter<=counter; dayscounter++)
			printf("%-5d", dayscounter);
		printf("\n");}

	/*Printing the final line of numbers of the days in the calendar.*/
	for(;dayscounter<=NumOfDays(month);dayscounter++)
		printf("%-5d", dayscounter);
	printf("\n\n");
	return;
}
