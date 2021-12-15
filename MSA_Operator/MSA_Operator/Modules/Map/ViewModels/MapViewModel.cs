using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Map.Business;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
using MSAEventAggregator.Core;
using Prism.Events;
using Prism.Regions;
using Map = Microsoft.Maps.MapControl.WPF.Map;
using System.Windows.Controls;
using System.Device.Location;
using System.Windows;
using MSAOperator.Services;

namespace Map.ViewModels
{
    public class MapViewModel : BindableBase
    {
        public DelegateCommand ChangeMapLayer { get; private set; }
       // public DelegateCommand CreatePinOnMap;
        public DelegateCommand test { get; private set; }
        public DelegateCommand<Location> SetViewCommand;
        private readonly IRegionManager _regionManager;
        private IEventAggregator _ea;

        GeoCoordinateWatcher _watcher;
        private MapDetails _mapDetails;
        private GeoLocalizationService _geoLoc;
        private GeoLocalizationService GeoLoc
        {
            get => _geoLoc;
            set
            {
                SetProperty(ref _geoLoc, value);
            }
        }
        public MapDetails MapDetails
        {
            get { return _mapDetails; }
            set { SetProperty(ref _mapDetails, value); }
        }
        public MapViewModel(IRegionManager regionManager,IEventAggregator ea, GeoLocalizationService GeoLoc)
        {
            this.GeoLoc = GeoLoc;
            MapDetails = new MapDetails();
            MapDetails.OperatorLocation = new Location(GeoLoc.Latitude, GeoLoc.Latitude);
            _regionManager = regionManager;
            //ChangeMapLayer = new DelegateCommand(OnExecuteChangeMapLayerCommand).ObservesProperty(() => CanChangeLayer);
            ChangeMapLayer = new DelegateCommand(OnExecuteChangeMapLayerCommand).ObservesProperty(() => CanChangeLayer);
          //  _localization = localization;
            _ea = ea;
            _ea.GetEvent<LocalizeEvent>().Subscribe(MessageReceived);
            _ea.GetEvent<AddPin>().Subscribe(AddPinReceived);
            SetViewCommand = new DelegateCommand<Location>(OnSetView);
        }
        int statusCounter = 0;
      
       
        private string _textLabelTest;
        public string TextLabelTest
        {
            get => _mapDetails.Location.Latitude + ", " + _mapDetails.Location.Longitude + "status: " + statusCounter;
            set
            {
                SetProperty(ref _textLabelTest, value);
            }
        }
        
        static bool x = false;
        private void OnSetView(Location loc)
        {
            // bool toChange = IsSetView;
            IsSetView = loc;
            IsSetView = new Location(0,0);
            if (x)
            {
                SetRoute = tempRoute();
                x = !x;
            }
            else
            {
                SetRoute = tempRoute2();
                x = !x;
            }
        }
        #region todelete
        private LocationCollection tempRoute()
        {
            LocationCollection locs = new LocationCollection();

            Location p1 = new Location(54.318477, 20.312711);
            Location p2 = new Location(53.053134, 20.598355);
            Location p3 = new Location(54.179187, 30.189420);
            Location p4 = new Location(58.133361, 28.475553);

            Pushpin pin1 = new Pushpin();
            Pushpin pin2 = new Pushpin();
            Pushpin pin3 = new Pushpin();
            Pushpin pin4 = new Pushpin();

            pin1.Location = p1;
            pin2.Location = p2;
            pin3.Location = p3;
            pin4.Location = p4;

            locs.Add(p1);
            locs.Add(p2);
            locs.Add(p3);
            locs.Add(p4);
            /*  
             *  54.318477, 20.312711   
             *  53.053134, 20.598355
             *  54.179187, 30.189420
             *  58.133361, 28.475553
            42.863397, -7.999191
            37.913341, -5.845871
            45.138959, 1.449051
            47.535296, 7.513504
            */
            return locs;
        }
        private LocationCollection tempRoute2()
        {
            LocationCollection locs = new LocationCollection();

            Location p1 = new Location(42.863397, -7.999191);
            Location p2 = new Location(37.913341, -5.845871);
            Location p3 = new Location(45.138959, 1.449051);
            Location p4 = new Location(47.535296, 7.513504);

            Pushpin pin1 = new Pushpin();
            Pushpin pin2 = new Pushpin();
            Pushpin pin3 = new Pushpin();
            Pushpin pin4 = new Pushpin();

            pin1.Location = p1;
            pin2.Location = p2;
            pin3.Location = p3;
            pin4.Location = p4;

            locs.Add(p1);
            locs.Add(p2);
            locs.Add(p3);
            locs.Add(p4);
            /*  
             *  54.318477, 20.312711   
             *  53.053134, 20.598355
             *  54.179187, 30.189420
             *  58.133361, 28.475553
            50,324.56, 18,653868
            50.324355, 18.652457
            50.323739, 18.652747
            50.323482, 18.653675
            */
            return locs;
        }
        #endregion

