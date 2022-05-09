using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BasquetballCount.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Rg.Plugins.Popup.Extensions;
using System.Linq;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace BasquetballCount.ViewModels.PopUps
{
    public class AsignScoreToPlayerViewModel
    {
        public INavigation Navigation = null;
        public ObservableCollection<Player> Players { get; set; }
        int Points;

        public Command<Player> AddScoreCommand { get; set; }
        public int PopUpHeight { get; set; }
        public AsignScoreToPlayerViewModel(INavigation navigation, ObservableCollection<Player> players, int points)
        {
            Navigation = navigation;
            Players = players;
            Points = points;
            AddScoreCommand = new Command<Player>(AddScore);
        }
        void AddScore(Player player)
        {
            AddScoreToPlayer(player, Points);
            Navigation.PopAllPopupAsync();
        }
        async void AddScoreToPlayer(Player intplayer, int points)
        {
            Players.Where(x => x.Name == intplayer.Name).ForEach(player => { player.Points = player.Points + points; });
            await PlayScoreSound(intplayer.Name);
            MessagingCenter.Send(this, "AddScore", Players);
        }
        async Task PlayScoreSound(string playerName)
        {
            await Task.Run(() => {
                var assembly = typeof(App).GetTypeInfo().Assembly;
                var audio = GetPlayerAudio(playerName);
                if (string.IsNullOrEmpty(audio))
                    return;
                Stream audioStream = assembly.GetManifestResourceStream($"BasquetballCount.Resources.Audios.{audio}");
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load(audioStream);
                player.Play();
            });
        }
        string GetPlayerAudio(string playerName)
        {
            switch (playerName)
            {
                case "Maty":
                    return "seagull-sound.mp3";
                case "Rashid":
                    return "harry_maguire.mp3";
            }
            return "";
        }
    }
}
