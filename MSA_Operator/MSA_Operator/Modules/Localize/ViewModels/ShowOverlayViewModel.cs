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
    public class ShowOverlayViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private IEventAggregator _ea;

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public ShowOverlayViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
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
