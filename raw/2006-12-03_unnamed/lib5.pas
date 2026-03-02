program exe (input,output);
uses crt;
var
x: real;
y: real;
begin
     clrscr;
     writeln('please enter a number:');
     readln(x);
     y:=x+5*x-2;
     writeln('x:', x:0:2, ' y=', y:0:2);
     readkey;
end.
