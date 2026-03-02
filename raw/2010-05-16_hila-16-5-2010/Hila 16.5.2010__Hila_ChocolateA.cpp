// Hila
// dinamic programming
// i don't have time to make any more improvments (going at 15:00)

#include <cstdio>
using namespace std ;

int N;
int A[1030][1030];

inline int MIN(int nX, int nY)
{
    return nX > nY ? nY : nX;
}
inline int MAX(int nX, int nY)
{
    return nX < nY ? nY : nX;
}

int main()
{
	int n,m,max;
	scanf("%d",&n);
	scanf("%d",&m);
	N = MAX(n,m);

	if (n==m)
		printf("0\n");
	else if (n==1)
		printf("%d\n",m-1);
	else if (m==1)
		printf("%d\n",n-1);
	else
	{
		if (n>m)
		{
			int tmp = n;
			n = m;
			m = tmp;
		}
		bool flag = true;
		for (int i=1; i<=N; i++)
		{
			A[i][i] = 0;
			A[i][1] = i-1;
			A[1][i] = i-1;
		}
		for (int i=2; i<=n && flag; i++)
		{
			for (int j=i; j<=m; j++)
			{
				if (i!=j)
				{
					// count minimum cut when horizontal cut --
					int min = 100000;
					for (int k=1; k<=i/2; k++)
					{
						min = MIN(min,A[k][j]+A[i-k][j]);
					}
					// count minimum cut when vertical cut |
					for (int k=1; k<=j/2; k++)
					{
						min = MIN(min,A[i][k]+A[i][j-k]);
					}
					A[j][i] = A[i][j] = min + 1;
					if ( (i==n && j==m) || (j==n && i==m))
					{
						printf("%d\n",A[n][m]);
						flag = false;
						break;
					}
				}
			}
		}
	}
	//for (int i=1; i<=N; i++)
	//{
	//	for (int j=1; j<=N; j++)
	//	{
	//		printf("%2d ",A[i][j]);
	//		
	//	}
	//	putchar('\n');
	//}
	fflush(stdout);
	return 0;
}