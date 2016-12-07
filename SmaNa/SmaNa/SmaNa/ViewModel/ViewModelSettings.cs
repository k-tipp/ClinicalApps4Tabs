using SmaNa.LocalDataAccess;
using SmaNa.Model;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmaNa.Multilanguage;

namespace SmaNa.ViewModel
{
    public class ViewModelSettings
    {
        private XMLAccess<Settings> _xmlAccess;

        public static Settings SmaNaSettings;

        public bool newSettings { private set; get; }
        public ViewModelSettings()
        {
            _xmlAccess = new XMLAccess<Settings>("Settings");
            SmaNaSettings = _xmlAccess.Load();
            newSettings = false;
            // the first time no settings are set, so we have to initialize them.
            if (SmaNaSettings == null)
            {
                newSettings = true;
                SmaNaSettings = new Settings()
                {
                    TnmM = Enumerations.TnmM.M0,
                    TnmN = Enumerations.TnmN.N0,
                    TnmT = Enumerations.TnmT.T0,
                    OperationDate = new DateTime(),
                    StageingComplete = false
                };
                // determine the correct, supported .NET culture

                // See ILocalize for specific source
                // Access the platformdependant resources over the ILocalize-Interface to get the right cultureInformation
                if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
                {
                    SmaNaSettings.Language = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                    App.SetCulture(SmaNaSettings.Language);
                }
            }
            else
            {
                App.SetCulture(SmaNaSettings.Language);
            }
        }

        public void SaveSettings()
        {
            _xmlAccess.Save(SmaNaSettings);
        }
    }
}
