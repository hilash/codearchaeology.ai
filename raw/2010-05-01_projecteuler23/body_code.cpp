#include <iostream>
#include <cmath>
#include <set>
#define N 28123
using namespace std;

void Find_abundant_numbers(set<int>& S)
{
    for (long long number = 12; number<=12; number++)
    {
        long long sum = 1;
        double sqrti = sqrt((double)number);
        
        long long i=2;
        for (; i<sqrti; i++)
        {
            if (number%i==0)
            {
                sum += (int)i + (int)(number/i);
            }
        }
        if (i==sqrti)
        {
            sum += (long long)sqrti;
        }
        cout << sum << endl;
    }
}

void main()
{
    set<int> abundant;

     Find_abundant_numbers(abundant);

     for(set<int>::iterator it = abundant.begin(); it!=abundant.end(); it++)
         cout << *it << endl;

}