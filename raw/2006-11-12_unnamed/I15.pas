program exe(input, output);
var
   a: real;
   b: real;
   c: real;
begin
     writeln('please enter a number [the coefficient of x^2]');
     readln(a);
     writeln('please enter a number [the coefficient of x]');
     readln(b);
     writeln('please enter a number [the constant term]');
     readln(c);
     writeln('the solutions to the equation ',a:0:1,'x^2+',b:0:1,'x+',c:0:1,' are:');
     writeln('x1=',((-b+sqrt(sqr(b)-4*a*c))/2*a));
     writeln('x2=',((-b-sqrt(sqr(b)-4*a*c))/2*a));
end.
