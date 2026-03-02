% calculates the size of the huffman code length and entropy length.
% we needed to encode Lena256B.bmp.

clear all;
pic = imread('Lena256B.bmp');

picSize = size(pic,1) * size(pic,2);
newPic = zeros(size(pic,1),size(pic,2));

for i=1 :size(pic,1)
    for j=1:size(pic,2)
        newPic(i,j)=pic(i,j,1);
    end
end

reshapedPic=reshape(newPic,1,picSize);

HuffmanLength = hufman_length(reshapedPic)
EntropyLength = entrophy_length(reshapedPic)