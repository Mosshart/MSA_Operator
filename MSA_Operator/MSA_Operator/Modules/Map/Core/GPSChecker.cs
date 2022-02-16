using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Map.Core
{
    class GPSChecker
    {
        double latToshow = 0.0;
        double longToshow = 0.0;
        int statusCounter = 0;
        int gpsCounter = 0;
        public bool isConnected = true;
        string timeThatGpsChanged = "";
        GeoCoordinateWatcher watcher;

        public GPSChecker()
        {
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);

            watcher.PositionChanged += (sender, e) =>
            {
                latToshow = e.Position.Location.Latitude;
                longToshow = e.Position.Location.Longitude;
                var coordinate = e.Position.Location;
                timeThatGpsChanged = DateTime.Now.ToString("HH:mm:ss");
                /*    Console.WriteLine("Count: {2} - Lat: {0}, Long: {1}, At time: {2}", coordinate.Latitude,
                        coordinate.Longi
                tude, Count, timer);*/                
                gpsCounter++;
            };

            watcher.StatusChanged += (sender, e) =>
            {
                statusCounter++;
            };
            // Begin listening for location updates.
            watcher.Start();
        }
        private string temp = "";
        int coutnerTemp = -1;
        public void ShowConsoleText()
        {
            Console.Clear();
            Console.WriteLine("Lat: {0}", latToshow);
            Console.WriteLine("Long: {0}", longToshow);
            Console.WriteLine("StatusChangedCounter: {0}", statusCounter);
            Console.WriteLine("Time gps changed: {0}", timeThatGpsChanged);

            Console.WriteLine("---------TEST----------------");
            if (temp != watcher.Position.Location.ToString())
            {
                coutnerTemp++;
                temp = watcher.Position.Location.ToString();
            }
            temp = watcher.Position.Location.ToString();
            Console.WriteLine("TEST: {0} \r\n Zmian: {1}", temp, coutnerTemp);
        }

    }

}
