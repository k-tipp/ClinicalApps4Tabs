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
        private Guid appointmentID;

        public Appointment Appointment { get; private set; }

        /// <summary>
        /// Use this constructor to create a new Appointment
        /// </summary>
        public ViewModelAppointmentEdit()
        {
            Appointment = new Appointment()
            {
                AppointmentDate = DateTime.Now,
                AppointmentPeriode = DateTime.Now,
                AppointmentReminder = false,
                AppointmentDone = false,
                AppointmentFixed = false,
                AppointmentID = Guid.NewGuid()
            };
        }
        /// <summary>
        /// The Appointment is used and will be edited.
        /// </summary>
        /// <param name="Appointment"></param>
        public ViewModelAppointmentEdit(Appointment Appointment)
        {
            this.Appointment = Appointment;
        }

        public ViewModelAppointmentEdit(Guid appointmentID)
        {
            this.appointmentID = appointmentID;
        }

        /// <summary>
        /// Saves the Appointment in the appointment list while holding its sorting after AppointmentDate.
        /// </summary>
        public void Save()
        {
            //currently we save a new Appointment in the static appointment-list, which is saved by the ViewModelOverview instance.
            var appointmentList = ViewModelOverview.Appointments;
            var listCount = appointmentList.Count;
            // we have to keep the appointmentList sorted after the AppointmentDate.
            if (listCount > 0)
            {
                // first we remove it. If it was not in the list, it doesn't matter (Remove returns false instead of true)
                appointmentList.Remove(Appointment);
                var current = appointmentList.ElementAt(0);
                // if the modified appointment is before the first one, we insert it right there.
                if (current.AppointmentDate > Appointment.AppointmentDate)
                {
                    appointmentList.Insert(0, Appointment);
                }
                else {
                    var Allbefore = appointmentList.Where(x => x.AppointmentDate < Appointment.AppointmentDate);
                    var index = Allbefore.Count();
                    if (index > 0) // we insert the Appointment right after the one which it's later than.
                    {
                        appointmentList.Insert(index, Appointment);
                    }
                    else // it has to be at the very end.
                    {
                        appointmentList.Add(Appointment);
                    }
                }
            }
            else // if there is just this appointment in the list we don't have to sort.
            {
                appointmentList.Add(Appointment);
            }
        }

    }
}
