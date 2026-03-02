from math import *
from decimal import *

N = 10**12
'''
getcontext().prec = 12*2+2


M =  292893220765
    #285730105831
# B1,2 = 0.5 *(1+ 2*R +- sqrt(1+8*R**2))

for R in xrange(M, N):
	s = Decimal(1+8*R**2).sqrt()
	print "s: ", s
	if Decimal(1+8*R**2).sqrt() % 1 == 0:
		print "OMgggggggG"
		B = 0.5 *(Decimal(1+ 2*R) + s)
		print "R: ", R, " B: ", B
		if B+R > N:
			print "OMG", B+R
'''

'''
Using Pell Equation: we want to find: 
1+8*R^2 = x^2 for some integer x

x^2 - 8*y^2 = 1

basic solution: n=8, x=3, y=1

X_k + Y_k*sqrt(n) = (x_1 + y_1*sqrt(n))^k

X_2 + Y_2*sqrt(n) = (x_1 + y_1*sqrt(n))^2
                  = x_1^2 + 2*x_1*y_1*sqrt(n) + n*y_1^2 
'''

n = 8
x1 = 3
y1 = 1

pells = [(x1, y1)]

for i in xrange(1,100):
	(xi,yi) = pells[i-1]
	X = x1*xi + n*y1*yi
	Y = x1*yi +   y1*xi
	pells.append((X,Y))
	print (X,Y)

	R = Y
	# sqrt(1+8*R**2)==X

	B = 0.5 *(1+ 2*R + X)
	print "R: ", R, " B: ", B
	if B+R > N:
		print "OMG", B+R

