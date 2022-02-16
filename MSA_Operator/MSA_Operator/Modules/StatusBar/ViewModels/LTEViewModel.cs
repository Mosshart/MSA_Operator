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
    /// Viewmodel of LTE control
    /// </summary>
    public class LTEViewModel : BindableBase
    {
      
        private string _lTEPower = @"../Images/Icon_LTE_3.png";
        /// <summary>
        /// get/set lte power text
        /// </summary>
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
