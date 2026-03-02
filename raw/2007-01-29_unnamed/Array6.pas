program Ar6(input, output);{input: N numbers, stored in array X.
                            output: array X's cells organized in araay Y so first stored
                            the complete numbers that biegger than 100}
uses crt;
const
N=10;
Type
Ar= Array [1..N] of real;
Var
X: Ar;
Y: Ar;
i: integer;
j: integer;
begin
     clrscr;
     writeln('PLEASE ENTER ',N,' NUMBERS:');
     j:=1;
     for i:=1 to N do
           begin
                readln(X[i]);
                if {(X[i]/1<>X[i]) and }(X[i]>100) then {if the number>100 and complete store it in array Y's first cells}
                   begin
                        Y[j]:=X[i];
                        j:=j+1;
                   end;
           end;
     for i:=1 to N do
         begin
              if {((X[i]/1)<>0) or }(X[i]<=100) then {if the number<100  or incomplete store it in array Y's remaining cells}
                begin
                     Y[j]:=X[i];
                     j:=j+1;
                end;
         end;
     for i:=1 to N do
         write(X[i]:0:1,'  ');
     writeln();
     for i:=1 to N do
         write(Y[i]:0:1,'  ');
     readkey;
end.
