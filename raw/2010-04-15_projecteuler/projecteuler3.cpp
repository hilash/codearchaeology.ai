/**********************************************************************
   
   What is the largest prime factor of the number 600851475143?

   we have a list of prime numbers;
   lets take x = 600851475143;

   we go over the list, p=1 to sqrt(6008514751430)
		if  x mod p = 0
		  then   we know that x = p*something so will do
			x = something

		  now will find what are something factors.
	that  proccess can give us the factors of x.

	// we go over the list, p=1 to x
	// since if x is a prime we need to find it on the final level

 **********************************************************************/

#include <iostream>
#include <fstream>
#include <cmath>

using namespace std;

int main()
{
	__int64 x=6008514751430;
	__int64 p;
	__int64 max_factor=0;
	//__int64 sqrtx=775146; // 6008514751430^0.5
	ifstream file("primes1.txt");

	file >> p;
	while (p<=x)
	{
		if (x%p==0) // then x = p*somthing
		{
			max_factor = max(max_factor,p);
			x = x/p;
			//sqrtx = x/2; //problem with implementing  qrt on __int64
			file.seekg (0, ios::beg);
			cout << max_factor << endl;
		}
		file >> p;
	}

	file.close();
	return 0;
}