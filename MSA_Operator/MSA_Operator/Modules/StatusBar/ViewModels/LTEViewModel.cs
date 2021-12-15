using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatusBar.ViewModels
{
    public class LTEViewModel : BindableBase
    {
        public LTEViewModel()
        {

        }
        private string _lTEPower = @"../Images/Icon_LTE_3.png";
        public string LTEPower
        {
            get
            {
                return _lTEPower;
            }
            set
            {
                if (_lTEPower == value)
                    return;
                SetProperty(ref _lTEPower, value);
            }
        }
    }
}
