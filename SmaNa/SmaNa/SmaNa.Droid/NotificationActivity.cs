
using Android.App;
using Android.OS;
using Android.Content.PM;

namespace SmaNa.Droid
{
    [Activity(Label = "SmaNa", Icon = "@drawable/logo", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class NotificationActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string AppointmentID = this.Intent.GetStringExtra("AppointmentID");

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            // savedInstanceState is null therefor the next line fails.
            LoadApplication(new App(AppointmentID));
            // Create your application here

            var x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            // Removes the App-Icon from the Navigation
            ActionBar.SetIcon(Android.Resource.Color.Transparent);
            //currentInstance = this;
        }
    }
}