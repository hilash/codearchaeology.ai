program exe (input,output);
uses crt;
var
c1: char;
c2: char;
c3: char;
c4: char;
counter: integer;
begin
     clrscr;
     writeln('please enter 4 chars [BIG ENGLISH LETTERS]:');
     read(c1);
     read(c2);
     read(c3);
     readln(c4);
     if (ord(c1)<65) or (ord(c1)>90)then {input control to check that the chars are BIG ENGLISH LETTERS}
            writeln('You did not typed BIG ENGLISH LETTERS. Bye!!!')
     else
          if (ord(c2)<65) or (ord(c2)>90)then {input control to check that the chars are BIG ENGLISH LETTERS}
            writeln('You did not typed BIG ENGLISH LETTERS. Bye!!!')
     else
          if (ord(c3)<65) or (ord(c3)>90)then {input control to check that the chars are BIG ENGLISH LETTERS}
            writeln('You did not typed BIG ENGLISH LETTERS. Bye!!!')
     else
          if (ord(c4)<65) or (ord(c4)>90)then {input control to check that the chars are BIG ENGLISH LETTERS}
            writeln('You did not typed BIG ENGLISH LETTERS. Bye!!!')
     else
         begin
              counter:=0;
              if c1='A' then {if the char is A BIG A}
                 counter:=counter+1;
              if c2='A' then {if the char is A BIG A}
                 counter:=counter+1;
              if c3='A' then {if the char is A BIG A}
                 counter:=counter+1;
              if c4='A' then {if the char is A BIG A}
                 counter:=counter+1;
              writeln('there is ',counter,' chars of the type of BIG A')
         end;
     readkey;
end.

