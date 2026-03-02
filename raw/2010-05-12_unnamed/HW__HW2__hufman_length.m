function S = hufman_length(v)
    
    Bins = hist(v,(0:265));  % Bins will hold the number of appearences for each element of v. because Lena is 256B the range of elements is 0 to 256
    
    non_zero = find(Bins >0);  % non_zero will hold the indexes on non zero cells in Bins vector
	
    Bins  = Bins(non_zero);  % now Bins will hold only the appearences that are larger then 0
  
    counter = 0;  % will hold the number of bits needed to Huffman code v 
 
    while (length(Bins) > 1)
	
        Bins  = sort(Bins );   % sorts the probabilities from min to max 
		    
		Bins(2) = Bins(1) + Bins(2);  % makes the pear of smallest probabilities to a single tree node (for progressing to next tree level)

        counter =  counter + Bins(2);  % gives 1 bit count for each element in pear of the lowest probabilities per level of code building
        
		Bins  = Bins(2:length(Bins));  % decreasing the Bins vector by 1 (1 tree level)
    
	end;
    
    S =  counter;  %the return value