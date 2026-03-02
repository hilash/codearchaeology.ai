// nur1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

using namespace std;
#include <iostream>
#include <stdio.h>
#include <math.h>
#include <fstream>
#include <string.h>
#include <stdlib.h>
#include <iomanip>
#include <time.h>



const double ALPHA = 0.019;
const int MAX_RETIES = 1000;
const int NOISE_RATE = 10;
const char* EXAMPLE_DIRECTORY = "C:/neuron/examples/";


enum neuron_input
{
  BAD  = -1,
  THRESHOLD = 0,
  GOOD =  1
};

class bool_neuron_data
{
public:
  int num_of_examples;
  int inputs_per_example;
  neuron_input** input;
  neuron_input* result;

  bool_neuron_data(){}

  bool_neuron_data(neuron_input expected_results[])
  {
	 num_of_examples = 4;
	 inputs_per_example=2;
	 int b;
	 input =  new neuron_input*[4];
	 for(b=0;b<num_of_examples;b++)
	 {
		input[b] = new neuron_input[2];
	 }
	 input[0][0] = BAD;
	 input[0][1] = BAD;
	 input[1][0] = GOOD;
	 input[1][1] = BAD;
	 input[2][0] = BAD;
	 input[2][1] = GOOD;
	 input[3][0] = GOOD;
	 input[3][1] = GOOD;

	 result = expected_results;
  }
  neuron_input* get_example(int i)
  {
	 return input[i];
  }
  neuron_input get_result(int i)
  {
	 return result[i];
  }

  void add_noise()
  {
	 int i,j;
	 for(i=0;i<num_of_examples;i++)
	 {
		for(j=0;j<inputs_per_example;j++)
		{
			if((rand()%NOISE_RATE)==0)
		  {
			 input[i][j]=(input[i][j]==BAD)?(GOOD):(BAD);
		  }
		}
	 }
  }

};
class icon_neuron_data: public bool_neuron_data
{
public:

  icon_neuron_data(neuron_input expected_results[]):bool_neuron_data()
  {
	 char* filename=new char[100];

	 num_of_examples = 18;
	 inputs_per_example = 100;
	 int b,c;
	 char ch='1', *num=new char[10];
	 input =  new neuron_input*[18];
	 for(b=0;b<num_of_examples;b++)
	 {
		c=0;
		input[b] = new neuron_input[100];
		strcpy(filename,EXAMPLE_DIRECTORY);
		itoa(b+1,num,10);
		strcat(filename,num);
		strcat(filename,".txt");
		ifstream input_file(filename);
		if(!input_file)
		  {cout << "error";throw -8;}
		while(!(input_file.eof()))
		{
		  input_file >>ch;
		  switch(ch)
		  {
			 case '_':
				input[b][c]=BAD;
				c++;
			 break;
			 case '0':
				input[b][c]=GOOD;
				c++;
			 break;
			 default:
			 break;
		  }
		}
		if(c!=101)
		{
		  throw -9;
		}
		input_file.close();
	 }
	 result = expected_results;
  }

  neuron_input* get_example(int i)
  {
	 return input[i];
  }
  neuron_input get_result(int i)
  {
	 return result[i];
  }
};

// this class simulate a basic perceptron
class perceptron
{
public:
  int num_of_inputs; // how many inputs the perceptro gets
  float* weight;     // an array of the weights

  perceptron(int inputs_needed)
  {
	 int i;
	 num_of_inputs = inputs_needed;
	 weight = new float[inputs_needed+1];
	 for(i=0;i<inputs_needed+1;i++)
	 {
		weight[i]=0;
	 }
  }
  // printing the perceptron is printing it's weights
  friend ostream& operator<< (ostream& os, const perceptron& p)
  {
	 for(int i=0;i<p.num_of_inputs;i++)
	 {
		os << setw(9)<< setprecision(4) <<(p.weight[i])<<",";
	 }
	 os << setw(9)  << setprecision(4) << p.weight[p.num_of_inputs];
	 return (os);
  }

