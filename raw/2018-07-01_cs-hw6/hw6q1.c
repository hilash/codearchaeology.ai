#include <stdio.h>
#define MAX_SIZE 1000

int is_sum(int x, int a[],int n);

int main() {
    int i, target_num, size_array, array[MAX_SIZE];
    scanf("%d",&size_array);
    for(i=0;i<size_array;i++)
        scanf("%d",&array[i]);
    scanf("%d",&target_num);
    printf("%d",is_sum(target_num,array,size_array));
    return 0;
}

int is_sum(int x, int a[], int n)
{
    if (x==0)
        return 1; /* return success if reached sum */
    else if (n<=0)
        return 0; /* return fail if not we're out of array bounds */

    return (is_sum(x-a[0], a+2, n-2)) || (is_sum(x-a[1], a+3, n-3));
}
