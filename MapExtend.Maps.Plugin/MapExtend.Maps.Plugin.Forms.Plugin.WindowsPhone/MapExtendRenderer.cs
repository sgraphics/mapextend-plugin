using Xam.Plugin.MapExtend.Abstractions;
using System;
using Xamarin.Forms;
using Xam.Plugin.MapExtend.WindowsPhone;
using Xamarin.Forms.Maps.WP8;
using Xamarin.Forms.Maps;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using Xamarin;
using Xamarin.Forms.Platform.WinPhone;
using System.Collections.Generic;
using System.Windows;

[assembly: ExportRenderer(typeof(Xam.Plugin.MapExtend.Abstractions.MapExtend), typeof(Xam.Plugin.MapExtend.WindowsPhone.MapExtendRenderer))]
namespace Xam.Plugin.MapExtend.WindowsPhone
{
    /// <summary>
    /// MapExtend.Maps.Plugin Implementation
    /// </summary>
    public class MapExtendRenderer : MapRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init(string appID, string authToken)
        {
            FormsMaps.Init(appID, authToken);


        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
        {
            base.OnElementChanged(e);

            var formsMap = (Xam.Plugin.MapExtend.Abstractions.MapExtend)Element;
            var winPhoneMapView = Control;
            winPhoneMapView.Tap += winPhoneMapView_Tap;


            if (formsMap != null)
            {
                ((System.Collections.ObjectModel.ObservableCollection<Xamarin.Forms.Maps.Pin>)formsMap.Pins).CollectionChanged += OnPinsCollectionChanged;
                ((System.Collections.ObjectModel.ObservableCollection<Position>)formsMap.polilenes).CollectionChanged += OnPolCollectionChanged;

            }


        }

        private void winPhoneMapView_Tap(object sender, System.Windows.Input.GestureEventArgs e)
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
                var winPhoneMapView = Control;
                var formsMap = (Xam.Plugin.MapExtend.Abstractions.MapExtend)Element;
                //androidMapView.Map.Clear();
                MapPolyline line = new MapPolyline();
                line.StrokeThickness = 5;
                foreach (var item in formsMap.polilenes)
                {

                    GeoCoordinate pos = new GeoCoordinate(item.Latitude, item.Longitude);
                    line.Path.Add(pos);
                }
                winPhoneMapView.MapElements.Add(line);
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
			//var winPhoneMapView = Control;
			//var formsMap = (Xam.Plugin.MapExtend.Abstractions.MapExtend)Element;

			//var items = formsMap.Pins;
			//var toRemove = new List<MapLayer>();
			//foreach (var item in winPhoneMapView.Layers)
			//{
			//	if (item.Count > 0)
			//		if (item[0].Content is Pushpin)
			//			toRemove.Add(item);
			//}
			//foreach (var item in toRemove)
			//{
			//	winPhoneMapView.Layers.Remove(item);
			//}

			//foreach (var item in items)
			//{
			//	MapLayer myLayer = new MapLayer();
			//	var markerWithIcon = new Pushpin();
			//	markerWithIcon.GeoCoordinate = new GeoCoordinate(item.Position.Latitude, item.Position.Longitude);
				
			//	markerWithIcon.Content = (string.IsNullOrWhiteSpace(item.Label) ? "-" : item.Label);

			//	//markerWithIcon.AllowDrop = true;

			//	MapOverlay myOverlay = new MapOverlay()
			//	{
			//		GeoCoordinate = markerWithIcon.GeoCoordinate,
			//		PositionOrigin = new System.Windows.Point(0.0, 1.0),
			//		Content = markerWithIcon
			//	};

			//	myLayer.Add(myOverlay);

			//	winPhoneMapView.Layers.Add(myLayer);
			//}
        }
    }

}
