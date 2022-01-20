using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Login.ViewModels
{
    public class LogoutButtonViewViewModel : BindableBase
    {
        public DelegateCommand LogOffCommand { get; private set; }
        private readonly IRegionManager _regionManager;

        public LogoutButtonViewViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LogOffCommand = new DelegateCommand(LogOff);
        }
        private void LogOff()
        {
            _regionManager.RequestNavigate("MainRegion", "LoginWindow");
        }
    }
}
