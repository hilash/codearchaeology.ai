#include <stdio.h>

#define N 100
#define LETTERS 26

void printLettersOrdered(char *, char *);
int isLower(char c);
int isUpper(char c);

int main() {
    char s1[N+1], s2[N+1];
    scanf("%s", s1); /* for the input string */
    printLettersOrdered(s1, s2);
    printf("The results for %s is %s\n", s1, s2); /*print both, input and output strings */
    return 0;
}

/* print for each letter it's occurencess in the input string, ordered by a-z, AaBb...
 * example: str"PrintCapitalLettersBeforeSmallLetters" 
 *       => res="aaaBCeeeeeefiiLLlllmnoPprrrrSsstttttt" */
void printLettersOrdered(char *str, char *res)
{
    int lower[LETTERS] = {0};
    int upper[LETTERS] = {0};
    int i = 0;
    int j = 0;

    /* write num of occurences for each letter (lower or upper) */
    while (*str != '\0') {
        if (isLower(*str)==1)
            lower[*str-'a']++;
        else if (isUpper(*str))
            upper[*str-'A']++;
        str++;
    }

    /* save the converted string */
    for (i=0; i<LETTERS; i++){
        for (j=0; j<upper[i]; j++){
            *res='A'+i;
            res++;
        }
        for (j=0; j<lower[i]; j++){
            *res='a'+i;
            res++;
        }
    }
    *res='\0';
}

/* returns if c is lowercase letter */
int isLower(char c)
{
    return (('a'<=c) && (c <= 'z'));
}

/* returns if c is uppercase letter */
int isUpper(char c)
{
    return (('A'<=c) && (c <= 'Z'));
}
