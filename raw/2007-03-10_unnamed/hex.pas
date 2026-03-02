program exe;
uses crt;
Const
N=100;
Type
word= string[10];
var
Hex: longint;
i: longint;
function DecToHex(Dec: longint):longint; {take ten base return 16 base}
var
num: longint;
ten: longint;
flag:boolean;
begin
     ten:=1;
     num:=0;
     flag:=true;
     while ((Dec div 16)>=0) and (Dec<>0) do
           begin
                if (Dec mod 16)>9 then
                   flag:=false;
                num:=num+(Dec mod 16)*ten;
                ten:=ten*10;
                Dec:=Dec div 16;
           end;
     if flag=true then
        DecToHex:=num
     else
        DecToHex:=-1;
end;
function SameDigits(Dec, Hex: longint): boolean;
var
flag: boolean;
HexStr: word;
DecStr: word;
HexAr: Array[1..10] of integer;
DecAr: Array[1..10] of integer;
k,m,numi,kod: integer;
temp:string[1];
begin

end;
BEGIN
     clrscr;
     for i:=1 to n do
         begin
              Hex:=DecToHex(i);
              if(Hex<>-1) and (SameDigits(i,Hex)=True) then
                          WRITELN(i,'  ',Hex);
       end;
     readkey;
END.

