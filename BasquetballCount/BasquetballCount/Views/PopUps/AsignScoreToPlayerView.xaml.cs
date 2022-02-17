using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BasquetballCount.ViewModels.PopUps;
using System.Collections.ObjectModel;
using BasquetballCount.Models;

namespace BasquetballCount.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsignScoreToPlayerView : PopupPage
    {
        public AsignScoreToPlayerView(ObservableCollection<Player> players , int points)
        {
            InitializeComponent();
            BindingContext = new AsignScoreToPlayerViewModel(Navigation,players ,points);
        }
    }
}