/*this is the assembler program*/

#include <stdio.h>

int main(int argc, char *argv[]) /*as an argument we get the file*/
{
	FILE *f1, *f2;
	int c;
	if (argc != 2){	/* not exactly one argument file */
		printf("Incorrect number of arguments, enter Assembler path_of_the_file\n");
		return 1;
	}
	if ((f1=fopen(argv[1],"r"))==NULL){
		printf("Can't open file %s\n",argv[1]);
		return 1;
	}
	f2 = fopen("hila.hack","w"); /* open for writing */
	while ((c=fgetc(f1))!=EOF){/*we copy the asm. file to the hack.file and then we'll translate*/
		fputc(c,f2);
	}
	fclose(f1);
	fclose(f2);
	return 0;



	return 0;
}


/*
	FILE *fp;
	char stuff[25];
	int index;
	fp = fopen("TENLINES.TXT","w"); /* open for writing 
	strcpy(stuff,"This is an example line.");
	for (index = 1; index <= 10; index++)
		fprintf(fp,"%s Line number %d\n", stuff, index);
	fclose(fp); /* close the file before ending program */