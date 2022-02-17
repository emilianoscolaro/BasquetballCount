using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BasquetballCount.Views;

namespace BasquetballCount
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CountPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
