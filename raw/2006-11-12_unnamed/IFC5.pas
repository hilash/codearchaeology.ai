program exe(input, output);
uses crt;
var
   income: longint;
   tax: real;
begin
     clrscr;
     writeln('Please enter your income');
     readln(income);
     if (income>=0) and (income<2001) then
        tax:=0
     else
         if (income>=2001) and (income<4001) then
            tax:=25
     else
         if (income>=4001) and (income<6501) then
            tax:=30
     else
         if (income>=6501) and (income<9001) then
            tax:=45
     else
         if income>=9001 then
            tax:=50;
     writeln('your taxes are:', (income*tax/100):0:1);
     readkey;
end.



