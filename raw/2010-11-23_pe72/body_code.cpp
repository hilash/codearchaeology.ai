#include <iostream>
#include <vector>
#include <list>
#include <ctime>
using namespace std;
#define N 9

int main()
{
    double phi[N];
    
    for (int i = 0; i < N; ++i){
        phi[i] = i;
    }

    phi[1] = phi[2] = 1;
    for (int i = 4; i < N; i+=2){
        phi[i] /= 2;
    }

    for (int p = 3; p < N; p+=2){
        if (phi[p] == p) // then it's a prime!
        {
            //cout << p << endl;
            phi[p] = p - 1;
            for (int j = 2*p; j < N; j+=p){
                phi[j] *= (double)(p-1);
                phi[j] /= (double)(p);
            }
        }
    }

    unsigned long long sum = 0;
    for (int i = 2; i<N; i++){
        sum += (int)phi[i];
    }

    cout << sum << endl;
    return 0;
}