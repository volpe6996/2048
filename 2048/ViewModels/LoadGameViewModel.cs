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
    public class LoadGameViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private GameViewModel _gameViewModel;

        public ICommand NavigateStartCommand { get; }
        public ICommand NavigateGameCommand { get; set; }

        public int[,] GridItems = new int[4, 4];
        public List<GameScore> BestScore;

        private List<GameSave> _gameSaves;
        public List<GameSave> GameSaves
        {
            get => _gameSaves;
            set
            {
                _gameSaves = value;
                OnPropertyChanged(nameof(GameSaves));
            }
        }

        public LoadGameViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _gameViewModel = new GameViewModel(_navigationStore);

            using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<GameSave>();
                GameSaves = conn.Table<GameSave>().ToList();

                BestScore = conn.Query<GameScore>("select Score from GameScore order by Score DESC limit 1").ToList();

                conn.Close();
            }

            NavigateStartCommand = new NavigateCommand<StartViewModel>(navigationStore, () => new StartViewModel(navigationStore));

            NavigateGameCommand = new NavigateCommand<GameViewModel>(_navigationStore, () => _gameViewModel);
        }

        public ICommand LoadSelectedGameCommand
        {
            get { return new DelegateCommand(new Action<object>(LoadSelecredGame)); }
        }

        public void LoadSelecredGame(object selectedRow)
        {
            int[,] temporaryGridItemsArray = new int[4, 4];
            GameSave gameSave = selectedRow as GameSave;

            temporaryGridItemsArray[0, 0] = gameSave.Cell1;
            temporaryGridItemsArray[0, 1] = gameSave.Cell2;
            temporaryGridItemsArray[0, 2] = gameSave.Cell3;
            temporaryGridItemsArray[0, 3] = gameSave.Cell4;
            temporaryGridItemsArray[1, 0] = gameSave.Cell5;
            temporaryGridItemsArray[1, 1] = gameSave.Cell6;
            temporaryGridItemsArray[1, 2] = gameSave.Cell7;
            temporaryGridItemsArray[1, 3] = gameSave.Cell8;
            temporaryGridItemsArray[2, 0] = gameSave.Cell9;
            temporaryGridItemsArray[2, 1] = gameSave.Cell10;
            temporaryGridItemsArray[2, 2] = gameSave.Cell11;
            temporaryGridItemsArray[2, 3] = gameSave.Cell12;
            temporaryGridItemsArray[3, 0] = gameSave.Cell13;
            temporaryGridItemsArray[3, 1] = gameSave.Cell14;
            temporaryGridItemsArray[3, 2] = gameSave.Cell15;
            temporaryGridItemsArray[3, 3] = gameSave.Cell16;

            _gameViewModel.LoadGame(temporaryGridItemsArray, gameSave.Score, BestScore[0].Score);

            NavigateGameCommand.Execute(null);
        }
    }
}