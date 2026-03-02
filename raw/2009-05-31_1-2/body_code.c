#include <stdio.h>
#define N 4

bool Matrix[N+1][N+1];
int counter;

bool isPer(int n);
void itoa(int n, char s[]);
void reverse(char s[]);
size_t strlen(const char * str);
int     power (int base, int n);
bool foo(char* num,int n);

int main()
{
int i,j;
counter=0;
char c[N+1];

for (j=0; j<N+1; j++)
for (i=0; i<N+1; i++)
Matrix[i][j]=false;

for (i=power(10,N-1); i<power(10,N); i++)
{
if(isPer(i))
{
itoa(i,c);
if (foo(c,N)==true)
{
counter++;
printf("%d\n",i);
}
}
}
}

bool isPer(int n)
{
bool a[N];
char c[N+1];
int i,b;

for (i=0; i<N; i++)
a[i]=false;

itoa(n,c);

for (i=0; i<N; i++)
{
if ((c[i]-48<1) || (c[i]-48>N) )
return false;
a[c[i]-49]=true;
}

for (i=0; i<N; i++)
if (a[i]==false)
return false;

return true;
}

/* itoa:  convert n to characters in s */
void itoa(int n, char s[])
{
    int i, sign;

    if ((sign = n) < 0)  /* record sign */
        n = -n;          /* make n positive */
    i = 0;
    do {       /* generate digits in reverse order */
        s[i++] = n % 10 + '0';   /* get next digit */
    } while ((n /= 10) > 0);     /* delete it */
    if (sign < 0)
        s[i++] = '-';
    s[i] = '\0';
    reverse(s);
} 

/* reverse:  reverse string s in place */
void reverse(char s[])
{
    int c, i, j;

    for (i = 0, j = strlen(s)-1; i<j; i++, j--) {
        c = s[i];
        s[i] = s[j];
        s[j] = c;
    }
}

size_t strlen(const char * str)
{
    const char * s = str;
    for (; *s; ++s);
    return(s - str);
}

int power (int base, int n) {
    int     i,
            p;
    p = 1;
    for (i = 1; i <= n; ++i)
p *= base;
    return p;
}
bool foo(char* num,int n)
{

if (n==1)
return true;
 if (Matrix[num[n-1]-48][num[n-2]-48]==true)
return false;
else
{
Matrix[num[n-1]-48][num[n-2]-48]=true;
Matrix[num[n-2]-48][num[n-1]-48]=true;
if (foo(num,n-1)==false)
{
Matrix[num[n-1]-48][num[n-2]-48]=false;
Matrix[num[n-2]-48][num[n-1]-48]=false;
return false;
}
}
}