﻿using Xam.Plugin.MapExtend.Abstractions;
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



            if (formsMap != null)
            {
                ((System.Collections.ObjectModel.ObservableCollection<Xamarin.Forms.Maps.Pin>)formsMap.Pins).CollectionChanged += OnPinsCollectionChanged;

                ((System.Collections.ObjectModel.ObservableCollection<Position>)formsMap.polilenes).CollectionChanged += OnPolCollectionChanged;

            }
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
                line.StrokeThickness = 15;
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
            var winPhoneMapView = Control;
            var formsMap = (Xam.Plugin.MapExtend.Abstractions.MapExtend)Element;




            var items = formsMap.Pins;

            foreach (var item in items)
            {
                MapLayer myLayer = new MapLayer();
                var markerWithIcon = new Pushpin();
                markerWithIcon.GeoCoordinate = new GeoCoordinate(item.Position.Latitude, item.Position.Longitude);
                markerWithIcon.Content = (string.IsNullOrWhiteSpace(item.Label) ? "-" : item.Label);
                markerWithIcon.AllowDrop = true;
                MapOverlay myOverlay = new MapOverlay();
                myOverlay.Content = markerWithIcon;
                myOverlay.GeoCoordinate = markerWithIcon.GeoCoordinate;
                myLayer.Add(myOverlay);

                winPhoneMapView.Layers.Add(myLayer);
            }
        }
    }
}
