using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.ViewModel
{
    /// <summary>
    /// Viewmodel for editing or creating an Appointment
    /// </summary>
    class ViewModelAppointmentEdit
    {
        public Appointment Appointment { get; set; }

        /// <summary>
        /// Use this constructor to create a new Appointment
        /// </summary>
        public ViewModelAppointmentEdit()
        {
            Appointment = new Appointment();
        }
        /// <summary>
        /// The Appointment is used and will be edited.
        /// </summary>
        /// <param name="Appointment"></param>
        public ViewModelAppointmentEdit(Appointment Appointment)
        {
            this.Appointment = Appointment;
        }
        public void Save()
        {
            //currently we save a new Appointment in the static appointment-list.
            Appointment inList = ViewModelOverview.Appointments.FirstOrDefault(x => x.Equals(Appointment));
            if(inList != null)
            {
            }
            else
            {
                ViewModelOverview.Appointments.Add(Appointment);
            }
        }

    }
}
