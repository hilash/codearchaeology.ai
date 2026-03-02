function MSC_dct_idct(image,msc_num)
    fun = @dct2;
    dct0 = blkproc(image,[8 8],fun);
    zig_zag_order=[1 3 4 10 11 21 22 36;
            2 5 9 12 20 23 35 37;
            6 8 13 19 24 34 38 49;
            7 14 18 25 33 39 48 50;
            15 17 26 32 40 47 51 58;
            16 27 31 41 46 52 57 59;
            28 30 42 45 53 56 60 63;
            29 43 44 54 55 61 62 64]; 
    
    % Create a -table that each index in the msc num will be 1 and other 0.
    check = (zig_zag_order <= msc_num + 1);
    fun1 = inline('x.*check');
    dct1 = blkproc(dct0,[8 8],fun1,check);
    
    ifun = @idct2;
    idct0 = blkproc(dct1,[8 8],ifun);
    
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

    nonZeroCoeff = find(dct1);
    coefficient = length(nonZeroCoeff);
    disp ('The number of non zero coefficient:');
    disp(coefficient);
end