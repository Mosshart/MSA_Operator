using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Map.Business;
using MSAEventAggregator.Core;
using Prism.Events;
using Prism.Regions;
using MSAOperator.Services;
using System.Windows.Threading;
using RosCommunication;
using System.IO;
using RosCommunication.Messages.sensor_msgs;
using System.Configuration;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Camera.ViewModels
{

    public class CameraMinimalizedViewModel : BindableBase
    {
        public CameraMinimalizedViewModel()
        {

        }


        private readonly IRegionManager _regionManager;
        private IEventAggregator _ea;

        private RosNodeService _node;
        Subscriber<CompressedImage> imageSubscriber;
        public DelegateCommand NavigateResizeCommand { get; private set; }


        public CameraMinimalizedViewModel(IRegionManager regionManager, IEventAggregator ea, RosNodeService node)
        {
            _regionManager = regionManager;
            _ea = ea;
            _node = node;
            NavigateResizeCommand = new DelegateCommand(Navigate);

            // imageSubscriber = _node.node.CreateSubscriber<CompressedImage>(MessageType.Image, "cam_front/compressed") as Subscriber<CompressedImage>;
            // imageSubscriber.AddCallback(ImageCallback);
            //System.Drawing.Image img = Image.FromFile(@"C:\Users\fmystek\Documents\Projekty\msa_operator\MSA_Operator\MSA_Operator\Modules\Camera\Images\Cam_Preview.png");


            //imageSubscriber = _node.node.CreateSubscriber<CompressedImage>(MessageType.Image, "cam_front/compressed") as Subscriber<CompressedImage>;
            imageSubscriber = _node.node.CreateSubscriber<CompressedImage>(MessageType.Image, "cam_depth/compressed") as Subscriber<CompressedImage>;
            imageSubscriber.AddCallback(ImageCallback);
        }
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
            
          /*  using (MemoryStream mem = new MemoryStream(image.data))
            {

                mem.Position = 0;

                //bitmap.StreamSource = mem;
                //mem.Dispose();
            }*/

            /*  var bitmap = new BitmapImage(); 
             bitmap.BeginInit();
             bitmap.CacheOption = BitmapCacheOption.OnLoad;
             bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
             bitmap.UriSource = null;
            
             //bitmap.Freeze();
             bitmap.EndInit();*/
            //bitmap.Freeze();
            //CameraImage = bitmap;
            // FramesCount++;
            // CameraImage = bitmap;
        }



        private void changeBitmapImage(BitmapImage image)
        {
            CameraImage = image;
        }
        private void Navigate()
        {
            _regionManager.RequestNavigate("MainRegion", "Camera");
            _ea.GetEvent<Events>().Publish("../Images/Control_Light.png");
            _ea.GetEvent<CameraWindowEvent>().Publish(true);
        } 

        private BitmapImage _cameraImage;
        public BitmapImage CameraImage
        {
            get => _cameraImage;
            set
            {
                SetProperty(ref _cameraImage, value);
            }
        }
        /*  private ImageSource _cameraStream = new BitmapImage(new Uri(@"/Camera;component/Images/Cam_PreViews.png", UriKind.Relative));
          public ImageSource Camer  aStream
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
      }*/
        private string _cameraStream = @"../Images/Cam_Preview.png";


        public string CameraStream
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