        private Location _isSetView;
        private LocationCollection _setRoute;
        private bool _canChangeLayer = true;
        
        public LocationCollection SetRoute
        {
            get => _setRoute;
            set
            {
                SetProperty(ref _setRoute, value);
            }
        }
        private bool _addPin;
        public bool AddPin
        {
            get => _addPin;
            set
            {
                SetProperty(ref _addPin, value);
            }
        }
        public bool CanChangeLayer
        {
            get => _canChangeLayer;
            set
            {
                SetProperty(ref _canChangeLayer, value);
            }
        }
        public Location IsSetView
        {
            get => _isSetView;
            set
            {
                SetProperty(ref _isSetView, value);
            }
        }

        private  void OnExecuteChangeMapLayerCommand()
        {
            CanChangeLayer = false;
            if (this.MapDetails.Mode.GetType() == typeof(RoadMode))
            {
                this.MapDetails.Mode = new AerialMode(true);
                this.MapDetails.Location = new Location(52.285690, 20.624833);
                _ea.GetEvent<Events>().Publish("../Images/Control_Light.png");
            }
            else
            {
                this.MapDetails.Mode = new RoadMode();
                _ea.GetEvent<Events>().Publish("../Images/Control_Dark.png");
            }

            setButtonEnableAgain();
        }

        private async void setButtonEnableAgain()
        {
            await Task.Delay(1000 * 2);
            CanChangeLayer = true;
        }
        private void MessageReceived(bool isRobot)
        {
            Location loc;
            if (isRobot)
                loc = MapDetails.RobotLocation;
            else 
                loc = MapDetails.OperatorLocation;
            //_mapDetails.Location = loc;
            OnSetView(loc);
        }
        private void AddPinReceived(bool addPin)
        {
            // Location loc = MapControl.ViewportPointToLocation(xy);
           
            
            AddPin = !AddPin;
           // pin.Location = loc;
            // Adds the pushpin to the map.
            //MapControl.Children.Add(pin);
        }
        #region drawing lines
        /*  private void updateMapRoute(List<Location> locations)
          {

          }
          private void drawLine()
          {
              LocationCollection locs = new LocationCollection();

              foreach (var pin in MapControl.Children)
              {
                  if (pin.GetType() == typeof(Pushpin))
                      locs.Add((pin as Pushpin).Location);
              }
              MapPolyline routeLine = new MapPolyline()
              {
                  Locations = locs,
                  Stroke = new SolidColorBrush(Colors.Blue),
                  StrokeThickness = 5

              };

              MapControl.Children.Add(routeLine);
          }
          UIElementCollection 
          private Pushpin createPoint(Location loc)
          {
              Pushpin pin = new Pushpin();
              pin.Location = pinLocation;
              return pin;
          }*/
        #endregion
    }
}
