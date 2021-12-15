using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;

namespace Map.Business
{
    public class MapDetails : INotifyPropertyChanged
    {
        #region Map 
        private MapMode _mode = new RoadMode();//new AerialMode(true);
        public MapMode Mode
        {
            get { return _mode; }
            set {   
                _mode = value;
                OnPropertyChanged();
            }
        }

        private double _zoomLevel = 16.0;
        public double ZoomLevel
        {
            get { return _zoomLevel; }
            set {   
                _zoomLevel = value;
                OnPropertyChanged();
            }
        }

        private Location _location = new Location(50.321549, 18.664897);
        public Location Location
        {
            get { return _location; }
            set {  
                _location = value;
                OnPropertyChanged();
            }
        }

        private AnimationLevel _animationLevel = AnimationLevel.None;
        public AnimationLevel AnimationLevel
        {
            get { return _animationLevel; }
            set {   
                _animationLevel = value;
                OnPropertyChanged();
            }
        }

        private CredentialsProvider _credentials;
        public CredentialsProvider Credentials
        {
            get { return _credentials; }
            set {   
                _credentials = value; 
                OnPropertyChanged();
            }
        }

        private LocationCollection _locations;
        public LocationCollection Locations
        {
            get => _locations;
            set{
                _locations = value;
                OnPropertyChanged();
            }
        }

       /* private Pushpin _operatorPushpin;
        public Pushpin OperatorPushpin
        {
            get => _operatorPushpin;
            set
            {
                _operatorPushpin = value;
                OnPropertyChanged();
            }
        }

        private Pushpin _robotPushpin;
        public Pushpin RobotPushpin
        {
            get => _robotPushpin;
            set
            {
                _robotPushpin = value;
                OnPropertyChanged();
            }
        }
       */
        private Location _operatorLocation = new Location(50, 18);
        public Location OperatorLocation
        {
            get => _operatorLocation;
            set
            {
                _operatorLocation = value;
                OnPropertyChanged();
            }

        }

        private Location _robotLocation = new Location(22.301549, 22.624897);
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
 