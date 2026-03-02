program listexe3;

uses list1, wincrt;

var
l,t:list_type;

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

procedure bulid_mean_list(old, neww: list_type);
var
p,q: pos_type;
y: list_info_type;
mean: real;
begin
     mean:=mean_list(old);
     p:=list_anchor(old);
     q:=list_anchor(neww);

     p:=list_next(old,p);
     while (p<>nil) do
           begin
                list_retrieve (old,p,y);
                if y>mean then
                   list_insert (neww,q,y);
                p:=list_next(old,p);
           end;
end;

begin
     list_init (l);
     list_init (t);
     bulid_list(l);
      bulid_mean_list(l,t);
     writeln('the mean in the old list: ',mean_list(l):0:3);
      writeln('the mean in the new list: ',mean_list(t):0:3);
     readkey;
end.
