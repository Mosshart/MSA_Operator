using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Login.ViewModels
{
    public class LogoutButtonViewViewModel : BindableBase
    {
        /// <summary>
        /// clear textbox button click action
        /// </summary>
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
