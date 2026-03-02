% calculates the size of the huffman code length and entropy length.

clear all;
pic = imread('Lena256B.bmp');

picRowSize = size(pic,1);
picColSize = size(pic,2);

v = rand(picRowSize,picColSize);
v = floor(v * 255);
picSize = picRowSize * picColSize;
reshapePic = reshape(v,1,picSize);

HuffmanLength=hufman_length(reshapePic)
EntropyLength=entrophy_length(reshapePic)