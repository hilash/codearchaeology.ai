program listexe4;

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

procedure print_list(m: list_type);
var
p: pos_type;
y: list_info_type;
begin
     p:=list_anchor(m);
     p:=list_next(m,p);
     while (p<>nil) do
           begin
                list_retrieve ( m,p,y);
                writeln(y);
                p:=list_next(m,p);
           end;
end;


procedure swap_list(m: list_type);
var
p,q: pos_type;
a,b: list_info_type;
begin
     p:=list_anchor(m);
     p:=list_next(m,p);
     q:=list_anchor(m);
     while (q<>nil) do
           q:=list_next(m,q);
     q:=list_prev(m,q); {now q is the last cell}

     while ( (p<>q) and (list_next(m,q)<>p) and (list_prev(m,p)<>q))  do
           begin
                list_retrieve ( m,p,a);
                list_retrieve ( m,q,b);
                list_update (m,p,b);
                list_update (m,q,a);
                p:=list_next(m,p);
                q:=list_prev (m,q);
           end;
end;


begin
     list_init (l);
     bulid_list(l);
      writeln('the list before the swap:');
     print_list(l);
     swap_list(l);
     writeln('the list after the swap:');
     print_list(l);
     readkey;
end.
