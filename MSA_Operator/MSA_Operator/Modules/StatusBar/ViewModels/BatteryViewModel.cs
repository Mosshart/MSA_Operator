using MSAOperator.Services.BatteryService;
using Prism.Mvvm;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace StatusBar.ViewModels
{
    /// <summary>
    /// View model of battery control 
    /// </summary>
    public class BatteryViewModel : BindableBase
    {
        BatteryInfoServiceBase _batteryInfo = null; 
        public BatteryInfoServiceBase BatteryInfo
        {
            get => _batteryInfo;
            set
            {
                SetProperty(ref _batteryInfo, value);
            }
        }
        
        #region private
        private string _batteryColor = "#FFFFFF";
        private string _batteryTextColor = "#FFFFFF";
        private string _batteryPercentText;
        private int _batteryPercent;
        private ModelType _modelType;
        #endregion
        #region public
        /// <summary>
        /// Get/set battery icon color 
        /// </summary>
        public string BatteryColor
        {
            get { return _batteryColor; }
            set
            {
                SetProperty(ref _batteryColor, value);
            }
        }
        /// <summary>
        /// get/set battery information text color 
        /// </summary>
        public string BatteryTextColor
        {
            get { return _batteryTextColor; }
            set
            {
                SetProperty(ref _batteryTextColor, value);
            }
        }
        /// <summary>
        /// Get/set battery text
        /// </summary>
        public string BatteryPercentText
        {
            get { return _batteryPercent.ToString() + "%"; }
            set { SetProperty(ref _batteryPercentText, value); }
        }

        /// <summary>
        /// Get/set battery text integer
        /// </summary>
        public int BatteryPercent
        {
            get { return _batteryPercent; }
            set { SetProperty(ref _batteryPercent, value); }
        }
        /// <summary>
        /// Get/set battery type robot/operator
        /// </summary>
        public ModelType ModelType
        {
            get { return _modelType; }
            set
            {
                SetProperty(ref _modelType, value);

                try
                {
#if !DEBUG
                    if (value == ModelType.Operator)
                    {
                        BatteryInfo = new OperatorBatteryInfoService();
                    }
                    else if (value == ModelType.Robot)
                    {
                        BatteryInfo = new RobotBatteryInfoService(); // do, zmiany
                    }
#endif
                }
                catch
                {
                    _batteryInfo = null;
                }                
            }
        }
#endregion

    }
    /// <summary>
    /// Kind of battery
    /// </summary>
    public enum ModelType
    {
        Default = 0,
        Robot = 1,
        Operator = 2
    }
}