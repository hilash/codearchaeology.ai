#include <stdio.h>
#include <stdlib.h>
#define  INPUT 0
#define  MEMORY 1

void error (int type);
int*  create_array(int size);
int** mymalloc(int row, int col);

int main()
{
	int** matrix;
	int m,n;
	int i,j;

	printf("Enter two numbers between 2..100:\n");
	scanf("%d",&n);
	scanf("%d",&m);

	matrix = mymalloc(n,m);

	for (i=0; i<n;i++)
		for (j=0; j<m; j++)
			matrix[i][j]=i;

	for (i=0; i<n;i++)
	{
		for (j=0; j<m; j++)
			printf("%d ",matrix[i][j]);
		printf("\n");
	}
}

void error (int type) 
{
 	switch(type) {
	case INPUT:
	     printf("Input error!!!! ");
	     break;
	case MEMORY:
	      printf("memory allocation failed");
	      break;
	}
	exit (1);
	return;
}

int*  create_array(int size)
{
   int* a;
   a = (int*) malloc (size*sizeof(int));
       if ( a  == NULL )
             error(MEMORY);
   return a;
}

int** mymalloc(int row, int col)
{
	int**m;
	int i;
	if((m = (int**)malloc(row*sizeof(int*))) == NULL) return NULL;
	for(i=0;i<row;i++)   m[i] =   create_array(col);

       return m;
}