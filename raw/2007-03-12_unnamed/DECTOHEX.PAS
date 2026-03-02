program exe;
uses crt;
Const
N=2147483647;
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
l,b: integer;
begin
     flag:=true;
     str(Hex, HexStr);
     str(Dec, DecStr);
     if length(HexStr)<>length(DecStr) then
        flag:=false;
     for l:=1 to length(HexStr) do
         begin
              b:=pos(HexStr[l], DecStr);
              if b>0 then
                 begin
                      HexStr[l]:='*';
                      DecStr[b]:='*';
                 end;
              if b=0 then
                  flag:=false;
         end;
     if flag=false then
        SameDigits:=false
     else
         SameDigits:=true;
end;
procedure Print(Dec: longint);
var
num: longint;
ten: longint;
begin
     ten:=1;
     num:=0;
     textcolor(3);
     write('division':10);
     write('div':10);
     writeln('mod':10);
     WRITELN('-----------------------------------');
     while ((Dec div 16)>=0) and (Dec<>0) do
           begin
                write(Dec:10,'/16');
                write((Dec div 16):10);
                writeln((Dec mod 16):10);
                num:=num+(Dec mod 16)*ten;
                ten:=ten*10;
                Dec:=Dec div 16;
           end;
end;
BEGIN
     clrscr;
     for i:=1 to n do
         begin
              Hex:=DecToHex(i);
              if(Hex<>-1) and (SameDigits(i,Hex)=True) then
                          begin
                               textcolor(6);
                               WRITELN(i,'   ------------->  ',Hex);
                               textcolor(3);
                               WRITELN('-----------------------------------');
                               Print(i);
                               WRITELN('-----------------------------------');
                               WRITELN();
                               WRITELN();
                          end;
       end;
     readkey;
END.

