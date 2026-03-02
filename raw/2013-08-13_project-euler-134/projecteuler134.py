import sys

N = 1000004
#N = 1000

def calc_primes(n):
    sieve = [0] * n 
    primes = [2]
    for i in xrange(3,n,2):
        if sieve[i] == 0:
            primes.append(i)
            for j in xrange( 3 * i, n, i):
                sieve[j] = 1

    del sieve
    return primes

def pairs(lst):
    i = iter(lst)
    first = prev = item = i.next()
    for item in i:
        yield prev, item
        prev = item
    #yield item, first    

def find_divisible_old(pair):
    p1, p2 = pair
    sp1 = str(p1)
    lenp1 = len(str(p1)) 
    ten = 10 ** lenp1 # 1219   19 2  100  1219 % 100 = 19 
    if lenp1 > 4:
	ten = 10 ** 4
    p14 = p1 % ( 10 ** 4)
    s = p2
    p22 = 2 * p2
    n = 1
    while True:
        if s == p14:
		t = 10 ** lenp1
		if p2 * n % t == p1:
			return p2 * n
        s = (s + p22) % ten
        n += 2

def get_mul_digit(a,b,c):
	# a * x + c = b
	for x in xrange(0, 10):
		if (a * x + c) % 10 == b:
			return x

def find_divisible(pair):
	p1, p2 = pair
	p2last = p2 % 10;
	p11 = p1
	x = []
	res = 0
	while p11 != 0:
		last_digit = p11 % 10
		#p2last * ? = last_digit
		xdigit  = get_mul_digit(p2last, last_digit, res % 10)
		x.append(xdigit)
		res += p2 * xdigit;
		res /= 10;  # 1253 % 10 = 125
		p11 /= 10 # 17
	return int(''.join(map(str,x[::-1]))) 

primes = calc_primes(N)
# delete 2 and 3
del primes[0]
del primes[0]
S = 0
for pair in pairs(primes):
    s = find_divisible(pair) 
    print pair, s, s * pair[1],  S
    S += s * pair[1]
print S
