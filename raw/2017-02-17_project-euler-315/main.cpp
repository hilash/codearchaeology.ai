//
//  main.cpp
//  ProjectEuler315
//
//  Created by Hila Shmuel on 2/17/17.
//  Copyright © 2017 Hila Shmuel. All rights reserved.
//
#include <iostream>
#include <bitset>
#include <vector>
#include <string>

using namespace std;

#define A 10000000
#define B 20000000

/*
       A
     _____
   F|     |B
     __G__
   E|     |C
     _____
       D
 
 */

unsigned char digits[10];
unsigned char digits_lines[10];
unsigned char digits_xor_table[10][10] = {0};
const string digit_string[10] =
//ABCDEFG
{"1111110", "0110000", "1101101","1111001","0110011", "1011011", "1011111", "1110010","1111111","1111011"};


vector<uint> primes;

void primes_sieve()
{
    bitset<B+1> sieve;
    
    for (uint p=3; p<=B; p+=2)
    {
        if (sieve[p]==0)
        {
            if (p >= A)
            {
                primes.push_back(p);
            }
            for (uint j = p; j <=B; j+=2*p)
            {
                sieve[j]=1;
            }
        }
    }
}

void handle_digits()
{
    for (int i = 0; i<=9; i++)
    {
        bitset<7> s(digit_string[i]);
        digits[i]=s.to_ulong();
        digits_lines[i]=s.count();
        cout << "digit: " <<digit_string[i]<< " -> " << (unsigned long) digits[i] << " lines: " <<(unsigned long) digits_lines[i] << endl;
    }
    
    for (int i = 0; i<=9; i++){
        for (int j = i; j<=9; j++){
            bitset<16> b(digits[i]^digits[j]);
            digits_xor_table[j][i] = digits_xor_table[i][j] = b.count();
        }
    }
}

uint sam_clock(unsigned int number)
{
    uint steps = 0;
    
    uint x = number;
    while (x > 0) {
        uint digits_sum = 0;
        
        uint y = x;
        while (y > 0)
        {
            uint digit = y % 10;
            steps += digits_lines[digit];
            digits_sum += digit;
            y = y / 10;
        }
        if (x < 10)
            break;
        x = digits_sum;
    }
    
    return steps * 2;
}

uint max_cmp(vector<unsigned char>* a, vector<unsigned char>* b)
{
    unsigned long sa = a->size();
    unsigned long sb = b->size();
    unsigned long size = min(sa, sb);
    uint ligths = 0;
    for (int i = 0; i < size; i++)
    {
        uint ia =a->at(i);
        uint ib = b->at(i);
        ligths += digits_xor_table[ia][ib];
    }
    if (sa > sb){
        for (unsigned long i = size; i < sa; i++)
        {
            ligths += digits_lines[a->at(i)];
        }
    }
    if (sa < sb){
        for (unsigned long i = size; i < sb; i++)
        {
            ligths += digits_lines[b->at(i)];
        }
    }
    return ligths;
}

uint max_clock(unsigned int number)
{
    uint steps = 0;
    bool ok = true;
    vector<unsigned char> number_digits_a;
    vector<unsigned char> number_digits_b;
    
    vector<unsigned char> * digits_a = &number_digits_a;
    vector<unsigned char> * digits_b = &number_digits_b;
    
    uint x = number;

    while (ok){
        
        //cout << "x: " << x << endl;
        // handle number
        uint new_number = 0;
        uint number_steps = 0;
        while (x > 0)
        {
            uint digit = x % 10;
            new_number += digit;
            digits_b->push_back(digit);
            number_steps += digits_lines[digit];
            x = x / 10;
        }
        
        if (digits_b->size() <=1)
        {
            steps += number_steps; // last - turn off the lights
            ok = false;
        }

        uint tmp = max_cmp(digits_a, digits_b);
        steps += tmp;
        //cout << "steps: " << tmp << endl;

        swap(digits_a, digits_b);
        digits_b->clear();
        x = new_number;
    }
    
    return steps;
    
    /*
    uint x = number;
    uint digits_sum = 0;
    while (x > 0)
    {
        uint digit = x % 10;
        number_digits.push_back(digit);
        steps += digits_lines[digit];
        digits_sum += digit;
        x = x / 10;
    }
    steps += digits_sum;
    
    //////////////////////
    
    vector<unsigned char> new_number_digits;
    
    x = digits_sum;
    while (x > 0)
    {
        uint digit = x % 10;
        new_number_digits.push_back(digit);
        x = x / 10;
    }
    
    */

}

int main() {
    
    primes_sieve();
    handle_digits();
    
    long int diff = 0;
    
    for (uint i = 0; i < 100; i++){
        cout << primes[i] << endl;
    }
    for (vector<uint>::iterator it = primes.begin(); it != primes.end(); ++it)
    {
        cout << *it << endl;
        diff += (sam_clock(*it)-max_clock(*it));
    }
    
    cout << "size " << primes.size() << endl;
    cout << "diff " << diff << endl;

    //cout << "sam " << sam_clock(137) << endl;
    //cout << "max " << sam_clock(137) << endl;
    return 0;
}
