% We calculate the MSE and PSNR of two pictures.
% In order to do so we need to reshape the pictures.
inputImage = imread('lena.tif');
newImage = imread('lena-error.tif');
imageSize = size(inputImage,1) * size(newImage,2);
inputImage = reshape(inputImage,1,imageSize);
newImage = reshape(newImage,1,imageSize);
MSE = mse(inputImage,newImage)
PSNR = snr(inputImage,newImage)