// constructing bitsets
#include <iostream>       // std::cout
#include <string>         // std::string
#include <bitset>         // std::bitset

#define N 1000000000000 // 10^12

using namespace std;

int main ()
{
  bitset<N+1> foo;
  unsigned long long int sum = 0;

  cout << "foo: " << foo << '\n';
  for (size_t i=2; i<foo.size(); ++i) {
      if (foo[i]==0) {
          // it's a prime
          int number_of_smallets = 0;
        for (size_t j=i; j<foo.size(); j+=i){
            if (foo[j]==0){
                ++number_of_smallets;
                //cout << "     " << i << ": " << j << '\n';
            }
            foo[j]=1;
            
        }
        cout << i << ": " << number_of_smallets << '\n';
        sum += number_of_smallets * i;
      }
   
  }
  
  cout << "sum: " << sum << '\n';
  return 0;
}