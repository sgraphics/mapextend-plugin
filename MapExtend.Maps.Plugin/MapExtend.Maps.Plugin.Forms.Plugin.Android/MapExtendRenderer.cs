using Xam.Plugin.MapExtend.Abstractions;
using System;
using Xamarin.Forms;
using Xam.Plugin.MapExtend.Droid;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Maps;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.App;
using Xamarin;


[assembly: ExportRenderer(typeof(Xam.Plugin.MapExtend.Abstractions.MapExtend), typeof(Xam.Plugin.MapExtend.Droid.MapExtendRenderer))]
namespace Xam.Plugin.MapExtend.Droid
{
    /// <summary>
    /// MapExtend.Maps.Plugin Implementation
    /// </summary>
    public class MapExtendRenderer : MapRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init(Activity activity, Android.OS.Bundle bundle)
        {
            FormsMaps.Init(activity, bundle);
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            var formsMap = (Xam.Plugin.MapExtend.Abstractions.MapExtend)Element;
            var androidMapView = (Android.Gms.Maps.MapView)Control;


            if (formsMap != null)
            {
                ((System.Collections.ObjectModel.ObservableCollection<Xamarin.Forms.Maps.Pin>)formsMap.Pins).CollectionChanged += OnPinsCollectionChanged;

                ((ObservableRangeCollection<Position>)formsMap.polilenes).CollectionChanged += OnPolCollectionChanged;

                androidMapView.Map.MarkerDragEnd += Map_MarkerDragEnd;
                androidMapView.Map.MapLongClick += (s, a) =>
                {
                    formsMap.Pins.Add(new Pin
                    {
                        Label = "Meu Endere�o",
                        Position = new Position(a.Point.Latitude, a.Point.Longitude)
                    }
                    );
                };
            }
        }

        private void Map_MarkerDragEnd(object sender, Android.Gms.Maps.GoogleMap.MarkerDragEndEventArgs e)
        {

        }

        private void OnPolCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            createLines();
        }

        private void createLines()
        {
            try
            {
                var androidMapView = (MapView)Control;
                var formsMap = (Xam.Plugin.MapExtend.Abstractions.MapExtend)Element;
                //androidMapView.Map.Clear();
                PolylineOptions line = new PolylineOptions();
                line.InvokeColor(global::Android.Graphics.Color.Blue);
                foreach (var item in formsMap.polilenes)
                {

                    LatLng pos = new LatLng(item.Latitude, item.Longitude);
                    line.Add(pos);
                }
                androidMapView.Map.AddPolyline(line);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private void OnPinsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            updatePins();
        }

        private void updatePins()
        {
			//var androidMapView = (MapView)Control;
			//var formsMap = (Xam.Plugin.MapExtend.Abstractions.MapExtend)Element;

			//androidMapView.Map.Clear();


			//androidMapView.Map.MyLocationEnabled = formsMap.IsShowingUser;


			//var items = formsMap.Pins;

			//foreach (var item in items)
			//{
			//	var markerWithIcon = new MarkerOptions();
			//	markerWithIcon.SetPosition(new LatLng(item.Position.Latitude, item.Position.Longitude));
			//	markerWithIcon.SetTitle(string.IsNullOrWhiteSpace(item.Label) ? "-" : item.Label);

			//	markerWithIcon.Draggable(true);

			//	androidMapView.Map.AddMarker(markerWithIcon);
			//}
        }
    }
}
