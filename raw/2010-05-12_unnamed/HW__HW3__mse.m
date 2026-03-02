function MSE = mse(inputImage,newImage)
    image = double(inputImage) - double(newImage);
    image = double(image).^2;
    imageSum = sum(double(image));
    imageLength = length(inputImage);
    MSE = (double(imageSum) / double(imageLength));
end