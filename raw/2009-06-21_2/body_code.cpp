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
int i,j,k;
int node;
int counter =0;
stack<int> stacky;       


fin >> vertices; // number of vertices
fin >> edges; // number of edges

 // generate graph matrix from the edge list
i = 1;
  while(fin >> vertex_left && fin >> vertex_right && i <= edges){
  G[vertex_left][vertex_right] = 1;
  G[vertex_right][vertex_left] = 1;
  i++;
  }
  cout << i << " edges are found." << endl;

  cout << "The graph matrix:" << endl;
  print(G,vertices); //print graph matrix

  
  for (j=1; j<=vertices; j++)
  {
  if (visited[j]==0)
  {
  lenght[j]=1;
  cout << "one compenent" << endl;
  stacky.push(j);
  while(stacky.empty()==false)
  {
  node = stacky.top();
  stacky.pop();
  visited[node]=1;
  cout << node << endl;
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
  cout << "cycle: "  << i;
  k=node;
  counter =0;
  while  ((k!=0)&&(k!=father[i]))
  {
  counter++;
  cout << " --> "<< k ;
  k=father[k];
  }
  if (counter%3!=0)
  {
   cout << endl <<  "DONE!!" ;
  }
   cout << endl <<  "counter: " << counter << " " << 
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