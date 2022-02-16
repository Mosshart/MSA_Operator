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
    public class HamburgerMenuBtnViewModel : BindableBase
    {
        public DelegateCommand ShowHamburgerMenuCommand { get; private set; }
        IRegionManager _regionManager;

        public HamburgerMenuBtnViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ShowHamburgerMenuCommand = new DelegateCommand(ShowHamburgerMenu);
        }

        private void ShowHamburgerMenu()
        {
            var parameters = new NavigationParameters();
            parameters.Add("IsAnimation", true);
            _regionManager.RequestNavigate("HamburgerMenuRegion", "HamburgerMenu", parameters);
        }
    }
}
