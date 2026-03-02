#include <iostream>
#include <bitset>
#include <vector>
#include <deque>
#include <set>
using namespace std;

class factor {
public:
int x;
int pow;
};

#define N 1000000
#define M 4

int main()
{
bitset<N> sieve;
vector<int> primes;
 // generate prime numbers
primes.push_back(2);
for (int i = 3; i<N; i+=2){
if (sieve[i]==0){ // that's a prime
primes.push_back(i);
for (int j = i*3; j < N; j+=i){
sieve[j] = 1;
}
}
}

vector<factor> V(M);
deque<factor> D;
int in_a_row = 0;
for (int n = 1; n < N; n++)
{
int number_of_factors = 0;
int pi = 0;
int x = n;
while (x > 1)
{
int pow = 0;
while (x % primes[pi] == 0)
{
x /= primes[pi];
pow++;
}
if (pow > 0)
{
V[number_of_factors].pow = pow;
V[number_of_factors].x = primes[pi];
number_of_factors++;
}
pi++;

if (( number_of_factors == M) && (x > 1))
{
number_of_factors++;
break;
}
}

if (number_of_factors == M)
{
in_a_row++;
for (int k = 0; k<M; k++)
{
D.push_front(V[k]);
}
if (in_a_row == M)
{
set<vector<int>> S;
for (deque<factor>::iterator it = D.begin(); it!=D.end(); it++)
{
vector<int> vvv(2);
vvv[0] = it->pow;
vvv[1] = it->x;
S.insert(vvv);
}
if (S.size() == M*M)
{
cout << n - M + 1 << endl;
break;
}
}
}
else 
{
D.clear();
in_a_row = max (0,in_a_row - 1);
}
}

return 0;
}