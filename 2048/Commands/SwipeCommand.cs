using _2048.Helpers;
using _2048.Services;
using _2048.Stores;
using _2048.ViewModels;
using System.Windows.Input;
using static System.Formats.Asn1.AsnWriter;

namespace _2048.Commands
{
    public class SwipeCommand : CommandBase
    {
        private GameViewModel _gameViewModel;
        private NavigationStore _naviagationStore;
        private MoveGameGridService _moveGameGridService;
        private CheckIfGameEndedService _checkIfGameEndedService;

        public ICommand NavigateStartCommand { get; set; }
        public ICommand SaveScoreCommand { get; set; }

        public SwipeCommand(GameViewModel gameViewModel, NavigationStore naviagationStore)
        {
            _gameViewModel = gameViewModel;
            _naviagationStore = naviagationStore;

            _moveGameGridService = new MoveGameGridService(_gameViewModel);

            _checkIfGameEndedService = new CheckIfGameEndedService();
        }

        public override void Execute(object? parameter)
        {
            if (_checkIfGameEndedService.IsGameEnded(_gameViewModel.GridItems))
            {
                // koniec gry -> zapisz wynik do DB -> nawigacja do STARTVIEW

                SaveScoreCommand = new SaveScoreCommand(_gameViewModel);
                SaveScoreCommand.Execute(null);

                NavigateStartCommand = new NavigateCommand<StartViewModel>(_naviagationStore, () => new StartViewModel(_naviagationStore));
                NavigateStartCommand.Execute(null);
            }
            else
            {
                if (parameter.ToString() == "Right")
                {
                    _moveGameGridService.MoveToRight(_gameViewModel.GridItems);
                }
                else if (parameter.ToString() == "Left")
                {
                    _moveGameGridService.MoveToLeft(_gameViewModel.GridItems);
                }
                else if (parameter.ToString() == "Top")
                {
                    _moveGameGridService.MoveToTop(_gameViewModel.GridItems);

                }
                else if (parameter.ToString() == "Bottom")
                {
                    _moveGameGridService.MoveToBottom(_gameViewModel.GridItems);
                }

                if (_gameViewModel.Score >= _gameViewModel.BestScore)
                    _gameViewModel.BestScore = _gameViewModel.Score;

                if (_gameViewModel.wykonanoRuch)
                    _gameViewModel.AddNumberAfterSwipe();
            }
        }
    }
}