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

namespace BasquetballCount.ViewModels
{
    public class CountPageViewModel : BaseViewModel
    {

        Timer Time;

        private Team _homeTeam;

        public Team HomeTeam
        {
            get { return _homeTeam; }
            set { _homeTeam = value; OnPropertyChanged(); }
        }

        private Team _visitTeam;

        public Team VisitTeam
        {
            get { return _visitTeam; }
            set { _visitTeam = value; OnPropertyChanged(); }
        }
        private int _homeTeamScore;

        public int HomeTeamScore
        {
            get { return _homeTeamScore; }
            set { _homeTeamScore = value; OnPropertyChanged(); }
        }
        private int _visiteamScore;

        public int VisiteamScore
        {
            get { return _visiteamScore; }
            set { _visiteamScore = value; OnPropertyChanged(); }
        }
        private Stopwatch _gameTime;

        public Stopwatch GameTime
        {
            get { return _gameTime; }
            set { _gameTime = value; OnPropertyChanged(); }
        }
        private string _gameTimeMinutes;

        public string GameTimeMinutes
        {
            get { return _gameTimeMinutes; }
            set { _gameTimeMinutes = value; OnPropertyChanged(); }
        }
        private string _gameTimeSeconds;

        public string GameTimeSeconds
        {
            get { return _gameTimeSeconds; }
            set { _gameTimeSeconds = value; OnPropertyChanged(); }
        }
        private bool _ShowGameStats;

        public bool ShowGameStats
        {
            get { return _ShowGameStats; }
            set { _ShowGameStats = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Player> _homePlayers;

        public ObservableCollection<Player> HomePlayers
        {
            get { return _homePlayers; }
            set { _homePlayers = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Player> _awayPlayers;

        public ObservableCollection<Player> AwayPlayers
        {
            get { return _awayPlayers; }
            set { _awayPlayers = value; OnPropertyChanged(); }
        }
        private string _PlayPauseImage;

        public string PlayPauseImage
        {
            get { return _PlayPauseImage; }
            set { _PlayPauseImage = value; OnPropertyChanged(); }
        }


        public Command<string> AddScoreHomeCommand { get; set; }
        public Command<string> AddScoreAwayCommand { get; set; }
        public Command ResetTimeCommand { get; set; }
        public Command ToogleTimerCommand { get; set; }
        public Command ChangeToStatsCommand { get; set; }
        public Command ChangeToGameCommand { get; set; }

        public CountPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SetTeamsName();
            RegisterCommand();
            GameTime = new Stopwatch();
            Time = new Timer();
            PlayPauseImage = "resource://BasquetballCount.Resources.Images.play_arrow_black.svg";
            startTimer();
            FillTeamsPlayers();
            ShowGameStats = true;
            RegisterMessages();
        }

        void RegisterMessages()
        {
            UrRegisterMessages();
            MessagingCenter.Instance.Subscribe<AsignScoreToPlayerViewModel, ObservableCollection<Player>>(this, "AddScore", (sender, players) =>
            {
                if (players.FirstOrDefault().TeamId== HomeTeam.Id)
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
        void ChangeToStats()
        {
            ShowGameStats = true;
        }
        void ChangeToSGame()
        {
            ShowGameStats = false;
        }

        void FillTeamsPlayers()
        {
            HomePlayers = FillHomePlayers();
            AwayPlayers = FillAwaylayers();
        }
        ObservableCollection<Player> FillHomePlayers()
        {
            var players = new ObservableCollection<Player>();
            players.Add(new Player { Name = "Augusto", Number = 88 });
            players.Add(new Player { Name = "Juanca", Number = 10 });
            players.Add(new Player { Name = "Leci", Number = 11 });
            players.Add(new Player { Name = "Marcos", Number = 8 });
            players.Add(new Player { Name = "Gori", Number = 4 });
            players.Add(new Player { Name = "Martin", Number = 9 });
            players.Add(new Player { Name = "Marce", Number = 7 });
            players.Add(new Player { Name = "Lucas", Number = 5 });

            foreach (var player in players)
            {
                player.TeamId = HomeTeam.Id;
            }

            return players;
        }
        ObservableCollection<Player> FillAwaylayers()
        {
            var players = new ObservableCollection<Player>();
            players.Add(new Player { Name = "Emi", Number = 3 });
            players.Add(new Player { Name = "Jose", Number = 87 });
            players.Add(new Player { Name = "Rashid", Number = 10 });
            players.Add(new Player { Name = "Dani", Number = 23 });
            players.Add(new Player { Name = "Maty", Number = 4 });
            players.Add(new Player { Name = "Jr.", Number = 9 });
            players.Add(new Player { Name = "Rami", Number = 5 });

            foreach (var player in players)
            {
                player.TeamId = VisitTeam.Id;
            }

            return players;
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
        void SetTeamsName()
        {
            HomeTeam = new Team { Name = "Los Angeles de Augusto" };
            VisitTeam = new Team { Name = "Angeles Libertarios" };
        }

        void AddScoreHome(string score)
        {
            AddScore(teamLocaly.Home, int.Parse(score));
        }
        void AddScoreAway(string score)
        {
            AddScore(teamLocaly.Away, int.Parse(score));
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
                    VisiteamScore = VisiteamScore + score;
                    Navigation.PushPopupAsync(new AsignScoreToPlayerView(AwayPlayers, score));
                    break;
            }

        }

        enum teamLocaly
        {
            Home,
            Away
        }
        void ResetGameTime()
        {
            GameTime.Reset();
        }

        void startTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
             {
                 GameTimeMinutes = GameTime.Elapsed.Minutes.ToString("000");
                 GameTimeSeconds = GameTime.Elapsed.Seconds.ToString("00");
                 return true;
             });
        }
    }
}
