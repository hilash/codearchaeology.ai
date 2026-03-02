/*****************************************************************
	This program finds all cliques of size <= Q in a graph
	in a linear time of the size of the cliques.
	time = O(number of cliques * n);
	
	to be more specific:
		O(Find Q-Cliques)= (number of Cliques of size (Q-1)) * (Q-1) * n  + O(Find (Q-1)-Cliques)...

	how?
	STEP A: CreateGraph. the graph is in the shape of adjacency list (map<int,set<int>> graph;)
	STEP B: FindKCliques. find all cliques of size 3.
		for each 3-clique, see if can form a 4-clique. find all cliques of size 4 that way.
		for each 4-clique, see if can form a 5-clique. find all cliques of size 5.
		...
		for each (Q-1)-clique, see if can form a Q-clique.

	
	By Hila Shmuel, 
	21.5.2010
*****************************************************************/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <math.h>

struct list_el {
   int* val;
   struct list_el * next;
};

typedef struct list_el list;

int** CreateCompleteGraph(int k);
void DeleteCompleteGraph(int** graph, int k);

int** CreatePrimesGraph(int k, int* n, int** dic);
void DeletePrimeGraph(int** graph, int k);
void PrintPrimeList(list* l,int max_klik,int* dictionary);

list* FindAllKliks(int** graph, int vertices, int max_klik);
void PrintList(list* l,int max_klik);


int IsPrime(int x);

int main()
{
	int* dic = NULL;
	int vertices = 0;
	int** graph = CreatePrimesGraph(10000, &vertices,&dic);
	list *l;
	time_t start,end;
	double differ;
	
	if (graph == NULL)
		return -1;

	putchar('*');

	time(&start);
	l = FindAllKliks(graph, vertices, 5);
	time(&end);
	differ = diff(end - start);
	printf("\n time: %lf\n",differ);

	PrintPrimeList(l,5,dic);

	DeletePrimeGraph(graph, vertices);
	return 0;
}

int** CreateCompleteGraph(int k)
{
	int i,j;
	int** graph;

	/* alocate adjacency matrix */
	if( (graph = (int**)calloc(k,sizeof(int*))) == NULL){
		printf( "CreateCompleteGraph.a: Can't allocate memory\n" );
		return NULL;
	}
	for (i=0; i<k; i++){
		if ( (graph[i] = (int*)calloc(k,sizeof(int))) == NULL ){
			printf( "CreateCompleteGraph.b: Can't allocate memory\n" );
			return NULL;}
	}

	/* create complete graph */
	for (i=0; i<k; i++)
		for (j=i+1; j<k; j++)
			graph[i][j] = 1;
	
	return graph;
}

void DeleteCompleteGraph(int** graph, int k)
{
	int i;
	for (i=0; i<k; i++)
		free(graph[i]);
	free(graph);
}

int** CreatePrimesGraph(int k, int* n, int** dic)
{
	int i,j = 1;
	int** graph;
	int* primes;
	int* shift;
	int* dictionary;

	putchar('1');

	/* find primes from 2..k */
	if( (primes = (int*)calloc(k+1,sizeof(int))) == NULL){
		printf( "CreatePrimesGraph.a: Can't allocate memory\n" );
		return NULL;
	}
	for (i=3; i<=k; i+=2){
		if (IsPrime(i)==1){
			primes[i] = 1;
			j++;
		}
	}

	if( (dictionary = (int*)calloc(j,sizeof(int))) == NULL){
		printf( "CreatePrimesGraph.b: Can't allocate memory\n" );
		return NULL;
	}

	if( (shift = (int*)calloc(j,sizeof(int))) == NULL){
		printf( "CreatePrimesGraph.c: Can't allocate memory\n" );
		return NULL;
	}

	dictionary[0] = 2; k = 3; i = 1;
	while (i<j)
	{
		while (primes[k] != 1)
			k++;
		dictionary[i]=k;
		i++;
		k++;
	}

	free(primes);


	putchar('2');

	/*shift array - for number x, has the number (10^lengh(x)); */
	for (k = 10, i=0; i<j;k *= 10)
	{
		while(dictionary[i]<k && i<j)
		{
			shift[i++] = k;
		}
	}

	putchar('3');


	/* alocate adjacency matrix */
	if( (graph = (int**)calloc(j,sizeof(int*))) == NULL){
		printf( "CreatePrimesGraph.d: Can't allocate memory\n" );
		return NULL;
	}
	for (i=0; i<j; i++){
		if ( (graph[i] = (int*)calloc(j,sizeof(int))) == NULL ){
			printf( "CreatePrimesGraph.e: Can't allocate memory\n" );
			return NULL;}
	}


	putchar('4');
	/* create prime graph */
	for (i=0; i<j; i++){
		for (k=i+1; k<j; k++){
			if ( IsPrime(dictionary[i]*shift[k]+dictionary[k])== 1
				&& IsPrime(dictionary[k]*shift[i]+dictionary[i]) == 1 )
			{
				graph[i][k] = 1;
			}
		}
	}

	*dic = dictionary;

	*n = j;

	free(shift);

	putchar('5');
	putchar('\n');
	
	return graph;
}

