import copy

#read
lines = open(r"C:\Python27\Scripts\p099_base_exp.txt").readlines()
base_exp = [[int(i) for i in line.strip().split(',')] for line in lines]
base_exp_orig = [[int(i) for i in line.strip().split(',')] for line in lines]
all_numbers = [item for sublist in base_exp for item in sublist]
N = max_number = max(all_numbers)

#prime sieve + decomposition
numbers_decoposition = dict((k, []) for k in all_numbers)
sieve = [0] * (N+1)
primes = [2]
for i in xrange(3,N+1,2):
    if sieve[i] == 0:
        sieve[i] = 1
        primes.append(i)
        for j in xrange(i, N+1, i):
            sieve[j]=1
            if j in numbers_decoposition:
                numbers_decoposition[j].append(i)

print "done"


def isAbiggerthanB(A,B):

    a = copy.deepcopy(A)
    b = copy.deepcopy(B)
    while (True):

        #print "a:",a, "b:", b
        if (a[1] <= 10 and b[1] <=10):
            return a[0]**a[1] > b[0]**b[1]

        if (a[1] == 0 or b[1] == 0):
            return a[0]**a[1] > b[0]**b[1]
        
        # a[0]**a[1] ==?== b[0]**b[1]
        if (a[1] > b[1]):
            c = a[1] - b[1]
            # a[0]**(b[1]+c) ==?== b[0]**b[1]
            # a[0]**(c) ==?== (b[0]/a[0])**b[1]
            a[1]=c
            b[0]=b[0]/float(a[0])
            continue

        else:
             c = b[1] - a[1]
             # a[0]**(a[1]) ==?== b[0]**(a[1]+c)
             # (a[0]/b[0])**(a[1]) ==?== b[0]**(c)
             #return isAbiggerthanB(((a[0]/float(b[0])),a[1]),((b[0]),c))
             a[0]=a[0]/float(b[0])
             b[1]=c
             continue


a = base_exp[0]
b = base_exp[1]
print isAbiggerthanB(a,b)

while len(base_exp) > 1:

    print "="*80
    print "a:",a, "b:", b
    if isAbiggerthanB(base_exp[0],base_exp[1]):
        base_exp.pop(1)
    else:
        base_exp.pop(0)
    print "="*20, "size:", len(base_exp)

print "the winner is:", base_exp,"in index:", base_exp_orig.index(base_exp[0])
