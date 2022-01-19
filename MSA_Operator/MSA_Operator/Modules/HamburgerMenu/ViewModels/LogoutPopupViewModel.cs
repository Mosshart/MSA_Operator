using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HamburgerMenu.ViewModels
{
    public class LogoutPopupViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand LogoutYesCommand { get; private set; }
        public DelegateCommand LogoutNoCommand { get; private set; }
        IRegionManager _regionManager;
        public LogoutPopupViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LogoutYesCommand = new DelegateCommand(OnLogoutYes);
            LogoutNoCommand = new DelegateCommand(OnLogoutNo);
        }

        private void OnLogoutNo()
        {
          
            RemoveViewFromRegion("HamburgerMenuRegion");
        }

        private void OnLogoutYes()
        {
            //Tutaj wylogowanie z bazy danych

            RemoveViewFromRegion("HamburgerMenuRegion");
            RemoveViewFromRegion("LocalizationListBtnRegion");
            RemoveViewFromRegion("ReturnHomeBtnRegion");
            RemoveViewFromRegion("MovementButtonRegion");
            RemoveViewFromRegion("HamburgerMenuBtnRegion");
            RemoveViewFromRegion("LocalizationRegion");


            _regionManager.RequestNavigate("MainRegion", "LoginOverlay");
            _regionManager.RequestNavigate("WindowRegion", "LoginWindow");


        }

        private void RemoveViewFromRegion(string viewName)
        {
            try
            {
                var singleView = _regionManager.Regions[viewName].ActiveViews.FirstOrDefault();

                _regionManager.Regions[viewName].Remove(singleView);
            }catch
            {

            }
        }



        private string _logoutInfo = "Czy napewno chcesz się wylogować";
        private string LogoutInfo
        {
            get
            {
                return _logoutInfo;
            }
            set
            {
                if(value != null)
                {
                    SetProperty(ref _logoutInfo, value);
                }
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            return;
            throw new NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
