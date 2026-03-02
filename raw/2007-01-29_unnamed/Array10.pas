program Ar6(input, output);{input: how many people in evey sadna.
                            output: the popular sadna number, the number of
                            people in the less popular sadna an the avarage
                            people for sandna}
uses crt;
const
S=15; {number of Sukot}
P=1000; {number of People}
Type
Ar= Array [1..S] of integer;
Var
Suka: Ar;
i: integer;
j: integer;
max: integer;
maxi: integer;
min: integer;
mini: integer;
begin
     clrscr;
     randomize;
     max:=0;
     min:=P;
     for i:=1 to S do
         Suka[i]:=0;
     for i:=1 to P do {for every person, ghose a suka}
           begin
                j:=random(S)+1;
                suka[j]:=suka[j]+1;
           end;
     j:=0;
     writeln('Suka number,     people choze it');
     for i:=1 to S do
         begin
              write(i,'                  ');
              writeln(Suka[i]);
              if Suka[i]>=max then {get the max number of people tht chose the suka}
                 begin
                      max:=Suka[i];
                      maxi:=i;
                 end;
              if Suka[i]<min then {get the min number of people in one sadna}
                 begin
                      min:=Suka[i];
                      mini:=i;
                 end;
              if Suka[i]>100 then
                 j:=j+1;
         end;
     writeln();
     writeln('1.The winning suka is suka number ', maxi,' it got ',max,' votes');
     writeln('2.sukas who got more than 100 votes:', j);
     writeln('3.The Suka who got the lowest rate of votes is suka number ', mini);
     readkey;
end.
