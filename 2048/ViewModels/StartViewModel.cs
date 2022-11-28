using _2048.Commands;
using _2048.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2048.ViewModels
{
    public class StartViewModel : ViewModelBase
    {
        public ICommand NavigateGameCommand { get; }
        public ICommand NavigateLoadGameCommand { get; }
        public ICommand NavigateScoreboardCommand { get; }

        public StartViewModel(NavigationStore navigationStore)
        {
            NavigateGameCommand = new NavigateCommand<GameViewModel>(navigationStore, () => new GameViewModel(navigationStore));

            NavigateLoadGameCommand = new NavigateCommand<LoadGameViewModel>(navigationStore, () => new LoadGameViewModel(navigationStore));

            NavigateScoreboardCommand = new NavigateCommand<ScoreboardViewModel>(navigationStore, () => new ScoreboardViewModel(navigationStore));
        }
    }
}
