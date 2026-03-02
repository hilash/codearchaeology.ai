#include <stdio.h>
#define N 10

bool digits[2*N+1];

void print(int arr[]);

int main()
{
	int i,j,num,twice,counter;
	bool twice_flag,one_flag;
	twice_flag=one_flag=false;

	for (i=0; i<2*N+1; i++)
		digits[N]=false;
	
	printf("Enter %d numbers between 1..%d:\n",N+1,2*N);
	
	counter=0;
	//init
	for (i=0;i<N+1; i++)
	{
		scanf("%d",&num);
		if (num==1)
		{
			one_flag=true;
		}
		if (digits[num]==true)
		{
			twice=num;
			twice_flag=true;
		}
		else digits[num]=true;

	}

	if (one_flag==true)
	{
		printf("there is one: %d %d\n",1,1);
	}
	else if (twice_flag==true)
		printf("twice: %d\n",twice);
	else
	{
		counter=0;
	//check
	//don't check if 1
	for (i=2; (i<=N) && (counter==0); i++)
	{
		if (digits[i]==true)
		{
			for (j=1; (j<=2*N/i) && (counter==0); j++)
				if ((i!=i*j)&&(digits[i*j]==true))
				{
					printf("i, i*j: %d %d\n",i,i*j);
					counter++;
				}
		}
	}
	if (counter==0)
		printf("no numbers were found");
	}
}