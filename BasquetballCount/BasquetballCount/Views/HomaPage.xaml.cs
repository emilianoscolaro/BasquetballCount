using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasquetballCount.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasquetballCount.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomaPage : ContentPage
    {
        public HomaPage()
        {
            InitializeComponent();
            BindingContext = new HomePageViewModel(Navigation);
        }
    }
}