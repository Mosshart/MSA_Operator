using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Media.Imaging;
using RosCommunication;
using RosCommunication.Messages.sensor_msgs;
using MSAOperator.Services;
using System.IO;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Camera.ViewModels
{
    /// <summary>
    /// view model of main camera window
    /// </summary>
    public class CameraViewModel : BindableBase, INavigationAware
    {
        Subscriber<CompressedImage> imageSubscriber;
        private RosNodeService _node;
        
        public CameraViewModel(RosNodeService node)
        {
            _node = node;
            imageSubscriber = _node.node.CreateSubscriber<CompressedImage>(MessageType.Image, "cam_front/compressed") as Subscriber<CompressedImage>;
            imageSubscriber.AddCallback(ImageCallback);
     
        }
        #region iterface implementation
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //odbieranie streama
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
        #endregion
        public CompressedImage ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                CompressedImage a;
                imageIn.Save(ms, imageIn.RawFormat);
                a.data = ms.ToArray();
                return a;
            }
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
                //  CameraImage.StreamSource.Dispose();
                CameraImage = bitmap;
            }
        }
      

        private BitmapImage _cameraImage;
        /// <summary>
        /// Get/set camera image
        /// </summary>
        public BitmapImage CameraImage
        {
            get => _cameraImage;
            set
            {
                SetProperty(ref _cameraImage, value);
            }
        }
    }
}
