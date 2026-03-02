#include <iostream>
#include <fstream>
//#include "PLAY.H"

using namespace std;

int main()
{
	int n,i,num;
	char C;
	int left,right;
	int even_sum=0; // 0,2,4..
	int odd_sum=0;  // 1,3,5..
	bool odd;

	//opens file as input
	std::ifstream ifile;
    ifile.open("INPUT.TXT",ios::in); 

	ifile >> n;

	left = 0;
	right = n-1;

	for (i=0; i<n; i++)
	{
		ifile >> num;
		if (i%2==0)
			even_sum+=num;
		else
			odd_sum+=num;
	}

	if (even_sum<odd_sum)
		odd = true;
	else odd = false;

	StartGame();

	for (i=n/2; i>0; i--)
	{
		if (odd==true)		//chose an odd location
		{
			if (left%2==1)
			{
				MyMove('L');
				left++;
			}
			else if (right%2==1)
			{
				MyMove('R');
				right--;
			}
		}
		else if (odd==false)		//chose an even location
		{
			if (left%2==0)
			{
				MyMove('L');
				left++;
			}
			else if (right%2==0)
			{
				MyMove('R');
				right--;
			}
		}
		YourMove(&C);
		if (C=='L')
			left++;
		else if (C=='R')
			right++;

	}
	
	//opens file as output 
	std::ofstream ffile;
    ffile.open("OUTPUT.TXT", ios::out);

	if (odd==true)
		ffile << odd_sum << " " << even_sum << endl;
	else
		ffile << even_sum << " " << odd_sum << endl;

    ffile.close();

	return 0;
}
