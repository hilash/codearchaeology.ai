%% Netwon method: $x_{0}=b,\quad x_{k+1}=x_{k}-\frac{f\left(x_{k}\right)}{f^{'}\left(x_{k}\right)}$

% syms f(x)
% f(x) = sin(x^2)
% [root,i,x,error]=newton(f,1,3 ,10^(-14),20);

function [root,i,x,error]=newton(f,a,b,tol,n)
% NEWTON calculates the root of function f.
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

    x=zeros(1,n+1);
    error=zeros(1,n+1);
    i=0;
    root = NaN;
    x(1)=b;
    roots = fsolve(matlabFunction(f),b,optimoptions('fsolve','Display','none','TolX',tol));
    roots = roots(imag(roots)==0);
    root = roots(a<=roots & roots<=b);
    if size(root) ~= 1
        disp ('no single root at given range.');
        return
    end
    df = diff(f);
    for i=2:n+1
        x(i) = x(i-1) - (f(x(i-1)))/(df(x(i-1)));
        error(i)=abs(x(i)-root);
        if error(i) < tol || f(x(i)) == f(x(i-1))
            disp (['Netown method converge to the root: ' , num2str(x(i)), ' after ', num2str(i-1), ' steps']);
            x = x(1:i);
            error = error(1:i);
            return
        end
    end
    disp (['Netown method did not converge to the root: ' , num2str(root), ' after ', num2str(i-1), ' steps']);
end