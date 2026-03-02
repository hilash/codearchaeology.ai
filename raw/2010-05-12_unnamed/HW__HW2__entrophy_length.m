function S = entrophy_length(v)
    
    v = double(v(:));  % converts the vector's type to double because it will contain probabilities
	
    Bins = hist(v,(0:256));  % Bins will hold the number of appearences for each element of v. because Lena is 256B the range of elements is 0 to 256
	
	non_zero = find(Bins>0);  % non_zero will hold the indexes on non zero cells in Bins vector
	
    Bins = Bins(non_zero);   % now Bins will hold only the appearences that are larger then 0
	
    Bins = Bins/sum(Bins);  % figuring the probability
	
    S = -sum (Bins .* log2(Bins)); % the entrophy by formula given in class
