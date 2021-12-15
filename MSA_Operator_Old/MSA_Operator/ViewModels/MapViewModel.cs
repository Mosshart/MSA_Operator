using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels
{
    class MapViewModel : BindableBase
    {
       
        #region Map 
        private MapMode _mode = new RoadMode();//new AerialMode(true);
        public MapMode Mode
        {
            get { return _mode; }
            set { SetProperty(ref _mode, value); }
        }

        private double _zoomLevel = 16.0;
        public double ZoomLevel
        {
            get { return _zoomLevel; }
            set { SetProperty(ref _zoomLevel, value); }
        }

        private Location _location = new Location(50.321549, 18.664897);
        public Location Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        private AnimationLevel _animationLevel = AnimationLevel.None;
        public AnimationLevel AnimationLevel
        {
            get { return _animationLevel; }
            set { SetProperty(ref _animationLevel, value); }
        }

        private CredentialsProvider _credentials;

        

        public CredentialsProvider Credentials
        {
            get { return _credentials; }
            set { SetProperty(ref _credentials, value); }
        }
        #endregion
    }
}
