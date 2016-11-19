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
        public Operation()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Hauptmenu");
        }
    }
}
