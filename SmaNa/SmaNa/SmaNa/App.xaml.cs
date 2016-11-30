using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmaNa.Multilanguage;
using Xamarin.Forms;
using System.Globalization;
using SmaNa.LocalDataAccess;
using SmaNa.Model;
using System.Collections.ObjectModel;

/// <summary>
/// @created: Marwin Philips
/// Portable App which is the entry point for the generic app program.
/// </summary>
namespace SmaNa
{
    public partial class App : Application
    {
        public static IFileManager FileManager { private set; get; }
        public static Encrypter Encrypter { private set; get; }
        public static ViewModel.ViewModelSettings ViewModelSettings { private set; get; }
        public static App currentApp;

        public App()
        {
            currentApp = this;
            InitializeComponent();

            // Load the FileAcces for secure Data Storage
            FileManager = DependencyService.Get<IFileManager>();
            var passwordManager = DependencyService.Get<IPasswordManager>();
            // loads the password from the platform dependent key manager
            var password = passwordManager.GetPassword();
            if(password == null)
            {
                // the first time we have to create a new password and save it.
                password = Encrypter.CreatePassword();
                passwordManager.SavePassword(password);
            }
            Encrypter = new Encrypter(password);
            ViewModelSettings = new ViewModel.ViewModelSettings();

            // Main Navigation for the whole app which works with a NavigationStack.
            if (ViewModelSettings.newSettings)
            {
                ((NavigationPage)MainPage).PushAsync(new View.Settings());
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
            currentApp.MainPage = new NavigationPage(new View.MainMenu());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
