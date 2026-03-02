#include <stdio.h>

#define N 101

void change(char *);
int isLower(char);
int isUpper(char);
char toUpper(char);
char toLower(char);
int strcmp(const char*, const char*);

int main()
{
    char s[N] = {0};

    while(1){
        scanf("%s", s);
        if ((strcmp(s,"X") == 0) || (strcmp(s,"x")==0))
            break;
        change(s);
        printf("%s\n", s);
    }
    return 0;
}


void change(char *s)
{
    char *new_s = s;
    char *p = s;
    int count_under = 0;
    int in_word = 0;

    while (*p != '\0'){
        if (*p == '_'){
            count_under++;
            in_word = 0;
        }
        else { /* it's a valid char */
            /* if we had before underscores, and we're not at the begining, write '_' */
            if ((new_s != s) && (count_under >0)) {
                *new_s = '_';
                new_s++;
            }
            count_under = 0;
            if (in_word == 0){ /* convert the first letter to upper, if lower*/
                *new_s = toUpper(*p); 
            }
            else { /* convert the rest letters to lower */
                *new_s = toLower(*p);
            }
            new_s++;

            in_word = 1;
        }
        p++;
    }
    *new_s = '\0';
}

int isLower(char c)
{
    return (('a' <= c) && (c <= 'z'));
}
int isUpper(char c)
{
    return (('A' <= c) && (c <= 'Z'));
}
char toLower(char c)
{
    if (isUpper(c)==1)
        return (c + 'a' - 'A');
    return c;
}
char toUpper(char c)
{
    if (isLower(c)==1)
        return ('A' + c - 'a');
    return c;
}

int strcmp(const char* s1, const char* s2)
{
    while((*s1 == *s2) && (*s1))
    {
        s1++;
        s2++;
    }
    return *(const unsigned char*)s1 - *(const unsigned char*)s2;
}
