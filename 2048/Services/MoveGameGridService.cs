using _2048.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Services
{
    public class MoveGameGridService
    {
        private GameViewModel _gameViewModel;

        public MoveGameGridService(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
        }

        public void MoveToRight(int[,] gameGrid)
        {
            _gameViewModel.wykonanoRuch = false;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (gameGrid[i, k] == 0)
                        {
                            continue;
                        }
                        else if (gameGrid[i, k] == gameGrid[i, j])
                        {
                            gameGrid[i, j] *= 2;
                            _gameViewModel.Score += gameGrid[i, j];
                            gameGrid[i, k] = 0;
                            _gameViewModel.wykonanoRuch = true;
                            break;
                        }
                        else
                        {
                            if (gameGrid[i, j] == 0 && gameGrid[i, k] != 0)
                            {
                                gameGrid[i, j] = gameGrid[i, k];
                                gameGrid[i, k] = 0;
                                j++;
                                _gameViewModel.wykonanoRuch = true;
                                break;
                            }
                            else if (gameGrid[i, j] != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void MoveToLeft(int[,] gameGrid)
        {
            _gameViewModel.wykonanoRuch = false;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = j + 1; k < 4; k++)
                    {
                        if (gameGrid[i, k] == 0)
                        {
                            continue;
                        }
                        else if (gameGrid[i, k] == gameGrid[i, j])
                        {
                            gameGrid[i, j] *= 2;
                            _gameViewModel.Score += gameGrid[i, j];
                            gameGrid[i, k] = 0;
                            _gameViewModel.wykonanoRuch = true;
                            break;
                        }
                        else
                        {
                            if (gameGrid[i, j] == 0 && gameGrid[i, k] != 0)
                            {
                                gameGrid[i, j] = gameGrid[i, k];
                                gameGrid[i, k] = 0;
                                j--;
                                _gameViewModel.wykonanoRuch = true;
                                break;
                            }
                            else if (gameGrid[i, j] != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void MoveToTop(int[,] gameGrid)
        {
            _gameViewModel.wykonanoRuch = false;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int k = i + 1; k < 4; k++)
                    {
                        if (gameGrid[k, j] == 0)
                        {
                            continue;
                        }
                        else if (gameGrid[k, j] == gameGrid[i, j])
                        {
                            gameGrid[i, j] *= 2;
                            _gameViewModel.Score += gameGrid[i, j];
                            gameGrid[k, j] = 0;
                            _gameViewModel.wykonanoRuch = true;
                            break;
                        }
                        else
                        {
                            if (gameGrid[i, j] == 0 && gameGrid[k, j] != 0)
                            {
                                gameGrid[i, j] = gameGrid[k, j];
                                gameGrid[k, j] = 0;
                                i--;
                                _gameViewModel.wykonanoRuch = true;
                                break;
                            }
                            else if (gameGrid[i, j] != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void MoveToBottom(int[,] gameGrid)
        {
            _gameViewModel.wykonanoRuch = false;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 3; i >= 0; i--)
                {
                    for (int k = i - 1; k >= 0; k--)
                    {
                        if (gameGrid[k, j] == 0)
                        {
                            continue;
                        }
                        else if (gameGrid[k, j] == gameGrid[i, j])
                        {
                            gameGrid[i, j] *= 2;
                            _gameViewModel.Score += gameGrid[i, j];
                            gameGrid[k, j] = 0;
                            _gameViewModel.wykonanoRuch = true;
                            break;
                        }
                        else
                        {
                            if (gameGrid[i, j] == 0 && gameGrid[k, j] != 0)
                            {
                                gameGrid[i, j] = gameGrid[k, j];
                                gameGrid[k, j] = 0;
                                i++;
                                _gameViewModel.wykonanoRuch = true;
                                break;
                            }
                            else if (gameGrid[i, j] != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
