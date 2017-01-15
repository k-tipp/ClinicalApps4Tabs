using SmaNa.ViewModel;

using Xamarin.Forms;

namespace SmaNa.View
{

    /// <summary>
    /// Page to edit the Operation Data
    /// @created: Michel Murbach
    /// </summary>
    public partial class Operation : ContentPage
    {
        private ViewModelOperation _viewModel;
        public Operation()
        {
            InitializeComponent();
            _viewModel = new ViewModelOperation();

            ToolbarItems.Add(new ToolbarItem(Multilanguage.TranslateExtension.getString("saveOperation"), "", () => Save()));
            var operation = _viewModel.Operation;
            Diagnose.Text = operation.Diagnose;
            Surgery.Text = operation.Surgery;
        }

        private void Save()
        {
            var opToSave = _viewModel.Operation;
            opToSave.Surgery = Surgery.Text;
            opToSave.Diagnose = Diagnose.Text;
            _viewModel.Save();
            Navigation.PopAsync();
        }
    }
}
