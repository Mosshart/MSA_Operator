using Prism.Mvvm;

namespace MSA_Operator.ViewModels.StatusBarViewModel
{
    class RobotSateliteStatusViewModel : BindableBase
    {
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
