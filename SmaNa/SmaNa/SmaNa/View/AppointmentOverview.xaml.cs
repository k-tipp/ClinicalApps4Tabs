using SmaNa.Model;
using SmaNa.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            AppointmentList.ItemsSource = ViewModelOverview.Appointments;

            //Displays a button in the navbar to add a new Appointment
            ToolbarItems.Add(new ToolbarItem(Multilanguage.TranslateExtension.getString("addNewAppointment"), "", async () =>
            {
                var edit = new AppointmentEdit();
                await Navigation.PushAsync(edit);
            }));
        }

        public async void OnAppointmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // the event is fired when you select and deselct an item. Because the edit menu only opens when we select the item we return here.
            if (e.SelectedItem == null)
            {
                return;
            }

            await Navigation.PushAsync(new AppointmentEdit((Appointment)e.SelectedItem));
            AppointmentList.SelectedItem = null;
        }
    }
}
