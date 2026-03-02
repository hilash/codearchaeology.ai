program exe;
uses crt;

const
N=3;

Type
ar=array[1..N, 1..N] of integer;

Var
a: ar;

procedure kelet(var b: ar);
var
i,j: integer;
begin
     writeln('plz write ',N*N,' numbers:');
     for i:=1 to N do
         for j:=1 to N do
             readln(b[i,j]);
end;

procedure pelet(b: ar);
var
i,j: integer;
begin
     writeln('here is the array');
     for i:=1 to N do
         begin
              for j:=1 to N do
                  write(b[i,j]:3);
              writeln();
         end;
end;

function Rows(b:ar; var rowsum1:integer):boolean;
var
i,j,sum1,sum2: integer;
booli: boolean;
begin
     booli:=true;
     sum1:=0;
     for j:=1 to N do
         sum1:=sum1+b[1,j];
     for i:=2 to N do
     begin
          sum2:=0;
          for j:=1 to N do
              sum2:=sum2+b[i,j];
          if (sum1<>sum2) then
             booli:=false;
     end;
     if (booli=true) then
     begin
          rowsum1:=sum1;
          Rows:=true;
     end
     else
         Rows:=false;

end;

function Cols(b:ar; var colsum1:integer):boolean;
var
i,j,sum1,sum2: integer;
booli: boolean;
begin
     booli:=true;
     sum1:=0;
     for i:=1 to N do
         sum1:=sum1+b[i,1];
     for j:=2 to N do
     begin
          sum2:=0;
          for i:=1 to N do
              sum2:=sum2+b[i,j];
          if (sum1<>sum2) then
             booli:=false;
     end;
     if (booli=true) then
     begin
          colsum1:=sum1;
          Cols:=true;
     end
     else
         Cols:=false;
end;

function Diagonals(b:ar; var diagonalsum1:integer):boolean;
var
i,sum1,sum2: integer;
booli: boolean;
begin
     booli:=true;
     sum1:=0;
     for i:=1 to N do
         sum1:=sum1+b[i,i];
     sum2:=0;
     for i:=1 to N do
         sum2:=sum2+b[i,N-i+1];
     if (sum1<>sum2) then
        booli:=false;
      if (booli=true) then
     begin

         diagonalsum1:=sum1;
         Diagonals:=true;
     end
     else
         Diagonals:=false;
end;

function IsMagic(b: ar): boolean;
var
rowsum, colsum, diagonalsum: integer;
boolie: boolean;
begin
     boolie:=( (Rows(b,rowsum)=true) and (Cols(b,colsum)=true) and (Diagonals(b,diagonalsum)=true) );
     if ( not( (rowsum=colsum) and (colsum=diagonalsum) and (diagonalsum=rowsum) ) ) then
        boolie:=false;
     IsMagic:=boolie;
end;

begin
     clrscr;
     kelet(a);
     pelet(a);
     if (IsMagic(a)=true) then
        writeln('The array is a magic array')
     else
         writeln('The array is not a magic array');
     readkey;
end.
