#include <stdio.h>
#include <stdlib.h>
#define  INPUT 0
#define  MEMORY 1

void error (int type);
int*  create_array(int size);
int** mymalloc(int row, int col);

void print(int n, int m);
void print_row(int k); // serias of ****
	  			       //		    ****
void print_col(int k); // serias of **
	  			       //		    **
					   //		    **
					   //		    **
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

	if ((m%4==0)&&(n%2==0))
	{
		printf("we can print the array:\n\n");
		print(n,m);
	}
	else if ((m%4==2)&&(n%4==0))
	{
		printf("we can print the array:\n\n");
		print(n,m);
	}
	else printf("we cant print the array\n\n");

	/*for (i=0; i<n;i++)
	{
		for (j=0; j<m; j++)
			printf("%d ",matrix[i][j]);
		printf("\n");
	}*/
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

void print(int n, int m)
{
	int i,j;
	i=n;
	j=m;


	//while (n>0)
	//{
	//	if (m%4==0)
	//		print_row(m/4);
	//	if (m%4==2)
	//		print_col(m/2);
	//	n-=2;
	//}

	if (m%4==0)
	{
		while (n>0)
		{
			print_row(m/4);
			n-=2;
		}
	}
	else if (m%4==2)
	{
		while (n>0)
		{
			print_col(m/2);
			n-=4;
		}
	}
}

// prints k    ****
//			   ****
void print_row(int k) 
{
	static int j=1;
	//first row
	for (int i=1; i<=k;i++)
	{
		printf("%d %d %d %d ",j,j,j,j+1);
	}
	printf("\n");
	for (int i=1; i<=k;i++)
	{
		printf("%d %d %d %d ",j,j+1,j+1,j+1);
	}
	if (j==1)
		j=3;
	else j=1;
	printf("\n");
}
void print_col(int k)
{
	static int j=5;
	int i,p;
	
	//first row
	for (i=1,p=j; i<=k;i++)
	{
		printf("%d %d ",p,p);
		if (p==5)
			p=7;
		else p=5;
	}
	printf("\n");

	for (i=1,p=j; i<=k;i++)
	{
		printf("%d %d ",p+1,p);
		if (p==5)
			p=7;
		else p=5;
	}
	printf("\n");

		for (i=1,p=j; i<=k;i++)
	{
		printf("%d %d ",p+1,p);
		if (p==5)
			p=7;
		else p=5;
	}
	printf("\n");

			for (i=1,p=j; i<=k;i++)
	{
		printf("%d %d ",p+1,p+1);
		if (p==5)
			p=7;
		else p=5;
	}
	printf("\n");

	if (j==5)
		j=7;
	else j=5;
}