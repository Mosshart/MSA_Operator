using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSAOperator.Services
{
    public class GeoLocalizationService : INotifyPropertyChanged
    {
       

        private double _latitude;
        /// <summary>
        ///  Current Latitude
        /// </summary>
        public double Latitude {
            get =>  _latitude;
            private set {
                if (_latitude != value)
                {
                    _latitude = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _longtitude;
        /// <summary>
        /// Current Longtutude
        /// </summary>
        public double Longtitude
        {
            get => _longtitude;
            private set
            {
                if (_longtitude != value)
                {
                    _longtitude = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isGPS;
        /// <summary>
        /// is gps valiable
        /// </summary>
        public bool IsGPS
        {
            get => _isGPS;
            private set
            {
                if (_isGPS != value)
                {
                    _isGPS = value;
                }
            }
        }

        private GeoCoordinateWatcher _watcher;

        public GeoLocalizationService()
        {
            _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            _watcher.PositionChanged += (sender, e) =>
            {
                Latitude = e.Position.Location.Latitude;
                Longtitude = e.Position.Location.Longitude;
                saveToFile();
            };
            _watcher.StatusChanged += (sender, e) =>
            {
                IsGPS = e.Status == GeoPositionStatus.Ready ? true : false;
            };
            _watcher.Start();
        }

        //save gps log to file format: Datetime || Latitude ,  Longtitude
        private void saveToFile()
        {
            using (StreamWriter writetext = new StreamWriter("GeoCoordinateLogs.txt"))
            {
                writetext.WriteLine("DT: {2} || Latitude: {0}, Longtitude: {0}", Latitude, Longtitude, DateTime.Now.ToString());
            }
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
