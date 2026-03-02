#include <iostream>
#include <algorithm>

#define N 100
#define Tm 10000
#define D 1
#define L 1000
#define Ep 0.25

/*
%Set Parameters
L = 1000;
N = [820 825 830 835 840 845 850 855 860 865 870 875 880];
D = 1;
Tm = 10000;
Ep = [0.05 0.15 0.25];
Realizations = 150;
*/

using namespace std;

double fRand(double fMin, double fMax)
{
    double f = (double)rand() / RAND_MAX;
    return fMin + f * (fMax - fMin);
}


int main()
{
   cout << "Hello World" << endl;

   //for (int i = 0; i < N; i++)
   {
       vector<double>   X(N,0);
       vector<bool>     XB(N,0);
       vector<int>      NAP(Tm,0);

       // a loop that initializing rand
       for (vector<double>::iterator it = X.begin() ; it != X.end(); ++it)
       {
            *it = fRand(0, L);
       }

       // sort
       sort(X.begin(), X.end());

        // a loop that prints
        for (vector<double>::iterator it = X.begin() ; it != X.end(); ++it)
       {
            cout << *it << endl;
       }

       cout << "PLAYYYYYYYYYYYYY" << endl;

       // loop over Tm
       for (int tm = 1; tm <= Tm; tm++)
       {
            //  Decide Which Particles are ACTIVE

            // Left Boundary
            if ( ((L - (X[N-1]-X[0])) < D) || ((X[1]-X[0]) < D) )
            {
                XB[0] = true;
            }
            else
            {
                XB[0] = false;
            }

            // Right Boundary
            if ( ((L - (X[N-1]-X[0])) < D) || ((X[N-1]-X[N-2]) < D) )
            {
                XB[N-1] = true;
            }
            else
            {
                XB[N-1] = false;
            }

            for (int j = 2; j <= N-1; j++)
            {
                 if (((X[j-1]-X[j-2]) < D) || ((X[j]-X[j-1]) < D))
                {
                    XB[j-1] = true;
                }
                else
                {
                    XB[j-1] = false;
                }
            }


///////////////////////////////////////////////////////////////////
            ///  Make a DISPLACEMENT for ACTIVE particles

///////////////////////////////////////////////////////////////////

            // Left Boundary
            if (XB[0] == true)
            {
                X[0] = X[0] - Ep + 2*Ep*fRand(0, 1);

                //Make Sure array is ORGANIZIED
                X[0] = abs(X[0]);

                if (X[0] > X[1])
                {
                    double tmp = X[0];
                    X[0] = X[1];
                    X[1] = tmp;
                }
            }

             // Right Boundary
            if (XB[N-1] == true)
            {
                X[N-1] = X[N-1] - Ep + 2*Ep*fRand(0, 1);

                //Make Sure array is ORGANIZIED // TODO - RON ALGO
                if (X[N-1] > L)
                {
                    X[N-1] = 2.0 * L - X[N-1];
                }

                if (X[N-1] < X[N-2]){
                    double tmp = X[N-1];
                    X[N-1] = X[N-2];
                    X[N-2] = tmp;
                }
            }

            // Inside

            for (int j = 1; j <= N-2; j++)
            {
                if (XB[j] == true)
                {
                    X[j] = X[j] - Ep + 2*Ep*fRand(0, 1);

                    // %Make Sure array is ORGANIZIED
                    if (X[j] > X[j+1])
                    {
                        double tmp = X[j];
                        X[j] = X[j+1];
                        X[j+1] = tmp;
                    }

                    if (X[j] < X[j-1])
                    {
                        double tmp = X[j];
                        X[j] = X[j-1];
                        X[j-1] = tmp;
                    }
                }
            }
       }

       // a loop that prints
        for (vector<double>::iterator it = X.begin() ; it != X.end(); ++it)
       {
            cout << *it << endl;
       }

   }

   return 0;
}