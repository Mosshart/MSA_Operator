using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels.CameraViewModel
{
    class CameraViewModel :BindableBase
    {


     //   public DispatcherTimer _timer;
        public CameraViewModel()
        {

           /* MJPEGStream stream = new MJPEGStream("https://webcam1.lpl.org/axis-cgi/mjpg/video.cgi");
            stream.NewFrame += new NewFrameEventHandler(video_NewFrame);
            stream.Start();*/
        }

     /*   int valueA = 0;
        private void video_NewFrame(object sender,NewFrameEventArgs eventArgs)
        {
            // get new frame
            valueA++;
            System.Drawing.Bitmap bitmap = eventArgs.Frame;
            ImageSource a = BitmapToImageSource(bitmap);
            CameraStream = BitmapToImageSource(bitmap);
            // process the frame 
        }
        public static void SaveClipboardImageToFile(string filePath)
        {
            var image = Clipboard.GetImage();
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
        }

        BitmapImage BitmapToImageSource(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }*/


        private ImageSource _cameraStream = new BitmapImage(new Uri(@"/MSA_Operator;component/Images/Cam_PreViews.png", UriKind.Relative));
        public ImageSource CameraStream
        {
            get
            {
                return _cameraStream;
            }
            set
            {
                if (_cameraStream == value)
                    return;
                SetProperty(ref _cameraStream, value);
            }
        }


    }
}