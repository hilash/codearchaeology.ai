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
#include <time.h>
#include <math.h>

int** CreateCompleteGraph(int k);
void DeleteCompleteGraph(int** graph, int k);

int main()
{
	int k=80;
	int** graph = CreateCompleteGraph(k);
	
	if (graph == NULL){
		printf( "CreateCompleteGraph.c: Can't allocate memory\n" );
		return -1;
	}

	DeleteCompleteGraph(graph,k);
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
