using Prism.Mvvm;
using Prism.Regions;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Login.ViewModels
{
    /// <summary>
    /// Overlay class 
    /// </summary>
    public class LoginOverlayViewModel : BindableBase, INavigationAware
    {
        #region interface implementation
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            string test = "A";
            if (test == "A")
                return;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string test = "A";
            if (test == "A")
                return;
        }
        #endregion
    }
}
