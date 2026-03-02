program stack_exe;

var
s: stack_type;


{  Exercise No. 3 - Print the stack }
{ complexity: O(stk)}
procedure print_stack (var stk: stack_type);
var
tmp: stack_type;
x: stack_info_type;
begin
     while (not stack_empty(stk)) do
           begin
                stack_pop(stk, x);
                writeln(x);
                stack_push(tmp, x);
           end;
     while (not stack_empty(tmp)) do
           begin
                stack_pop(tmp, x);
                stack_push(stk, x);
           end;
end;

{  Exercise No. 4 - Is in stack }
{ complexity: O(stk)}
function is_in_stack (var stk: stack_type, a: stack_info_type):boolean;
var
tmp: stack_type;
x: stack_info_type;
flag: boolean;
begin
     flag:=false;
     while ( (not stack_empty(stk)) and (flag=false) ) do
           begin
                stack_pop(stk, x);
                if (x=a) then
                   flag:=true;
                stack_push(tmp, x);
           end;
     while (not stack_empty(tmp)) do
           begin
                stack_pop(tmp, x);
                stack_push(stk, x);
           end;
     is_in_stack:=flag;
end;

{  Exercise No. 5 - extract from stack }
{ complexity: O(stk)}
procedure extract_stack (var stk: stack_type, a: stack_info_type);
var
tmp: stack_type;
x: stack_info_type;
flag: boolean;
begin
     flag:=false;
     while ( (not stack_empty(stk)) and (flag=false) ) do
           begin
                stack_pop(stk, x);
                if (x=a) then
                        flag:=true
                else
                    stack_push(tmp, x);
           end;
     while (not stack_empty(tmp)) do
           begin
                stack_pop(tmp, x);
                stack_push(stk, x);
           end;
end;



