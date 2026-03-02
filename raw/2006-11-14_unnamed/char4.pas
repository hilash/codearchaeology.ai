program exe (input,output);
uses crt;
var
  a: char; {the operator}
begin
     clrscr;
     writeln('please enter a char that represent a guess in the SuperToTo file');
     readln(a);
     if (a<>'0') and (a<>'1') and (a<>'2') then{input control to check that the input is 1 or 2 or 0}
        writeln('You did not typed correct answer. Bye!!!')
     else
        writeln('the char is a ligal char in the superTOTO');
     readkey;
end.

