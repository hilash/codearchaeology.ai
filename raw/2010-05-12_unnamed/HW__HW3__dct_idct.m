function dct_idct(image)
    
    fun = @dct2;
    dct0 = blkproc(image,[8 8],fun);
    ifun = @idct2;
    idct0 = blkproc(dct0,[8 8],ifun);
    
    figure();
    imshow(image);
    figure();
    imshow(idct0./255);
    
    imageSize = size(image,1) * size(idct0,2);
    image = reshape(image,1,imageSize);
    idct0 = reshape(idct0,1,imageSize);
    
    disp('MSE:');
    MSE = mse(image,idct0);
    disp(MSE);
    disp('PSNR');
    PSNR = snr(image,idct0);
    disp(PSNR);
end