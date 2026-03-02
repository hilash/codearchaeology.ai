EPSILON = 1;


%% read and play gif
%fullFileName = '\\cstud\hilash\WINDOWS\Redirect\Documents\MATLAB\Ronen\rotating_dog.gif';
%[gifImage cmap] = imread(fullFileName, 'Frames', 'all');
%size(gifImage)
%implay(gifImage);

fullFileName = '\\cstud\hilash\WINDOWS\Redirect\Documents\MATLAB\Ronen\dog.gif';
[gifImage, cmap] = imread(fullFileName, 'Frames', 'all');
all_dimensions = size(gifImage);
number_of_frames = all_dimensions(end);

 %for k=1:number_of_frames
     % an alternative way to read frame
     % [frame,map] = imread(fullFileName,'frames',k);
     %% you can check with this:
     %isequal(gifImage(:,:,:,1), frame)
     %figure;
     %imshow(frame,map);
 %end

% very lazy - need to flat all vectors, in order to use norm (and not
% matrix norm)
k = zeros(number_of_frames,number_of_frames);
gifImage2 = im2double(gifImage);

 for i=1:number_of_frames
     frame_i = gifImage2(:,:,:,i);
     frame_i_flat = reshape(frame_i.',1,[]);
     for j=i:number_of_frames
        frame_j = gifImage2(:,:,:,j);
        frame_j_flat = reshape(frame_j.',1,[]);
        
        diff_i_j = minus(frame_i_flat, frame_j_flat);
        norm_i_j = norm(diff_i_j);
        k(i,j)=(norm_i_j);
        k(j,i)=k(i,j);
     end
 end

% http://stackoverflow.com/questions/1773099/how-do-i-divide-matrix-elements-by-column-sums-in-matlab
%p = bsxfun(@rdivide, k, sum(k)); % for some reason normc or normr are not working
%p = k*diag(1./sum(k,1));
p = k./repmat(sum(k),size(k,1),1);

p_sum = sum(p);
p_sum_t = sum(p');

[V,D] = eig(p);
[V2,D2] = eig(p');


phi = V2(:,2);                          % The "principal" eigenvector 
order = [phi (1:number_of_frames)'];
new_order = sortrows(order);
frame_order = new_order(:,2);

% re ordering frames
new_gifImage = gifImage;
 for k=1:number_of_frames
     % an alternative way to read frame
     new_frame_id = frame_order(k);
     new_gifImage(:,:,:,k) = gifImage(:,:,:,new_frame_id);
     %RGB = ind2rgb(gifImage(:,:,:,new_frame_id),cmap);
     %new_gifImage(:,:,:,k) = RGB;
 end
imGray = ind2gray(new_gifImage,cmap);
implay(imGray);
%imwrite(new_gifImage,cmap,strrep(fullFileName, '.gif', '_new.gif'))
