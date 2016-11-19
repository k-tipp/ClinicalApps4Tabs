using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.Model
{
    class Appointment
    {
        public String Name { get; set; }
        public String Doctor { get; set; }
        public String Location { get; set; }
        public bool AppointmentFixed { get; set; }
        public bool AppointmentDone { get; set; }
        public DateTime AppointmentDate { get; set; }

        public String GuiFirstLine { get { return Name + " (" + AppointmentDate.ToString("dd.MM.yyyy") + ")"; } }
        public String GuiSecondLine { get { return Doctor + ", " + Location; } }
    }
}
