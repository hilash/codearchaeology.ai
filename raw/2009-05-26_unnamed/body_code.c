#include <stdio.h>
#define N 4

bool digits[N+1]; //
int a[N+1];		//in this array - the digits by order
int foo(int n);
void print(int arr[]);

int main()
{
	int i;

	//1..N

	
	//init
	for (i=0; i<N+1; i++)
	{
		a[N]=0;
		digits[N]=false;
	}

	foo(1);
}

int foo(int n)
{
	int i;

	for (i=1; i<N+1; i++)
	{
		digits[i]=true;
		a[n]=i;
		if (n!=N)
			foo(n+1);
		else print(a);
		digits[i]=false;

	}
	return 0;
}

void print(int arr[])
{
	for (int i=1; i<N+1; i++)
		printf("%d ",arr[i]);
	printf("\n");
}