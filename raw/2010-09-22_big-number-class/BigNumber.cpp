#include "BigNumber.h"

BigNumber::BigNumber(int n)
{
	unsigned int x, l = 0;
	static char tmp[20];

	sign = (n < 0)? true : false;
	x = sign ? n*(-1) : n;
	do
	{
		tmp[l++] = x % 10;
		x /= 10;
	}
	while (x > 0);

	length = l;
	number = new char[length];
	memcpy(number, tmp, length);
}

BigNumber::BigNumber(const BigNumber& a)
{
	sign	= a.sign;
	length	= a.length;
	number = new char[length];
	memcpy(number, a.number, length);
}

BigNumber::~BigNumber()
{
	delete[] number;
}

BigNumber& BigNumber::operator= (BigNumber const& a)
{
	if (this != &a)
	{
		sign	= a.sign;
		length	= a.length;

		delete[] number;
		number = new char[length];
		memcpy(number, a.number, length);
	}
	return *this;
}

BigNumber BigNumber::operator* (const BigNumber& b) const
{
	const BigNumber a = *this;
	BigNumber c;
	char* mul = new char[a.length + b.length];
	char tmp;

	delete[] c.number;
	memset(mul,0,a.length + b.length);

	// multiplication
	for (int i = 0; i< a.length; ++i)
	{
		for (int j = 0; j<b.length; ++j)
		{
			tmp = a.number[i]*b.number[j] + mul[i+j];
			mul[i+j] = tmp % 10;
			mul[i+j+1] += tmp/ 10;
		}
	}

	// detect real length - take only valid digits (ignore MSB zeros)
	int i = a.length + b.length - 1;
	while ((i > 0) && (mul[i]==0)) { i--;}

	c.length = i + 1;
	c.sign = a.sign^b.sign;
	c.number = new char[c.length];
	memcpy(c.number, mul, c.length);
	delete[] mul;
	return c;
}

BigNumber BigNumber::operator+ (const BigNumber& b) const
{
	// only for positive numbers
	const BigNumber a = *this;
	BigNumber c;
	int max_len = max(a.length,b.length)+1;
	char* addition = new char[max_len];
	char tmp;

	delete[] c.number;
	memset(addition,0,max_len*sizeof(char));

	// addition
	for (int i = 0; i< max_len - 1; ++i)
	{
		tmp = addition[i];
		tmp += (i < a.length)?a.number[i]:0;
		tmp += (i < b.length)?b.number[i]:0;
		addition[i] = tmp % 10;
		addition[i + 1] = tmp / 10;
	}

	// detect real length - take only valid digits (ignore MSB zeros)
	int i = max_len - 1;
	while ((i > 0) && (addition[i]==0)) { i--;}

	c.length = i + 1;
	c.sign = a.sign;
	c.number = new char[c.length];
	memcpy(c.number, addition, c.length);
	delete[] addition;
	return c;
}

BigNumber BigNumber::reverse() const
{
	const BigNumber a = *this;
	BigNumber c;
	delete[] c.number;

	int i;
	for (i=0; a.number[i] == 0; i++);

	c.length = a.length - i;
	c.sign = a.sign;
	c.number = new char[c.length];
	for (i = 0; i <c.length; i++)
	{
		c.number[i] = a.number[a.length - i - 1];
	}
	return c;
}
int BigNumber::DigitsSum()
{
	int sum = 0;
	for (int i=0; i<length; ++i)
	{
		sum += number[i];
	}
	return sum;
}

bool BigNumber::IsPalindrom()
{
	int i = 0;
	while (i<length/2)
	{
		if (number[i++]!=number[length - i - 1])
		{
			return false;
		}
	}
	return true;
}

istream& operator>> (istream &is, BigNumber &aR)
{
//	string contType;
//	int numCont;
//	double diameter;
//	
//	is >> contType >> numCont >> diameter; //("plastic";,5,5)
//aRecMac.set_containerType(contType); //string 
//aRecMac.set_numContainers(numCont);//int
//aRecMac.set_diameter(diameter);//double
return is;
}

ostream& operator<< (ostream &os, const BigNumber &a)
{
	const int len = a.length;
	char *tmp = new char[len+1];

	if(a.sign) { os.put('-');}

	for (int i=0; i<len; ++i)
	{
		tmp[i] = a.number[len - 1 - i] + 48;
	}
	os.write(tmp,len);
	delete[] tmp;
	return os;
}