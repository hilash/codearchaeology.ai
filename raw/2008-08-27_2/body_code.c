int pidA,pidB;

volatile int flag;

void xmain()
{
	int procA(),procB();
	int i;
	for (i=(-1); i<=1; i++)
	{
		printf("The Current Battle: A");
		if (i==(-1)) printf(" < B\n");
		if (i==0) printf(" = B\n");
		if (i==1) printf(" > B\n");
		pidA = create(procA,INITSTK,INITPRIO+i,"procA",0);
		pidB = create(procB,INITSTK,INITPRIO,"procB",0);
		flag=0;
		resume(pidA);
		while (flag==0);
		if (flag==1) printf("A wins!\n\n");
		else if (flag==2) printf("B wins!\n\n");
		else printf("None were activated!\n\n");
		kill(pidB);
	}
}

procA()
{
	int procB();
	resume(pidB);
	if (flag==0)
		flag=1;
}

procB()
{
	if (flag==0)
		flag=2;
}