
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Android.Content.Res;
using Android.Content;
using SmaNa.Droid.PlatformDependent;
using System;

namespace SmaNa.Droid
{
    // the path for  for the android logo is set here
    [Activity(Label = "SmaNa", Icon = "@drawable/logo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        static MainActivity currentInstance;
        //DroidNotificationServiceConnection droidNotificationServiceConnection;
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
            currentInstance = this;
            //Console.WriteLine(this.ApplicationContext.ApplicationInfo.DataDir);


        }


        //protected override void OnStart()
        //{
        //    base.OnStart();
        //    var droidNotificationServiceIntent = new Intent(this, typeof(NotificationIntentService));
        //    droidNotificationServiceConnection = new DroidNotificationServiceConnection();
        //    BindService(droidNotificationServiceIntent, droidNotificationServiceConnection, Bind.AutoCreate);
        //    //var demoServiceIntent = new Intent("com.xamarin.DemoMessengerService");
        //    //demoServiceConnection = new DemoServiceConnection(this);
        //    //BindService(demoServiceIntent, demoServiceConnection, Bind.AutoCreate);
        //}

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();

        //    if (droidNotificationServiceConnection.IsConnected)
        //    {
        //        UnbindService(droidNotificationServiceConnection);
        //    }
        //}

        public static MainActivity GetInstance()
        {
            return currentInstance;
        }


        //protected override void OnNewIntent(Intent intent)
        //{
        //    base.OnNewIntent(intent);
        //}

        // The following two methods disable the Landscape orientation so we only have Portrait mode enabled. 
        // Thank you https://forums.xamarin.com/discussion/43132/guide-lock-screen-orientation
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            switch (newConfig.Orientation)
            {
                case Orientation.Landscape:
                    switch (Device.Idiom)
                    {
                        case TargetIdiom.Phone:
                            LockRotation(Orientation.Portrait);
                            break;
                        case TargetIdiom.Tablet:
                            LockRotation(Orientation.Landscape);
                            break;
                    }
                    break;
                case Orientation.Portrait:
                    switch (Device.Idiom)
                    {
                        case TargetIdiom.Phone:
                            LockRotation(Orientation.Portrait);
                            break;
                        case TargetIdiom.Tablet:
                            LockRotation(Orientation.Landscape);
                            break;
                    }
                    break;
            }
        }

        private void LockRotation(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Portrait:
                    RequestedOrientation = ScreenOrientation.Portrait;
                    break;
                case Orientation.Landscape:
                    RequestedOrientation = ScreenOrientation.Landscape;
                    break;
            }
        }

    }
}

