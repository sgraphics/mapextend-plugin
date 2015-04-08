using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Xam.Plugin.MapExtend.WindowsPhone;

namespace HowUseMapExtend.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            global::Xamarin.Forms.Forms.Init();

            string applicationId = "c8c9b802-012d-486d-a7a0-a991d78b4d04";
            string authToken = "BJwZ5VMpoCBJID0f-Hb7AQ";
            MapExtendRenderer.Init(applicationId, authToken);

            LoadApplication(new HowUseMapExtend.App());
        }
    }
}
