using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Media.Imaging;
using MSAEventAggregator.Core;
using Prism.Events;
using Prism.Regions;
using MSAOperator.Services;
using RosCommunication;
using System.IO;
using RosCommunication.Messages.sensor_msgs;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Camera.ViewModels
{
    /// <summary>
    /// view model of minimized camera button
    /// </summary>
    public class CameraMinimalizedViewModel : BindableBase
    {
        public CameraMinimalizedViewModel()
        {

        }


        private readonly IRegionManager _regionManager;
        private IEventAggregator _ea;

        private RosNodeService _node;
        Subscriber<CompressedImage> imageSubscriber;

        /// <summary>
        /// camera button click action
        /// </summary>
        public DelegateCommand NavigateResizeCommand { get; private set; }


        public CameraMinimalizedViewModel(IRegionManager regionManager, IEventAggregator ea, RosNodeService node)
        {
            _regionManager = regionManager;
            _ea = ea;
            _node = node;
            NavigateResizeCommand = new DelegateCommand(Navigate);
                   
            imageSubscriber = _node.node.CreateSubscriber<CompressedImage>(MessageType.Image, "cam_depth/compressed") as Subscriber<CompressedImage>;
            imageSubscriber.AddCallback(ImageCallback);
        }
        private CompressedImage ImageToByteArray(System.Drawing.Image imageIn)
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
                CameraImage = bitmap;
            }
            
        }
        private void Navigate()
        {
            _regionManager.RequestNavigate("MainRegion", "Camera");
            _ea.GetEvent<Events>().Publish("../Images/Control_Light.png");
            _ea.GetEvent<CameraWindowEvent>().Publish(true);
        } 

        private BitmapImage _cameraImage;
        /// <summary>
        /// get/set camera image
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