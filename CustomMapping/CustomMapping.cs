using System;

using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;

namespace CustomMapping
{
    public class App : Application
    {
        public EventHandler<PositionErrorEventArgs> PositionError;
        public EventHandler<PositionEventArgs> PositionChanged;

        public MyLatLong MyPosition { get; set; }

        public Position CurrentPosition { get; set; }
        public static App Self { get; private set; }

        public static Size ScreenSize { get; set; }

        public App()
        {
            App.Self = this;

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = locator.GetPositionAsync(timeoutMilliseconds: 10000).ContinueWith((t) =>
           {
               if (t.IsCompleted)
               {
                   if (MyPosition == null)
                       MyPosition = new MyLatLong();
                   MyPosition.Latitude = t.Result.Latitude;
                   MyPosition.Longitude = t.Result.Longitude;
               }
           }).ConfigureAwait(true);

            PositionChanged += (object sender, PositionEventArgs e) =>
            {
                MyPosition.Latitude = e.Position.Latitude;
                MyPosition.Longitude = e.Position.Longitude;
                MessagingCenter.Send<App>(this, "LocChange");
            };

            PositionError += (object sender, PositionErrorEventArgs e) =>
            {
                MessagingCenter.Send<App, string>(this, "LocError", e.Error.ToString());
            };

            CrossGeolocator.Current.StartListeningAsync(1, 50, false);

            MainPage = new NavigationPage(new MappingPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

