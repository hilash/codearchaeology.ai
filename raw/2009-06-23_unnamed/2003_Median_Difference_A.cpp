//	
//	2003 Median Difference
//	http://www.ioinformatics.org/locations/ioi03/contest/day0/median.pdf
//	naive solution
//	O(n^3)

#include <iostream>
#include <algorithm>
#include <math.h>
#include <time.h>

using namespace std;

int main()
{
	int n;
	int i,j,k;
	int median;
	double mean;
	double max=0;
	double diff;
	int x,y,z;
	unsigned int* input;
	
	srand ( time(NULL) );
	n=6;	//n = rand()%20000 + 1;

	input = new unsigned int[n];

	// generate array with random numbers
	for ( i=0; i<n; i++)
		input[i]=rand()%250000 + 1 ;

	// sort the array
	sort(input,input+n);

	// print sorted array
	for ( i=0; i<n; i++)
		cout << input[i] << " ";

	// for each trio, find the difference between its mean and median
	for (i=0; i<n; i++)
		for (j=i+1; j<n; j++)
			for (k=j+1; k<n; k++)
			{
				median = input[j];
				mean = (input[i]+input[j]+input[k])/3;
				diff = fabs(mean-median);
				if (max<diff)
				{
					max=diff;
					x=input[i];
					y=input[j];
					z=input[k];
				}
			}

	// print the trio with max difference between its mean and median
	cout  << endl;
	cout << x << " " << y << " " << z << endl << endl;

	return 0;
}
