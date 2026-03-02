namespace std; 

int
main() 

{

int n,m,i,j,k,x,y; 

 

cout << 
"The computer is player B. you are player A." << endl; 

cout << 
"enter N and M: " << endl; 

cin >> n >> m;

char **arr = new char*[n]; 

for (i=0; i<n; i++) 

{

arr[i] = 
new char[m]; 

}

for (i=0; i<n; i++) 

for (j=0; j<m; j++) 

arr[i][j]=
'-'; 

// number of steps 

for (k=0; k<n*m/2; k++) 

{

cout << 
"enter: x y " << endl; 

cin >> x >> y;

arr[x-1][y-1]=
'A'; 

//fill domino 

if ( n%2 == 0) 

{

if ( x%2 == 0 ) 

{

arr[x-2][y-1]=
'B'; 

}

else arr[x][y-1]='B'; 

}

if ( m%2 == 0) 

{

if ( y%2 == 0 ) 

{

arr[x-1][y-2]=
'B'; 

}

else arr[x-1][y]='B'; 

}

//print board 

for (i=0; i<n; i++) 

{

for (j=0; j<m; j++) 

cout << arr[i][j] << 
" "; 

cout << endl;

}

}

 

while(1); 

return 0; 

}