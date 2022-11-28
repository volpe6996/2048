using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Helpers
{
    public class RandomNumbersHelper
    {
        public Random r = new Random();

        public int[] PossibleNumbers = new int[2] { 2, 4 };

        public int RandomIndex()
        {
            return r.Next(4);
        }

        public int RandomNumber()
        {
            if (r.Next(1, 11) == 1)
                return PossibleNumbers[1];
            else
                return PossibleNumbers[0];
        }
    }
}
