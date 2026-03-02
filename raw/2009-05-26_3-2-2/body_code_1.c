#include <stdio.h>
#define N 5

bool digits[2*N+1];

void print(int arr[]);

int main()
{
	int i,j,num,twice;
	bool flag,one;
	flag=one=false;

	for (i=0; i<2*N+1; i++)
		digits[N]=false;
	
	printf("Enter %d numbers between 1..%d:\n",N+1,2*N);
	
	//init
	for (i=0;i<N+1; i++)
	{
		scanf("%d",&num);
		//if (num==1)
		//{
			
		//}
		if (digits[num]==true)
		{
			printf("!!!");
			twice=num;
			flag=true;
		}
		else digits[num]=true;
	}
	if (flag==true)
		printf("twice: %d\n",twice);


	//check
	//don't check if 1
	for (i=2; i<=N; i++)
	{
		if (digits[i]==true)
		{
			for (j=1; j<=2*N/i; j++)
				if (digits[i*j]==true)
					printf("i, i*j: %d %d\n",i,i*j);
		}
	}
}

void print(int arr[])
{
	for (int i=1; i<N+1; i++)
		printf("%d ",arr[i]);
	printf("\n");
}