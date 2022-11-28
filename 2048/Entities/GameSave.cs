using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Entities
{
    public class GameSave
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        // japierdole ale patola
        public int Cell1 { get; set; }
        public int Cell2 { get; set; }
        public int Cell3 { get; set; }
        public int Cell4 { get; set; }
        public int Cell5 { get; set; }
        public int Cell6 { get; set; }
        public int Cell7 { get; set; }
        public int Cell8 { get; set; }
        public int Cell9 { get; set; }
        public int Cell10 { get; set; }
        public int Cell11 { get; set; }
        public int Cell12 { get; set; }
        public int Cell13 { get; set; }
        public int Cell14 { get; set; }
        public int Cell15 { get; set; }
        public int Cell16 { get; set; }
        // co jest glupie, ale dziala nie jest glupie
    }
}