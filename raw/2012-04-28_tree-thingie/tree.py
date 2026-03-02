MAG = 1337
BAD = 0xdead

import traceback

###
# Generic tree structure
###

class MultiFailure(Exception):
    pass
        
class Failure(Exception):
    pass

class Quit(Exception):
    pass
    
class Basic(object):
    def __init__(self, id):
        self.id = id
        
class Leaf(Basic):
    def __init__(self, id):
        Basic.__init__(self, id)
        
    def calc(self):
        tmp_res = self._calc()
        res = self._verify(tmp_res)
        return res
        
    def _verify(self, tmp_res):
        raise NotImplementedError
        
    def _calc(self):
        raise NotImplementedError
        
class Multi(Basic):
    def __init__(self, lst, id, stop_on_failure = True):
        self.lst = lst
        self.stop_on_failure = stop_on_failure
        Basic.__init__(self, id)

    def contained_failure(self):
        x = raw_input('Failure in #%d.. \'bye\' to quit >>' % self.id)
        if 'bye' == x:
            raise Quit
        else:
            pass
    
    def gen(self):
        results = []
        
        for i in self.lst:
            res = (yield i)
            results.append(res)
            if None == res:
                # All leaves must return a value. Otherwise, it's a failure..
                if self.stop_on_failure:
                    self.contained_failure()
        
        self._verify(results)
    
    def _verify(self, tmp_res):
        if not all(tmp_res):
            raise MultiFailure('Atleast 1 leaf failed')
            
class Root(Multi):
    def __init__(self, lst, id):
        Multi.__init__(self, lst, id, True)
    
####
#Test with tree of type: Equation
###

class One(Leaf):
    def __init__(self, id):
        Leaf.__init__(self, id)
        
    def _calc(self):
        print 'One'
    
    def _verify(self, res):
        return 1
        
class Two(Leaf):
    def __init__(self, id):
        Leaf.__init__(self, id)
        
    def _calc(self):
        print 'Two'
    
    def _verify(self, res):
        raise Failure('I forgot what is the number 2!')
        return 2
            
class Add(Multi):
    def __init__(self, lst, id):
        Multi.__init__(self, lst, id, stop_on_failure = True)

    def _verify(self, res):
        try:
            if sum(res) != 3:
                raise MultiFailure('Expected result of addition to be 3!')
        except:
            raise MultiFailure('sum() failed!')