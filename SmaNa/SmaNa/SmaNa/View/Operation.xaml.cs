using SmaNa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmaNa.View
{
    public partial class Operation : ContentPage
    {
        private ViewModelOperation _viewModel;
        public Operation()
        {
            InitializeComponent();
            _viewModel = new ViewModelOperation();

            ToolbarItems.Add(new ToolbarItem(Multilanguage.TranslateExtension.getString("saveOperation"), "", () => Save()));
            var operation = _viewModel.Operation;
            Treatment.Text = operation.Treatment;
            Surgery.Text = operation.Surgery;
        }

        private void Save()
        {
            var opToSave = _viewModel.Operation;
            opToSave.Surgery = Surgery.Text;
            opToSave.Treatment = Treatment.Text;
            _viewModel.Save();
            Navigation.PopAsync();
        }
    }
}