void DeletePrimeGraph(int** graph, int k)
{
	int i;
	for (i=0; i<k; i++)
		free(graph[i]);
	free(graph);
}

list* FindAllKliks(int** graph, int vertices, int max_klik)
{
	list * curr, * head;
	list * kcurr, * khead;
	int i,j,klik_size;
	int* candidate;
	head = NULL;

	if (max_klik == 1)
	{
		printf( "FindAllKliks.a: klik size must be bigger then 1\n" );
		return NULL;
	}

	/* every edge is a 2-klik */
	for (i=0; i<vertices; i++){
		for (j=i+1; j<vertices; j++){
			if (graph[i][j])
			{
				if ( (curr = (list*)malloc(sizeof(list))) == NULL ){
					printf( "FindAllKliks.b: Can't allocate memory\n" );
					return NULL;
				}
				if ( (curr->val = (int*)malloc(2*sizeof(int))) == NULL ){
					printf( "FindAllKliks.c: Can't allocate memory\n" );
					return NULL;
				}
				curr->val[0] = i;
				curr->val[1] = j;
				curr->next  = head;
				head = curr;
			}
		}
	}

	khead = head;

	if ( (candidate = (int*)malloc(vertices*sizeof(int))) == NULL ){
		printf( "FindAllKliks.d: Can't allocate memory\n" );
		return NULL;
	}

	for (klik_size = 3; klik_size<= max_klik; klik_size++)
	{
		curr = head;
		khead = NULL;
		while(curr) /* find (klik_size) kliks. go over all (klik_size - 1 )kliks and search for candidates. */
		{
			if (curr->val[klik_size-2] == vertices - 1)
			{
				curr = curr->next ;
				continue;
			}
			if (curr->val[klik_size-2] < vertices - 1)
				memset (candidate+curr->val[klik_size-2] + 1,0,(vertices - curr->val[klik_size-2] - 1)*sizeof(int));
				
			/* in curr->val we have a klik of size (klik_size - 1).*/
			for (i=0; i<klik_size-1; i++)
			{
				for (j = curr->val[klik_size-2] + 1; j<vertices; j++)
				{
					candidate[j] += graph[curr->val[i]][j];
				}
			}

			for (i = curr->val[klik_size-2] + 1; i<vertices; i++)
			{
				if (candidate[i]==klik_size-1) // we have a klik!
				{
					if ( (kcurr = (list*)malloc(sizeof(list))) == NULL ){
						printf( "FindAllKliks.e: Can't allocate memory\n" );
						return NULL;
					}
					if ( (kcurr->val = (int*)malloc((klik_size)*sizeof(int))) == NULL ){
						printf( "FindAllKliks.f: Can't allocate memory\n" );
						return NULL;
					}
					for (j=0; j<klik_size-1; j++)
						kcurr->val[j] = curr->val[j];
					kcurr->val[j] = i;
					kcurr->next  = khead;
					khead = kcurr;
				}
			}

			curr = curr->next ;
		}

		// delete "head" - the old klik list, and make head = khead
		curr = head;
		head = khead;
		khead = curr;
		// delete khead
		while(khead)
		{
			curr = khead->next;
			free(khead->val);
			free(khead);
			khead = curr;
		}
	}
	return head;
}

void PrintList(list* l,int max_klik)
{
	int i;
	while(l)
	{
		for (i=0; i<max_klik; i++)
			printf("%d, ",l->val[i]);
		putchar('\n');
		l = l->next ;
	}
}

void PrintPrimeList(list* l,int max_klik,int* dictionary)
{
	int i;
	while(l)
	{
		for (i=0; i<max_klik; i++)
		{
			printf("%d, ",dictionary[l->val[i]]);
		}
		putchar('\n');
		l = l->next ;
	}
}
int IsPrime(int x)
{
	int i;
	if (x!=2 && x%2==0)
		return 0;
	for (i=3; i<=sqrt(x); i++)
		if (x%i==0)
			return 0;
	return 1;
}
