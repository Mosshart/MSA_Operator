using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Localization.Business;
using Prism.Regions;
using Prism.Events;
using MSAEventAggregator.Core;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Localization.ViewModels
{
    public class LocalizationListBtnViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private IEventAggregator _ea;
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public LocalizationListBtnViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            _ea = ea;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("IsAnimation", true);
                _regionManager.RequestNavigate("LocalizationRegion", navigatePath, parameters);



                _ea.GetEvent<LocalizationFindEvent>().Publish(true);
            }
        }
        private void RemoveViewFromRegion(string viewName)
        {
            try
            {
                var singleView = _regionManager.Regions[viewName].ActiveViews.FirstOrDefault();

                _regionManager.Regions[viewName].Remove(singleView);
            }
            catch
            {

            }
        }

    }
}
