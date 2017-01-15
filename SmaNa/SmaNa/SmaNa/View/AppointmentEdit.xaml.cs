using System;
using SmaNa.Model;
using Xamarin.Forms;
using SmaNa.ViewModel;

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
        /// Call this constructor when you come from a pushed message
        /// </summary>
        /// <param name="anAppointmentID"></param>
        public AppointmentEdit(Guid anAppointmentID)
        {
            InitializeComponent();
            if (ViewModelOverview.Appointments == null)
                new ViewModelOverview();

            Appointment appointment = ViewModelOverview.GetAppointment(anAppointmentID);
            _viewModel = new ViewModelAppointmentEdit(appointment);
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
            DeleteLayout.IsVisible = false;
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
            // if the AppointmentDate is default, we display the button and not the whole date, so the user first has to click the Create Appointment button.
            if (Appointment.AppointmentDate.Equals(default(DateTime)))
            {
                setAppointmentDateVisibility(false);
            }
            else
            {
                setAppointmentDateVisibility(true);
                AppointmentTime.Time = Appointment.AppointmentDate.TimeOfDay;
            }
            AppointmentDate.Date = Appointment.AppointmentDate;
            AppointmentDoctor.Text = Appointment.Doctor;
            AppointmentLocation.Text = Appointment.Location;
            AppointmentReminder.IsToggled = Appointment.AppointmentReminder;
            // Manually generated Appointments can be deleted, their title can be set manually and you can modify the Periode.
            DeleteLayout.IsVisible = !Appointment.Generated;
            TitleLayout.IsVisible = Appointment.Generated;
            EditTitleLayout.IsVisible = !Appointment.Generated;

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
            // Comparing with default(DateTime) does not work because min datetime for datepicker is 01.01.1900
            if (!AppointmentDate.Date.Equals(new DateTime(599266080000000000)))
            {
                editedAppointment.AppointmentDate = editedAppointment.AppointmentDate
                    .Add(AppointmentTime.Time);
            }
            else
            {
                editedAppointment.AppointmentDate = new DateTime(0);
            }
            editedAppointment.AppointmentPeriode = AppointmentPeriode.Date;
            editedAppointment.AppointmentReminder = AppointmentReminder.IsToggled;

            editedAppointment.Doctor = AppointmentDoctor.Text;
            editedAppointment.Location = AppointmentLocation.Text;
            editedAppointment.Name = AppointmentName.Text;
            _viewModel.Save();
            Navigation.PopAsync();
        }

        private void setAppointmentDateVisibility(bool dateSet)
        {
            AppointmentDateButton.IsVisible = !dateSet;
            AppointmentDatePicker.IsVisible = dateSet;
        }
        public void OnSetDateClicked(object sender, EventArgs e)
        {
            setAppointmentDateVisibility(true);
            if (AppointmentPeriode.Date != new DateTime(599266080000000000))
            {
                AppointmentDate.Date = AppointmentPeriode.Date;
                AppointmentTime.Time = default(TimeSpan);
            }
            else {
                AppointmentDate.Date = DateTime.Now;
                AppointmentTime.Time = DateTime.Now.TimeOfDay;
            }
        }

        public void OnUnsetDateClicked(object sender, EventArgs e)
        {
            setAppointmentDateVisibility(false);
            AppointmentDate.Date = new DateTime(0);
            AppointmentTime.Time = default(TimeSpan);
            _viewModel.Appointment.AppointmentDate = new DateTime(0);
            _viewModel.Appointment.AppointmentTime = default(TimeSpan);
        }
        public void OnDeleteClicked(object sender, EventArgs e)
        {
            _viewModel.DeleteAppointment();
            Navigation.PopAsync();
        }
    }
}
