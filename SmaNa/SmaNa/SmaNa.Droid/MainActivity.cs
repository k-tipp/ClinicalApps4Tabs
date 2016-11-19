using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SmaNa.Droid
{
    // the path for  for the android logo is set here
    [Activity(Label = "SmaNa", Icon = "@drawable/logo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // Default code to get the app running
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());


            // Workaround needed to be able to use the light theme https://developer.xamarin.com/guides/xamarin-forms/themes/#loadtheme 
            var x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            // Removes the App-Icon from the Navigation
            ActionBar.SetIcon(Android.Resource.Color.Transparent);
        }
    }
}

