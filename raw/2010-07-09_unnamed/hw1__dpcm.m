function d=dpcm(in_vec)

%תחילה נקודד. נקבע כי יש 2 ביט, כלומר 4 רמות ייצוג

a=max(in_vec);
b=min(in_vec);
delta=a-b;
delta=delta/4;

c=[];
c(1)=in_vec(1);
len=length(in_vec);
for i=2:len
    c(i)=in_vec(i)-in_vec(i-1);
end


c./4.000001; %נועד בשביל למנוע לולאת פור ארוכה. לחלופין ניתן היה לרוץ ולמצוא מהו הטווח האופטימלי
e=floor(c);

%שיחזור האות
f=[];
f(1)=in_vec(1); %מניחים שנשלח גם הדלתה וגם האות הראשוני
for i=2:len
    f(i)=f(i-1)+c(i)*delta;
end


