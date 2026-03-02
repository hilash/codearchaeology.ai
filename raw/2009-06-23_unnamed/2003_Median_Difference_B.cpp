//	
//	2003 Median Difference
//	http://www.ioinformatics.org/locations/ioi03/contest/day0/median.pdf
//	good solution
//	O(nlgn)

#include <iostream>
#include <fstream>
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
	int mini,maxi;
	double diff;
	int x,y,z;
	unsigned int* input;
	int range; // P = maxi-mini+1
	
	// create an input file
	std::ofstream ofile;
    ofile.open("median.in", ios::out); // opens file as output 

	srand ( time(NULL) );
	n = rand()%20000 + 1;
	//n=20000;

    ofile << n << endl;
	for (i=0; i<n; i++)
		ofile<< rand()%250000 + 1 << endl;

    ofile.close();

	// the program
    std::ifstream ifile;
    ifile.open("median.in",ios::in); //opens file as input
	
	ifile >> n;
	input = new unsigned int[n];

	// get numbers from file
	ifile >> input[0];
	mini=maxi=input[0];
	for ( i=1; i<n; i++)
	{
		ifile >> input[i];
		mini = (mini<input[i])?mini:input[i];
		maxi = (maxi>input[i])?maxi:input[i];
	}

	range = maxi-mini+1;

	// sort array - see in what way, depends on n and range
	if (range<=n)
	{
		//do a totally sort (counting sort) O(N)
		bool *count = new bool[range];
		for(i = 0; i < range; i++)
			count[i] = 0;
		
		for(i = 0; i < n; i++)
			count[ input[i] - mini ]=true;
		
		j=k=0;
		for(i = 0; i < range; i++)
			if (count[i]==true)
				input[k++] = i+mini;
		
		delete[] count;
	}
	else
	{
		// introsort algorithm, O(NlogN)
		sort(input,input+n);
	}
		

	// for each pair of neighbors + the number on their other edge of the array = trio
	// seek for the trio thats gives the maximal absolute difference
	// between its mean and median
	for (i=0; i<n-1; i++)
	{
		if (input[i]!=input[i+1])
		{
			if (i<n/2)
			{
				median = input[i+1];
				mean = (input[i]+input[i+1]+input[n-1])/3;
				j=n-1;
			}
			else
			{
				median = input[i];
				mean = (input[i]+input[i+1]+input[0])/3;
				j=0;
			}
			diff = fabs(mean-median);
			//cout << input[i] << " " << input[i+1] << " " << input[j] << "     " << median << " " << mean << " " << diff << endl;
			if ((max<diff)&&(input[i+1]!=input[j])&&(input[j]!=input[i]))
			{
				max=diff;
				x=input[i];
				y=input[i+1];
				z=input[j];
			}
		}
	}

	// print the trio with max difference between its mean and median
	//cout << endl << endl;
	//cout << x << " " << y << " " << z << " " << max << endl;

	std::ofstream ffile;
    ffile.open("median.out", ios::out); // opens file as output 

    ffile << x << endl;
	ffile << y << endl;
	ffile << z;
    ffile.close();

	return 0;
}
