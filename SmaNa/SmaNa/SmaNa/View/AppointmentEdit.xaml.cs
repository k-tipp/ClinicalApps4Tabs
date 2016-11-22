using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <summary>
        /// Delegate-Function which is called when the view is closed due to save or abort action
        /// </summary>
        Func<string> OnClose;

        ViewModel.ViewModelAppointmentEdit ViewModel;

        /// <summary>
        /// Call this constructor when you want to edit an existing Appointment
        /// </summary>
        /// <param name="OnClose">The function called right before this page is closed </param>
        /// <param name="Appointment">The appointment to edit</param>
        public AppointmentEdit(Func<string> OnClose, Appointment Appointment)
        {
            InitializeComponent();
            this.OnClose = OnClose;
            
            ViewModel = new ViewModelAppointmentEdit(Appointment);
            // set all the Attributes of an Appointment
            AppointmentName.Text = Appointment.Name;
            addToolbarItems();
        }
        /// <summary>
        /// Call this constructor when you want to create a new Appointment
        /// </summary>
        /// <param name="Function"></param>
        public AppointmentEdit(Func<string> OnClose)
        {
            InitializeComponent();
            this.OnClose = OnClose;
            ViewModel = new ViewModelAppointmentEdit();
            addToolbarItems();
        }

        private void addToolbarItems()
        {
            ToolbarItems.Add(new ToolbarItem(Multilanguage.TranslateExtension.getString("saveAppointment"), "", () => Save()));
        }

        private void Save()
        {
            Appointment editedAppointment = ViewModel.Appointment;
            editedAppointment.Name = AppointmentName.Text;
            ViewModel.Save();
            OnClose();
            Navigation.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            OnClose();
            return base.OnBackButtonPressed();
        }
    }
}
