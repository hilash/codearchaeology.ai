program Ar6(input, output);{input: how many people in evey sadna.
                            output: the popular sadna number, the number of
                            people in the less popular sadna an the avarage
                            people for sandna}
uses crt;
const
S=10; {number of Sadnaot}
W=800; {number of Workers}
Type
Ar= Array [1..S] of integer;
Var
Sadna: Ar;
i: integer;
j: integer;
max: integer;
maxi: integer;
min: integer;
begin
     clrscr;
     randomize;
     max:=0;
     min:=800;
     for i:=1 to S do
         Sadna[i]:=0;
     for i:=1 to W do {for every worker, get the sadna}
           begin
                j:=random(10)+1;
                Sadna[j]:=Sadna[j]+1;
           end;
     writeln('Sadna number,     people in sadna');
     for i:=1 to S do
         begin
              write(i,'                  ');
              writeln(Sadna[i]);
              if Sadna[i]>=max then {get the max number of people in one sadna}
                 begin
                      max:=Sadna[i];
                      maxi:=i;
                 end;
              if Sadna[i]<min then {get the min number of people in one sadna}
                 min:=Sadna[i];
         end;
     writeln();
     writeln('The popular sadna is sadna number ', maxi);
     writeln('The number of people in the less popular sadna is ', min);
     writeln('The avarge people for sadna is ', W/S:0:2);
     readkey;
end.
