using Geolocator.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin.MapExtend.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HowUseMapExtend
{
    public class App : Application
    {
        public App()
        {
            var map = new MapExtend()
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var entryEnd = new Entry
            {
                Placeholder = "Adress",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var btnSearch = new Button
            {
                Text = "P",
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent
            };

            var btnCreateRoute = new Button
            {
                Text = "Route",
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent
            };

            var barItens = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            btnSearch.Clicked += async (sender, args) =>
            {
                await map.SearchAdress(entryEnd.Text).ContinueWith(t =>
                {
                    var d = t;
                });

            };

            barItens.Children.Add(entryEnd);
            barItens.Children.Add(btnSearch);
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(barItens);
            stack.Children.Add(map);

            btnCreateRoute.Clicked += async (sender, args) =>
            {
                await map.CreateRoute(map.Pins[0].Position, map.Pins[1].Position).ContinueWith(t =>
                {
                    var d = t;
                });
            };
            stack.Children.Add(btnCreateRoute);

            var btnNearbyLocation = new Button()
            {
                Text = "Nearby Pleaces"
            };

            btnNearbyLocation.Clicked += async (sender, args) =>
            {
                await map.NearbyLocations("AIzaSyBuATAkE41ioaMXd6MvWOmFlG2p-MlE6HM", "").ContinueWith(t =>
                {
                    var d = t;
                });

            };

            stack.Children.Add(btnNearbyLocation);

            var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 50;

            var geoLocation = locator.GetPositionAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    //System.Diagnostics.Debug.WriteLine("Error : {0}", ((GeolocationException)t.Exception.InnerException).Error.ToString());
                }
                else if (t.IsCanceled)
                {
                    System.Diagnostics.Debug.WriteLine("Error : The geolocation has got canceled !");
                }
                else
                {
                    var currentLocation = new Xamarin.Forms.Maps.Position(t.Result.Latitude, t.Result.Longitude);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(currentLocation, Xamarin.Forms.Maps.Distance.FromMiles(0.5)));
                        map.Pins.Add(new Pin { Label = "seu endereço", Position = currentLocation });
                    });
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            // The root page of your application
            MainPage = new ContentPage
            {
                Content = stack
            };
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
