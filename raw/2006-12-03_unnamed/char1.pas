program exe (input,output);
uses crt;
var
c1: char;
A: char;
B: char;
begin
     clrscr;
     writeln('please enter a char');
     read(c1);
     if c1='-' then
        begin
            A:='A';
            B:='B'
        end
     else
         begin
            A:='B';
            B:='A'
         end;
     writeln('A=',A,' B=',B);
     readkey;
end.

