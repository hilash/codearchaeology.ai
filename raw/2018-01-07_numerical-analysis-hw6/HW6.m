syms X;
functions = {symfun(-1 -X^3, X),
             symfun(-1/(1+X^2), X),
             symfun(-(X^2+X)/(X^2+X+1), X),
             symfun(-(1+X)^(1/3), X)}

for k=1:length(functions)
    f = functions{k};
    df = diff(f);

    x = linspace(-1,1,1000);
    subplot(1,2,1);
    plot(x,f(x),'LineWidth',2,'DisplayName',num2str(k));
    title('g(x)')
    xlabel('x')
    ylabel('g(x)')
    legend('show');
    legend('Location','southwest');
    hold on;
    
    x = linspace(-0.99,0.99,1000);
    subplot(1,2,2);
    plot(x, df(x),'LineWidth',2,'DisplayName',num2str(k));
    title('dg(x)/dx')
    xlabel('x')
    ylabel('dg(x)/dx')
    legend('show');
    legend('Location','southwest');
    hold on;
end
