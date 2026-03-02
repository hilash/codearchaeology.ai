{This program gets a string and print it in nice way}

program exe(input, output);
uses crt;
var
   A: array[1..26,1..5] of string;
   word: string;
   wordlong: integer;
   letter1: char;
   K, M, num: integer; {HELP VARIBALS }

Function LetterToNumber(Letter : char) : Integer;
var
   number: integer;
Begin
     if 'A'=Letter then
        number:=1
     else
         if 'B'=Letter then
        number:=2
     else
         if 'C'=Letter then
        number:=3
     else
         if 'D'=Letter then
        number:=4
     else
         if 'E'=Letter then
        number:=5
     else
         if 'F'=Letter then
        number:=6
     else
         if 'G'=Letter then
        number:=7
     else
         if 'H'=Letter then
        number:=8
     else
         if 'I'=Letter then
        number:=9
     else
         if 'J'=Letter then
        number:=10
     else
         if 'K'=Letter then
        number:=11
     else
         if 'L'=Letter then
        number:=12
     else
         if 'M'=Letter then
        number:=13;
     LetterToNumber:=number;
End;

begin
     clrscr;
     {Define the strings for each letter}
     {A}
     A[1,1]:='   ';
     A[1,2]:='  ';
     A[1,3]:=' ';
     A[1,4]:='  ';
     A[1,5]:='  ';
     {B}
     A[2,1]:=' ';
     A[2,2]:='  ';
     A[2,3]:='  ';
     A[2,4]:='  ';
     A[2,5]:=' ';
     {C}
     A[3,1]:='  ';
     A[3,2]:='   ';
     A[3,3]:='    ';
     A[3,4]:='   ';
     A[3,5]:='  ';
     {D}
     A[4,1]:='  ';
     A[4,2]:='  ';
     A[4,3]:='  ';
     A[4,4]:='  ';
     A[4,5]:='  ';
     {E}
     A[5,1]:=' ';
     A[5,2]:='    ';
     A[5,3]:=' ';
     A[5,4]:='    ';
     A[5,5]:=' ';
     {F}
     A[6,1]:=' ';
     A[6,2]:='    ';
     A[6,3]:=' ';
     A[6,4]:='    ';
     A[6,5]:='    ';
     {G}
     A[7,1]:='  ';
     A[7,2]:='   ';
     A[7,3]:='  ';
     A[7,4]:='  ';
     A[7,5]:='  ';
     {H}
     A[8,1]:='  ';
     A[8,2]:='  ';
     A[8,3]:=' ';
     A[8,4]:='  ';
     A[8,5]:='  ';
     {I}
     A[9,1]:=' ';
     A[9,2]:='   ';
     A[9,3]:='   ';
     A[9,4]:='   ';
     A[9,5]:=' ';
     {J}
     A[10,1]:='  ';
     A[10,2]:='    ';
     A[10,3]:='    ';
     A[10,4]:='   ';
     A[10,5]:='  ';
     {K}
     A[11,1]:='  ';
     A[11,2]:='  ';
     A[11,3]:='  ';
     A[11,4]:='  ';
     A[11,5]:='  ';
     {L}
     A[12,1]:='    ';
     A[12,2]:='    ';
     A[12,3]:='    ';
     A[12,4]:=' ';
     A[12,5]:=' ';
     {M}
     A[13,1]:=' ';
     A[13,2]:=' ';
     A[13,3]:='   ';
     A[13,4]:='    ';
     A[13,5]:='    ';

     {Ask the user to enter a string}
     writeln('Plz enter a word!');
     readln(word);
     wordlong:=length(word);{find out the string length}
     For K:=1 to 5 Do
         begin
              M:=1;
              while M<(wordlong+1) do
                    begin
                         letter1:=word[M];
                         num:=LetterToNumber(letter1);
                         Write(A[num,K]);
                         M:=M+1;
                    end;
              writeln;
         end;
     readkey;
end.



