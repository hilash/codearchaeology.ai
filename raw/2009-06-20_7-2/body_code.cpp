namespace std; 

int
main() 

{

int n,i,j,k,x,y,li,ji,p,q; 

bool flag=false; 

bool flag1=true; 

bool flag2=true; 

 

cout << 
"The computer is player A. you are player B." << endl; 

cout << 
"enter N" << endl; 

cin >> n;

char **arr = new char*[n]; 

for (i=0; i<n; i++) 

{

arr[i] = 
new char[n]; 

}

for (i=0; i<n; i++) 

for (j=0; j<n; j++) 

arr[i][j]=
'-'; 

arr[0][0]=
'A'; 

//next location 

li=0;

ji=1;

p=n-2;

q=0;

// number of steps 

for (k=0; k<n*n/2; k++) 

{

//print board 

for (i=0; i<n; i++) 

{

for (j=0; j<n; j++) 

cout << arr[i][j] << 
" "; 

cout << endl;

}

cout << endl << 
"enter: x y " << endl; 

cin >> x >> y;

arr[x-1][y-1]=
'B'; 

if (li<n-2) 

{

//where to put A 

while (arr[li][ji]!='-') 

{

if (ji<n) 

ji++;

else if (ji==n) 

{

li++;

ji=0;

}

}

if (li<n-2) 

{

arr[li][ji]=
'A'; 

ji++;

}

}

if ((arr[n-3][n-1]!='-') && (flag==false)) 

{

flag1=flag2=
true; 

for (p=n-2; (p<n) && (flag1==true); p++) 

{

for (q=0; (q<n) && (flag2==true); q++) 

{

if ( (q==0) && (arr[p][0]=='-') && (arr[p][1]=='B') ) 

{

arr[p][0]=
'A'; 

flag1=flag2=
false; 

}

else if ( (q==n-1) && (arr[p][n-2]=='B') && (arr[p][n-1]=='-') ) 

{

arr[p][n-1]=
'A'; 

flag1=flag2=
false; 

}

else if ( (0<q) && (q<n-1) && (arr[p][q-1]=='B') && (arr[p][q]=='-') && 
(arr[p][q+1]=='B') ) 

{

arr[p][q]=
'A'; 

flag1=flag2=
false; 

}

}

flag=
true; 

li=n-2;

ji=0;

}

}

if ((arr[n-3][n-1]!='-') && (flag==true)) 

{

while ((li<n) &&(arr[li][ji]!='-')) 

{

if (ji<n) 

ji++;

else if (ji==n) 

{

li++;

ji=0;

}

}

if (li<n) 

{

arr[li][ji]=
'A'; 

ji++;

}

}

if (li==n) 

cout << endl << 
"GAME OVER!"; 

}

cout << endl << 
"enter: li ji " << li << " " <<ji <<endl; 

while(1); 

return 0; 

}