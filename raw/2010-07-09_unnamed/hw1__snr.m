function s=snr(in_vec,out_vec)
c=wavread(in_vec); %קורא את האות
d=sum(c.*c);%החישוב נעשה על פי מה שנלמד בכיתה וניתן בחומר רקע

a=wavread(out_vec);
b=c-a;
e=sum(b.*b);
f=d/e;
g=log10(f);
g=g*10