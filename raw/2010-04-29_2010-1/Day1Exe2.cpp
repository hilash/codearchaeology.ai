#include <iostream>

using namespace std;

__int64 Factorial(__int64 n)
{
	__int64 f = 1;

	for (__int64 i=n; i>1; i--)
		f *= i;

	return f;
}

__int64 Combination(__int64 n,__int64 k)
{
	__int64 up = 1;
	__int64 down = 1;

	for (__int64 i=n; i>=n-k+1; i--)
		up *= i;

	for (__int64 i=k; i>=1; i--)
		down *= i;

	return up/down;
}

__int64 foo(int max, int n, int k)
{
	int biggest_table = max;
	static __int64 total = 0;
	
	if (n==0)
		return 1;
	if (n==1)
		return 1;
	if ((k==0) && (max>0))
		return 0;

	if (max>n)
		max = n;

	for (biggest_table = max; biggest_table >=1; biggest_table--)
	{
		if (biggest_table == n - biggest_table)
			//total += (((biggest_table+1)*biggest_table)/2)*Factorial(biggest_table-1)*foo(biggest_table,n-biggest_table, k-1);
		{
			
		}
	
		else if ((biggest_table*k>=n) && (biggest_table == n - biggest_table))
			total += Combination(n,biggest_table)*Factorial(biggest_table-1)*foo(biggest_table,n-biggest_table, k-1);
	}

	return total;
}

void main()
{
	cout << foo(3,3,3) << endl;
}