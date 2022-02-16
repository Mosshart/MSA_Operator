using Prism.Mvvm;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace StatusBar.ViewModels
{
    /// <summary>
    /// Viewmodel of gps control
    /// </summary>
    public class GPSViewModel : BindableBase
    {
        public GPSViewModel()
        {
           
        }
        private string _isGPS = @"../Images/Icon_GPS.png";
        /// <summary>
        /// get/set gps text
        /// </summary>
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
