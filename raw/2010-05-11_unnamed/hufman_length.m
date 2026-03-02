function s = hufman_length( v )
% compute the hufman code length.
    
    % convert v to a vector row.
    vecSize = 1;
    origSize = size(size(v),2);
    for i = 1 : origSize
        vecSize = vecSize * size(v,i);
    end
    v = reshape(v,1,vecSize);
    
    temp = 0 : 255;
    Pr = hist(v,temp);
    Pr = Pr ./ length(v);
    Pr = [Pr ; 0:length(Pr)-1];
    Pr = Pr';   % first col Pr , second col index.
    
    % sort Pr + delete zeros
    Pr = sortrows(Pr);
    while (Pr(1) == 0)
        Pr = Pr(2:end,:);
        Pr = sortrows(Pr);
    end

    % we start to build a huffman tree but instead of creating a huffman
    % code for the symbols we will count the number of merges in the tree.
    i = 1;
    while (Pr(1) ~= 1)
        row1 = Pr(1,:);
        % add the two probabiliteies with the lowest probability.
        Pr(2) = Pr(2) + row1(1); 
        Pr = Pr(2:end,:); % delete the first row.
        row1 = row1( :,2:end);
        tmp = zeros(size(Pr,1),1);
        tmp = tmp - 1;
        Pr = [Pr tmp];
        row2 = Pr(1, :);
        for j = 2 : size(Pr,2)
             if ((row2(j) == -1) && (~isempty(row1))) 
                 row2(j) = row1(1);
                 row1 = row1( :,2:end);
             end
        end
        Pr = Pr(2:end,:);  % delete the second row.
        Pr = [row2 ; Pr];   % insert new second row. 
        row1 = Pr(1,2:end);
        x{i} = row1;
        i = i+1;
        Pr = sortrows(Pr);
    end
    mergeNum = size(x,2);
    HuffmanCodeLength = zeros(1,256);
    
    for i = 1 : mergeNum
        tmp = x{i};
        xSize = size(tmp,2);
        for j= 1 : xSize
            if (tmp(j) ~= -1)
                HuffmanCodeLength(tmp(j)+1)=HuffmanCodeLength(tmp(j)+1)+1;
            end
        end
    end
   
    Pr = hist(v,temp);
    s = 0;
    
    % compute the length.
    for i= 1: length(temp)
        s = s + (Pr(i) * HuffmanCodeLength(i));
    end
end

