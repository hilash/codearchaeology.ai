#include <iostream>
#include <vector>
#include <list>
#include <ctime>
using namespace std;

#define N 10000000

bool is_parm(int a, int b)
{
    static char digits[10] = {0};

    memset (digits,0,10);
    
    do {
        digits[a%10]++;
        a /= 10;
    }
    while (a != 0);

    do {
        digits[b%10]--;
        b /= 10;
    }
    while (b != 0);

    for (int i = 0; i < 10; i++)
    {
        if (digits[i] != 0 ){
            return false;
        }
    }
    return true;
}

int main()
{
    vector<double>  phi(N);
    time_t start,end;
    double dif;

    time (&start);

    for (int i = 1; i<N; i+=2){
        phi[i] = i;
    }

    // anything divides by two
    phi[2] = 1;
    for (int i = 4; i<N; i+=2){
        phi[i] = i / 2;
    }

    for (int p = 3; p < N; p+=2){
        if (phi[p] == p) // a prime!
        {
            phi[p] -= 1;
            for (int i = 2*p; i < N; i+=p) // mark all odd not prime numbers 
(no need to mark even numbers). 
            {
                phi[i] = (phi[i]*(double)(p-1))/(double)p;
            }
        }
    }

    time (&end);
    dif = difftime (end,start);
    printf ("find phi: %.2lf seconds\n", dif );

    time (&start);

    double ratio = 87109.0/79180.0;//n/φ(n)

    for (int i = 2; i<N; i++){
        if ((double)i/(double)phi[i] <= ratio){
            // check if perm.
            if (is_parm(i,phi[i])==true)
            {
                ratio = (double)i/(double)phi[i];
                cout << i << "/" << phi[i] << " = " << ratio << endl;
            }
        }
    }

    time (&end);
    dif = difftime (end,start);
    printf ("find reault: %.2lf seconds\n", dif );

    return 0;
}


 for(i = 2; i <= MAX; ++i) 
        for(j = 2; i * j <= MAX; ++j) 
            phi[i * j] -= phi[i];
 אני עובר על הכפולות
 ומחסיר
Hila says:
 וואי זה באמת יוצא מהר אז. 
koolk says:
 ובPE מישהו עשה אפילו יותר מהיר:
     for( i = 2 ; i <= n ; ++i )
        if( p[i] == i - 1 ) for( j = n / i ; j >= 2 ; --j ) p[i * j] -= 
p[j];