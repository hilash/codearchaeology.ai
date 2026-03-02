program exe (input,output);
uses crt;
var
pens: real;
shirt: real;
shoe: real;
sum: real;
begin
     clrscr;
     pens:=123.45;
     shirt:=99.99;
     shoe:=324.45;
     sum:=round(pens)+round(shirt)+round(shoe);
     writeln('If the seller rounds every product each, the final price will be ', sum:0:3);
     sum:=pens+shirt+shoe;
     sum:=round(sum);
     writeln('If the seller rounds the sum of all the prices of the products [without round them each], the final price will be ', sum:0:3);
     writeln('If the seller gives Ziva 4% discount, the final price will be ', (sum*96/100):0:3);
     readkey;
end.
