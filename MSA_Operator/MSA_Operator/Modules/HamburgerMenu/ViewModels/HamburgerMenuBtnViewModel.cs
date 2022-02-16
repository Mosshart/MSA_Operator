using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace HamburgerMenu.ViewModels
{
    /// <summary>
    /// view model class of user menu navigation button
    /// </summary>
    public class HamburgerMenuBtnViewModel : BindableBase
    {
        /// <summary>
        /// show menu button click action
        /// </summary>
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
