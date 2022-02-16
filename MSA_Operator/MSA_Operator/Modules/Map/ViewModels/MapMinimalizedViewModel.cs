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

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Map.ViewModels
{
    public class MapMinimalizedViewModel : BindableBase
    {
        /// <summary>
        /// View model of minimized button control 
        /// </summary>
        public MapMinimalizedViewModel()
        {

        }

        private readonly IRegionManager _regionManager;
        private IEventAggregator _ea;
        /// <summary>
        /// map button click action
        /// </summary>
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
            _ea.GetEvent<CameraWindowEvent>().Publish(false);
        }

      
        private MapDetails _mapDetails;
        /// <summary>
        /// get/set map details
        /// </summary>
        public MapDetails MapDetails
        {
            get { return _mapDetails; }
            set { SetProperty(ref _mapDetails, value); }
        }
      
        }
}
