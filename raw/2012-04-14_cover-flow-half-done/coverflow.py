'''
Imlements a "Cover flow" of pictures using openCV

Hila Shmuel
13.04.2012
'''
import sys
import cv

class CoverFlowImage(object):
    '''
    This class handles a single picture.
    It allows manipulation of the picture, i.e transformation, reflection, etc.
    '''
    REFLECTION_DARKNESS = 500.0

    def __init__(self, image_path, scaled_width = None, scaled_height = None):
        image = cv.LoadImage(image_path)
        self.image = self._create_resized_image(image, scaled_width, scaled_height) 
        self._reflected_image = self._create_image_reflection(self.image) 

        # 'static' variables, here for preformance
        self.transformed_image = cv.CreateImage((self.image.width,
            self.image.height * 2), self.image.depth, self.image.nChannels)
        self._transformed_reflected_image =  cv.CreateImage((self.image.width,
            self.image.height * 2), self.image.depth, self.image.nChannels)
        self._transfrom_matix = cv.CreateMat(3, 3, cv.CV_32FC1)
    
    def _create_resized_image(self, image, scaled_width = None, scaled_height = None):
        if scaled_width is None:
            scaled_width = image.width
        if scaled_height is None:
            scaled_height = image.height
        scaled_width  = min(scaled_width,  image.width)
        scaled_height = min(scaled_height, image.height)
        
        resized_image = cv.CreateImage((scaled_width, scaled_height), image.depth, image.nChannels)
        cv.Resize(image, resized_image)
        return resized_image

    def _create_image_reflection(self, image):
        reflected_image = cv.CreateImage((image.width, image.height), image.depth, image.nChannels)
        cv.Copy(image, reflected_image)
        dark_image = cv.GetMat(reflected_image)
        
        for x in xrange(dark_image.cols):
            for y in xrange(dark_image.rows):
                dark_pixle = lambda p: max(0, int(p * y / self.REFLECTION_DARKNESS))
                dark_image[y, x] = tuple(dark_pixle(c) for c in dark_image[y, x])
        
        cv.Flip(dark_image, None, 0)
        return dark_image

    def create_image_transform(self, rotation):
        '''
        returns the picture with its reflection, rotated by the rotation level
        rotation - between 0 - 100, to indicat the image rotation level,
                    where location is 50 the image is fully displayed
        '''
       
        h1 = self.image.height
        w1 = self.image.width
        src = [(0,0), (w1, 0), (0, h1), (w1,h1)]

        # rotation transform
        a  = abs(50.0 - rotation)
        if rotation < 50.0:
            dst = [(a, 0), (w1 - a, a), (a, h1), (w1 - a, h1 - a)]
        else:
            dst = [(a , a), (w1 - a, 0), (a, h1 - a), (w1 - a, h1)]
        
        cv.GetPerspectiveTransform(src, dst, self._transfrom_matix)
        cv.WarpPerspective(self.image, self.transformed_image, self._transfrom_matix)
       
        # reflection transform
        w0, h0 = dst[0]
        w1, h1 = dst[1]
        w2, h2 = dst[2]
        w3, h3 = dst[3]
        dst = [(w2, h2 - 1), (w3, h3 - 1), (w0, h2 * 2 - h0 - 1), (w1, h3 * 2 - h1 - 1)]
        
        cv.GetPerspectiveTransform(src, dst, self._transfrom_matix)
        cv.WarpPerspective(self._reflected_image, self._transformed_reflected_image, self._transfrom_matix)
        
        cv.Or(self._transformed_reflected_image, self.transformed_image, self.transformed_image)
        return self.transformed_image

class CoverFlowImageData(object):
    '''
    for each image, this class contains:
       path, CoverFlowImage, location within the coverflow, state 
    '''

    def __init__(self, image_path, cover_flow_image):
        self.path = image_path
        self.cover_flow_image = cover_flow_image 
        self.location = 0
        self.rotation = 0      # [0-100]

