#include <stdio.h>
#define N 3

int girls[N+1]; // the coises of the girls
int boys[N+1];  // how many girls pick each boy

int ZeroAtBoy(); // check if some boy has 0
void SitBoyAndGirl(int i); // throw throw the parter i

int main()
{
	int i,n=0;

	// init boys
	for ( i=0; i<N+1; i++)
	{
		boys[i]=0;
	}

	// get the girl's choises
	printf("list:\n");
	for ( i=1; i<N+1; i++)
	{
		scanf("%d",&girls[i]);
		boys[girls[i]]++;
	}



	// get out the couples that nobody picked the boy
	for ( i=1; i<N+1; i++)
	{
		if (boys[i]==0)
		{
			boys[i]=-1;
			boys[girls[i]]--; // update boys array
			girls[i]=-1;
		}
	}

	//start a chain of deletation. start where the boy[i]=0.
	//thats means that girl that is sitted picked him.
	//now he gotta go to his partner girl
	while ((i=ZeroAtBoy())!=0)
	{
		// i = the boy that must be with his girl
		SitBoyAndGirl(i);
	}


	for ( i=1; i<N+1; i++)
	{
		printf("\n %d:  %d  %d",i,girls[i],boys[i]);\
		if ( (boys[i]!=-1) && (girls[i]!=-1) )
			n++;
	}
	printf("\n n is: %d\n",n);

	return 0;
}

int ZeroAtBoy()
{
	int i;
	for ( i=1; i<N+1; i++)
	{
		if (boys[i]==0)
		{
			return i;
		}
	}
	return 0;
}

void SitBoyAndGirl(int i)
{
	int GirlChoice;
	boys[i]=-1;
	GirlChoice=girls[i];
	girls[i]=-1;

	boys[GirlChoice]--; // update boys array
	if (boys[GirlChoice]==0)
		SitBoyAndGirl(GirlChoice);	
}