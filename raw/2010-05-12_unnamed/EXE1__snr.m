% Hila Shmuel 
% Guy Libzon 

function s=snr(in_vec, out_vec)
sgnl=in_vec;
nois=out_vec-in_vec;
s=10*(log10((sum(sgnl.*sgnl))/(sum(nois.*nois))))
end

