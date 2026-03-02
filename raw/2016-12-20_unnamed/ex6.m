N = 100000;
x = linspace(0,40,N);
figure;

C = [0.05, 2,50];

i = 1;
n = size(C);
n = n(2);
for C = C

    f = cos(x)+C.*sin(x)./x;
    subplot(2,n,i);
    plot(x./pi,f, 'DisplayName',sprintf('C=%0.2f',C));
    hold on;
    
    title(sprintf('Graph of f(X=ka), C=%0.2f',C));
    ax = gca;
    ax.XGrid = 'on';
    ax.YGrid = 'on';
    h1 = refline(0,1);
    h2 = refline(0,-1);
    h1.Color = 'r';
    h2.Color = 'r';
    xlabel('X/pi = ka/pi') % x-axis label
    ylabel('f(X=ka)') % y-axis label
    
    f(1,1)=f(1,2);
    onecrossindx = zerocross(f-1);
    minusonecrossindx = zerocross(f+1);
    cross_points = sort([onecrossindx, minusonecrossindx]);
    plot(x(cross_points)./pi,f(cross_points),'O', 'DisplayName',sprintf('C=%0.2f',C)) 

    
    % this is pairs of the valid energies ranges ( in x=ka terms)
    % x(cross_points)/pi
    hold on;
    subplot(2,n,i+3);
    hbar = 1;
    m = 1;
    energy = ((hbar^2)/(2*m))*x.^2;
    plot(x./pi,energy);
    hold on;
    
    %for each energy range, draw a reference line 
    for j=1:length(cross_points)
        ka = x(cross_points(j));
        h1 = refline(0,((hbar^2)/(2*m))*ka^2);
    end
    i = i+1;
end;


