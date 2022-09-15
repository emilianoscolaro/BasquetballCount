using BasquetballCount.Local.DataBase;
using BasquetballCount.Models;
using BasquetballCount.ViewModels.BaseViewModels;
using BasquetballCount.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BasquetballCount.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region Properties & Constructors
        private Team _selectedHomeTeam;
        private Team _selectedAwayTeam;
        private List<Team> _homeTeams;
        private List<Team> _awayTeams;


        public HomePageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            RegisterCommands();
            FillTeamsList();
        }
        #endregion

        #region LifeCycle Events
        #endregion

        #region Commands & Bindings
        public Command GoToCountPageCommand { get; set; }
        public Command GoToEditTeamsCommand { get; set; }
        public Team SelectedHomeTeam
        {
            get { return _selectedHomeTeam; }
            set { _selectedHomeTeam = value; OnPropertyChanged(); }
        }
        public Team SelectedAwayTeam
        {
            get { return _selectedAwayTeam; }
            set { _selectedAwayTeam = value; OnPropertyChanged(); }
        }
        public List<Team> HomeTeams
        {
            get { return _homeTeams; }
            set { _homeTeams = value; OnPropertyChanged(); }
        }
        public List<Team> AwayTeams
        {
            get { return _awayTeams; }
            set { _awayTeams = value; OnPropertyChanged(); }
        }


        #endregion

        #region Command Executions
        async void GoToCountPage()
        {
            await Navigation.PushAsync(new CountPage(SelectedHomeTeam, SelectedAwayTeam));
        }
        async void GoToEditTeams()
        {
            await Navigation.PushAsync(new EditTeamsPage());
        }
        async void FillTeamsList()
        {
            HomeTeams = await DataBase.Instance.GetTeamsAsync();
            AwayTeams = HomeTeams;
            SelectedHomeTeam = HomeTeams.FirstOrDefault();
            SelectedAwayTeam = AwayTeams.LastOrDefault();
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
