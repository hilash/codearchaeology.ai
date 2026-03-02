#include <stdio.h>
#define N 11
#define PARENT(i)  i/2
#define LEFT(i)  2*i
#define RIGHT(i)  2*i+1

void get_array(int arr[],int n);
void print_array(int arr[],int n);

void insertion_sort(int arr[],int n);
void selection_sort(int a[],int n);
//void merge_sort(int a[],int p, int r);
//void merge(int a[],int p,int q, int r);
//http://www.algorithmist.com/index.php/Merge_sort.cpp

int linear_search(int arr[],int n,int v,int heap_size);

void Heapify(int a[],int i,int heap_size); // O(lg(n))
void BulidHeap(int a[],int n); // O(n)
void HeapSort(int a[],int n); // O(nlg(n)) // doesn't sort the first cell
int HeapMaximum(int a[]); // O(1)
int HeapExtractMax(int a[],int n); // O(lg(n))
void HeapInsert(int a[],int n,int key); // O(lg(n))

int main()
{
	int a[N+2];
	
	get_array(a,N);
	print_array(a,N);
	////insertion_sort(a,N);
	//selection_sort(a,N);
	//HeapSort(a,N);
	//BulidHeap(a,N-1);
	print_array(a,N);
	HeapInsert(a,10,15);
	//printf("max: %d\n",HeapMaximum(a));
	//HeapExtractMax(a,N-1);
	print_array(a,N+1);
}

void get_array(int arr[],int n)
{
	int i;
	printf("Enter %d numbers:\n",n);
	for (i=0; i<n; i++)
		scanf("%d",&arr[i]);
}
void print_array(int arr[],int n)
{
	int i;
	printf("Array:  ",n);
	for (i=0; i<n; i++)
		printf(" %d",arr[i]);
	printf("\n");
}

void insertion_sort(int arr[],int n)
{
	int i,j,key;

	for ( j=1 ; j<n ; j++)
	{
		key = arr[j];
		//insert arr[j] into the sorted sequence arr[1..j-1]
		i=j-1;
		while ( (i>=0) && (arr[i]>key) )
		{
			arr[i+1]=arr[i];
			i--;
		}
		arr[i+1]=key;
	}
}

void selection_sort(int a[],int n) // from wikipedia
{
	int SmallestValue;
	for(unsigned int i = 0; i < n -1; i++)
	{
		SmallestValue = i;
		for(unsigned int currentValue = SmallestValue + 1; currentValue < n; currentValue++)
		{
			if(a[currentValue] > a[SmallestValue])
			{
				SmallestValue = currentValue;
			}
		}

		//swap the values
		int b = a[SmallestValue];
		a[SmallestValue] = a[i];
		a[i] = b;
	}
}

int linear_search(int arr[],int n,int v)
{
	int i;

	for ( i=0 ; i<n ; i++)
		if (arr[i]==v)
			return i;
	return -1;
}

void Heapify(int a[],int i,int heap_size)
{
	int l,r,largest;

	l=LEFT(i);
	r=RIGHT(i);
	if ( (l<=heap_size) && (a[l]>a[i]) )
		largest=l;
	else largest=i;
	if ( (r<=heap_size) && (a[r]>a[largest]) )
		largest=r;
	if ( largest != i )
	{
		//swap, r is the tmp varible
		r=a[largest];
		a[largest]=a[i];
		a[i]=r;

		Heapify(a,largest,heap_size);
	}
}
void BulidHeap(int a[],int n)
{
	for (int i=n/2; i>=1; i--)
		Heapify(a,i,n);
}
void HeapSort(int a[],int n)
{
	int i,tmp,heap_size=n-1;
	BulidHeap(a,n-1); // we don't use the cell a[0]
	for (i=heap_size; i>=2; i--)
	{
		// swap a[1] & a[i]
		tmp=a[1];
		a[1]=a[i];
		a[i]=tmp;
		heap_size--;
		Heapify(a,1,heap_size);
	}
}

int HeapMaximum(int a[])
{
	return a[1];
}

int HeapExtractMax(int a[],int n)
{
	int max;
	if (n<1)
		return -1; // heap underflow
	max = a[1];
	a[1] = a[n];
	Heapify(a,1,n-1);
	return max;
}

void HeapInsert(int a[],int n,int key)
{
	int i = n+1;
	while ( (i>1) && (a[PARENT(i)]<key) )
	{
		a[i]=a[PARENT(i)];
		if (i>1)
			i=PARENT(i);
	}
	a[i]=key;
}