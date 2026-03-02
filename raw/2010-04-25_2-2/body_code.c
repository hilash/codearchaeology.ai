#include <stdio.h>
#define N 10

void foo(int d,int n,bool first);

bool digits[N];
int factor[N];

int main()
{
	int i,j;

	for (i=0;i<N;i++)
		digits[i]=true;

	factor[0]=1;
	for (i=1;i<=N;i++)
		factor[i]=(i+1)*factor[i-1];

	//for (i=0;i<N;i++)
	//	printf("%d ",factor[i]);

	//scanf("%d",&number);
	for(i=1;i<=12;i++)
	{
		for (j=0;j<N;j++)
			digits[j]=true;
		printf("\n%d  ",i);
		foo(i,N,true);
	}
		printf("\n");

	return 0;
}

void foo(int d, int n,bool first)
{
	int j,digit,i;
	int truesCounter=0;
	//decide the which digit to print
	if(first==true)
	{
		if(d==1)
		{
			printf("%d",0);
			digits[0]=false;
		}
		else
		{
			printf("%d",(d-1)/(factor[n-1]/n));
			digits[(d-1)/(factor[n-1]/n)]=false;
		}
	}
	else
	{
		//find the correct digit after mixups

		//this gives me the index of the digit
		if(d==1)
		{
			digit=0;
		}
		else
		{
			digit=(d-1)/(factor[n-1]/n);
		}
		//find in the array the fit digit

		//digits[digit]=false;
		for(i=0;i<N;i++)
		{
			truesCounter+=digits[i];
			if ((truesCounter-1)==digit)
			{
				digit=i;
				break;
			}
		}
		digits[digit]=false;
		printf("%d",digit); 

	}
	
	//call to foo() again with the segment number
	if(n==1)
	{
		return;
	}
	else
	{
		j=1+(d-1)%(factor[n-1]/n);
		//printf("\nj n : %d %d\n",j,n-1);
		 foo(j,n-1,false);
	}
}