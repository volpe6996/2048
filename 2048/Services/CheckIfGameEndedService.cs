using _2048.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Services
{
    public class CheckIfGameEndedService
    {
        public int[,] GridItemsCopy;
        public bool czyRuchMozliwy = false;

        // metoda bazuje na kopii gridu by nie zmienic wartosci oryginalengo grida
        public bool IsGameEnded(int[,] GridItemsOriginal)
        {      
            bool[] results = new bool[4];

            GridItemsCopy = Extention.CloneJson<int[,]>(GridItemsOriginal); // do skopiowania typu referencyjnego niezbędna będzie taka oto metoda rozszerzająca
            MoveToRight(GridItemsCopy); 
            results[0] = czyRuchMozliwy;

            GridItemsCopy = Extention.CloneJson<int[,]>(GridItemsOriginal);
            MoveToLeft(GridItemsCopy);
            results[1] = czyRuchMozliwy;

            GridItemsCopy = Extention.CloneJson<int[,]>(GridItemsOriginal);
            MoveToTop(GridItemsCopy);
            results[2] = czyRuchMozliwy;

            GridItemsCopy = Extention.CloneJson<int[,]>(GridItemsOriginal);
            MoveToBottom(GridItemsCopy);
            results[3] = czyRuchMozliwy;

            if (results[0] == false && results[1] == false && results[2] == false && results[3] == false)
                return true;
            else
                return false;
        }

        public void MoveToRight(int[,] gameGrid)
        {
            this.czyRuchMozliwy = false;

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
                            gameGrid[i, k] = 0;
                            this.czyRuchMozliwy = true;
                            break;
                        }
                        else
                        {
                            if (gameGrid[i, j] == 0 && gameGrid[i, k] != 0)
                            {
                                gameGrid[i, j] = gameGrid[i, k];
                                gameGrid[i, k] = 0;
                                j++;
                                this.czyRuchMozliwy = true;
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
            this.czyRuchMozliwy = false;

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
                            gameGrid[i, k] = 0;
                            this.czyRuchMozliwy = true;
                            break;
                        }
                        else
                        {
                            if (gameGrid[i, j] == 0 && gameGrid[i, k] != 0)
                            {
                                gameGrid[i, j] = gameGrid[i, k];
                                gameGrid[i, k] = 0;
                                j--;
                                this.czyRuchMozliwy = true;
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
            this.czyRuchMozliwy = false;

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
                            gameGrid[k, j] = 0;
                            this.czyRuchMozliwy = true;
                            break;
                        }
                        else
                        {
                            if (gameGrid[i, j] == 0 && gameGrid[k, j] != 0)
                            {
                                gameGrid[i, j] = gameGrid[k, j];
                                gameGrid[k, j] = 0;
                                i--;
                                this.czyRuchMozliwy = true;
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
            this.czyRuchMozliwy = false;

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
                            gameGrid[k, j] = 0;
                            this.czyRuchMozliwy = true;
                            break;
                        }
                        else
                        {
                            if (gameGrid[i, j] == 0 && gameGrid[k, j] != 0)
                            {
                                gameGrid[i, j] = gameGrid[k, j];
                                gameGrid[k, j] = 0;
                                i++;
                                this.czyRuchMozliwy = true;
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

    public static class Extention
    {
        public static T CloneJson<T>(this T source)
        {

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
    }
}
