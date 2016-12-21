using SmaNa.Model;
using SmaNa.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private static int _rowHeight = 60;
        public AppointmentOverview()
        {
            InitializeComponent();

            viewModel = new ViewModelOverview();
            // Item-Binding in Code, all Appointments in the Lists are Displayed in the GUI.
            PlannedAppointmentList.ItemsSource = viewModel.PlannedAppointments;
            DoneAppointmentList.ItemsSource = viewModel.DoneAppointments;
            FixedAppointmentList.ItemsSource = viewModel.FixedAppointments;
            //// Attaching ChangeListeners
            //viewModel.PlannedAppointments.CollectionChanged += PlannedAppointments_CollectionChanged;
            //viewModel.FixedAppointments.CollectionChanged += FixedAppointments_CollectionChanged; ;
            //viewModel.DoneAppointments.CollectionChanged += DoneAppointments_CollectionChanged;
            //// At Load-Time we want to actually load the list which happens on Change.
            //PlannedAppointments_CollectionChanged(null, null);
            //FixedAppointments_CollectionChanged(null, null);
            //DoneAppointments_CollectionChanged(null, null);
            //Displays a button in the navbar to add a new Appointment
            ToolbarItems.Add(new ToolbarItem(Multilanguage.TranslateExtension.getString("addNewAppointment"), "", async () =>
            {
                var edit = new AppointmentEdit();
                await Navigation.PushAsync(edit);
            }));
        }

        //private void DoneAppointments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    setListViewHeight(DoneAppointmentList, viewModel.DoneAppointments, DoneLabel);
        //}

        //private void FixedAppointments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    setListViewHeight(FixedAppointmentList, viewModel.FixedAppointments, FixedLabel);
        //}

        //private void PlannedAppointments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    setListViewHeight(PlannedAppointmentList, viewModel.PlannedAppointments, PlannedLabel);
        //}
        ///// <summary>
        ///// sets the lv's height according to the lists count. if count==0 sets visibility of lv and titleLabel to unvisible, otherwhise to visible.
        ///// </summary>
        ///// <param name="lv"></param>
        ///// <param name="list"></param>
        ///// <param name="titleLabel"></param>
        //private void setListViewHeight(ListView lv, ObservableCollection<Appointment> list, Label titleLabel)
        //{
        //    var count = list.Count;
        //    if (count > 0)
        //    {
        //        lv.IsVisible = true;
        //        titleLabel.IsVisible = true;
        //        //lv.HeightRequest = count * _rowHeight;
        //    }
        //    else
        //    {
        //        lv.IsVisible = false;
        //        titleLabel.IsVisible = false;
        //    }
        //}

        public async void OnAppointmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // the event is fired when you select and deselct an item. Because the edit menu only opens when we select the item we return here.
            if (e.SelectedItem == null)
            {
                return;
            }

            await Navigation.PushAsync(new AppointmentEdit((Appointment)e.SelectedItem));
            DoneAppointmentList.SelectedItem = null;
            FixedAppointmentList.SelectedItem = null;
            PlannedAppointmentList.SelectedItem = null;
        }
    }
}
