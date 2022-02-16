using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Map.Core
{
    /// <summary>
    /// Interaction with map 
    /// </summary>
    class MapInteractivity
    {
        #region setView
        public static readonly DependencyProperty MapViewChangeProperty = DependencyProperty.RegisterAttached(
            "MapViewChange",
            typeof(Location),
            typeof(MapInteractivity),
            new UIPropertyMetadata(null, new PropertyChangedCallback(OnSetMapViewChanged)));
        
        /// <summary>
        /// Set new map location center
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
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

    }
}