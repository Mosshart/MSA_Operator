using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Microsoft.Maps.MapControl.WPF;

namespace Map.Core
{
    class MapInteractivity
    {
        #region setView
        public static readonly DependencyProperty MapViewChangeProperty = DependencyProperty.RegisterAttached(
            "MapViewChange",
            typeof(Location),
            typeof(MapInteractivity),
            new UIPropertyMetadata(null, new PropertyChangedCallback(OnSetMapViewChanged)));

        public static void SetMapViewChange(DependencyObject target, Location value)
        {
            target.SetValue(MapViewChangeProperty, value);
        }

        private static void OnSetMapViewChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Microsoft.Maps.MapControl.WPF.Map map = (Microsoft.Maps.MapControl.WPF.Map) o;
            Location loc = (e.NewValue as Location);

            if (loc.Latitude == 0 && loc.Longitude == 0 || (e.NewValue as Location) == null)
                return;

            try
            {
                // var a = map.ZoomLevel; // min 3.14 max ...
                map.SetView(loc, 16);
            }
            catch (Exception ex){}
        }
        #endregion

        #region setRoute
        public static readonly DependencyProperty MapRouteChangeProperty = DependencyProperty.RegisterAttached(
            "MapRouteChange",
            typeof(LocationCollection),
            typeof(MapInteractivity),
            new UIPropertyMetadata(null, new PropertyChangedCallback(OnDrawRouteOnMapChanged)));

        public static void SetMapRouteChange(DependencyObject target, LocationCollection pointsCollection)
        {
            target.SetValue(MapRouteChangeProperty, pointsCollection);
        }

        private static void OnDrawRouteOnMapChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Microsoft.Maps.MapControl.WPF.Map map = (Microsoft.Maps.MapControl.WPF.Map)o;
            LocationCollection loc = (e.NewValue as LocationCollection);

            UIElementCollection a = map.Children;
            foreach(var x in a)
            {
                if(x.GetType() == typeof(MapPolyline))
                {
                    map.Children.Remove((MapPolyline)x);
                    break;
                }
            }
            // map.Children.Remove(a);
            MapPolyline routeLine = new MapPolyline()
            {
                Locations = loc,
                Stroke = new SolidColorBrush(Colors.Blue),
                StrokeThickness = 5
            };

            map.Children.Add(routeLine);                
        }

        #endregion

        #region AddPinInCenter
        public static readonly DependencyProperty AddPinInCenterProperty = DependencyProperty.RegisterAttached(
             "AddPinInCenterChanged",
             typeof(bool),
             typeof(MapInteractivity),
             new UIPropertyMetadata(true, new PropertyChangedCallback(OnAddPinInCenterChanged)));

        public static void SetAddPinInCenterChanged(DependencyObject target, bool value)
        {
            target.SetValue(AddPinInCenterProperty, value);
        }

        private static void OnAddPinInCenterChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Microsoft.Maps.MapControl.WPF.Map map = (Microsoft.Maps.MapControl.WPF.Map)o;
            Location loc = (e.NewValue as Location);
            Point centerPoint = new Point((map.ActualWidth / 2), (map.ActualHeight / 2));
            Location pinLocation = map.ViewportPointToLocation(centerPoint);


            Canvas midPin = new Canvas();
            Image midPinImage = new Image();
            midPinImage.Source = new BitmapImage(new Uri(@"/Images/Icon_Destination.png", UriKind.Relative));
            midPin.Children.Add(midPinImage);
            midPin.Width = 30;
            midPin.Height = 30;
        


            //MapLayer xx = new MapLayer();

            // Image pin = new Image();

            // pin.Source = new BitmapImage(new Uri(@"/Images/Icon_Destination.png", UriKind.Relative));
            //xx.AddChild(pin, loc);
            // Pushpin x = (Pushpin)pin;
            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;

          
           // string template = "<ControlTemplate><Image Source='../Images/Icon_Destination.png'/></ControlTemplate>";
           // pin.Template = (ControlTemplate)XamlReader.Parse(template);
            // ControlTemplate template = new ControlTemplate(typeof(Canvas));
            // template.
            // pin.Template = midPin;
            // Adds the pushpin to the map.
            // var childrens = map.Children;
            map.Children.Add(pin);
        }
        #endregion

        /*

          #region xa
          public static readonly DependencyProperty AddPinInCenterProperty = DependencyProperty.RegisterAttached(
              "AddPinInCenter",
              typeof(Location),
              typeof(MapInteractivity),
              new UIPropertyMetadata(null, new PropertyChangedCallback(OnAddPinInCenterChanged)));

          public static void AddPinInCenterChange(DependencyObject target, Location pinLocation)
          {
              target.SetValue(AddPinInCenterProperty, pinLocation);
          }

          private static void OnAddPinInCenterChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
          {
              Microsoft.Maps.MapControl.WPF.Map map = (Microsoft.Maps.MapControl.WPF.Map)o;
              LocationCollection loc = (e.NewValue as LocationCollection);

          }

          #endregion
        */
    }
}