% Hila Shmuel 
% Guy Libzon 

a=wavread('lathe'); 
b=wavread('lathe-lpf');
s=snr(a,b); 
disp(s);




