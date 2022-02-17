using System;
using System.Collections.Generic;
using System.Text;

namespace BasquetballCount.Models
{
    public class Player
    {
        public string Name { get; set; }
        public Team Team { get; set; }
        public int Number { get; set; }
        public int Points { get; set; }
        public int Rebounds { get; set; }
        public int Asist { get; set; }
    }
}
