# -*- coding: utf-8 -*-
"""
Spyder Editor

This is a temporary script file.
"""

'''
N = 6

#(hexa base, n) = number of new hexa added to this level in this base

total = []

for n in xrange(3,N+1):

    n_base = []
    
    for base in xrange(1, n/3+1):
        #number of new with base 1 is - type A
        n_base.append((n-2*base)-base+1)

    for base in xrange(2, n/3+1):
        #number of new with base 1 is - type b
        n_base.append((n-2*base)-base+1)
        
    total.append((n, n_base));

S = 0
for t in total:
    print t
    s = sum(t[1])
    S+=s

print S

'''

from Tkinter import *
from math import *

n = 6
edge = 100.0
height = cos(pi/6)*edge
total_heigth = n*height
total_width = n*edge

master = Tk()

w = Canvas(master, width=total_width, height=total_heigth)
w.pack()

start = [total_width/2, 0]
end   = [0, total_heigth]
for m in xrange(n):
    w.create_line(start, end)
    start[0]+= edge/2;
    start[1]+=height;
    end[0]+= edge;

start = [total_width/2, 0]
end   = [total_width, total_heigth]
for m in xrange(n):
    w.create_line(start, end)
    start[0]-= edge/2;
    start[1]+=height;
    end[0]-= edge;

start = [total_width/2 + edge/2, height]
end   = [total_width/2 - edge/2, height]
for m in xrange(n):
    w.create_line(start, end)
    start[0]+= edge/2;
    start[1]+=height;
    end[0]-= edge/2;
    end[1]+= height;


from Point import *
from math import *
from time import *
M = n

#span all latice points inorder to find all valid hexagons
# A = a(1/2 x + sqrt(3)/2 y)
# B = a x + 0 y

A = Point(-0.5, sqrt(3)/2)*edge
B = Point(1,0)*edge
POINT_SIZE = 3

for a in xrange (0,M+1):
    for b in xrange (0,a+1):
        p = A*a + B*b;
        p = p + Point(total_width/2, 0)
        length = p.length()
        
        if (a==1) and (b==1):
            
            #k =  A*3 + B*2;
            p_i = p
            p_0 = Point(total_width/2, 0); #p + k; # 
            
            print p_i
            max_y = min_y = p_i.y
            
            w.create_line(p_0.x, p_0.y, p_i.x, p_i.y, fill="#990000", width=3)
            for i in xrange(1,6):
                p_f = p_i.rotate_about_me(p_0, -pi/3.0);
    
                fill = "#" + (hex(int(256*a*b/((M*M))))[2:]).zfill(6)
                #w.create_oval(p.x-POINT_SIZE, p.y-POINT_SIZE, p.x+POINT_SIZE, p.y+POINT_SIZE, fill="#476042")
                #w.create_line(p_i.x, p_i.y, p_f.x, p_f.y, fill="#009900", width=3)   
                
                p_0 = p_i
                p_i = p_f 
                
                max_y = max(p_f.y,max_y)
                min_y = min(p_f.y,min_y)

                
                print p_f
                
            print "max, min", max_y, min_y
            diff = (2.0*((max_y - min_y)/sqrt(3)))/edge
            hexagon_height = int(round(diff))
            print "diff", diff
            print "hexagon_height", hexagon_height

        #print p
        
print "done"
#mainloop()

