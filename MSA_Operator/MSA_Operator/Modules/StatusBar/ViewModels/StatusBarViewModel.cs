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
    public class StatusBarViewModel : BindableBase
    {
        private WLanInfoService _wlan;
        public WLanInfoService Wlan
        {
            get => _wlan;
            set
            {
                SetProperty(ref _wlan, value);
            }
        }
        private GeoLocalizationService _geoLoc;
        public GeoLocalizationService GeoLoc
        {
            get => _geoLoc;
            set
            {
                SetProperty(ref _geoLoc, value);
            }
        }
        public StatusBarViewModel(WLanInfoService wlan, GeoLocalizationService GeoLoc)
        {
            this.Wlan = wlan; 
            this.GeoLoc = GeoLoc;
        }

    }
}
