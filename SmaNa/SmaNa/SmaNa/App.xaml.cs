using System;

using SmaNa.Multilanguage;
using Xamarin.Forms;
using System.Globalization;
using SmaNa.LocalDataAccess;
using SmaNa.MidataAccess;
using SmaNa.ViewModel;

/// <summary>
/// @created: Marwin Philips
/// Portable App which is the entry point for the generic app program.
/// </summary>
namespace SmaNa
{
    public partial class App : Application
    {
        // all the following variables are awailable in the entire app

        /// <summary>
        /// The FileManager can save and load Files from the local file system
        /// </summary>
        public static IFileManager FileManager { private set; get; }
        /// <summary>
        /// The NotificationEventReceiver is needed to manage Push-Messages in Android
        /// </summary>
        public static INotificationEventReceiver NotificationEventReceiver { private set; get; }
        /// <summary>
        /// With the Encrypter you can en- and decrypt Strings
        /// </summary>
        public static Encrypter Encrypter { private set; get; }
        /// <summary>
        /// The App-Settings are stored in the ViewModelSettings.Settings
        /// </summary>
        public static ViewModel.ViewModelSettings ViewModelSettings { private set; get; }
        /// <summary>
        /// If the PushNotifParameter is set it will open the Appointment with the GUID. Has to be the GUID of the Appointment you want to open.
        /// </summary>
        public static string PushNotifParameter { get; set; }
        /// <summary>
        /// Is the currently App, which is only once awailable
        /// </summary>
        public static App currentApp { get; set; }
        /// <summary>
        /// The MiData-Access is handled over this Object, which is initially logged in.
        /// </summary>
        public static MidataLogin Midata { get; set; }

        public App(string pushNotifParam = null)
        {
            PushNotifParameter = pushNotifParam;
            currentApp = this;
            InitializeComponent();

            // Load the FileAcces for secure Data Storage
            FileManager = DependencyService.Get<IFileManager>();
            NotificationEventReceiver = DependencyService.Get<INotificationEventReceiver>();
            var passwordManager = DependencyService.Get<IPasswordManager>();
            // loads the password from the platform dependent key manager
            var password = passwordManager.GetPassword();
            if(password == null)
            {
                // the first time we have to create a new password and save it.
                password = Encrypter.CreatePassword();
                passwordManager.SavePassword(password);
            }
            // With the initialized Password we can create the Encrypter, which is needed for any ViewModels access to data.
            Encrypter = new Encrypter(password);
            ViewModelSettings = new ViewModel.ViewModelSettings();

            // The Midata-Access is loging in right at the beginning so it is always available (until the Timeout)
            Midata = new MidataLogin();
            Midata.Login();
            //BodyWeight bw = new BodyWeight("85", DateTime.Now);
            //midataAccess.SaveWeight(bw);

            // Main Navigation for the whole app which works with a NavigationStack.
            if (ViewModelSettings.newSettings)
            {
                if (PushNotifParameter == null)
                {
                    ((NavigationPage)MainPage).PushAsync(new View.Settings());
                }
            }
        }
        
        /// <summary>
        /// Sets the Apps culture to ci
        /// </summary>
        /// <param name="ci"></param>
        public static void SetCulture(CultureInfo ci)
        {
            SmaNa.Multilanguage.AppResources.Culture = ci; // set the RESX for resource localization
            DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware method
            TranslateExtension.ci = ci;

            if (PushNotifParameter == null)
            {
                currentApp.MainPage = new NavigationPage(new View.MainMenu());
            }
            else
            {
                currentApp.MainPage = new NavigationPage(new View.MainMenu());
                ((NavigationPage)currentApp.MainPage).PushAsync(new View.AppointmentEdit(Guid.Parse(PushNotifParameter)));
                PushNotifParameter = null;
            }
        }
    }
}
