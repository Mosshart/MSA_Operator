﻿using Prism.Commands;
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
//using Map = Microsoft.Maps.MapControl.WPF.Map;
using System.Windows.Controls;
using System.Device.Location;
using System.Windows;
using MSAOperator.Services;
using System.Windows.Media.Imaging;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Input;
using System.Windows.Media;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Map.ViewModels
{
    public class MapViewModel : BindableBase
    {
        public DelegateCommand ChangeMapLayer { get; private set; }
        public DelegateCommand StartRouteCommand { get; private set; }
       // public DelegateCommand CreatePinOnMap;
        public DelegateCommand<object> AddPinButtonCommand { get; private set; }
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
            MapDetails = new MapDetails(ea);
           // MapDetails.OperatorLocation = new Location(GeoLoc.Latitude, GeoLoc.Latitude);
            _regionManager = regionManager;
            //ChangeMapLayer = new DelegateCommand(OnExecuteChangeMapLayerCommand).ObservesProperty(() => CanChangeLayer);
            ChangeMapLayer = new DelegateCommand(OnExecuteChangeMapLayerCommand).ObservesProperty(() => CanChangeLayer);
            StartRouteCommand = new DelegateCommand(OnExecuteStartRouteCommand).ObservesCanExecute(() => CanCliCkSetRouteLayer);
            AddPinButtonCommand = new DelegateCommand<object>(AddPinButtonPress);
             //  _localization = localization;
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
            //AddPin++;
            
            AddPinFunc(map);
        }

        private Microsoft.Maps.MapControl.WPF.Map map;
        private void AddPinFunc(object o)
        {
            if (map == null)
                map = (Microsoft.Maps.MapControl.WPF.Map)o;
            //Location loc = (e.NewValue as Location);
            Point centerPoint = new Point((map.ActualWidth / 2), ((map.ActualHeight / 2) + 96));

            Location pinLocation = map.ViewportPointToLocation(centerPoint);

            Pushpin pin = new Pushpin();
            //   pin.s
          //  ControlTemplate template = pin.Template;
            // template.I
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
               // LocationCollection locCol = tempRoute();

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
        public Visibility CameraMinimizedVisibility
        {
            get => _cameraMinimizedVisibility;
            set
            {
                SetProperty(ref _cameraMinimizedVisibility, value);
            }
        }
        private Visibility _localizenBtnRegionVisibility = Visibility.Visible;
        public Visibility LocalizenBtnRegionVisibility
        {
            get => _localizenBtnRegionVisibility;
            set
            {
                SetProperty(ref _localizenBtnRegionVisibility, value);
            }
        }
        private Visibility _midLinesVisibility = Visibility.Collapsed;
        public Visibility MidLinesVisibility
        {
            get => _midLinesVisibility;
            set
            {
                SetProperty(ref _midLinesVisibility, value);
            }
        }
        private Visibility _addPinButtonVisibility = Visibility.Collapsed;
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
            //  MidLinesVisibility = status;
            CameraMinimizedVisibility = status;
           // LocalizenBtnRegionVisibility = status;           
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
            // bool toChange = IsSetView;
            IsSetView = loc;
            //IsSetView = new Location(0,0);
            if (x)
            {
               // SetRoute = tempRoute();
                x = !x;
            }
            else
            {
              //  SetRoute = tempRoute2();
                x = !x;
            }
        }
     

        private Location _isSetView;
        private LocationCollection _setRoute;
        private bool _canChangeLayer = true;
        private bool _canCliCkSetRouteLayer = false;
        
        public LocationCollection SetRoute
        {
            get => _setRoute;
            set
            {
                SetProperty(ref _setRoute, value);
            }
        }
        private int _addPin = 0;
        public int AddPin
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
        public bool CanCliCkSetRouteLayer
        {
            get => _canCliCkSetRouteLayer;
            set
            {
                SetProperty(ref _canCliCkSetRouteLayer, value);
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

           // AddPinButtonCommand();
           // AddPin++;           // pin.Location = loc;
            // Adds the pushpin to the map.
            //MapControl.Children.Add(pin);
        }
        

        private SolidColorBrush _buttonStartColor = (SolidColorBrush) new BrushConverter().ConvertFrom("#707070");//("#0FEFAB"
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
