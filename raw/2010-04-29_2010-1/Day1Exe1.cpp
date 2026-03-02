#include <iostream>
#include <algorithm>
#include <vector>
#include <set>

using namespace std;

class person
{
public:
	int name;
	int time;
	bool start;
	int job;
	
	person(int n,int t,bool s, int j){
		name = n; time = t; start = s; job = j;
	}

	bool operator <(const person& b) const
	{
		return (this->time<b.time);
	}
};

vector<person> P;
vector<person> times;
set<int> D;
bool sol = false;

int foo( )
{
	return 2;
}

void main()
{
	int n;

	// input
	cin >> n;
	for (int i=1; i<=n; i++)
	{
		int tmp;
		cin >> tmp;
		P.push_back(person(i,tmp,true,0));
		times.push_back(person(i,tmp,true,0));
		cin >> tmp;
		P.push_back(person(i,tmp,false,0));
		times.push_back(person(i,tmp,false,0));
	}
	
	//for (vector<person>::iterator it = P.begin(); it!=P.end(); it++)
	//{
	//	cout << it->time << " ";
	//}
	//cout << endl;

	sort(times.begin(),times.end());

	// first pass - see who is a a mentor
	set<int> S;
	for (vector<person>::iterator it = times.begin(); it!=times.end(); it++)
	{
		if (S.size()==1)
		{
			set<int>::iterator k = S.begin();
			P[(*k-1)*2].job = 4;
			P[(*k-1)*2+1].job = 4;
		}
		if (it->start==true)
			S.insert(it->name);
		else S.erase(it->name);

		if (S.size()==1)
		{
			set<int>::iterator k = S.begin();
			P[(*k-1)*2].job = 4;
			P[(*k-1)*2+1].job = 4;
		}
	}
	
}