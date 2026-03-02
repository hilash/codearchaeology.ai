program exe (input,output);
uses crt;
var
a: real; {gets the height of the first student}
b: real; {gets the height of the second student}
c: real; {gets the height of the third student}
difference: real; {calcs the difference between the high height and the low height}
begin
     clrscr;
     writeln('please enter the first students height');
     readln(a);
     writeln('please enter the second students height');
     readln(b);
     writeln('please enter the third students height');
     readln(c);
     writeln(a:0:2,'    ', b:0:2,'    ', c:0:2);
     if ((b>c) and (c>a)) or ((a>c) and (c>b)) then{if a is the bigger number and b is the smaller or the opposite}
        difference:=abs(a-b)
     else
         if ((b>a) and (a>c)) or ((c>a) and (a>b)) then{if b is the bigger number and c is the smaller or the opposite}
            difference:=abs(b-c)
     else
         if ((a>b) and (b>c)) or ((c>b) and (b>a)) then{if a is the bigger number and c is the smaller or the opposite}
            difference:=abs(a-c);
     writeln(difference:0:2);
     readkey;
end.
