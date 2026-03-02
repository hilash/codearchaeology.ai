program listexe2;

uses list1, wincrt;

var
l:list_type;

procedure bulid_list(m: list_type);
var
p: pos_type;
y: list_info_type;
ans: char;
begin
     p:=list_anchor(m);
     writeln('do you want to add a number? y/n');
     readln(ans);
     while (ans='y') do
           begin
                writeln('enter number:');
                readln(y);
                list_insert (m,p,y);
                writeln('do you want to add a number? y/n');
                readln(ans);
           end;
end;

function mean_list(m: list_type):real;
var
p: pos_type;
y: list_info_type;
counter,sum: integer;

begin
     counter:=0;
     sum:=0;
     p:=list_anchor(m);
     p:=list_next(m,p);
     while (p<>nil) do
           begin
                list_retrieve (m,p,y);
                counter:=counter+1;
                sum:=sum+y;
                p:=list_next(m,p);
           end;
    mean_list:=sum / counter;
end;

begin
     list_init (l);
     bulid_list(l);
     writeln('the mean in the list: ',mean_list(l):0:3);
     readkey;
end.
