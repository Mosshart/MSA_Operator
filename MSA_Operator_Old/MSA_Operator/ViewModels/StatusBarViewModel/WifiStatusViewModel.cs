using System;
using System.IO;
using System.Windows.Threading;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels.StatusBarViewModel
{
    class WifiStatusViewModel :BindableBase
    {
        public DispatcherTimer _timer;
        public WifiStatusViewModel()
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
            string resultString = @"/Images/StatusBar_Icons/Icon_Wifi.png";
            switch (status) {
                case 0:
                    resultString = @"/Images/StatusBar_Icons/Icon_Wifi_0.png";
                    break;
                case 1:
                    resultString = @"/Images/StatusBar_Icons/Icon_Wifi_1.png";
                    break;
                case 2:
                    resultString = @"/Images/StatusBar_Icons/Icon_Wifi_2.png";
                    break;
                case 3:
                    resultString = @"/Images/StatusBar_Icons/Icon_Wifi_3.png";
                    break;
                default:
                    resultString = @"/Images/StatusBar_Icons/Icon_Wifi.png";
                    break;

            }
            return projectDirectory + resultString;//"//Images//StatusBar_Icons//Icon_Wifi.png";
        }

        private string _wifiPower = @"/Images/StatusBar_Icons/Icon_Wifi.png";
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
