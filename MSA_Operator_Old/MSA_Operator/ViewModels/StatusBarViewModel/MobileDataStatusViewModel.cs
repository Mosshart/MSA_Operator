using System;
using System.IO;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels.StatusBarViewModel
{
    class MobileDataStatusViewModel : BindableBase
    {
        private string _lTEPower = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "//Images//StatusBar_Icons//Icon_LTE_3.png";
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
