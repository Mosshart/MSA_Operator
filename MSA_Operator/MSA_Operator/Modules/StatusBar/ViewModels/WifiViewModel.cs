using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Threading;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace StatusBar.ViewModels
{
    /// <summary>
    /// Viewmodel of wifi control
    /// </summary>
    public class WifiViewModel : BindableBase
    {
        
        private DispatcherTimer _timer;
        /// <summary>
        /// wifi status constructor 
        /// </summary>
        public WifiViewModel()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(0.5);
            _timer.Tick += (sender, args) =>
            {   
                WifiPower = setWifiPower(4);
                
            };
            _timer.Start();
        }

        private string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        private string setWifiPower(int status)
        {
            string resultString = @"../Images/Icon_Wifi.png";
            switch (status)
            {
                case 0:
                    resultString = @"../Images/Icon_Wifi_0.png";
                    break;
                case 1:
                    resultString = @"../Images/Icon_Wifi_1.png";
                    break;
                case 2:
                    resultString = @"../Images/Icon_Wifi_2.png";
                    break;
                case 3:
                    resultString = @"../Images/Icon_Wifi_3.png";
                    break;
                default:
                    resultString = @"../Images/Icon_Wifi.png";
                    break;

            }

            return resultString; //projectDirectory + resultString;//"//Images//StatusBar_Icons//Icon_Wifi.png";
        }

        private string _wifiPower = @"../Images/Icon_Wifi.png";
        /// <summary>
        /// get/set wifi power image as path to file in string
        /// </summary>
        public string WifiPower
        {
            get
            {
                return _wifiPower;
            }
            set
            {
                if (_wifiPower == value)
                    return;
                SetProperty(ref _wifiPower, value);
            }
        }
    }
}
