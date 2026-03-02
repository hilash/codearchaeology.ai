program exe(input, output);
uses crt;
var
   Liter: real;
   complete: real;
   average: real;
begin
     clrscr;
     writeln('Please enter number of liters');
     readln(Liter);
     if Liter<=5 then
       complete:=10*Liter
     else
         if (Liter>5) and (Liter<=15) then
            complete:=5*10+(Liter-5)*7
     else
         complete:=((5*10)+(10*7)+((Liter-15)*5.5));
     average:=complete/Liter;
     writeln('The complete payment is:', complete:0:2);
     writeln('The average for each liter is:', average:0:2);
     readkey;
end.



