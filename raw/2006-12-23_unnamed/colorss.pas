program Cacl(input, output); {this program is a math learning program to kids}
uses crt;
var
i,j,k: integer;{loops varibales}
op: char;{the opertator that the user choose}
movingline: integer;{there is a moving line in the program. if varible=1 the line moving, if varible=0, not}
input: char; {this is the main varible of the program - it sets the current condition of the program}
num1: integer;
num2: integer;
result: integer;
modresult: integer;
userresult: integer;
usermodresult: integer;
GoodAnswerCounter: integer; {counter - how many corect answer the user answered}
TenCounter: integer; {count the number of exercises that had already been given to the use}
runprog: integer; {while it's 1 -> the program runs, 0 -> the user don't want anymore exercise, the program stop}
begin
     runprog:=1;
     while runprog=1 do {loop that runs the game or stops it}
           begin
                TenCounter:=0;
                GoodAnswerCounter:=0;
                randomize;
                input:='0'; {when input='0', the start point of the program}
                movingline:=1;
                k:=12;
                while ((input<>'s') and (TenCounter<=10)) do  {the main loop - each time gives a different exercise, rums 10 times}
                           begin
                                textbackground(k);{k is the color that changes during the program}
                                textcolor(k);
                                {BASIC DESIGN - apper all the time during the running of the program }
                                for i:=1 to 2 do {two lines of *******}
                                    begin
                                         for j:=1 to 79 do
                                             Write('*');
                                         writeln('')
                                    end;
                                for i:=3 to 12 do  {10 empty lines}
                                    begin
                                         Write('***');
                                                      for j:=1 to 73 do
                                                          Write(' ');
                                         Writeln('***')
                                    end;
                                for i:=13 to 14 do {two lines of *******}
                                    begin
                                         for j:=1 to 79 do
                                             Write('*');
                                         writeln('')
                                    end;
                                for i:=14 to 22 do {9 empty lines}
                                    begin
                                         Write('***');
                                         for j:=1 to 73 do
                                             Write(' ');
                                         Writeln('***')
                                    end;
                                for i:=23 to 24 do {two lines of *****}
                                    begin
                                         for j:=1 to 79 do
                                             Write('*');
                                         writeln('')
                                    end;
                                {END of basic design}
                                if input='f' then {after the user finished all the 10 exercises}
                                   begin
                                        textcolor(k+1);
                                        gotoxy(12, 5);
                                        writeln('You Finished! your score is',GoodAnswerCounter*10,'   (of 100).');
                                        gotoxy(10,16);
                                        writeln('If you want to run the program again, Press 1');
                                        gotoxy(10,17);
                                        writeln('If you dont, press any key');
                                        gotoxy(10,18);
                                        write('NOW... what would you like to do?? ');
                                        readln(runprog);
                                        input:='s' {stop this loop, and go to the first loop of the program, there the program start over or finished}
                                   end;
                                if input='2' then  {After we got a good operator, this is the exercise part of the program}
                                   begin
                                        textcolor(k+1);
                                        gotoxy(12, 5);
                                        write(TenCounter,'.  ');{exercise number}
                                        {in this part, for each operator the program randomizes numbers that in the matching range}
                                        if op='*' then
                                           begin
                                                num1:=random(21);{we can get number in range 0-20}
                                                num2:=random(21);{we can get number in range 0-20}
                                                result:=num1*num2;
                                                writeln(num1, '*',num2,'=?')
                                           end
                                        else
                                            begin
                                                 num1:=random(101);{we can get number in range 0-100}
                                                  if op='/' then
                                                     begin
                                                          num2:=random(100)+1; {we can get number in range 1-100}
                                                          result:=num1 div num2;
                                                          modresult:=num1 mod num2;
                                                          writeln(num1, '/',num2,'=?')
                                                     end;
                                                  if op='+' then
                                                     begin
                                                          num2:=random(101-num1);{we can get number that num1+num2<=100, in range 0-(100-num1)}
                                                          result:=num1+num2;
                                                          writeln(num1, '+',num2,'=?')
                                                     end;
                                                  if op='-' then
                                                     begin
                                                          num2:=random(num1+1);{we can number between 0 to num1, so the result won't be negative}
                                                          result:=num1-num2;
                                                          writeln(num1, '-',num2,'=?')
                                                     end
                                            end;
                                        gotoxy(10,16);
                                        textcolor(k+2);
                                        Write('You have 3 trials to write the correct answer:');
                                        for i:=1 to 3 do  {the trials part - the user has 3 trials to write the corect answer}
                                            begin
                                                 gotoxy(11,16+i);
                                                 textcolor(k+2);
                                                 write(i,'. ');{trial number}
                                                 if op='/' then {if op='/' there is two results- one for the div, and second for the mod}
                                                    begin
                                                         write('div result: ');
                                                         readln(userresult);
                                                         gotoxy(30,16+i);
                                                         write(' ,mod result: ');
                                                         readln(usermodresult);
                                                         if (userresult=result) and (usermodresult=modresult) then {if the reaults are correct, we don't need more trials}
                                                            begin
                                                                 GoodAnswerCounter:=GoodAnswerCounter+1;
                                                                 i:=4
                                                            end
                                                         end
                                                 else  { if op IS NOT '/' there is only one results}
                                                       begin
                                                            readln(userresult);
                                                            if (userresult>100)
                                                            if userresult=result then  {if the reaults are correct, we don't need more trials}
                                                               begin
                                                                    GoodAnswerCounter:=GoodAnswerCounter+1;
                                                                    i:=4
                                                               end
                                                       end
                                            end;
                                        TenCounter:=TenCounter+1;
                                        if TenCounter=10 then {if we finishes the 10 circles}
                                           input:='f'{FInal mode}
                                   end;
                                if (input='0') or (input='1') then {SHOW ONLY IN THE BEGINING OR WHEN THE OPERATOR INPUT IS WRONG}
                                   begin
                                        gotoxy(12, 5); {nice WELCOME}
                                        writeln('*     *  ******  *       ******  ******  *******   ******');
                                        gotoxy(12, 6);
                                        writeln('*  *  *  *       *       *       *    *  *  *  *   *     ');
                                        gotoxy(12, 7);
                                        writeln('*  *  *  *****   *       *       *    *  *  *  *   ******');
                                        gotoxy(12, 8);
                                        writeln('*  *  *  *       *       *       *    *  *  *  *   *     ');
                                        gotoxy(12, 9);
                                        writeln('*******  ******  ******  ******  ******  *     *   ******');
                                        if movingline=1 then {a nice moving line, only once in the begining}
                                           begin
                                                for i:=13 to 35 do  {moving right}
                                                    begin
                                                         gotoxy(i,11);
                                                         textcolor(k+1);
                                                         Write(' welcome to the math learning program :-) ');
                                                         delay(100)
                                                    end;
                                                for i:=35 downto 13 do {moving left}
                                                    begin
                                                         gotoxy(i,11);
                                                         Write(' welcome to the math learning program :-) ');
                                                         delay(100)
                                                    end;
                                                movingline:=0 {so the line will be moving only once}
                                           end;
                                        gotoxy(13,11);
                                        textcolor(k+1);
                                        Write(' welcome to the math learning program :-) ');
                                        if input='1' then {if the operator that was inserted before is worng}
                                           begin
                                                gotoxy(13,17);
                                                textcolor(k);
                                                write('Wrong input. please try again')
                                           end;
                                        gotoxy(13,18);
                                        textcolor(k+2);
                                        Write(' You can chose one of those fore operators:');
                                        textcolor(k+3);
                                        gotoxy(23,19);
                                        Write('  +    -    *    /');
                                        gotoxy(13,21);
                                        textcolor(k+2);
                                        Write(' Plese enter the operator you would like to use: ');
                                        readln(op);
                                        if (op<>'+') and (op<>'-') and (op<>'*') and (op<>'/') then {if the input is wormg the program ask to insert correct op again}
                                           input:='1';
                                        if (op='+') or (op='-') or (op='*') or (op='/') then {if the operator is correct we go to the exercise part}
                                           input:='2'
                                   end;
                                clrscr;
                                k:=k+1; {chabge of the color}
                                if (k=15) then {in case that the color is black}
                                   k:=9
                           end
           end;
           readkey;
     end.
