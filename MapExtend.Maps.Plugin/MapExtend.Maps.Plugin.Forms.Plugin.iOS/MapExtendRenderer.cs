using Xam.Plugin.MapExtend.Abstractions;
using System;
using Xamarin.Forms;
using Xam.Plugin.MapExtend.iOS;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.MapKit;
using System.Drawing;
using Xamarin.Forms.Maps;
using MonoTouch.CoreLocation;
using System.Collections.Generic;
using Xamarin;


[assembly: ExportRenderer(typeof(Xam.Plugin.MapExtend.Abstractions.MapExtend), typeof(Xam.Plugin.MapExtend.iOS.MapExtendRenderer))]
namespace Xam.Plugin.MapExtend.iOS
{
    /// <summary>
    /// MapExtend.Maps.Plugin Implementation
    /// </summary>
    public class MapExtendRenderer : ViewRenderer<Xam.Plugin.MapExtend.Abstractions.MapExtend, MKMapView>
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

            SetNativeControl(new MKMapView(RectangleF.Empty));
            var formsMap = Element;
            MKMapView mkMapView = Control;


            if (formsMap != null)
            {
                ((System.Collections.ObjectModel.ObservableCollection<Xamarin.Forms.Maps.Pin>)formsMap.Pins).CollectionChanged += OnPinsCollectionChanged;

                ((ObservableRangeCollection<Position>)formsMap.polilenes).CollectionChanged += OnPolCollectionChanged;

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
                var mkMapView = Control;
                var formsMap = (Xam.Plugin.MapExtend.Abstractions.MapExtend)Element;
                //androidMapView.Map.Clear();
                List<CLLocationCoordinate2D> lineCords = new List<CLLocationCoordinate2D>();
                if (formsMap.polilenes.Count > 0)
                {
                    foreach (var item in formsMap.polilenes)
                    {

                        lineCords.Add(new CLLocationCoordinate2D(item.Latitude, item.Longitude));
                    }
                    MKPolyline line = MKPolyline.FromCoordinates(lineCords.ToArray());
                    mkMapView.AddOverlay(line);
                    mkMapView.SetVisibleMapRect(line.BoundingMapRect, true);
                }

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
            var mkMapView = Control;
            var formsMap = (Xam.Plugin.MapExtend.Abstractions.MapExtend)Element;

            if (mkMapView.Annotations.Length > 0)
                mkMapView.RemoveAnnotations(mkMapView.Annotations);

            var items = formsMap.Pins;

            foreach (var item in items)
            {
                CLLocationCoordinate2D coord = new CLLocationCoordinate2D(item.Position.Latitude, item.Position.Longitude);

                MKPointAnnotation point = new MKPointAnnotation()
                {
                    Title = item.Label,
                    Coordinate = coord
                };

                mkMapView.AddAnnotation(point);

            }
        }
    }
}
