N = 1000000;
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
    
    valid_points = []
    %for each energy range, draw a reference line 
    for j=1:2:length(cross_points)
        if j >= length(cross_points)
            break
        end
        ka = x(cross_points(j));
        h1 = refline(0,((hbar^2)/(2*m))*ka^2);
        %h1.Color = 'r';
        
        ka = x(cross_points(j+1));
        h2 = refline(0,((hbar^2)/(2*m))*ka^2);
        %h2.Color = 'r';
        
        disp(sprintf('cross points: %d %d', cross_points(j), cross_points(j+1)));
        valid_points = [valid_points, [cross_points(j):cross_points(j+1)]];
    end
    
    % show the valid energy levels
    plot(x(valid_points)./pi,energy(valid_points),'.r');
    title(sprintf('Valid Energies, C=%0.2f',C));
    xlabel('X/pi = ka/pi') % x-axis label
    ylabel('Energy = (h*k*a)^2/(2m), h=m=1') % y-axis label
    hold on;
    
    i = i+1;
end;


