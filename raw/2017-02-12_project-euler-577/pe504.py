
import math
'''



           B(0,b)
              |
              |
              |
 C(-c,0)------+-------- A(a,0)
              |
              |
              |
            D(0,-d)


'''

M = 4 


def main():
    print "Hila"

    number_of_lattice = [[0 for x in range(M+1)] for x in range(M+1)]
    
    # number of lattice points in AOB (not include axis points)
    for a in xrange(1, M+1):
        for b in xrange(1,M+1):
            '''
           B(0,b)
              |
              |
              |
              +-------- A(a,0)

            '''
            da =float(a)
            db = float(b)
            for y in xrange(1, b + 1):
                #y = (db)/(-da) * x + b
                x = (db - y) * da + db
                number_of_lattice[a][b] += int(math.floor(x))
                number_of_lattice[b][a] += int(math.floor(x))



    for a in xrange(1,M+1):
        for b in xrange(1,M+1):
            aob = number_of_lattice[a][b]
            for c in xrange(1,M+1):
                abc = number_of_lattice[b][c] + aob
                for d in xrange(1,M+1):
                    abcd = abc + number_of_lattice[d][c] + number_of_lattice[d][a] + a + b + c + d + 1
                    print "a,c,b,c:", a,b,c,d, "res:", abcd
                    
if __name__=='__main__':
    main()
