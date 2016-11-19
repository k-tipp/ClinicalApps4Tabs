using SmaNa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

/// <summary>
/// @created by Marwin Philips
/// View to display Appointments and start adding a new Appointment.
/// </summary>

namespace SmaNa.View
{
    public partial class AppointmentOverview : ContentPage
    {
        /*
        The ViewModel conatains all Data displayed on this view
        */
        ViewModelOverview viewModel { get; set; }
        public AppointmentOverview()
        {
            InitializeComponent();
            viewModel = new ViewModelOverview();
            // Item-Binding in Code, all Appointments in the List are Displayed in the GUI.
            AppointmentList.ItemsSource = viewModel.Appointments;
            //Displays a button in the navbar to add a new Appointment
            ToolbarItems.Add(new ToolbarItem(Multilanguage.TranslateExtension.getString("addNewAppointment"), "", async () => await Navigation.PushAsync(new AppointmentEdit())));
        }
    }
}
