using MSAOperator.Services;
using MSAOperator.Services.BatteryService;
using MSAOperator.Services.BatteryService.Operator;
using MSAOperator.Services.BatteryService.Robot;
using Prism.Commands;
using Prism.Mvvm;
//using StatusBar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace StatusBar.ViewModels
{
    public class BatteryViewModel : BindableBase
    {
        //  private DispatcherTimer _timer;
        BatteryInfoServiceBase _batteryInfo = null; 
        public BatteryInfoServiceBase BatteryInfo
        {
            get => _batteryInfo;
            set
            {
               /* if (ModelType != ModelType.Operator)
                    SetProperty(ref _batteryInfo, new OperatorBatteryInfoService());
                else */
                    SetProperty(ref _batteryInfo, value);
            }
        }
        public BatteryViewModel()
        {
            //_batteryInfo = batteryInfo;
        }
        #region private
        private string _batteryColor = "#FFFFFF";
        private string _batteryTextColor = "#FFFFFF";
        private string _batteryPercentText;
        private int _batteryPercent;
        private ModelType _modelType;
        #endregion
        #region public
        public string BatteryColor
        {
            get { return _batteryColor; }
            set
            {
                SetProperty(ref _batteryColor, value);
            }
        }
        public string BatteryTextColor
        {
            get { return _batteryTextColor; }
            set
            {
                SetProperty(ref _batteryTextColor, value);
            }
        }
        public string BatteryPercentText
        {
            get { return _batteryPercent.ToString() + "%"; }
            set { SetProperty(ref _batteryPercentText, value); }
        }

        public int BatteryPercent
        {
            get { return _batteryPercent; }
            set { SetProperty(ref _batteryPercent, value); }
        }

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

    public enum ModelType
    {
        Default = 0,
        Robot = 1,
        Operator = 2
    }
}