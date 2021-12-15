using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Localization.Business;
using Prism.Regions;

namespace Localization.ViewModels
{
    public class LocalizationListBtnViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public LocalizationListBtnViewModel(IRegionManager regionManager)
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
                _regionManager.RequestNavigate("LocalizationRegion", navigatePath, parameters);
            }
        }
    }
}
