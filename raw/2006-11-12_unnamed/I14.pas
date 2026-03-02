program exe(input, output);
var
   a: real;
   b: real;
begin
     writeln('please enter a number [the coefficient of x');
     readln(a);
     writeln('please enter a number [the constant term]');
     readln(b);
     writeln('the solution to the equation is x=',((-b)/a):0:3);

end.
