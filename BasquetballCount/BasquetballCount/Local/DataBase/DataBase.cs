using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using BasquetballCount.Models;
using System.Threading.Tasks;
using System.IO;

namespace BasquetballCount.Local.DataBase
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection _dataBase;
        readonly static string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data.db3");
        private static DataBase instance;
        public static DataBase Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DataBase(DbPath);
                }
                return instance;
            }
        }

        public DataBase(string dbPath)
        {
            _dataBase = new SQLiteAsyncConnection(dbPath);
            _dataBase.CreateTableAsync<Team>();
            _dataBase.CreateTableAsync<Player>();
        }

        #region Team
        public Task<List<Team>> GetTeamsAsync() => _dataBase.Table<Team>().ToListAsync();
        public Task<int> SaveTeamAsync(Team team)
        {
            return _dataBase.InsertAsync(team);
        }
        public Task<int> DeleteTeam(Team team)
        {
            return _dataBase.DeleteAsync(team);
        }
        #endregion

        #region Player
        public Task<List<Player>> GetPlayersAsync() => _dataBase.Table<Player>().ToListAsync();
        public Task<int> SavePlayerAsync(Player team)
        {
            return _dataBase.InsertAsync(team);
        }
        public Task<List<Player>> GetTeamPlayers(int teamId)
        {
            return _dataBase.Table<Player>().Where(x => x.TeamId == teamId).ToListAsync();
        }
        public Task<int> DeletePlayer(Player player)
        {
            return _dataBase.DeleteAsync(player);
        }
        #endregion
    }
}
