using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using SmaNa.LocalDataAccess;
/// <summary>
/// @created: Marwin Philips
/// ViewModel for the AppointmentOverview which contains a list of all Appointments.
/// </summary>
namespace SmaNa.ViewModel
{
    class ViewModelOverview
    {
        private XMLAccess<ObservableCollection<Appointment>> _xmlAccess;
        /// <summary>
        /// All Appointments are stored in this list.
        /// </summary>
        public static BulkObservableCollection<Appointment> Appointments { get; set; }

        public ObservableCollection<Appointment> PlannedAppointments { get; private set; }
        public ObservableCollection<Appointment> FixedAppointments { get; private set; } 
        public ObservableCollection<Appointment> DoneAppointments { get; private set; }

        /// <summary>
        /// Constructor for ViewModelOverview which initializes all Lists and loads the static Appointments-List if it's not set already.
        /// </summary>
        public ViewModelOverview()
        {
            _xmlAccess = new XMLAccess<ObservableCollection<Appointment>>("Appointments");
            PlannedAppointments = new ObservableCollection<Appointment>();
            FixedAppointments = new ObservableCollection<Appointment>();
            DoneAppointments = new ObservableCollection<Appointment>();
            if (Appointments == null)
            {
                Reload();
            }
            Regroup();
            Appointments.CollectionChanged += new NotifyCollectionChangedEventHandler(SaveData);
        }
        /// <summary>
        /// Saves the Data to the database. No Params needed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveData(object sender, NotifyCollectionChangedEventArgs e)
        {
            _xmlAccess.Save(new ObservableCollection<Appointment>(Appointments));
            App.NotificationEventReceiver.UpdateAlarms(Appointments.Where(appointment => appointment.AppointmentReminder && !appointment.AppointmentDone));
            Regroup();
        }


        public static Appointment GetAppointment(Guid AppointmentID)
        {
            return Appointments.First<Appointment>(x => x.AppointmentID.Equals(AppointmentID)); 
        }

        /// <summary>
        /// Reloads the data from the XML-File. If there is no data, creates a default set of appointments.
        /// </summary>
        private void Reload()
        {
            ObservableCollection<Appointment> loadedList;
            try
            {
                loadedList = _xmlAccess.Load();
                if (loadedList == null) Appointments = new BulkObservableCollection<Appointment>();
                else Appointments = new BulkObservableCollection<Appointment>(loadedList.OrderBy(x => (x.AppointmentDate == null|| x.AppointmentDate==default(DateTime)) ? x.AppointmentPeriode : x.AppointmentDate));
                
            }
            catch
            {
                
            }
            

        }
        /// <summary>
        /// Fills the three display-lists with its content from the Appointments-List according to its actual state.
        /// </summary>
        private void Regroup()
        {
            PlannedAppointments.Clear();
            FixedAppointments.Clear();
            DoneAppointments.Clear();
            if (Appointments.Count == 0) return;
            foreach(Appointment a in Appointments)
            {
                switch (a.AppointmentState){
                    case Enumerations.AppointmentState.geplant:
                        PlannedAppointments.Add(a);
                        break;
                    case Enumerations.AppointmentState.abgemacht:
                        FixedAppointments.Add(a);
                        break;
                    case Enumerations.AppointmentState.durchgeführt:
                        DoneAppointments.Add(a);
                        break;
                }
            }
        }
    }
}
