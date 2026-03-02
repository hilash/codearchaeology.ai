#include <iostream>
#include <cmath>
#include <list>
#include <algorithm>
#define MIN(a,b) ((a>=b)?b:a)
#define MAX(a,b) ((a<=b)?b:a)

using namespace std;
void main()
{
	long number[3];
	long first[3];
	long last[3];
	list<long> show[3]; // we can make the lists little by adding 'prevx'
	int n;

	for (int j=0; j<3; j++)
		number[j]=first[j]=last[j]=0;

	cin >> n;

	for (long i=1; i<=n; i++)
	{
		int x; 
		cin >> x;
		cout << endl;
		
		// insert x to array
		if ( (number[0]==0) || (number[0]-x>2) ||
			( (number[0]!=0) && (number[1]==0) && (number[2]==0) && (x-number[0]>2) ) ||
			( (number[0]!=0) && (number[1]!=0) && (number[2]==0) && (x-number[1]>1) ) )
			// then the array is empty, at first only. or if 
			// the number is bigger2 or smaller2 then the limits 
		{
			number[0] = x;
			first[0] = last[0]=i;
			number[1] = first[1] = last[1] = 0;
			number[2] = first[2] = last[2] = 0;

			show[0].clear();show[1].clear();show[2].clear();
			show[0].push_back(i);
		}
		// case when we need to enter a new number that can be excist in the array
		// and bigger then the smallest number
		else if ( (0 <=x - number[0]) && (x - number[0] <=2) )
		{
			// just updat last appearnce
			if (x - number[0] ==0)
			{
				last[0]=i;
				show[0].push_back(i);
			}
			else if (x - number[1] ==0)
			{
				last[1]=i;
				show[1].push_back(i);
			}
			else if (x - number[2] ==0)
			{
				last[2]=i;
				show[2].push_back(i);
			}

			//if the number doen't exist
			else if (x - number[0] == 1)
			{
				number[1] = x;
				first [1] = last[1]=i;
				show[1].clear();
				show[1].push_back(i);
			}
			else if (x - number[0] == 2)
			{
				number[2] = x;
				first [2] = last[2]=i;
				show[2].clear();
				show[2].push_back(i);
			}
		}
		else
		{
			// we need to insert a new number that will delete old numbers
			if (x == number[0] - 1) // we need to move out number[2]
									// and upadate the first and last indexes,
									// since they can be between the deleted number
			{
				//insert new number from the left, delete number[2]
				long l = last[2]; // we need to find the shows of number[0] && number[1] after that
				long p0=0, p1=0;

				list<long>::iterator it;
				for (it = show[0].begin(); it!=show[0].end(); it++)
				{
					if (*it>l)
						break;
				}
				if (it!=show[0].end())
					p0 = *it;

				for (it = show[1].begin(); it!=show[1].end(); it++)
				{
					if (*it>l)
						break;
				}
				if (it!=show[1].end())
					p1 = *it;

				if ( (p1 > 0) && (p0 > 0))
				{
					show[2] = show[1];
					show[1] = show[0];
					show[0].clear();
					show[0].push_back(i);

					first[2] = p1;
					last[2] = last[1];

					first[1] = p0;
					last[1] = last[0];
					first[0]=last[0] = i;

					number[2] = number[1];
					number[1] = number[0];
					number[0] = x;
				}
				else if ( (p1 == 0) && (p0 == 0))
				{
					number[0] = x;
					first[0] = last[0]=i;
					number[1] = first[1] = last[1] = 0;
					number[2] = first[2] = last[2] = 0;

					show[0].clear();show[1].clear();show[2].clear();
					show[0].push_back(i);
				}
				else if (p0 > 0)
				{
					number[2] = 0;
					number[1] = number[0];
					number[0] = x;
					show[1] = show[0];
					show[2].clear();
					show[0].clear();
					show[0].push_back(i);
					first[2] = last[2] = 0;
					first[1] = p0;
					last[1] = last[0];
					last[0] = first[0] = i;
				}
				else if (p1 > 0)
				{
					number[2] = number[1];
					number[1] = 0;
					number[0] = x;
					
					show[2] = show[1];
					show[1].clear();
					show[0].clear();
					show[0].push_back(i);

					first[2] = p1;
					last[2] = last[1];
					first[1] = last[1] = 0;
					first[0] = last[0] = i;	
				}
			}
			else if (x == number[0] - 2) 
			{
				if (last[0] == i-1) // else we can't do it
				{
					long l = MAX(last[2],last[1]); // we need to find the shows of number[0] && number[1] after that
					long p = 0;
					
					list<long>::iterator it;
					for (it = show[0].begin(); it!=show[0].end(); it++)
					{
						if (*it>l)
							break;
					}
					if (it!=show[0].end())
						p = *it;

					show[2] = show[0];
					show[1].clear();
					show[0].clear();
					show[0].push_back(i);

					first[2] = p;
					last[2] = i-1;
					first[1] = last[1] = 0;
					first[0] = last[0] = i;

					number[2] = number[0];
					number[1] = 0;
					number[0] = x;
				}
				else
				{
				}
			}
			else if (x == number[0] + 3)
			{
				if ( last[0]==i-1 )
				{
					// then start a new sequence
					show[0].clear();show[1].clear();show[2].clear();
					show[0].push_back(i);

					first[0] = last[0] = i;
					first[1] = last[1] = first[2] = last[2] = 0;
					
					number[0] = x;
					number[1] = number[2] = 0;
				}
				else
				{
					long l = last[0]; // we need to find the shows of number[2] && number[1] after that
					long p2=0, p1=0;

					list<long>::iterator it;
					for (it = show[1].begin(); it!=show[1].end(); it++)
					{
						if (*it>l)
							break;
					}
					if (it!=show[1].end())
						p1 = *it;

					for (it = show[2].begin(); it!=show[2].end(); it++)
					{
						if (*it>l)
							break;
					}
					if (it!=show[2].end())
						p2 = *it;

					if ( (p1 > 0) && (p2 > 0))
					{
						// shift left
						show[0] = show[1];
						show[1] = show[2];
						show[2].clear();
						show[2].push_back(i);

						number[0] = number[1];
						number[1] = number[2];
						number[2] = x;

						first[0] = p1;
						last[0] = last[1];
						first[1] = p2;
						last[1] = last[2];
						first[2]=last[2]=i;
					}
					else if ( (p1 == 0) && (p2 == 0))
					{
						// then start a new sequence
						show[0].clear();show[1].clear();show[2].clear();
						show[0].push_back(i);

						first[0] = last[0] = i;
						first[1] = last[1] = first[2] = last[2] = 0;
						
						number[0] = x;
						number[1] = number[2] = 0;
					}
					else if (p2 > 0)
					{
						number[0] = number[2];
						number[1] = x;
						number[2] = 0;
						
						show[0] = show[2];
						show[1].clear();
						show[1].push_back(i);
						show[2].clear();

						first[0] = p2;
						last[0] = last[2];
						first[1] = last[1] = i;
						first[2] = last[2] = 0;
					}
					else if (p1 > 0)
					{
						number[0] = number[1];
						number[1] = 0;
						number[2] = x;
						
						show[0] = show[1];
						show[1].clear();
						show[2].clear();
						show[2].push_back(i);

						first[0] = p1;
						last[0] = last[1];
						first[1] = first[1] = 0;
						first[2] = last[2] = i;
					}
				}
			}
		}

		// print
		cout << i << ":  " << number[0] << " " <<number[1] << " " << number[2] << endl;
		cout << "    " << first[0] << " " <<first[1] << " " << first[2] << endl;
		cout << "    " << last[0] << " " <<last[1] << " " << last[2] << endl;
		// lenght = max(last)-min(fisrt)
		cout << "    " << *max_element(last,last+3) - MIN(first[2],MIN(first[0],first[1])) << endl;
		cout << endl;
	}
}