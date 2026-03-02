//
//  main.cpp
//  ProjectEuler226
//
//  Created by Hila Shmuel on 2/17/17.
//  Copyright © 2017 Hila Shmuel. All rights reserved.
//

#include <iostream>
#include <iomanip>
#include <cmath>
using namespace std;


#define MM 10000000000
#define NN 60

double powers[NN] = {};

double M = 100000000;
double  N = 20;
double DX = 1.0 / M;

long double x = 0;

double s(double a)
{
    return min(ceil(a)-a, a-floor(a));
}

void calc_powers()
{
    for (int n = 0; n < NN; n++)
        powers[n] = pow(2,n);
}

double calc_y(double x)
{
    double y = 0;
    for (int n = 0; n < N; n++)
    {
        y += s(powers[n]*x)/powers[n];
    }
    return y;
}

double foo()
{
    double Area = 0;
    
    for (unsigned long long int  m=0; m < M; m++)
    {
        Area += calc_y(m*DX);
    }
    return Area*DX;
}

double circle()
{
    
    double Area = 0;
    
    double x = 0;
    for (unsigned long long int  m=0; m < M; m++)
    {
        double curve_height = calc_y(m*DX);
        double circle_height = 0.5 - sqrt(x/2 -x*x);
        double diff = curve_height - circle_height;
        if (diff > 0)
            Area += diff;
        x+=DX;
        if (x > 0.5) break;
    }
    return Area*DX;
    
    
    return 0;
}

int main(int argc, const char * argv[]) {
    cout << "lets go" << endl;
    calc_powers();
    


    
    for (M = 10000000; true; M = M *5)
    {
        DX = 1.0 / M;
        //for (N = 20; N < 40; N+= 10)
        {
            N = 45;
            cout << "m,n: " << M << ", " << N << "  circle ";
            cout << setprecision (9) << circle() << endl;
        }
    }

    return 0;
}
