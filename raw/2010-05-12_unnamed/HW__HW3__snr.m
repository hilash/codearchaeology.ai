function PSNR=snr(inputImage,newImage)
% We assume that the number of bit per samples is 8 (n=8).
% We assume that mse(inputImage,newImage) != 0.
    subtruct = ((2^8) - 1)^2;
    devision = (subtruct / mse(inputImage,newImage));
    logarithm = log10(devision);
    PSNR = 10 * logarithm;
end