using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Events;
using Prism.Regions;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Localize.ViewModels
{
    /// <summary>
    /// View model of show overlay control 
    /// </summary>
    public class ShowOverlayViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private IEventAggregator _ea;

        /// <summary>
        /// On show overlay button click action
        /// </summary>
        public DelegateCommand<string> ShowOverlay { get; private set; }

        public ShowOverlayViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;

            ShowOverlay = new DelegateCommand<string>(Navigate);
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("IsAnimation", true);
                _regionManager.RequestNavigate("LocalizeOverlay", navigatePath, parameters);
            }
        }
    }
}