  // getting an input and using it for the learning.
  // return the error found
  virtual float  input_example(neuron_input perceptron_input[],
			  neuron_input expected_result)
  {
	 int i;
	 double sum=0;

	 // summing the inputs
	 for(i=0;i<num_of_inputs;i++)
	 {
		sum+= perceptron_input[i]* weight[i];
	 }
	 sum+= weight[num_of_inputs];

	 // check if the answer was correct
	 if(((sum>THRESHOLD) && (expected_result==GOOD)) ||
		 ((sum<THRESHOLD) && (expected_result==BAD)))
	 {
		// answer was OK - return 0 errors
		return 0;
	 }
	 else
	 {
		// answer was bad, need fixing
		for(i=0;i<num_of_inputs;i++)
		{
		  weight[i] += expected_result*perceptron_input[i];
		}
		weight[num_of_inputs] += expected_result;

		// returning 1 error
		return 1;
	 }
  }

  // calculating the perceptron's output
  virtual float calc(neuron_input perceptron_input[])
  {
	 int i;
	 double sum=0;

	 for(i=0;i<num_of_inputs;i++)
	 {
		sum+= perceptron_input[i]* weight[i];
	 }
	 sum+= weight[num_of_inputs];
	 if(sum>THRESHOLD)
		return GOOD;
	 return BAD;
  }

  // going over all the examples one time
  float single_training_phase(bool_neuron_data data, bool is_input)
  {
	 int i;
	 float result;
	 float count=0;
	 for(i=0;i<data.num_of_examples;i++)
	 {
	    if(is_input)
		{   
			count+= input_example(data.get_example(i), data.get_result(i));
		}else
		{
		   result = calc(data.get_example(i));
		   if(((result<THRESHOLD) &&  (data.get_result(i)>THRESHOLD)) ||
			   ((result>THRESHOLD) && (data.get_result(i)<THRESHOLD)))
			{
			   count++;
			}
		}
	 }
	 // returning the sum of the errors
	 return count;
  }
  // training on all the examples until all the inputs are correct
  int train(bool_neuron_data data)
  {
	 int i=0;
	 float count=1;
	 while(count>0)
	 {
		count= single_training_phase(data, true);

		i++;
		if(i>(MAX_RETIES))
		{
		  return -i;
		}
	 }
	 do
	 {
	    count = single_training_phase(data,false);
	    if(count>0)
           single_training_phase(data, true);
	 }while(count>0);
	 return i;
  }
  void train_run_n_print(char*            perceptron_name,
								 bool_neuron_data data)
  {
	 int i,num_of_iter=0;
	 {
		num_of_iter = train(data);
	 }
	 if(num_of_iter<0)
	 {
		cerr << "ERROR: number of iterations needed for "<< perceptron_name
			  <<" is over " << -num_of_iter << endl;
		//return;
	 }
	 if(data.num_of_examples<1)
	 {
		cerr << "ERROR: no example was given for " << perceptron_name << endl;

		return;
	 }

	 cout << "the number of iterations needed for "
			<< perceptron_name <<" learning: "
			<< num_of_iter << "\n";
	 cout << "the perceptron look like this :"
			<< *this << "\n";
	 cout << perceptron_name << " results           : " << (calc(data.get_example(0))>THRESHOLD?("T"):("F"));
	 for(i=1;i<data.num_of_examples;i++)
	 {
		cout << ", " << (calc(data.get_example(i))>THRESHOLD?("T"):("F"));
	 }
	 cout << "\n";
     
	 data.add_noise();
	 cout << perceptron_name << " results with noise: " << (calc(data.get_example(0))>THRESHOLD?("T"):("F"));
	 for(i=1;i<data.num_of_examples;i++)
	 {
		cout << ", " << (calc(data.get_example(i))>THRESHOLD?("T"):("F"));
	 }
	 cout << "\n\n";
  }
};
class adaline : public perceptron
{
  public:
  adaline (int inputs_needed): perceptron(inputs_needed)
  {
  }
  virtual float input_example(neuron_input perceptron_input[],
										neuron_input  expected_result)
  {
	 int i;
	 float result_calculated,error_calculated;

	 result_calculated= calc(perceptron_input);
	 error_calculated= (expected_result-result_calculated);
	 for(i=0;i<num_of_inputs;i++)
	 {
		weight[i]+=ALPHA*error_calculated*perceptron_input[i];
	 }
	 weight[num_of_inputs]+= ALPHA*error_calculated;

	 if(expected_result*result_calculated<=0)
	 {
		return 1;
	 }
	 else
	 {
		return 0;
	 }
  }

