using Prism.Mvvm;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace StatusBar.ViewModels
{
    /// <summary>
    /// Viewmodel of sattelite counter control
    /// </summary>
    public class SatteliteViewModel : BindableBase
    {
        public SatteliteViewModel()
        {

        }
        private string _satelliteNumber = "3";
        /// <summary>
        /// get/set satelite number text
        /// </summary>
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
