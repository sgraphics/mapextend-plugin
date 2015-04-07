# mapextend-plugin
MapExtend for Xamarin.Forms is a plugin for Xamarin.Forms allowing users to draw routes, reveal nearby locations, and place pins at certain addresses.

# Links
Nuget PCL: https://www.nuget.org/packages/Xamarin.Forms.MapExtend.Abstractions/
Nuget Android: https://www.nuget.org/packages/Xamarin.Forms.MapExtend.Android/
Nuget WindowsPhone: https://www.nuget.org/packages/MapExtend.Maps.Plugin.Forms.Plugin.WindowsPhone/


# Initialization

When adding MapExtend to a Xamarin.Forms application,Xamarin.Forms.MapExtend is a a separate NuGet package that you should add to every project in the solution. On Android, this also has a dependency on GooglePlayServices (another NuGet) and Xamarin.Forms.Maps (too) which is downloaded automatically  when you add Xamarin.Forms.MapExtend.

After installing the NuGet package, the following initialization code is required in each application project:

Xamarin.FormsMaps.Init();
MapExtend.Init()

This call should be made after the Xamarin.Forms.Forms.Init() method call. The Xamarin.Forms templates have this call in the following files for each platform:

iOS - AppDelegate.cs file, in the FinishedLaunching method.
Android - MainActivity.cs file, in the OnCreate method.
Windows Phone - MainPage.xaml.cs file, in the MainPage constructor.

Once the NuGet package has been added and the initialization method called inside each applcation, Xamarin.Forms.MapExtend APIs can be used in the common PCL or Shared Project code.

# Initialization

Platform Configuration

#iOS

Work On it

#Android

To use the Google Maps API v2 on Android you must generate an API key and add it to your Android project. Follow the instructions in the Xamarin doc on obtaining a Google Maps API v2 key. After following those instructions, paste the API key in the Properties/AndroidManifest.xml file (view source and find/update the following element):

<meta-data android:name="com.google.android.maps.v2.API_KEY" android:value="YoApiKey" />

Without a valid API key the maps control will display as a grey box on Android.
