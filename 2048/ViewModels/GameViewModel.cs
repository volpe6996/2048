using _2048.Commands;
using _2048.Entities;
using _2048.Helpers;
using _2048.Services;
using _2048.Stores;
using Microsoft.IdentityModel.Tokens;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace _2048.ViewModels
{
    // TO DO:
    // Widoki
    public class GameViewModel : ViewModelBase
    {
        public int[,] GridItems = new int[4, 4];

        private List<string> _gridItemsList = new List<string>();
        public List<string> GridItemsList
        {
            get => _gridItemsList;
            set
            {
                _gridItemsList = value;
                OnPropertyChanged("GridItemsList");
            }
        }

        private List<string> _cellColorsList = new List<string>();
        public List<string> CellsColorsList
        {
            get => _cellColorsList;
            set
            {
                _cellColorsList = value;
                OnPropertyChanged(nameof(CellsColorsList));
            }
        }

        private int _score = 0;
        private int _bestScore = 0;
        private string _scoreBind;
        private string _bestScoreBind;
        public List<GameScore> gameScoresList = new List<GameScore>(); // wyniki pobrane z DB

        public string ScoreBind
        {
            get => _scoreBind;
            set 
            {
                _scoreBind = value;
                OnPropertyChanged("ScoreBind");
            }
        }

        public string BestScoreBind
        {
            get => _bestScoreBind;
            set 
            {
                _bestScoreBind = value;
                OnPropertyChanged("BestScoreBind");
            }
        }

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                ScoreBind = $"SCORE: {_score}";
            }
        }

        public int BestScore
        {
            get => _bestScore;
            set
            {
                _bestScore = value;
                BestScoreBind = $"BEST SCORE: {_bestScore}";
            }
        }

        public ICommand SwipeCommand { get; }

        public ICommand NavigateStartCommand { get; }
        public ICommand SaveGameCommand { get; }

        private RandomNumbersHelper _r = new RandomNumbersHelper();

        public bool wykonanoRuch = false;

        public GameViewModel(NavigationStore navigationStore)
        {
            GameInitialization();

            NavigateStartCommand = new NavigateCommand<StartViewModel>(navigationStore, () => new StartViewModel(navigationStore));

            SaveGameCommand = new SaveGameCommand(this);

            SwipeCommand = new SwipeCommand(this, navigationStore);
        }

        //METHODS

        // Inicjalizacja gry, w ktorej sklad wchodzi: wyswietlenie 2 początkowych liczb, wyswietlenie pustego wyniku
        public void GameInitialization()
        {
            bool IsReady = false;
            int firstIndex = _r.RandomIndex(), secondIndex = _r.RandomIndex(), thirdIndex = _r.RandomIndex(), fourthIndex = _r.RandomIndex();

            GridItems[firstIndex, secondIndex] = _r.RandomNumber();

            while (!IsReady)
            {
                // dwukrotnie została wylosowana ta sama pozycja
                if (firstIndex == thirdIndex && secondIndex == fourthIndex)
                {
                    thirdIndex = _r.RandomIndex();
                    fourthIndex = _r.RandomIndex();
                    continue;
                }
                else
                {
                    GridItems[thirdIndex, fourthIndex] = _r.RandomNumber();
                    IsReady = true;
                }
            }

            using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<GameScore>();
                gameScoresList = conn.Query<GameScore>("select Score from GameScore order by Score DESC limit 1").ToList();

                conn.Close();
            }

            ConvertToCollectionOfStrings();
            ConvertResultsToBackGroudColor();
            Score = 0;
            if(!gameScoresList.IsNullOrEmpty())
            {
                BestScore = gameScoresList[0].Score;
            }
            else
            {
                BestScore = 0;
            }
        }

        // dodanie do kolekcji stringow, ktora jest zbindowana z gridem w grze, wartosci z tablicy, na ktorej wykonujemy operacje logiczne gry
        public void ConvertToCollectionOfStrings()
        {
            List<string> tempGridList = new List<string>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (GridItems[i, j] == 0)
                    {
                        tempGridList.Add("");
                    }
                    else
                    {
                        tempGridList.Add(GridItems[i, j].ToString());
                    }
                }
            }
            GridItemsList = tempGridList; // Whole grid -> OnPropertyChanged

            ConvertResultsToBackGroudColor(); 
        }

        // Przypisanie do kafelka zgodnie z jego wartoscią koloru tła
        public void ConvertResultsToBackGroudColor()
        {
            CellColorsDictonary _cellColorsDictionary = new CellColorsDictonary();

            List<string> temp = new List<string>();

            foreach (var cell in GridItems)
            {
                foreach (var color in _cellColorsDictionary.Colors)
                {
                    if (cell == color.Key)
                        temp.Add(color.Value);
                }
            }
            CellsColorsList = temp;
        }

        // po udanym ruchu, dodaj nową liczbę i przekonwertuj wszystko do kolekcji stringow
        public void AddNumberAfterSwipe()
        {
            bool IsReady = false;
            int firstIndex = _r.RandomIndex(), secondIndex = _r.RandomIndex();

            while (!IsReady)
            {
                if (GridItems[firstIndex, secondIndex] == 0)
                {
                    GridItems[firstIndex, secondIndex] = _r.RandomNumber();
                    IsReady = true;
                    break;
                }
                else
                {
                    firstIndex = _r.RandomIndex();
                    secondIndex = _r.RandomIndex();
                }
            }
            ConvertToCollectionOfStrings();
        }

        // wstrzyknięcie danych zapisanej gry z DB 
        public void LoadGame(int[,] loadedGameGrid, int loadedScore, int loadedBestScore)
        {
            GridItems = loadedGameGrid;
            Score = loadedScore;
            BestScore = loadedBestScore;

            ConvertToCollectionOfStrings();
        }
    }
}