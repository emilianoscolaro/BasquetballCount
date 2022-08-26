using BasquetballCount.Local.DataBase;
using BasquetballCount.Models;
using BasquetballCount.ViewModels.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BasquetballCount.ViewModels
{
    public class EditTeamsPageVireModel : BaseViewModel
    {
        #region Properties & Constructors
        private ObservableCollection<Team> _teamsList;
        private ObservableCollection<Player> _playersList;
        private string _newPlayerName;
        private int _newplyerNumber;
        private string _NewTeamName;
        private Team _selectemTeam;
        public EditTeamsPageVireModel(INavigation navigation)
        {
            Navigation = navigation;
            RegisterCommands();
            FillTeamsList();
        }
        #endregion

        #region LifeCycle Events
        #endregion

        #region Commands & Bindings
        public Command AddTeamCommand { get; set; }
        public Command<Team> TeamSelectedCommand { get; set; }
        public Command AddPlayerCommand { get; set; }
        public Command<Team> DeleteTeamCommand { get; set; }
        public ObservableCollection<Team> TeamsList
        {
            get { return _teamsList; }
            set { _teamsList = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Player> PlayersList
        {
            get { return _playersList; }
            set { _playersList = value; OnPropertyChanged(); }
        }
        public string NewTeamName
        {
            get { return _NewTeamName; }
            set { _NewTeamName = value; OnPropertyChanged(); }
        }
        public string NewPlayerName
        {
            get { return _newPlayerName; }
            set { _newPlayerName = value; OnPropertyChanged(); }
        }
        public int NewPlyerNumber
        {
            get { return _newplyerNumber; }
            set { _newplyerNumber = value; OnPropertyChanged(); }
        }
        public Team SelectemTeam
        {
            get { return _selectemTeam; }
            set { _selectemTeam = value; OnPropertyChanged(); }
        }
        #endregion

        #region Command Executions
        async void AddTeam()
        {
            await DataBase.Instance.SaveTeamAsync(new Team { Name = NewTeamName });
            FillTeamsList();
        }
        async void AddPlayer()
        {
            await DataBase.Instance.SavePlayerAsync(new Player { Name = NewPlayerName, Number = NewPlyerNumber, TeamId = SelectemTeam.Id });
        }
        async void OnTeamSelected(Team team)
        {
            var list = await DataBase.Instance.GetTeamPlayers(team.Id);
            PlayersList = new ObservableCollection<Player>(list);
        }
        async void DeleteTeam(Team team)
        {
            await DataBase.Instance.DeleteTeam(team);
            FillTeamsList();
        }
        #endregion

        #region Methods
        void RegisterCommands()
        {
            AddTeamCommand = new Command(AddTeam);
            TeamSelectedCommand = new Command<Team>(OnTeamSelected);
            AddPlayerCommand = new Command(AddPlayer);
            DeleteTeamCommand = new Command<Team>(DeleteTeam);
        }
        async void FillTeamsList()
        {
            var teamsList = await DataBase.Instance.GetTeamsAsync();
            TeamsList = new ObservableCollection<Team>(teamsList);
        }
        #endregion

    }
}