  virtual float calc(neuron_input perceptron_input[])
  {
	 int i;
	 double sum=0;

	 for(i=0;i<num_of_inputs;i++)
	 {
		sum+= perceptron_input[i]* weight[i];
	 }
	 sum+= weight[num_of_inputs];
	 return sum;
  }
};

int _tmain(int argc, _TCHAR* argv[])
{
  srand( (unsigned)time( NULL ) );
  perceptron binari_perceptron(2),icon_perceptron(100);
  adaline    binari_adaline(2),   icon_adaline(100);
  bool_neuron_data *data;
  icon_neuron_data *data2;
  
  cout << " QUESTION 1,3 :\n" <<
	      " --------------\n";
  neuron_input and_results[4] = {BAD,BAD,BAD,GOOD};
  data = new  bool_neuron_data(and_results);
  binari_perceptron.train_run_n_print("AND",*data);
  data = new  bool_neuron_data(and_results);
  binari_adaline.train_run_n_print("AND",*data);

  neuron_input or_results[4]  = {BAD,GOOD,GOOD,GOOD};
  data = new  bool_neuron_data(or_results);
  binari_perceptron.train_run_n_print("OR",*data);
  data = new  bool_neuron_data(or_results);
  binari_adaline.train_run_n_print("OR",*data);

  neuron_input implies_results[4]  = {GOOD,BAD,GOOD,GOOD};
  data = new  bool_neuron_data(implies_results);
  binari_perceptron.train_run_n_print("IMPLIES",*data);
  data = new  bool_neuron_data(implies_results);
  binari_adaline.train_run_n_print("IMPLIES",*data);


  cout << " QUESTION 2 :\n" <<
	      " ------------\n";
  neuron_input xor_results[4]  = {BAD,GOOD,GOOD,BAD};
  data = new  bool_neuron_data(xor_results);
  binari_perceptron.train_run_n_print("XOR",*data);
  data = new  bool_neuron_data(xor_results);
  binari_adaline.train_run_n_print("XOR",*data);



  cout << " QUESTION 4 :\n" <<
	      " ------------\n";
  neuron_input B_results[18]  = {GOOD,GOOD,GOOD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD};
  data2 = new  icon_neuron_data(B_results);
  icon_perceptron.train_run_n_print("B",*data2);
  data2 = new  icon_neuron_data(B_results);
  icon_adaline.train_run_n_print("B",*data2);

  neuron_input E_results[18]  = {BAD,BAD,BAD,GOOD,GOOD,GOOD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD};
  data2 = new  icon_neuron_data(E_results);
  icon_perceptron.train_run_n_print("E",*data2);
  data2 = new  icon_neuron_data(E_results);
  icon_adaline.train_run_n_print("E",*data2);

  neuron_input F_results[18]  = {BAD,BAD,BAD,BAD,BAD,BAD,GOOD,GOOD,GOOD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD};
  data2 = new  icon_neuron_data(F_results);
  icon_perceptron.train_run_n_print("F",*data2);
  data2 = new  icon_neuron_data(F_results);
  icon_adaline.train_run_n_print("F",*data2);

  neuron_input I_results[18]  = {BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,GOOD,GOOD,GOOD,BAD,BAD,BAD,BAD,BAD,BAD};
  data2 = new  icon_neuron_data(I_results);
  icon_perceptron.train_run_n_print("I",*data2);
  data2 = new  icon_neuron_data(I_results);
  icon_adaline.train_run_n_print("I",*data2);
 
  neuron_input J_results[18]  = {BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,GOOD,GOOD,GOOD,BAD,BAD,BAD};
  data2 = new  icon_neuron_data(J_results);
  icon_perceptron.train_run_n_print("J",*data2);
  data2 = new  icon_neuron_data(J_results);
  icon_adaline.train_run_n_print("J",*data2);

  neuron_input K_results[18]  = {BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,BAD,GOOD,GOOD,GOOD};
  data2 = new  icon_neuron_data(K_results);
  icon_perceptron.train_run_n_print("K",*data2);
  data2 = new  icon_neuron_data(K_results);
  icon_adaline.train_run_n_print("K",*data2);

  getchar();

  return 0;
}