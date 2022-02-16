using MSAOperator.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

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
