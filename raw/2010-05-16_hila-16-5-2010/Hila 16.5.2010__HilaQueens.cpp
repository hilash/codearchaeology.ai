// N queen Problem by Hila
// simple backtaracking

#include <cstdio>
#include <bitset>
#define n 1000

int N;
using namespace std;
// indexes are from 1...N for convinece

int* row;
bitset<n+1> col;
bitset<2*n+1> diagA; // main diagnols
bitset<2*n+1> diagB; // second diagnols

unsigned long long  count = 0 ;

inline int DA(int i,int j)
 {
   return j-i+N;
 }

inline int DB(int i,int j)
 {
   return i+j;
 }

void putQueen(int r)
{
	static bool flag = false;

	if (flag)
		return;

	if (r==N+1)
	{
		if (count==0)
		{
			flag = true;
			for (int i = 1; i<=N; i++)
			{	
				printf("%d\n",row[i]);
			}
		}
		count ++;
	}
	for (int i=1; i<=N; i++)
	{
		if ( col[i]==0 && diagA[DA(r,i)]==0 && diagB[DB(r,i)]==0 )
		{
			row[r] = i;
			col[i] = 1;
			diagA[DA(r,i)] = 1;
			diagB[DB(r,i)] = 1;
			putQueen(r+1);
			diagB[DB(r,i)] = 0;
			diagA[DA(r,i)] = 0;
			col[i] = 0;
		}
	}
}
int main()
{
	scanf("%d",&N);
	row = new int[N+1];
	if (N!=1 && N<4)
		printf("NO SOLUTION\n");
	else
	putQueen(1);
	fflush(stdout);
	return 0;
}