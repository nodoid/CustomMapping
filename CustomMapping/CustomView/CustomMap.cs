using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CustomMapping
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty MapPinLocationProperty =
            BindableProperty.Create<CustomMap, MyLatLong>(p => p.MapPinLocation, new MyLatLong { Latitude = 53.4299, Longitude = -2.9615 });

        public MyLatLong MapPinLocation
        {
            get {
                return (MyLatLong)GetValue(MapPinLocationProperty);
            }
            set {
                SetValue(MapPinLocationProperty, value);
            }
        }

        private MapSpan visibleRegion;

        public MapSpan LastMoveToRegion { get; private set; }

        public new MapSpan VisibleRegion
        {
            get { return visibleRegion; }
            set {
                if (visibleRegion == value)
                {
                    return;
                }
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                OnPropertyChanging("VisibleRegion");
                visibleRegion = value;
                OnPropertyChanged("VisibleRegion");
            }
        }

        public CustomMap()
        {
        }

        public CustomMap(MapSpan map)
        {
            visibleRegion = map;
        }
    }
}

