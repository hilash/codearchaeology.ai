program exe (input,output);
uses crt;
var
  num1: integer;
  num2: integer;
  op: char; {the operator}
begin
     clrscr;
     writeln('please enter the first number');
     readln(num1);
     writeln('please enter the second number number');
     readln(num2);
     writeln('please enter the operator');
     readln(op);
     if (op='/') and (num2=0) then {if the user wants to divide a number in 0 the program will print an error messege}
        writeln('You can not divide a number in 0. Bye!!!')
     else
         if ((op<>'/') and (op<>'*') and (op<>'-') and (op<>'+')) then{input control to check that the input as a char is an operator}
            writeln('You did not typed an operator. Bye!!!')
     else
         begin
              write(num1,op,num2,'=');
              if op='+' then {if the user wants to sum both numbers calc & print the sum}
                 writeln(num1+num2);
              if op='-' then {if the user wants to subtract number1 of number2 calc & print the sum}
                 writeln(num1-num2);
              if op='*' then {if the user wants to multiply both numbers calc & print the sum}
                 writeln(num1*num2);
              if op='/' then {if the user wants to  divide number1 of number2 calc & print the sum}
                 writeln(num1/num2:0:2)
         end;
     readkey;
end.

