using _2048.Entities;
using _2048.ViewModels;
using _2048.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Commands
{
    public class SaveGameCommand : CommandBase
    {
        private GameViewModel _gameViewModel;

        public SaveGameCommand(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
        }

        public override void Execute(object? parameter)
        {

            GameSave gameSave = new GameSave()
            {
                Name = $"Zapis z dnia: {DateTime.Now:dd/MM/yyyy} {DateTime.Now:t}",
                Score = _gameViewModel.Score,

                Cell1 = _gameViewModel.GridItems[0, 0],
                Cell2 = _gameViewModel.GridItems[0, 1],
                Cell3 = _gameViewModel.GridItems[0, 2],
                Cell4 = _gameViewModel.GridItems[0, 3],
                Cell5 = _gameViewModel.GridItems[1, 0],
                Cell6 = _gameViewModel.GridItems[1, 1],
                Cell7 = _gameViewModel.GridItems[1, 2],
                Cell8 = _gameViewModel.GridItems[1, 3],
                Cell9 = _gameViewModel.GridItems[2, 0],
                Cell10 = _gameViewModel.GridItems[2, 1],
                Cell11 = _gameViewModel.GridItems[2, 2],
                Cell12 = _gameViewModel.GridItems[2, 3],
                Cell13 = _gameViewModel.GridItems[3, 0],
                Cell14 = _gameViewModel.GridItems[3, 1],
                Cell15 = _gameViewModel.GridItems[3, 2],
                Cell16 = _gameViewModel.GridItems[3, 3],
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<GameSave>();
                conn.Insert(gameSave);

                conn.Close();
            }
        }
    }
}
