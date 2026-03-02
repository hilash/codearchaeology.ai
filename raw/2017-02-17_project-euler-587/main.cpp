//
//  main.cpp
//  ProjcetEuler587
//
//  Created by Hila Shmuel on 2/17/17.
//  Copyright © 2017 Hila Shmuel. All rights reserved.
//

#include <iostream>
#include <cmath>

#define R 1.0

using namespace std;


double RR = R*R;

inline double line_circle_cross_point(double n)
{
    
    // (1+1/n^2) * x^2 + -(2R*(1+1/n)) * x + (R^2) = 0
    double nn = n*n;
    
    double A = nn + n;
    double B = sqrt(2)*pow(n,1.5);
    double C = nn + 1;
    
    double x1 = R*(A+B)/C;
    double x2 = R*(A-B)/C;
    
    double minx = min(x1,x2);
    if (minx >=0){
        return minx;
    }
    else{
        return max(x1,x2);
    }
}


inline double integral(double x)
{
    double sqrtx = sqrt(x);
    double sqrt2Rx = sqrt(2*R-x);
    double A =sqrtx* sqrt2Rx;
    
    return 0.5 * (2*RR*(M_PI_2 - atan(sqrt(2*R/x - 1))) + A*(x-R));
}


inline double Area(double a, double b)
{
    return R*(b-a) - (integral(b)-integral(a));
}

inline double lines_Area(double n)
{
    double p = line_circle_cross_point(n);
    return p*p/(2*n) + Area(p,R);
}

/*
 
 line equation: y = x/n
 cicrcle equation in first quarter: y = R - sqrt(2XR-x^2)
 

 */


int main() {

    double total_area = (4.0 - M_PI)*RR/4.0;
    cout << "True Area: " << total_area<< endl;
    cout << "Area: " << Area(0, R) << endl;
    cout << "diff: " <<total_area - Area(0, R)  << endl;
    
    int n = 1;
    double prec = 100;
    while (prec > 0.1)
    {
        double line_area = lines_Area(double(n));
        prec = line_area/total_area * 100.0;
        
        cout << "n: " << n << endl;
        cout << "lines_Area: " << line_area << endl;
        cout << "lines_Area/total: " << line_area/total_area * 100.0 << endl;
        
        n++;
    }

    return 0;
}

