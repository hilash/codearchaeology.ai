function  s=avg(in_vec)%מחשב את האות אשר יחזיר החזאי השני.
b=length(in_vec)
a=[]
a(1)=in_vec(1)
a(2)=in_vec(2)
a(3)=in_vec(3)
for i=4:b
    a(i)=in_vec(i-1)+in_vec(i-2)+in_vec(i-3);%סכום של שלוש דגימות אחרונות
end
a./3 %ממוצע שלהן
