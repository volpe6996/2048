using _2048.Commands;
using _2048.Entities;
using _2048.Stores;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2048.ViewModels
{
    public class ScoreboardViewModel : ViewModelBase
    {
        private List<GameScore> _gameScores;
        public List<GameScore> GameScores
        {
            get => _gameScores;
            set
            {
                _gameScores = value;
                OnPropertyChanged(nameof(GameScores));
            }
        }

        public ICommand NavigateStartCommand { get; }

        public ScoreboardViewModel(NavigationStore navigationStore)
        {
            NavigateStartCommand = new NavigateCommand<StartViewModel>(navigationStore, () => new StartViewModel(navigationStore));

            using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<GameScore>();
                GameScores = conn.Query<GameScore>("select Name, Score from GameScore order by Score DESC").ToList();


                conn.Close();
            }
        }
    }
}
