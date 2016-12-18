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

        private static string old_schema;

        public bool newSettings { private set; get; }
        public ViewModelSettings()
        {
            _xmlAccess = new XMLAccess<Settings>("Settings");
            SmaNaSettings = _xmlAccess.Load();
            newSettings = false;
            old_schema = null;
            // the first time no settings are set, so we have to initialize them.
            if (SmaNaSettings == null)
            {
                newSettings = true;
                SmaNaSettings = new Settings()
                {
                    //TnmM = Enumerations.TnmM.M0,
                    //TnmN = Enumerations.TnmN.N0,
                    //TnmT = Enumerations.TnmT.T0,
                    Schema = "",
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
                old_schema = SmaNaSettings.Schema;
            }
            //TODO initialize Appointments-List in a more clever way 
            if (ViewModelOverview.Appointments == null)
                new ViewModelOverview();
        }

        public void SaveSettings()
        {
            if (old_schema != SmaNaSettings.Schema)
            {
                CsvAccess csv = new CsvAccess(SmaNaSettings.Schema);
                var removeList = ViewModelOverview.Appointments.Where(x => x.Generated &&
                    x.AppointmentState == Enumerations.AppointmentState.geplant).ToList();

                foreach (var toRemove in removeList)
                {
                    ViewModelOverview.Appointments.Remove(toRemove);
                }
                var appointments = csv.Load(SmaNaSettings.OperationDate);
                foreach (Appointment appointment in appointments)
                {
                    ViewModelOverview.Appointments.Add(appointment);
                }

                old_schema = SmaNaSettings.Schema;
            }
            _xmlAccess.Save(SmaNaSettings);
        }
    }
}
