#include <stdio.h>

void foo(int n,int k,int t,int* sum)
{
	// n = digits range
	// k = current index position
	static char digits[100];
	int i,j;

	if (k==0)
	{
		j=0;
		for (i=t; i>0; i--)
		{
			//putchar(digits[i]+48);
			j+=digits[i];
		}
		sum[j]++;
		//printf("   %d\n\n",j);
		/*if (j==t*n)
			for (i=1*t; i<=t*n; i++)
			{
				printf("%5d %d\n",i,sum[i]);
			}*/
	}
	else
	{
		for (i=1; i<=n; i++)
		{
			digits[k]=i;
			foo(n,k-1,t,sum);
		}
	}
	return;
}

void main()
{
	// when peter will win? if colin sum is smaller.

	// prob(petersum=x wins) = prob(colinsum=1) +  prob(colinsum=2) + prob(colinsum=x-1)
	// prob(peter wins) = prob(petersum=x)*prob(petersum=x wins) for each x=1*9... 4*9

	// prob(petersum=x) = (1/(4^9))*number of times when petersum=x
	// prob(colinsum=x) = (1/(6^6))*number of times when colinsum=x

	static int petersum[4*9+1];
	static int colinsum[6*6+1];

	foo(4,9,9,petersum);
	foo(6,6,6,colinsum);

	double peterwins=0;
	double petersumxwins=0;
	unsigned long long z;
	int y=0;

	for (int x=9; x<=36; x++)
	{
		// proccess of thinking:
		//peterwins + = prob(petersum=x)*prob(petersum=x wins) ==>
		//peterwins + = (1/(4^9))*number of times when petersum=x *prob(petersum=x wins) ==>
		//peterwins + = (1/(4^9))*petersum[x]*prob(petersum=x wins)
		// move out 1/4^9 from the sum
		//peterwins + = petersum[x]*prob(petersum=x wins) ==> in the end of the loop

		// calc prob(petersum=x wins):

		// prob(petersum=x wins) = prob(colinsum=1) +  prob(colinsum=2) + prob(colinsum=x-1)
		// prob(colinsum=x) = (1/(6^6))*number of times when colinsum=x
		// ==> prob(petersum=x wins) = (1/(6^6))*number of times when colinsum=1 +  (1/(6^6))*number of times when colinsum=2.. + prob(colinsum=x-1)
		// ==> prob(petersum=x wins) = (1/(6^6))* [ number of times when colinsum=1 + ... +number of times when colinsum=x-1]
		
		// can do it without loop
		for (;y<x; y++)
		{
			petersumxwins += colinsum[y];
		}
		
		
		peterwins += petersum[x]*petersumxwins;
	}
	z = 262144; // 4^9
	peterwins /= z;

	z = 46656;   //6^6
	peterwins /= z;

}