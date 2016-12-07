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
        public string Name { get; set; }
        public string Doctor { get; set; }
        public string Location { get; set; }
        public bool AppointmentFixed { get; set; }
        public bool AppointmentDone { get; set; }
        public bool AppointmentReminder { get; set; }
        public DateTime AppointmentDate { get; set; }

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
