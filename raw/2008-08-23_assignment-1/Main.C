/* Main */

#include <stdio.h>
#include <dos.h>

extern void start_track(void);
extern void end_track(void);

void main(void)
 {
   unsigned int u1, u2,u3,u4, result;
   
   start_track();
   
	  printf("\n 1234 + 4567\n");
	  u1 = 1234;
	  u2 = 4567;

	  result = u1 + u2;

	  printf("\n 33333 + 44444\n");
	  u1 = 33333;
	  u2 = 44444;

	  result = u1 + u2;

	  printf("\n 11111 + 20000\n");

	  u1 = 11111;
	  u2 = 20000;

	  result = u1 + u2;

	  printf("\n 65000 + 1000\n");

	  u1 = 65000;
	  u2 = 1000;

	  result = u1 + u2;

	  printf("\n 65000 + 1000\n");
	  u1 = 65000;
	  u2 = 1000;

	  result = u1 + u2;

	   printf("\n 11111 + 20000\n");
	 
	  u1 = 11111;
	  u2 = 20000;

	  result = u1 + u2;
	   printf("\n 33333 + 44444 without u1&u2\n");

	  result = 33333 + 44444;
	   printf("\n 33333 + 44444\n");
	  u1 = 33333;
	  u2 = 44444;

	  result = u1 + u2;
	   printf("\n 1234 + 4567\n");
	  u1 = 1234;
	  u2 = 4567;

	  result = u1 + u2;

	   printf("\n (1234 + 4567)+(33333 + 44444)\n");
	    u1 = 1234;
	  u2 = 4567;
	  u3=33333;
		  u4=44444;

	  result = (u1 + u2)+(u3+u4);


	end_track();
}

/* tcc -ml -r- main.c hw1.asm */