//============================================================================
// Name        : AllCycles.cpp
// Author      : Anwar Mamat
//       : anwar at cse.unl.edu
// Version     : 1.0
// Date   : 02/18/2009
// Description : Enumerates all elementary cycles in a graph.
// The input file is given as:
// number of vertices, number of edges
// all edges(in random order)
//============================================================================

#include <iostream>
#include <fstream>
#include <set>
#include <vector>
#include<iterator>
#include <stack>
using namespace std;

const unsigned int N = 100;

int G[N+1][N+1]={0};
int visited[N+1] = {0};
int father[N+1] = {0};
int lenght[N+1] = {0};
int head[N+1] = {0};
int edges_arr[2][N+1] = {0};
int edge_ptr;

int* arr[N+1] = {0}; // an array for the circles


//************************************
//
// print Graph matrix
//
//************************************
void print(int G[N+1][N+1], int vertex)
{
int i,j;
for(i = 0 ; i <= vertex; i++)
 cout << i << "\t";
cout << endl;
cout << "---------------------------------------------" << endl;
  for(i = 1; i <= vertex; i++){
  cout << i << " |\t";
  for(j = 1; j <= vertex; j++)
  cout << G[i][j] <<"\t";
 cout << endl;
  }
  cout << "---------------------------------------------" << endl;
}


void add_egde(int* arr, int a, int b)
{
int i;
for (i=0; i<N+1; i++)
 {
if (((edges_arr[0][i]==b) && (edges_arr[1][i]==a)) || (edges_arr[0][i]==a) 
&& (edges_arr[1][i]==b) )
 arr[i]=1;
}
}
//********************************
//
// main
//
//**********************************
int main() {


ifstream fin("graph.txt");
 int vertex_left = 0;
int vertex_right = 0;
 int edges = 0;
int vertices = 0;
 int i,j,k,p;
int node;
 int counter =0;
stack<int> stacky;
edge_ptr=0;


fin >> vertices; // number of vertices
fin >> edges; // number of edges

 // generate graph matrix from the edge list
i = 1;
  while(fin >> vertex_left && fin >> vertex_right && i <= edges){
  G[vertex_left][vertex_right] = 1;
  G[vertex_right][vertex_left] = 1;
  edges_arr[0][edge_ptr]=vertex_left;
  edges_arr[1][edge_ptr]=vertex_right;
  edge_ptr++;
  i++;
  }
  cout << i << " edges are found." << endl;

  cout << "The graph matrix:" << endl;
  print(G,vertices); //print graph matrix

//  for (i=0; i<N+1; i++)
 //  cout << edges_arr[0][i] << " " << edges_arr[1][i] << endl;

  
  for (j=1; j<=vertices; j++)
  {
  
  if (visited[j]==0)
  {
  for (p=0; p<N+1; p++)
  lenght[p]=father[p]=visited[p]=0;
head[j]=1;
  lenght[j]=1;
 // cout << "one compenent" << endl;
  stacky.push(j);
  while(stacky.empty()==false)
  {
  node = stacky.top();
  stacky.pop();
  visited[node]=1;
  head[node]=1;
  //cout << node << endl;
  for (i=1; i<=vertices; i++)
  {
  if (G[node][i]==1)
  {
  if (visited[i]==0)
  {
  father[i]=node;
  lenght[i]=lenght[node]+1;
  stacky.push(i);
  }
  if (visited[i]==2)
  {
  // see if not father
  if (father[node]!=i)
  {
  //cout << "cycle: "  << i;
  k=node;
  counter =0;
  while  ((k!=0)&&(k!=father[i]))
  {
  counter++;
  //cout << " --> "<< k ;
  k=father[k];
  }
  if (counter%3!=0)
  {
  int* a = new int[N+1];
  for (int i=0; i<N+1; i++)
  a[i]=0;
  k=node;
  add_egde(a,i,k);
  cout << endl << i << " ";
  while  ((k!=0)&&(k!=father[i]))
  {
  cout << k  << " ";
  k=father[k];
  add_egde(a,k,father[k]);
  }
  cout << endl;
  for (int i=0; i<edges; i++)
  cout << a[i] << " ";
  cout << endl;
  }
   //cout << endl <<  "counter: " << counter << " " << 
lenght[node]-lenght[i]+1 <<endl;
  }
  }

  }

  }
  visited[node]=2;
  }
  }
  }
  
  while(1);
return 0;
}