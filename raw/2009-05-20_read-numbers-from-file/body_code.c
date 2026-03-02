#include <stdio.h>
#define N 10000

int main() {

  FILE *file;
  const int size;
  int deg[N]; // deg[i]=the degree of vertex i
  int fix[N]; // fix[i]=the new number of vertex i	int old[N]; // old[i]=the old number of vertex i (holds the same values as fix)
  int numbers[30];

  /* make sure it is large enough to hold all the data! */
  int i,j;

  file = fopen("exe1.txt", "r");

  if(file==NULL) {
    printf("Error: can't open file.\n");
    return 1;
  }
  else {
    printf("File opened successfully.\n");

    i = 0 ;    

    while(!feof(file)) { 
      /* loop through and store the numbers into the array */
      fscanf(file, "%d", &numbers[i]);
      i++;
    }

    printf("Number of numbers read: %d\n\n", i);
    printf("The numbers are:\n");

    for(j=0 ; j<i ; j++) { /* now print them out 1 by 1 */
      printf("%d\n", numbers[j]);
    }

    fclose(file);
    return 0;
  }
}