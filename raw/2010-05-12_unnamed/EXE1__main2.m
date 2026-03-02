% Hila Shmuel 
% Guy Libzon 

original=wavread('lathe');

gaus0=wavread('lathe0');
s=snr(original,gaus0); 
disp(s);

gaus5=wavread('lathe5');
s=snr(original,gaus5); 
disp(s);

gaus15=wavread('lathe15');
s=snr(original,gaus15); 
disp(s);