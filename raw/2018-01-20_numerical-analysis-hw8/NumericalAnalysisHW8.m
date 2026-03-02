format compact;

%% Question 2 - matlab

clear all
N = 70;
x0 =1;
exact = 9*x0^8;
h = 5*10.^(-(1:N));
df_dx = ((x0+h).^9-x0^9)./h;
Er = abs(df_dx-exact);
subplot(2,1,1)
loglog(h,Er,'-',h,Er,'*');
axis([1e-18 1 5e-8 1e2]);
text(0.75,-0.2,'h','Units','normalized');
ylabel('|error|');
set(gca,'Xtick',[1e-17 1e-13 1e-8 1],'Ytick',[1e-7 1e-3 1e1 1e2]);
title('Error of forward differencing formula'); shg
legend('Error line', 'Error');



%% Error Estimation

% The same calculation with more points
h = logspace(-25,0,50);
df_dx = ((x0+h).^9-x0^9)./h;
Er = abs(df_dx-exact);
subplot(2,1,2)
loglog(h,Er,'-',h,Er,'*');
title('Error (real and theoretical) of forward differencing formula');

% find what is h_opt
[min_e, min_index] = min(Er);
h_opt = h(min_index);
disp(['The h that results the minimum value is h_opt: ', num2str(h_opt)]);
disp(['And the minial Error is: ', num2str(min_e)]);

hold on;

% Show the theoretical Error
Theory_Error = abs(36*((h_opt^2)./h-h));
plot(h,Theory_Error, '-','LineWidth',2);
hold on;
plot(h,Theory_Error, '*');
hold on;
legend('Error line', 'Error','Theoretical Error line','Theoretical Error');
ylabel('|error|');
xlabel('h');
