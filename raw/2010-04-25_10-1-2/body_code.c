#include <stdio.h>
#define N 10

int foo(int n);

bool digits[N];
int factor[N];

int main()
{
	int i;

	for (i=0;i<N;i++)
		digits[i]=true;

	factor[0]=1;
	for (i=1;i<=N;i++)
		factor[i]=(i+1)*factor[i-1];

	//for (i=0;i<N;i++)
	//	printf("%d ",factor[i]);
		

	printf("\n\n%d",foo(N));

	return 0;
}

int foo(int n)
{
	int digit,i,segment;

	digit=getchar()-48;
	digits[digit]=false;
	//printf("%d",digit);

	//in the first time don't chack the digits array a[]
	if (n == N) // exactly the same like the else part 
	{
		return digit*(factor[n-1]/n)+foo(n-1);
	}
	else if ( n == 1 )
	{
		// do it!!!!!
		return 1;
	}
	else
	{
		//find in which 'segment' it is at
		//go through the array digits[] and count the 'true' till we reach
		//the digit d;
		segment=0;
		for (i=0;i<digit;i++)
			if (digits[i]==true)
				segment++;

		return segment*(factor[n-1]/n)+foo(n-1);
	}	
}