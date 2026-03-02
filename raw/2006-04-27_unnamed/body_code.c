#include <stdio.h>
char last;
int main(){
	char first;
	printf("Please enter a word:\n");
	first=getchar();
	if(first<'a'||first>'z')
		printf("You didn't type a letter. by!");
	BWTN(first);
	return 0;
}

int BWTN(first){

	char mid, enter;

	enter=getchar();

	if(enter!='\n'){
		mid=enter;
		BWTN(first);}

	else if(enter=='\n'){
		return; 
		return last=mid;}

	printf("%c", last);	
	return last;
}