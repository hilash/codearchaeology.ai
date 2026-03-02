clc;

Lena = imread('Lena256B.bmp');   % reads Lena as matrix 

Lena_vector = Lena(:);    % converts Lena from matrix to vector

hufman_length(Lena_vector)  % Lena's Huffman codding length

entrophy_length(Lena_vector)   % Lena entropy

Noise =  ceil(256.*rand(length(Lena_vector),1));   % simulates random vector of integers 0 to 256 by Lena's length

hufman_length(Noise)  % Noise's Huffman codding length

entrophy_length(Noise)  % Noise's entropy