using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string GuiFirstLine { get { return Name + " (" + AppointmentDate.ToString("dd.MM.yyyy") + ")"; } }
        public string GuiSecondLine { get { return Doctor + ", " + Location; } }
    }
}
