%% HW5

% print data nicely
format long;
format compact;

%% (a) Solve the equation $x^5-2x-10=0$
%  The real root in [1,3] is 1.6794
p = [1 0 0 0 -2 -10];
r = roots(p)
root = r(imag(r)==0)

%% (b) Solve the equation $f=x^5-2x-10=0$ using  the Newton, Secant, and Bisection methods
a=1;
b=3;
iterations_num=20;
tol=10^(-14);
syms f(x);
f(x) = x^5-2*x-10;

%% (b.1) Newton method: $x_{0}=b,\quad x_{k+1}=x_{k}-\frac{f\left(x_{k}\right)}{f^{'}\left(x_{k}\right)}=\frac{4x_{k}^{5}+10}{5x_{k}^{4}-2}$
[root,i,x,error]=newton(f,a,b,tol,iterations_num)

%% (b.2) Secant method: $x_{0}=b,\quad x_{k+1}=x_{k}-f\left(x_{k}\right)\frac{x_{k}-x_{k-1}}{f\left(x_{k}\right)-f\left(x_{k-1}\right)}=x_{k}-\left(x_{k}^{5}-2x_{k}-10\right)\frac{x_{k}-x_{k-1}}{x_{k}^{5}-x_{k-1}^{5}-2x_{k}+2x_{k-1}}$
[root,i,x,error]=secant(f,a,b,a,b, tol,iterations_num)


%% (b.3) Bisection method: $f\left(a\right)\cdot f\left(b\right)<0 ,$ $c=\frac{a+b}{2}$
[root,i,x,error]=bisection(f,a,b, tol,iterations_num)

%% (b.4) - tests, verify that the code implments the algorithm correctly
% test 1 - check converge to root 1
a=0;
b=1.1;
iterations_num=100;
tol=10^(-14);
syms f(x);
f(x) = (x-1)*(x-2)*(x-3)*(x-4)*(x-5);
newton(f,a,b,tol,iterations_num);
secant(f,a,b,a,b, tol,iterations_num);
bisection(f,a,b, tol,iterations_num);

%%
% test 2- the bisection did not converge because of the problem of subtracting
% close number. so - let's take a more tolerace tolerace. 
% (we take 10^(-6) which is the standart matlab tolerance, (as in 'fsolve')

tol = 10^(-6);
iterations_num=20;
newton(f,a,b,tol,iterations_num);
secant(f,a,b,a,b, tol,iterations_num);
bisection(f,a,b, tol,iterations_num);


%%
% test 3 - check what happens if (a,b) includes more than one root.
%          we should expect inconclusive results
a = 1.7;
b = 3.2;
newton(f,a,b,tol,iterations_num);
secant(f,a,b,a,b, tol,iterations_num);
bisection(f,a,b, tol,iterations_num);


%%
% test 4 - check what happens if (a,b) is really close to root
a = 2.9;
b = 3.1;
tol = 10^(-14); 
newton(f,a,b,tol,iterations_num);
secant(f,a,b,a,b, tol,iterations_num);
bisection(f,a,b, tol,iterations_num);


%%
% test 5 - check a function that around 0, very different than
%          it's behavior elsewhere. newton and secant would
%           divide by zero (the slop of the direvative of tanh at 0).
a = -1;
b = 1;
tol = 10^(-14); 
f(x) = tanh(5*x);
try
    newton(f,a,b,tol,iterations_num);
catch ME
    warning(getReport(ME));
end
try
    secant(f,a,b,a,b, tol,iterations_num);
catch ME
    warning(getReport(ME));
end

bisection(f,a,b, tol,iterations_num);


%% (c) The convergence rate of the iterations for solving the equation
%           $f=x^5-2x-10=0$ using  the Newton, Secant, and Bisection
%           methods.
a=1;
b=3;
iterations_num=20;
tol=10^(-14);
syms f(x);
f(x) = x^5-2*x-10;

% calculate the roots using all methods
[root,i,x,error]=newton(f,a,b,tol,iterations_num);
error1 = error(2:end);
[root,i,x,error]=secant(f,a,b,a,b, tol,iterations_num);
error2 = error(3:end);
[root,i,x,error]=bisection(f,a,b, tol,iterations_num);
error3 = error;

Errors = {error1, error2, error3};
Titles = {'Newton', 'Secant', 'Bisection'};

figure('position', [0, 0, 1000, 800]);

for i=1:3
    
    error = Errors{i};
    x_n = log(error(1:end-1));
    y_n = log(error(2:end));
    x1 = linspace(min(x_n),max(x_n));

    subplot(2,2,i);
    
    % calculate fit - only for error ranges
    disp(['Linear fit of the error convergence of the ', Titles{i}, ' method']);
    f = fit(x_n',y_n','poly1')
    
    % we can use 'fit', but we'll use 'polyfit' and 'polyval' as requested
    p = polyfit(x_n,y_n,1);
    f1 = polyval(p,x1);
    plot(x_n,y_n,'o',x1,f1,'-');

    title(sprintf('Error convergence of %s method', Titles{i}));
    ylabel('$$\log E_{n+1}$$', 'Interpreter', 'Latex');
    xlabel('$$\log E_{n}$$'  ,'Interpreter', 'Latex');
    axis tight;
    legend('data',sprintf('fit: y=%f*x+%f',p(1),p(2)));
    legend('Location','northwest');

end
