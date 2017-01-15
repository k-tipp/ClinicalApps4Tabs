using System;

using Xamarin.Forms;

namespace SmaNa.View
{
    /// <summary>
    /// Main Page for the application which provides the 3 Navigation Actions to Appointments, Operation and Settings.
    /// </summary>
    public partial class MainMenu : ContentPage
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());
        }

        async void OnAppointmentsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppointmentOverview());
        }
        async void OnOperationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Operation());

        }
    }
}
