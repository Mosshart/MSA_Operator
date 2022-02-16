using MSAOperator.Services;
using Prism.Mvvm;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace StatusBar.ViewModels
{
    /// <summary>
    /// Viewmodel of statusbar control
    /// </summary>
    public class StatusBarViewModel : BindableBase
    {
        private WLanInfoService _wlan;
       
        private GeoLocalizationService _geoLoc;
        
    
        public StatusBarViewModel(WLanInfoService wlan, GeoLocalizationService GeoLoc)
        {
            this._wlan = wlan; 
            this._geoLoc = GeoLoc;
        }

    }
}
