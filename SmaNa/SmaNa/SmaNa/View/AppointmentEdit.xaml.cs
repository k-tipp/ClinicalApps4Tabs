using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmaNa.Model;
using Xamarin.Forms;
using SmaNa.ViewModel;
using SmaNa.LocalDataAccess;

namespace SmaNa.View
{
    /// <summary>
    /// This pages allows to Create a new appointment or edit an existing one.
    /// @created: Marwin Philips
    /// </summary>
    public partial class AppointmentEdit : ContentPage
    {

        private ViewModelAppointmentEdit _viewModel;

        /// <summary>
        /// Call this constructor when you want to edit an existing Appointment
        /// </summary>
        /// <param name="Appointment">The appointment to edit</param>
        public AppointmentEdit(Appointment Appointment)
        {
            InitializeComponent();
            
            _viewModel = new ViewModelAppointmentEdit(Appointment);
            addToolbarItems();
            bindData();
        }
        /// <summary>
        /// Call this constructor when you want to create a new Appointment
        /// </summary>
        public AppointmentEdit()
        {
            InitializeComponent();
            _viewModel = new ViewModelAppointmentEdit();
            addToolbarItems();
            bindData();
        }
        /// <summary>
        ///  set all the Attributes of an Appointment to the GUI-Controls
        /// </summary>
        private void bindData()
        {
            var Appointment = _viewModel.Appointment;
            AppointmentTitle.Text = Appointment.Name;
            AppointmentName.Text = Appointment.Name;
            AppointmentPeriode.Date = Appointment.AppointmentPeriode;
            AppointmentDate.Date = Appointment.AppointmentDate;
            AppointmentDoctor.Text = Appointment.Doctor;
            AppointmentLocation.Text = Appointment.Location;
            AppointmentFixed.IsToggled = Appointment.AppointmentFixed;
            AppointmentReminder.IsToggled = Appointment.AppointmentReminder;
            AppointmentDone.IsToggled = Appointment.AppointmentDone;
        }

        private void addToolbarItems()
        {
            ToolbarItems.Add(new ToolbarItem(Multilanguage.TranslateExtension.getString("saveAppointment"), "", () => Save()));
        }
        /// <summary>
        /// Saves all the Content from the view into the object from the viewModel and stores it to the Database over the viewModel.
        /// Navigates back to the Overview.
        /// </summary>
        private void Save()
        {
            Appointment editedAppointment = _viewModel.Appointment;
            editedAppointment.AppointmentDate = AppointmentDate.Date;
            editedAppointment.AppointmentDone = AppointmentDone.IsToggled;
            editedAppointment.AppointmentFixed = AppointmentFixed.IsToggled;
            editedAppointment.AppointmentPeriode = AppointmentPeriode.Date;
            editedAppointment.AppointmentReminder = AppointmentReminder.IsToggled;
            if(editedAppointment.AppointmentReminder)
            {
                App.DroidNotificationManager.createNotification(editedAppointment);
            }

            editedAppointment.Doctor = AppointmentDoctor.Text;
            editedAppointment.Location = AppointmentLocation.Text;
            editedAppointment.Name = AppointmentName.Text;
            _viewModel.Save();
            Navigation.PopAsync();
        }
    }
}
