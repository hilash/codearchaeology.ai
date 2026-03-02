program BEZEK;
uses crt;
Const
N=4;
Type
    Member=record
       first_name: string[10];
       last_name: string[20];
       kod: string[2];
       mikud: string[4];
       zone: string[3];
       phone: string[7];
    end;
Ar=array[1..N] of Member;
var
A: Ar;
procedure KELET(var B:Ar);
var
i: integer;
begin
     for i:=1 to N do
         begin
              write('first_name: ');
              readln(B[i].first_name);
              write('last_name: ');
              readln(B[i].last_name);
              write('kod: ');
              readln(B[i].kod);
              write('mikud: ');
              readln(B[i].mikud);
              write('zone: ');
              readln(B[i].zone);
              write('phone: ');
              readln(B[i].phone);
         end;
end;

procedure DEL(var B:Ar);
var
i: integer;
begin
     for i:=1 to N do
         if copy(B[i].phone,1,3)='014' then
            begin
                 delete(B[i].phone,1,3);
                 B[i].phone:='08'+B[i].phone;
            end;
end;

procedure kod9(var B:Ar);
var
i: integer;
begin
     for i:=1 to N do
         if B[i].kod='9'then
            B[i].phone:='4'+B[i].phone;
end;
procedure PELET(var B:Ar);
var
i: integer;
begin
     for i:=1 to N do
              writeln(B[i].first_name,B[i].last_name:15, B[i].kod:5, B[i].mikud:5, B[i].zone:5, B[i].phone:10);
end;

begin
     clrscr;
     KELET(A);
     PELET(A);
     Del(A);
     kod9(A);
     PELET(A);
     readkey;
end.
