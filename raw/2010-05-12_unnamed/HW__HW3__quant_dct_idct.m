function quant_dct_idct(image_name,q,s)
    fun = @dct2;
    dct0 = blkproc(image_name,[s s],fun);
    dct1 = round(dct0 / (2*q)) * (2*q);
    ifun = @idct2;
    idct0 = blkproc(dct1,[s s],ifun);
   
    figure();
    imshow(image_name);
    figure();
    imshow(idct0./255);
  
    imageSize = size(image_name,1) * size(idct0,2);
    image = reshape(image_name,1,imageSize);
    idct0 = reshape(idct0,1,imageSize);
    
    disp('MSE:');
    MSE = mse(image,idct0);
    disp(MSE);
    disp('PSNR');
    PSNR = snr(image,idct0);
    disp(PSNR);
    
    nonZeroCoeff = find(dct1);
    coefficient = length(nonZeroCoeff);
    disp ('The number of non zero coefficient:');
    disp(coefficient);
end