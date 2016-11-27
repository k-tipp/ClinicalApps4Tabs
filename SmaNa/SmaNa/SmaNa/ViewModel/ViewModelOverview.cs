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
        XMLAccess<ObservableCollection<Appointment>> _xmlAccess;
        /// <summary>
        /// All Appointments are stored in this list.
        /// </summary>
        public static ObservableCollection<Appointment> Appointments { get; set; }
        public ViewModelOverview()
        {
            _xmlAccess = new XMLAccess<ObservableCollection<Appointment>>("Appointments");
            if (Appointments == null)
            {
                Reload();
            }
            Appointments.CollectionChanged += new NotifyCollectionChangedEventHandler(SaveData);
        }

        private void SaveData(object sender, NotifyCollectionChangedEventArgs e)
        {
            _xmlAccess.Save(Appointments);
        }

        public void Reload()
        {
            ObservableCollection<Appointment> loadedList;
            try {
                loadedList = _xmlAccess.Load();
                Appointments = new ObservableCollection<Appointment>(loadedList.OrderBy(x => x.AppointmentDate));
            }catch
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
                    Location = "Biel"},
                new Appointment() {
                    Name = "Koloskopie",
                    AppointmentDate = new DateTime(2017,11,1),
                    AppointmentDone = false,
                    AppointmentFixed = true,
                    Doctor = "Dr. Troyanski",
                    Location = "Biel"},
                new Appointment() {
                    Name = "CT Thorax-Abdomen-Becken",
                    AppointmentDate = new DateTime(2017,11,1),
                    AppointmentDone = false,
                    AppointmentFixed = true,
                    Doctor = "Dr. House",
                    Location = "Biel"}
            };
            }
        }
    }
}
