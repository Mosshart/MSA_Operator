using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MSAOperator.Services
{
    public class WLanInfoService : INotifyPropertyChanged
    {

        /// <summary>
        /// Enum wifi power
        /// </summary>
        public enum WiFiPower
        {
            wifi_disable = 0,
            wifi_low = 1,
            wifi_mid = 2,
            wifi_high = 3,
            wifi_error = 4
        }

        #region private
        private DispatcherTimer _timer;
        private string _wifiName;
        private int _wifiPower;
        #endregion

        #region public 
        public string WifiName {
            get => _wifiName;
            private set
            {
                if (value != _wifiName)
                {
                    _wifiName = value;
                    OnPropertyChanged();
                }
            }
        }
        public int WifiPower
        {
            get => _wifiPower;
            private set
            {
                if (value != _wifiPower)
                {
                    _wifiPower = value;
                    OnPropertyChanged();
                }
            }
        }
        public WiFiPower WifiStatus = WiFiPower.wifi_disable;

        #endregion

        public WLanInfoService()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(0.5);
            _timer.Tick += (sender, args) =>
            {
                string name;
                int power;
                netshWifiInfo(out name, out power);
                UpdateStatus();
                if(WifiStatus != WiFiPower.wifi_error || WifiStatus != WiFiPower.wifi_disable)
                {
                    this.WifiName = name;
                    this.WifiPower = power;
                }
            };
            _timer.Start();
        }
        /// <summary>
        /// gets wifi information from netsh log
        /// </summary>
        /// <param name="name">SSID</param>
        /// <param name="power">Signal</param>
        private void netshWifiInfo(out string name, out int power)
        {
            string input = cmdNetshOpen(); 
            if (input.Contains(" There is no wireless interface on the system."))
            {
                WifiStatus = WiFiPower.wifi_disable;
                name = "No WIFI";
                power = 0;
            }else
            {
                string wifiName = "";
                string wifiPower = "";
                try
                {
                    //Name
                    wifiName = input.Substring(input.IndexOf("SSID"));
                    wifiName = wifiName.Substring(wifiName.IndexOf(":"));
                    int test = wifiName.IndexOf("\n");
                    wifiName = wifiName.Substring(2, test).Trim();
                    name = wifiName;
                    //power
                    wifiPower = input.Substring(input.IndexOf("Signal"));
                    wifiPower = wifiPower.Substring(wifiPower.IndexOf(":"));
                    wifiPower = wifiPower.Substring(2, wifiPower.IndexOf("\n")).Trim();
                    power = Int32.Parse(wifiPower.Trim('%'));
                    if (WifiStatus == WiFiPower.wifi_error) { 

                        WifiStatus = WiFiPower.wifi_disable;
                    }
                }
                catch
                {
                    WifiStatus = WiFiPower.wifi_error;
                    name = "Error WIFI";
                    power = 0;
                }
            }
        }
        /// <summary>
        /// open netsh and get info
        /// </summary>
        /// <returns></returns>
        private string cmdNetshOpen()
        {

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "netsh.exe";
            p.StartInfo.Arguments = "wlan show interfaces";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            return p.StandardOutput.ReadToEnd();

        }
        /// <summary>
        ///Update status of wifi power 
        /// disable/low/mid/high/error
        /// </summary>
        private void UpdateStatus()
        {
            if (WifiStatus == WiFiPower.wifi_error)
                return; 


            if (WifiPower == 0)
                WifiStatus = WiFiPower.wifi_disable;
            else if (WifiPower > 0 && WifiPower <= 33)
                WifiStatus = WiFiPower.wifi_low;            
            else if (WifiPower > 33 && WifiPower <= 66)
                WifiStatus = WiFiPower.wifi_mid;
            else if (WifiPower > 66 && WifiPower <= 100)
                WifiStatus = WiFiPower.wifi_high;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged
    }
}
