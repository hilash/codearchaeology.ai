program exe(input, output);
uses crt;
var
   num1: real;
   num2: real;
   num3: real;
   all: real;
begin
     clrscr;
     writeln('Please enter 3 prices');
     readln(num1,num2,num3);
     all:=num1+num2+num3;
     if all>100 then
        begin
             writeln('the older price was:', all:0:2);
             all:=all*90/100;
             writeln('the new price is:', all:0:2)
        end
     else
      begin
             writeln('the price is:', all:0:2);
             writeln('Good Happy YEAR!:')
        end;
 readkey;
end.



