program Recursion1;
uses crt;

const
N=5;

type
ar= array[1..N] of integer;

var
a: ar;
i,m, o,p: integer;
c: char;


{-----------EXE 1---------------------------------}
function ATZ (n:integer): integer;
begin
     if n=1 then
        ATZ:=1
     else
         ATZ:=ATZ(n-1)*n;
end;


{-----------EXE 2---------------------------------}
function MUL (n,m:integer): integer;
begin
     if n=1 then
        MUL:=m
     else
         MUL:=MUL(m,n-1)+m;
end;

function MULT (n,m:integer): integer;
begin
     if ( ( (n<=0) and (m>=0) ) or  ( (n>=0) and (m<=0) ) ) then
        MULT:=-MUL(abs(n),abs(m))
     else
         MULT:=MUL(abs(n),abs(m));
end;

{-----------EXE 3---------------------------------}
function power (n,m:integer): integer;
begin
     if n=1 then
        power:=m
     else
         power:=power(n-1,m)*m;
end;

{-----------EXE 4---------------------------------}
function fib (n:integer): integer;
begin
     if (n=1) then
        fib:=0
     else if (n=2) then
          fib:=1
     else
         fib:=fib(n-1)+fib(n-2);
end;

{----------Exe 5-----------------------------------}
{----------complexity: O(n)------------------------}
procedure Print_Backwards_num (n: integer);
begin
     if n<>0 then
     begin
          write(n mod 10);
          Print_Backwards_num(n div 10);
     end;
end;

{----------Exe 6-----------------------------------}
{----------complexity: O(n)------------------------}
procedure Print_Backwards ();
var
c: char;
begin
     readln(c);
     if c<>'.' then
     begin
          print_Backwards();
          writeln(c);
     end;
end;

{----------Exe 7-----------------------------------}
{----------complexity: O(n)------------------------}
procedure Print_Backwards_dot ();
var
c: char;
begin
     readln(c);
     if c<>'.' then
          print_Backwards_dot();
     writeln(c);
end;

{-----------EXE 8---------------------------------}
procedure tur1 (n: integer);
begin
     if n>0 then
        begin
             tur1(n-2);
             write(n,',');
        end;
end;

procedure tur2 (n: integer);
begin

end;

{-----------EXE 9---------------------------------}
function sum_in_ar ( a: ar; n: integer): integer;
begin
     if n=1 then
        sum_in_ar:=a[1]
     else
         sum_in_ar:=a[n]+sum_in_ar(a, n-1);
end;

{-----------EXE 10--------------------------------}
procedure wave ( c: char; n: integer);
var
i: integer;
begin
     for i:=1 to n do
         write(c);
     writeln;
     if (n>1) then
        begin
             wave(c,n-1);
             for i:=1 to n do
                 write(c);
             writeln;
        end;
end;

begin
     clrscr;
     {for i:=1 to N do
         readln(a[i]);
     for i:=1 to N do
         writeln(a[i]);
     writeln(sum_in_ar(a,3));
     writeln(sum_in_ar(a,4));
      writeln(sum_in_ar(a,5));
       writeln(sum_in_ar(a,10));}
     { readln(i);
      wave('$',i);
      tur1(20);
      writeln;
      tur1(9);
      writeln;
      readln(o,p);
      writeln(MULT(o,p));
      writeln(atz(p));
      writeln(power(o,p));}
      writeln(fib(1));
      writeln(fib(2));
      writeln(fib(3));
      writeln(fib(4));
      writeln(fib(5));
      writeln(fib(6));

     readkey;
end.