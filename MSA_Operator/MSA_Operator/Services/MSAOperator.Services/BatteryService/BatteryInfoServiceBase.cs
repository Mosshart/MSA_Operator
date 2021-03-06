using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace MSAOperator.Services.BatteryService
{
    public class BatteryInfoServiceBase : INotifyPropertyChanged
    {
        #region private 
        private DispatcherTimer _timer;
        private int _percentCap;
        private string _batteryPercentText;
        #endregion
        #region public 
        /// <summary>
        /// Get/Set batter percent in number
        /// </summary>
        public int PercentCap
        {
            get => _percentCap;
            set { _percentCap = value; BatteryPercentText = value.ToString() + "%"; OnPropertyChanged(); }
        }
        /// <summary>
        /// Get/Set batter percent in string
        /// </summary>
        public string BatteryPercentText
        {
            get => _batteryPercentText;
            set { _batteryPercentText = value; OnPropertyChanged(); }
        }
        public IBatteryInfoData BatteryInfoData;
        private IBatteryInfoService BatteryInfoService;

        /// <summary>
        /// constructor battery info service, main logic for battery information
        /// </summary>
        /// <param name="service">Robot or operator device class</param>
        public BatteryInfoServiceBase(IBatteryInfoService service)
        {
            this.BatteryInfoService = service;
            PercentCap = -1;
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, args) =>
            {
                IBatteryInfoData data = this.BatteryInfoService.GetBatteryInformation();
                PercentCap = getPercentLevel(data.CurrentCapacity, data.FullChargeCapacity); //PercentCap - 1;//
                Console.WriteLine(BatteryPercentText);
            };
            _timer.Start();
        }
        #endregion
        /// <summary>
        /// calculates amount of battery to capacity percent 
        /// </summary>
        /// <param name="value">battery current capacity level</param>
        /// <param name="maxValue"> battery capacity</param>
        /// <returns>0-100 value</returns>
        public virtual int getPercentLevel(uint value, int maxValue)
        {
            return (int)Math.Round((float)(Convert.ToInt32(value) * 100) / maxValue);
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
