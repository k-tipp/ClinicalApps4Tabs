using SmaNa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmaNa.View
{
    public partial class AppointmentOverview : ContentPage
    {
        ViewModelOverview viewModel { get; set; }
        public AppointmentOverview()
        {
            InitializeComponent();
            viewModel = new ViewModelOverview();
            AppointmentList.ItemsSource = viewModel.Appointments;
            ToolbarItems.Add(new ToolbarItem(Multilanguage.TranslateExtension.getString("addNewAppointment"), "", async () => await Navigation.PushAsync(new AppointmentEdit())));
        }

        public void onNewEntryClicked(object sender, EventArgs e)
        {

        }
    }
}
