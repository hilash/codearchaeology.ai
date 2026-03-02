program exe (input,output);
uses crt;
var
num: real; {gets a number}
begin
     clrscr;
     writeln('please enter number');
     readln(num);
     writeln('shkels: ',trunc(num)); {show the real value of the num}
     writeln('agorot: ',frac(num):0:3); {show the complete value of the num}
     readkey;
end.
