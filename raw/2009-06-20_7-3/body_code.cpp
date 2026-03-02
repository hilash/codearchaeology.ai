namespace std; 

int
main() 

{

//m_max no needed 

int n,m,i,j,n_sum,m_sum,n_max,m_max,m_height,m_length,total,row; 

n_sum=m_sum=n_max=m_max=total=0;

// Get N 

cin >> n;

int *n_height = new int[n]; 

int *n_length = new int[n]; 

for (i=0; i<n; i++) 

{

cin >> n_height[i] >> n_length[i];

n_max=MAX(n_max,n_height[i]);

n_sum+=n_height[i]*n_length[i];

}

//cout << "N:" << n << endl; 

//for (i=0; i<n; i++) 

// cout << n_height[i] << " " << n_length[i] << endl; 

//// Get M with array 

//cin >> m; 

//int *m_height = new int[m]; 

//int *m_length = new int[m]; 

//for (i=0; i<m; i++) 

// cin >> m_height[i] >> m_length[i]; 

//cout << "M:" << endl; 

//for (i=0; i<m; i++) 

// cout << m_height[i] << " " << m_length[i] << endl; 

// Get M 

cin >> m;

for (i=0; i<m; i++) 

{

//answer to question 1 

cin >> m_height >> m_length;

m_max=MAX(m_max,m_height);

m_sum+=m_height*m_length;

//answer to question 2 

//we look at the current (row) block with m_height. 

//lets see the max breaks feet in it. go through n array 

row = 0;

for (j=0; j<n; j++) 

{

row+=n_length[j]*MIN(m_height,n_height[j]);

}

total+=row*m_length;

}

cout << 
"m_max: " << m_max << " n_max: " << n_max << endl; 

cout << 
"m_sum: " << m_sum << " n_sum: " << n_sum << endl; 

cout << 
"output: " << n_sum+m_sum-n_max << endl; 

cout << 
"total: " << total << endl; 

 

while(1); 

return 0; 

}