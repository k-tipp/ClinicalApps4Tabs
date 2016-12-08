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
        public static ObservableCollection<Appointment> Appointments { get; set; }

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
            _xmlAccess.Save(Appointments);
            Regroup();
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
                Appointments = new ObservableCollection<Appointment>(loadedList.OrderBy(x => x.AppointmentDate));
            }
            catch
            {
            }
            if (Appointments == null)
            {
                // Currently we load all Data from this static list.
                Appointments = new ObservableCollection<Appointment>()
                    {
                        new Appointment() {
                            Name = "Klinische Untersuchung CEA-Titer",
                            AppointmentDate = new DateTime(2017,1,1),
                            AppointmentDone = false,
                            AppointmentFixed = true,
                            Doctor = "Dr. Meier",
                            Location = "Biel",
                            AppointmentPeriode = new DateTime(2017,1,1),
                            AppointmentReminder = false,
                            Generated = false
                        },
                        new Appointment() {
                            Name = "Koloskopie",
                            AppointmentDate = new DateTime(2017,11,1),
                            AppointmentDone = false,
                            AppointmentFixed = true,
                            Doctor = "Dr. Troyanski",
                            Location = "Biel",
                            AppointmentPeriode = new DateTime(2017,11,1),
                            AppointmentReminder = false,
                            Generated = false
                        },
                        new Appointment() {
                            Name = "CT Thorax-Abdomen-Becken",
                            AppointmentDate = new DateTime(2017,11,1),
                            AppointmentDone = false,
                            AppointmentFixed = true,
                            Doctor = "Dr. House",
                            Location = "Biel",
                            AppointmentPeriode = new DateTime(2017,11,1),
                            AppointmentReminder = false,
                            Generated = false
                        }
                    };
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
