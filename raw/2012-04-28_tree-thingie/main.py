from tree import *

LEAF_RESULT_INDEX = 0
LEAF_EXCEPTION_INDEX = 1

eqs = Root([     
                Add([
                        One(3),
                        Two(4),
                        One(5),
                        ], 2)
                
                
                ], 1)
                           
def do_multi(multi):
    g = multi.gen()
    res = (None, None)
    while True:
        try:
            eq = g.send(res[LEAF_RESULT_INDEX])
        except MultiFailure, e:
            return (None, e)
        except StopIteration:
            break
        print '[#%d]' % eq.id, ' now..'
        if isinstance(eq, Multi):
            res = do_multi(eq)
        else:
            res = do_single(eq)
        
        if res[LEAF_RESULT_INDEX]:
            print '[#%d]' % eq.id, ' succeeded!' 
        else:
            print '[#%d]' % eq.id, ' failed! Reason:', res[1]
    
    return (True, None)
        
def do_single(eq):
    print '----- pre leaf -----'
    try:
        res = (eq.calc(), None)
    except Failure, e:
        res = (None, e)
    except: 
        res = (None, None)
        print 'Unknown exception in equation #%d:' % eq.id
        traceback.print_stack()
        
    return res

def main():
    do_multi(eqs)
        
if __name__ == '__main__':
    main()