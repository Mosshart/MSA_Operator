using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatusBar.ViewModels
{
    public class SatteliteViewModel : BindableBase
    {
        public SatteliteViewModel()
        {

        }
        private string _satelliteNumber = "3";
        public string SatelliteNumber
        {
            get
            {
                return _satelliteNumber;
            }
            set
            {
                if (_satelliteNumber == value)
                    return;
                SetProperty(ref _satelliteNumber, value);
            }
        }
    }
}
