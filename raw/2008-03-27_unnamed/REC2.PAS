program Recursion2;
uses wincrt;

const
M=10;

type
ar= array[1..M] of integer;
cr= array[1..M] of char;

var
a: ar;
c: cr;

{----------Build & print array---------------------}
procedure Build_Array (var a: ar);
var
i: integer;
begin
     writeln('please enter ',M,' numbers');
     for i:=1 to M do
         readln(a[i]);
end;

procedure Print_Array (a: ar);
var
i: integer;
begin
     writeln('The array:');
     for i:=1 to M do
         writeln(a[i]);
end;

procedure Build_Array_C (var a: cr);
var
i: integer;
begin
     writeln('please enter ',M,' chars');
     for i:=1 to M do
         readln(a[i]);
end;

procedure Print_Array_C (a: cr);
var
i: integer;
begin
     writeln('The array:');
     for i:=1 to M do
         writeln(a[i]);
end;

{----------Exe 1-----------------------------------}
{----------complexity: O(n)------------------------}
function Array_Sum (a: ar; n: integer): integer;
begin
     if n=1 then
        Array_Sum:= a[1]
     else
         Array_Sum:= Array_Sum(a, n-1) + a[n];
end;

{----------Exe 2-----------------------------------}
{----------complexity: O(n)------------------------}
function Array_Average (a: ar; n: integer): real;
begin
     if n=1 then
        Array_Average:= a[1]
     else if n=M then
         Array_Average:= (Array_Average(a, M-1) + a[M])/M
     else Array_Average:= Array_Average(a, n-1) + a[n];
end;

{----------Exe 3-----------------------------------}
{----------complexity: O(n)------------------------}
function Array_Bigger (a: ar; n: integer; k:real): integer;
begin
     if n=1 then
        if a[1]>k then
           Array_Bigger:= 1
        else
            Array_Bigger:= 0
     else
         if a[n]>k then
            Array_Bigger:= Array_Bigger(a, n-1,k) + 1
         else
             Array_Bigger:= Array_Bigger(a, n-1,k) + 0;
end;

{----------Exe 4-----------------------------------}
{----------complexity: O(n)------------------------}
function Array_Bigger_Average (a: ar; n: integer): integer;
begin
     Array_Bigger_Average:=Array_Bigger(a,n,Array_Average(a,n));
end;


{----------Exe 5-----------------------------------}
{----------complexity: O(n)------------------------}
function Array_Palindrome (a: cr; i,j: integer): boolean;
begin
     if (i=j+1) or (i=j) then
        if a[i]=a[j] then
           Array_Palindrome:=true
        else
            Array_Palindrome:=false
     else
         if a[i]=a[j] then
                Array_Palindrome:=Array_Palindrome(a,i+1,j-1)
             else
                 Array_Palindrome:=false;
end;

{----------Exe 6-----------------------------------}
{----------complexity: O(n)------------------------}
function Array_Sorted (a: ar; n: integer): boolean;
begin
     if n=1 then
        Array_Sorted := true
     else
         if a[n]>=a[n-1] then
            Array_Sorted:= Array_Sorted(a, n-1)
         else
             Array_Sorted:=false;
end;

begin
     Build_Array(a);
     Print_Array(a);
     writeln('Array_Sum(a,M):  ',Array_Sum(a,M));
     writeln('Array_Average(a,M):  ',Array_Average(a,M):0:3);
     writeln('Array_Bigger(a,M,5):  ',Array_Bigger(a,M,5));
     writeln('Array_Bigger_Average(a,M):  ',Array_Bigger_Average(a,M));
     Build_Array_C(c);
     Print_Array_C(c);
     writeln('Array_Palindrome(c,3,5):  ',Array_Palindrome(c,3,5));
     writeln('Array_Sorted(a,M):  ',Array_Sorted(a,M));
     readkey;
end.
