using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.ViewModel
{
    class ViewModelOverview
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        public ViewModelOverview()
        {
            Appointments = new ObservableCollection<Appointment>()
            {
                new Appointment() {
                    Name = "first Appointment",
                    AppointmentDate = new DateTime(2016,1,1),
                    AppointmentDone = false,
                    AppointmentFixed = true,
                    Doctor = "Dr. House"},
                new Appointment() {
                    Name = "second Appointment",
                    AppointmentDate = new DateTime(2016,6,1),
                    AppointmentDone = false,
                    AppointmentFixed = true,
                    Doctor = "Dr. House"},
                new Appointment() {
                    Name = "third Appointment",
                    AppointmentDate = new DateTime(2016,10,1),
                    AppointmentDone = false,
                    AppointmentFixed = true,
                    Doctor = "Dr. House"}
            };

        }


    }
}
