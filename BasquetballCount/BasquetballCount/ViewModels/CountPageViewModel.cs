using BasquetballCount.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using BasquetballCount.Views.PopUps;
using Rg.Plugins.Popup.Extensions;
using BasquetballCount.ViewModels.PopUps;
using BasquetballCount.ViewModels.BaseViewModels;
using BasquetballCount.Local.DataBase;

namespace BasquetballCount.ViewModels
{
    public class CountPageViewModel : BaseViewModel
    {
        #region Properties & Constructors
        Timer Time;
        private Team _homeTeam;
        private Team _awayTeam;
        private int _homeTeamScore;
        private int _awayTeamScore;
        private Stopwatch _gameTime;
        private string _gameTimeMinutes;
        private string _gameTimeSeconds;
        private bool _ShowGameStats;
        private ObservableCollection<Player> _homePlayers;
        private ObservableCollection<Player> _awayPlayers;
        private string _PlayPauseImage;

        public CountPageViewModel(INavigation navigation, Team homeTeam, Team awayTeam)
        {
            Navigation = navigation;
            RegisterCommand();
            GameTime = new Stopwatch();
            Time = new Timer();
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            PlayPauseImage = "resource://BasquetballCount.Resources.Images.play_arrow_black.svg";
            startTimer();
            FillTeamsPlayers();
            ShowGameStats = true;
            RegisterMessages();
        }
        #endregion

        #region LifeCycle Events
        #endregion

        #region Commands & Bindings
        public Command<string> AddScoreHomeCommand { get; set; }
        public Command<string> AddScoreAwayCommand { get; set; }
        public Command ResetTimeCommand { get; set; }
        public Command ToogleTimerCommand { get; set; }
        public Command ChangeToStatsCommand { get; set; }
        public Command ChangeToGameCommand { get; set; }
        public Team HomeTeam
        {
            get { return _homeTeam; }
            set { _homeTeam = value; OnPropertyChanged(); }
        }
        public Team AwayTeam
        {
            get { return _awayTeam; }
            set { _awayTeam = value; OnPropertyChanged(); }
        }
        public int HomeTeamScore
        {
            get { return _homeTeamScore; }
            set { _homeTeamScore = value; OnPropertyChanged(); }
        }
        public int AwayTeamScore
        {
            get { return _awayTeamScore; }
            set { _awayTeamScore = value; OnPropertyChanged(); }
        }
        public Stopwatch GameTime
        {
            get { return _gameTime; }
            set { _gameTime = value; OnPropertyChanged(); }
        }
        public string GameTimeMinutes
        {
            get { return _gameTimeMinutes; }
            set { _gameTimeMinutes = value; OnPropertyChanged(); }
        }
        public string GameTimeSeconds
        {
            get { return _gameTimeSeconds; }
            set { _gameTimeSeconds = value; OnPropertyChanged(); }
        }
        public bool ShowGameStats
        {
            get { return _ShowGameStats; }
            set { _ShowGameStats = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Player> HomePlayers
        {
            get { return _homePlayers; }
            set { _homePlayers = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Player> AwayPlayers
        {
            get { return _awayPlayers; }
            set { _awayPlayers = value; OnPropertyChanged(); }
        }
        public string PlayPauseImage
        {
            get { return _PlayPauseImage; }
            set { _PlayPauseImage = value; OnPropertyChanged(); }
        }
        #endregion

        #region Command Executions
        void ChangeToStats()
        {
            ShowGameStats = true;
        }
        void ChangeToSGame()
        {
            ShowGameStats = false;
        }
        void AddScoreHome(string score)
        {
            AddScore(teamLocaly.Home, int.Parse(score));
        }
        void AddScoreAway(string score)
        {
            AddScore(teamLocaly.Away, int.Parse(score));
        }
        void ToogleTimer()
        {
            if (GameTime.IsRunning)
            {
                GameTime.Stop();
                PlayPauseImage = "resource://BasquetballCount.Resources.Images.play_arrow_black.svg";
            }
            else
            {
                GameTime.Start();
                PlayPauseImage = "resource://BasquetballCount.Resources.Images.pause_black.svg";
            }
        }
        void ResetGameTime()
        {
            GameTime.Reset();
        }
        #endregion

        #region Methods
        void RegisterMessages()
        {
            UrRegisterMessages();
            MessagingCenter.Instance.Subscribe<AsignScoreToPlayerViewModel, ObservableCollection<Player>>(this, "AddScore", (sender, players) =>
            {
                if (players.FirstOrDefault().TeamId == HomeTeam.Id)
                {
                    HomePlayers = players;
                    HomePlayers = new ObservableCollection<Player>(HomePlayers.OrderByDescending(i => i.Points));
                }
                else
                {
                    AwayPlayers = players;
                    AwayPlayers = new ObservableCollection<Player>(AwayPlayers.OrderByDescending(i => i.Points));
                }
            });
        }
        void UrRegisterMessages()
        {
            MessagingCenter.Instance.Unsubscribe<AsignScoreToPlayerViewModel, ObservableCollection<Player>>(this, "AddScore");
        }
        void RegisterCommand()
        {
            AddScoreAwayCommand = new Command<string>(AddScoreAway);
            AddScoreHomeCommand = new Command<string>(AddScoreHome);
            ResetTimeCommand = new Command(ResetGameTime);
            ToogleTimerCommand = new Command(ToogleTimer);
            ChangeToStatsCommand = new Command(ChangeToStats);
            ChangeToGameCommand = new Command(ChangeToSGame);
        }
        async void FillTeamsPlayers()
        {
            HomePlayers = new ObservableCollection<Player>(await DataBase.Instance.GetTeamPlayers(HomeTeam.Id));
            AwayPlayers = new ObservableCollection<Player>(await DataBase.Instance.GetTeamPlayers(AwayTeam.Id));
        }

        void AddScore(teamLocaly team, int score)
        {
            switch (team)
            {
                case teamLocaly.Home:
                    HomeTeamScore = HomeTeamScore + score;
                    Navigation.PushPopupAsync(new AsignScoreToPlayerView(HomePlayers, score));
                    break;
                case teamLocaly.Away:
                    AwayTeamScore = AwayTeamScore + score;
                    Navigation.PushPopupAsync(new AsignScoreToPlayerView(AwayPlayers, score));
                    break;
            }
        }
        void startTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                GameTimeMinutes = GameTime.Elapsed.Minutes.ToString("00");
                GameTimeSeconds = GameTime.Elapsed.Seconds.ToString("00");
                return true;
            });
        }
        #endregion

        enum teamLocaly
        {
            Home,
            Away
        }
    }
}
