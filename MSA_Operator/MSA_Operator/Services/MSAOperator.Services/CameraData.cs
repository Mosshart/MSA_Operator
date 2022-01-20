using RosCommunication;
using System.IO;
using RosCommunication.Messages.sensor_msgs;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;

namespace MSAOperator.Services
{
    public class CameraData : INotifyPropertyChanged
    {
        RosNodeService _rosNodeService;
        Subscriber<CompressedImage> imageSubscriber;
        public CameraData()
        {
            //_rosNodeService = new RosNodeService();

            //imageSubscriber = RosNodeService.node.CreateSubscriber<CompressedImage>(MessageType.Image, "cam_front/compressed") as Subscriber<CompressedImage>;
            imageSubscriber = RosNodeService.node.CreateSubscriber<CompressedImage>(MessageType.Image, "cam_depth/compressed") as Subscriber<CompressedImage>;
            imageSubscriber.AddCallback(ImageCallback);
        }

        private void ImageCallback(CompressedImage image)
        {
            if (image.data == null || image.data.Length == 0)
                return;

           
            using (var mem = new MemoryStream(image.data))
            {
                var bitmap = new BitmapImage();
                mem.Position = 0;
                bitmap.BeginInit();
                bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = null;
                bitmap.StreamSource = mem;
                bitmap.EndInit();
                bitmap.Freeze();
                Dupa = bitmap;
            }
        }

        private BitmapImage _cameraImage;
        public BitmapImage Dupa
        {
            get => _cameraImage;
            set
            {
                _cameraImage = value;
                OnPropertyChanged("Dupa");
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged
    }
}
