program exe (input,output);
uses crt;
var
c1: char;
c2: char;
c3: char;
begin
     randomize;
     clrscr;
     c1:=chr(random(57-48+1)+48); {get a random chare between 0 to 9}
     c2:=chr(random(57-48+1)+48);
     c3:=chr(random(57-48+1)+48);
     writeln('the chars are ',c1,' ',c2,' ',c3);
     writeln('the sum is ',(ord(c1)-48)+(ord(c2)-48)+(ord(c3)-48));
     readkey;
end.

