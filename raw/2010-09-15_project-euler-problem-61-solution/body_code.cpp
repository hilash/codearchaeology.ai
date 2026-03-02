#include <iostream>
#include <set>
#include <map>
#include <vector>
#include <algorithm>
#include <string>
using namespace std;


vector<map<char,set<char>>> MSB(9);
bool Taken[9]; // if Taken[x] == 1, we had a x-poly number
char Number[10];
int Number_K[10];
int bbb;

void set_parm()
{
for (int k = 3; k<=8; ++k)
{
bool flag = true;
int p,n = 1;
while (flag)
{
switch (k)
{
case 3:
p = ( (n*(n+1)) >>1 );
break;
case 4:
p = n*n;
break;
case 5:
p = ( (n*(3*n-1)) >>1 );
break;
case 6:
p = (n*(2*n-1));
break;
case 7:
p = ( (n*(5*n-3)) >>1 );
break;
case 8:
p = (n*(3*n-2));
break;
default:
break;
}

if (p > 9999)
{
flag = false;
}
else if ( p >= 1000)
{
// we have the number ABCD, so MSB[k][CD] = AB;
MSB[k][p%100].insert(p/100);
}
++n;
}
}
}

void search(int CD, int counter)
{
/*we have LSB of number, CD.
lets search for a matching AB, such that ABCD is a valid number under the 
conditions:
if ABCD is an x-number, and Taken[x]==1, return.


else, set Taken[x] == 1; counter ++;
if counter == 6, printf the number;
*/

for (int k = 3; k<8; ++k)
{
if (!Taken[k])
{
for (set<char>::iterator itAB = MSB[k][CD].begin(); itAB != 
MSB[k][CD].end(); itAB++)
{
Taken[k] = true;
Number[counter] = *itAB;
Number_K[counter] = k;
if (counter == 5)
{
for (int k2 = 3; k2<8; ++k2)
{
if (!Taken[k2])
{
Number_K[counter+1] = k2;
for (set<char>::iterator it2 = MSB[k2][Number[counter]].begin();  it2 != 
MSB[k2][Number[counter]].end(); it2++)
{
if (*it2==Number[0])
{
Number[counter+1] = Number[0];
int sum = 0;
for (int i = 0; i<=5; i++)
{
cout << (int)Number[i] << endl;
sum += 100*Number[i+1] + Number[i];
}
cout << "count: " << bbb << endl;
cout << "sum: " << sum << endl;
bbb++;
}
}
}
}
}
search(*itAB,counter+1);
Taken[k] = false;
}
}
}
}

void start_search()
{
Taken[8] = true;
for (map<char,set<char>>::iterator itCD = MSB[8].begin(); 
itCD!=MSB[8].end(); itCD++)
{
Number[0] = itCD->first;
for (set<char>::iterator itAB = itCD->second.begin(); itAB != 
itCD->second.end(); itAB++)
{
Number[1] = *itAB;
search(*itAB,2);
}
}
}


int main()
{
 // we have the number ABCD, so MSB8[CD] = AB;
bbb = 0;
set_parm();
start_search();

return 0;
}