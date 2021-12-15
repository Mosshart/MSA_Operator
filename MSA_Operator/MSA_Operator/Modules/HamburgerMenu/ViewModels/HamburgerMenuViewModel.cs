using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HamburgerMenu.ViewModels
{
    public class HamburgerMenuViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand HideMenuCommand { get; private set; }
        public DelegateCommand LogOffCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        
        IRegionManager _regionManager;
        public HamburgerMenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            HideMenuCommand = new DelegateCommand(HideMenu);
            LogOffCommand = new DelegateCommand(LogOff);
            ExitCommand = new DelegateCommand(Exit);
        }

        private void LogOff()
        {
        }
        private void Exit()
        {
            System.Environment.Exit(0);
        }
        private void HideMenu()
        {
            var singleView = _regionManager.Regions["HamburgerMenuRegion"].ActiveViews.FirstOrDefault();
            _regionManager.Regions["HamburgerMenuRegion"].Remove(singleView);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
