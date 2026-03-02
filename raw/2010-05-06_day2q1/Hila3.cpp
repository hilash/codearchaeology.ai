#include <iostream>
#include <map>

using namespace std;

typedef unsigned long long ULL;

ULL base(char* number)
{
	ULL x=0;
	ULL base = 3;
	char *p = number;
	while ((*(p++))!='\0');
	p-=2;

	x+= *p-48;
	p--;
	while (p>=number)
	{
		x+= ((*p-48)*base) % 1048576;
		base *=3;
		p--;
	}

	return x;
}

void main()
{
	//map<ULL,bool> x;
	//ULL counter = 2;


	//x[0] = true;
	//x[1] = true;


	//for (ULL i=2; i<20000; i++)
	//{
	//	bool flag = true;
	//	for (map<ULL,bool>::iterator it=x.begin(); it!=x.end(); it++)
	//	{
	//		ULL tmp;
	//		if ((i + it->first)%2==0)
	//			tmp = (i + it->first)/2;
	//		else tmp = -1;
	//		if (x.count(tmp)==1)
	//		{
	//			flag = false;
	//			break;
	//		}
	//	}
	//	if (flag)
	//	{
	//		x[i] = true;
	//		ULL index = counter++;
	//		char number[100];

	//		itoa(index,number,2);
	//		cout << index << " " << base(number) << " " << i << endl;
	//	}
	//}

	//////////////////////////

	ULL index;
	char number[100];
	cin >> index;

	index--;
	itoa(index,number,2);
	cout << base(number) << endl;
}