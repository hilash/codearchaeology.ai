program exe;
uses crt;

const
N=6;
M=8;

Type
ar2D=array[1..N, 1..M] of integer;
arROW=array[1..N] of integer;
arCOL=array[1..M] of integer;

Var
a: ar2D;
b: arROW;
c: arCOL;

procedure kelet(var s: ar2D);
var
i,j: integer;
begin
     writeln('plz write ',N*M,' numbers:');
     for i:=1 to N do
         for j:=1 to M do
             readln(s[i,j]);
end;

procedure pelet(s: ar2D);
var
i,j: integer;
begin
     writeln('here is the array');
     for i:=1 to N do
         begin
              for j:=1 to M do
                  write(s[i,j]:3);
              writeln();
         end;
end;

procedure peletRow(s: arROW);
var
i: integer;
begin
     writeln('here is each row sum:');
     for i:=1 to N do
         writeln(i:3,': ',s[i]:3);
end;

procedure peletCol(s: arCol);
var
i: integer;
begin
     writeln('here is each column sum:');
     for i:=1 to M do
         writeln(i:3,': ',s[i]:3);
end;


procedure SumRow(s: ar2D;var row:arROW);
var
i,j,sum: integer;
begin
     for i:=1 to N do
         begin
              sum:=0;
              for j:=1 to M do
                  sum:=sum+s[i,j];
              row[i]:=sum;
         end;
end;

procedure SumCol(s: ar2D;var col:arCol);
var
i,j,sum: integer;
begin
     for j:=1 to M do
         begin
              sum:=0;
              for i:=1 to N do
                  sum:=sum+s[i,j];
              col[j]:=sum;
         end;
end;

begin
     clrscr;
     kelet(a);
     SumRow(a,b);
     SumCol(a,c);
     pelet(a);
     peletRow(b);
     peletCol(c);
     readkey;
end.
