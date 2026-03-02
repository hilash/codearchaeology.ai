a=wavread('lathe.wav')
%החזאי הראשון

b=dpcm(a)
c=avg(a)


d=distortion(b)
e=distortion(c)

scale=[1:100]
f=plot(scale,a,scale,d,scale,e)%מציג את שלושת הגלים שקיבלנו