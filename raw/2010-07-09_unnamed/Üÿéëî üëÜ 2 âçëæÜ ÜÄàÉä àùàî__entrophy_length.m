function s = entrophy_length(v)
% computes the entrophy length.

    temp = 0:255;
    Pr = hist(v,temp);
    Pr = Pr ./ length(v);
    
    % sort Pr + delete zeros
    Pr = sort(Pr);
    while (Pr(1) == 0)
        Pr = Pr(:,2:end);
        Pr = sort(Pr);
    end
    
    PrLog = log2(Pr);
    
    Pr = Pr .* PrLog;   % Pi * log2(Pi)
    s = sum(Pr);
    s = -s;
end

