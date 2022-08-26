using BasquetballCount.ViewModels.BaseViewModels;
using BasquetballCount.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BasquetballCount.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region Properties & Constructors
        public HomePageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            RegisterCommands();
        }
        #endregion

        #region LifeCycle Events
        #endregion

        #region Commands & Bindings
        public Command GoToCountPageCommand { get; set; }
        public Command GoToEditTeamsCommand { get; set; }
        #endregion

        #region Command Executions
        async void GoToCountPage()
        {
            await Navigation.PushAsync(new CountPage());
        }
        async void GoToEditTeams()
        {
            await Navigation.PushAsync(new EditTeamsPage());
        }
        #endregion

        #region Methods
        void RegisterCommands()
        {
            GoToCountPageCommand = new Command(GoToCountPage);
            GoToEditTeamsCommand = new Command(GoToEditTeams);
        }
        #endregion

    }
}
