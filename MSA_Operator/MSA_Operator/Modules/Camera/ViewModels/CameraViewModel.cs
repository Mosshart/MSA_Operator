using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;
using System.Windows.Media.Imaging;
using RosCommunication;
using RosCommunication.Messages.sensor_msgs;
using MSAOperator.Services;
using System.IO;

namespace Camera.ViewModels
{
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

            var bitmap = new BitmapImage();
            using (var mem = new MemoryStream(image.data))
            {
                mem.Position = 0;
                bitmap.BeginInit();
                bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = null;
                bitmap.StreamSource = mem;
                bitmap.EndInit();
            }
            bitmap.Freeze();
            CameraImage = bitmap;
            // FramesCount++;
            CameraImage = bitmap;
        }
        private void changeBitmapImage(BitmapImage image)
        {
            CameraImage = image;
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
    }
}
