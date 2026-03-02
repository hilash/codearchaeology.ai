%% Bisection method: $f\left(a\right)\cdot f\left(b\right)<0 ,$ $c=\frac{a+b}{2}$

% syms f(x)
% f(x) = sin(x^2)
% [root,i,x,error]=bisection(f,1,3 ,10^(-14),20);

function [root,i,c,error]=bisection(f,a,b,tol,n)
% BISECTION calculates the root of function f.
% INPUT
%       f     - function. definded by: syms f(x); f(x) = sin(x^2)
%       a,b   - function range
%       tol   - maximal error for convergence (tolerance)
%       n     - maximal number of iterations
% OUTPUT
%       x    - iterations values
%       error- iterations errors values
%       i    - number of iterations for convergence
%       root - the real root, calculated by fsolve.


    c=zeros(1,n);
    error=zeros(1,n);
    i=0;
    root = NaN;
        
    roots = fsolve(matlabFunction(f),b,optimoptions('fsolve','Display','none','TolX',tol));
    roots = roots(imag(roots)==0);
    root = roots(a<=roots & roots<=b);
    if size(root) ~= 1
        disp ('no single root at given range.');
        return
    end
    
        
    if b <= a
        disp (['Bisection method failes: b>=a']);
        return
    elseif f(a) == 0
        i=1
        c(1) = a
        disp (['Bisection successeded: root is a']);
        return
    elseif f(b) == 0
        i=1
        c(1) = b
        disp (['Bisection successeded: root is b']);
        return
    end
    
    if f(a)*f(b) > 0
        disp (['Bisection method failes: f(a)*f(b) > 0. please choose other values.']);
        return
    end
            
    for i=1:n
        c(i)= (a+b)/2;
        error(i)=abs(c(i)-root);
        
        if f(c(i))*f(a) < 0
            b = c(i);
        else
            a = c(i);
        end
        if error(i) < tol
            disp (['Bisection method converge to the root: ' , num2str(c(i)), ' after ', num2str(i), ' steps']);
            c = c(1:i);
            error = error(1:i);
            return
        end
    end
    disp (['Bisection method did not converge to the root: ' , num2str(root), ' after ', num2str(i), ' steps']);
end