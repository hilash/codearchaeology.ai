program listexe6;

uses list2, wincrt;

var
l:list_type;

procedure bulid_list(m: list_type);
var
p: pos_type;
y: list_info_type;
ans: char;
begin
     p:=list_anchor(m);
     writeln('do you want to add a student? y/n');
     readln(ans);
     while (ans='y') do
           begin
                writeln('enter student name and grade:');
                readln(y.name);
                readln(y.grade);
                list_insert (m,p,y);
                writeln('do you want to add a student? y/n');
                readln(ans);
           end;
end;




function max_grade(m: list_type):integer;
var
p: pos_type;
y:list_info_type;
max: integer;
begin
     max:=0;
     p:=list_anchor(m);
     while (p<>nil) do
           begin
                list_retrieve (m,p,y);
                if (y.grade>max) then
                   max:=y.grade;
                p:=list_next(m,p);
           end;
    max_grade:=max;
end;

procedure delete_max_grade(m: list_type);
var
p: pos_type;
y:list_info_type;
max: integer;
begin
     max:=max_grade(m);
     p:=list_anchor(m);
     while (p<>nil) do
           begin
                list_retrieve (m,p,y);
                if (y.grade=max) then
                   list_delete(m,p);
                p:=list_next(m,p);
           end;
end;
begin
     list_init (l);
     bulid_list(l);
     writeln('max grade is: ',max_grade(l));
     delete_max_grade(l);
     writeln('max grade is: ',max_grade(l));
     readkey;
end.
