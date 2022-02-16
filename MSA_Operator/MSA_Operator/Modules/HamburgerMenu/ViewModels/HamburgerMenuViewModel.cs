using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace HamburgerMenu.ViewModels
{
    /// <summary>
    /// view model of user menu
    /// </summary>
    public class HamburgerMenuViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// hide menu button click action
        /// </summary>
        public DelegateCommand HideMenuCommand { get; private set; }
        /// <summary>
        /// log off button click action
        /// </summary>
        public DelegateCommand LogOffCommand { get; private set; }
        /// <summary>
        /// close application button click action
        /// </summary>
        public DelegateCommand ExitCommand { get; private set; }
        
        IRegionManager _regionManager;
        public HamburgerMenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            HideMenuCommand = new DelegateCommand(HideMenu);
            LogOffCommand = new DelegateCommand(LogOff);
            ExitCommand = new DelegateCommand(Exit);//;.
        }

        private void LogOff()
        {
            _regionManager.RequestNavigate("PopupRegion", "LogoutPopup");
           // string test = "";
           // _regionManager.Regions["PopupRegion"].ActiveViews.FirstOrDefault();
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

        #region interface implementation
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
        #endregion
    }
}
