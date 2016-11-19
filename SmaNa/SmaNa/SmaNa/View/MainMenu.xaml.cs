using SmaNa.Multilanguage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmaNa.View
{
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
            CultureInfo ci = new CultureInfo("fr-CH");
            DependencyService.Get<ILocalize>().SetLocale(ci);
            AppResources.Culture = ci;
            await Navigation.PushAsync(new Operation());

        }
    }
}
