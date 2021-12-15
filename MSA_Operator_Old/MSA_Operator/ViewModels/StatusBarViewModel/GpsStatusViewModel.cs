using Prism.Mvvm;

namespace MSA_Operator.ViewModels.StatusBarViewModel
{
    class GpsStatusViewModel : BindableBase
    {


        private string _isGPS = @"/Images/StatusBar_Icons/Icon_GPS.png";
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
