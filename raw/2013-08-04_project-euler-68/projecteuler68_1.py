import sys
import itertools

N = 10 
R = 3
SUM = 16
C = 5 
trios = []
circls = []
valid_circls = []
valid_rings = []

def summ(m):
	if m:
		return sum(m)
	else:
		return 0

def calc_groups(members = None, a = 1):

	if not members:
		members = []	


	members_sum = summ(members)

	if len(members) == R and members_sum == SUM:
		trios.append(members)
		return

	if a + members_sum > SUM:
		return
	
	b = min(N,SUM - members_sum) 
	for i in xrange(a, b + 1):
		k = members[:]
		k.append(i)
		calc_groups(k,i+1)


def calc_circls(members = None, a = 0):

	if not members:
		members = []	

	if len(members) == C:
		circls.append(members)
		return

	# we can make it more efficent	
	for i in xrange(a, len(trios)):
		k = members[:]
		k.append(trios[i])
		calc_circls(k,i+1)

def is_valid_circle(circle):
	
	# check if a number is appear more then twice.
	# the total number should be: C with 1 apearance, C with 2

	d = {}
	for trio in circle:
		for number in trio:
			if number not in d:
				d[number] = 0
			d[number] += 1
	
	# check if all the digits are in the keys
	if d.keys() != range(1, N+1):
		return False

	#print 'keys:', d.keys(), range(1, N+1)
	#print circle

	# check that half number show 1 time, half 2	 
	inv_d = {}
	for k, v in d.iteritems():
		inv_d[v] = inv_d.get(v, [])
		inv_d[v].append(k)

	if len(inv_d[1]) != N/2 or len(inv_d[2]) != N/2:
		return False
	
	return True

def calc_rings(trios, ring = None):

	#print 'trios, ring:', trios, ring
	
	if ring is None:
		ring = []
	
	for i, n in enumerate(trios):
		first_trio = n
		other_trios = trios[:i] + trios[i+1:]
		#print 'first, trios:', first_trio, other_trios		
		for trio in itertools.permutations(first_trio):
			#print 'trio', trio
			
			# check if the middle number in the trio is the last number in the previos trio 
			# like in this ring: 4,3,2; 6,2,1; 5,1,3

			if ring:
				current_trio_middle = trio[len(trio)/2]
				previos_trio = ring[-1]
				#print 'previos trio', previos_trio	
				previos_trio_last = previos_trio[-1]

				if current_trio_middle != previos_trio_last:
					continue
					
			new_ring = ring[:]
			new_ring.append(trio)
		
			if not other_trios:
				
				# check if the last trio is ok
				first_trio = new_ring[0]
				first_trio_middle = first_trio[len(trio)/2]
				current_trio_last = trio[-1]
				
				if current_trio_last == first_trio_middle:
					valid_rings.append(new_ring)
				else:
					continue	
						
			# calc new params
			calc_rings(other_trios, new_ring)		 
			
def get_numerical_representation(ring):

	# get the index of the trio where the first item is the lowest
	first_nodes = {}	
	for i, trio in enumerate(ring):
		first_node = trio[0]
		first_nodes[first_node] = i

	min_first_node = min(first_nodes.keys())
	min_node_index = first_nodes[min_first_node]

	new_ring =  ring[min_node_index:] + ring[:min_node_index]
	new_ring_string =  ''.join(''.join(str(i) for i in trio) for trio in new_ring)
	return new_ring_string


def calc_all(sum):
	
	global SUM, trios, circls, valid_circls, valid_rings	
	SUM = sum 
	trios = []
	circls = []
	valid_circls = []
	valid_rings = []

	calc_groups()
	calc_circls()
	
	# len of circle should be the binomial coffinent: (C, len(trios))
	#print 'len of possible circles:', len(circls), ' == C(%d, %d)' % (C,len(trios))

	valid_circls  = [circle for circle in circls if is_valid_circle(circle)]
	for circle in valid_circls:
		calc_rings(circle)
	
	if not valid_rings:
		return 
	
	ids = [get_numerical_representation(ring) for ring in valid_rings]
	ids2 = [int(id) for id in ids if len(id) == 16]
	if not ids2:
		return

	print 'sum, max, len:', SUM, max(ids2)

for i in range(1,50):
	calc_all(i)			
