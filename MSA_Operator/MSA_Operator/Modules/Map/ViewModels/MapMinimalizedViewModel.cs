using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Map.Business;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
using MSAEventAggregator.Core;
using Prism.Events;
using Prism.Regions;

namespace Map.ViewModels
{
    public class MapMinimalizedViewModel : BindableBase
    {
        public MapMinimalizedViewModel()
        {

        }

        private readonly IRegionManager _regionManager;
        private IEventAggregator _ea;

        public DelegateCommand NavigateResizeCommand { get; private set; }

        public MapMinimalizedViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            _ea = ea;

            NavigateResizeCommand = new DelegateCommand(Navigate);
        }
      
        private void Navigate( )
        {
            _regionManager.RequestNavigate("MainRegion", "Map");
            _ea.GetEvent<Events>().Publish("../Images/Control_Dark.png");
        }

      
        private MapDetails _mapDetails;
        public MapDetails MapDetails
        {
            get { return _mapDetails; }
            set { SetProperty(ref _mapDetails, value); }
        }
     /*   #region Map 
        private MapMode _mode = new RoadMode();//new AerialMode(true);
        public MapMode Mode
        {
            get { return _mode; }
            set { SetProperty(ref _mode, value); }
        }

        private double _zoomLevel = 24.0;
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
    */
        }
}
