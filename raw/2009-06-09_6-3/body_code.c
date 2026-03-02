#include <stdio.h>

void foo(int x,int y, int z);

bool End(int x, int y, int z);

 

int main()

{

int x,y,z;

printf("Enter 3 numbers:\n");

scanf("%d%d%d",&x,&y,&z);

foo(x,y,z);

}

void foo(int x,int y, int z)

{

if (!End(x,y,z))

{

if ( (x>=y) && (x>=z) )

{

// x ma

if ((y>=z) && (y*2<=x))

{// y second

printf("y<--x\n");

foo(x-y,y+y,z);

}

else

{

// z second 

if (z*2<=x)

{

printf("z<--x\n");

foo(x-z,y,z+z);

}

else

{

if (y>=z)

{

printf("y<--x\n");

foo(x-y,y+y,z);

}

else 

{

printf("z<--x\n");

foo(x-z,y,z+z);

}

}

}

}

else if ( (z>=y) && (z>=x) )

{

// z max

if ((y>=x) && (y*2<=z))

{// y second

printf("y<--z\n");

foo(x,y+y,z-y);

}

else

{

// x second 

if (x*2<=z)

{

printf("x<--z\n");

foo(x+x,y,z-x);

}

else

{

if (y<=z)

{

printf("y<--z\n");

foo(x,y+y,z-y);

}

else 

{

printf("x<--z\n");

foo(x+x,y,z-x);

}

}

}

}

else if ( (x<=y) && (y>=z) )

{

// y max

if ( (x>=z) && (x*2<=y) )

{

// x second

printf("x<--y\n");

foo(x+x,y-x,z);

}

else

{

// z second 

if (z*2<=y)

{

printf("z<--y\n");

foo(x,y-z,z+z);

}

else

{

if (y>=z)

{

printf("z<--y\n");

foo(x,y-z,z+z);

}

else 

{

printf("x<--y\n");

foo(x+x,y-x,z);

}

}

}

}

}

}

bool End(int x, int y, int z)

{

if (x==y)

{

printf("x<--y\nTHE END!\n");

return true;

}

if (x==z)

{

printf("x<--z\nTHE END!\n");

return true;

}

if (z==y)

{

printf("z<--y\nTHE END!\n");

return true;

}

return false;

}