using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CustomMapping
{
    public class MappingPage : ContentPage
    {
        CustomMap map;
        Pin pin;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<App, string>(this, "LocError", async (s, e) => await DisplayAlert("Mapping Error", e, "OK"));
            MessagingCenter.Subscribe<App>(this, "LocChange", (t) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    map.MoveToRegion(new MapSpan(new Position(App.Self.MyPosition.Latitude, App.Self.MyPosition.Longitude),
                                                 53.43800, 2.96764));
                });
            });
        }

        public MappingPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.OS == TargetPlatform.iOS)
                Padding = new Thickness(0, 20, 0, 0);

            if (App.Self.MyPosition == null)
            {
                App.Self.MyPosition = new MyLatLong
                {
                    Latitude = 53.4299,
                    Longitude = -2.9615
                };
            }

            map = new CustomMap(new MapSpan(new Position(App.Self.MyPosition.Latitude, App.Self.MyPosition.Longitude),
                                      53.43800, 2.96764))
            {
                HasZoomEnabled = true,
                HasScrollEnabled = true,
                MapType = MapType.Hybrid,
                HeightRequest = App.ScreenSize.Height,
                WidthRequest = App.ScreenSize.Width,
                IsShowingUser = true,
            };
            pin = new Pin
            {
                Label = "You are here",
                Position = new Position(53.4299, -2.9615),
            };
            map.Pins.Add(pin);

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { map }
            };

        }
    }
}


