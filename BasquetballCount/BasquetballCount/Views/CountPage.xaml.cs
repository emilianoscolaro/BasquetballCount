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
    public partial class CountPage : ContentPage
    {
        public CountPage()
        {
            InitializeComponent();
            BindingContext = new CountPageViewModel(Navigation);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ShowStats();
        }
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            ShowGame();
        }
        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            ShowStats();
        }
        private void SwipeGestureRecognizer_Swiped_1(object sender, SwipedEventArgs e)
        {
            ShowGame();
        }
        void ShowStats()
        {
            GameTab.TextColor = Color.WhiteSmoke;
            GameTab.FontAttributes = FontAttributes.Bold;
            ScoreTab.TextColor = Color.Black;
            ScoreTab.FontAttributes = FontAttributes.None;
        }
        void ShowGame()
        {
            GameTab.TextColor = Color.Black;
            GameTab.FontAttributes = FontAttributes.None;
            ScoreTab.TextColor = Color.WhiteSmoke;
            ScoreTab.FontAttributes = FontAttributes.Bold;
        }
    }
}