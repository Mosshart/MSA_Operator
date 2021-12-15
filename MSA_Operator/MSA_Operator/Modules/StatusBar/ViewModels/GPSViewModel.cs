using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatusBar.ViewModels
{
    public class GPSViewModel : BindableBase
    {
        public GPSViewModel()
        {
           
        }
        private string _isGPS = @"../Images/Icon_GPS.png";
        public string IsGPS
        {
            get
            {
                return _isGPS;
            }
            set
            {
                if (_isGPS == value)
                    return;
                SetProperty(ref _isGPS, value);
            }
        }
        
    }
}
