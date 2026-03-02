program FLY;
uses crt;
Const
     N=3;
     P=200;
     KL=10;
     KH=99;
Type
    flight=
    record
          kod: integer;
          name: string[15];
          yaad: string[20];
          seat: integer;
          date: string[10];
    end;
    Ar=array[1..N] of flight;
Var
   A: Ar;

procedure Kelet(var B:Ar);
var
   i: integer;
begin
     for i:=1 to N do
         begin
              write('');
              write('plz enter flight kod: ');
              readln(B[i].kod);
              write('plz enter company name: ');
              readln(B[i].name);
              write('plz enter flight yaad: ');
              readln(B[i].yaad);
              write('plz enter number of seats: ');
              readln(B[i].seat);
              write('plz enter flight date: ');
              readln(B[i].date);
              writeln('');
         end;
end;

procedure Date_Change(var B:Ar);
var
   i: integer;
begin
     for i:=1 to N do
         if (B[i].yaad='USA') and (B[i].date='12.05.06') then
            begin
                 writeln('');
                 writeln('i: ',i,' date: ',B[i].date);
                 B[i].date:='14.05.06';
                 writeln('');
            end;
end;

function Place_Free(B:Ar):integer;
var
   Kod_Mone: array[KL..KH] of integer;
   Min: integer; {minimum people on plane}
   i: integer;
   MinI: integer;
begin
     for i:=KL to KH do
         Kod_Mone[i]:=0;
     for i:=1 to N do
         Kod_Mone[B[i].kod]:=Kod_Mone[B[i].kod]+B[i].seat;
     Min:=200;
     MinI:=0;
     for i:=KL to KH do
         if Kod_Mone[i]>=Min then
            begin
                 Min:=Kod_Mone[i];
                 MinI:=I;
            end;
     writeln('');
     writeln('the flight with kod ',MinI,' had max empty seats: ',P-Min);
     writeln('');
     Place_Free:=MinI;
end;

procedure Update(var B:Ar);
var
i: integer;
new_name: string[7];
old_name: string[7];
begin
     writeln('');
     write('plz enter old name: ');
     readln(old_name);
     write('plz enter new name: ');
     readln(new_name);
     for i:=1 to N do
         if B[i].name=old_name then
            begin
                 B[i].name:=old_name+'&'+new_name;
                 writeln('new name: ',B[i].name);
            end;
end;

begin
     clrscr;
     Kelet(A);
     Date_Change(A);
     Place_Free(A);
     Update(A);
     readkey;
end.
