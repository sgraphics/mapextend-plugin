using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CoreLocation;
using MapKit;
using UIKit;
using Xam.Plugin.MapExtend.Abstractions;
using Xam.Plugin.MapExtend.iOSUnified;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MapExtend), typeof(MapExtendRenderer))]
namespace Xam.Plugin.MapExtend.iOSUnified
{
    /// <summary>
    /// MapExtend.Maps.Plugin Implementation
    /// </summary>
    public class MapExtendRenderer : ViewRenderer<Abstractions.MapExtend, MKMapView>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            FormsMaps.Init();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Abstractions.MapExtend> e)
        {
            base.OnElementChanged(e);

            var formsMap = Element;

            if (formsMap == null)
            {
                return;
            }

            var mapView = new MKMapView
            {
                MapType = MKMapType.Standard,
                ShowsUserLocation = formsMap.IsShowingUser,          
                ZoomEnabled = true,
                ScrollEnabled = true,
                ShowsBuildings = true,
                PitchEnabled = true,
            };

            var mapDelegate = new MapDelegate();

            mapView.Delegate = mapDelegate;

            SetNativeControl(mapView);

            ((ObservableCollection<Pin>)formsMap.Pins).CollectionChanged += OnPinsCollectionChanged;

            formsMap.polilenes.CollectionChanged += OnPolCollectionChanged;
        }

        private void OnPolCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CreateLines();
        }

        private void CreateLines()
        {
            try
            {
                var mkMapView = Control;
                var formsMap = Element;

                var lineCords = new List<CLLocationCoordinate2D>();

                if (formsMap.polilenes.Count <= 0)
                {
                    return;
                }

                foreach (var item in formsMap.polilenes)
                {
                    lineCords.Add(new CLLocationCoordinate2D(item.Latitude, item.Longitude));
                }

                var line = MKPolyline.FromCoordinates(lineCords.ToArray());

                mkMapView.AddOverlay(line);

                mkMapView.SetVisibleMapRect(line.BoundingMapRect, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void OnPinsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePins();
        }

        private void UpdatePins()
        {
            var mkMapView = Control;
            var formsMap = Element;
            var items = formsMap.Pins;

            foreach (var item in items)
            {
                var coord = new CLLocationCoordinate2D(item.Position.Latitude, item.Position.Longitude);

                var point = new MKPointAnnotation()
                {
                    Title = item.Label
                };

                point.SetCoordinate(coord);

                mkMapView.AddAnnotation(point);
            }
        }
    }

    class MapDelegate : MKMapViewDelegate
    {
        //Override OverLayRenderer to draw Polyline returned from directions
        public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlay)
        {
            if (!(overlay is MKPolyline))
            {
                return null;
            }

            var route = (MKPolyline)overlay;
            var renderer = new MKPolylineRenderer(route) { StrokeColor = UIColor.Blue };

            return renderer;
        }
    }
}
