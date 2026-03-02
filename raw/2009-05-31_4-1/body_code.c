#include <stdio.h>
#define N 3

 int n,k;

int foo(int a[],bool flag);

int main()
{
int a[N],i,k,j,counter=0;
int c[N],b[N],d[N];

k=1;

for (i=0; i<N; i++)
{
scanf("%d",&a[i]);
b[N-1-i]=c[N-1-i]=d[i]=a[i];
}

for (i=0; i<N; i++)
printf("%d ",a[i]);

foo (a,true);
foo (c,true);
//foo(d,true);
//else foo (c,true);
return 0;
}

int foo(int a[],bool flag)
{

int i,b,j,counter=0;

b=a[N-1]-k;
if (b>0)
if (a[0]<k)
{
if (b+a[0]>k)
{
j=k-a[0]; // what i move
a[N-1]-=j;
a[0]+=j;
printf("place: %d      how:  %d     dir: R\n",N,j);
}
else {
a[0]+=b;
printf("place: %d      how:  %d     dir: R\n",N,b);
a[N-1]=k;
}
counter++;
}

for (i=0; i<N-1; i++)
{
b=a[i]-k;
if (b>0)
{
a[i]=k;
a[i+1]+=b;
if (flag==true)
printf("place: %d      how:  %d     dir: R\n",i+1,b);
counter++;
}
if (b<0)
{
//a[i]=k;
a[i+1]+=b;
if (flag==true)
printf("place: %d      how:  %d     dir: L\n",i+2,-b);
counter++;
}
}
//for (i=0; i<N; i++)
//printf("%d ",a[i]);

if (flag==true)
printf ("\ncounter: %d\n",counter);
return counter;
}