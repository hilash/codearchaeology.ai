/*****************************
* ID:
* Assignment number:04
*****************************/

#include <stdio.h>
int main()
{
	int l, spc1,sp,e,s,t,j,i,r,k,n;
	printf("To print a triangle, you need to insert 2 numbers - \nThe first number 'n' is the size of the big triangle basis, and the second\nnumber 'k' is the size of the small triangle basis.\nBoth 'n' and 'k' should be odds, and n=k*1+r-1.\n");
	while (scanf("%d%d",&n, &k)) {
		r=n/k;
		sp=n/2;

		/*scanf("%d%d",&n, &k);*/

		if (  (k- (k/2)*2)  == 0 || (k*r+r-1)!=n )  {

			printf("wrong inputs try again\n");
			continue;
		}

		r=n/k;
		sp=n/2;

		for (i=1; i<=r; i++) {   /*sort each triangle block  */
  
			for (j=1; j<=(k + 1) / 2; j++){   /*sort each line in triangle block  */

				for (spc1=1; spc1<=sp; spc1++)	printf(" ");  sp--;   /*create the space at the begining of each line */
					 
				for (t=1; t<=i; t++){ /*build the line  */
						  
					for (s=1; s<=j * 2 - 1; s++) 	printf("*");  /*insert *  */

					l= ((k + 1) - j * 2 + 1);
					for (e=1; e<=l; e++) printf(" ");    								 
				}			
				printf("\n");  /*new line */
			}
		}
		break;
	}
	return 0;
}
