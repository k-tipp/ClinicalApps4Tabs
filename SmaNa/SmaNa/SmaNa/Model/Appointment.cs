using SmaNa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
/*
@created: Marwin Philips
POCO to store the information needed to modify an Appointment. 
*/
namespace SmaNa.Model
{
    public class Appointment
    {
        [XmlIgnore]
        public string Name { get
            {
                if (ViewModelSettings.SmaNaSettings.LanguageString.Equals("fr-CH"))
                {
                    return Name_F!=null? Name_F : Name_D;
                }
                else
                {
                    return Name_D;
                }
            }
            set
            {
                Name_D = value;
            }
        }

        public string Name_D { get; set; }
        public string Name_F { get; set; }
        public string Doctor { get; set; }
        public string Location { get; set; }
        /// <summary>
        /// if the appointment has a date set, it is fixed.
        /// </summary>
        [XmlIgnore]
        public bool AppointmentFixed
        {
            get
            {
                return !AppointmentDate.Equals(default(DateTime));
            }
        }
        /// <summary>
        /// If the appointment is in the past, it is done.
        /// </summary>
        [XmlIgnore]
        public bool AppointmentDone
        {
            get
            {
                return AppointmentFixed && DateTime.Compare(DateTime.Now, AppointmentDate) > 0;
            }
        }
        public bool AppointmentReminder { get; set; }
        public bool Generated { get; set; }
        public Guid AppointmentID { get; set; }

        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }

        public DateTime AppointmentPeriode { get; set; }

        public Enumerations.AppointmentState AppointmentState
        {
            get
            {
                if (AppointmentDone == true) return Enumerations.AppointmentState.durchgeführt;
                if (AppointmentFixed == true) return Enumerations.AppointmentState.abgemacht;
                return Enumerations.AppointmentState.geplant;
            }
        }
        [XmlIgnore]
        public string AppointmentStateString
        {
            get
            {
                return Multilanguage.TranslateExtension.getString(AppointmentState.ToString());
            }
        }
        [XmlIgnore]
        public string GuiFirstLine
        {
            get
            {
                var ret = Name;
                if (AppointmentDate == default(DateTime))
                {
                    if (AppointmentPeriode == default(DateTime))
                    {
                        return ret;
                    }
                    else
                    {
                        return ret + " (" + AppointmentPeriode.ToString("MMMM yyyy") + ")";
                    }
                }
                else
                {
                    return ret + " (" + AppointmentDate.ToString("dd.MM.yyyy") + ")";
                }

            }
        }

        [XmlIgnore]
        public string PushMessageHeader
        {
            get
            {
                var prefix = "";
                if (AppointmentDate == default(DateTime))
                {
                    if (SmaNa.ViewModel.ViewModelSettings.SmaNaSettings.LanguageString.Equals("fr-CH"))
                    {
                        prefix = "rendez-vous prévue: "+ AppointmentPeriode.ToString("MMMM yyyy");
                    }
                    else
                    {
                        prefix = "abzumachender Termin: " + AppointmentPeriode.ToString("MMMM yyyy");
                    }
                }
                else
                {
                    if (ViewModelSettings.SmaNaSettings.LanguageString.Equals("fr-CH"))
                    {
                        prefix = "Rendez-vous: " + AppointmentDate.ToString("dd.MM.yyyy HH:mm");
                    }
                    else
                    {
                        prefix = "Termin: " + AppointmentDate.ToString("dd.MM.yyyy HH:mm");
                    }
                }
                return prefix;
            }
        }

        [XmlIgnore]
        public string GuiSecondLine
        {
            get
            {
                var ret = Doctor;
                if (!string.IsNullOrEmpty(ret) && !string.IsNullOrEmpty(Location)) ret += ", " + Location;
                return ret;
            }
        }
    }
}
