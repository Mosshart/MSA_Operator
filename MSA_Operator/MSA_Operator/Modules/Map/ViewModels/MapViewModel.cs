using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Map.Business;
using Microsoft.Maps.MapControl.WPF;
using MSAEventAggregator.Core;
using Prism.Events;
using Prism.Regions;
using System.Windows.Controls;
using System.Device.Location;
using System.Windows;
using MSAOperator.Services;
using System.Windows.Input;
using System.Windows.Media;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Map.ViewModels
{
    public class MapViewModel : BindableBase
    {
        /// <summary>
        /// change map layer button click actin
        /// </summary>
        public DelegateCommand ChangeMapLayer { get; private set; }
        /// <summary>
        /// Start route button click action
        /// </summary>
        public DelegateCommand StartRouteCommand { get; private set; }
        /// <summary>
        /// Addpin button click action
        /// </summary>
        public DelegateCommand<object> AddPinButtonCommand { get; private set; }
        /// <summary>
        /// set map view action
        /// </summary>
        public DelegateCommand<Location> SetViewCommand;
       
        private readonly IRegionManager _regionManager;
        private IEventAggregator _ea;
        private GeoCoordinateWatcher _watcher;
        private MapDetails _mapDetails;
        private GeoLocalizationService _geoLoc;

        
        public MapDetails MapDetails
        {
            get { return _mapDetails; }
            set { SetProperty(ref _mapDetails, value); }
        }
        public MapViewModel(IRegionManager regionManager,IEventAggregator ea, GeoLocalizationService GeoLoc)
        {
            this._geoLoc = GeoLoc;
            MapDetails = new MapDetails(ea);
            _regionManager = regionManager;
            ChangeMapLayer = new DelegateCommand(OnExecuteChangeMapLayerCommand).ObservesProperty(() => CanChangeLayer);
            StartRouteCommand = new DelegateCommand(OnExecuteStartRouteCommand).ObservesCanExecute(() => CanCliCkSetRouteLayer);
            AddPinButtonCommand = new DelegateCommand<object>(AddPinButtonPress);
             _ea = ea;
            _ea.GetEvent<LocalizationFindEvent>().Subscribe(HideShowForLocalization);
            _ea.GetEvent<LocalizeEvent>().Subscribe(MessageReceived);
            _ea.GetEvent<AddPin>().Subscribe(AddPinReceived);
            SetViewCommand = new DelegateCommand<Location>(OnSetView);
        }

        private void OnExecuteStartRouteCommand()
        {
            //Tutaj start drogi (wyslanie wiadomosci do robota)

            _ea.GetEvent<CloseLocalizationDetails>().Publish();
        }

        private void AddPinButtonPress(object map)
        {
            AddPinFunc(map);
        }

        private Microsoft.Maps.MapControl.WPF.Map map;
        private void AddPinFunc(object o)
        {
            if (map == null)
                map = (Microsoft.Maps.MapControl.WPF.Map)o;
            Point centerPoint = new Point((map.ActualWidth / 2), ((map.ActualHeight / 2) + 96));

            Location pinLocation = map.ViewportPointToLocation(centerPoint);

            Pushpin pin = new Pushpin();
            pin.MouseRightButtonUp += DeletePushpin;
            pin.Location = pinLocation;
            pins = map.Children;
            map.Children.Add(pin);
            getLocationsToRoute();

        }
        UIElementCollection pins; 
        private void DeletePushpin(object sender, MouseButtonEventArgs e)
        {
            Pushpin pushPinTodelete = (sender as Pushpin);
            map.Children.Remove(pushPinTodelete);
            getLocationsToRoute();
        }       
        private void getLocationsToRoute()
        {
            UIElementCollection pushpinsInMap = map.Children;
            List<Location> locations = new List<Location>();
            MapPolyline toRemove = null;
            foreach (var x in pushpinsInMap)
            {
                Type test = x.GetType();
                if (x.GetType() == typeof(Pushpin))
                {
                    Pushpin pin = x as Pushpin;
                    Location loc = pin.Location;
                    locations.Add(loc);
                   // break;
                }

                if (x.GetType() == typeof(MapPolyline))
                {
                    toRemove = (MapPolyline)x;
                   // break;
                }
            }
            if(toRemove != null)
                map.Children.Remove(toRemove);
            if (locations.Count >= 1)
            {
                LocationCollection locCol = CreateRoute(locations);

                MapPolyline routeLine = new MapPolyline()
                {
                    Locations = locCol,
                    Stroke = new SolidColorBrush(Colors.Blue),
                    StrokeThickness = 4
                };
                map.Children.Add(routeLine);
                ButtonStartColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#0FEFAB");
                CanCliCkSetRouteLayer = true;
            }
            else
            {
                ButtonStartColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#707070");
                CanCliCkSetRouteLayer = false;
            }
            
        }
        private LocationCollection CreateRoute(List<Location> points)
        {
            LocationCollection locs = new LocationCollection();
            locs.Add(_mapDetails.RobotLocation);
            foreach(Location x in points)
            {
                locs.Add(x);
            }
            return locs;
        }

        int statusCounter = 0;
        #region element visibility
        private Visibility _cameraMinimizedVisibility = Visibility.Visible;
        /// <summary>
        /// Is minimized camera visible
        /// </summary>
        public Visibility CameraMinimizedVisibility
        {
            get => _cameraMinimizedVisibility;
            set
            {
                SetProperty(ref _cameraMinimizedVisibility, value);
            }
        }
        private Visibility _localizenBtnRegionVisibility = Visibility.Visible;
        /// <summary>
        /// Is localize button visible
        /// </summary>
        public Visibility LocalizenBtnRegionVisibility
        {
            get => _localizenBtnRegionVisibility;
            set
            {
                SetProperty(ref _localizenBtnRegionVisibility, value);
            }
        }
        private Visibility _midLinesVisibility = Visibility.Collapsed;
        /// <summary>
        /// is cross poiting pin to place visible
        /// </summary>
        public Visibility MidLinesVisibility
        {
            get => _midLinesVisibility;
            set
            {
                SetProperty(ref _midLinesVisibility, value);
            }
        }
        private Visibility _addPinButtonVisibility = Visibility.Collapsed;
        /// <summary>
        /// is add pin button visible
        /// </summary>
        public Visibility AddPinButtonVisibility
        {
            get => _addPinButtonVisibility;
            set
            {
                SetProperty(ref _addPinButtonVisibility, value);
            }
        }
        private void HideShowForLocalization(bool isHidden)
        {
            Visibility status;
            if (isHidden)
            {
                status = Visibility.Collapsed;
                MidLinesVisibility = Visibility.Visible;
                AddPinButtonVisibility = Visibility.Visible;
            }
            else
            {
                status = Visibility.Visible;
                MidLinesVisibility = Visibility.Collapsed; 
                AddPinButtonVisibility = Visibility.Collapsed;
            }
            CameraMinimizedVisibility = status;     
        }

        #endregion

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
            IsSetView = loc;
            if (x)
            {
                x = !x;
            }
            else
            {
                x = !x;
            }
        }
     
        private Location _isSetView;
        private LocationCollection _setRoute;
        private bool _canChangeLayer = true;
        private bool _canCliCkSetRouteLayer = false;
        
        /// <summary>
        /// get/Set route on map
        /// </summary>
        public LocationCollection SetRoute
        {
            get => _setRoute;
            set
            {
                SetProperty(ref _setRoute, value);
            }
        }
        private int _addPin = 0;
        /// <summary>
        /// Pin counter
        /// </summary>
        public int AddPin
        {
            get => _addPin;
            set
            {
                SetProperty(ref _addPin, value);
            }
        }
        /// <summary>
        /// get/set info is layer changed 
        /// </summary>
        public bool CanChangeLayer
        {
            get => _canChangeLayer;
            set
            {
                SetProperty(ref _canChangeLayer, value);
            }
        }
        /// <summary>
        /// Get/set info if route click is possible
        /// </summary>
        public bool CanCliCkSetRouteLayer
        {
            get => _canCliCkSetRouteLayer;
            set
            {
                SetProperty(ref _canCliCkSetRouteLayer, value);
            }
        }
        /// <summary>
        /// Get/set map view
        /// </summary>
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
        }
        

        private SolidColorBrush _buttonStartColor = (SolidColorBrush) new BrushConverter().ConvertFrom("#707070");
        /// <summary>
        /// Get/set button start color
        /// </summary>
        public SolidColorBrush ButtonStartColor
        {
            get => _buttonStartColor;
            set
            {
                SetProperty(ref _buttonStartColor, value);
            }
        }
    }
}
