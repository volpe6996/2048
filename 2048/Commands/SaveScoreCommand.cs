using _2048.Entities;
using _2048.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Commands
{
    public class SaveScoreCommand : CommandBase
    {
        private GameViewModel _gameViewModel;

        public SaveScoreCommand(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
        }

        public override void Execute(object? parameter)
        {
            GameScore gameScore = new GameScore()
            {
                Name = $"Wynik z dnia: {DateTime.Now:dd/MM/yyyy} {DateTime.Now:t}",
                Score = _gameViewModel.Score,
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<GameScore>();
                conn.Insert(gameScore);

                conn.Close();
            }
        }
    }
}
