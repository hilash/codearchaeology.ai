#include <stdio.h>
void print_base_2(int num);
int main() {
    int x;
    scanf("%d",&x);
    print_base_2(x);
    return 0;
}
void print_base_2(int num)
{
    int reminder, div;
    reminder = num % 2;
    div = num / 2;

    printf("%d", reminder);
    if (div == 0)
        printf("-");
    else
        print_base_2(div);
    printf("%d", reminder);
}