class CoverFlow(object):
    
    SPACE_BETWEEN_IMAGES = 180
    
    def __init__(self, width, height, images_paths, max_image_width = 200, max_image_height = 200, writer = None):
        '''
        creates a coverflow image viewer.
        images_paths - list of paths
        width, height - of the cover flow
        max_image_width/height - of the pictures displayed on the coverflow
        '''
        self.height = height
        self.width = width
        self._display_line = self.height / 2
        self._middle_line  = self.width  / 2
        self._background = cv.CreateImage((self.width, self.height), cv.IPL_DEPTH_8U, 3)
        self._writer = writer
        
        assert max_image_width > 0 and type(max_image_width) is int
        assert max_image_height > 0 and type(max_image_height) is int
        self.max_image_height = max_image_height
        self.max_image_width  = max_image_width

        self._images = []
        for path in images_paths:
            image = CoverFlowImage(path, self.max_image_width, self.max_image_height)
            self._images.append(CoverFlowImageData(path, image))
        self._initial_coverflow_images()
        
    def _initial_coverflow_images(self):
        '''
        Set the initial state - left picture on the middle
        '''
        location = self._middle_line
        for image in self._images[::-1]:
            image.location = location
            location += self.SPACE_BETWEEN_IMAGES
    
    def move_right(self, movement):
        '''
        move the images right.
        the rightmost(last) image, if appears, should be placed on the middle
        '''
        last_image_location = self._images[-1].location
        middle = self._middle_line

        if last_image_location <= middle: 
            if last_image_location + movement > middle:
                movement = middle - last_image_location 
     
        if movement == 0:
            return movement
        
        for image in self._images:
            image.location += movement
        
        return movement
        
    def move_left(self, movement):
        '''
        move the images left.
        the leftmost(first) image, if appears, should be places on the middle
        '''
        first_image_location = self._images[0].location
        middle = self._middle_line
        
        if first_image_location >=  middle:
            if first_image_location - movement < middle:
                movement = first_image_location - middle
       
        if movement == 0:
            return
        
        for image in self._images:
            image.location -= movement
        
        return movement
    
    def update(self):
        '''
        update the coverflow image
        '''
       
        # sorted so that the background images
        # won't hide those in the middle
        current_images = {}

        for image in self._images:
            if 0 <= image.location and image.location <= self.width:
                image.rotation = (100.0 * image.location) / self.width
                image.cover_flow_image.create_image_transform(image.rotation)
                total_rotation = int(abs(50.0 - image.rotation)) 
                current_images[total_rotation] = image    
        
        cv.Set(self._background, (0, 0, 0))

        rotations = current_images.keys()
        rotations.sort()
        
        # draw every image on cover flow background
        for i in rotations[::-1]:
            image_data = current_images[i]
            image = image_data.cover_flow_image.transformed_image
            rect = (image_data.location - image.width / 2,
                    self._display_line - image.height / 2,
                    image.width,
                    image.height)
   
            if rect[0] < 0:
                continue
                 
            if rect[0] + rect[2] >= self.width or rect[1] + rect[3] >= self.height:
                continue

            cv.SetImageROI(self._background, rect)
            cv.Copy(image, self._background)
       
        cv.SetImageROI(self._background, (0, 0, self.width, self.height))
        if self._writer: 
            cv.WriteFrame(self._writer, self._background)
        return self._background      
            
prev_x = 0
count = 0

def on_mouse_movement(event, x, y, flags, cover_flow):
  
    global prev_x
    global count

    if count < 4:
        count += 1
        return None
    count = 0
     
    if event is not cv.CV_EVENT_MOUSEMOVE:
        return None

   
    if x - prev_x > 0:
        cover_flow.move_right(13) 
    
    elif prev_x - x> 0:
        cover_flow.move_left(13) 
    
    prev_x = x
    cover_flow.update()
    cv.ShowImage('window', cover_flow._background)
    return None

def main():
    
    writer = cv.CreateVideoWriter('movieforbakki.mpeg',cv.CV_FOURCC('M','J','P','G') , 30,
        (1000, 500), 1)
    print writer
    a = CoverFlow(1000, 500,  sys.argv[1:], 200, 200)

    cv.NamedWindow('window')
    cv.SetMouseCallback('window', on_mouse_movement, a)
    a.update()
    cv.ShowImage('window', a._background)
    cv.WaitKey(50000)
    
    cv.DestroyAllWindows() 

if '__main__' == __name__:
    main()
