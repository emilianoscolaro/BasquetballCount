﻿using System;
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
        void AddScoreToPlayer(Player intplayer, int points)
        {
            Players.Where(x => x.Name == intplayer.Name).ForEach(player => { player.Points = player.Points + points; });
            MessagingCenter.Send(this, "AddScore", Players);
        }

    }
}