#include <iostream>
#include <vector>
using namespace std;

#define N 500

int main()
{
    vector<bool>    sieve(N);
    vector<int>        prime;
    
    sieve[0] = sieve[1] = true; 
    for (int p = 2; p < N; p++){
        if (sieve[p] == false){
            prime.push_back(p);
            for (int i = 2*p; i < N; i+=p){
                sieve[i] = true;
            }
        }
    }

    // ways[n][k] = number of ways to write n with max number prime[k];
    int ways[N][N];

    memset(ways,0,N*N*sizeof(int));


    for (vector<int>::iterator it = prime.begin(); it!= prime.end(); it++)
    {
        int p = *it;
        ways[p][p] = 1;
    }

    sieve.clear();

    for (int n = 1; n < N; n++){
        int S = 0;
        for (vector<int>::iterator it = prime.begin(); it!= prime.end() && 
(*it < n); it++){
            int p = *it;
            int np = n - p;
            int sum = 0;
            for (int k = 0; k <= p; k++)
            {
                sum  += ways[np][k];
            }
            ways[n][p] = sum;
            S += sum;
        }
        cout << n << " " << S << endl;
    }

    return 0;
}