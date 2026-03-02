// constructing bitsets
#include <iostream>       // std::cout
#include <string>         // std::string
#include <bitset>         // std::bitset

#define N 1000000000000 // 10^12

using namespace std;

void boo();
void boo2();
void boo3();

int main ()
{
    //boo();
    //boo2();
    boo3();
    return 0;
}

void boo()
{
  bitset<N+1> foo;
  unsigned long long int sum = 0;

  //cout << "foo: " << foo << '\n';
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
        //cout << i << ": " << number_of_smallets << '\n';
        sum += number_of_smallets * i;
      }

  }

  cout << "sum: " << sum << '\n';
}

void boo2()
{
    // a vesion with half size
    //0...,980,99,100
    //10
    // 2,4,6,8,10
    //foo[i] ==> foo [2i-1]

  bitset<N+1> foo;
  unsigned long long int sum = 0;

  //cout << "foo: " << foo << '\n';
  sum += (N/2)*2;
  // lets look only at odd numbers
  for (size_t i=3; i<foo.size(); i+=2) {
      if (foo[i]==0) {
          // it's a prime
          int number_of_smallets = 0;
        for (size_t j=i; j<foo.size(); j+=i){
            if ((foo[j]==0) && (j & 1)){
                ++number_of_smallets;
                //cout << "     " << i << ": " << j << '\n';
            }
            foo[j]=1;

        }
        //cout << i << ": " << number_of_smallets << '\n';
        sum += number_of_smallets * i;
      }

  }

  cout << "sum: " << sum << '\n';
}










void boo3()
{
    // a vesion with half size
    //0...,980,99,100
    //10
    // 2,4,6,8,10
    //foo[i] ==> foo [2i-1]

  bitset<N/2 + 2> foo;
  unsigned long long int sum = 0;

  //cout << "foo: " << foo << '\n';

  // value:         3 5 7 9
  // its new index: 1 2 3 4
  // (value - 1)/2
  // value >> 1

  sum += (N/2)*2;
  // lets look only at odd numbers
  for (size_t i=3; i<=N; i+=2) {
      if (foo[i >> 1]==0) {
          // it's a prime
          int number_of_smallets = 0;
        for (size_t j=i; j<=N; j+=i){
            if (j & 1) {
                if (foo[j >> 1]==0){
                    ++number_of_smallets;
                    //cout << "     " << i << ": " << j << '\n';
                }
                foo[j >> 1]=1;
            }

        }
        //cout << i << ": " << number_of_smallets << '\n';
        sum += number_of_smallets * i;
      }

  }
  cout << "sum: " << sum << '\n';
}