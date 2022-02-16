using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
using MSAEventAggregator.Core;
using Prism.Events;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Map.Business
{
    /// <summary>
    /// Map details data
    /// </summary>
    public class MapDetails : INotifyPropertyChanged
    {
        private readonly IEventAggregator _ea;
        /// <summary>
        /// map details constructor
        /// </summary>
        /// <param name="ea"></param>
        public MapDetails(IEventAggregator ea)
        {
            _ea = ea;
        }
       

        #region Map 
        private MapMode _mode = new RoadMode();
        /// <summary>
        /// get/set map mode (stree/road)
        /// </summary>
        public MapMode Mode
        {
            get { return _mode; }
            set {   
                _mode = value;
                OnPropertyChanged();
            }
        }

        private double _zoomLevel = 20.0;
        /// <summary>
        /// Get/set map zoom level
        /// </summary>
        public double ZoomLevel
        {
            get { return _zoomLevel; }
            set {
                if (value < 7.0)
                {
                    value = 7.0;
                }
                else
                {
                    _zoomLevel = value;
                }
                OnPropertyChanged();
            }
        }

        private Location _location = new Location(50.321549, 18.664897);
        /// <summary>
        /// Get/set current map localizaion
        /// </summary>
        public Location Location
        {
            get { return _location; }
            set {  
                _location = value;

                _ea.GetEvent<CenterLocationChange>().Publish(_location.ToString());
                OnPropertyChanged();
            }
        }

        private AnimationLevel _animationLevel = AnimationLevel.None;
        /// <summary>
        /// get/set map animation level
        /// </summary>
        public AnimationLevel AnimationLevel
        {
            get { return _animationLevel; }
            set {   
                _animationLevel = value;

                OnPropertyChanged();
            }
        }

        private CredentialsProvider _credentials;
        /// <summary>
        /// get/set map credentials data
        /// </summary>
        public CredentialsProvider Credentials
        {
            get { return _credentials; }
            set {   
                _credentials = value; 
                OnPropertyChanged();
            }
        }

        private LocationCollection _locations;
        /// <summary>
        /// get/set location point list (markers, pins, object icons)
        /// </summary>
        public LocationCollection Locations
        {
            get => _locations;
            set{
                _locations = value;
                OnPropertyChanged();
            }
        }

        private Location _operatorLocation = new Location(50.321340, 18.665592);
        /// <summary>
        /// Get/set operator icon location
        /// </summary>
        public Location OperatorLocation
        {
            get => _operatorLocation;
            set
            {
                _operatorLocation = value;
                OnPropertyChanged();
            }

        }

        private Location _robotLocation = new Location(50.321589, 18.665184);
        /// <summary>
        /// Get/set robot icon location
        /// </summary>
        public Location RobotLocation
        {
            get => _robotLocation;
            set
            {
                _robotLocation = value;
                OnPropertyChanged();
            }
        }

        private int _robotOrientation = 43;
        /// <summary>
        /// Get/set robot icon angle
        /// </summary>
        public int RobotOrientation
        {
            get => _robotOrientation;
            set
            {
                _robotOrientation = value;
                OnPropertyChanged();
            }
        }
        private Location _midPinLocation = new Location(0, 0);

        /// <summary>
        /// get/set pin that shows in middle (only for visualization)
        /// </summary>
        public Location MidPinLocation
        {
            get => _midPinLocation;
            set
            {
                _midPinLocation = value;

                OnPropertyChanged();
            }
        }
        #endregion


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged
    }
}
 