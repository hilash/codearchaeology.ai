  /*****************************
	* ID: 
	* Assignment number: HW2
	* Exercise number: 01
	*****************************/

#include <stdio.h>

int main ()
{
	double rate,dollar,shekel;
	
	printf("Please enter today's dollar rate value\n"); /*part A of the Program - Enter dollar rate */
	if(scanf("%lf",&rate)<1){
		printf("Failed to read the dollar rate\n");
		return 1;
	}
	printf("Please enter value of dollars you want to exchange to shekels\n"); /*part B of the Program - exchange dollars to shekels  */
	if(scanf("%lf",&dollar)<1){
		printf("Failed to read the dollar's value\n");
		return 1;
	}
	printf("The value of %f dollars in shekels is %f\n",dollar,dollar*rate);
	
	printf("Please enter value of shekels you want to change to dollars\n"); /*part c of the Program - exchange shekels to dollars  */
	if(scanf("%lf",&shekel)<1){
		printf("Failed to read the shekel's value\n");
		return 1;
	}
	printf("The value of %f shekels in dollars is %f\n",shekel,shekel/rate);
	
	return 0;
}
